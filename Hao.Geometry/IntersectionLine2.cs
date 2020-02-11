using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x02000053 RID: 83
	public static class IntersectionLine2
	{
	
		public static bool Intersects(this Line2 line, Triangle2 triangle)
		{
			IntersectionLine2Triangle2 intersectionLine2Triangle = new IntersectionLine2Triangle2(line, triangle);
			return intersectionLine2Triangle.Test();
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000DD6C File Offset: 0x0000BF6C
		public static bool Intersects(this Line2 line, Arc2 arc)
		{
			IntersectionLine2Arc2 intersectionLine2Arc = new IntersectionLine2Arc2(line, arc);
			return intersectionLine2Arc.Find();
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000DD8C File Offset: 0x0000BF8C
		public static bool Intersects(this Line2 line, Box2 box)
		{
			IntersectionLine2Box2 intersectionLine2Box = new IntersectionLine2Box2(line, box);
			return intersectionLine2Box.Test();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000DDAC File Offset: 0x0000BFAC
		public static bool Intersects(this Line2 line, AxisAlignedBox2 axisAlignedBox)
		{
			Box2 box = new Box2(axisAlignedBox.Center, UnitVector2.UnitX, UnitVector2.UnitY, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY);
			IntersectionLine2Box2 intersectionLine2Box = new IntersectionLine2Box2(line, box);
			return intersectionLine2Box.Test();
		}


		/// <summary>
		/// 线和线相交
		/// </summary>
		/// <param name="line"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static bool Intersects(this Line2 line, Line2 other)
		{
			IntersectionLine2Line2 intersectionLine2Line = new IntersectionLine2Line2(line, other);
			return intersectionLine2Line.Find();
		}

		/// <summary>
		/// 线和圆形相交
		/// </summary>
		/// <param name="line"></param>
		/// <param name="circle"></param>
		/// <returns></returns>
		public static bool Intersects(this Line2 line, Circle2 circle)
		{
			IntersectionLine2Circle2 intersectionLine2Circle = new IntersectionLine2Circle2(line, circle);
			return intersectionLine2Circle.Find();
		}

		/// <summary>
		/// 线和三角形相交
		/// </summary>
		/// <param name="line"></param>
		/// <param name="triangle"></param>
		/// <returns></returns>
		public static Segment2? IntersectionWith(this Line2 line, Triangle2 triangle)
		{
			IntersectionLine2Triangle2 intersectionLine2Triangle = new IntersectionLine2Triangle2(line, triangle);
			bool flag = intersectionLine2Triangle.Find();
			if (flag && intersectionLine2Triangle.IntersectionType == Intersection.Type.IT_SEGMENT)
			{
				return new Segment2?(new Segment2(intersectionLine2Triangle.Point0, intersectionLine2Triangle.Point1));
			}
			if (flag && intersectionLine2Triangle.IntersectionType == Intersection.Type.IT_POINT)
			{
				return new Segment2?(new Segment2(intersectionLine2Triangle.Point0, line.Direction, 0.0));
			}
			return null;
		}

		/// <summary>
		/// 线和BOX相交
		/// </summary>
		/// <param name="line"></param>
		/// <param name="box"></param>
		/// <returns></returns>
		public static Segment2? IntersectionWith(this Line2 line, Box2 box)
		{
			IntersectionLine2Box2 intersectionLine2Box = new IntersectionLine2Box2(line, box);
			bool flag = intersectionLine2Box.Find();
			if (flag && intersectionLine2Box.IntersectionType == Intersection.Type.IT_SEGMENT)
			{
				return new Segment2?(new Segment2(intersectionLine2Box.Point0, intersectionLine2Box.Point1));
			}
			if (flag && intersectionLine2Box.IntersectionType == Intersection.Type.IT_POINT)
			{
				return new Segment2?(new Segment2(intersectionLine2Box.Point0, line.Direction, 0.0));
			}
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="line"></param>
		/// <param name="axisAlignedBox"></param>
		/// <returns></returns>
		public static Segment2? IntersectionWith(this Line2 line, AxisAlignedBox2 axisAlignedBox)
		{
			Box2 box = new Box2(axisAlignedBox.Center, UnitVector2.UnitX, UnitVector2.UnitY, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY);
			IntersectionLine2Box2 intersectionLine2Box = new IntersectionLine2Box2(line, box);
			bool flag = intersectionLine2Box.Find();
			if (flag && intersectionLine2Box.IntersectionType == Intersection.Type.IT_SEGMENT)
			{
				return new Segment2?(new Segment2(intersectionLine2Box.Point0, intersectionLine2Box.Point1));
			}
			if (flag && intersectionLine2Box.IntersectionType == Intersection.Type.IT_POINT)
			{
				return new Segment2?(new Segment2(intersectionLine2Box.Point0, line.Direction, 0.0));
			}
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="line"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static Vector2? IntersectionWith(this Line2 line, Line2 other)
		{
			IntersectionLine2Line2 intersectionLine2Line = new IntersectionLine2Line2(line, other);
			if (intersectionLine2Line.Find() && intersectionLine2Line.IntersectionType == Intersection.Type.IT_POINT)
			{
				return new Vector2?(intersectionLine2Line.Point);
			}
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="line"></param>
		/// <param name="triangle"></param>
		/// <returns></returns>
		public static ICollection<Vector2> IntersectionPointsWith(this Line2 line, Triangle2 triangle)
		{
			IntersectionLine2Triangle2 intersectionLine2Triangle = new IntersectionLine2Triangle2(line, triangle);
			intersectionLine2Triangle.Find();
			List<Vector2> list = new List<Vector2>();
			Vector2[] array = new Vector2[]
			{
				intersectionLine2Triangle.Point0,
				intersectionLine2Triangle.Point1
			};
			for (int i = 0; i < intersectionLine2Triangle.Quantity; i++)
			{
				list.Add(array[i]);
			}
			return list;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="line"></param>
		/// <param name="arc"></param>
		/// <returns></returns>
		public static ICollection<Vector2> IntersectionPointsWith(this Line2 line, Arc2 arc)
		{
			IntersectionLine2Arc2 intersectionLine2Arc = new IntersectionLine2Arc2(line, arc);
			intersectionLine2Arc.Find();
			List<Vector2> list = new List<Vector2>();
			Vector2[] array = new Vector2[]
			{
				intersectionLine2Arc.Point0,
				intersectionLine2Arc.Point1
			};
			for (int i = 0; i < intersectionLine2Arc.Quantity; i++)
			{
				list.Add(array[i]);
			}
			return list;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="line"></param>
		/// <param name="box"></param>
		/// <returns></returns>
		public static ICollection<Vector2> IntersectionPointsWith(this Line2 line, Box2 box)
		{
			IntersectionLine2Box2 intersectionLine2Box = new IntersectionLine2Box2(line, box);
			intersectionLine2Box.Find();
			List<Vector2> list = new List<Vector2>();
			Vector2[] array = new Vector2[]
			{
				intersectionLine2Box.Point0,
				intersectionLine2Box.Point1
			};
			for (int i = 0; i < intersectionLine2Box.Quantity; i++)
			{
				list.Add(array[i]);
			}
			return list;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="line"></param>
		/// <param name="axisAlignedBox"></param>
		/// <returns></returns>
		public static ICollection<Vector2> IntersectionPointsWith(this Line2 line, AxisAlignedBox2 axisAlignedBox)
		{
			Box2 box = new Box2(axisAlignedBox.Center, UnitVector2.UnitX, UnitVector2.UnitY, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY);
			IntersectionLine2Box2 intersectionLine2Box = new IntersectionLine2Box2(line, box);
			intersectionLine2Box.Find();
			List<Vector2> list = new List<Vector2>();
			Vector2[] array = new Vector2[]
			{
				intersectionLine2Box.Point0,
				intersectionLine2Box.Point1
			};
			for (int i = 0; i < intersectionLine2Box.Quantity; i++)
			{
				list.Add(array[i]);
			}
			return list;
		}

		public static ICollection<Vector2> IntersectionPointsWith(this Line2 line, Circle2 circle)
		{
			IntersectionLine2Circle2 intersectionLine2Circle = new IntersectionLine2Circle2(line, circle);
			intersectionLine2Circle.Find();
			List<Vector2> list = new List<Vector2>();
			Vector2[] array = new Vector2[]
			{
				intersectionLine2Circle.Point0,
				intersectionLine2Circle.Point1
			};
			for (int i = 0; i < intersectionLine2Circle.Quantity; i++)
			{
				list.Add(array[i]);
			}
			return list;
		}
	}
}
