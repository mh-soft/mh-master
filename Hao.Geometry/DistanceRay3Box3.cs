using System;

namespace Hao.Geometry
{
	// Token: 0x02000020 RID: 32
	internal struct DistanceRay3Box3
	{
		// Token: 0x06000135 RID: 309 RVA: 0x000066CC File Offset: 0x000048CC
		public DistanceRay3Box3(Ray3 ray, Box3 box)
		{
			this = default(DistanceRay3Box3);
			Line3 line = new Line3(ray.Origin, ray.Direction);
			DistanceLine3Box3 distanceLine3Box = new DistanceLine3Box3(line, box);
			if (distanceLine3Box.LineParameter >= 0.0)
			{
				this.SquaredDistance = distanceLine3Box.SquaredDistance;
				this.ClosestPointOnRay = distanceLine3Box.ClosestPointOnLine;
				this.ClosestPointOnBox = distanceLine3Box.ClosestPointOnBox;
				return;
			}
			DistanceVector3Box3 distanceVector3Box = new DistanceVector3Box3(ray.Origin, box);
			this.SquaredDistance = distanceVector3Box.SquaredDistance;
			this.ClosestPointOnRay = distanceVector3Box.ClosestPointOnVector;
			this.ClosestPointOnBox = distanceVector3Box.ClosestPointOnBox;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000676E File Offset: 0x0000496E
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00006776 File Offset: 0x00004976
		public double SquaredDistance { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000677F File Offset: 0x0000497F
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000678C File Offset: 0x0000498C
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00006794 File Offset: 0x00004994
		public Vector3 ClosestPointOnRay { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000679D File Offset: 0x0000499D
		// (set) Token: 0x0600013C RID: 316 RVA: 0x000067A5 File Offset: 0x000049A5
		public Vector3 ClosestPointOnBox { get; private set; }
	}
}
