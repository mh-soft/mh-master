using System;

namespace Hao.Geometry
{
	// Token: 0x0200000B RID: 11
	internal struct DistancePlane3Circle3
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public DistancePlane3Circle3(Plane3 plane, Circle3 circle)
		{
			this = default(DistancePlane3Circle3);
			double value = circle.Normal.Dot(plane.Normal);
			if (1.0 - Math.Abs(value) < 1E-08)
			{
				this.ClosestPointOnCircle = circle.Center + circle.Radius * circle.UnitU;
				this.SignedDistance = this.ClosestPointOnCircle.SignedDistanceTo(plane);
				return;
			}
			UnitVector3 vector = circle.Normal.UnitCross(plane.Normal);
			double num = circle.Center.SignedDistanceTo(plane);
			if (num < 0.0)
			{
				vector = -vector;
			}
			UnitVector3 vector2 = circle.Normal.UnitCross(vector);
			this.ClosestPointOnCircle = circle.Center + circle.Radius * vector2;
			double num2 = this.ClosestPointOnCircle.SignedDistanceTo(plane);
			if (Math.Sign(num) == Math.Sign(num2))
			{
				this.SignedDistance = num2;
				return;
			}
			this.SignedDistance = 0.0;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002F11 File Offset: 0x00001111
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002F19 File Offset: 0x00001119
		public Vector3 ClosestPointOnCircle { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002F22 File Offset: 0x00001122
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002F2A File Offset: 0x0000112A
		public double SignedDistance { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002F33 File Offset: 0x00001133
		public double Distance
		{
			get
			{
				return Math.Abs(this.SignedDistance);
			}
		}
	}
}
