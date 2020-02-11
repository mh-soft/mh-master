using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Hao.Geometry.Shapes
{
	// Token: 0x02000003 RID: 3
	[DebuggerDisplay("(FacetedBRep Vertices:{this.vertices.Count} OuterWires:{this.OuterWires.Length}, InnerWires:{this.InnerWires.Count})")]
	public class FacetedBRep
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020C8 File Offset: 0x000002C8
		public FacetedBRep(Vector3[] vertices, int[][] outerWires, IDictionary<int, int[][]> innerWires)
		{
			this.vertices = new ReadOnlyCollection<Vector3>(vertices);
			int count = this.vertices.Count;
			foreach (int[] array in outerWires)
			{
				foreach (int num in array)
				{
					if (num < 0 || num >= count)
					{
						throw new ArgumentException("FacetedBRep, invalid index found", "outerWires");
					}
				}
			}
			foreach (int[][] array2 in innerWires.Values)
			{
				foreach (int[] array in array2)
				{
					foreach (int num2 in array)
					{
						if (num2 < 0 || num2 >= count)
						{
							throw new ArgumentException("FacetedBRep, invalid index found", "outerWires");
						}
					}
				}
			}
			this.OuterWires = outerWires;
			this.InnerWires = innerWires;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021CC File Offset: 0x000003CC
		public ReadOnlyCollection<Vector3> Vertices
		{
			get
			{
				return this.vertices;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021D4 File Offset: 0x000003D4
		public ICollection<FacetedBRepFace> Faces
		{
			get
			{
				return new FacetedBRep.FacetedBrepFaceCollection(this);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021DC File Offset: 0x000003DC
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021E4 File Offset: 0x000003E4
		public int[][] OuterWires { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021ED File Offset: 0x000003ED
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000021F5 File Offset: 0x000003F5
		public IDictionary<int, int[][]> InnerWires { get; private set; }

		// Token: 0x0600000E RID: 14 RVA: 0x000021FE File Offset: 0x000003FE
		public int[] GetOuterFace(int faceIndex)
		{
			return this.OuterWires[faceIndex];
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002208 File Offset: 0x00000408
		public int GetInnerFaceCount(int faceIndex)
		{
			int[][] array;
			if (!this.InnerWires.TryGetValue(faceIndex, out array))
			{
				return 0;
			}
			return array.Length;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000222C File Offset: 0x0000042C
		public int[] GetInnerFace(int faceIndex)
		{
			int[][] array;
			if (!this.InnerWires.TryGetValue(faceIndex, out array))
			{
				return null;
			}
			return array[faceIndex];
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002250 File Offset: 0x00000450
		public bool CheckForTwoManifold()
		{
			HashSet<uint> hashSet = new HashSet<uint>();
			foreach (int[] wire in this.OuterWires)
			{
				this.AddWire(hashSet, wire);
			}
			foreach (KeyValuePair<int, int[][]> keyValuePair in this.InnerWires)
			{
				foreach (int[] wire2 in keyValuePair.Value)
				{
					this.AddWire(hashSet, wire2);
				}
			}
			return hashSet.Count == 0;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022F0 File Offset: 0x000004F0
		private void AddWire(HashSet<uint> halfEdges, int[] wire)
		{
			int num = wire.Length;
			ushort v = (ushort)wire[num - 1];
			for (int i = 0; i < num; i++)
			{
				ushort num2 = (ushort)wire[i];
				uint item = this.MakeKey(v, num2);
				v = num2;
				if (!halfEdges.Remove(item))
				{
					halfEdges.Add(item);
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002338 File Offset: 0x00000538
		private uint MakeKey(ushort v0, ushort v1)
		{
			if (v0 > v1)
			{
				return (uint)((int)v0 + ((int)v1 << 16));
			}
			return (uint)((int)v1 + ((int)v0 << 16));
		}

		// Token: 0x04000002 RID: 2
		private readonly ReadOnlyCollection<Vector3> vertices;

		// Token: 0x0200002A RID: 42
		private class FacetedBrepFaceCollection : ICollection<FacetedBRepFace>, IEnumerable<FacetedBRepFace>, IEnumerable
		{
			// Token: 0x060001E9 RID: 489 RVA: 0x00007174 File Offset: 0x00005374
			internal FacetedBrepFaceCollection(FacetedBRep facetedBrep)
			{
				this.faces = new List<FacetedBRepFace>();
				for (int i = 0; i < facetedBrep.OuterWires.Length; i++)
				{
					int[] array = facetedBrep.OuterWires[i];
					List<int> list = new List<int>(array.Length);
					foreach (int item in array)
					{
						list.Add(item);
					}
					List<FacetedBRepFaceHole> list2 = null;
					if (facetedBrep.InnerWires.ContainsKey(i))
					{
						list2 = new List<FacetedBRepFaceHole>();
						foreach (int[] verticeIndexes in facetedBrep.InnerWires[i])
						{
							FacetedBRepFaceHole item2 = new FacetedBRepFaceHole(facetedBrep, verticeIndexes);
							list2.Add(item2);
						}
					}
					this.faces.Add(new FacetedBRepFace(facetedBrep, list, list2));
				}
			}

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x060001EA RID: 490 RVA: 0x00007240 File Offset: 0x00005440
			public int Count
			{
				get
				{
					return this.faces.Count;
				}
			}

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x060001EB RID: 491 RVA: 0x00002657 File Offset: 0x00000857
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060001EC RID: 492 RVA: 0x0000724D File Offset: 0x0000544D
			public IEnumerator<FacetedBRepFace> GetEnumerator()
			{
				return this.faces.GetEnumerator();
			}

			// Token: 0x060001ED RID: 493 RVA: 0x0000725F File Offset: 0x0000545F
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060001EE RID: 494 RVA: 0x00002626 File Offset: 0x00000826
			public void Add(FacetedBRepFace item)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001EF RID: 495 RVA: 0x00002626 File Offset: 0x00000826
			public void Clear()
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001F0 RID: 496 RVA: 0x00007267 File Offset: 0x00005467
			public bool Contains(FacetedBRepFace item)
			{
				return this.faces.Contains(item);
			}

			// Token: 0x060001F1 RID: 497 RVA: 0x00007275 File Offset: 0x00005475
			public void CopyTo(FacetedBRepFace[] array, int arrayIndex)
			{
				this.faces.CopyTo(array, arrayIndex);
			}

			// Token: 0x060001F2 RID: 498 RVA: 0x00002626 File Offset: 0x00000826
			public bool Remove(FacetedBRepFace item)
			{
				throw new NotImplementedException();
			}

			// Token: 0x04000089 RID: 137
			private readonly List<FacetedBRepFace> faces;
		}
	}
}
