using System;

namespace Hao.Geometry
{
	
	internal static class Intersection
	{
		
		internal static Segment3? DoClipping(double fT0, double fT1, Vector3 origin, UnitVector3 direction, Box3 box)
		{
			MathBase.Assert(fT0 < fT1, "Intersection.DoClipping(): invalid arguments");
			Vector3 vector = origin - box.Center;
			Vector3 vector2 = new Vector3(vector.Dot(box.Axis0), vector.Dot(box.Axis1), vector.Dot(box.Axis2));
			Vector3 vector3 = new Vector3(direction.Dot(box.Axis0), direction.Dot(box.Axis1), direction.Dot(box.Axis2));
			if (!Intersection.Clip(vector3.X, -vector2.X - box.Extent0, ref fT0, ref fT1) || !Intersection.Clip(-vector3.X, vector2.X - box.Extent0, ref fT0, ref fT1) || !Intersection.Clip(vector3.Y, -vector2.Y - box.Extent1, ref fT0, ref fT1) || !Intersection.Clip(-vector3.Y, vector2.Y - box.Extent1, ref fT0, ref fT1) || !Intersection.Clip(vector3.Z, -vector2.Z - box.Extent2, ref fT0, ref fT1) || !Intersection.Clip(-vector3.Z, vector2.Z - box.Extent2, ref fT0, ref fT1))
			{
				return null;
			}
			if (fT1 > fT0)
			{
				Vector3 left = origin + fT0 * direction;
				Vector3 right = origin + fT1 * direction;
				Vector3 origin2 = (left + right) * 0.5;
				double extent = (left - right).Length * 0.5;
				return new Segment3?(new Segment3(origin2, direction, extent));
			}
			return new Segment3?(new Segment3(origin + fT0 * direction, direction, 0.0));
		}

		
		internal static bool DoClipping(double t0, double t1, Vector2 origin, UnitVector2 direction, Box2 box, bool solid, out int quantity, out Vector2 point0, out Vector2 point1, out Intersection.Type intrType)
		{
			Vector2 vector = origin - box.Center;
			Vector2 vector2 = new Vector2(vector.Dot(box.Axis0), vector.Dot(box.Axis1));
			Vector2 vector3 = new Vector2(direction.Dot(box.Axis0), direction.Dot(box.Axis1));
			double num = t0;
			double num2 = t1;
			bool flag = Intersection.Clip(vector3.X, -vector2.X - box.Extent0, ref t0, ref t1) && Intersection.Clip(-vector3.X, vector2.X - box.Extent0, ref t0, ref t1) && Intersection.Clip(vector3.Y, -vector2.Y - box.Extent1, ref t0, ref t1) && Intersection.Clip(-vector3.Y, vector2.Y - box.Extent1, ref t0, ref t1);
			point0 = Vector2.Zero;
			point1 = Vector2.Zero;
			if (flag && (solid || t0 != num || t1 != num2))
			{
				if (t1 > t0)
				{
					intrType = Intersection.Type.IT_SEGMENT;
					quantity = 2;
					point0 = origin + t0 * direction;
					point1 = origin + t1 * direction;
				}
				else
				{
					intrType = Intersection.Type.IT_POINT;
					quantity = 1;
					point0 = origin + t0 * direction;
				}
			}
			else
			{
				intrType = Intersection.Type.IT_EMPTY;
				quantity = 0;
			}
			return intrType > Intersection.Type.IT_EMPTY;
		}

		
		private static bool Clip(double denom, double numer, ref double t0, ref double t1)
		{
			if (denom > 0.0)
			{
				if (numer > denom * t1 + 1E-08)
				{
					return false;
				}
				if (numer > denom * t0)
				{
					t0 = numer / denom;
				}
				return true;
			}
			else
			{
				if (denom >= 0.0)
				{
					return numer < 1E-08;
				}
				if (numer > denom * t0 + 1E-08)
				{
					return false;
				}
				if (numer > denom * t1)
				{
					t1 = numer / denom;
				}
				return true;
			}
		}

		/// <summary>
		/// 当前相交的类型
		/// </summary>
		internal enum Type
		{
			IT_EMPTY,
			IT_POINT,
			IT_SEGMENT,
			IT_LINE,
			IT_POLYGON,
			IT_PLANE,
			IT_OTHER
		}
	}
}
