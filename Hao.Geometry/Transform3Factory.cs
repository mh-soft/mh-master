using System;

namespace Hao.Geometry
{
	// Token: 0x02000085 RID: 133
	public static class Transform3Factory
	{
		// Token: 0x060004CE RID: 1230 RVA: 0x00018B8C File Offset: 0x00016D8C
		public static AffineTransform3 Combine(AffineTransform3 first, AffineTransform3 second)
		{
			UnitVector3 axisX = second.AxisX;
			UnitVector3 axisY = second.AxisY;
			UnitVector3 axisZ = second.AxisZ;
			double scale = second.Scale;
			UnitVector3 axisX2 = first.AxisX;
			UnitVector3 axisY2 = first.AxisY;
			UnitVector3 axisZ2 = first.AxisZ;
			Vector3 origin = second.Origin;
			Vector3 origin2 = first.Origin;
			double x = axisX.X * axisX2.X + axisY.X * axisX2.Y + axisZ.X * axisX2.Z;
			double x2 = axisX.X * axisY2.X + axisY.X * axisY2.Y + axisZ.X * axisY2.Z;
			double x3 = axisX.X * axisZ2.X + axisY.X * axisZ2.Y + axisZ.X * axisZ2.Z;
			double y = axisX.Y * axisX2.X + axisY.Y * axisX2.Y + axisZ.Y * axisX2.Z;
			double y2 = axisX.Y * axisY2.X + axisY.Y * axisY2.Y + axisZ.Y * axisY2.Z;
			double y3 = axisX.Y * axisZ2.X + axisY.Y * axisZ2.Y + axisZ.Y * axisZ2.Z;
			double z = axisX.Z * axisX2.X + axisY.Z * axisX2.Y + axisZ.Z * axisX2.Z;
			double z2 = axisX.Z * axisY2.X + axisY.Z * axisY2.Y + axisZ.Z * axisY2.Z;
			double z3 = axisX.Z * axisZ2.X + axisY.Z * axisZ2.Y + axisZ.Z * axisZ2.Z;
			double x4 = scale * (axisX.X * origin2.X + axisY.X * origin2.Y + axisZ.X * origin2.Z) + origin.X;
			double y4 = scale * (axisX.Y * origin2.X + axisY.Y * origin2.Y + axisZ.Y * origin2.Z) + origin.Y;
			double z4 = scale * (axisX.Z * origin2.X + axisY.Z * origin2.Y + axisZ.Z * origin2.Z) + origin.Z;
			UnitVector3 axisX3 = new UnitVector3(x, y, z);
			UnitVector3 axisY3 = new UnitVector3(x2, y2, z2);
			UnitVector3 axisZ3 = new UnitVector3(x3, y3, z3);
			Vector3 origin3 = new Vector3(x4, y4, z4);
			return new AffineTransform3(axisX3, axisY3, axisZ3, origin3, second.Scale * first.Scale);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00018E99 File Offset: 0x00017099
		public static AffineTransform3 Combine(AffineTransform3 first, AffineTransform3 second, AffineTransform3 third)
		{
			return Transform3Factory.Combine(Transform3Factory.Combine(first, second), third);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00018EA8 File Offset: 0x000170A8
		public static AffineTransform3 CreateTranslate(Vector3 translate)
		{
			return new AffineTransform3(UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, translate);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00018EBF File Offset: 0x000170BF
		[Obsolete("replace parameter double angle (as radians) with Angle, f.ex. Angle.FromRadians(angle)")]
		public static AffineTransform3 CreateFromAxisAngle(UnitVector3 axis, double angle, Vector3 origin, double scale)
		{
			return Transform3Factory.CreateFromAxisAngle(axis, Angle.FromRadians(angle), origin, scale);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00018ED0 File Offset: 0x000170D0
		public static AffineTransform3 CreateFromAxisAngle(UnitVector3 axis, Angle angle, Vector3 origin, double scale)
		{
			double cos = angle.Cos;
			double sin = angle.Sin;
			double num = 1.0 - cos;
			double num2 = axis.X * axis.X;
			double num3 = axis.Y * axis.Y;
			double num4 = axis.Z * axis.Z;
			double num5 = axis.X * axis.Y * num;
			double num6 = axis.X * axis.Z * num;
			double num7 = axis.Y * axis.Z * num;
			double num8 = axis.X * sin;
			double num9 = axis.Y * sin;
			double num10 = axis.Z * sin;
			UnitVector3 axisX = new UnitVector3(num2 * num + cos, num5 + num10, num6 - num9);
			UnitVector3 axisY = new UnitVector3(num5 - num10, num3 * num + cos, num7 + num8);
			UnitVector3 axisZ = new UnitVector3(num6 + num9, num7 - num8, num4 * num + cos);
			double x = axisX.X * origin.X + axisY.X * origin.Y + axisZ.X * origin.Z;
			double y = axisX.Y * origin.X + axisY.Y * origin.Y + axisZ.Y * origin.Z;
			double z = axisX.Z * origin.X + axisY.Z * origin.Y + axisZ.Z * origin.Z;
			Vector3 right = new Vector3(x, y, z);
			return new AffineTransform3(axisX, axisY, axisZ, origin - right);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001907C File Offset: 0x0001727C
		[Obsolete("replace parameter double angle (as radians) with Angle, f.ex. Angle.FromRadians(angle)")]
		public static AffineTransform3 CreateFromAxisAngle(UnitVector3 axis, double angle, Vector3 origin)
		{
			return Transform3Factory.CreateFromAxisAngle(axis, Angle.FromRadians(angle), origin, 1.0);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00019094 File Offset: 0x00017294
		public static AffineTransform3 CreateFromAxisAngle(UnitVector3 axis, Angle angle, Vector3 origin)
		{
			return Transform3Factory.CreateFromAxisAngle(axis, angle, origin, 1.0);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000190A7 File Offset: 0x000172A7
		public static AffineTransform3 CreateFromAxisAngle(UnitVector3 axis, Angle angle)
		{
			return Transform3Factory.CreateFromAxisAngle(axis, angle, Vector3.Zero);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000190B5 File Offset: 0x000172B5
		[Obsolete("replace parameter double angle (as radians) with Angle, f.ex. Angle.FromRadians(angle)")]
		public static AffineTransform3 CreateFromAxisAngle(UnitVector3 axis, double angle)
		{
			return Transform3Factory.CreateFromAxisAngle(axis, angle, Vector3.Zero);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000190C4 File Offset: 0x000172C4
		public static AffineTransform3 CreateFromQuaternion(Quaternion quaternion, Vector3 origin, double scale)
		{
			double num = 2.0 * quaternion.X;
			double num2 = 2.0 * quaternion.Y;
			double num3 = 2.0 * quaternion.Z;
			double num4 = num * quaternion.W;
			double num5 = num2 * quaternion.W;
			double num6 = num3 * quaternion.W;
			double num7 = num * quaternion.X;
			double num8 = num2 * quaternion.X;
			double num9 = num3 * quaternion.X;
			double num10 = num2 * quaternion.Y;
			double num11 = num3 * quaternion.Y;
			double num12 = num3 * quaternion.Z;
			UnitVector3 axisX = new UnitVector3(1.0 - (num10 + num12), num8 + num6, num9 - num5);
			UnitVector3 axisY = new UnitVector3(num8 - num6, 1.0 - (num7 + num12), num11 + num4);
			UnitVector3 axisZ = new UnitVector3(num9 + num5, num11 - num4, 1.0 - (num7 + num10));
			return new AffineTransform3(axisX, axisY, axisZ, origin, scale);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000191CB File Offset: 0x000173CB
		public static AffineTransform3 CreateFromQuaternion(Quaternion quaternion, Vector3 origin)
		{
			return Transform3Factory.CreateFromQuaternion(quaternion, origin, 1.0);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000191DD File Offset: 0x000173DD
		public static AffineTransform3 CreateFromQuaternion(Quaternion quaternion)
		{
			return Transform3Factory.CreateFromQuaternion(quaternion, Vector3.Zero);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000191EC File Offset: 0x000173EC
		public static AffineTransform3 CreateOrthonormalBasis(UnitVector3 axis2)
		{
			UnitVector3 axisX;
			UnitVector3 axisY;
			if (Math.Abs(axis2.X) >= Math.Abs(axis2.Y))
			{
				double num = 1.0 / Math.Sqrt(axis2.X * axis2.X + axis2.Z * axis2.Z);
				axisX = new UnitVector3(-axis2.Z * num, 0.0, axis2.X * num);
				axisY = new UnitVector3(axis2.Y * axisX.Z, axis2.Z * axisX.X - axis2.X * axisX.Z, -axis2.Y * axisX.X);
			}
			else
			{
				double num2 = 1.0 / Math.Sqrt(axis2.Y * axis2.Y + axis2.Z * axis2.Z);
				axisX = new UnitVector3(0.0, axis2.Z * num2, -axis2.Y * num2);
				axisY = new UnitVector3(axis2.Y * axisX.Z - axis2.Z * axisX.Y, -axis2.X * axisX.Z, axis2.X * axisX.Y);
			}
			return new AffineTransform3(axisX, axisY, axis2);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00019354 File Offset: 0x00017554
		public static bool TryCreateFromMatrix(Matrix4 matrix, out AffineTransform3 transform)
		{
			double length = matrix.Column0.Length;
			if (Math.Abs(length - matrix.Column1.Length) > 1E-05 || Math.Abs(length - matrix.Column2.Length) > 1E-05)
			{
				transform = AffineTransform3.Identity;
				return false;
			}
			Vector4 column = matrix.Column0;
			Vector4 column2 = matrix.Column1;
			Vector4 column3 = matrix.Column2;
			Vector4 column4 = matrix.Column3;
			UnitVector3 axisX = new UnitVector3(column.X / length, column.Y / length, column.Z / length);
			UnitVector3 unitVector = new UnitVector3(column2.X / length, column2.Y / length, column2.Z / length);
			UnitVector3 unitVector2 = new UnitVector3(column3.X / length, column3.Y / length, column3.Z / length);
			UnitVector3 unitVector3;
			if (!axisX.TryGetUnitCross(unitVector, out unitVector3))
			{
				transform = AffineTransform3.Identity;
				return false;
			}
			if (((Vector3)unitVector2 - (Vector3)unitVector3).Length > 1E-05)
			{
				transform = AffineTransform3.Identity;
				return false;
			}
			Vector3 origin = new Vector3(column4.X, column4.Y, column4.Z);
			transform = new AffineTransform3(axisX, unitVector, unitVector2, origin, length);
			return true;
		}
	}
}
