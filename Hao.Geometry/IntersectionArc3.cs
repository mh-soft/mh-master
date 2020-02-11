using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x02000036 RID: 54
	public static class IntersectionArc3
	{
		// Token: 0x06000253 RID: 595 RVA: 0x0000A245 File Offset: 0x00008445
		public static bool Intersects(this Arc3 arc, Plane3 plane)
		{
			return plane.Intersects(arc);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000A24E File Offset: 0x0000844E
		public static ICollection<Vector3> IntersectionPointsWith(this Arc3 arc, Plane3 plane)
		{
			return plane.IntersectionPointsWith(arc);
		}
	}
}
