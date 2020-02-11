using System;
using System.Collections.Generic;

namespace Hao.Geometry.Shapes.Tools
{
	// Token: 0x02000002 RID: 2
	public class FacetedBRepEdgeBuilder
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public FacetedBRepEdgeBuilder(double featureAngle)
		{
			this.FeatureAngle = featureAngle;
			if (this.FeatureAngle < 0.0)
			{
				this.FeatureAngle = 0.0;
				return;
			}
			if (this.FeatureAngle > 3.1415926535897931)
			{
				this.FeatureAngle = 3.1415926535897931;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020AC File Offset: 0x000002AC
		public void Build(FacetedBRep brep)
		{
			this.Edges = new List<FacetedBRepEdgeBuilder.Edge>();
			this.Faces = new List<List<int>>();
			int count = brep.Faces.Count;
			Dictionary<FacetedBRepEdgeBuilder.Edge, Tuple<int, int>> dictionary = new Dictionary<FacetedBRepEdgeBuilder.Edge, Tuple<int, int>>();
			List<UnitVector3> list = new List<UnitVector3>();
			int num = 0;
			foreach (FacetedBRepFace facetedBRepFace in brep.Faces)
			{
				UnitVector3 polygonNormal = this.getPolygonNormal(facetedBRepFace.Vertices);
				list.Add(polygonNormal);
				int count2 = facetedBRepFace.VerticeIndexes.Count;
				for (int i = 0; i < count2; i++)
				{
					int index = (i + 1) % count2;
					FacetedBRepEdgeBuilder.Edge edge = new FacetedBRepEdgeBuilder.Edge(facetedBRepFace.VerticeIndexes[i], facetedBRepFace.VerticeIndexes[index]);
					this.addFace(dictionary, edge, num);
				}
				if (facetedBRepFace.HasHoles)
				{
					foreach (FacetedBRepFaceHole facetedBRepFaceHole in facetedBRepFace.Holes)
					{
						int count3 = facetedBRepFaceHole.VerticeIndexes.Count;
						for (int j = 0; j < count3; j++)
						{
							int index2 = (j + 1) % count3;
							FacetedBRepEdgeBuilder.Edge edge2 = new FacetedBRepEdgeBuilder.Edge(facetedBRepFaceHole.VerticeIndexes[j], facetedBRepFaceHole.VerticeIndexes[index2]);
							this.addFace(dictionary, edge2, num);
						}
					}
				}
				num++;
			}
			this.Edges = this.TagEdgesByFeatureAngle(dictionary, list);
			foreach (FacetedBRepEdgeBuilder.Edge key in this.Edges)
			{
				Tuple<int, int> tuple = dictionary[key];
				List<int> list2 = new List<int>();
				if (tuple.Item1 != -1)
				{
					list2.Add(tuple.Item1);
				}
				if (tuple.Item2 != -1)
				{
					list2.Add(tuple.Item2);
				}
				this.Faces.Add(list2);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000022F4 File Offset: 0x000004F4
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000022FC File Offset: 0x000004FC
		public double FeatureAngle { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002305 File Offset: 0x00000505
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000230D File Offset: 0x0000050D
		public List<FacetedBRepEdgeBuilder.Edge> Edges { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002316 File Offset: 0x00000516
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000231E File Offset: 0x0000051E
		public List<List<int>> Faces { get; private set; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002328 File Offset: 0x00000528
		private UnitVector3 getPolygonNormal(IList<Vector3> vertices)
		{
			if (vertices.Count < 3)
			{
				return UnitVector3.UnitX;
			}
			Vector3 vector = new Vector3(0.0, 0.0, 0.0);
			int count = vertices.Count;
			for (int i = 0; i < count; i++)
			{
				int index = i;
				int index2 = (i + 1) % count;
				int index3 = (i + 2) % count;
				Vector3 vector2 = vertices[index2] - vertices[index];
				Vector3 vector3 = vertices[index3] - vertices[index2];
				vector += vector2.Cross(vector3);
			}
			double length = vector.Length;
			if (length < 1E-127)
			{
				return UnitVector3.UnitX;
			}
			vector /= length;
			return new UnitVector3(vector.X, vector.Y, vector.Z);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002404 File Offset: 0x00000604
		private void addFace(Dictionary<FacetedBRepEdgeBuilder.Edge, Tuple<int, int>> edges, FacetedBRepEdgeBuilder.Edge edge, int face)
		{
			if (edges.ContainsKey(edge))
			{
				int item = edges[edge].Item1;
				edges[edge] = new Tuple<int, int>(item, face);
				return;
			}
			edges[edge] = new Tuple<int, int>(face, -1);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002444 File Offset: 0x00000644
		private List<FacetedBRepEdgeBuilder.Edge> TagEdgesByFeatureAngle(Dictionary<FacetedBRepEdgeBuilder.Edge, Tuple<int, int>> edges, List<UnitVector3> normals)
		{
			List<FacetedBRepEdgeBuilder.Edge> list = new List<FacetedBRepEdgeBuilder.Edge>();
			foreach (KeyValuePair<FacetedBRepEdgeBuilder.Edge, Tuple<int, int>> keyValuePair in edges)
			{
				bool sharp = true;
				int item = keyValuePair.Value.Item1;
				int item2 = keyValuePair.Value.Item2;
				if (item != -1 && item2 != -1)
				{
					UnitVector3 unitVector = normals[keyValuePair.Value.Item1];
					UnitVector3 vector = normals[keyValuePair.Value.Item2];
					if (Math.Acos(unitVector.Dot(vector)) < this.FeatureAngle)
					{
						sharp = false;
					}
				}
				FacetedBRepEdgeBuilder.Edge item3 = new FacetedBRepEdgeBuilder.Edge(keyValuePair.Key.FirstVertex, keyValuePair.Key.SecondVertex, sharp);
				list.Add(item3);
			}
			return list;
		}

		// Token: 0x02000007 RID: 7
		public class Edge : IEquatable<FacetedBRepEdgeBuilder.Edge>
		{
			// Token: 0x0600001C RID: 28 RVA: 0x000033AF File Offset: 0x000015AF
			public Edge(int first, int second)
			{
				this.FirstVertex = ((first < second) ? first : second);
				this.SecondVertex = ((first > second) ? first : second);
				this.IsSharp = true;
			}

			// Token: 0x0600001D RID: 29 RVA: 0x000033DA File Offset: 0x000015DA
			public Edge(int first, int second, bool sharp)
			{
				this.FirstVertex = ((first < second) ? first : second);
				this.SecondVertex = ((first > second) ? first : second);
				this.IsSharp = sharp;
			}

			// Token: 0x0600001E RID: 30 RVA: 0x00003405 File Offset: 0x00001605
			public override int GetHashCode()
			{
				return this.FirstVertex + 1000 * this.SecondVertex;
			}

			// Token: 0x0600001F RID: 31 RVA: 0x0000341A File Offset: 0x0000161A
			public override bool Equals(object obj)
			{
				return this.Equals(obj as FacetedBRepEdgeBuilder.Edge);
			}

			// Token: 0x06000020 RID: 32 RVA: 0x00003428 File Offset: 0x00001628
			public bool Equals(FacetedBRepEdgeBuilder.Edge edge)
			{
				return edge != null && edge.FirstVertex == this.FirstVertex && edge.SecondVertex == this.SecondVertex;
			}

			// Token: 0x04000004 RID: 4
			public int FirstVertex;

			// Token: 0x04000005 RID: 5
			public int SecondVertex;

			// Token: 0x04000006 RID: 6
			public bool IsSharp;
		}
	}
}
