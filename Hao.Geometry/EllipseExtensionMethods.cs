using System;

namespace Hao.Geometry
{
	// Token: 0x0200007F RID: 127
	public static class EllipseExtensionMethods
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x00016D10 File Offset: 0x00014F10
		public static AxisAlignedBox2 ComputeAxisAlignedBoundingBox(this Ellipse2 ellipse)
		{
			double num;
			double num2;
			double num3;
			double num4;
			double val;
			double val2;
			double val3;
			double val4;
			EllipseExtensionMethods.ComputeEllipseBoundingBoxVariables(ellipse, out num, out num2, out num3, out num4, out val, out val2, out val3, out val4);
			return new AxisAlignedBox2(Math.Min(val, val2), Math.Max(val, val2), Math.Min(val3, val4), Math.Max(val3, val4));
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00016D5C File Offset: 0x00014F5C
		public static AxisAlignedBox2 ComputeAxisAlignedBoundingBox(this EllipticArc2 ellipticArc)
		{
			double radians;
			double radians2;
			double radians3;
			double radians4;
			double num;
			double num2;
			double num3;
			double num4;
			EllipseExtensionMethods.ComputeEllipseBoundingBoxVariables(ellipticArc.Ellipse, out radians, out radians2, out radians3, out radians4, out num, out num2, out num3, out num4);
			Vector2 startPoint = ellipticArc.StartPoint;
			Vector2 endPoint = ellipticArc.EndPoint;
			if (!ellipticArc.Contains(Angle.FromRadians(radians)))
			{
				num = ((num < num2) ? Math.Min(startPoint.X, endPoint.X) : Math.Max(startPoint.X, endPoint.X));
			}
			if (!ellipticArc.Contains(Angle.FromRadians(radians2)))
			{
				num2 = ((num2 < num) ? Math.Min(startPoint.X, endPoint.X) : Math.Max(startPoint.X, endPoint.X));
			}
			if (!ellipticArc.Contains(Angle.FromRadians(radians3)))
			{
				num3 = ((num3 < num4) ? Math.Min(startPoint.Y, endPoint.Y) : Math.Max(startPoint.Y, endPoint.Y));
			}
			if (!ellipticArc.Contains(Angle.FromRadians(radians4)))
			{
				num4 = ((num4 < num3) ? Math.Min(startPoint.Y, endPoint.Y) : Math.Max(startPoint.Y, endPoint.Y));
			}
			return new AxisAlignedBox2(Math.Min(num, num2), Math.Max(num, num2), Math.Min(num3, num4), Math.Max(num3, num4));
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00016EB8 File Offset: 0x000150B8
		internal static bool Contains(this EllipticArc2 arc, Angle angle)
		{
			return ArcExtensionMethods.Contains(arc.StartAngle, arc.DeltaAngle, angle);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00016ED0 File Offset: 0x000150D0
		private static void ComputeEllipseBoundingBoxVariables(Ellipse2 ellipse, out double tx1, out double tx2, out double ty1, out double ty2, out double x1, out double x2, out double y1, out double y2)
		{
			double x3 = ellipse.Center.X;
			double y3 = ellipse.Center.Y;
			double extent = ellipse.Extent0;
			double extent2 = ellipse.Extent1;
			double num = Math.Acos(ellipse.Axis0.Dot(UnitVector2.UnitX));
			if (ellipse.Axis0.Dot(UnitVector2.UnitY) < 0.0)
			{
				num = -num;
			}
			double num2 = Math.Tan(num);
			double num3 = 1.0 / num2;
			double num4 = Math.Cos(num);
			double num5 = Math.Sin(num);
			tx1 = (Math.Atan(-extent2 * num2 / extent) + 6.2831853071795862) % 6.2831853071795862;
			ty1 = (Math.Atan(extent2 * num3 / extent) + 6.2831853071795862) % 6.2831853071795862;
			tx2 = (tx1 + 3.1415926535897931) % 6.2831853071795862;
			ty2 = (ty1 + 3.1415926535897931) % 6.2831853071795862;
			x1 = x3 + extent * Math.Cos(tx1) * num4 - extent2 * Math.Sin(tx1) * num5;
			x2 = x3 + extent * Math.Cos(tx2) * num4 - extent2 * Math.Sin(tx2) * num5;
			y1 = y3 + extent2 * Math.Sin(ty1) * num4 + extent * Math.Cos(ty1) * num5;
			y2 = y3 + extent2 * Math.Sin(ty2) * num4 + extent * Math.Cos(ty2) * num5;
		}
	}
}
