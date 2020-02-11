using System;

namespace Hao.Geometry
{
	// Token: 0x02000060 RID: 96
	internal static class IntersectionRay3AxisAlignedBox3
	{
		// Token: 0x060003CA RID: 970 RVA: 0x00010F64 File Offset: 0x0000F164
		internal static bool Test(Ray3 ray, AxisAlignedBox3 axisAlignedBox)
		{
			Vector3 vector = ray.Origin - axisAlignedBox.Center;
			if (Math.Abs(vector.X) > axisAlignedBox.Extents.X && vector.X * ray.Direction.X >= 0.0)
			{
				return false;
			}
			if (Math.Abs(vector.Y) > axisAlignedBox.Extents.Y && vector.Y * ray.Direction.Y >= 0.0)
			{
				return false;
			}
			if (Math.Abs(vector.Z) > axisAlignedBox.Extents.Z && vector.Z * ray.Direction.Z >= 0.0)
			{
				return false;
			}
			Vector3 vector2 = ray.Direction.Cross(vector);
			Vector3 vector3 = new Vector3(Math.Abs(ray.Direction.X), Math.Abs(ray.Direction.Y), Math.Abs(ray.Direction.Z));
			return Math.Abs(vector2.X) <= axisAlignedBox.Extents.Y * vector3.Z + axisAlignedBox.Extents.Z * vector3.Y + 1E-08 && Math.Abs(vector2.Y) <= axisAlignedBox.Extents.Z * vector3.X + axisAlignedBox.Extents.X * vector3.Z + 1E-08 && Math.Abs(vector2.Z) <= axisAlignedBox.Extents.X * vector3.Y + axisAlignedBox.Extents.Y * vector3.X + 1E-08;
		}
	}
}
