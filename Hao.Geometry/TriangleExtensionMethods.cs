using System;

namespace Hao.Geometry
{
	// Token: 0x0200007C RID: 124
	public static class TriangleExtensionMethods
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x000168B0 File Offset: 0x00014AB0
		public static AxisAlignedBox3 ComputeAxisAlignedBoundingBox(this Triangle3 triangle)
		{
			double minX = Math.Min(triangle.V0.X, Math.Min(triangle.V1.X, triangle.V2.X));
			double minY = Math.Min(triangle.V0.Y, Math.Min(triangle.V1.Y, triangle.V2.Y));
			double minZ = Math.Min(triangle.V0.Z, Math.Min(triangle.V1.Z, triangle.V2.Z));
			double maxX = Math.Max(triangle.V0.X, Math.Max(triangle.V1.X, triangle.V2.X));
			double maxY = Math.Max(triangle.V0.Y, Math.Max(triangle.V1.Y, triangle.V2.Y));
			double maxZ = Math.Max(triangle.V0.Z, Math.Max(triangle.V1.Z, triangle.V2.Z));
			return new AxisAlignedBox3(minX, maxX, minY, maxY, minZ, maxZ);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00016A2C File Offset: 0x00014C2C
		public static AxisAlignedBox2 ComputeAxisAlignedBoundingBox(this Triangle2 triangle)
		{
			double minX = Math.Min(triangle.V0.X, Math.Min(triangle.V1.X, triangle.V2.X));
			double minY = Math.Min(triangle.V0.Y, Math.Min(triangle.V1.Y, triangle.V2.Y));
			double maxX = Math.Max(triangle.V0.X, Math.Max(triangle.V1.X, triangle.V2.X));
			double maxY = Math.Max(triangle.V0.Y, Math.Max(triangle.V1.Y, triangle.V2.Y));
			return new AxisAlignedBox2(minX, maxX, minY, maxY);
		}
	}
}
