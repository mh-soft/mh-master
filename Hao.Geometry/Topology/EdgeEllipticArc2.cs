using System;

namespace Hao.Geometry.Topology
{
	// Token: 0x02000005 RID: 5
	public class EdgeEllipticArc2 : IEdge2
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002341 File Offset: 0x00000541
		public EdgeEllipticArc2(EllipticArc2 ellipticArc)
		{
			this.EllipticArc = ellipticArc;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002350 File Offset: 0x00000550
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002358 File Offset: 0x00000558
		public EllipticArc2 EllipticArc { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002364 File Offset: 0x00000564
		public Vector2 StartPoint
		{
			get
			{
				return this.EllipticArc.StartPoint;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002380 File Offset: 0x00000580
		public Vector2 EndPoint
		{
			get
			{
				return this.EllipticArc.EndPoint;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000239B File Offset: 0x0000059B
		public UnitVector2 StartDirection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000239B File Offset: 0x0000059B
		public UnitVector2 EndDirection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023A2 File Offset: 0x000005A2
		public AxisAlignedBox2 ComputeAxisAlignedBoundingBox()
		{
			return this.EllipticArc.ComputeAxisAlignedBoundingBox();
		}
	}
}
