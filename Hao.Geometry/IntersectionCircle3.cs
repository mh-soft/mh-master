using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x02000037 RID: 55
	public static class IntersectionCircle3
	{
		// Token: 0x06000255 RID: 597 RVA: 0x0000A257 File Offset: 0x00008457
		public static bool Intersects(this Circle3 circle, Plane3 plane)
		{
			return plane.Intersects(circle);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000A260 File Offset: 0x00008460
		public static ICollection<Vector3> IntersectionPointsWith(this Circle3 circle, Plane3 plane)
		{
			return plane.IntersectionPointsWith(circle);
		}
	}
}
