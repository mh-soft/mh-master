using System;
using System.Collections.Generic;

namespace Hao.Geometry.Shapes.Zkit
{
	// Token: 0x0200000A RID: 10
	public class TriangleMesh
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002770 File Offset: 0x00000970
		public TriangleMesh(Vector3 origin, IEnumerable<TriangleMesh.Normal> normals, IEnumerable<TriangleMesh.Vertex> vertices, IEnumerable<TriangleMesh.EdgeLine> edgeLines, IEnumerable<TriangleMesh.SurfaceLine> surfaceLines, IEnumerable<TriangleMesh.Line> lines, IEnumerable<TriangleMesh.Corner> corners, IEnumerable<TriangleMesh.Triangle> triangles, IEnumerable<TriangleMesh.ReferenceLine> referenceLines, double referenceLineRadius)
		{
			this.Origin = origin;
			this.Normals = new List<TriangleMesh.Normal>(normals);
			this.Vertices = new List<TriangleMesh.Vertex>(vertices);
			this.EdgeLines = new List<TriangleMesh.EdgeLine>(edgeLines);
			this.SurfaceLines = new List<TriangleMesh.SurfaceLine>(surfaceLines);
			this.Lines = new List<TriangleMesh.Line>(lines);
			this.Corners = new List<TriangleMesh.Corner>(corners);
			this.Triangles = new List<TriangleMesh.Triangle>(triangles);
			this.ReferenceLines = new List<TriangleMesh.ReferenceLine>(referenceLines);
			this.ReferenceLineRadius = referenceLineRadius;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000027F8 File Offset: 0x000009F8
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002800 File Offset: 0x00000A00
		public Vector3 Origin { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002809 File Offset: 0x00000A09
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002811 File Offset: 0x00000A11
		public IList<TriangleMesh.Normal> Normals { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000281A File Offset: 0x00000A1A
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002822 File Offset: 0x00000A22
		public IList<TriangleMesh.Vertex> Vertices { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000282B File Offset: 0x00000A2B
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002833 File Offset: 0x00000A33
		public IList<TriangleMesh.EdgeLine> EdgeLines { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000283C File Offset: 0x00000A3C
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002844 File Offset: 0x00000A44
		public IList<TriangleMesh.SurfaceLine> SurfaceLines { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000284D File Offset: 0x00000A4D
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002855 File Offset: 0x00000A55
		public IList<TriangleMesh.Line> Lines { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000285E File Offset: 0x00000A5E
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002866 File Offset: 0x00000A66
		public IList<TriangleMesh.Corner> Corners { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000286F File Offset: 0x00000A6F
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002877 File Offset: 0x00000A77
		public IList<TriangleMesh.Triangle> Triangles { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002880 File Offset: 0x00000A80
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002888 File Offset: 0x00000A88
		public IList<TriangleMesh.ReferenceLine> ReferenceLines { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002891 File Offset: 0x00000A91
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002899 File Offset: 0x00000A99
		public double ReferenceLineRadius { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000028A4 File Offset: 0x00000AA4
		public AxisAlignedBox3 BoundingBox
		{
			get
			{
				TriangleMesh.Vertex vertex = new TriangleMesh.Vertex(0f, 0f, 0f);
				if (this.Vertices.Count > 0)
				{
					vertex = this.Vertices[0];
				}
				Vector3 vector = new Vector3((double)vertex.X, (double)vertex.Y, (double)vertex.Z);
				Vector3 vector2 = vector;
				foreach (TriangleMesh.Vertex vertex2 in this.Vertices)
				{
					Vector3 value = new Vector3((double)vertex2.X, (double)vertex2.Y, (double)vertex2.Z);
					vector = Vector3.Min(vector, value);
					vector2 = Vector3.Max(vector2, value);
				}
				return new AxisAlignedBox3(this.Origin + vector, this.Origin + vector2);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000298C File Offset: 0x00000B8C
		public Vector3 Center
		{
			get
			{
				TriangleMesh.Vertex vertex = new TriangleMesh.Vertex(0f, 0f, 0f);
				if (this.Vertices.Count > 0)
				{
					vertex = this.Vertices[0];
				}
				double num = (double)vertex.X;
				double num2 = (double)vertex.Y;
				double num3 = (double)vertex.Z;
				double num4 = num;
				double num5 = num2;
				double num6 = num3;
				foreach (TriangleMesh.Vertex vertex2 in this.Vertices)
				{
					num = Math.Min(num, (double)vertex2.X);
					num2 = Math.Min(num2, (double)vertex2.Y);
					num3 = Math.Min(num3, (double)vertex2.Z);
					num4 = Math.Max(num4, (double)vertex2.X);
					num5 = Math.Max(num5, (double)vertex2.Y);
					num6 = Math.Max(num6, (double)vertex2.Z);
				}
				double num7 = (num + num4) / 2.0;
				double num8 = (num2 + num5) / 2.0;
				double num9 = (num3 + num6) / 2.0;
				return new Vector3(num7 + this.Origin.X, num8 + this.Origin.Y, num9 + this.Origin.Z);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public Vector3 Extent
		{
			get
			{
				TriangleMesh.Vertex vertex = new TriangleMesh.Vertex(0f, 0f, 0f);
				if (this.Vertices.Count > 0)
				{
					vertex = this.Vertices[0];
				}
				double num = (double)vertex.X;
				double num2 = (double)vertex.Y;
				double num3 = (double)vertex.Z;
				double num4 = num;
				double num5 = num2;
				double num6 = num3;
				foreach (TriangleMesh.Vertex vertex2 in this.Vertices)
				{
					num = Math.Min(num, (double)vertex2.X);
					num2 = Math.Min(num2, (double)vertex2.Y);
					num3 = Math.Min(num3, (double)vertex2.Z);
					num4 = Math.Max(num4, (double)vertex2.X);
					num5 = Math.Max(num5, (double)vertex2.Y);
					num6 = Math.Max(num6, (double)vertex2.Z);
				}
				double x = (num4 - num) / 2.0;
				double y = (num5 - num2) / 2.0;
				double z = (num6 - num3) / 2.0;
				return new Vector3(x, y, z);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002C2C File Offset: 0x00000E2C
		public TriangleMesh Transform(AffineTransform3 placement)
		{
			Vector3 origin = placement.Transform(this.Origin);
			AffineTransform3 rotation = new AffineTransform3(placement.AxisX, placement.AxisY, placement.AxisZ);
			List<TriangleMesh.Normal> list = new List<TriangleMesh.Normal>();
			foreach (TriangleMesh.Normal normal in this.Normals)
			{
				list.Add(TriangleMesh.Rotate(rotation, normal));
			}
			List<TriangleMesh.Vertex> list2 = new List<TriangleMesh.Vertex>();
			foreach (TriangleMesh.Vertex vertex in this.Vertices)
			{
				list2.Add(TriangleMesh.Transform(placement, vertex));
			}
			List<TriangleMesh.ReferenceLine> list3 = new List<TriangleMesh.ReferenceLine>();
			foreach (TriangleMesh.ReferenceLine referenceLine in this.ReferenceLines)
			{
				list3.Add(TriangleMesh.Transform(placement, referenceLine));
			}
			return new TriangleMesh(origin, list, list2, this.EdgeLines, this.SurfaceLines, this.Lines, this.Corners, this.Triangles, this.ReferenceLines, this.ReferenceLineRadius * placement.Scale);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002D90 File Offset: 0x00000F90
		public bool CheckForTwoManifold()
		{
			HashSet<uint> hashSet = new HashSet<uint>();
			foreach (TriangleMesh.Triangle triangle in this.Triangles)
			{
				ushort v = triangle.V0;
				ushort v2 = triangle.V1;
				ushort v3 = triangle.V2;
				TriangleMesh.Corner corner = this.Corners[(int)v];
				TriangleMesh.Corner corner2 = this.Corners[(int)v2];
				TriangleMesh.Corner corner3 = this.Corners[(int)v3];
				uint item = TriangleMesh.MakeKey(corner.VertexIndex, corner2.VertexIndex);
				if (!hashSet.Remove(item))
				{
					hashSet.Add(item);
				}
				uint item2 = TriangleMesh.MakeKey(corner2.VertexIndex, corner3.VertexIndex);
				if (!hashSet.Remove(item2))
				{
					hashSet.Add(item2);
				}
				uint item3 = TriangleMesh.MakeKey(corner3.VertexIndex, corner.VertexIndex);
				if (!hashSet.Remove(item3))
				{
					hashSet.Add(item3);
				}
			}
			return hashSet.Count == 0;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002EAC File Offset: 0x000010AC
		public Vector3 CalculateCenteroid()
		{
			Vector3 vector = Vector3.Zero;
			double num = 0.0;
			foreach (TriangleMesh.Triangle triangle in this.Triangles)
			{
				ushort v = triangle.V0;
				ushort v2 = triangle.V1;
				ushort v3 = triangle.V2;
				TriangleMesh.Corner corner = this.Corners[(int)v];
				TriangleMesh.Corner corner2 = this.Corners[(int)v2];
				TriangleMesh.Corner corner3 = this.Corners[(int)v3];
				TriangleMesh.Vertex vertex = this.Vertices[(int)corner.VertexIndex];
				TriangleMesh.Vertex vertex2 = this.Vertices[(int)corner2.VertexIndex];
				TriangleMesh.Vertex vertex3 = this.Vertices[(int)corner3.VertexIndex];
				double num2 = this.TetrahedronVolume(this.Origin, new Vector3((double)vertex2.X, (double)vertex2.Y, (double)vertex2.Z), new Vector3((double)vertex.X, (double)vertex.Y, (double)vertex.Z), new Vector3((double)vertex3.X, (double)vertex3.Y, (double)vertex3.Z));
				Vector3 vector2 = this.TetrahedronCenteroid(this.Origin, new Vector3((double)vertex2.X, (double)vertex2.Y, (double)vertex2.Z), new Vector3((double)vertex.X, (double)vertex.Y, (double)vertex.Z), new Vector3((double)vertex3.X, (double)vertex3.Y, (double)vertex3.Z));
				vector += vector2 * num2;
				num += num2;
			}
			return vector / num;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000308C File Offset: 0x0000128C
		public double CalculateVolume()
		{
			double num = 0.0;
			foreach (TriangleMesh.Triangle triangle in this.Triangles)
			{
				ushort v = triangle.V0;
				ushort v2 = triangle.V1;
				ushort v3 = triangle.V2;
				TriangleMesh.Corner corner = this.Corners[(int)v];
				TriangleMesh.Corner corner2 = this.Corners[(int)v2];
				TriangleMesh.Corner corner3 = this.Corners[(int)v3];
				TriangleMesh.Vertex vertex = this.Vertices[(int)corner.VertexIndex];
				TriangleMesh.Vertex vertex2 = this.Vertices[(int)corner2.VertexIndex];
				TriangleMesh.Vertex vertex3 = this.Vertices[(int)corner3.VertexIndex];
				num += this.TetrahedronVolume(this.Origin, new Vector3((double)vertex2.X, (double)vertex2.Y, (double)vertex2.Z), new Vector3((double)vertex.X, (double)vertex.Y, (double)vertex.Z), new Vector3((double)vertex3.X, (double)vertex3.Y, (double)vertex3.Z));
			}
			return num;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000031D8 File Offset: 0x000013D8
		private double TetrahedronVolume(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
		{
			return (v0.X * (v1.Y * (v2.Z - v3.Z) - v2.Y * (v1.Z - v3.Z) + v3.Y * (v1.Z - v2.Z)) - v1.X * (v0.Y * (v2.Z - v3.Z) - v2.Y * (v0.Z - v3.Z) + v3.Y * (v0.Z - v2.Z)) + v2.X * (v0.Y * (v1.Z - v3.Z) - v1.Y * (v0.Z - v3.Z) + v3.Y * (v0.Z - v1.Z)) - v3.X * (v0.Y * (v1.Z - v2.Z) - v1.Y * (v0.Z - v2.Z) + v2.Y * (v0.Z - v1.Z))) / 6.0;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003330 File Offset: 0x00001530
		private Vector3 TetrahedronCenteroid(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
		{
			return new Vector3(0.25 * (v0.X + v1.X + v2.X + v3.X), 0.25 * (v0.Y + v1.Y + v2.Y + v3.Y), 0.25 * (v0.Z + v1.Z + v2.Z + v3.Z));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000033C0 File Offset: 0x000015C0
		public TriangleMesh CreateSurfaceLines()
		{
			Dictionary<uint, ushort> dictionary = new Dictionary<uint, ushort>();
			List<TriangleMesh.EdgeLine> edgeLines = new List<TriangleMesh.EdgeLine>();
			List<TriangleMesh.SurfaceLine> surfaceLines = new List<TriangleMesh.SurfaceLine>();
			List<TriangleMesh.Line> list = new List<TriangleMesh.Line>();
			foreach (TriangleMesh.Triangle triangle in this.Triangles)
			{
				ushort v = triangle.V0;
				ushort v2 = triangle.V1;
				ushort v3 = triangle.V2;
				TriangleMesh.Corner corner = this.Corners[(int)v];
				TriangleMesh.Corner corner2 = this.Corners[(int)v2];
				TriangleMesh.Corner corner3 = this.Corners[(int)v3];
				TriangleMesh.AddHalfEdge(corner.VertexIndex, corner2.VertexIndex, corner.NormalIndex, this.Normals, dictionary, edgeLines, surfaceLines);
				TriangleMesh.AddHalfEdge(corner2.VertexIndex, corner3.VertexIndex, corner2.NormalIndex, this.Normals, dictionary, edgeLines, surfaceLines);
				TriangleMesh.AddHalfEdge(corner3.VertexIndex, corner.VertexIndex, corner3.NormalIndex, this.Normals, dictionary, edgeLines, surfaceLines);
			}
			foreach (uint num in dictionary.Keys)
			{
				ushort v4 = (ushort)(num & 65535u);
				ushort v5 = (ushort)(num >> 16);
				list.Add(new TriangleMesh.Line(v4, v5));
			}
			return new TriangleMesh(this.Origin, this.Normals, this.Vertices, edgeLines, surfaceLines, list, this.Corners, this.Triangles, this.ReferenceLines, this.ReferenceLineRadius);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003568 File Offset: 0x00001768
		private static void AddHalfEdge(ushort v0, ushort v1, ushort normalId, IList<TriangleMesh.Normal> normals, Dictionary<uint, ushort> halfEdges, List<TriangleMesh.EdgeLine> edgeLines, List<TriangleMesh.SurfaceLine> surfaceLines)
		{
			uint key = TriangleMesh.MakeKey(v0, v1);
			ushort num;
			if (!halfEdges.TryGetValue(key, out num))
			{
				halfEdges.Add(key, normalId);
				return;
			}
			halfEdges.Remove(key);
			TriangleMesh.Normal normal = normals[(int)normalId];
			TriangleMesh.Normal normal2 = normals[(int)num];
			float num2 = normal.X * normal2.X + normal.Y * normal2.Y + normal.Z * normal2.Z;
			if ((double)num2 < 0.999)
			{
				if ((double)num2 > 0.3)
				{
					surfaceLines.Add(new TriangleMesh.SurfaceLine(normalId, v0, num, v1));
					return;
				}
				edgeLines.Add(new TriangleMesh.EdgeLine(normalId, v0, num, v1));
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000361A File Offset: 0x0000181A
		private static uint MakeKey(ushort v0, ushort v1)
		{
			if (v0 > v1)
			{
				return (uint)((int)v0 + ((int)v1 << 16));
			}
			return (uint)((int)v1 + ((int)v0 << 16));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003630 File Offset: 0x00001830
		private static TriangleMesh.Normal Rotate(AffineTransform3 rotation, TriangleMesh.Normal normal)
		{
			Vector3 input = new Vector3((double)normal.X, (double)normal.Y, (double)normal.Z);
			Vector3 vector = rotation.Transform(input);
			return new TriangleMesh.Normal((float)vector.X, (float)vector.Y, (float)vector.Z);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003684 File Offset: 0x00001884
		private static TriangleMesh.Vertex Transform(AffineTransform3 placement, TriangleMesh.Vertex vertex)
		{
			Vector3 input = new Vector3((double)vertex.X, (double)vertex.Y, (double)vertex.Z);
			Vector3 vector = placement.Transform(input);
			return new TriangleMesh.Vertex((float)vector.X, (float)vector.Y, (float)vector.Z);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000036D5 File Offset: 0x000018D5
		private static TriangleMesh.ReferenceLine Transform(AffineTransform3 placement, TriangleMesh.ReferenceLine referenceLine)
		{
			return new TriangleMesh.ReferenceLine(TriangleMesh.Transform(placement, referenceLine.V0), TriangleMesh.Transform(placement, referenceLine.V1));
		}

		// Token: 0x0200002C RID: 44
		public struct Vertex
		{
			// Token: 0x060001F7 RID: 503 RVA: 0x000072D8 File Offset: 0x000054D8
			public Vertex(float x, float y, float z)
			{
				this = default(TriangleMesh.Vertex);
				this.X = x;
				this.Y = y;
				this.Z = z;
			}

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x060001F8 RID: 504 RVA: 0x000072F6 File Offset: 0x000054F6
			// (set) Token: 0x060001F9 RID: 505 RVA: 0x000072FE File Offset: 0x000054FE
			public float X { get; private set; }

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x060001FA RID: 506 RVA: 0x00007307 File Offset: 0x00005507
			// (set) Token: 0x060001FB RID: 507 RVA: 0x0000730F File Offset: 0x0000550F
			public float Y { get; private set; }

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x060001FC RID: 508 RVA: 0x00007318 File Offset: 0x00005518
			// (set) Token: 0x060001FD RID: 509 RVA: 0x00007320 File Offset: 0x00005520
			public float Z { get; private set; }
		}

		// Token: 0x0200002D RID: 45
		public struct Normal
		{
			// Token: 0x060001FE RID: 510 RVA: 0x00007329 File Offset: 0x00005529
			public Normal(float x, float y, float z)
			{
				this = default(TriangleMesh.Normal);
				this.X = x;
				this.Y = y;
				this.Z = z;
			}

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x060001FF RID: 511 RVA: 0x00007347 File Offset: 0x00005547
			// (set) Token: 0x06000200 RID: 512 RVA: 0x0000734F File Offset: 0x0000554F
			public float X { get; private set; }

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x06000201 RID: 513 RVA: 0x00007358 File Offset: 0x00005558
			// (set) Token: 0x06000202 RID: 514 RVA: 0x00007360 File Offset: 0x00005560
			public float Y { get; private set; }

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x06000203 RID: 515 RVA: 0x00007369 File Offset: 0x00005569
			// (set) Token: 0x06000204 RID: 516 RVA: 0x00007371 File Offset: 0x00005571
			public float Z { get; private set; }
		}

		// Token: 0x0200002E RID: 46
		public struct EdgeLine
		{
			// Token: 0x06000205 RID: 517 RVA: 0x0000737A File Offset: 0x0000557A
			public EdgeLine(ushort n0, ushort v0, ushort n1, ushort v1)
			{
				this = default(TriangleMesh.EdgeLine);
				this.N0 = n0;
				this.V0 = v0;
				this.N1 = n1;
				this.V1 = v1;
			}

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x06000206 RID: 518 RVA: 0x000073A0 File Offset: 0x000055A0
			// (set) Token: 0x06000207 RID: 519 RVA: 0x000073A8 File Offset: 0x000055A8
			public ushort N0 { get; private set; }

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x06000208 RID: 520 RVA: 0x000073B1 File Offset: 0x000055B1
			// (set) Token: 0x06000209 RID: 521 RVA: 0x000073B9 File Offset: 0x000055B9
			public ushort V0 { get; private set; }

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x0600020A RID: 522 RVA: 0x000073C2 File Offset: 0x000055C2
			// (set) Token: 0x0600020B RID: 523 RVA: 0x000073CA File Offset: 0x000055CA
			public ushort N1 { get; private set; }

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x0600020C RID: 524 RVA: 0x000073D3 File Offset: 0x000055D3
			// (set) Token: 0x0600020D RID: 525 RVA: 0x000073DB File Offset: 0x000055DB
			public ushort V1 { get; private set; }
		}

		// Token: 0x0200002F RID: 47
		public struct SurfaceLine
		{
			// Token: 0x0600020E RID: 526 RVA: 0x000073E4 File Offset: 0x000055E4
			public SurfaceLine(ushort n0, ushort v0, ushort n1, ushort v1)
			{
				this = default(TriangleMesh.SurfaceLine);
				this.N0 = n0;
				this.V0 = v0;
				this.N1 = n1;
				this.V1 = v1;
			}

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x0600020F RID: 527 RVA: 0x0000740A File Offset: 0x0000560A
			// (set) Token: 0x06000210 RID: 528 RVA: 0x00007412 File Offset: 0x00005612
			public ushort N0 { get; private set; }

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x06000211 RID: 529 RVA: 0x0000741B File Offset: 0x0000561B
			// (set) Token: 0x06000212 RID: 530 RVA: 0x00007423 File Offset: 0x00005623
			public ushort V0 { get; private set; }

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x06000213 RID: 531 RVA: 0x0000742C File Offset: 0x0000562C
			// (set) Token: 0x06000214 RID: 532 RVA: 0x00007434 File Offset: 0x00005634
			public ushort N1 { get; private set; }

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000215 RID: 533 RVA: 0x0000743D File Offset: 0x0000563D
			// (set) Token: 0x06000216 RID: 534 RVA: 0x00007445 File Offset: 0x00005645
			public ushort V1 { get; private set; }
		}

		// Token: 0x02000030 RID: 48
		public struct Line
		{
			// Token: 0x06000217 RID: 535 RVA: 0x0000744E File Offset: 0x0000564E
			public Line(ushort v0, ushort v1)
			{
				this = default(TriangleMesh.Line);
				this.V0 = v0;
				this.V1 = v1;
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000218 RID: 536 RVA: 0x00007465 File Offset: 0x00005665
			// (set) Token: 0x06000219 RID: 537 RVA: 0x0000746D File Offset: 0x0000566D
			public ushort V0 { get; private set; }

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x0600021A RID: 538 RVA: 0x00007476 File Offset: 0x00005676
			// (set) Token: 0x0600021B RID: 539 RVA: 0x0000747E File Offset: 0x0000567E
			public ushort V1 { get; private set; }
		}

		// Token: 0x02000031 RID: 49
		public struct Corner
		{
			// Token: 0x0600021C RID: 540 RVA: 0x00007487 File Offset: 0x00005687
			public Corner(ushort normalIndex, ushort vertexIndex)
			{
				this = default(TriangleMesh.Corner);
				this.NormalIndex = normalIndex;
				this.VertexIndex = vertexIndex;
			}

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x0600021D RID: 541 RVA: 0x0000749E File Offset: 0x0000569E
			// (set) Token: 0x0600021E RID: 542 RVA: 0x000074A6 File Offset: 0x000056A6
			public ushort NormalIndex { get; private set; }

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x0600021F RID: 543 RVA: 0x000074AF File Offset: 0x000056AF
			// (set) Token: 0x06000220 RID: 544 RVA: 0x000074B7 File Offset: 0x000056B7
			public ushort VertexIndex { get; private set; }
		}

		// Token: 0x02000032 RID: 50
		public struct Triangle
		{
			// Token: 0x06000221 RID: 545 RVA: 0x000074C0 File Offset: 0x000056C0
			public Triangle(ushort v0, ushort v1, ushort v2)
			{
				this = default(TriangleMesh.Triangle);
				this.V0 = v0;
				this.V1 = v1;
				this.V2 = v2;
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000222 RID: 546 RVA: 0x000074DE File Offset: 0x000056DE
			// (set) Token: 0x06000223 RID: 547 RVA: 0x000074E6 File Offset: 0x000056E6
			public ushort V0 { get; private set; }

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000224 RID: 548 RVA: 0x000074EF File Offset: 0x000056EF
			// (set) Token: 0x06000225 RID: 549 RVA: 0x000074F7 File Offset: 0x000056F7
			public ushort V1 { get; private set; }

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06000226 RID: 550 RVA: 0x00007500 File Offset: 0x00005700
			// (set) Token: 0x06000227 RID: 551 RVA: 0x00007508 File Offset: 0x00005708
			public ushort V2 { get; private set; }
		}

		// Token: 0x02000033 RID: 51
		public struct ReferenceLine
		{
			// Token: 0x06000228 RID: 552 RVA: 0x00007511 File Offset: 0x00005711
			public ReferenceLine(TriangleMesh.Vertex v0, TriangleMesh.Vertex v1)
			{
				this = default(TriangleMesh.ReferenceLine);
				this.V0 = v0;
				this.V1 = v1;
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000229 RID: 553 RVA: 0x00007528 File Offset: 0x00005728
			// (set) Token: 0x0600022A RID: 554 RVA: 0x00007530 File Offset: 0x00005730
			public TriangleMesh.Vertex V0 { get; private set; }

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x0600022B RID: 555 RVA: 0x00007539 File Offset: 0x00005739
			// (set) Token: 0x0600022C RID: 556 RVA: 0x00007541 File Offset: 0x00005741
			public TriangleMesh.Vertex V1 { get; private set; }
		}
	}
}
