using System;

namespace Hao.Geometry
{
	// Token: 0x02000079 RID: 121
	public static class ArcExtensionMethods
	{
		// Token: 0x06000482 RID: 1154 RVA: 0x0001616C File Offset: 0x0001436C
		public static AxisAlignedBox2 ComputeAxisAlignedBoundingBox(this Arc2 arc)
		{
			double minX = arc.Circle.Center.X - arc.Circle.Radius;
			double maxX = arc.Circle.Center.X + arc.Circle.Radius;
			double minY = arc.Circle.Center.Y - arc.Circle.Radius;
			double maxY = arc.Circle.Center.Y + arc.Circle.Radius;
			if (!arc.Contains(Angle.FromDegrees(0.0)))
			{
				maxX = Math.Max(arc.StartPoint.X, arc.EndPoint.X);
			}
			if (!arc.Contains(Angle.FromDegrees(90.0)))
			{
				maxY = Math.Max(arc.StartPoint.Y, arc.EndPoint.Y);
			}
			if (!arc.Contains(Angle.FromDegrees(180.0)))
			{
				minX = Math.Min(arc.StartPoint.X, arc.EndPoint.X);
			}
			if (!arc.Contains(Angle.FromDegrees(270.0)))
			{
				minY = Math.Min(arc.StartPoint.Y, arc.EndPoint.Y);
			}
			return new AxisAlignedBox2(minX, maxX, minY, maxY);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00016320 File Offset: 0x00014520
		public static AxisAlignedBox3 ComputeAxisAlignedBoundingBox(this Arc3 arc)
		{
			AxisAlignedBox3 axisAlignedBox = arc.Circle.ComputeAxisAlignedBoundingBox();
			double minX = axisAlignedBox.MinX;
			double maxX = axisAlignedBox.MaxX;
			double minY = axisAlignedBox.MinY;
			double maxY = axisAlignedBox.MaxY;
			double minZ = axisAlignedBox.MinZ;
			double maxZ = axisAlignedBox.MaxZ;
			Vector3 startPoint = arc.StartPoint;
			Vector3 endPoint = arc.EndPoint;
			UnitVector3 unitVector;
			if (arc.Circle.Normal.TryGetUnitCross(UnitVector3.UnitX, out unitVector))
			{
				UnitVector3 vector = unitVector.UnitCross(arc.Circle.Normal);
				double num = Math.Acos(arc.Circle.UnitU.Dot(vector));
				if (arc.Circle.UnitV.Dot(UnitVector3.UnitX) < 0.0)
				{
					num = 6.2831853071795862 - num;
				}
				if (!arc.Contains(Angle.FromRadians(num)))
				{
					maxX = Math.Max(startPoint.X, endPoint.X);
				}
				if (!arc.Contains(Angle.FromRadians((num + 3.1415926535897931) % 6.2831853071795862)))
				{
					minX = Math.Min(startPoint.X, endPoint.X);
				}
			}
			UnitVector3 unitVector2;
			if (arc.Circle.Normal.TryGetUnitCross(UnitVector3.UnitY, out unitVector2))
			{
				UnitVector3 vector2 = unitVector2.UnitCross(arc.Circle.Normal);
				double num2 = Math.Acos(arc.Circle.UnitU.Dot(vector2));
				if (arc.Circle.UnitV.Dot(UnitVector3.UnitY) < 0.0)
				{
					num2 = 6.2831853071795862 - num2;
				}
				if (!arc.Contains(Angle.FromRadians(num2)))
				{
					maxY = Math.Max(startPoint.Y, endPoint.Y);
				}
				if (!arc.Contains(Angle.FromRadians((num2 + 3.1415926535897931) % 6.2831853071795862)))
				{
					minY = Math.Min(startPoint.Y, endPoint.Y);
				}
			}
			UnitVector3 unitVector3;
			if (arc.Circle.Normal.TryGetUnitCross(UnitVector3.UnitZ, out unitVector3))
			{
				UnitVector3 vector3 = unitVector3.UnitCross(arc.Circle.Normal);
				double num3 = Math.Acos(arc.Circle.UnitU.Dot(vector3));
				if (arc.Circle.UnitV.Dot(UnitVector3.UnitZ) < 0.0)
				{
					num3 = 6.2831853071795862 - num3;
				}
				if (!arc.Contains(Angle.FromRadians(num3)))
				{
					maxZ = Math.Max(startPoint.Z, endPoint.Z);
				}
				if (!arc.Contains(Angle.FromRadians((num3 + 3.1415926535897931) % 6.2831853071795862)))
				{
					minZ = Math.Min(startPoint.Z, endPoint.Z);
				}
			}
			return new AxisAlignedBox3(minX, maxX, minY, maxY, minZ, maxZ);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00016663 File Offset: 0x00014863
		internal static bool Contains(this Arc3 arc, Angle angle)
		{
			return ArcExtensionMethods.Contains(arc.StartAngle, arc.DeltaAngle, angle);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00016679 File Offset: 0x00014879
		internal static bool Contains(this Arc2 arc, Angle angle)
		{
			return ArcExtensionMethods.Contains(arc.StartAngle, arc.DeltaAngle, angle);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00016690 File Offset: 0x00014890
		internal static bool Contains(Angle arcStartAngle, Angle arcDeltaAngle, Angle angle)
		{
			if (Math.Abs(arcDeltaAngle.Radians) >= 6.2831853071795862)
			{
				return true;
			}
			double num = angle.Radians % 6.2831853071795862;
			if (num < 0.0)
			{
				num += 6.2831853071795862;
			}
			double num2 = arcStartAngle.Radians % 6.2831853071795862;
			if (num2 < 0.0)
			{
				num2 += 6.2831853071795862;
			}
			double radians = arcDeltaAngle.Radians;
			if (radians > 0.0)
			{
				double num3 = (num2 + radians) % 6.2831853071795862;
				num2 -= 1E-08;
				num3 += 1E-08;
				if (num2 < num3)
				{
					if (num >= num2 && num <= num3)
					{
						return true;
					}
				}
				else if (num >= num2 || num <= num3)
				{
					return true;
				}
			}
			else
			{
				double num4 = (num2 + radians + 6.2831853071795862) % 6.2831853071795862;
				num2 += 1E-08;
				num4 -= 1E-08;
				if (num2 > num4)
				{
					if (num <= num2 && num >= num4)
					{
						return true;
					}
				}
				else if (num <= num2 || num >= num4)
				{
					return true;
				}
			}
			return false;
		}
	}
}
