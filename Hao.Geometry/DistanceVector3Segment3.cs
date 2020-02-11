using System;

namespace Hao.Geometry
{
	// Token: 0x02000034 RID: 52
	internal struct DistanceVector3Segment3
	{
		// Token: 0x0600023A RID: 570 RVA: 0x00009920 File Offset: 0x00007B20
		public DistanceVector3Segment3(Vector3 vector, Segment3 segment)
		{
			this = default(DistanceVector3Segment3);
			Vector3 vector2 = vector - segment.Origin;
			double num = segment.Direction.Dot(vector2);
			if (num > -segment.Extent)
			{
				if (num < segment.Extent)
				{
					this.SegmentParameter = num;
					this.ClosestPointOnSegment = segment.Origin + num * segment.Direction;
				}
				else
				{
					this.ClosestPointOnSegment = segment.PositiveEnd;
					this.SegmentParameter = segment.Extent;
				}
			}
			else
			{
				this.ClosestPointOnSegment = segment.NegativeEnd;
				this.SegmentParameter = -segment.Extent;
			}
			this.ClosestPointOnVector = vector;
			this.SquaredDistance = (this.ClosestPointOnSegment - this.ClosestPointOnVector).SquaredLength;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600023B RID: 571 RVA: 0x000099ED File Offset: 0x00007BED
		// (set) Token: 0x0600023C RID: 572 RVA: 0x000099F5 File Offset: 0x00007BF5
		public double SquaredDistance { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000099FE File Offset: 0x00007BFE
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00009A0B File Offset: 0x00007C0B
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00009A13 File Offset: 0x00007C13
		public double SegmentParameter { get; private set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00009A1C File Offset: 0x00007C1C
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00009A24 File Offset: 0x00007C24
		public Vector3 ClosestPointOnVector { get; private set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00009A2D File Offset: 0x00007C2D
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00009A35 File Offset: 0x00007C35
		public Vector3 ClosestPointOnSegment { get; private set; }
	}
}
