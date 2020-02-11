using System;

namespace Hao.Geometry
{
	// Token: 0x02000052 RID: 82
	internal struct IntersectionBox3Box3
	{
		// Token: 0x06000338 RID: 824 RVA: 0x0000D199 File Offset: 0x0000B399
		public IntersectionBox3Box3(Box3 box0, Box3 box1)
		{
			this.box0 = box0;
			this.box1 = box1;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000D1AC File Offset: 0x0000B3AC
		public bool Test()
		{
			bool flag = false;
			Vector3 vector = this.box1.Center - this.box0.Center;
			double[,] array = new double[3, 3];
			double[,] array2 = new double[3, 3];
			double[] array3 = new double[3];
			array[0, 0] = this.box0.Axis0.Dot(this.box1.Axis0);
			array2[0, 0] = Math.Abs(array[0, 0]);
			if (array2[0, 0] > 0.99999999)
			{
				flag = true;
			}
			array[0, 1] = this.box0.Axis0.Dot(this.box1.Axis1);
			array2[0, 1] = Math.Abs(array[0, 1]);
			if (array2[0, 1] > 0.99999999)
			{
				flag = true;
			}
			array[0, 2] = this.box0.Axis0.Dot(this.box1.Axis2);
			array2[0, 2] = Math.Abs(array[0, 2]);
			if (array2[0, 2] > 0.99999999)
			{
				flag = true;
			}
			array3[0] = this.box0.Axis0.Dot(vector);
			double num = Math.Abs(array3[0]);
			double num2 = this.box1.Extent0 * array2[0, 0] + this.box1.Extent1 * array2[0, 1] + this.box1.Extent2 * array2[0, 2];
			double num3 = this.box0.Extent0 + num2;
			if (num > num3)
			{
				return false;
			}
			array[1, 0] = this.box0.Axis1.Dot(this.box1.Axis0);
			array2[1, 0] = Math.Abs(array[1, 0]);
			if (array2[1, 0] > 0.99999999)
			{
				flag = true;
			}
			array[1, 1] = this.box0.Axis1.Dot(this.box1.Axis1);
			array2[1, 1] = Math.Abs(array[1, 1]);
			if (array2[1, 1] > 0.99999999)
			{
				flag = true;
			}
			array[1, 2] = this.box0.Axis1.Dot(this.box1.Axis2);
			array2[1, 2] = Math.Abs(array[1, 2]);
			if (array2[1, 2] > 0.99999999)
			{
				flag = true;
			}
			array3[1] = this.box0.Axis1.Dot(vector);
			double num4 = Math.Abs(array3[1]);
			num2 = this.box1.Extent0 * array2[1, 0] + this.box1.Extent1 * array2[1, 1] + this.box1.Extent2 * array2[1, 2];
			num3 = this.box0.Extent1 + num2;
			if (num4 > num3)
			{
				return false;
			}
			array[2, 0] = this.box0.Axis2.Dot(this.box1.Axis0);
			array2[2, 0] = Math.Abs(array[2, 0]);
			if (array2[2, 0] > 0.99999999)
			{
				flag = true;
			}
			array[2, 1] = this.box0.Axis2.Dot(this.box1.Axis1);
			array2[2, 1] = Math.Abs(array[2, 1]);
			if (array2[2, 1] > 0.99999999)
			{
				flag = true;
			}
			array[2, 2] = this.box0.Axis2.Dot(this.box1.Axis2);
			array2[2, 2] = Math.Abs(array[2, 2]);
			if (array2[2, 2] > 0.99999999)
			{
				flag = true;
			}
			array3[2] = this.box0.Axis2.Dot(vector);
			double num5 = Math.Abs(array3[2]);
			num2 = this.box1.Extent0 * array2[2, 0] + this.box1.Extent1 * array2[2, 1] + this.box1.Extent2 * array2[2, 2];
			num3 = this.box0.Extent2 + num2;
			if (num5 > num3)
			{
				return false;
			}
			double num6 = Math.Abs(this.box1.Axis0.Dot(vector));
			num3 = this.box0.Extent0 * array2[0, 0] + this.box0.Extent1 * array2[1, 0] + this.box0.Extent2 * array2[2, 0] + this.box1.Extent0;
			if (num6 > num3)
			{
				return false;
			}
			double num7 = Math.Abs(this.box1.Axis1.Dot(vector));
			num3 = this.box0.Extent0 * array2[0, 1] + this.box0.Extent1 * array2[1, 1] + this.box0.Extent2 * array2[2, 1] + this.box1.Extent1;
			if (num7 > num3)
			{
				return false;
			}
			double num8 = Math.Abs(this.box1.Axis2.Dot(vector));
			num3 = this.box0.Extent0 * array2[0, 2] + this.box0.Extent1 * array2[1, 2] + this.box0.Extent2 * array2[2, 2] + this.box1.Extent2;
			if (num8 > num3)
			{
				return false;
			}
			if (flag)
			{
				return true;
			}
			double num9 = Math.Abs(array3[2] * array[1, 0] - array3[1] * array[2, 0]);
			double num10 = this.box0.Extent1 * array2[2, 0] + this.box0.Extent2 * array2[1, 0];
			num2 = this.box1.Extent1 * array2[0, 2] + this.box1.Extent2 * array2[0, 1];
			num3 = num10 + num2;
			if (num9 > num3)
			{
				return false;
			}
			double num11 = Math.Abs(array3[2] * array[1, 1] - array3[1] * array[2, 1]);
			double num12 = this.box0.Extent1 * array2[2, 1] + this.box0.Extent2 * array2[1, 1];
			num2 = this.box1.Extent0 * array2[0, 2] + this.box1.Extent2 * array2[0, 0];
			num3 = num12 + num2;
			if (num11 > num3)
			{
				return false;
			}
			double num13 = Math.Abs(array3[2] * array[1, 2] - array3[1] * array[2, 2]);
			double num14 = this.box0.Extent1 * array2[2, 2] + this.box0.Extent2 * array2[1, 2];
			num2 = this.box1.Extent0 * array2[0, 1] + this.box1.Extent1 * array2[0, 0];
			num3 = num14 + num2;
			if (num13 > num3)
			{
				return false;
			}
			double num15 = Math.Abs(array3[0] * array[2, 0] - array3[2] * array[0, 0]);
			double num16 = this.box0.Extent0 * array2[2, 0] + this.box0.Extent2 * array2[0, 0];
			num2 = this.box1.Extent1 * array2[1, 2] + this.box1.Extent2 * array2[1, 1];
			num3 = num16 + num2;
			if (num15 > num3)
			{
				return false;
			}
			double num17 = Math.Abs(array3[0] * array[2, 1] - array3[2] * array[0, 1]);
			double num18 = this.box0.Extent0 * array2[2, 1] + this.box0.Extent2 * array2[0, 1];
			num2 = this.box1.Extent0 * array2[1, 2] + this.box1.Extent2 * array2[1, 0];
			num3 = num18 + num2;
			if (num17 > num3)
			{
				return false;
			}
			double num19 = Math.Abs(array3[0] * array[2, 2] - array3[2] * array[0, 2]);
			double num20 = this.box0.Extent0 * array2[2, 2] + this.box0.Extent2 * array2[0, 2];
			num2 = this.box1.Extent0 * array2[1, 1] + this.box1.Extent1 * array2[1, 0];
			num3 = num20 + num2;
			if (num19 > num3)
			{
				return false;
			}
			double num21 = Math.Abs(array3[1] * array[0, 0] - array3[0] * array[1, 0]);
			double num22 = this.box0.Extent0 * array2[1, 0] + this.box0.Extent1 * array2[0, 0];
			num2 = this.box1.Extent1 * array2[2, 2] + this.box1.Extent2 * array2[2, 1];
			num3 = num22 + num2;
			if (num21 > num3)
			{
				return false;
			}
			double num23 = Math.Abs(array3[1] * array[0, 1] - array3[0] * array[1, 1]);
			double num24 = this.box0.Extent0 * array2[1, 1] + this.box0.Extent1 * array2[0, 1];
			num2 = this.box1.Extent0 * array2[2, 2] + this.box1.Extent2 * array2[2, 0];
			num3 = num24 + num2;
			if (num23 > num3)
			{
				return false;
			}
			double num25 = Math.Abs(array3[1] * array[0, 2] - array3[0] * array[1, 2]);
			double num26 = this.box0.Extent0 * array2[1, 2] + this.box0.Extent1 * array2[0, 2];
			num2 = this.box1.Extent0 * array2[2, 1] + this.box1.Extent1 * array2[2, 0];
			num3 = num26 + num2;
			return num25 <= num3;
		}

		// Token: 0x040000DC RID: 220
		private readonly Box3 box0;

		// Token: 0x040000DD RID: 221
		private readonly Box3 box1;
	}
}
