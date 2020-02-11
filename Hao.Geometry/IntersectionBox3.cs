using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x02000051 RID: 81
	public static class IntersectionBox3
	{
		// Token: 0x0600032B RID: 811 RVA: 0x0000D110 File Offset: 0x0000B310
		public static bool Intersects(this Box3 box3, Box3 other)
		{
			IntersectionBox3Box3 intersectionBox3Box = new IntersectionBox3Box3(box3, other);
			return intersectionBox3Box.Test();
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000D12D File Offset: 0x0000B32D
		public static bool Intersects(this Box3 box3, Line3 line3)
		{
			return line3.Intersects(box3);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000D136 File Offset: 0x0000B336
		public static Segment3? IntersectionWith(this Box3 box3, Line3 line3)
		{
			return line3.IntersectionWith(box3);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000D13F File Offset: 0x0000B33F
		public static ICollection<Vector3> IntersectionPointsWith(this Box3 box3, Line3 line3)
		{
			return line3.IntersectionPointsWith(box3);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000D148 File Offset: 0x0000B348
		public static bool Intersects(this Box3 box3, Plane3 plane3)
		{
			return plane3.Intersects(box3);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000D151 File Offset: 0x0000B351
		public static bool Intersects(this Box3 box3, Ray3 ray3)
		{
			return ray3.Intersects(box3);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000D15A File Offset: 0x0000B35A
		public static Segment3? IntersectionWith(this Box3 box3, Ray3 ray3)
		{
			return ray3.IntersectionWith(box3);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000D163 File Offset: 0x0000B363
		public static ICollection<Vector3> IntersectionPointsWith(this Box3 box3, Ray3 ray3)
		{
			return ray3.IntersectionPointsWith(box3);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000D16C File Offset: 0x0000B36C
		public static bool Intersects(this Box3 box3, Segment3 segment3)
		{
			return segment3.Intersects(box3);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000D175 File Offset: 0x0000B375
		public static Segment3? IntersectionWith(this Box3 box3, Segment3 segment3)
		{
			return segment3.IntersectionWith(box3);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000D17E File Offset: 0x0000B37E
		public static ICollection<Vector3> IntersectionPointsWith(this Box3 box3, Segment3 segment3)
		{
			return segment3.IntersectionPointsWith(box3);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000D187 File Offset: 0x0000B387
		public static bool Intersects(this Box3 box3, Triangle3 triangle3)
		{
			return triangle3.Intersects(box3);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000D190 File Offset: 0x0000B390
		public static ICollection<Vector3> IntersectionPointsWith(this Box3 box3, Triangle3 triangle3)
		{
			return triangle3.IntersectionPointsWith(box3);
		}
	}
}
