using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x0200006D RID: 109
	public static class IntersectionTriangle2
	{
		// Token: 0x06000434 RID: 1076 RVA: 0x00013108 File Offset: 0x00011308
		public static bool Intersects(this Triangle2 triangle, Triangle2 other)
		{
			IntersectionTriangle2Triangle2 intersectionTriangle2Triangle = new IntersectionTriangle2Triangle2(triangle, other);
			return intersectionTriangle2Triangle.Test();
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00013128 File Offset: 0x00011328
		public static ICollection<Vector2> IntersectionPointsWith(this Triangle2 triangle, Triangle2 other)
		{
			IntersectionTriangle2Triangle2 intersectionTriangle2Triangle = new IntersectionTriangle2Triangle2(triangle, other);
			intersectionTriangle2Triangle.Find();
			List<Vector2> list = new List<Vector2>();
			for (int i = 0; i < intersectionTriangle2Triangle.Quantity; i++)
			{
				list.Add(intersectionTriangle2Triangle.Points[i]);
			}
			return list;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00013172 File Offset: 0x00011372
		public static bool Intersects(this Triangle2 triangle, Line2 line)
		{
			return line.Intersects(triangle);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001317B File Offset: 0x0001137B
		public static Segment2? IntersectionWith(this Triangle2 triangle, Line2 line)
		{
			return line.IntersectionWith(triangle);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00013184 File Offset: 0x00011384
		public static ICollection<Vector2> IntersectionPointsWith(this Triangle2 triangle, Line2 line)
		{
			return line.IntersectionPointsWith(triangle);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001318D File Offset: 0x0001138D
		public static bool Intersects(this Triangle2 triangle, Segment2 segment)
		{
			return segment.Intersects(triangle);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00013196 File Offset: 0x00011396
		public static Segment2? IntersectionWith(this Triangle2 triangle, Segment2 segment)
		{
			return segment.IntersectionWith(triangle);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001319F File Offset: 0x0001139F
		public static ICollection<Vector2> IntersectionPointsWith(this Triangle2 triangle, Segment2 segment)
		{
			return segment.IntersectionPointsWith(triangle);
		}
	}
}
