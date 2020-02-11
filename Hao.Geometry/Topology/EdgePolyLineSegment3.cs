using System;
using System.Collections.Generic;

namespace Hao.Geometry.Topology
{
	// Token: 0x02000009 RID: 9
	public class EdgePolyLineSegment3 : IEdge3
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002684 File Offset: 0x00000884
		public EdgePolyLineSegment3(ICollection<Vector3> vertices)
		{
			if (vertices.Count < 3)
			{
				throw new ArgumentException("needs at least three vertices", "vertices");
			}
			this.vertices = new List<Vector3>(vertices);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000026B1 File Offset: 0x000008B1
		public Vector3 StartPoint
		{
			get
			{
				return this.vertices[0];
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000026BF File Offset: 0x000008BF
		public Vector3 EndPoint
		{
			get
			{
				if (this.vertices.Count == 0)
				{
					return Vector3.Zero;
				}
				return this.vertices[this.vertices.Count - 1];
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000026EC File Offset: 0x000008EC
		public UnitVector3 StartDirection
		{
			get
			{
				if (this.vertices.Count == 0)
				{
					return UnitVector3.UnitX;
				}
				return (this.vertices[1] - this.vertices[0]).GetNormalized();
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002734 File Offset: 0x00000934
		public UnitVector3 EndDirection
		{
			get
			{
				int count = this.vertices.Count;
				return (this.vertices[count - 1] - this.vertices[count - 3]).GetNormalized();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002776 File Offset: 0x00000976
		public IList<Vector3> Vertices
		{
			get
			{
				return this.vertices;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002780 File Offset: 0x00000980
		public AxisAlignedBox3 ComputeAxisAlignedBoundingBox()
		{
			Vector3 vector = this.vertices[0];
			Vector3 vector2 = vector;
			foreach (Vector3 value in this.vertices)
			{
				vector = Vector3.Min(vector, value);
				vector2 = Vector3.Max(vector2, value);
			}
			return new AxisAlignedBox3(vector, vector2);
		}

		// Token: 0x04000009 RID: 9
		private readonly List<Vector3> vertices;
	}
}
