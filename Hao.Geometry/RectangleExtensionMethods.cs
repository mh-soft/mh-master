using System;

namespace Hao.Geometry
{
	// Token: 0x0200007E RID: 126
	public static class RectangleExtensionMethods
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x00016CC0 File Offset: 0x00014EC0
		public static AxisAlignedBox3 ComputeAxisAlignedBoundingBox(this Rectangle3 rectangle)
		{
			Vector3 vector = rectangle.Center;
			Vector3 vector2 = vector;
			foreach (Vector3 value in rectangle.ComputeVertices())
			{
				vector = Vector3.Min(vector, value);
				vector2 = Vector3.Max(vector2, value);
			}
			return new AxisAlignedBox3(vector, vector2);
		}
	}
}
