using System;

namespace Hao.Geometry
{
	// Token: 0x02000026 RID: 38
	internal struct DistanceRectangle3Rectangle3
	{
		// Token: 0x06000183 RID: 387 RVA: 0x000074D8 File Offset: 0x000056D8
		public DistanceRectangle3Rectangle3(Rectangle3 rectangle0, Rectangle3 rectangle1)
		{
			this = default(DistanceRectangle3Rectangle3);
			this.SquaredDistance = double.MaxValue;
			for (int i = 0; i < 2; i++)
			{
				for (int j = -1; j <= 1; j += 2)
				{
					double num = (i == 0) ? rectangle0.Extent1 : rectangle0.Extent0;
					UnitVector3 vector = (i == 0) ? rectangle0.Axis1 : rectangle0.Axis0;
					Vector3 origin = rectangle0.Center + (double)j * num * vector;
					UnitVector3 direction = (i == 0) ? rectangle0.Axis0 : rectangle0.Axis1;
					double extent = (i == 0) ? rectangle0.Extent0 : rectangle0.Extent1;
					Segment3 segment = new Segment3(origin, direction, extent);
					DistanceSegment3Rectangle3 distanceSegment3Rectangle = new DistanceSegment3Rectangle3(segment, rectangle1);
					if (distanceSegment3Rectangle.SquaredDistance < this.SquaredDistance)
					{
						this.ClosestPoint0 = distanceSegment3Rectangle.ClosestPointOnSegment;
						this.ClosestPoint1 = distanceSegment3Rectangle.ClosestPointOnRectangle;
						this.SquaredDistance = distanceSegment3Rectangle.SquaredDistance;
					}
				}
			}
			for (int i = 0; i < 2; i++)
			{
				for (int j = -1; j <= 1; j += 2)
				{
					double num2 = (i == 0) ? rectangle1.Extent1 : rectangle1.Extent0;
					UnitVector3 vector2 = (i == 0) ? rectangle1.Axis1 : rectangle1.Axis0;
					Vector3 origin2 = rectangle1.Center + (double)j * num2 * vector2;
					UnitVector3 direction2 = (i == 0) ? rectangle1.Axis0 : rectangle1.Axis1;
					double extent2 = (i == 0) ? rectangle1.Extent0 : rectangle1.Extent1;
					Segment3 segment2 = new Segment3(origin2, direction2, extent2);
					DistanceSegment3Rectangle3 distanceSegment3Rectangle2 = new DistanceSegment3Rectangle3(segment2, rectangle0);
					if (distanceSegment3Rectangle2.SquaredDistance < this.SquaredDistance)
					{
						this.ClosestPoint0 = distanceSegment3Rectangle2.ClosestPointOnRectangle;
						this.ClosestPoint1 = distanceSegment3Rectangle2.ClosestPointOnSegment;
						this.SquaredDistance = distanceSegment3Rectangle2.SquaredDistance;
					}
				}
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000076B5 File Offset: 0x000058B5
		// (set) Token: 0x06000185 RID: 389 RVA: 0x000076BD File Offset: 0x000058BD
		public double SquaredDistance { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000076C6 File Offset: 0x000058C6
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000076D3 File Offset: 0x000058D3
		// (set) Token: 0x06000188 RID: 392 RVA: 0x000076DB File Offset: 0x000058DB
		public Vector3 ClosestPoint0 { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000076E4 File Offset: 0x000058E4
		// (set) Token: 0x0600018A RID: 394 RVA: 0x000076EC File Offset: 0x000058EC
		public Vector3 ClosestPoint1 { get; private set; }
	}
}
