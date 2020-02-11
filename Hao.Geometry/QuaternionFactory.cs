using System;

namespace Hao.Geometry
{
	// Token: 0x02000083 RID: 131
	public static class QuaternionFactory
	{
		// Token: 0x060004AC RID: 1196 RVA: 0x00017B97 File Offset: 0x00015D97
		public static Quaternion CreateFromTransform(AffineTransform3 transform)
		{
			return QuaternionFactory.CreateFromRotationMatrix(new Matrix3((Vector3)transform.AxisX, (Vector3)transform.AxisY, (Vector3)transform.AxisZ));
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00017BC8 File Offset: 0x00015DC8
		public static Quaternion CreateFromAxisAngle(UnitVector3 axis, double angle)
		{
			Vector3 vector = Math.Sin(angle / 2.0) * axis;
			double x = vector.X;
			double y = vector.Y;
			double z = vector.Z;
			return new Quaternion(Math.Cos(angle / 2.0), x, y, z);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00017C1C File Offset: 0x00015E1C
		public static Quaternion CreateAlign(Vector3 vector1, Vector3 vector2)
		{
			double num = 0.0;
			UnitVector3 vector3;
			if ((vector1 + vector2).TryGetNormalized(out vector3))
			{
				num = vector1.Dot(vector3);
			}
			double w = num;
			double x;
			double y;
			double z;
			if (num != 0.0)
			{
				Vector3 vector4 = vector1.Cross(vector3);
				x = vector4.X;
				y = vector4.Y;
				z = vector4.Z;
			}
			else if (Math.Abs(vector1.X) >= Math.Abs(vector1.Y))
			{
				double num2 = 1.0 / Math.Sqrt(vector1.X * vector1.X + vector1.Z * vector1.Z);
				x = -vector1.Z * num2;
				y = 0.0;
				z = vector1.X * num2;
			}
			else
			{
				double num2 = 1.0 / Math.Sqrt(vector1.Y * vector1.Y + vector1.Z * vector1.Z);
				x = 0.0;
				y = vector1.Z * num2;
				z = -vector1.Y * num2;
			}
			return new Quaternion(w, x, y, z);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00017D58 File Offset: 0x00015F58
		internal static Quaternion CreateFromRotationMatrix(Matrix3 matrix)
		{
			double num = matrix[0, 0] + matrix[1, 1] + matrix[2, 2];
			int[] array = new int[3];
			array[0] = 1;
			array[1] = 2;
			int[] array2 = array;
			double w;
			double x;
			double y;
			double z;
			if (num > 0.0)
			{
				double num2 = Math.Sqrt(num + 1.0);
				w = 0.5 * num2;
				num2 = 0.5 / num2;
				x = (matrix[2, 1] - matrix[1, 2]) * num2;
				y = (matrix[0, 2] - matrix[2, 0]) * num2;
				z = (matrix[1, 0] - matrix[0, 1]) * num2;
			}
			else
			{
				int num3 = 0;
				if (matrix[1, 1] > matrix[0, 0])
				{
					num3 = 1;
				}
				double num4 = matrix[2, 2];
				int num5 = num3;
				if (num4 > matrix[num5, num5])
				{
					num3 = 2;
				}
				int num6 = array2[num3];
				int num7 = array2[num6];
				int num8 = num3;
				double num9 = matrix[num8, num8];
				int num10 = num6;
				double num11 = num9 - matrix[num10, num10];
				int num12 = num7;
				double num2 = Math.Sqrt(num11 - matrix[num12, num12] + 1.0);
				double num13 = 0.5 / num2;
				w = (matrix[num7, num6] - matrix[num6, num7]) * num13;
				double num14 = 0.5 * num2;
				double num15 = (matrix[num6, num3] + matrix[num3, num6]) * num13;
				double num16 = (matrix[num7, num3] + matrix[num3, num7]) * num13;
				switch (num3)
				{
				case 0:
					x = num14;
					y = num15;
					z = num16;
					break;
				case 1:
					y = num14;
					z = num15;
					x = num16;
					break;
				case 2:
					z = num14;
					x = num15;
					y = num16;
					break;
				default:
					y = (x = (z = 0.0));
					break;
				}
			}
			return new Quaternion(w, x, y, z);
		}
	}
}
