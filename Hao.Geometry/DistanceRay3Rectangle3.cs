using System;

namespace Hao.Geometry
{
	// Token: 0x02000022 RID: 34
	internal struct DistanceRay3Rectangle3
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00006AC4 File Offset: 0x00004CC4
		public DistanceRay3Rectangle3(Ray3 ray, Rectangle3 rectangle)
		{
			this = default(DistanceRay3Rectangle3);
			Line3 line = new Line3(ray.Origin, ray.Direction);
			DistanceLine3Rectangle3 distanceLine3Rectangle = new DistanceLine3Rectangle3(line, rectangle);
			if (distanceLine3Rectangle.LineParameter >= 0.0)
			{
				this.SquaredDistance = distanceLine3Rectangle.SquaredDistance;
				this.ClosestPointOnRay = distanceLine3Rectangle.ClosestPointOnLine;
				this.ClosestPointOnRectangle = distanceLine3Rectangle.ClosestPointOnRectangle;
				this.RayParameter = distanceLine3Rectangle.LineParameter;
				this.RectCoord0 = distanceLine3Rectangle.RectCoord0;
				this.RectCoord1 = distanceLine3Rectangle.RectCoord1;
				return;
			}
			DistanceVector3Rectangle3 distanceVector3Rectangle = new DistanceVector3Rectangle3(ray.Origin, rectangle);
			this.SquaredDistance = distanceVector3Rectangle.SquaredDistance;
			this.ClosestPointOnRay = ray.Origin;
			this.ClosestPointOnRectangle = distanceVector3Rectangle.ClosestPointOnRectangle;
			this.RayParameter = 0.0;
			this.RectCoord0 = distanceVector3Rectangle.RectCoord0;
			this.RectCoord1 = distanceVector3Rectangle.RectCoord1;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00006BB6 File Offset: 0x00004DB6
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00006BBE File Offset: 0x00004DBE
		public double SquaredDistance { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00006BC7 File Offset: 0x00004DC7
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00006BD4 File Offset: 0x00004DD4
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00006BDC File Offset: 0x00004DDC
		public double RayParameter { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00006BE5 File Offset: 0x00004DE5
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00006BED File Offset: 0x00004DED
		public double RectCoord0 { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00006BF6 File Offset: 0x00004DF6
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00006BFE File Offset: 0x00004DFE
		public double RectCoord1 { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00006C07 File Offset: 0x00004E07
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00006C0F File Offset: 0x00004E0F
		public Vector3 ClosestPointOnRay { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006C18 File Offset: 0x00004E18
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00006C20 File Offset: 0x00004E20
		public Vector3 ClosestPointOnRectangle { get; private set; }
	}
}
