using System;

namespace Hao.Geometry.Topology
{
	// Token: 0x02000003 RID: 3
	public class EdgeArc2 : IEdge2
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002161 File Offset: 0x00000361
		public EdgeArc2(Arc2 arc)
		{
			this.Arc = arc;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002170 File Offset: 0x00000370
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002178 File Offset: 0x00000378
		public Arc2 Arc { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002184 File Offset: 0x00000384
		public Vector2 StartPoint
		{
			get
			{
				return this.Arc.StartPoint;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021A0 File Offset: 0x000003A0
		public Vector2 EndPoint
		{
			get
			{
				return this.Arc.EndPoint;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021BC File Offset: 0x000003BC
		public UnitVector2 StartDirection
		{
			get
			{
				return this.Arc.Circle.GetTangent(this.Arc.StartAngle).Direction;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021F8 File Offset: 0x000003F8
		public UnitVector2 EndDirection
		{
			get
			{
				return this.Arc.Circle.GetTangent(this.Arc.StartAngle + this.Arc.DeltaAngle).Direction;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002244 File Offset: 0x00000444
		public AxisAlignedBox2 ComputeAxisAlignedBoundingBox()
		{
			return this.Arc.ComputeAxisAlignedBoundingBox();
		}
	}
}
