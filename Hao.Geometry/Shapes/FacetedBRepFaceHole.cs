using System;
using System.Collections;
using System.Collections.Generic;

namespace Hao.Geometry.Shapes
{
	// Token: 0x02000006 RID: 6
	public struct FacetedBRepFaceHole : ICollection<int>, IEnumerable<int>, IEnumerable
	{
		// Token: 0x0600002A RID: 42 RVA: 0x0000265A File Offset: 0x0000085A
		internal FacetedBRepFaceHole(FacetedBRep facetedBrep, IList<int> verticeIndexes)
		{
			this = default(FacetedBRepFaceHole);
			this.FacetedBrep = facetedBrep;
			this.verticeIndexes = verticeIndexes;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002671 File Offset: 0x00000871
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002679 File Offset: 0x00000879
		public FacetedBRep FacetedBrep { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002684 File Offset: 0x00000884
		public IList<Vector3> Vertices
		{
			get
			{
				List<Vector3> list = new List<Vector3>();
				foreach (int index in this.verticeIndexes)
				{
					list.Add(this.FacetedBrep.Vertices[index]);
				}
				return list;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000026E8 File Offset: 0x000008E8
		public IList<int> VerticeIndexes
		{
			get
			{
				return this.verticeIndexes;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026F0 File Offset: 0x000008F0
		public IEnumerator<int> GetEnumerator()
		{
			return this.verticeIndexes.GetEnumerator();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026FD File Offset: 0x000008FD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002626 File Offset: 0x00000826
		public void Add(int item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002626 File Offset: 0x00000826
		public void Clear()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002705 File Offset: 0x00000905
		public bool Contains(int item)
		{
			return this.verticeIndexes.Contains(item);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002713 File Offset: 0x00000913
		public void CopyTo(int[] array, int arrayIndex)
		{
			this.verticeIndexes.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002626 File Offset: 0x00000826
		public bool Remove(int item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002722 File Offset: 0x00000922
		public int Count
		{
			get
			{
				return this.verticeIndexes.Count;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002657 File Offset: 0x00000857
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000009 RID: 9
		private readonly IList<int> verticeIndexes;
	}
}
