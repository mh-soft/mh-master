using System;

namespace Hao.Geometry
{
	// Token: 0x02000082 RID: 130
	public static class QuaternionExtensionMethods
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x00017A74 File Offset: 0x00015C74
		public static void ToAxisAngle(this Quaternion quaternion, out Vector3 axis, out double angle)
		{
			double num = quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z;
			axis = default(Vector3);
			if (num > 1E-08)
			{
				angle = 2.0 * Math.Acos(quaternion.W);
				double num2 = 1.0 / Math.Sqrt(num);
				axis.X = quaternion.X * num2;
				axis.Y = quaternion.Y * num2;
				axis.Z = quaternion.Z * num2;
				return;
			}
			angle = 0.0;
			axis.X = 1.0;
			axis.Y = 0.0;
			axis.Z = 0.0;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00017B54 File Offset: 0x00015D54
		public static Vector3 Rotate(this Quaternion quaternion, Vector3 vector)
		{
			AffineTransform3 affineTransform = Transform3Factory.CreateFromQuaternion(quaternion);
			return new Matrix3((Vector3)affineTransform.AxisX, (Vector3)affineTransform.AxisY, (Vector3)affineTransform.AxisZ) * vector;
		}
	}
}
