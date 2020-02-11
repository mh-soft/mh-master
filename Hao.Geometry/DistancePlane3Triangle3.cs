using System;

namespace Hao.Geometry
{
	// Token: 0x02000010 RID: 16
	internal struct DistancePlane3Triangle3
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00003258 File Offset: 0x00001458
		public DistancePlane3Triangle3(Plane3 plane, Triangle3 triangle)
		{
			this = default(DistancePlane3Triangle3);
			DistanceVector3Plane3 distanceVector3Plane = new DistanceVector3Plane3(triangle.V0, plane);
			DistanceVector3Plane3 distanceVector3Plane2 = new DistanceVector3Plane3(triangle.V1, plane);
			DistanceVector3Plane3 distanceVector3Plane3 = new DistanceVector3Plane3(triangle.V2, plane);
			double signedDistance = distanceVector3Plane.SignedDistance;
			double signedDistance2 = distanceVector3Plane2.SignedDistance;
			double signedDistance3 = distanceVector3Plane3.SignedDistance;
			if (signedDistance > 0.0 && signedDistance2 > 0.0 && signedDistance3 > 0.0)
			{
				this.SignedDistance = Math.Min(signedDistance, Math.Min(signedDistance2, signedDistance3));
				return;
			}
			if (signedDistance < 0.0 && signedDistance2 < 0.0 && signedDistance3 < 0.0)
			{
				this.SignedDistance = Math.Max(signedDistance, Math.Max(signedDistance2, signedDistance3));
				return;
			}
			this.SignedDistance = 0.0;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000333A File Offset: 0x0000153A
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003342 File Offset: 0x00001542
		public double SignedDistance { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000334B File Offset: 0x0000154B
		public double Distance
		{
			get
			{
				return Math.Abs(this.SignedDistance);
			}
		}
	}
}
