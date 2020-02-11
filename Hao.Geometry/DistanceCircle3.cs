using System;

namespace Hao.Geometry
{

	/// <summary>
	/// 
	/// </summary>
	public static class DistanceCircle3
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public static double DistanceTo(this Circle3 circle3, Line3 line3)
		{
			DistanceLine3Circle3 distanceLine3Circle = new DistanceLine3Circle3(line3, circle3);
			return distanceLine3Circle.Distance;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public static Vector3 ClosestPointTo(this Circle3 circle3, Line3 line3)
		{
			DistanceLine3Circle3 distanceLine3Circle = new DistanceLine3Circle3(line3, circle3);
			return distanceLine3Circle.ClosestPointOnCircle;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B08 File Offset: 0x00000D08
		public static Segment3 ShortestSegmentTo(this Circle3 circle3, Line3 line3)
		{
			DistanceLine3Circle3 distanceLine3Circle = new DistanceLine3Circle3(line3, circle3);
			return new Segment3(distanceLine3Circle.ClosestPointOnCircle, distanceLine3Circle.ClosestPointOnLine);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B31 File Offset: 0x00000D31
		public static double DistanceTo(this Circle3 circle3, Ray3 ray3)
		{
			return ray3.DistanceTo(circle3);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B3C File Offset: 0x00000D3C
		public static Vector3 ClosestPointTo(this Circle3 circle3, Ray3 ray3)
		{
			Line3 line = new Line3(ray3.Origin, ray3.Direction);
			DistanceLine3Circle3 distanceLine3Circle = new DistanceLine3Circle3(line, circle3);
			if (distanceLine3Circle.LineParameter < 0.0)
			{
				return circle3.ClosestPointTo(ray3.Origin);
			}
			return distanceLine3Circle.ClosestPointOnCircle;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B90 File Offset: 0x00000D90
		public static Segment3 ShortestSegmentTo(this Circle3 circle3, Ray3 ray3)
		{
			Line3 line = new Line3(ray3.Origin, ray3.Direction);
			DistanceLine3Circle3 distanceLine3Circle = new DistanceLine3Circle3(line, circle3);
			if (distanceLine3Circle.LineParameter < 0.0)
			{
				return circle3.ShortestSegmentTo(ray3.Origin);
			}
			return new Segment3(distanceLine3Circle.ClosestPointOnCircle, distanceLine3Circle.ClosestPointOnLine);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public static double DistanceTo(this Circle3 circle3, Vector3 vector3)
		{
			DistanceVector3Circle3 distanceVector3Circle = new DistanceVector3Circle3(vector3, circle3);
			return distanceVector3Circle.Distance;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C10 File Offset: 0x00000E10
		public static Vector3 ClosestPointTo(this Circle3 circle3, Vector3 vector3)
		{
			DistanceVector3Circle3 distanceVector3Circle = new DistanceVector3Circle3(vector3, circle3);
			return distanceVector3Circle.ClosestPointOnCircle;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002C30 File Offset: 0x00000E30
		public static Segment3 ShortestSegmentTo(this Circle3 circle3, Vector3 vector3)
		{
			DistanceVector3Circle3 distanceVector3Circle = new DistanceVector3Circle3(vector3, circle3);
			return new Segment3(distanceVector3Circle.ClosestPointOnCircle, distanceVector3Circle.ClosestPointOnVector);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002C5C File Offset: 0x00000E5C
		public static double DistanceTo(this Circle3 circle3, Plane3 plane3)
		{
			DistancePlane3Circle3 distancePlane3Circle = new DistancePlane3Circle3(plane3, circle3);
			return distancePlane3Circle.Distance;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002C7C File Offset: 0x00000E7C
		public static double SignedDistanceTo(this Circle3 circle3, Plane3 plane3)
		{
			DistancePlane3Circle3 distancePlane3Circle = new DistancePlane3Circle3(plane3, circle3);
			return distancePlane3Circle.SignedDistance;
		}
	}
}
