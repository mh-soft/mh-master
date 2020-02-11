using System;

namespace Hao.Geometry
{
	// Token: 0x02000029 RID: 41
	internal struct DistanceSegment3Rectangle3
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x00007CC4 File Offset: 0x00005EC4
		public DistanceSegment3Rectangle3(Segment3 segment, Rectangle3 rectangle)
		{
			this = default(DistanceSegment3Rectangle3);
			Line3 line = new Line3(segment.Origin, segment.Direction);
			DistanceLine3Rectangle3 distanceLine3Rectangle = new DistanceLine3Rectangle3(line, rectangle);
			if (distanceLine3Rectangle.LineParameter < -segment.Extent)
			{
				DistanceVector3Rectangle3 distanceVector3Rectangle = new DistanceVector3Rectangle3(segment.NegativeEnd, rectangle);
				this.SquaredDistance = distanceVector3Rectangle.SquaredDistance;
				this.ClosestPointOnSegment = segment.NegativeEnd;
				this.ClosestPointOnRectangle = distanceVector3Rectangle.ClosestPointOnRectangle;
				this.SegmentParameter = -segment.Extent;
				this.RectCoord0 = distanceVector3Rectangle.RectCoord0;
				this.RectCoord1 = distanceVector3Rectangle.RectCoord1;
				return;
			}
			if (distanceLine3Rectangle.LineParameter <= segment.Extent)
			{
				this.SquaredDistance = distanceLine3Rectangle.SquaredDistance;
				this.ClosestPointOnSegment = distanceLine3Rectangle.ClosestPointOnLine;
				this.ClosestPointOnRectangle = distanceLine3Rectangle.ClosestPointOnRectangle;
				this.SegmentParameter = distanceLine3Rectangle.LineParameter;
				this.RectCoord0 = distanceLine3Rectangle.RectCoord0;
				this.RectCoord1 = distanceLine3Rectangle.RectCoord1;
				return;
			}
			DistanceVector3Rectangle3 distanceVector3Rectangle2 = new DistanceVector3Rectangle3(segment.PositiveEnd, rectangle);
			this.SquaredDistance = distanceVector3Rectangle2.SquaredDistance;
			this.ClosestPointOnSegment = segment.PositiveEnd;
			this.ClosestPointOnRectangle = distanceVector3Rectangle2.ClosestPointOnRectangle;
			this.SegmentParameter = segment.Extent;
			this.RectCoord0 = distanceVector3Rectangle2.RectCoord0;
			this.RectCoord1 = distanceVector3Rectangle2.RectCoord1;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00007E25 File Offset: 0x00006025
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00007E2D File Offset: 0x0000602D
		public double SquaredDistance { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00007E36 File Offset: 0x00006036
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00007E43 File Offset: 0x00006043
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00007E4B File Offset: 0x0000604B
		public double SegmentParameter { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00007E54 File Offset: 0x00006054
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00007E5C File Offset: 0x0000605C
		public double RectCoord0 { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00007E65 File Offset: 0x00006065
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00007E6D File Offset: 0x0000606D
		public double RectCoord1 { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00007E76 File Offset: 0x00006076
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00007E7E File Offset: 0x0000607E
		public Vector3 ClosestPointOnSegment { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00007E87 File Offset: 0x00006087
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00007E8F File Offset: 0x0000608F
		public Vector3 ClosestPointOnRectangle { get; private set; }
	}
}
