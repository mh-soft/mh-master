using System;

namespace Hao.Geometry.Topology
{
	// Token: 0x02000006 RID: 6
	public class EdgeLineSegment2 : IEdge2
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000023AF File Offset: 0x000005AF
		public EdgeLineSegment2(Vector2 startPoint, Vector2 endPoint)
		{
			this.Segment = new Segment2(startPoint, endPoint);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023C4 File Offset: 0x000005C4
		public EdgeLineSegment2(Segment2 segment)
		{
			this.Segment = segment;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000023D3 File Offset: 0x000005D3
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000023DB File Offset: 0x000005DB
		public Segment2 Segment { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000023E4 File Offset: 0x000005E4
		public Vector2 StartPoint
		{
			get
			{
				return this.Segment.NegativeEnd;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002400 File Offset: 0x00000600
		public Vector2 EndPoint
		{
			get
			{
				return this.Segment.PositiveEnd;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000241C File Offset: 0x0000061C
		public UnitVector2 StartDirection
		{
			get
			{
				return this.Segment.Direction;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002438 File Offset: 0x00000638
		public UnitVector2 EndDirection
		{
			get
			{
				return this.Segment.Direction;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002453 File Offset: 0x00000653
		public AxisAlignedBox2 ComputeAxisAlignedBoundingBox()
		{
			return this.Segment.ComputeAxisAlignedBoundingBox();
		}
	}
}
