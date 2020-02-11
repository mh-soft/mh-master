using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x0200003B RID: 59
	public static class IntersectionCircle2
	{
		// Token: 0x06000269 RID: 617 RVA: 0x0000A540 File Offset: 0x00008740
		public static bool Intersects(this Circle2 circle, Circle2 other)
		{
			IntersectionCircle2Circle2 intersectionCircle2Circle = new IntersectionCircle2Circle2(circle, other);
			return intersectionCircle2Circle.Find();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000A560 File Offset: 0x00008760
		public static ICollection<Vector2> IntersectionPointsWith(this Circle2 circle, Circle2 other)
		{
			IntersectionCircle2Circle2 intersectionCircle2Circle = new IntersectionCircle2Circle2(circle, other);
			intersectionCircle2Circle.Find();
			List<Vector2> list = new List<Vector2>();
			Vector2[] array = new Vector2[]
			{
				intersectionCircle2Circle.Point0,
				intersectionCircle2Circle.Point1
			};
			for (int i = 0; i < intersectionCircle2Circle.Quantity; i++)
			{
				list.Add(array[i]);
			}
			return list;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A5C7 File Offset: 0x000087C7
		public static bool Intersects(this Circle2 circle, Arc2 arc)
		{
			return arc.Intersects(circle);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000A5D0 File Offset: 0x000087D0
		public static ICollection<Vector2> IntersectionPointsWith(this Circle2 circle, Arc2 arc)
		{
			return arc.IntersectionPointsWith(circle);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000A5D9 File Offset: 0x000087D9
		public static bool Intersects(this Circle2 circle, Line2 line)
		{
			return line.Intersects(circle);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000A5E2 File Offset: 0x000087E2
		public static ICollection<Vector2> IntersectionPointsWith(this Circle2 circle, Line2 line)
		{
			return line.IntersectionPointsWith(circle);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000A5EB File Offset: 0x000087EB
		public static bool Intersects(this Circle2 circle, Ray2 ray)
		{
			return ray.Intersects(circle);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A5F4 File Offset: 0x000087F4
		public static ICollection<Vector2> IntersectionPointsWith(this Circle2 circle, Ray2 ray)
		{
			return ray.IntersectionPointsWith(circle);
		}
	}
}
