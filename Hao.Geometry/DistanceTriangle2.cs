using System;

namespace Hao.Geometry
{
	// Token: 0x02000004 RID: 4
	public static class DistanceTriangle2
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002497 File Offset: 0x00000697
		public static double DistanceTo(this Triangle2 triangle, Vector2 vector)
		{
			return vector.DistanceTo(triangle);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024A0 File Offset: 0x000006A0
		public static Vector2 ClosestPointTo(this Triangle2 triangle, Vector2 vector)
		{
			DistanceVector2Triangle2 distanceVector2Triangle = new DistanceVector2Triangle2(vector, triangle);
			return distanceVector2Triangle.ClosestPointOnTriangle;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024C0 File Offset: 0x000006C0
		public static Segment2 ShortestSegmentTo(this Triangle2 triangle, Vector2 vector)
		{
			DistanceVector2Triangle2 distanceVector2Triangle = new DistanceVector2Triangle2(vector, triangle);
			return new Segment2(distanceVector2Triangle.ClosestPointOnTriangle, distanceVector2Triangle.ClosestPointOnVector);
		}
	}
}
