using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x0200004F RID: 79
	public static class IntersectionBox2
	{
		// Token: 0x06000322 RID: 802 RVA: 0x0000CECC File Offset: 0x0000B0CC
		public static bool Intersects(this Box2 box, Box2 other)
		{
			IntersectionBox2Box2 intersectionBox2Box = new IntersectionBox2Box2(box, other);
			return intersectionBox2Box.Test();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000CEE9 File Offset: 0x0000B0E9
		public static bool Intersects(this Box2 box, Line2 line2)
		{
			return line2.Intersects(box);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000CEF2 File Offset: 0x0000B0F2
		public static bool Intersects(this Box2 box, Segment2 segment2)
		{
			return segment2.Intersects(box);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000CEFB File Offset: 0x0000B0FB
		public static Segment2? IntersectionWith(this Box2 box, Line2 line)
		{
			return line.IntersectionWith(box);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000CF04 File Offset: 0x0000B104
		public static Segment2? IntersectionWith(this Box2 box, Segment2 segment)
		{
			return segment.IntersectionWith(box);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000CF0D File Offset: 0x0000B10D
		public static ICollection<Vector2> IntersectionPointsWith(this Box2 box, Line2 line)
		{
			return line.IntersectionPointsWith(box);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000CF16 File Offset: 0x0000B116
		public static ICollection<Vector2> IntersectionPointsWith(this Box2 box, Segment2 segment)
		{
			return segment.IntersectionPointsWith(box);
		}
	}
}
