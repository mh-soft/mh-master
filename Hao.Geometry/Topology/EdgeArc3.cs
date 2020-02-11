using System;

namespace Hao.Geometry.Topology
{
	// Token: 0x02000004 RID: 4
	public class EdgeArc3 : IEdge3
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002251 File Offset: 0x00000451
		public EdgeArc3(Arc3 arc)
		{
			this.Arc = arc;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002260 File Offset: 0x00000460
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002268 File Offset: 0x00000468
		public Arc3 Arc { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002274 File Offset: 0x00000474
		public Vector3 StartPoint
		{
			get
			{
				return this.Arc.StartPoint;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002290 File Offset: 0x00000490
		public Vector3 EndPoint
		{
			get
			{
				return this.Arc.EndPoint;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022AC File Offset: 0x000004AC
		public UnitVector3 StartDirection
		{
			get
			{
				return this.Arc.Circle.GetTangent(this.Arc.StartAngle).Direction;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022E8 File Offset: 0x000004E8
		public UnitVector3 EndDirection
		{
			get
			{
				return this.Arc.Circle.GetTangent(this.Arc.StartAngle + this.Arc.DeltaAngle).Direction;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002334 File Offset: 0x00000534
		public AxisAlignedBox3 ComputeAxisAlignedBoundingBox()
		{
			return this.Arc.ComputeAxisAlignedBoundingBox();
		}
	}
}
