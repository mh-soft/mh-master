using System;

namespace Hao.Geometry
{

	public static class CylinderExtensionMethods
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x000167AC File Offset: 0x000149AC
		public static AxisAlignedBox3 ComputeAxisAlignedBoundingBox(this Cylinder3 cylinder)
		{
			double scalar = cylinder.Height / 2.0;
			AffineTransform3 affineTransform = Transform3Factory.CreateOrthonormalBasis(cylinder.Axis.Direction);
			Circle3 circle = new Circle3(cylinder.Axis.Origin + scalar * cylinder.Axis.Direction, affineTransform.AxisX, affineTransform.AxisY, affineTransform.AxisZ, cylinder.Radius);
			Circle3 circle2 = new Circle3(cylinder.Axis.Origin - scalar * cylinder.Axis.Direction, affineTransform.AxisX, affineTransform.AxisY, affineTransform.AxisZ, cylinder.Radius);
			AxisAlignedBox3 axisAlignedBox = circle.ComputeAxisAlignedBoundingBox();
			AxisAlignedBox3 other = circle2.ComputeAxisAlignedBoundingBox();
			return axisAlignedBox.CreateMergedWith(other);
		}
	}
}
