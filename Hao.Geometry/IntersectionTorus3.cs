using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x02000039 RID: 57
	public static class IntersectionTorus3
	{
		// Token: 0x0600025D RID: 605 RVA: 0x0000A3E7 File Offset: 0x000085E7
		public static bool Intersects(this Torus3 torus, Line3 line)
		{
			return line.Intersects(torus);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A3F0 File Offset: 0x000085F0
		public static ICollection<Vector3> IntersectionPointsWith(this Torus3 torus, Line3 line)
		{
			return line.IntersectionPointsWith(torus);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A3F9 File Offset: 0x000085F9
		public static bool Intersects(this Torus3 torus, Ray3 ray)
		{
			return ray.Intersects(torus);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000A402 File Offset: 0x00008602
		public static ICollection<Vector3> IntersectionPointsWith(this Torus3 torus, Ray3 ray)
		{
			return ray.IntersectionPointsWith(torus);
		}
	}
}
