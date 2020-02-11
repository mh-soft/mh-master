using System;

namespace Hao.Geometry
{
	// Token: 0x02000077 RID: 119
	public static class BoxExtensionMethods
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x00015E88 File Offset: 0x00014088
		public static AxisAlignedBox3 ComputeAxisAlignedBoundingBox(this Box3 box)
		{
			Vector3 vector = box.Center;
			Vector3 vector2 = box.Center;
			foreach (Vector3 value in box.ComputeVertices())
			{
				vector = Vector3.Min(vector, value);
				vector2 = Vector3.Max(vector2, value);
			}
			return new AxisAlignedBox3(vector, vector2);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00015EF8 File Offset: 0x000140F8
		public static AxisAlignedBox2 ComputeAxisAlignedBoundingBox(this Box2 box)
		{
			Vector2 vector = box.Center;
			Vector2 vector2 = box.Center;
			foreach (Vector2 value in box.ComputeVertices())
			{
				vector = Vector2.Min(vector, value);
				vector2 = Vector2.Max(vector2, value);
			}
			return new AxisAlignedBox2(vector, vector2);
		}
	}
}
