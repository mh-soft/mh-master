using System;

namespace Hao.Geometry
{
	// Token: 0x02000078 RID: 120
	public static class CircleExtensionMethods
	{
		// Token: 0x0600047E RID: 1150 RVA: 0x00015F68 File Offset: 0x00014168
		public static AxisAlignedBox2 ComputeAxisAlignedBoundingBox(this Circle2 circle)
		{
			Vector2 min = circle.Center - new Vector2(circle.Radius, circle.Radius);
			Vector2 max = circle.Center + new Vector2(circle.Radius, circle.Radius);
			return new AxisAlignedBox2(min, max);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00015FBC File Offset: 0x000141BC
		public static AxisAlignedBox3 ComputeAxisAlignedBoundingBox(this Circle3 circle)
		{
			Vector3 center = circle.Center;
			UnitVector3 normal = circle.Normal;
			double a = Math.Acos(normal.Dot(UnitVector3.UnitX));
			double a2 = Math.Acos(normal.Dot(UnitVector3.UnitY));
			double a3 = Math.Acos(normal.Dot(UnitVector3.UnitZ));
			Vector3 vector = new Vector3(Math.Sin(a), Math.Sin(a2), Math.Sin(a3));
			vector *= circle.Radius;
			return new AxisAlignedBox3(center - vector, center + vector);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00016050 File Offset: 0x00014250
		internal static Angle GetPointDirectionAngle(this Circle2 circle, Vector2 point)
		{
			UnitVector2 unitVector;
			if (!(point - circle.Center).TryGetNormalized(out unitVector))
			{
				return Angle.Zero;
			}
			double num = Math.Acos(unitVector.Dot(UnitVector2.UnitX));
			if (unitVector.Dot(UnitVector2.UnitY) < 0.0)
			{
				num = 6.2831853071795862 - num;
			}
			return Angle.FromRadians(num);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000160B8 File Offset: 0x000142B8
		internal static Angle GetPointDirectionAngle(this Circle3 circle, Vector3 point)
		{
			UnitVector3 unitVector;
			if (!(new Plane3(circle.Normal, circle.Center).ClosestPointTo(point) - circle.Center).TryGetNormalized(out unitVector))
			{
				return Angle.Zero;
			}
			double num = unitVector.Dot(circle.UnitU);
			if (num > 1.0)
			{
				num = 1.0;
			}
			else if (num < -1.0)
			{
				num = -1.0;
			}
			double num2 = Math.Acos(num);
			if (unitVector.Dot(circle.UnitV) < 0.0)
			{
				num2 = 6.2831853071795862 - num2;
			}
			return Angle.FromRadians(num2);
		}
	}
}
