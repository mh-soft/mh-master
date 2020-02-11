using System;

namespace Hao.Geometry
{
	// Token: 0x0200000D RID: 13
	internal struct DistancePlane3Ray3
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002FBC File Offset: 0x000011BC
		public DistancePlane3Ray3(Plane3 plane, Ray3 ray)
		{
			this = default(DistancePlane3Ray3);
			DistanceVector3Plane3 distanceVector3Plane = new DistanceVector3Plane3(ray.Origin, plane);
			double signedDistance = distanceVector3Plane.SignedDistance;
			double num = ray.Direction.Dot(plane.Normal);
			if ((signedDistance > 0.0 && num > -1E-08) || (signedDistance < 0.0 && num < 1E-08))
			{
				this.SignedDistance = signedDistance;
				return;
			}
			this.SignedDistance = 0.0;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003045 File Offset: 0x00001245
		// (set) Token: 0x06000054 RID: 84 RVA: 0x0000304D File Offset: 0x0000124D
		public double SignedDistance { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003056 File Offset: 0x00001256
		public double Distance
		{
			get
			{
				return Math.Abs(this.SignedDistance);
			}
		}
	}
}
