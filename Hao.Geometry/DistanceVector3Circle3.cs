using System;

namespace Hao.Geometry
{
	// Token: 0x02000031 RID: 49
	internal struct DistanceVector3Circle3
	{
		// Token: 0x0600021E RID: 542 RVA: 0x000095DC File Offset: 0x000077DC
		public DistanceVector3Circle3(Vector3 vector, Circle3 circle)
		{
			this = default(DistanceVector3Circle3);
			Vector3 left = vector - circle.Center;
			double num = left.Dot(circle.Normal);
			Vector3 vector2 = left - num * circle.Normal;
			double squaredLength = vector2.SquaredLength;
			if (squaredLength >= 1E-08)
			{
				this.ClosestPointOnCircle = circle.Center + circle.Radius / Math.Sqrt(squaredLength) * vector2;
				this.SquaredDistance = (vector - this.ClosestPointOnCircle).SquaredLength;
			}
			else
			{
				this.ClosestPointOnCircle = circle.GetEdgePoint(Angle.FromRadians(0.0));
				double num2 = circle.Radius * circle.Radius;
				double num3 = num;
				this.SquaredDistance = num2 + num3 * num3;
			}
			this.ClosestPointOnVector = vector;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000096B3 File Offset: 0x000078B3
		// (set) Token: 0x06000220 RID: 544 RVA: 0x000096BB File Offset: 0x000078BB
		public double SquaredDistance { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000096C4 File Offset: 0x000078C4
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000096D1 File Offset: 0x000078D1
		// (set) Token: 0x06000223 RID: 547 RVA: 0x000096D9 File Offset: 0x000078D9
		public Vector3 ClosestPointOnVector { get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000096E2 File Offset: 0x000078E2
		// (set) Token: 0x06000225 RID: 549 RVA: 0x000096EA File Offset: 0x000078EA
		public Vector3 ClosestPointOnCircle { get; private set; }
	}
}
