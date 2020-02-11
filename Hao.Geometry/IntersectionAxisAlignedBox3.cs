using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x0200004D RID: 77
	public static class IntersectionAxisAlignedBox3
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		public static bool Intersects(this AxisAlignedBox3 axisAlignedBox3, AxisAlignedBox3 other)
		{
			IntersectionAxisAlignedBox3AxisAlignedBox3 intersectionAxisAlignedBox3AxisAlignedBox = new IntersectionAxisAlignedBox3AxisAlignedBox3(axisAlignedBox3, other);
			return intersectionAxisAlignedBox3AxisAlignedBox.Test();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000C7FC File Offset: 0x0000A9FC
		public static AxisAlignedBox3? IntersectionWith(this AxisAlignedBox3 axisAlignedBox3, AxisAlignedBox3 other)
		{
			IntersectionAxisAlignedBox3AxisAlignedBox3 intersectionAxisAlignedBox3AxisAlignedBox = new IntersectionAxisAlignedBox3AxisAlignedBox3(axisAlignedBox3, other);
			AxisAlignedBox3 value;
			if (intersectionAxisAlignedBox3AxisAlignedBox.Find(out value))
			{
				return new AxisAlignedBox3?(value);
			}
			return null;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000C82D File Offset: 0x0000AA2D
		public static bool Intersects(this AxisAlignedBox3 axisAlignedBox3, Line3 line3)
		{
			return line3.Intersects(axisAlignedBox3);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000C836 File Offset: 0x0000AA36
		public static Segment3? IntersectionWith(this AxisAlignedBox3 axisAlignedBox3, Line3 line3)
		{
			return line3.IntersectionWith(axisAlignedBox3);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000C83F File Offset: 0x0000AA3F
		public static ICollection<Vector3> IntersectionPointsWith(this AxisAlignedBox3 axisAlignedBox3, Line3 line3)
		{
			return line3.IntersectionPointsWith(axisAlignedBox3);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000C848 File Offset: 0x0000AA48
		public static bool Intersects(this AxisAlignedBox3 axisAlignedBox3, Plane3 plane3)
		{
			return plane3.Intersects(axisAlignedBox3);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000C851 File Offset: 0x0000AA51
		public static bool Intersects(this AxisAlignedBox3 axisAlignedBox3, Ray3 ray)
		{
			return ray.Intersects(axisAlignedBox3);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000C85A File Offset: 0x0000AA5A
		public static Segment3? IntersectionWith(this AxisAlignedBox3 axisAlignedBox3, Ray3 ray3)
		{
			return ray3.IntersectionWith(axisAlignedBox3);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000C863 File Offset: 0x0000AA63
		public static ICollection<Vector3> IntersectionPointsWith(this AxisAlignedBox3 axisAlignedBox3, Ray3 ray3)
		{
			return ray3.IntersectionPointsWith(axisAlignedBox3);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000C86C File Offset: 0x0000AA6C
		public static bool Intersects(this AxisAlignedBox3 axisAlignedBox3, Segment3 segment3)
		{
			return segment3.Intersects(axisAlignedBox3);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000C875 File Offset: 0x0000AA75
		public static Segment3? IntersectionWith(this AxisAlignedBox3 axisAlignedBox3, Segment3 segment3)
		{
			return segment3.IntersectionWith(axisAlignedBox3);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000C87E File Offset: 0x0000AA7E
		public static ICollection<Vector3> IntersectionPointsWith(this AxisAlignedBox3 axisAlignedBox3, Segment3 segment3)
		{
			return segment3.IntersectionPointsWith(axisAlignedBox3);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000C887 File Offset: 0x0000AA87
		public static bool Intersects(this AxisAlignedBox3 axisAlignedBox3, Triangle3 triangle3)
		{
			return triangle3.Intersects(axisAlignedBox3);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000C890 File Offset: 0x0000AA90
		public static ICollection<Vector3> IntersectionPointsWith(this AxisAlignedBox3 axisAlignedBox3, Triangle3 triangle3)
		{
			return triangle3.IntersectionPointsWith(axisAlignedBox3);
		}
	}
}
