using System;

namespace Hao.Geometry
{
	// Token: 0x02000028 RID: 40
	internal struct DistanceSegment3Box3
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00007B9C File Offset: 0x00005D9C
		public DistanceSegment3Box3(Segment3 segment, Box3 box)
		{
			this = default(DistanceSegment3Box3);
			Line3 line = new Line3(segment.Origin, segment.Direction);
			DistanceLine3Box3 distanceLine3Box = new DistanceLine3Box3(line, box);
			if (distanceLine3Box.LineParameter < -segment.Extent)
			{
				DistanceVector3Box3 distanceVector3Box = new DistanceVector3Box3(segment.NegativeEnd, box);
				this.SquaredDistance = distanceVector3Box.SquaredDistance;
				this.ClosestPointOnSegment = distanceVector3Box.ClosestPointOnVector;
				this.ClosestPointOnBox = distanceVector3Box.ClosestPointOnBox;
				return;
			}
			if (distanceLine3Box.LineParameter <= segment.Extent)
			{
				this.SquaredDistance = distanceLine3Box.SquaredDistance;
				this.ClosestPointOnSegment = distanceLine3Box.ClosestPointOnLine;
				this.ClosestPointOnBox = distanceLine3Box.ClosestPointOnBox;
				return;
			}
			DistanceVector3Box3 distanceVector3Box2 = new DistanceVector3Box3(segment.PositiveEnd, box);
			this.SquaredDistance = distanceVector3Box2.SquaredDistance;
			this.ClosestPointOnSegment = distanceVector3Box2.ClosestPointOnVector;
			this.ClosestPointOnBox = distanceVector3Box2.ClosestPointOnBox;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00007C84 File Offset: 0x00005E84
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00007C8C File Offset: 0x00005E8C
		public double SquaredDistance { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00007C95 File Offset: 0x00005E95
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00007CA2 File Offset: 0x00005EA2
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00007CAA File Offset: 0x00005EAA
		public Vector3 ClosestPointOnSegment { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00007CB3 File Offset: 0x00005EB3
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00007CBB File Offset: 0x00005EBB
		public Vector3 ClosestPointOnBox { get; private set; }
	}
}
