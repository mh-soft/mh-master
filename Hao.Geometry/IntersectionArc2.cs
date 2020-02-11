using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x0200003A RID: 58
	public static class IntersectionArc2
	{
		// Token: 0x06000261 RID: 609 RVA: 0x0000A40C File Offset: 0x0000860C
		public static bool Intersects(this Arc2 arc, Arc2 other)
		{
			IntersectionArc2Arc2 intersectionArc2Arc = new IntersectionArc2Arc2(arc, other);
			return intersectionArc2Arc.Find();
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A42C File Offset: 0x0000862C
		public static ICollection<Vector2> IntersectionPointsWith(this Arc2 arc, Arc2 other)
		{
			IntersectionArc2Arc2 intersectionArc2Arc = new IntersectionArc2Arc2(arc, other);
			intersectionArc2Arc.Find();
			List<Vector2> list = new List<Vector2>();
			Vector2[] array = new Vector2[]
			{
				intersectionArc2Arc.Point0,
				intersectionArc2Arc.Point1
			};
			for (int i = 0; i < intersectionArc2Arc.Quantity; i++)
			{
				list.Add(array[i]);
			}
			return list;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A494 File Offset: 0x00008694
		public static bool Intersects(this Arc2 arc, Circle2 circle)
		{
			IntersectionArc2Circle2 intersectionArc2Circle = new IntersectionArc2Circle2(arc, circle);
			return intersectionArc2Circle.Find();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000A4B4 File Offset: 0x000086B4
		public static ICollection<Vector2> IntersectionPointsWith(this Arc2 arc, Circle2 circle)
		{
			IntersectionArc2Circle2 intersectionArc2Circle = new IntersectionArc2Circle2(arc, circle);
			intersectionArc2Circle.Find();
			List<Vector2> list = new List<Vector2>();
			Vector2[] array = new Vector2[]
			{
				intersectionArc2Circle.Point0,
				intersectionArc2Circle.Point1
			};
			for (int i = 0; i < intersectionArc2Circle.Quantity; i++)
			{
				list.Add(array[i]);
			}
			return list;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000A51B File Offset: 0x0000871B
		public static bool Intersects(this Arc2 arc, Line2 line)
		{
			return line.Intersects(arc);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000A524 File Offset: 0x00008724
		public static ICollection<Vector2> IntersectionPointsWith(this Arc2 arc, Line2 line)
		{
			return line.IntersectionPointsWith(arc);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000A52D File Offset: 0x0000872D
		public static bool Intersects(this Arc2 arc, Ray2 ray)
		{
			return ray.Intersects(arc);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000A536 File Offset: 0x00008736
		public static ICollection<Vector2> IntersectionPointsWith(this Arc2 arc, Ray2 ray)
		{
			return ray.IntersectionPointsWith(arc);
		}
	}
}
