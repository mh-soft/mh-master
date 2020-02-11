using System;

namespace Hao.Geometry
{
	// Token: 0x02000063 RID: 99
	internal struct IntersectionRay3Plane3
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x000115F4 File Offset: 0x0000F7F4
		public IntersectionRay3Plane3(Ray3 ray, Plane3 plane)
		{
			this = default(IntersectionRay3Plane3);
			this.ray = ray;
			this.plane = plane;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0001160B File Offset: 0x0000F80B
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x00011613 File Offset: 0x0000F813
		public double RayParameter { get; private set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0001161C File Offset: 0x0000F81C
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x00011624 File Offset: 0x0000F824
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x060003DA RID: 986 RVA: 0x0001162D File Offset: 0x0000F82D
		public bool Test()
		{
			return this.Find();
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00011638 File Offset: 0x0000F838
		public bool Find()
		{
			Line3 line = new Line3(this.ray.Origin, this.ray.Direction);
			IntersectionLine3Plane3 intersectionLine3Plane = new IntersectionLine3Plane3(line, this.plane);
			if (intersectionLine3Plane.Find())
			{
				this.IntersectionType = intersectionLine3Plane.IntersectionType;
				this.RayParameter = intersectionLine3Plane.LineParameter;
				return this.RayParameter >= 0.0;
			}
			this.IntersectionType = Intersection.Type.IT_EMPTY;
			return false;
		}

		// Token: 0x04000112 RID: 274
		private readonly Ray3 ray;

		// Token: 0x04000113 RID: 275
		private readonly Plane3 plane;
	}
}
