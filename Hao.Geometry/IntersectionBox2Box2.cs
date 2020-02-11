using System;

namespace Hao.Geometry
{
	// Token: 0x02000050 RID: 80
	internal struct IntersectionBox2Box2
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0000CF1F File Offset: 0x0000B11F
		public IntersectionBox2Box2(Box2 box0, Box2 box1)
		{
			this.box0 = box0;
			this.box1 = box1;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000CF30 File Offset: 0x0000B130
		public bool Test()
		{
			UnitVector2 axis = this.box0.Axis0;
			UnitVector2 axis2 = this.box0.Axis1;
			UnitVector2 axis3 = this.box1.Axis0;
			UnitVector2 axis4 = this.box1.Axis1;
			double extent = this.box0.Extent0;
			double extent2 = this.box0.Extent1;
			double extent3 = this.box1.Extent0;
			double extent4 = this.box1.Extent1;
			Vector2 vector = this.box1.Center - this.box0.Center;
			double[,] array = new double[2, 2];
			array[0, 0] = Math.Abs(axis.Dot(axis3));
			array[0, 1] = Math.Abs(axis.Dot(axis4));
			double num = Math.Abs(axis.Dot(vector));
			double num2 = extent + extent3 * array[0, 0] + extent4 * array[0, 1];
			if (num > num2)
			{
				return false;
			}
			array[1, 0] = Math.Abs(axis2.Dot(axis3));
			array[1, 1] = Math.Abs(axis2.Dot(axis4));
			double num3 = Math.Abs(axis2.Dot(vector));
			num2 = extent2 + extent3 * array[1, 0] + extent4 * array[1, 1];
			if (num3 > num2)
			{
				return false;
			}
			double num4 = Math.Abs(axis3.Dot(vector));
			num2 = extent3 + extent * array[0, 0] + extent2 * array[1, 0];
			if (num4 > num2)
			{
				return false;
			}
			double num5 = Math.Abs(axis4.Dot(vector));
			num2 = extent4 + extent * array[0, 1] + extent2 * array[1, 1];
			return num5 <= num2;
		}

		// Token: 0x040000DA RID: 218
		private readonly Box2 box0;

		// Token: 0x040000DB RID: 219
		private readonly Box2 box1;
	}
}
