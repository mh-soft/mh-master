using System;
using System.Collections.Generic;
using LibTessDotNet;
using Hao.Geometry.Algorithms;
using Hao.Geometry.Shapes.Zkit;

namespace Hao.Geometry.Shapes.Tools
{
	// Token: 0x02000006 RID: 6
	public static class FacetedBRepTesselationExtensions
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002F7C File Offset: 0x0000117C
		public static TriangleMesh CreateTriangleMesh(this FacetedBRep facetedBRep)
		{
			List<TriangleMesh.EdgeLine> edgeLines = new List<TriangleMesh.EdgeLine>();
			List<TriangleMesh.SurfaceLine> surfaceLines = new List<TriangleMesh.SurfaceLine>();
			List<TriangleMesh.Line> lines = new List<TriangleMesh.Line>();
			List<TriangleMesh.Triangle> list = new List<TriangleMesh.Triangle>();
			List<TriangleMesh.ReferenceLine> referenceLines = new List<TriangleMesh.ReferenceLine>();
			double referenceLineRadius = 0.0;
			Dictionary<Tuple<int, int>, int> dictionary = new Dictionary<Tuple<int, int>, int>();
			int num = 0;
			KdTree3 kdTree = new KdTree3(1E-05);
			Vector3 vector = facetedBRep.Vertices[0];
			List<TriangleMesh.Vertex> list2 = new List<TriangleMesh.Vertex>(facetedBRep.Vertices.Count);
			foreach (Vector3 left in facetedBRep.Vertices)
			{
				Vector3 vector2 = left - vector;
				list2.Add(new TriangleMesh.Vertex((float)vector2.X, (float)vector2.Y, (float)vector2.Z));
			}
			foreach (int[] array in facetedBRep.OuterWires)
			{
				Tess tess = new Tess();
				Vector3 left2 = facetedBRep.Vertices[array[0]];
				Vector3 right = facetedBRep.Vertices[array[1]];
				Vector3 left3 = facetedBRep.Vertices[array[2]];
				Vector3 vector3 = left2 - right;
				Vector3 vector4 = left3 - right;
				UnitVector3 unitZ;
				if (!vector3.TryGetUnitCross(vector4, out unitZ))
				{
					unitZ = UnitVector3.UnitZ;
				}
				int normalIndex = kdTree.Insert((Vector3)unitZ);
				List<ContourVertex> list3 = new List<ContourVertex>();
				foreach (int num2 in array)
				{
					Vector3 vector5 = facetedBRep.Vertices[num2];
					Vec3 vec = default(Vec3);
					vec.X = (float)(vector5.X - vector.X);
					vec.Y = (float)(vector5.Y - vector.Y);
					vec.Z = (float)(vector5.Z - vector.Z);
					Vec3 position = vec;
					ContourVertex contourVertex = default(ContourVertex);
					contourVertex.Position = position;
					contourVertex.Data = num2;
					ContourVertex item = contourVertex;
					list3.Add(item);
				}
				tess.AddContour(list3.ToArray());
				tess.Tessellate(0, 0, 3);
				for (int k = 0; k < tess.ElementCount; k++)
				{
					int num3 = tess.Elements[k * 3 + 0];
					int num4 = tess.Elements[k * 3 + 1];
					int num5 = tess.Elements[k * 3 + 2];
					int pointIndex = (int)tess.Vertices[num3].Data;
					int pointIndex2 = (int)tess.Vertices[num4].Data;
					int pointIndex3 = (int)tess.Vertices[num5].Data;
					int corner = FacetedBRepTesselationExtensions.GetCorner(normalIndex, pointIndex, dictionary, ref num);
					int corner2 = FacetedBRepTesselationExtensions.GetCorner(normalIndex, pointIndex2, dictionary, ref num);
					int corner3 = FacetedBRepTesselationExtensions.GetCorner(normalIndex, pointIndex3, dictionary, ref num);
					list.Add(new TriangleMesh.Triangle((ushort)corner, (ushort)corner2, (ushort)corner3));
				}
			}
			TriangleMesh.Corner[] array3 = new TriangleMesh.Corner[dictionary.Count];
			foreach (KeyValuePair<Tuple<int, int>, int> keyValuePair in dictionary)
			{
				int value = keyValuePair.Value;
				array3[value] = new TriangleMesh.Corner((ushort)keyValuePair.Key.Item1, (ushort)keyValuePair.Key.Item2);
			}
			List<TriangleMesh.Normal> list4 = new List<TriangleMesh.Normal>();
			foreach (Vector3 vector6 in kdTree.Vertices)
			{
				list4.Add(new TriangleMesh.Normal((float)vector6.X, (float)vector6.Y, (float)vector6.Z));
			}
			return new TriangleMesh(vector, list4, list2, edgeLines, surfaceLines, lines, array3, list, referenceLines, referenceLineRadius);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000337C File Offset: 0x0000157C
		private static int GetCorner(int normalIndex, int pointIndex0, Dictionary<Tuple<int, int>, int> cornerIndex, ref int cornerCounter)
		{
			Tuple<int, int> key = new Tuple<int, int>(normalIndex, pointIndex0);
			int num;
			if (!cornerIndex.TryGetValue(key, out num))
			{
				int num2 = cornerCounter;
				cornerCounter = num2 + 1;
				num = num2;
				cornerIndex.Add(key, num);
			}
			return num;
		}
	}
}
