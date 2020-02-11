using System;

namespace Hao.Geometry
{
	// Token: 0x02000041 RID: 65
	internal struct IntersectionRay2Segment2
	{
		// Token: 0x06000294 RID: 660 RVA: 0x0000AD0E File Offset: 0x00008F0E
		public IntersectionRay2Segment2(Ray2 ray, Segment2 segment)
		{
			this = default(IntersectionRay2Segment2);
			this.ray = ray;
			this.segment = segment;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000AD25 File Offset: 0x00008F25
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000AD2D File Offset: 0x00008F2D
		public Vector2 Point0 { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000AD36 File Offset: 0x00008F36
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000AD3E File Offset: 0x00008F3E
		public Vector2 Point1 { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000AD47 File Offset: 0x00008F47
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000AD4F File Offset: 0x00008F4F
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x0600029B RID: 667 RVA: 0x0000AD58 File Offset: 0x00008F58
		public bool Find()
		{
			double[] array = new double[2];
			this.IntersectionType = IntersectionLine2Line2.Classify(this.ray.Origin, this.ray.Direction, this.segment.Origin, this.segment.Direction, 1E-08, ref array);
			if (this.IntersectionType == Intersection.Type.IT_POINT)
			{
				if (array[0] >= 0.0 && Math.Abs(array[1]) <= this.segment.Extent)
				{
					this.Point0 = this.ray.Origin + array[0] * this.ray.Direction;
				}
				else
				{
					this.IntersectionType = Intersection.Type.IT_EMPTY;
				}
			}
			else if (this.IntersectionType == Intersection.Type.IT_LINE)
			{
				double num = this.ray.Direction.Dot(this.segment.Origin - this.ray.Origin);
				double v = num - this.segment.Extent;
				double v2 = num + this.segment.Extent;
				Intersector1 intersector = new Intersector1(0.0, double.MaxValue, v, v2);
				intersector.Find();
				int quantity = intersector.Quantity;
				if (quantity == 2)
				{
					this.IntersectionType = Intersection.Type.IT_SEGMENT;
					this.Point0 = this.ray.Origin + intersector.Overlap0 * this.ray.Direction;
					this.Point1 = this.ray.Origin + intersector.Overlap1 * this.ray.Direction;
				}
				else if (quantity == 1)
				{
					this.IntersectionType = Intersection.Type.IT_POINT;
					this.Point0 = this.ray.Origin;
				}
				else
				{
					this.IntersectionType = Intersection.Type.IT_EMPTY;
				}
			}
			return this.IntersectionType > Intersection.Type.IT_EMPTY;
		}

		// Token: 0x0400009A RID: 154
		private readonly Ray2 ray;

		// Token: 0x0400009B RID: 155
		private readonly Segment2 segment;
	}
}
