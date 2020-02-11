using System;

namespace Hao.Geometry
{
	// Token: 0x0200000F RID: 15
	internal struct DistancePlane3Rectangle3
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00003120 File Offset: 0x00001320
		public DistancePlane3Rectangle3(Plane3 plane, Rectangle3 rectangle)
		{
			this = default(DistancePlane3Rectangle3);
			DistanceVector3Plane3 distanceVector3Plane = new DistanceVector3Plane3(rectangle.MMCorner, plane);
			DistanceVector3Plane3 distanceVector3Plane2 = new DistanceVector3Plane3(rectangle.MPCorner, plane);
			DistanceVector3Plane3 distanceVector3Plane3 = new DistanceVector3Plane3(rectangle.PMCorner, plane);
			DistanceVector3Plane3 distanceVector3Plane4 = new DistanceVector3Plane3(rectangle.PPCorner, plane);
			double signedDistance = distanceVector3Plane.SignedDistance;
			double signedDistance2 = distanceVector3Plane2.SignedDistance;
			double signedDistance3 = distanceVector3Plane3.SignedDistance;
			double signedDistance4 = distanceVector3Plane4.SignedDistance;
			if (signedDistance > 0.0 && signedDistance2 > 0.0 && signedDistance3 > 0.0 && signedDistance4 > 0.0)
			{
				this.SignedDistance = Math.Min(signedDistance, Math.Min(signedDistance2, Math.Min(signedDistance3, signedDistance4)));
				return;
			}
			if (signedDistance < 0.0 && signedDistance2 < 0.0 && signedDistance3 < 0.0)
			{
				this.SignedDistance = Math.Max(signedDistance, Math.Max(signedDistance2, Math.Max(signedDistance3, signedDistance4)));
				return;
			}
			this.SignedDistance = 0.0;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000323A File Offset: 0x0000143A
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003242 File Offset: 0x00001442
		public double SignedDistance { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000324B File Offset: 0x0000144B
		public double Distance
		{
			get
			{
				return Math.Abs(this.SignedDistance);
			}
		}
	}
}
