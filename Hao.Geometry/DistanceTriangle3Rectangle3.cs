using System;

namespace Hao.Geometry
{
	// Token: 0x0200002D RID: 45
	internal struct DistanceTriangle3Rectangle3
	{
		// Token: 0x060001EE RID: 494 RVA: 0x00008D90 File Offset: 0x00006F90
		public DistanceTriangle3Rectangle3(Triangle3 triangle, Rectangle3 rectangle)
		{
			this = default(DistanceTriangle3Rectangle3);
			double num = double.MaxValue;
			int i = 2;
			int j = 0;
			while (j < 3)
			{
				Segment3 segment = new Segment3(triangle[i], triangle[j]);
				DistanceSegment3Rectangle3 distanceSegment3Rectangle = new DistanceSegment3Rectangle3(segment, rectangle);
				if (distanceSegment3Rectangle.SquaredDistance < num)
				{
					this.ClosestPointOnTriangle = distanceSegment3Rectangle.ClosestPointOnSegment;
					this.ClosestPointOnRectangle = distanceSegment3Rectangle.ClosestPointOnRectangle;
					num = distanceSegment3Rectangle.SquaredDistance;
				}
				i = j++;
			}
			for (j = 0; j < 2; j++)
			{
				for (i = -1; i <= 1; i += 2)
				{
					double num2 = (j == 0) ? rectangle.Extent1 : rectangle.Extent0;
					UnitVector3 vector = (j == 0) ? rectangle.Axis1 : rectangle.Axis0;
					Vector3 origin = rectangle.Center + (double)i * num2 * vector;
					UnitVector3 direction = (j == 0) ? rectangle.Axis0 : rectangle.Axis1;
					double extent = (j == 0) ? rectangle.Extent0 : rectangle.Extent1;
					Segment3 segment2 = new Segment3(origin, direction, extent);
					DistanceSegment3Triangle3 distanceSegment3Triangle = new DistanceSegment3Triangle3(segment2, triangle);
					if (distanceSegment3Triangle.SquaredDistance < num)
					{
						this.ClosestPointOnTriangle = distanceSegment3Triangle.ClosestPointOnTriangle;
						this.ClosestPointOnRectangle = distanceSegment3Triangle.ClosestPointOnSegment;
						num = distanceSegment3Triangle.SquaredDistance;
					}
				}
			}
			this.SquaredDistance = num;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00008EE6 File Offset: 0x000070E6
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00008EEE File Offset: 0x000070EE
		public double SquaredDistance { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00008EF7 File Offset: 0x000070F7
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00008F04 File Offset: 0x00007104
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00008F0C File Offset: 0x0000710C
		public Vector3 ClosestPointOnTriangle { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00008F15 File Offset: 0x00007115
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00008F1D File Offset: 0x0000711D
		public Vector3 ClosestPointOnRectangle { get; private set; }
	}
}
