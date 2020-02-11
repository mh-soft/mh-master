using System;

namespace Hao.Geometry
{
	// Token: 0x02000006 RID: 6
	public static class DistanceVector2
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002540 File Offset: 0x00000740
		public static double DistanceTo(this Vector2 vector, Vector2 other)
		{
			return (other - vector).Length;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000255C File Offset: 0x0000075C
		public static Segment2 ShortestSegmentTo(this Vector2 vector2, Vector2 other)
		{
			return new Segment2(vector2, other);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002568 File Offset: 0x00000768
		public static double DistanceTo(this Vector2 vector, Line2 line)
		{
			DistanceVector2Line2 distanceVector2Line = new DistanceVector2Line2(vector, line);
			return distanceVector2Line.Distance;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002588 File Offset: 0x00000788
		public static Segment2 ShortestSegmentTo(this Vector2 vector, Line2 line)
		{
			DistanceVector2Line2 distanceVector2Line = new DistanceVector2Line2(vector, line);
			return new Segment2(distanceVector2Line.ClosestPointOnVector, distanceVector2Line.ClosestPointOnLine);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025B4 File Offset: 0x000007B4
		public static double DistanceTo(this Vector2 vector, Triangle2 triangle)
		{
			DistanceVector2Triangle2 distanceVector2Triangle = new DistanceVector2Triangle2(vector, triangle);
			return distanceVector2Triangle.Distance;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025D4 File Offset: 0x000007D4
		public static Segment2 ShortestSegmentTo(this Vector2 vector, Triangle2 triangle)
		{
			DistanceVector2Triangle2 distanceVector2Triangle = new DistanceVector2Triangle2(vector, triangle);
			return new Segment2(distanceVector2Triangle.ClosestPointOnVector, distanceVector2Triangle.ClosestPointOnTriangle);
		}
	}
}
