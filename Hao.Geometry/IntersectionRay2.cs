using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x02000038 RID: 56
	public static class IntersectionRay2
	{
		// Token: 0x06000257 RID: 599 RVA: 0x0000A26C File Offset: 0x0000846C
		public static bool Intersects(this Ray2 ray, Segment2 segment)
		{
			IntersectionRay2Segment2 intersectionRay2Segment = new IntersectionRay2Segment2(ray, segment);
			return intersectionRay2Segment.Find();
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000A28C File Offset: 0x0000848C
		public static Vector2? IntersectionWith(this Ray2 ray, Segment2 segment)
		{
			IntersectionRay2Segment2 intersectionRay2Segment = new IntersectionRay2Segment2(ray, segment);
			if (intersectionRay2Segment.Find() && (intersectionRay2Segment.IntersectionType == Intersection.Type.IT_POINT || intersectionRay2Segment.IntersectionType == Intersection.Type.IT_SEGMENT))
			{
				return new Vector2?(intersectionRay2Segment.Point0);
			}
			return null;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000A2D8 File Offset: 0x000084D8
		public static bool Intersects(this Ray2 ray, Arc2 arc)
		{
			IntersectionRay2Arc2 intersectionRay2Arc = new IntersectionRay2Arc2(ray, arc);
			return intersectionRay2Arc.Find();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000A2F8 File Offset: 0x000084F8
		public static ICollection<Vector2> IntersectionPointsWith(this Ray2 ray, Arc2 arc)
		{
			IntersectionRay2Arc2 intersectionRay2Arc = new IntersectionRay2Arc2(ray, arc);
			intersectionRay2Arc.Find();
			List<Vector2> list = new List<Vector2>();
			Vector2[] array = new Vector2[]
			{
				intersectionRay2Arc.Point0,
				intersectionRay2Arc.Point1
			};
			for (int i = 0; i < intersectionRay2Arc.Quantity; i++)
			{
				list.Add(array[i]);
			}
			return list;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000A360 File Offset: 0x00008560
		public static bool Intersects(this Ray2 ray, Circle2 circle)
		{
			IntersectionRay2Circle2 intersectionRay2Circle = new IntersectionRay2Circle2(ray, circle);
			return intersectionRay2Circle.Find();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000A380 File Offset: 0x00008580
		public static ICollection<Vector2> IntersectionPointsWith(this Ray2 ray, Circle2 circle)
		{
			IntersectionRay2Circle2 intersectionRay2Circle = new IntersectionRay2Circle2(ray, circle);
			intersectionRay2Circle.Find();
			List<Vector2> list = new List<Vector2>();
			Vector2[] array = new Vector2[]
			{
				intersectionRay2Circle.Point0,
				intersectionRay2Circle.Point1
			};
			for (int i = 0; i < intersectionRay2Circle.Quantity; i++)
			{
				list.Add(array[i]);
			}
			return list;
		}
	}
}
