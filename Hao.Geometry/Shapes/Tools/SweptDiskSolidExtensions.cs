using System;
using System.Collections.Generic;
using Hao.Geometry.Topology;

namespace Hao.Geometry.Shapes.Tools
{
	// Token: 0x02000005 RID: 5
	public static class SweptDiskSolidExtensions
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000026EC File Offset: 0x000008EC
		public static FacetedBRep ConvertToFacetedBRep(this SweptDiskSolid sweptDiskSolid, Angle tesselation, double epsilon = 1E-05)
		{
			Wire3 wire = sweptDiskSolid.Wire;
			if (wire.Edges.Count == 0 || wire.ContinuousError > epsilon || tesselation.Radians < epsilon)
			{
				return null;
			}
			IEdge3 edge = wire.Edges[0];
			AffineTransform3 affineTransform = Transform3Factory.CreateOrthonormalBasis(edge.StartDirection);
			AffineTransform3 transform = new AffineTransform3(affineTransform.AxisX, affineTransform.AxisY, affineTransform.AxisZ, edge.StartPoint);
			IList<Vector3> sweepContour = SweptDiskSolidExtensions.CreateSweepContour(Math.Round(Angle.FullCircle / tesselation, MidpointRounding.AwayFromZero), sweptDiskSolid.Radius);
			List<Vector3> list = new List<Vector3>();
			List<int[]> list2 = new List<int[]>();
			SweptDiskSolidExtensions.AddStartFace(list, list2, sweepContour, transform);
			foreach (IEdge3 edge2 in wire.Edges)
			{
				SweptDiskSolidExtensions.CreateTesselatedEdge(list, list2, sweepContour, tesselation, ref transform, edge2);
			}
			SweptDiskSolidExtensions.AddEndFace(list, list2, sweepContour);
			return new FacetedBRep(list.ToArray(), list2.ToArray(), new Dictionary<int, int[][]>());
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002808 File Offset: 0x00000A08
		private static IList<Vector3> CreateSweepContour(double diskVertexCount, double radius)
		{
			int num = (int)diskVertexCount;
			List<Vector3> list = new List<Vector3>(num);
			for (int i = 0; i < num; i++)
			{
				Angle angle = -Angle.FullCircle * ((double)i / diskVertexCount);
				Vector3 left = UnitVector3.UnitX * angle.Sin;
				Vector3 right = UnitVector3.UnitY * angle.Cos;
				Vector3 item = (left + right) * radius;
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000287C File Offset: 0x00000A7C
		private static void CreateTesselatedEdge(List<Vector3> vertices, List<int[]> wires, IList<Vector3> sweepContour, Angle curveTesselation, ref AffineTransform3 transform, IEdge3 edge)
		{
			EdgeLineSegment3 edgeLineSegment = edge as EdgeLineSegment3;
			if (edgeLineSegment != null)
			{
				SweptDiskSolidExtensions.CreateTesselatedEdgeLine(vertices, wires, sweepContour, ref transform, edgeLineSegment);
				return;
			}
			EdgeArc3 edgeArc = edge as EdgeArc3;
			if (edgeArc != null)
			{
				SweptDiskSolidExtensions.CreateTesselatedEdgeArc(vertices, wires, sweepContour, curveTesselation, ref transform, edgeArc);
				return;
			}
			EdgePolyLineSegment3 edgePolyLineSegment = edge as EdgePolyLineSegment3;
			if (edgePolyLineSegment != null)
			{
				SweptDiskSolidExtensions.CreateTesselatedEdgePolyLine(vertices, wires, sweepContour, ref transform, edgePolyLineSegment);
				return;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000028D0 File Offset: 0x00000AD0
		private static void CreateTesselatedEdgeLine(List<Vector3> vertices, List<int[]> wires, IList<Vector3> sweepContour, ref AffineTransform3 transform, EdgeLineSegment3 edgeLineSegment)
		{
			int num = vertices.Count - sweepContour.Count;
			int count = vertices.Count;
			transform = new AffineTransform3(transform.AxisX, transform.AxisY, transform.AxisZ, transform.Origin + edgeLineSegment.Segment.Direction * (edgeLineSegment.Segment.Extent * 2.0));
			int num2 = 0;
			foreach (Vector3 input in sweepContour)
			{
				int num3 = (num2 + 1) % sweepContour.Count;
				vertices.Add(transform.Transform(input));
				int[] item = new int[]
				{
					num + num2,
					num + num3,
					count + num3,
					count + num2
				};
				wires.Add(item);
				num2++;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000029D0 File Offset: 0x00000BD0
		private static void CreateTesselatedEdgeArc(List<Vector3> vertices, List<int[]> wires, IList<Vector3> sweepContour, Angle curveTesselation, ref AffineTransform3 transform, EdgeArc3 edgeArc)
		{
			if (edgeArc.Arc.DeltaAngle.Radians < 0.001)
			{
				return;
			}
			if (edgeArc.Arc.StartAngle.Radians != 0.0)
			{
				throw new NotSupportedException("The start angle of the arc should be zero.");
			}
			int num = Math.Max(1, (int)(Math.Abs(edgeArc.Arc.DeltaAngle.Radians) / curveTesselation.Radians + 0.9));
			for (int i = 0; i < num; i++)
			{
				AffineTransform3 transform2 = SweptDiskSolidExtensions.CreateSegmentTransform(transform, edgeArc, i, num);
				SweptDiskSolidExtensions.CreateTransformedSweep(vertices, sweepContour, transform2);
				SweptDiskSolidExtensions.BuildWires(vertices, wires, sweepContour.Count);
			}
			AffineTransform3 transformer = Transform3Factory.CreateFromAxisAngle(edgeArc.Arc.Circle.Normal, edgeArc.Arc.DeltaAngle, edgeArc.Arc.Circle.Center);
			transform = transformer.Transform(transform);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002AF8 File Offset: 0x00000CF8
		private static void CreateTransformedSweep(List<Vector3> vertices, IList<Vector3> sweepContour, AffineTransform3 transform)
		{
			foreach (Vector3 input in sweepContour)
			{
				vertices.Add(transform.Transform(input));
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002B48 File Offset: 0x00000D48
		private static AffineTransform3 CreateSegmentTransform(AffineTransform3 transform, EdgeArc3 edgeArc, int segmentIndex, int segmentCount)
		{
			if (segmentIndex + 1 == segmentCount)
			{
				AffineTransform3 affineTransform = Transform3Factory.CreateFromAxisAngle(edgeArc.Arc.Circle.Normal, edgeArc.Arc.DeltaAngle, edgeArc.Arc.Circle.Center).Transform(transform);
				UnitVector3 normalized = (edgeArc.Arc.Circle.UnitV * edgeArc.Arc.DeltaAngle.Cos - edgeArc.Arc.Circle.UnitU * edgeArc.Arc.DeltaAngle.Sin).GetNormalized();
				UnitVector3 axisY = normalized.UnitCross(affineTransform.AxisX);
				return new AffineTransform3(axisY.UnitCross(normalized), axisY, normalized, edgeArc.Arc.EndPoint);
			}
			Angle angle = edgeArc.Arc.DeltaAngle * (((double)segmentIndex + 1.0) / (double)segmentCount);
			return Transform3Factory.CreateFromAxisAngle(edgeArc.Arc.Circle.Normal, angle, edgeArc.Arc.Circle.Center).Transform(transform);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002CB4 File Offset: 0x00000EB4
		private static void CreateTesselatedEdgePolyLine(List<Vector3> vertices, List<int[]> wires, IList<Vector3> sweepContour, ref AffineTransform3 transform, EdgePolyLineSegment3 edgePolyLine)
		{
			UnitVector3 unitVector = transform.AxisZ;
			Vector3 vector = (Vector3)transform.AxisX;
			IList<Vector3> vertices2 = edgePolyLine.Vertices;
			for (int i = 0; i < vertices2.Count - 2; i++)
			{
				Vector3 right = vertices2[i + 0];
				Vector3 vector2 = vertices2[i + 1];
				Vector3 left = vertices2[i + 2];
				Vector3 vector3 = vector2 - right;
				Vector3 vector4 = left - vector2;
				UnitVector3 unitVector2;
				UnitVector3 unitVector3;
				if (vector3.TryGetNormalized(out unitVector2) && vector4.TryGetNormalized(out unitVector3))
				{
					unitVector = unitVector3;
					UnitVector3 planeNormal;
					if (((Vector3)unitVector2 + (Vector3)unitVector3).TryGetNormalized(out planeNormal))
					{
						Plane3 plane = new Plane3(planeNormal, vector2);
						SweptDiskSolidExtensions.ProjectToPlane(vertices, sweepContour.Count, unitVector2, plane);
						SweptDiskSolidExtensions.BuildWires(vertices, wires, sweepContour.Count);
						double scalar = plane.Normal.Dot(vector);
						vector -= plane.Normal * scalar;
					}
				}
			}
			UnitVector3 unitVector4 = unitVector;
			UnitVector3 axisY;
			UnitVector3 axisX;
			if (unitVector4.TryGetUnitCross(vector, out axisY) && axisY.TryGetUnitCross(unitVector4, out axisX))
			{
				IList<Vector3> list = vertices2;
				Vector3 origin = list[list.Count - 1];
				transform = new AffineTransform3(axisX, axisY, unitVector4, origin);
				SweptDiskSolidExtensions.CreateTransformedSweep(vertices, sweepContour, transform);
				SweptDiskSolidExtensions.BuildWires(vertices, wires, sweepContour.Count);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002E10 File Offset: 0x00001010
		private static void ProjectToPlane(List<Vector3> vertices, int sweepCount, UnitVector3 direction, Plane3 plane)
		{
			int num = vertices.Count - sweepCount;
			for (int i = 0; i < sweepCount; i++)
			{
				Ray3 ray = new Ray3(vertices[num + i], direction);
				Vector3? vector = plane.IntersectionWith(ray);
				vertices.Add((vector != null) ? vector.Value : vertices[num + i]);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002E6C File Offset: 0x0000106C
		private static void AddStartFace(List<Vector3> vertices, List<int[]> wires, IList<Vector3> sweepContour, AffineTransform3 transform)
		{
			int[] array = new int[sweepContour.Count];
			int count = vertices.Count;
			int num = 0;
			foreach (Vector3 input in sweepContour)
			{
				vertices.Add(transform.Transform(input));
				array[num++] = count++;
			}
			wires.Add(array);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002EE4 File Offset: 0x000010E4
		private static void AddEndFace(List<Vector3> vertices, List<int[]> wires, IList<Vector3> sweepContour)
		{
			int[] array = new int[sweepContour.Count];
			int num = vertices.Count - 1;
			for (int i = 0; i < sweepContour.Count; i++)
			{
				array[i] = num--;
			}
			wires.Add(array);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002F28 File Offset: 0x00001128
		private static void BuildWires(List<Vector3> vertices, List<int[]> wires, int sweepContourCount)
		{
			int num = vertices.Count - sweepContourCount;
			int num2 = num - sweepContourCount;
			for (int i = 0; i < sweepContourCount; i++)
			{
				int num3 = (i + 1) % sweepContourCount;
				int[] item = new int[]
				{
					num2 + i,
					num2 + num3,
					num + num3,
					num + i
				};
				wires.Add(item);
			}
		}
	}
}
