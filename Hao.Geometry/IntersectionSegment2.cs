using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x02000065 RID: 101
	public static class IntersectionSegment2
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x000118EC File Offset: 0x0000FAEC
		public static bool Intersects(this Segment2 segment, AxisAlignedBox2 axisAlignedBox)
		{
			Box2 box = new Box2(axisAlignedBox.Center, UnitVector2.UnitX, UnitVector2.UnitY, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY);
			IntersectionSegment2Box2 intersectionSegment2Box = new IntersectionSegment2Box2(segment, box, true);
			return intersectionSegment2Box.Test();
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00011930 File Offset: 0x0000FB30
		public static Segment2? IntersectionWith(this Segment2 segment, AxisAlignedBox2 axisAlignedBox)
		{
			Box2 box = new Box2(axisAlignedBox.Center, UnitVector2.UnitX, UnitVector2.UnitY, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY);
			IntersectionSegment2Box2 intersectionSegment2Box = new IntersectionSegment2Box2(segment, box, true);
			bool flag = intersectionSegment2Box.Find();
			if (flag && intersectionSegment2Box.IntersectionType == Intersection.Type.IT_SEGMENT)
			{
				return new Segment2?(new Segment2(intersectionSegment2Box.Point0, intersectionSegment2Box.Point1));
			}
			if (flag && intersectionSegment2Box.IntersectionType == Intersection.Type.IT_POINT)
			{
				return new Segment2?(new Segment2(intersectionSegment2Box.Point0, segment.Direction, 0.0));
			}
			return null;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000119D4 File Offset: 0x0000FBD4
		public static ICollection<Vector2> IntersectionPointsWith(this Segment2 segment, AxisAlignedBox2 axisAlignedBox)
		{
			Box2 box = new Box2(axisAlignedBox.Center, UnitVector2.UnitX, UnitVector2.UnitY, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY);
			IntersectionSegment2Box2 intersectionSegment2Box = new IntersectionSegment2Box2(segment, box, true);
			intersectionSegment2Box.Find();
			List<Vector2> list = new List<Vector2>();
			Vector2[] array = new Vector2[]
			{
				intersectionSegment2Box.Point0,
				intersectionSegment2Box.Point1
			};
			for (int i = 0; i < intersectionSegment2Box.Quantity; i++)
			{
				list.Add(array[i]);
			}
			return list;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00011A67 File Offset: 0x0000FC67
		public static bool Intersects(this Segment2 segment, Ray2 ray)
		{
			return ray.Intersects(segment);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00011A70 File Offset: 0x0000FC70
		public static Vector2? IntersectionWith(this Segment2 segment, Ray2 ray)
		{
			return ray.IntersectionWith(segment);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00011A7C File Offset: 0x0000FC7C
		public static bool Intersects(this Segment2 segment, Box2 box)
		{
			IntersectionSegment2Box2 intersectionSegment2Box = new IntersectionSegment2Box2(segment, box, true);
			return intersectionSegment2Box.Test();
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00011A9C File Offset: 0x0000FC9C
		public static Segment2? IntersectionWith(this Segment2 segment, Box2 box)
		{
			IntersectionSegment2Box2 intersectionSegment2Box = new IntersectionSegment2Box2(segment, box, true);
			bool flag = intersectionSegment2Box.Find();
			if (flag && intersectionSegment2Box.IntersectionType == Intersection.Type.IT_SEGMENT)
			{
				return new Segment2?(new Segment2(intersectionSegment2Box.Point0, intersectionSegment2Box.Point1));
			}
			if (flag && intersectionSegment2Box.IntersectionType == Intersection.Type.IT_POINT)
			{
				return new Segment2?(new Segment2(intersectionSegment2Box.Point0, segment.Direction, 0.0));
			}
			return null;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00011B1C File Offset: 0x0000FD1C
		public static ICollection<Vector2> IntersectionPointsWith(this Segment2 segment, Box2 box)
		{
			IntersectionSegment2Box2 intersectionSegment2Box = new IntersectionSegment2Box2(segment, box, true);
			intersectionSegment2Box.Find();
			List<Vector2> list = new List<Vector2>();
			Vector2[] array = new Vector2[]
			{
				intersectionSegment2Box.Point0,
				intersectionSegment2Box.Point1
			};
			for (int i = 0; i < intersectionSegment2Box.Quantity; i++)
			{
				list.Add(array[i]);
			}
			return list;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00011B84 File Offset: 0x0000FD84
		public static bool Intersects(this Segment2 segment, Triangle2 triangle)
		{
			IntersectionSegment2Triangle2 intersectionSegment2Triangle = new IntersectionSegment2Triangle2(segment, triangle);
			return intersectionSegment2Triangle.Test();
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00011BA4 File Offset: 0x0000FDA4
		public static Segment2? IntersectionWith(this Segment2 segment, Triangle2 triangle)
		{
			IntersectionSegment2Triangle2 intersectionSegment2Triangle = new IntersectionSegment2Triangle2(segment, triangle);
			bool flag = intersectionSegment2Triangle.Find();
			if (flag && intersectionSegment2Triangle.IntersectionType == Intersection.Type.IT_SEGMENT)
			{
				return new Segment2?(new Segment2(intersectionSegment2Triangle.Point0, intersectionSegment2Triangle.Point1));
			}
			if (flag && intersectionSegment2Triangle.IntersectionType == Intersection.Type.IT_POINT)
			{
				return new Segment2?(new Segment2(intersectionSegment2Triangle.Point0, segment.Direction, 0.0));
			}
			return null;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00011C20 File Offset: 0x0000FE20
		public static ICollection<Vector2> IntersectionPointsWith(this Segment2 segment, Triangle2 triangle)
		{
			IntersectionSegment2Triangle2 intersectionSegment2Triangle = new IntersectionSegment2Triangle2(segment, triangle);
			intersectionSegment2Triangle.Find();
			List<Vector2> list = new List<Vector2>();
			Vector2[] array = new Vector2[]
			{
				intersectionSegment2Triangle.Point0,
				intersectionSegment2Triangle.Point1
			};
			for (int i = 0; i < intersectionSegment2Triangle.Quantity; i++)
			{
				list.Add(array[i]);
			}
			return list;
		}
	}
}
