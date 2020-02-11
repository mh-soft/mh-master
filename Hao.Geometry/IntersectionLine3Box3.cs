using System;

namespace Hao.Geometry
{
	// Token: 0x02000057 RID: 87
	internal struct IntersectionLine3Box3
	{
		// Token: 0x06000370 RID: 880 RVA: 0x0000ED46 File Offset: 0x0000CF46
		public IntersectionLine3Box3(Line3 line, Box3 box)
		{
			this = default(IntersectionLine3Box3);
			this.line = line;
			this.box = box;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000ED60 File Offset: 0x0000CF60
		public bool Test()
		{
			Vector3 vector = this.line.Origin - this.box.Center;
			Vector3 vector2 = this.line.Direction.Cross(vector);
			double num = Math.Abs(this.line.Direction.Dot(this.box.Axis1));
			double num2 = Math.Abs(this.line.Direction.Dot(this.box.Axis2));
			double num3 = Math.Abs(vector2.Dot(this.box.Axis0));
			double num4 = this.box.Extent1 * num2 + this.box.Extent2 * num;
			if (num3 > num4 + 1E-08)
			{
				return false;
			}
			double num5 = Math.Abs(this.line.Direction.Dot(this.box.Axis0));
			double num6 = Math.Abs(vector2.Dot(this.box.Axis1));
			double num7 = this.box.Extent0 * num2 + this.box.Extent2 * num5;
			if (num6 > num7 + 1E-08)
			{
				return false;
			}
			double num8 = Math.Abs(vector2.Dot(this.box.Axis2));
			double num9 = this.box.Extent0 * num + this.box.Extent1 * num5;
			return num8 <= num9 + 1E-08;
		}

		// Token: 0x040000EA RID: 234
		private readonly Line3 line;

		// Token: 0x040000EB RID: 235
		private readonly Box3 box;
	}
}
