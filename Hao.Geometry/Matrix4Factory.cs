using System;

namespace Hao.Geometry
{
	// Token: 0x02000080 RID: 128
	public static class Matrix4Factory
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x00017068 File Offset: 0x00015268
		public static Matrix4 CreateFromAffineTransform3(AffineTransform3 transform)
		{
			Vector4 column = new Vector4(transform.AxisX.X, transform.AxisX.Y, transform.AxisX.Z, 0.0) * transform.Scale;
			Vector4 column2 = new Vector4(transform.AxisY.X, transform.AxisY.Y, transform.AxisY.Z, 0.0) * transform.Scale;
			Vector4 column3 = new Vector4(transform.AxisZ.X, transform.AxisZ.Y, transform.AxisZ.Z, 0.0) * transform.Scale;
			Vector4 column4 = new Vector4(transform.Origin.X, transform.Origin.Y, transform.Origin.Z, 1.0);
			return new Matrix4(column, column2, column3, column4);
		}
	}
}
