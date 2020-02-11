using System;
using System.Collections.Generic;
using System.Linq;
using Hao.Geometry.Algorithms;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x0200000E RID: 14
	public class BRep
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00003894 File Offset: 0x00001A94
		public BRep(IEnumerable<Solid> solids)
		{
			Dictionary<ISurface, int> dictionary = new Dictionary<ISurface, int>();
			Dictionary<BRep.IndexedEdge, int> dictionary2 = new Dictionary<BRep.IndexedEdge, int>();
			Dictionary<ICurve3, int> dictionary3 = new Dictionary<ICurve3, int>();
			Dictionary<ICurve2, int> dictionary4 = new Dictionary<ICurve2, int>();
			List<BRep.IndexedSolid> list = new List<BRep.IndexedSolid>();
			List<BRep.IndexedShell> list2 = new List<BRep.IndexedShell>();
			List<BRep.IndexedFace> list3 = new List<BRep.IndexedFace>();
			List<ISurface> list4 = new List<ISurface>();
			List<BRep.IndexedWire> list5 = new List<BRep.IndexedWire>();
			List<BRep.IndexedOrientedEdge> list6 = new List<BRep.IndexedOrientedEdge>();
			List<BRep.IndexedEdge> list7 = new List<BRep.IndexedEdge>();
			List<ICurve3> list8 = new List<ICurve3>();
			List<ICurve2> list9 = new List<ICurve2>();
			KdTree3 kdTree = new KdTree3(1E-06);
			foreach (Solid solid in solids)
			{
				BRep.IndexedSolid item = new BRep.IndexedSolid
				{
					ShellIndices = new int[solid.Shells.Count]
				};
				int num = 0;
				foreach (Shell shell in solid.Shells)
				{
					int count = list2.Count;
					item.ShellIndices[num++] = count;
					BRep.IndexedShell item2 = new BRep.IndexedShell
					{
						FaceIndices = new int[shell.Faces.Count]
					};
					int num2 = 0;
					foreach (Face face in shell.Faces)
					{
						int count2 = list3.Count;
						item2.FaceIndices[num2++] = count2;
						if (!dictionary.ContainsKey(face.Surface))
						{
							dictionary[face.Surface] = list4.Count;
							list4.Add(face.Surface);
						}
						BRep.IndexedFace item3 = new BRep.IndexedFace
						{
							Orientation = face.Orientation,
							SurfaceIndex = dictionary[face.Surface],
							WireIndices = new int[face.Wires.Count]
						};
						Dictionary<int, int> dictionary5 = new Dictionary<int, int>();
						int num3 = 0;
						foreach (Wire3 wire in face.Wires)
						{
							int count3 = list5.Count;
							item3.WireIndices[num3++] = count3;
							BRep.IndexedWire item4 = new BRep.IndexedWire
							{
								Orientation = wire.Orientation,
								OrientedEdgeIndices = new int[wire.Edges.Count]
							};
							int num4 = 0;
							foreach (Edge3 edge in wire.Edges)
							{
								int count4 = list6.Count;
								item4.OrientedEdgeIndices[num4++] = count4;
								if (!dictionary3.ContainsKey(edge.Curve))
								{
									dictionary3[edge.Curve] = list8.Count;
									list8.Add(edge.Curve);
								}
								if (edge.CurveOnSurface != null && !dictionary4.ContainsKey(edge.CurveOnSurface))
								{
									dictionary4[edge.CurveOnSurface] = list9.Count;
									list9.Add(edge.CurveOnSurface);
								}
								int startVertexIndex = kdTree.Insert(edge.StartVertex);
								int endVertexIndex = kdTree.Insert(edge.EndVertex);
								BRep.IndexedEdge indexedEdge = new BRep.IndexedEdge
								{
									CurveIndex = dictionary3[edge.Curve],
									StartParameter = edge.StartParameter,
									EndParameter = edge.EndParameter,
									StartVertexIndex = startVertexIndex,
									EndVertexIndex = endVertexIndex
								};
								if (!dictionary2.ContainsKey(indexedEdge))
								{
									dictionary2[indexedEdge] = list7.Count;
									list7.Add(indexedEdge);
								}
								int curveOnSurfaceIndex = (edge.CurveOnSurface != null) ? dictionary4[edge.CurveOnSurface] : -1;
								int curveOnSurfaceIndex2 = -1;
								int num5 = dictionary2[indexedEdge];
								if (dictionary5.ContainsKey(num5))
								{
									int index = dictionary5[num5];
									curveOnSurfaceIndex2 = list6[index].CurveOnSurfaceIndex;
								}
								BRep.IndexedOrientedEdge item5 = new BRep.IndexedOrientedEdge
								{
									Orientation = edge.Orientation,
									CurveOnSurfaceIndex = curveOnSurfaceIndex,
									CurveOnSurfaceIndex2 = curveOnSurfaceIndex2,
									EdgeIndex = num5
								};
								list6.Add(item5);
								if (dictionary5.ContainsKey(num5))
								{
									int index2 = dictionary5[num5];
									BRep.IndexedOrientedEdge value = list6[index2];
									value.CurveOnSurfaceIndex2 = item5.CurveOnSurfaceIndex;
									list6[index2] = value;
								}
								else
								{
									dictionary5[num5] = count4;
								}
							}
							list5.Add(item4);
						}
						list3.Add(item3);
					}
					list2.Add(item2);
				}
				list.Add(item);
			}
			this.IndexedSolids = list.AsReadOnly();
			this.IndexedShells = list2.AsReadOnly();
			this.IndexedFaces = list3.AsReadOnly();
			this.Surfaces = list4.AsReadOnly();
			this.IndexedWires = list5.AsReadOnly();
			this.IndexedOrientedEdges = list6.AsReadOnly();
			this.IndexedEdges = list7.AsReadOnly();
			this.Curves = list8.AsReadOnly();
			this.CurvesOnSurface = list9.AsReadOnly();
			this.Vertices = new List<Vector3>(kdTree.Vertices).AsReadOnly();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003E80 File Offset: 0x00002080
		public BRep(IEnumerable<BRep.IndexedSolid> indexedSolids, IEnumerable<BRep.IndexedShell> indexedShells, IEnumerable<BRep.IndexedFace> indexedFaces, IEnumerable<ISurface> surfaces, IEnumerable<BRep.IndexedWire> indexedWires, IEnumerable<BRep.IndexedOrientedEdge> indexedOrientedEdges, IEnumerable<BRep.IndexedEdge> indexedEdges, IEnumerable<ICurve3> curves, IEnumerable<ICurve2> curvesOnSurface, IEnumerable<Vector3> vertices)
		{
			this.IndexedSolids = new List<BRep.IndexedSolid>(indexedSolids).AsReadOnly();
			this.IndexedShells = new List<BRep.IndexedShell>(indexedShells).AsReadOnly();
			this.IndexedFaces = new List<BRep.IndexedFace>(indexedFaces).AsReadOnly();
			this.Surfaces = new List<ISurface>(surfaces).AsReadOnly();
			this.IndexedWires = new List<BRep.IndexedWire>(indexedWires).AsReadOnly();
			this.IndexedOrientedEdges = new List<BRep.IndexedOrientedEdge>(indexedOrientedEdges).AsReadOnly();
			this.IndexedEdges = new List<BRep.IndexedEdge>(indexedEdges).AsReadOnly();
			this.Curves = new List<ICurve3>(curves).AsReadOnly();
			this.CurvesOnSurface = new List<ICurve2>(curvesOnSurface).AsReadOnly();
			this.Vertices = new List<Vector3>(vertices).AsReadOnly();
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003F44 File Offset: 0x00002144
		public IList<Solid> Solids
		{
			get
			{
				List<Solid> list = new List<Solid>();
				foreach (BRep.IndexedSolid indexedSolid in this.IndexedSolids)
				{
					List<Shell> list2 = new List<Shell>();
					foreach (int index in indexedSolid.ShellIndices)
					{
						BRep.IndexedShell indexedShell = this.IndexedShells[index];
						List<Face> list3 = new List<Face>();
						foreach (int faceIndex in indexedShell.FaceIndices)
						{
							list3.Add(this.GetFace(faceIndex));
						}
						list2.Add(new Shell(list3));
					}
					list.Add(new Solid(list2));
				}
				return list.AsReadOnly();
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004028 File Offset: 0x00002228
		public IList<Face> Faces
		{
			get
			{
				List<Face> list = new List<Face>();
				for (int i = 0; i < this.IndexedFaces.Count; i++)
				{
					list.Add(this.GetFace(i));
				}
				return list.AsReadOnly();
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00004064 File Offset: 0x00002264
		public IList<Edge3> Edges
		{
			get
			{
				List<Edge3> list = new List<Edge3>();
				for (int i = 0; i < this.IndexedOrientedEdges.Count; i++)
				{
					list.Add(this.GetEdge(i));
				}
				return list.AsReadOnly();
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000040A0 File Offset: 0x000022A0
		public AxisAlignedBox3 BoundingBox
		{
			get
			{
				Vector3 vector = Vector3.MaxValue;
				Vector3 vector2 = Vector3.MinValue;
				foreach (Edge3 edge in this.Edges)
				{
					AxisAlignedBox3? boundingBox = edge.BoundingBox;
					if (boundingBox != null)
					{
						vector = Vector3.Min(vector, boundingBox.Value.Min);
						vector2 = Vector3.Max(vector2, boundingBox.Value.Max);
					}
				}
				return new AxisAlignedBox3(vector, vector2);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00004138 File Offset: 0x00002338
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00004140 File Offset: 0x00002340
		public IList<BRep.IndexedSolid> IndexedSolids { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004149 File Offset: 0x00002349
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00004151 File Offset: 0x00002351
		public IList<BRep.IndexedShell> IndexedShells { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000415A File Offset: 0x0000235A
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00004162 File Offset: 0x00002362
		public IList<BRep.IndexedFace> IndexedFaces { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000416B File Offset: 0x0000236B
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00004173 File Offset: 0x00002373
		public IList<ISurface> Surfaces { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000417C File Offset: 0x0000237C
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00004184 File Offset: 0x00002384
		public IList<BRep.IndexedWire> IndexedWires { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000418D File Offset: 0x0000238D
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00004195 File Offset: 0x00002395
		public IList<BRep.IndexedOrientedEdge> IndexedOrientedEdges { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000419E File Offset: 0x0000239E
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000041A6 File Offset: 0x000023A6
		public IList<BRep.IndexedEdge> IndexedEdges { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000041AF File Offset: 0x000023AF
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000041B7 File Offset: 0x000023B7
		public IList<ICurve3> Curves { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000041C0 File Offset: 0x000023C0
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000041C8 File Offset: 0x000023C8
		public IList<ICurve2> CurvesOnSurface { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000041D1 File Offset: 0x000023D1
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000041D9 File Offset: 0x000023D9
		public IList<Vector3> Vertices { get; private set; }

		// Token: 0x06000091 RID: 145 RVA: 0x000041E4 File Offset: 0x000023E4
		public Face GetFace(int faceIndex)
		{
			if (faceIndex < 0 || faceIndex >= this.IndexedFaces.Count)
			{
				throw new ArgumentException("BRep.GetFace(): invalid index.");
			}
			BRep.IndexedFace indexedFace = this.IndexedFaces[faceIndex];
			List<Wire3> list = new List<Wire3>();
			foreach (int index in indexedFace.WireIndices)
			{
				BRep.IndexedWire indexedWire = this.IndexedWires[index];
				List<Edge3> list2 = new List<Edge3>();
				foreach (int orientedEdgeIndex in indexedWire.OrientedEdgeIndices)
				{
					list2.Add(this.GetEdge(orientedEdgeIndex));
				}
				list.Add(new Wire3(indexedWire.Orientation, list2));
			}
			return new Face(indexedFace.Orientation, this.Surfaces[indexedFace.SurfaceIndex], list);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000042B8 File Offset: 0x000024B8
		public Edge3 GetEdge(int orientedEdgeIndex)
		{
			if (orientedEdgeIndex < 0 || orientedEdgeIndex >= this.IndexedOrientedEdges.Count)
			{
				throw new ArgumentException("BRep.GetEdge(): invalid index.");
			}
			BRep.IndexedOrientedEdge indexedOrientedEdge = this.IndexedOrientedEdges[orientedEdgeIndex];
			BRep.IndexedEdge indexedEdge = this.IndexedEdges[indexedOrientedEdge.EdgeIndex];
			return new Edge3(indexedOrientedEdge.Orientation, this.Curves[indexedEdge.CurveIndex], (indexedOrientedEdge.CurveOnSurfaceIndex < 0) ? null : this.CurvesOnSurface[indexedOrientedEdge.CurveOnSurfaceIndex], indexedEdge.StartParameter, indexedEdge.EndParameter, this.Vertices[indexedEdge.StartVertexIndex], this.Vertices[indexedEdge.EndVertexIndex]);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004371 File Offset: 0x00002571
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((BRep)obj);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004397 File Offset: 0x00002597
		public bool Equals(BRep other)
		{
			return this.Solids.SequenceEqual(other.Solids);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000043AA File Offset: 0x000025AA
		public override int GetHashCode()
		{
			return this.Solids.GetHashCode();
		}

		// Token: 0x02000034 RID: 52
		public struct IndexedSolid
		{
			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x0600022D RID: 557 RVA: 0x0000754A File Offset: 0x0000574A
			// (set) Token: 0x0600022E RID: 558 RVA: 0x00007552 File Offset: 0x00005752
			public int[] ShellIndices { get; set; }
		}

		// Token: 0x02000035 RID: 53
		public struct IndexedShell
		{
			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x0600022F RID: 559 RVA: 0x0000755B File Offset: 0x0000575B
			// (set) Token: 0x06000230 RID: 560 RVA: 0x00007563 File Offset: 0x00005763
			public int[] FaceIndices { get; set; }
		}

		// Token: 0x02000036 RID: 54
		public struct IndexedFace
		{
			// Token: 0x170000CA RID: 202
			// (get) Token: 0x06000231 RID: 561 RVA: 0x0000756C File Offset: 0x0000576C
			// (set) Token: 0x06000232 RID: 562 RVA: 0x00007574 File Offset: 0x00005774
			public Orientation Orientation { get; set; }

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x06000233 RID: 563 RVA: 0x0000757D File Offset: 0x0000577D
			// (set) Token: 0x06000234 RID: 564 RVA: 0x00007585 File Offset: 0x00005785
			public int SurfaceIndex { get; set; }

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x06000235 RID: 565 RVA: 0x0000758E File Offset: 0x0000578E
			// (set) Token: 0x06000236 RID: 566 RVA: 0x00007596 File Offset: 0x00005796
			public int[] WireIndices { get; set; }
		}

		// Token: 0x02000037 RID: 55
		public struct IndexedWire
		{
			// Token: 0x170000CD RID: 205
			// (get) Token: 0x06000237 RID: 567 RVA: 0x0000759F File Offset: 0x0000579F
			// (set) Token: 0x06000238 RID: 568 RVA: 0x000075A7 File Offset: 0x000057A7
			public Orientation Orientation { get; set; }

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x06000239 RID: 569 RVA: 0x000075B0 File Offset: 0x000057B0
			// (set) Token: 0x0600023A RID: 570 RVA: 0x000075B8 File Offset: 0x000057B8
			public int[] OrientedEdgeIndices { get; set; }
		}

		// Token: 0x02000038 RID: 56
		public struct IndexedOrientedEdge
		{
			// Token: 0x170000CF RID: 207
			// (get) Token: 0x0600023B RID: 571 RVA: 0x000075C1 File Offset: 0x000057C1
			// (set) Token: 0x0600023C RID: 572 RVA: 0x000075C9 File Offset: 0x000057C9
			public Orientation Orientation { get; set; }

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x0600023D RID: 573 RVA: 0x000075D2 File Offset: 0x000057D2
			// (set) Token: 0x0600023E RID: 574 RVA: 0x000075DA File Offset: 0x000057DA
			public int CurveOnSurfaceIndex { get; set; }

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x0600023F RID: 575 RVA: 0x000075E3 File Offset: 0x000057E3
			// (set) Token: 0x06000240 RID: 576 RVA: 0x000075EB File Offset: 0x000057EB
			public int CurveOnSurfaceIndex2 { get; set; }

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06000241 RID: 577 RVA: 0x000075F4 File Offset: 0x000057F4
			// (set) Token: 0x06000242 RID: 578 RVA: 0x000075FC File Offset: 0x000057FC
			public int EdgeIndex { get; set; }
		}

		// Token: 0x02000039 RID: 57
		public struct IndexedEdge
		{
			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06000243 RID: 579 RVA: 0x00007605 File Offset: 0x00005805
			// (set) Token: 0x06000244 RID: 580 RVA: 0x0000760D File Offset: 0x0000580D
			public int CurveIndex { get; set; }

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x06000245 RID: 581 RVA: 0x00007616 File Offset: 0x00005816
			// (set) Token: 0x06000246 RID: 582 RVA: 0x0000761E File Offset: 0x0000581E
			public double StartParameter { get; set; }

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x06000247 RID: 583 RVA: 0x00007627 File Offset: 0x00005827
			// (set) Token: 0x06000248 RID: 584 RVA: 0x0000762F File Offset: 0x0000582F
			public double EndParameter { get; set; }

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x06000249 RID: 585 RVA: 0x00007638 File Offset: 0x00005838
			// (set) Token: 0x0600024A RID: 586 RVA: 0x00007640 File Offset: 0x00005840
			public int StartVertexIndex { get; set; }

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x0600024B RID: 587 RVA: 0x00007649 File Offset: 0x00005849
			// (set) Token: 0x0600024C RID: 588 RVA: 0x00007651 File Offset: 0x00005851
			public int EndVertexIndex { get; set; }
		}
	}
}
