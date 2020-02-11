using System;

namespace Hao.Geometry
{
	// Token: 0x02000025 RID: 37
	public static class DistanceRectangle3
	{
		// Token: 0x0600016F RID: 367 RVA: 0x0000726C File Offset: 0x0000546C
		public static double DistanceTo(this Rectangle3 rectangle, Vector3 vector)
		{
			return vector.DistanceTo(rectangle);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007278 File Offset: 0x00005478
		public static Vector3 ClosestPointTo(this Rectangle3 rectangle, Vector3 vector)
		{
			DistanceVector3Rectangle3 distanceVector3Rectangle = new DistanceVector3Rectangle3(vector, rectangle);
			return distanceVector3Rectangle.ClosestPointOnRectangle;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00007298 File Offset: 0x00005498
		public static Segment3 ShortestSegmentTo(this Rectangle3 rectangle, Vector3 vector)
		{
			DistanceVector3Rectangle3 distanceVector3Rectangle = new DistanceVector3Rectangle3(vector, rectangle);
			return new Segment3(distanceVector3Rectangle.ClosestPointOnRectangle, distanceVector3Rectangle.ClosestPointOnVector);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000072C1 File Offset: 0x000054C1
		public static double DistanceTo(this Rectangle3 rectangle, Segment3 segment)
		{
			return segment.DistanceTo(rectangle);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000072CC File Offset: 0x000054CC
		public static Vector3 ClosestPointTo(this Rectangle3 rectangle, Segment3 segment)
		{
			DistanceSegment3Rectangle3 distanceSegment3Rectangle = new DistanceSegment3Rectangle3(segment, rectangle);
			return distanceSegment3Rectangle.ClosestPointOnRectangle;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000072EC File Offset: 0x000054EC
		public static Segment3 ShortestSegmentTo(this Rectangle3 rectangle, Segment3 segment)
		{
			DistanceSegment3Rectangle3 distanceSegment3Rectangle = new DistanceSegment3Rectangle3(segment, rectangle);
			return new Segment3(distanceSegment3Rectangle.ClosestPointOnRectangle, distanceSegment3Rectangle.ClosestPointOnSegment);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007315 File Offset: 0x00005515
		public static double DistanceTo(this Rectangle3 rectangle, Line3 line)
		{
			return line.DistanceTo(rectangle);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007320 File Offset: 0x00005520
		public static Vector3 ClosestPointTo(this Rectangle3 rectangle, Line3 line)
		{
			DistanceLine3Rectangle3 distanceLine3Rectangle = new DistanceLine3Rectangle3(line, rectangle);
			return distanceLine3Rectangle.ClosestPointOnRectangle;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007340 File Offset: 0x00005540
		public static Segment3 ShortestSegmentTo(this Rectangle3 rectangle, Line3 line)
		{
			DistanceLine3Rectangle3 distanceLine3Rectangle = new DistanceLine3Rectangle3(line, rectangle);
			return new Segment3(distanceLine3Rectangle.ClosestPointOnRectangle, distanceLine3Rectangle.ClosestPointOnLine);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007369 File Offset: 0x00005569
		public static double DistanceTo(this Rectangle3 rectangle, Ray3 ray)
		{
			return ray.DistanceTo(rectangle);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007374 File Offset: 0x00005574
		public static Vector3 ClosestPointTo(this Rectangle3 rectangle, Ray3 ray)
		{
			DistanceRay3Rectangle3 distanceRay3Rectangle = new DistanceRay3Rectangle3(ray, rectangle);
			return distanceRay3Rectangle.ClosestPointOnRectangle;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007394 File Offset: 0x00005594
		public static Segment3 ShortestSegmentTo(this Rectangle3 rectangle, Ray3 ray)
		{
			DistanceRay3Rectangle3 distanceRay3Rectangle = new DistanceRay3Rectangle3(ray, rectangle);
			return new Segment3(distanceRay3Rectangle.ClosestPointOnRectangle, distanceRay3Rectangle.ClosestPointOnRay);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000073C0 File Offset: 0x000055C0
		public static double DistanceTo(this Rectangle3 rectangle, Rectangle3 other)
		{
			DistanceRectangle3Rectangle3 distanceRectangle3Rectangle = new DistanceRectangle3Rectangle3(rectangle, other);
			return distanceRectangle3Rectangle.Distance;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000073E0 File Offset: 0x000055E0
		public static Vector3 ClosestPointTo(this Rectangle3 rectangle, Rectangle3 other)
		{
			DistanceRectangle3Rectangle3 distanceRectangle3Rectangle = new DistanceRectangle3Rectangle3(rectangle, other);
			return distanceRectangle3Rectangle.ClosestPoint0;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007400 File Offset: 0x00005600
		public static Segment3 ShortestSegmentTo(this Rectangle3 rectangle, Rectangle3 other)
		{
			DistanceRectangle3Rectangle3 distanceRectangle3Rectangle = new DistanceRectangle3Rectangle3(rectangle, other);
			return new Segment3(distanceRectangle3Rectangle.ClosestPoint0, distanceRectangle3Rectangle.ClosestPoint1);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000742C File Offset: 0x0000562C
		public static double DistanceTo(this Rectangle3 rectangle, Triangle3 triangle)
		{
			DistanceTriangle3Rectangle3 distanceTriangle3Rectangle = new DistanceTriangle3Rectangle3(triangle, rectangle);
			return distanceTriangle3Rectangle.Distance;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000744C File Offset: 0x0000564C
		public static Vector3 ClosestPointTo(this Rectangle3 rectangle, Triangle3 triangle)
		{
			DistanceTriangle3Rectangle3 distanceTriangle3Rectangle = new DistanceTriangle3Rectangle3(triangle, rectangle);
			return distanceTriangle3Rectangle.ClosestPointOnRectangle;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000746C File Offset: 0x0000566C
		public static Segment3 ShortestSegmentTo(this Rectangle3 rectangle, Triangle3 triangle)
		{
			DistanceTriangle3Rectangle3 distanceTriangle3Rectangle = new DistanceTriangle3Rectangle3(triangle, rectangle);
			return new Segment3(distanceTriangle3Rectangle.ClosestPointOnRectangle, distanceTriangle3Rectangle.ClosestPointOnTriangle);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007498 File Offset: 0x00005698
		public static double DistanceTo(this Rectangle3 rectangle, Plane3 plane)
		{
			DistancePlane3Rectangle3 distancePlane3Rectangle = new DistancePlane3Rectangle3(plane, rectangle);
			return distancePlane3Rectangle.Distance;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000074B8 File Offset: 0x000056B8
		public static double SignedDistanceTo(this Rectangle3 rectangle, Plane3 plane)
		{
			DistancePlane3Rectangle3 distancePlane3Rectangle = new DistancePlane3Rectangle3(plane, rectangle);
			return distancePlane3Rectangle.SignedDistance;
		}
	}
}
