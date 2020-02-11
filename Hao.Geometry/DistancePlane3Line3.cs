using System;

namespace Hao.Geometry
{
	// Token: 0x0200000C RID: 12
	internal struct DistancePlane3Line3
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002F40 File Offset: 0x00001140
		public DistancePlane3Line3(Plane3 plane, Line3 line)
		{
			this = default(DistancePlane3Line3);
			if (Math.Abs(line.Direction.Dot(plane.Normal)) < 1E-08)
			{
				this.SignedDistance = line.Origin.SignedDistanceTo(plane);
				return;
			}
			this.SignedDistance = 0.0;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002F9D File Offset: 0x0000119D
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002FA5 File Offset: 0x000011A5
		public double SignedDistance { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002FAE File Offset: 0x000011AE
		public double Distance
		{
			get
			{
				return Math.Abs(this.SignedDistance);
			}
		}
	}
}
