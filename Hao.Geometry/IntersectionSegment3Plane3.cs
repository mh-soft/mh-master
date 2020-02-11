using System;

namespace Hao.Geometry
{
	// Token: 0x0200006B RID: 107
	internal struct IntersectionSegment3Plane3
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x00012B4F File Offset: 0x00010D4F
		public IntersectionSegment3Plane3(Segment3 segment, Plane3 plane)
		{
			this = default(IntersectionSegment3Plane3);
			this.segment = segment;
			this.plane = plane;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00012B66 File Offset: 0x00010D66
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00012B6E File Offset: 0x00010D6E
		public double SegmentParameter { get; private set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00012B77 File Offset: 0x00010D77
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00012B7F File Offset: 0x00010D7F
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x06000425 RID: 1061 RVA: 0x00012B88 File Offset: 0x00010D88
		public bool Test()
		{
			Vector3 negativeEnd = this.segment.NegativeEnd;
			double num = this.plane.Normal.Dot(negativeEnd) - this.plane.Constant;
			if (Math.Abs(num) <= 1E-08)
			{
				num = 0.0;
			}
			Vector3 positiveEnd = this.segment.PositiveEnd;
			double num2 = this.plane.Normal.Dot(positiveEnd) - this.plane.Constant;
			if (Math.Abs(num2) <= 1E-08)
			{
				num2 = 0.0;
			}
			double num3 = num * num2;
			if (num3 < 0.0)
			{
				this.IntersectionType = Intersection.Type.IT_POINT;
				return true;
			}
			if (num3 > 0.0)
			{
				this.IntersectionType = Intersection.Type.IT_EMPTY;
				return false;
			}
			if (num != 0.0 || num2 != 0.0)
			{
				this.IntersectionType = Intersection.Type.IT_POINT;
				return true;
			}
			this.IntersectionType = Intersection.Type.IT_SEGMENT;
			return true;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00012C9C File Offset: 0x00010E9C
		public bool Find()
		{
			Line3 line = new Line3(this.segment.Origin, this.segment.Direction);
			IntersectionLine3Plane3 intersectionLine3Plane = new IntersectionLine3Plane3(line, this.plane);
			if (intersectionLine3Plane.Find())
			{
				this.IntersectionType = intersectionLine3Plane.IntersectionType;
				this.SegmentParameter = intersectionLine3Plane.LineParameter;
				return Math.Abs(this.SegmentParameter) < this.segment.Extent + 1E-08;
			}
			this.IntersectionType = Intersection.Type.IT_EMPTY;
			return false;
		}

		// Token: 0x04000132 RID: 306
		private readonly Segment3 segment;

		// Token: 0x04000133 RID: 307
		private readonly Plane3 plane;
	}
}
