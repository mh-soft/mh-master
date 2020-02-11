using System;

namespace Hao.Geometry
{
	// Token: 0x0200002F RID: 47
	public static class DistanceVector3
	{
		// Token: 0x060001FE RID: 510 RVA: 0x00009080 File Offset: 0x00007280
		public static double DistanceTo(this Vector3 vector3, Vector3 other)
		{
			return (other - vector3).Length;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000909C File Offset: 0x0000729C
		public static Segment3 ShortestSegmentTo(this Vector3 vector3, Vector3 other)
		{
			return new Segment3(vector3, other);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000090A8 File Offset: 0x000072A8
		public static double DistanceTo(this Vector3 vector3, Line3 line3)
		{
			Vector3 vector4 = vector3 - line3.Origin;
			double scalar = line3.Direction.Dot(vector4);
			return (line3.Origin + scalar * line3.Direction - vector3).Length;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000090FC File Offset: 0x000072FC
		public static Segment3 ShortestSegmentTo(this Vector3 vector3, Line3 line3)
		{
			Vector3 vector4 = vector3 - line3.Origin;
			double scalar = line3.Direction.Dot(vector4);
			Vector3 positiveEnd = line3.Origin + scalar * line3.Direction;
			return new Segment3(vector3, positiveEnd);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000914C File Offset: 0x0000734C
		public static double DistanceTo(this Vector3 vector3, Box3 box3)
		{
			DistanceVector3Box3 distanceVector3Box = new DistanceVector3Box3(vector3, box3);
			return distanceVector3Box.Distance;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000916C File Offset: 0x0000736C
		public static Segment3 ShortestSegmentTo(this Vector3 vector3, Box3 box3)
		{
			DistanceVector3Box3 distanceVector3Box = new DistanceVector3Box3(vector3, box3);
			return new Segment3(distanceVector3Box.ClosestPointOnVector, distanceVector3Box.ClosestPointOnBox);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00009198 File Offset: 0x00007398
		public static double DistanceTo(this Vector3 vector3, AxisAlignedBox3 axisAlignedBox3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceVector3Box3 distanceVector3Box = new DistanceVector3Box3(vector3, box);
			return distanceVector3Box.Distance;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000091E8 File Offset: 0x000073E8
		public static Segment3 ShortestSegmentTo(this Vector3 vector3, AxisAlignedBox3 axisAlignedBox3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceVector3Box3 distanceVector3Box = new DistanceVector3Box3(vector3, box);
			return new Segment3(distanceVector3Box.ClosestPointOnVector, distanceVector3Box.ClosestPointOnBox);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00009244 File Offset: 0x00007444
		public static double DistanceTo(this Vector3 vector3, Triangle3 triangle3)
		{
			DistanceVector3Triangle3 distanceVector3Triangle = new DistanceVector3Triangle3(vector3, triangle3);
			return distanceVector3Triangle.Distance;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00009264 File Offset: 0x00007464
		public static Segment3 ShortestSegmentTo(this Vector3 vector3, Triangle3 triangle3)
		{
			DistanceVector3Triangle3 distanceVector3Triangle = new DistanceVector3Triangle3(vector3, triangle3);
			return new Segment3(distanceVector3Triangle.ClosestPointOnVector, distanceVector3Triangle.ClosestPointOnTriangle);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00009290 File Offset: 0x00007490
		public static double DistanceTo(this Vector3 vector3, Segment3 segment3)
		{
			DistanceVector3Segment3 distanceVector3Segment = new DistanceVector3Segment3(vector3, segment3);
			return distanceVector3Segment.Distance;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000092B0 File Offset: 0x000074B0
		public static Segment3 ShortestSegmentTo(this Vector3 vector3, Segment3 segment3)
		{
			DistanceVector3Segment3 distanceVector3Segment = new DistanceVector3Segment3(vector3, segment3);
			return new Segment3(distanceVector3Segment.ClosestPointOnVector, distanceVector3Segment.ClosestPointOnSegment);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000092DC File Offset: 0x000074DC
		public static double DistanceTo(this Vector3 vector3, Plane3 plane3)
		{
			DistanceVector3Plane3 distanceVector3Plane = new DistanceVector3Plane3(vector3, plane3);
			return distanceVector3Plane.Distance;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000092FC File Offset: 0x000074FC
		public static double SignedDistanceTo(this Vector3 vector3, Plane3 plane3)
		{
			DistanceVector3Plane3 distanceVector3Plane = new DistanceVector3Plane3(vector3, plane3);
			return distanceVector3Plane.SignedDistance;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000931C File Offset: 0x0000751C
		public static Segment3 ShortestSegmentTo(this Vector3 vector3, Plane3 plane3)
		{
			DistanceVector3Plane3 distanceVector3Plane = new DistanceVector3Plane3(vector3, plane3);
			return new Segment3(distanceVector3Plane.ClosestPointOnVector, distanceVector3Plane.ClosestPointOnPlane);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00009345 File Offset: 0x00007545
		public static double DistanceTo(this Vector3 vector3, Ray3 ray3)
		{
			return ray3.DistanceTo(vector3);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00009350 File Offset: 0x00007550
		public static Segment3 ShortestSegmentTo(this Vector3 vector3, Ray3 ray3)
		{
			Vector3 vector4 = vector3 - ray3.Origin;
			double num = ray3.Direction.Dot(vector4);
			if (num > 0.0)
			{
				Vector3 positiveEnd = ray3.Origin + num * ray3.Direction;
				return new Segment3(vector3, positiveEnd);
			}
			return new Segment3(vector3, ray3.Origin);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000093B8 File Offset: 0x000075B8
		public static double DistanceTo(this Vector3 vector3, Rectangle3 rectangle3)
		{
			DistanceVector3Rectangle3 distanceVector3Rectangle = new DistanceVector3Rectangle3(vector3, rectangle3);
			return distanceVector3Rectangle.Distance;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000093D8 File Offset: 0x000075D8
		public static Segment3 ShortestSegmentTo(this Vector3 vector3, Rectangle3 rectangle3)
		{
			DistanceVector3Rectangle3 distanceVector3Rectangle = new DistanceVector3Rectangle3(vector3, rectangle3);
			return new Segment3(distanceVector3Rectangle.ClosestPointOnVector, distanceVector3Rectangle.ClosestPointOnRectangle);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00009404 File Offset: 0x00007604
		public static double DistanceTo(this Vector3 vector3, Circle3 circle3)
		{
			DistanceVector3Circle3 distanceVector3Circle = new DistanceVector3Circle3(vector3, circle3);
			return distanceVector3Circle.Distance;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00009424 File Offset: 0x00007624
		public static Segment3 ShortestSegmentTo(this Vector3 vector3, Circle3 circle3)
		{
			DistanceVector3Circle3 distanceVector3Circle = new DistanceVector3Circle3(vector3, circle3);
			return new Segment3(distanceVector3Circle.ClosestPointOnVector, distanceVector3Circle.ClosestPointOnCircle);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00009450 File Offset: 0x00007650
		public static double DistanceTo(this Vector3 vector3, Arc3 arc3)
		{
			DistanceVector3Arc3 distanceVector3Arc = new DistanceVector3Arc3(vector3, arc3);
			return distanceVector3Arc.Distance;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00009470 File Offset: 0x00007670
		public static Segment3 ShortestSegmentTo(this Vector3 vector3, Arc3 arc3)
		{
			DistanceVector3Arc3 distanceVector3Arc = new DistanceVector3Arc3(vector3, arc3);
			return new Segment3(distanceVector3Arc.ClosestPointOnVector, distanceVector3Arc.ClosestPointOnArc);
		}
	}
}
