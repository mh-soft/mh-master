using System;

namespace Hao.Geometry
{
	// Token: 0x02000061 RID: 97
	internal struct IntersectionRay3Box3
	{
		// Token: 0x060003CB RID: 971 RVA: 0x0001117F File Offset: 0x0000F37F
		public IntersectionRay3Box3(Ray3 ray, Box3 box)
		{
			this = default(IntersectionRay3Box3);
			this.ray = ray;
			this.box = box;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00011198 File Offset: 0x0000F398
		public bool Test()
		{
			Vector3 vector = this.ray.Origin - this.box.Center;
			double num = this.ray.Direction.Dot(this.box.Axis0);
			double num2 = Math.Abs(num);
			double num3 = vector.Dot(this.box.Axis0);
			if (Math.Abs(num3) > this.box.Extent0 && num3 * num >= 0.0)
			{
				return false;
			}
			double num4 = this.ray.Direction.Dot(this.box.Axis1);
			double num5 = Math.Abs(num4);
			double num6 = vector.Dot(this.box.Axis1);
			if (Math.Abs(num6) > this.box.Extent1 && num6 * num4 >= 0.0)
			{
				return false;
			}
			double num7 = this.ray.Direction.Dot(this.box.Axis2);
			double num8 = Math.Abs(num7);
			double num9 = vector.Dot(this.box.Axis2);
			if (Math.Abs(num9) > this.box.Extent2 && num9 * num7 >= 0.0)
			{
				return false;
			}
			Vector3 vector2 = this.ray.Direction.Cross(vector);
			double num10 = Math.Abs(vector2.Dot(this.box.Axis0));
			double num11 = this.box.Extent1 * num8 + this.box.Extent2 * num5;
			if (num10 > num11 + 1E-08)
			{
				return false;
			}
			double num12 = Math.Abs(vector2.Dot(this.box.Axis1));
			double num13 = this.box.Extent0 * num8 + this.box.Extent2 * num2;
			if (num12 > num13 + 1E-08)
			{
				return false;
			}
			double num14 = Math.Abs(vector2.Dot(this.box.Axis2));
			double num15 = this.box.Extent0 * num5 + this.box.Extent1 * num2;
			return num14 <= num15 + 1E-08;
		}

		// Token: 0x0400010B RID: 267
		private readonly Ray3 ray;

		// Token: 0x0400010C RID: 268
		private readonly Box3 box;
	}
}
