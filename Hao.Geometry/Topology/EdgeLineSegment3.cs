using System;

namespace Hao.Geometry.Topology
{
	// Token: 0x02000007 RID: 7
	public class EdgeLineSegment3 : IEdge3
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002460 File Offset: 0x00000660
		public EdgeLineSegment3(Vector3 startPoint, Vector3 endPoint)
		{
			this.Segment = new Segment3(startPoint, endPoint);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002475 File Offset: 0x00000675
		public EdgeLineSegment3(Segment3 segment)
		{
			this.Segment = segment;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002484 File Offset: 0x00000684
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000248C File Offset: 0x0000068C
		public Segment3 Segment { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002498 File Offset: 0x00000698
		public Vector3 StartPoint
		{
			get
			{
				return this.Segment.NegativeEnd;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000024B4 File Offset: 0x000006B4
		public Vector3 EndPoint
		{
			get
			{
				return this.Segment.PositiveEnd;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000024D0 File Offset: 0x000006D0
		public UnitVector3 StartDirection
		{
			get
			{
				return this.Segment.Direction;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000024EC File Offset: 0x000006EC
		public UnitVector3 EndDirection
		{
			get
			{
				return this.Segment.Direction;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002507 File Offset: 0x00000707
		public AxisAlignedBox3 ComputeAxisAlignedBoundingBox()
		{
			return this.Segment.ComputeAxisAlignedBoundingBox();
		}
	}
}
