using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x0200004B RID: 75
	public static class IntersectionAxisAlignedBox2
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0000C410 File Offset: 0x0000A610
		public static bool Intersects(this AxisAlignedBox2 axisAlignedBox, AxisAlignedBox2 other)
		{
			IntersectionAxisAlignedBox2AxisAlignedBox2 intersectionAxisAlignedBox2AxisAlignedBox = new IntersectionAxisAlignedBox2AxisAlignedBox2(axisAlignedBox, other);
			return intersectionAxisAlignedBox2AxisAlignedBox.Test();
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000C430 File Offset: 0x0000A630
		public static AxisAlignedBox2? IntersectionWith(this AxisAlignedBox2 axisAlignedBox, AxisAlignedBox2 other)
		{
			IntersectionAxisAlignedBox2AxisAlignedBox2 intersectionAxisAlignedBox2AxisAlignedBox = new IntersectionAxisAlignedBox2AxisAlignedBox2(axisAlignedBox, other);
			AxisAlignedBox2 value;
			if (intersectionAxisAlignedBox2AxisAlignedBox.Find(out value))
			{
				return new AxisAlignedBox2?(value);
			}
			return null;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000C461 File Offset: 0x0000A661
		public static bool Intersects(this AxisAlignedBox2 axisAlignedBox, Segment2 segment)
		{
			return segment.Intersects(axisAlignedBox);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000C46A File Offset: 0x0000A66A
		public static Segment2? IntersectionWith(this AxisAlignedBox2 axisAlignedBox, Segment2 segment)
		{
			return segment.IntersectionWith(axisAlignedBox);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000C473 File Offset: 0x0000A673
		public static ICollection<Vector2> IntersectionPointsWith(this AxisAlignedBox2 axisAlignedBox, Segment2 segment)
		{
			return segment.IntersectionPointsWith(axisAlignedBox);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000C47C File Offset: 0x0000A67C
		public static bool Intersects(this AxisAlignedBox2 axisAlignedBox, Line2 line)
		{
			return line.Intersects(axisAlignedBox);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000C485 File Offset: 0x0000A685
		public static Segment2? IntersectionWith(this AxisAlignedBox2 axisAlignedBox, Line2 line)
		{
			return line.IntersectionWith(axisAlignedBox);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000C48E File Offset: 0x0000A68E
		public static ICollection<Vector2> IntersectionPointsWith(this AxisAlignedBox2 axisAlignedBox, Line2 line)
		{
			return line.IntersectionPointsWith(axisAlignedBox);
		}
	}
}
