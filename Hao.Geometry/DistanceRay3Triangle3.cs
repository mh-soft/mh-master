using System;

namespace Hao.Geometry
{
	// Token: 0x02000024 RID: 36
	internal struct DistanceRay3Triangle3
	{
		// Token: 0x0600015F RID: 351 RVA: 0x000070DC File Offset: 0x000052DC
		public DistanceRay3Triangle3(Ray3 ray, Triangle3 triangle)
		{
			this = default(DistanceRay3Triangle3);
			Line3 line = new Line3(ray.Origin, ray.Direction);
			DistanceLine3Triangle3 distanceLine3Triangle = new DistanceLine3Triangle3(line, triangle);
			if (distanceLine3Triangle.LineParameter >= 0.0)
			{
				this.SquaredDistance = distanceLine3Triangle.SquaredDistance;
				this.ClosestPointOnRay = distanceLine3Triangle.ClosestPointOnLine;
				this.ClosestPointOnTriangle = distanceLine3Triangle.ClosestPointOnTriangle;
				this.RayParameter = distanceLine3Triangle.LineParameter;
				this.TriangleBary0 = distanceLine3Triangle.TriangleBary0;
				this.TriangleBary1 = distanceLine3Triangle.TriangleBary1;
				this.TriangleBary2 = distanceLine3Triangle.TriangleBary2;
				return;
			}
			DistanceVector3Triangle3 distanceVector3Triangle = new DistanceVector3Triangle3(ray.Origin, triangle);
			this.SquaredDistance = distanceVector3Triangle.SquaredDistance;
			this.ClosestPointOnRay = ray.Origin;
			this.ClosestPointOnTriangle = distanceVector3Triangle.ClosestPointOnTriangle;
			this.RayParameter = 0.0;
			this.TriangleBary0 = distanceVector3Triangle.TriangleBary0;
			this.TriangleBary1 = distanceVector3Triangle.TriangleBary1;
			this.TriangleBary2 = distanceVector3Triangle.TriangleBary2;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000071E8 File Offset: 0x000053E8
		// (set) Token: 0x06000161 RID: 353 RVA: 0x000071F0 File Offset: 0x000053F0
		public double SquaredDistance { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000071F9 File Offset: 0x000053F9
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00007206 File Offset: 0x00005406
		// (set) Token: 0x06000164 RID: 356 RVA: 0x0000720E File Offset: 0x0000540E
		public double RayParameter { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00007217 File Offset: 0x00005417
		// (set) Token: 0x06000166 RID: 358 RVA: 0x0000721F File Offset: 0x0000541F
		public double TriangleBary0 { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00007228 File Offset: 0x00005428
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00007230 File Offset: 0x00005430
		public double TriangleBary1 { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00007239 File Offset: 0x00005439
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00007241 File Offset: 0x00005441
		public double TriangleBary2 { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000724A File Offset: 0x0000544A
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00007252 File Offset: 0x00005452
		public Vector3 ClosestPointOnRay { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000725B File Offset: 0x0000545B
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00007263 File Offset: 0x00005463
		public Vector3 ClosestPointOnTriangle { get; private set; }
	}
}
