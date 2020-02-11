using System;

namespace Hao.Geometry
{
	// Token: 0x0200000A RID: 10
	internal struct DistancePlane3Arc3
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002C9C File Offset: 0x00000E9C
		public DistancePlane3Arc3(Plane3 plane, Arc3 arc)
		{
			this = default(DistancePlane3Arc3);
			double num = arc.StartPoint.SignedDistanceTo(plane);
			double num2 = arc.EndPoint.SignedDistanceTo(plane);
			if (Math.Sign(num) != Math.Sign(num2))
			{
				this.SignedDistance = 0.0;
				return;
			}
			DistancePlane3Circle3 distancePlane3Circle = new DistancePlane3Circle3(plane, arc.Circle);
			double signedDistance = distancePlane3Circle.SignedDistance;
			Vector3 closestPointOnCircle = distancePlane3Circle.ClosestPointOnCircle;
			bool flag = arc.Contains(arc.Circle.GetPointDirectionAngle(closestPointOnCircle));
			if (signedDistance != 0.0)
			{
				if (flag)
				{
					this.SignedDistance = signedDistance;
					return;
				}
				this.SignedDistance = ((num > 0.0) ? Math.Min(num, num2) : Math.Max(num, num2));
				return;
			}
			else
			{
				if (Math.Sign(arc.Circle.Center.SignedDistanceTo(plane)) != Math.Sign(num))
				{
					this.SignedDistance = ((num > 0.0) ? Math.Min(num, num2) : Math.Max(num, num2));
					return;
				}
				if (flag)
				{
					this.SignedDistance = 0.0;
					return;
				}
				this.SignedDistance = ((num > 0.0) ? Math.Min(num, num2) : Math.Max(num, num2));
				return;
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002DDA File Offset: 0x00000FDA
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002DE2 File Offset: 0x00000FE2
		public double SignedDistance { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002DEB File Offset: 0x00000FEB
		public double Distance
		{
			get
			{
				return Math.Abs(this.SignedDistance);
			}
		}
	}
}
