using System;

namespace Hao.Geometry
{
	// Token: 0x0200002E RID: 46
	internal struct DistanceTriangle3Triangle3
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x00008F28 File Offset: 0x00007128
		public DistanceTriangle3Triangle3(Triangle3 triangle0, Triangle3 triangle1)
		{
			this = default(DistanceTriangle3Triangle3);
			double num = double.MaxValue;
			int index = 2;
			int i = 0;
			while (i < 3)
			{
				Segment3 segment = new Segment3(triangle0[index], triangle0[i]);
				DistanceSegment3Triangle3 distanceSegment3Triangle = new DistanceSegment3Triangle3(segment, triangle1);
				double squaredDistance = distanceSegment3Triangle.SquaredDistance;
				if (squaredDistance < num)
				{
					this.ClosestPoint0 = distanceSegment3Triangle.ClosestPointOnSegment;
					this.ClosestPoint1 = distanceSegment3Triangle.ClosestPointOnTriangle;
					num = squaredDistance;
					if (num <= 1E-08)
					{
						this.SquaredDistance = 0.0;
						return;
					}
				}
				index = i++;
			}
			int index2 = 2;
			int j = 0;
			while (j < 3)
			{
				Segment3 segment2 = new Segment3(triangle1[index2], triangle1[j]);
				DistanceSegment3Triangle3 distanceSegment3Triangle2 = new DistanceSegment3Triangle3(segment2, triangle0);
				double squaredDistance = distanceSegment3Triangle2.SquaredDistance;
				if (squaredDistance < num)
				{
					this.ClosestPoint0 = distanceSegment3Triangle2.ClosestPointOnTriangle;
					this.ClosestPoint1 = distanceSegment3Triangle2.ClosestPointOnSegment;
					num = squaredDistance;
					if (num <= 1E-08)
					{
						this.SquaredDistance = 0.0;
						return;
					}
				}
				index2 = j++;
			}
			this.SquaredDistance = num;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000903F File Offset: 0x0000723F
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00009047 File Offset: 0x00007247
		public Vector3 ClosestPoint0 { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00009050 File Offset: 0x00007250
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00009058 File Offset: 0x00007258
		public Vector3 ClosestPoint1 { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00009061 File Offset: 0x00007261
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00009069 File Offset: 0x00007269
		public double SquaredDistance { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00009072 File Offset: 0x00007272
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}
	}
}
