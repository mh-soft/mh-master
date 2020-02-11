using System;

namespace Hao.Geometry
{
	// Token: 0x0200000E RID: 14
	internal struct DistancePlane3Segment3
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00003064 File Offset: 0x00001264
		public DistancePlane3Segment3(Plane3 plane, Segment3 segment)
		{
			this = default(DistancePlane3Segment3);
			DistanceVector3Plane3 distanceVector3Plane = new DistanceVector3Plane3(segment.NegativeEnd, plane);
			DistanceVector3Plane3 distanceVector3Plane2 = new DistanceVector3Plane3(segment.PositiveEnd, plane);
			double signedDistance = distanceVector3Plane.SignedDistance;
			double signedDistance2 = distanceVector3Plane2.SignedDistance;
			if (signedDistance > 0.0 && signedDistance2 > 0.0)
			{
				this.SignedDistance = Math.Min(signedDistance, signedDistance2);
				return;
			}
			if (signedDistance < 0.0 && signedDistance2 < 0.0)
			{
				this.SignedDistance = Math.Max(signedDistance, signedDistance2);
				return;
			}
			this.SignedDistance = 0.0;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003101 File Offset: 0x00001301
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003109 File Offset: 0x00001309
		public double SignedDistance { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003112 File Offset: 0x00001312
		public double Distance
		{
			get
			{
				return Math.Abs(this.SignedDistance);
			}
		}
	}
}
