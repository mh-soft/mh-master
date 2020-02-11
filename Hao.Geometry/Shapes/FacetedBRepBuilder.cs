using System;
using System.Collections.Generic;
using Hao.Geometry.Algorithms;

namespace Hao.Geometry.Shapes
{
	// Token: 0x02000004 RID: 4
	public class FacetedBRepBuilder
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000235E File Offset: 0x0000055E
		public void AddOuterWire(ICollection<Vector3> outerWire)
		{
			this.outerWires.Add(new FacetedBRepBuilder.FacetedBrepOuterWire(outerWire));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002371 File Offset: 0x00000571
		public void AddInnerWire(ICollection<Vector3> innerWire)
		{
			if (this.outerWires.Count == 0)
			{
				throw new ArgumentException("Cannot add inner wire without outer wire", "innerWire");
			}
			this.outerWires[this.outerWires.Count - 1].AddInnerWire(innerWire);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023B0 File Offset: 0x000005B0
		public FacetedBRep Build(double epsilon)
		{
			KdTree3 kdTree = new KdTree3(epsilon);
			Dictionary<int, int[][]> dictionary = new Dictionary<int, int[][]>();
			List<int[]> list = new List<int[]>();
			foreach (FacetedBRepBuilder.FacetedBrepOuterWire facetedBrepOuterWire in this.outerWires)
			{
				List<int> list2 = new List<int>();
				foreach (Vector3 value in facetedBrepOuterWire.Vertices)
				{
					list2.Add(kdTree.Insert(value));
				}
				int count = list.Count;
				list.Add(list2.ToArray());
				if (facetedBrepOuterWire.InnerWires.Count != 0)
				{
					List<int[]> list3 = new List<int[]>();
					foreach (FacetedBRepBuilder.FacetedBrepOuterWire facetedBrepOuterWire2 in facetedBrepOuterWire.InnerWires)
					{
						list2.Clear();
						foreach (Vector3 value2 in facetedBrepOuterWire2.Vertices)
						{
							list2.Add(kdTree.Insert(value2));
						}
						list3.Add(list2.ToArray());
					}
					dictionary.Add(count, list3.ToArray());
				}
			}
			return new FacetedBRep(kdTree.Vertices, list.ToArray(), dictionary);
		}

		// Token: 0x04000005 RID: 5
		private readonly List<FacetedBRepBuilder.FacetedBrepOuterWire> outerWires = new List<FacetedBRepBuilder.FacetedBrepOuterWire>();

		// Token: 0x0200002B RID: 43
		private class FacetedBrepOuterWire
		{
			// Token: 0x060001F3 RID: 499 RVA: 0x00007284 File Offset: 0x00005484
			internal FacetedBrepOuterWire(ICollection<Vector3> vertices)
			{
				this.vertices = new Vector3[vertices.Count];
				vertices.CopyTo(this.vertices, 0);
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x060001F4 RID: 500 RVA: 0x000072B5 File Offset: 0x000054B5
			public Vector3[] Vertices
			{
				get
				{
					return this.vertices;
				}
			}

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x060001F5 RID: 501 RVA: 0x000072BD File Offset: 0x000054BD
			public ICollection<FacetedBRepBuilder.FacetedBrepOuterWire> InnerWires
			{
				get
				{
					return this.innerWires;
				}
			}

			// Token: 0x060001F6 RID: 502 RVA: 0x000072C5 File Offset: 0x000054C5
			public void AddInnerWire(ICollection<Vector3> innerWireVertices)
			{
				this.innerWires.Add(new FacetedBRepBuilder.FacetedBrepOuterWire(innerWireVertices));
			}

			// Token: 0x0400008A RID: 138
			private readonly Vector3[] vertices;

			// Token: 0x0400008B RID: 139
			private readonly List<FacetedBRepBuilder.FacetedBrepOuterWire> innerWires = new List<FacetedBRepBuilder.FacetedBrepOuterWire>();
		}
	}
}
