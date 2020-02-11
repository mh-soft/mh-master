using System;

namespace Hao.Geometry
{
	// Token: 0x0200002C RID: 44
	public static class DistanceTriangle3
	{
		// Token: 0x060001DA RID: 474 RVA: 0x00008B24 File Offset: 0x00006D24
		public static double DistanceTo(this Triangle3 triangle3, Vector3 vector3)
		{
			return vector3.DistanceTo(triangle3);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008B30 File Offset: 0x00006D30
		public static Vector3 ClosestPointTo(this Triangle3 triangle3, Vector3 vector3)
		{
			DistanceVector3Triangle3 distanceVector3Triangle = new DistanceVector3Triangle3(vector3, triangle3);
			return distanceVector3Triangle.ClosestPointOnTriangle;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008B50 File Offset: 0x00006D50
		public static Segment3 ShortestSegmentTo(this Triangle3 triangle3, Vector3 vector3)
		{
			DistanceVector3Triangle3 distanceVector3Triangle = new DistanceVector3Triangle3(vector3, triangle3);
			return new Segment3(distanceVector3Triangle.ClosestPointOnTriangle, distanceVector3Triangle.ClosestPointOnVector);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008B79 File Offset: 0x00006D79
		public static double DistanceTo(this Triangle3 triangle3, Line3 line3)
		{
			return line3.DistanceTo(triangle3);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00008B84 File Offset: 0x00006D84
		public static Vector3 ClosestPointTo(this Triangle3 triangle3, Line3 line3)
		{
			DistanceLine3Triangle3 distanceLine3Triangle = new DistanceLine3Triangle3(line3, triangle3);
			return distanceLine3Triangle.ClosestPointOnTriangle;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008BA4 File Offset: 0x00006DA4
		public static Segment3 ShortestSegmentTo(this Triangle3 triangle3, Line3 line3)
		{
			DistanceLine3Triangle3 distanceLine3Triangle = new DistanceLine3Triangle3(line3, triangle3);
			return new Segment3(distanceLine3Triangle.ClosestPointOnTriangle, distanceLine3Triangle.ClosestPointOnLine);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00008BCD File Offset: 0x00006DCD
		public static double DistanceTo(this Triangle3 triangle3, Ray3 ray3)
		{
			return ray3.DistanceTo(triangle3);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008BD8 File Offset: 0x00006DD8
		public static Vector3 ClosestPointTo(this Triangle3 triangle3, Ray3 ray3)
		{
			DistanceRay3Triangle3 distanceRay3Triangle = new DistanceRay3Triangle3(ray3, triangle3);
			return distanceRay3Triangle.ClosestPointOnTriangle;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00008BF8 File Offset: 0x00006DF8
		public static Segment3 ShortestSegmentTo(this Triangle3 triangle3, Ray3 ray3)
		{
			DistanceRay3Triangle3 distanceRay3Triangle = new DistanceRay3Triangle3(ray3, triangle3);
			return new Segment3(distanceRay3Triangle.ClosestPointOnTriangle, distanceRay3Triangle.ClosestPointOnRay);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008C24 File Offset: 0x00006E24
		public static double DistanceTo(this Triangle3 triangle3, Rectangle3 rectangle3)
		{
			DistanceTriangle3Rectangle3 distanceTriangle3Rectangle = new DistanceTriangle3Rectangle3(triangle3, rectangle3);
			return distanceTriangle3Rectangle.Distance;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008C44 File Offset: 0x00006E44
		public static Vector3 ClosestPointTo(this Triangle3 triangle3, Rectangle3 rectangle3)
		{
			DistanceTriangle3Rectangle3 distanceTriangle3Rectangle = new DistanceTriangle3Rectangle3(triangle3, rectangle3);
			return distanceTriangle3Rectangle.ClosestPointOnTriangle;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008C64 File Offset: 0x00006E64
		public static Segment3 ShortestSegmentTo(this Triangle3 triangle3, Rectangle3 rectangle3)
		{
			DistanceTriangle3Rectangle3 distanceTriangle3Rectangle = new DistanceTriangle3Rectangle3(triangle3, rectangle3);
			return new Segment3(distanceTriangle3Rectangle.ClosestPointOnTriangle, distanceTriangle3Rectangle.ClosestPointOnRectangle);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00008C90 File Offset: 0x00006E90
		public static double DistanceTo(this Triangle3 triangle3, Triangle3 other)
		{
			DistanceTriangle3Triangle3 distanceTriangle3Triangle = new DistanceTriangle3Triangle3(triangle3, other);
			return distanceTriangle3Triangle.Distance;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00008CB0 File Offset: 0x00006EB0
		public static Vector3 ClosestPointTo(this Triangle3 triangle3, Triangle3 other)
		{
			DistanceTriangle3Triangle3 distanceTriangle3Triangle = new DistanceTriangle3Triangle3(triangle3, other);
			return distanceTriangle3Triangle.ClosestPoint0;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00008CD0 File Offset: 0x00006ED0
		public static Segment3 ShortestSegmentTo(this Triangle3 triangle3, Triangle3 other)
		{
			DistanceTriangle3Triangle3 distanceTriangle3Triangle = new DistanceTriangle3Triangle3(triangle3, other);
			return new Segment3(distanceTriangle3Triangle.ClosestPoint0, distanceTriangle3Triangle.ClosestPoint1);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008CF9 File Offset: 0x00006EF9
		public static double DistanceTo(this Triangle3 triangle3, Segment3 segment3)
		{
			return segment3.DistanceTo(triangle3);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008D04 File Offset: 0x00006F04
		public static Vector3 ClosestPointTo(this Triangle3 triangle3, Segment3 segment3)
		{
			DistanceSegment3Triangle3 distanceSegment3Triangle = new DistanceSegment3Triangle3(segment3, triangle3);
			return distanceSegment3Triangle.ClosestPointOnTriangle;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00008D24 File Offset: 0x00006F24
		public static Segment3 ShortestSegmentTo(this Triangle3 triangle3, Segment3 segment3)
		{
			DistanceSegment3Triangle3 distanceSegment3Triangle = new DistanceSegment3Triangle3(segment3, triangle3);
			return new Segment3(distanceSegment3Triangle.ClosestPointOnTriangle, distanceSegment3Triangle.ClosestPointOnSegment);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00008D50 File Offset: 0x00006F50
		public static double DistanceTo(this Triangle3 triangle3, Plane3 plane3)
		{
			DistancePlane3Triangle3 distancePlane3Triangle = new DistancePlane3Triangle3(plane3, triangle3);
			return distancePlane3Triangle.Distance;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00008D70 File Offset: 0x00006F70
		public static double SignedDistanceTo(this Triangle3 triangle3, Plane3 plane3)
		{
			DistancePlane3Triangle3 distancePlane3Triangle = new DistancePlane3Triangle3(plane3, triangle3);
			return distancePlane3Triangle.SignedDistance;
		}
	}
}
