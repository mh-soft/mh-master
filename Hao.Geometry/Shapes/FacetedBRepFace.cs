using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hao.Geometry.Shapes
{
	// Token: 0x02000005 RID: 5
	public struct FacetedBRepFace : ICollection<int>, IEnumerable<int>, IEnumerable
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000253C File Offset: 0x0000073C
		internal FacetedBRepFace(FacetedBRep facetedBrep, IList<int> verticeIndexes, IList<FacetedBRepFaceHole> holes)
		{
			this = default(FacetedBRepFace);
			this.FacetedBrep = facetedBrep;
			this.VerticeIndexes = verticeIndexes;
			this.Holes = holes;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000255A File Offset: 0x0000075A
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002562 File Offset: 0x00000762
		public FacetedBRep FacetedBrep { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000256B File Offset: 0x0000076B
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002573 File Offset: 0x00000773
		public IList<int> VerticeIndexes { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000257C File Offset: 0x0000077C
		public bool HasHoles
		{
			get
			{
				return this.Holes != null && this.Holes.GetEnumerator().MoveNext();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002598 File Offset: 0x00000798
		public IList<Vector3> Vertices
		{
			get
			{
				ReadOnlyCollection<Vector3> vertices = this.FacetedBrep.Vertices;
				List<Vector3> list = new List<Vector3>();
				foreach (int index in this.VerticeIndexes)
				{
					list.Add(vertices[index]);
				}
				return list;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002600 File Offset: 0x00000800
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002608 File Offset: 0x00000808
		public IList<FacetedBRepFaceHole> Holes { get; private set; }

		// Token: 0x06000021 RID: 33 RVA: 0x00002611 File Offset: 0x00000811
		public IEnumerator<int> GetEnumerator()
		{
			return this.VerticeIndexes.GetEnumerator();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000261E File Offset: 0x0000081E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002626 File Offset: 0x00000826
		public void Add(int item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002626 File Offset: 0x00000826
		public void Clear()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000262D File Offset: 0x0000082D
		public bool Contains(int item)
		{
			return this.VerticeIndexes.Contains(item);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000263B File Offset: 0x0000083B
		public void CopyTo(int[] array, int arrayIndex)
		{
			this.VerticeIndexes.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002626 File Offset: 0x00000826
		public bool Remove(int item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000264A File Offset: 0x0000084A
		public int Count
		{
			get
			{
				return this.VerticeIndexes.Count;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002657 File Offset: 0x00000857
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}
	}
}
