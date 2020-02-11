using System;

namespace Hao.Geometry
{
	// Token: 0x0200001E RID: 30
	public static class DistancePlane3
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00005D98 File Offset: 0x00003F98
		public static double DistanceTo(this Plane3 plane3, Vector3 vector3)
		{
			DistanceVector3Plane3 distanceVector3Plane = new DistanceVector3Plane3(vector3, plane3);
			return distanceVector3Plane.Distance;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005DB8 File Offset: 0x00003FB8
		public static double SignedDistanceTo(this Plane3 plane3, Vector3 vector3)
		{
			DistanceVector3Plane3 distanceVector3Plane = new DistanceVector3Plane3(vector3, plane3);
			return distanceVector3Plane.SignedDistance;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005DD8 File Offset: 0x00003FD8
		public static Vector3 ClosestPointTo(this Plane3 plane3, Vector3 vector3)
		{
			DistanceVector3Plane3 distanceVector3Plane = new DistanceVector3Plane3(vector3, plane3);
			return distanceVector3Plane.ClosestPointOnPlane;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005DF8 File Offset: 0x00003FF8
		public static Segment3 ShortestSegmentTo(this Plane3 plane3, Vector3 vector3)
		{
			DistanceVector3Plane3 distanceVector3Plane = new DistanceVector3Plane3(vector3, plane3);
			return new Segment3(distanceVector3Plane.ClosestPointOnPlane, distanceVector3Plane.ClosestPointOnVector);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005E24 File Offset: 0x00004024
		public static double DistanceTo(this Plane3 plane3, Segment3 segment3)
		{
			DistancePlane3Segment3 distancePlane3Segment = new DistancePlane3Segment3(plane3, segment3);
			return distancePlane3Segment.Distance;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005E44 File Offset: 0x00004044
		public static double SignedDistanceTo(this Plane3 plane3, Segment3 segment3)
		{
			DistancePlane3Segment3 distancePlane3Segment = new DistancePlane3Segment3(plane3, segment3);
			return distancePlane3Segment.SignedDistance;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005E64 File Offset: 0x00004064
		public static double DistanceTo(this Plane3 plane3, Ray3 ray3)
		{
			DistancePlane3Ray3 distancePlane3Ray = new DistancePlane3Ray3(plane3, ray3);
			return distancePlane3Ray.Distance;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005E84 File Offset: 0x00004084
		public static double SignedDistanceTo(this Plane3 plane3, Ray3 ray3)
		{
			DistancePlane3Ray3 distancePlane3Ray = new DistancePlane3Ray3(plane3, ray3);
			return distancePlane3Ray.SignedDistance;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005EA4 File Offset: 0x000040A4
		public static double DistanceTo(this Plane3 plane3, Line3 line3)
		{
			DistancePlane3Line3 distancePlane3Line = new DistancePlane3Line3(plane3, line3);
			return distancePlane3Line.Distance;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005EC4 File Offset: 0x000040C4
		public static double SignedDistanceTo(this Plane3 plane3, Line3 line3)
		{
			DistancePlane3Line3 distancePlane3Line = new DistancePlane3Line3(plane3, line3);
			return distancePlane3Line.SignedDistance;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005EE4 File Offset: 0x000040E4
		public static double DistanceTo(this Plane3 plane3, Triangle3 triangle3)
		{
			DistancePlane3Triangle3 distancePlane3Triangle = new DistancePlane3Triangle3(plane3, triangle3);
			return distancePlane3Triangle.Distance;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005F04 File Offset: 0x00004104
		public static double SignedDistanceTo(this Plane3 plane3, Triangle3 triangle3)
		{
			DistancePlane3Triangle3 distancePlane3Triangle = new DistancePlane3Triangle3(plane3, triangle3);
			return distancePlane3Triangle.SignedDistance;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005F24 File Offset: 0x00004124
		public static double DistanceTo(this Plane3 plane3, Rectangle3 rectangle3)
		{
			DistancePlane3Rectangle3 distancePlane3Rectangle = new DistancePlane3Rectangle3(plane3, rectangle3);
			return distancePlane3Rectangle.Distance;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005F44 File Offset: 0x00004144
		public static double SignedDistanceTo(this Plane3 plane3, Rectangle3 rectangle3)
		{
			DistancePlane3Rectangle3 distancePlane3Rectangle = new DistancePlane3Rectangle3(plane3, rectangle3);
			return distancePlane3Rectangle.SignedDistance;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005F64 File Offset: 0x00004164
		public static double DistanceTo(this Plane3 plane3, Circle3 circle3)
		{
			DistancePlane3Circle3 distancePlane3Circle = new DistancePlane3Circle3(plane3, circle3);
			return distancePlane3Circle.Distance;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005F84 File Offset: 0x00004184
		public static double SignedDistanceTo(this Plane3 plane3, Circle3 circle3)
		{
			DistancePlane3Circle3 distancePlane3Circle = new DistancePlane3Circle3(plane3, circle3);
			return distancePlane3Circle.SignedDistance;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005FA4 File Offset: 0x000041A4
		public static double DistanceTo(this Plane3 plane3, Arc3 arc3)
		{
			DistancePlane3Arc3 distancePlane3Arc = new DistancePlane3Arc3(plane3, arc3);
			return distancePlane3Arc.Distance;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005FC4 File Offset: 0x000041C4
		public static double SignedDistanceTo(this Plane3 plane3, Arc3 arc3)
		{
			DistancePlane3Arc3 distancePlane3Arc = new DistancePlane3Arc3(plane3, arc3);
			return distancePlane3Arc.SignedDistance;
		}
	}
}
