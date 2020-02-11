using System;

namespace Hao.Geometry
{
	// Token: 0x0200005C RID: 92
	internal struct IntersectionPlane3Box3
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x0000FD64 File Offset: 0x0000DF64
		public IntersectionPlane3Box3(Plane3 plane, Box3 box)
		{
			this.plane = plane;
			this.box = box;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000FD74 File Offset: 0x0000DF74
		public bool Test()
		{
			double value = this.box.Extent0 * this.plane.Normal.Dot(this.box.Axis0);
			double value2 = this.box.Extent1 * this.plane.Normal.Dot(this.box.Axis1);
			double value3 = this.box.Extent2 * this.plane.Normal.Dot(this.box.Axis2);
			double num = Math.Abs(value) + Math.Abs(value2) + Math.Abs(value3);
			return Math.Abs(this.plane.Normal.Dot(this.box.Center) - this.plane.Constant) <= num;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000FE78 File Offset: 0x0000E078
		public PlaneSideClassification SideClassification()
		{
			double value = this.box.Extent0 * this.plane.Normal.Dot(this.box.Axis0);
			double value2 = this.box.Extent1 * this.plane.Normal.Dot(this.box.Axis1);
			double value3 = this.box.Extent2 * this.plane.Normal.Dot(this.box.Axis2);
			double num = Math.Abs(value) + Math.Abs(value2) + Math.Abs(value3);
			double num2 = this.plane.Normal.Dot(this.box.Center) - this.plane.Constant;
			if (num2 < -num)
			{
				return PlaneSideClassification.Back;
			}
			if (num2 > num)
			{
				return PlaneSideClassification.Front;
			}
			return PlaneSideClassification.Intersecting;
		}

		// Token: 0x040000FC RID: 252
		private readonly Plane3 plane;

		// Token: 0x040000FD RID: 253
		private readonly Box3 box;
	}
}
