using System;
using System.Collections.Generic;

namespace Hao.Geometry.Topology
{
	// Token: 0x02000008 RID: 8
	public class EdgePolyLineSegment2 : IEdge2
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002514 File Offset: 0x00000714
		public EdgePolyLineSegment2(ICollection<Vector2> vertices)
		{
			if (vertices.Count < 3)
			{
				throw new ArgumentException("needs at least three vertices", "vertices");
			}
			this.vertices = new List<Vector2>(vertices);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002541 File Offset: 0x00000741
		public Vector2 StartPoint
		{
			get
			{
				return this.vertices[0];
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000254F File Offset: 0x0000074F
		public Vector2 EndPoint
		{
			get
			{
				if (this.vertices.Count == 0)
				{
					return Vector2.Zero;
				}
				return this.vertices[this.vertices.Count - 1];
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000257C File Offset: 0x0000077C
		public UnitVector2 StartDirection
		{
			get
			{
				if (this.vertices.Count == 0)
				{
					return UnitVector2.UnitX;
				}
				return (this.vertices[1] - this.vertices[0]).GetNormalized();
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000025C4 File Offset: 0x000007C4
		public UnitVector2 EndDirection
		{
			get
			{
				int count = this.vertices.Count;
				return (this.vertices[count - 1] - this.vertices[count - 2]).GetNormalized();
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002606 File Offset: 0x00000806
		public IList<Vector2> Vertices
		{
			get
			{
				return this.vertices;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002610 File Offset: 0x00000810
		public AxisAlignedBox2 ComputeAxisAlignedBoundingBox()
		{
			Vector2 vector = this.vertices[0];
			Vector2 vector2 = vector;
			foreach (Vector2 value in this.vertices)
			{
				vector = Vector2.Min(vector, value);
				vector2 = Vector2.Max(vector2, value);
			}
			return new AxisAlignedBox2(vector, vector2);
		}

		// Token: 0x04000008 RID: 8
		private readonly List<Vector2> vertices;
	}
}
