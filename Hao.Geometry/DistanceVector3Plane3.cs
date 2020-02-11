using System;

namespace Hao.Geometry
{
	// Token: 0x02000032 RID: 50
	internal struct DistanceVector3Plane3
	{
		// Token: 0x06000226 RID: 550 RVA: 0x000096F4 File Offset: 0x000078F4
		public DistanceVector3Plane3(Vector3 vector, Plane3 plane)
		{
			this = default(DistanceVector3Plane3);
			double num = plane.Normal.Dot(vector) - plane.Constant;
			this.ClosestPointOnVector = vector;
			this.ClosestPointOnPlane = vector - num * plane.Normal;
			this.SignedDistance = num;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00009748 File Offset: 0x00007948
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00009750 File Offset: 0x00007950
		public double SignedDistance { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00009759 File Offset: 0x00007959
		public double Distance
		{
			get
			{
				return Math.Abs(this.SignedDistance);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00009766 File Offset: 0x00007966
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000976E File Offset: 0x0000796E
		public Vector3 ClosestPointOnVector { get; private set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00009777 File Offset: 0x00007977
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000977F File Offset: 0x0000797F
		public Vector3 ClosestPointOnPlane { get; private set; }
	}
}
