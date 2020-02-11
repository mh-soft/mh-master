using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x0200003C RID: 60
	public static class IntersectionCylinder3
	{
		// Token: 0x06000271 RID: 625 RVA: 0x0000A5FD File Offset: 0x000087FD
		public static bool Intersects(this Cylinder3 cylinder, Line3 line)
		{
			return line.Intersects(cylinder);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A606 File Offset: 0x00008806
		public static Segment3? IntersectionWith(this Cylinder3 cylinder, Line3 line)
		{
			return line.IntersectionWith(cylinder);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A60F File Offset: 0x0000880F
		public static ICollection<Vector3> IntersectionPointsWith(this Cylinder3 cylinder, Line3 line)
		{
			return line.IntersectionPointsWith(cylinder);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000A618 File Offset: 0x00008818
		public static bool Intersects(this Cylinder3 cylinder, Ray3 ray)
		{
			return ray.Intersects(cylinder);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000A621 File Offset: 0x00008821
		public static Segment3? IntersectionWith(this Cylinder3 cylinder, Ray3 ray)
		{
			return ray.IntersectionWith(cylinder);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A62A File Offset: 0x0000882A
		public static ICollection<Vector3> IntersectionPointsWith(this Cylinder3 cylinder, Ray3 ray)
		{
			return ray.IntersectionPointsWith(cylinder);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A633 File Offset: 0x00008833
		public static bool Intersects(this Cylinder3 cylinder, Segment3 segment)
		{
			return segment.Intersects(cylinder);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000A63C File Offset: 0x0000883C
		public static Segment3? IntersectionWith(this Cylinder3 cylinder, Segment3 segment)
		{
			return segment.IntersectionWith(cylinder);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000A645 File Offset: 0x00008845
		public static ICollection<Vector3> IntersectionPointsWith(this Cylinder3 cylinder, Segment3 segment)
		{
			return segment.IntersectionPointsWith(cylinder);
		}
	}
}
