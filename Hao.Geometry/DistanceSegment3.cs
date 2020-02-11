using System;

namespace Hao.Geometry
{
	// Token: 0x02000027 RID: 39
	public static class DistanceSegment3
	{
		// Token: 0x0600018B RID: 395 RVA: 0x000076F5 File Offset: 0x000058F5
		public static double DistanceTo(this Segment3 segment3, Vector3 vector3)
		{
			return vector3.DistanceTo(segment3);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007700 File Offset: 0x00005900
		public static Vector3 ClosestPointTo(this Segment3 segment3, Vector3 vector3)
		{
			DistanceVector3Segment3 distanceVector3Segment = new DistanceVector3Segment3(vector3, segment3);
			return distanceVector3Segment.ClosestPointOnSegment;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007720 File Offset: 0x00005920
		public static Segment3 ShortestSegmentTo(this Segment3 segment3, Vector3 vector3)
		{
			DistanceVector3Segment3 distanceVector3Segment = new DistanceVector3Segment3(vector3, segment3);
			return new Segment3(distanceVector3Segment.ClosestPointOnSegment, distanceVector3Segment.ClosestPointOnVector);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007749 File Offset: 0x00005949
		public static double DistanceTo(this Segment3 segment3, Line3 line3)
		{
			return line3.DistanceTo(segment3);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007754 File Offset: 0x00005954
		public static Vector3 ClosestPointTo(this Segment3 segment3, Line3 line3)
		{
			DistanceLine3Segment3 distanceLine3Segment = new DistanceLine3Segment3(line3, segment3);
			return distanceLine3Segment.ClosestPointOnSegment;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007774 File Offset: 0x00005974
		public static Segment3 ShortestSegmentTo(this Segment3 segment3, Line3 line3)
		{
			DistanceLine3Segment3 distanceLine3Segment = new DistanceLine3Segment3(line3, segment3);
			return new Segment3(distanceLine3Segment.ClosestPointOnSegment, distanceLine3Segment.ClosestPointOnLine);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000779D File Offset: 0x0000599D
		public static double DistanceTo(this Segment3 segment3, Ray3 ray3)
		{
			return ray3.DistanceTo(segment3);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000077A8 File Offset: 0x000059A8
		public static Vector3 ClosestPointTo(this Segment3 segment3, Ray3 ray3)
		{
			DistanceRay3Segment3 distanceRay3Segment = new DistanceRay3Segment3(ray3, segment3);
			return distanceRay3Segment.ClosestPointOnSegment;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000077C8 File Offset: 0x000059C8
		public static Segment3 ShortestSegmentTo(this Segment3 segment3, Ray3 ray3)
		{
			DistanceRay3Segment3 distanceRay3Segment = new DistanceRay3Segment3(ray3, segment3);
			return new Segment3(distanceRay3Segment.ClosestPointOnSegment, distanceRay3Segment.ClosestPointOnRay);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000077F4 File Offset: 0x000059F4
		public static double DistanceTo(this Segment3 segment3, Rectangle3 rectangle3)
		{
			DistanceSegment3Rectangle3 distanceSegment3Rectangle = new DistanceSegment3Rectangle3(segment3, rectangle3);
			return distanceSegment3Rectangle.Distance;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007814 File Offset: 0x00005A14
		public static Vector3 ClosestPointTo(this Segment3 segment3, Rectangle3 rectangle3)
		{
			DistanceSegment3Rectangle3 distanceSegment3Rectangle = new DistanceSegment3Rectangle3(segment3, rectangle3);
			return distanceSegment3Rectangle.ClosestPointOnSegment;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00007834 File Offset: 0x00005A34
		public static Segment3 ShortestSegmentTo(this Segment3 segment3, Rectangle3 rectangle3)
		{
			DistanceSegment3Rectangle3 distanceSegment3Rectangle = new DistanceSegment3Rectangle3(segment3, rectangle3);
			return new Segment3(distanceSegment3Rectangle.ClosestPointOnSegment, distanceSegment3Rectangle.ClosestPointOnRectangle);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007860 File Offset: 0x00005A60
		public static double DistanceTo(this Segment3 segment3, Triangle3 triangle3)
		{
			DistanceSegment3Triangle3 distanceSegment3Triangle = new DistanceSegment3Triangle3(segment3, triangle3);
			return distanceSegment3Triangle.Distance;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007880 File Offset: 0x00005A80
		public static Vector3 ClosestPointTo(this Segment3 segment3, Triangle3 triangle3)
		{
			DistanceSegment3Triangle3 distanceSegment3Triangle = new DistanceSegment3Triangle3(segment3, triangle3);
			return distanceSegment3Triangle.ClosestPointOnSegment;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000078A0 File Offset: 0x00005AA0
		public static Segment3 ShortestSegmentTo(this Segment3 segment3, Triangle3 triangle3)
		{
			DistanceSegment3Triangle3 distanceSegment3Triangle = new DistanceSegment3Triangle3(segment3, triangle3);
			return new Segment3(distanceSegment3Triangle.ClosestPointOnSegment, distanceSegment3Triangle.ClosestPointOnTriangle);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000078CC File Offset: 0x00005ACC
		public static double DistanceTo(this Segment3 segment3, Box3 box3)
		{
			DistanceSegment3Box3 distanceSegment3Box = new DistanceSegment3Box3(segment3, box3);
			return distanceSegment3Box.Distance;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000078EC File Offset: 0x00005AEC
		public static Vector3 ClosestPointTo(this Segment3 segment3, Box3 box3)
		{
			DistanceSegment3Box3 distanceSegment3Box = new DistanceSegment3Box3(segment3, box3);
			return distanceSegment3Box.ClosestPointOnSegment;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000790C File Offset: 0x00005B0C
		public static Segment3 ShortestSegmentTo(this Segment3 segment3, Box3 box3)
		{
			DistanceSegment3Box3 distanceSegment3Box = new DistanceSegment3Box3(segment3, box3);
			return new Segment3(distanceSegment3Box.ClosestPointOnSegment, distanceSegment3Box.ClosestPointOnBox);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007938 File Offset: 0x00005B38
		public static double DistanceTo(this Segment3 segment3, AxisAlignedBox3 axisAlignedBox3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceSegment3Box3 distanceSegment3Box = new DistanceSegment3Box3(segment3, box);
			return distanceSegment3Box.Distance;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007988 File Offset: 0x00005B88
		public static Vector3 ClosestPointTo(this Segment3 segment3, AxisAlignedBox3 axisAlignedBox3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceSegment3Box3 distanceSegment3Box = new DistanceSegment3Box3(segment3, box);
			return distanceSegment3Box.ClosestPointOnSegment;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000079D8 File Offset: 0x00005BD8
		public static Segment3 ShortestSegmentTo(this Segment3 segment3, AxisAlignedBox3 axisAlignedBox3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceSegment3Box3 distanceSegment3Box = new DistanceSegment3Box3(segment3, box);
			return new Segment3(distanceSegment3Box.ClosestPointOnSegment, distanceSegment3Box.ClosestPointOnBox);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00007A34 File Offset: 0x00005C34
		public static double DistanceTo(this Segment3 segment3, Segment3 other)
		{
			DistanceSegment3Segment3 distanceSegment3Segment = new DistanceSegment3Segment3(segment3, other);
			return distanceSegment3Segment.Distance;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007A54 File Offset: 0x00005C54
		public static Vector3 ClosestPointTo(this Segment3 segment3, Segment3 other)
		{
			DistanceSegment3Segment3 distanceSegment3Segment = new DistanceSegment3Segment3(segment3, other);
			return distanceSegment3Segment.ClosestPoint0;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00007A74 File Offset: 0x00005C74
		public static Segment3 ShortestSegmentTo(this Segment3 segment3, Segment3 other)
		{
			DistanceSegment3Segment3 distanceSegment3Segment = new DistanceSegment3Segment3(segment3, other);
			return new Segment3(distanceSegment3Segment.ClosestPoint0, distanceSegment3Segment.ClosestPoint1);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007A9D File Offset: 0x00005C9D
		public static double DistanceTo(this Segment3 segment3, Arc3 arc3)
		{
			return arc3.DistanceTo(segment3);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00007AA8 File Offset: 0x00005CA8
		public static Vector3 ClosestPointTo(this Segment3 segment3, Arc3 arc3)
		{
			Line3 line = new Line3(segment3.NegativeEnd, segment3.Direction);
			DistanceLine3Arc3 distanceLine3Arc = new DistanceLine3Arc3(line, arc3);
			if (distanceLine3Arc.LineParameter < 0.0)
			{
				return segment3.NegativeEnd;
			}
			Line3 line2 = new Line3(segment3.PositiveEnd, -segment3.Direction);
			DistanceLine3Arc3 distanceLine3Arc2 = new DistanceLine3Arc3(line2, arc3);
			if (distanceLine3Arc2.LineParameter < 0.0)
			{
				return segment3.PositiveEnd;
			}
			return distanceLine3Arc2.ClosestPointOnLine;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00007B34 File Offset: 0x00005D34
		public static Segment3 ShortestSegmentTo(this Segment3 segment3, Arc3 arc3)
		{
			Segment3 segment4 = arc3.ShortestSegmentTo(segment3);
			return new Segment3(segment4.PositiveEnd, segment4.NegativeEnd);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007B5C File Offset: 0x00005D5C
		public static double DistanceTo(this Segment3 segment3, Plane3 plane3)
		{
			DistancePlane3Segment3 distancePlane3Segment = new DistancePlane3Segment3(plane3, segment3);
			return distancePlane3Segment.Distance;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007B7C File Offset: 0x00005D7C
		public static double SignedDistanceTo(this Segment3 segment3, Plane3 plane3)
		{
			DistancePlane3Segment3 distancePlane3Segment = new DistancePlane3Segment3(plane3, segment3);
			return distancePlane3Segment.SignedDistance;
		}
	}
}
