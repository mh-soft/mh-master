using System;

namespace Hao.Geometry
{
	// Token: 0x0200002B RID: 43
	internal struct DistanceSegment3Triangle3
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00008918 File Offset: 0x00006B18
		public DistanceSegment3Triangle3(Segment3 segment, Triangle3 triangle)
		{
			this = default(DistanceSegment3Triangle3);
			Line3 line = new Line3(segment.Origin, segment.Direction);
			DistanceLine3Triangle3 distanceLine3Triangle = new DistanceLine3Triangle3(line, triangle);
			if (distanceLine3Triangle.LineParameter < -segment.Extent)
			{
				DistanceVector3Triangle3 distanceVector3Triangle = new DistanceVector3Triangle3(segment.NegativeEnd, triangle);
				this.SquaredDistance = distanceVector3Triangle.SquaredDistance;
				this.ClosestPointOnSegment = segment.NegativeEnd;
				this.ClosestPointOnTriangle = distanceVector3Triangle.ClosestPointOnTriangle;
				this.SegmentParameter = -segment.Extent;
				this.TriangleBary0 = distanceVector3Triangle.TriangleBary0;
				this.TriangleBary1 = distanceVector3Triangle.TriangleBary1;
				this.TriangleBary2 = distanceVector3Triangle.TriangleBary2;
				return;
			}
			if (distanceLine3Triangle.LineParameter <= segment.Extent)
			{
				this.SquaredDistance = distanceLine3Triangle.SquaredDistance;
				this.ClosestPointOnSegment = distanceLine3Triangle.ClosestPointOnLine;
				this.ClosestPointOnTriangle = distanceLine3Triangle.ClosestPointOnTriangle;
				this.SegmentParameter = distanceLine3Triangle.LineParameter;
				this.TriangleBary0 = distanceLine3Triangle.TriangleBary0;
				this.TriangleBary1 = distanceLine3Triangle.TriangleBary1;
				this.TriangleBary2 = distanceLine3Triangle.TriangleBary2;
				return;
			}
			DistanceVector3Triangle3 distanceVector3Triangle2 = new DistanceVector3Triangle3(segment.PositiveEnd, triangle);
			this.SquaredDistance = distanceVector3Triangle2.SquaredDistance;
			this.ClosestPointOnSegment = segment.PositiveEnd;
			this.ClosestPointOnTriangle = distanceVector3Triangle2.ClosestPointOnTriangle;
			this.SegmentParameter = segment.Extent;
			this.TriangleBary0 = distanceVector3Triangle2.TriangleBary0;
			this.TriangleBary1 = distanceVector3Triangle2.TriangleBary1;
			this.TriangleBary2 = distanceVector3Triangle2.TriangleBary2;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00008AA0 File Offset: 0x00006CA0
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00008AA8 File Offset: 0x00006CA8
		public double SquaredDistance { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00008AB1 File Offset: 0x00006CB1
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00008ABE File Offset: 0x00006CBE
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00008AC6 File Offset: 0x00006CC6
		public double SegmentParameter { get; private set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00008ACF File Offset: 0x00006CCF
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00008AD7 File Offset: 0x00006CD7
		public double TriangleBary0 { get; private set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00008AE0 File Offset: 0x00006CE0
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00008AE8 File Offset: 0x00006CE8
		public double TriangleBary1 { get; private set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00008AF1 File Offset: 0x00006CF1
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00008AF9 File Offset: 0x00006CF9
		public double TriangleBary2 { get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00008B02 File Offset: 0x00006D02
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00008B0A File Offset: 0x00006D0A
		public Vector3 ClosestPointOnSegment { get; private set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00008B13 File Offset: 0x00006D13
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00008B1B File Offset: 0x00006D1B
		public Vector3 ClosestPointOnTriangle { get; private set; }
	}
}
