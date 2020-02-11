using System;

namespace Hao.Geometry
{
	// Token: 0x0200001F RID: 31
	public static class DistanceRay3
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00005FE4 File Offset: 0x000041E4
		public static double DistanceTo(this Ray3 ray3, Vector3 vector3)
		{
			Vector3 vector4 = vector3 - ray3.Origin;
			double num = ray3.Direction.Dot(vector4);
			if (num > 0.0)
			{
				return (ray3.Origin + num * ray3.Direction - vector3).Length;
			}
			return (ray3.Origin - vector3).Length;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000605C File Offset: 0x0000425C
		public static Vector3 ClosestPointTo(this Ray3 ray3, Vector3 vector3)
		{
			Vector3 vector4 = vector3 - ray3.Origin;
			double num = ray3.Direction.Dot(vector4);
			if (num > 0.0)
			{
				return ray3.Origin + num * ray3.Direction;
			}
			return ray3.Origin;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000060B8 File Offset: 0x000042B8
		public static Segment3 ShortestSegmentTo(this Ray3 ray3, Vector3 vector3)
		{
			Vector3 vector4 = vector3 - ray3.Origin;
			double num = ray3.Direction.Dot(vector4);
			if (num > 0.0)
			{
				return new Segment3(ray3.Origin + num * ray3.Direction, vector3);
			}
			return new Segment3(ray3.Origin, vector3);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000611D File Offset: 0x0000431D
		public static double DistanceTo(this Ray3 ray3, Line3 line3)
		{
			return line3.DistanceTo(ray3);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006128 File Offset: 0x00004328
		public static Vector3 ClosestPointTo(this Ray3 ray3, Line3 line3)
		{
			DistanceLine3Ray3 distanceLine3Ray = new DistanceLine3Ray3(line3, ray3);
			return distanceLine3Ray.ClosestPointOnRay;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006148 File Offset: 0x00004348
		public static Segment3 ShortestSegmentTo(this Ray3 ray3, Line3 line3)
		{
			DistanceLine3Ray3 distanceLine3Ray = new DistanceLine3Ray3(line3, ray3);
			return new Segment3(distanceLine3Ray.ClosestPointOnRay, distanceLine3Ray.ClosestPointOnLine);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006174 File Offset: 0x00004374
		public static double DistanceTo(this Ray3 ray3, Box3 box3)
		{
			DistanceRay3Box3 distanceRay3Box = new DistanceRay3Box3(ray3, box3);
			return distanceRay3Box.Distance;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006194 File Offset: 0x00004394
		public static Vector3 ClosestPointTo(this Ray3 ray3, Box3 box3)
		{
			DistanceRay3Box3 distanceRay3Box = new DistanceRay3Box3(ray3, box3);
			return distanceRay3Box.ClosestPointOnRay;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000061B4 File Offset: 0x000043B4
		public static Segment3 ShortestSegmentTo(this Ray3 ray3, Box3 box3)
		{
			DistanceRay3Box3 distanceRay3Box = new DistanceRay3Box3(ray3, box3);
			return new Segment3(distanceRay3Box.ClosestPointOnRay, distanceRay3Box.ClosestPointOnBox);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000061E0 File Offset: 0x000043E0
		public static double DistanceTo(this Ray3 ray3, AxisAlignedBox3 axisAlignedBox3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceRay3Box3 distanceRay3Box = new DistanceRay3Box3(ray3, box);
			return distanceRay3Box.Distance;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006230 File Offset: 0x00004430
		public static Vector3 ClosestPointTo(this Ray3 ray3, AxisAlignedBox3 axisAlignedBox3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceRay3Box3 distanceRay3Box = new DistanceRay3Box3(ray3, box);
			return distanceRay3Box.ClosestPointOnRay;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006280 File Offset: 0x00004480
		public static Segment3 ShortestSegmentTo(this Ray3 ray3, AxisAlignedBox3 axisAlignedBox3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceRay3Box3 distanceRay3Box = new DistanceRay3Box3(ray3, box);
			return new Segment3(distanceRay3Box.ClosestPointOnRay, distanceRay3Box.ClosestPointOnBox);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000062DC File Offset: 0x000044DC
		public static double DistanceTo(this Ray3 ray3, Ray3 other)
		{
			DistanceRay3Ray3 distanceRay3Ray = new DistanceRay3Ray3(ray3, other);
			return distanceRay3Ray.Distance;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000062FC File Offset: 0x000044FC
		public static Vector3 ClosestPointTo(this Ray3 ray3, Ray3 other)
		{
			DistanceRay3Ray3 distanceRay3Ray = new DistanceRay3Ray3(ray3, other);
			return distanceRay3Ray.ClosestPoint0;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000631C File Offset: 0x0000451C
		public static Segment3 ShortestSegmentTo(this Ray3 ray3, Ray3 other)
		{
			DistanceRay3Ray3 distanceRay3Ray = new DistanceRay3Ray3(ray3, other);
			return new Segment3(distanceRay3Ray.ClosestPoint0, distanceRay3Ray.ClosestPoint1);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006348 File Offset: 0x00004548
		public static double DistanceTo(this Ray3 ray3, Rectangle3 rectangle3)
		{
			DistanceRay3Rectangle3 distanceRay3Rectangle = new DistanceRay3Rectangle3(ray3, rectangle3);
			return distanceRay3Rectangle.Distance;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006368 File Offset: 0x00004568
		public static Vector3 ClosestPointTo(this Ray3 ray3, Rectangle3 rectangle3)
		{
			DistanceRay3Rectangle3 distanceRay3Rectangle = new DistanceRay3Rectangle3(ray3, rectangle3);
			return distanceRay3Rectangle.ClosestPointOnRay;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006388 File Offset: 0x00004588
		public static Segment3 ShortestSegmentTo(this Ray3 ray3, Rectangle3 rectangle3)
		{
			DistanceRay3Rectangle3 distanceRay3Rectangle = new DistanceRay3Rectangle3(ray3, rectangle3);
			return new Segment3(distanceRay3Rectangle.ClosestPointOnRay, distanceRay3Rectangle.ClosestPointOnRectangle);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000063B4 File Offset: 0x000045B4
		public static double DistanceTo(this Ray3 ray3, Segment3 segment3)
		{
			DistanceRay3Segment3 distanceRay3Segment = new DistanceRay3Segment3(ray3, segment3);
			return distanceRay3Segment.Distance;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000063D4 File Offset: 0x000045D4
		public static Vector3 ClosestPointTo(this Ray3 ray3, Segment3 segment3)
		{
			DistanceRay3Segment3 distanceRay3Segment = new DistanceRay3Segment3(ray3, segment3);
			return distanceRay3Segment.ClosestPointOnRay;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000063F4 File Offset: 0x000045F4
		public static Segment3 ShortestSegmentTo(this Ray3 ray3, Segment3 segment3)
		{
			DistanceRay3Segment3 distanceRay3Segment = new DistanceRay3Segment3(ray3, segment3);
			return new Segment3(distanceRay3Segment.ClosestPointOnRay, distanceRay3Segment.ClosestPointOnSegment);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006420 File Offset: 0x00004620
		public static double DistanceTo(this Ray3 ray3, Triangle3 triangle3)
		{
			DistanceRay3Triangle3 distanceRay3Triangle = new DistanceRay3Triangle3(ray3, triangle3);
			return distanceRay3Triangle.Distance;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006440 File Offset: 0x00004640
		public static Vector3 ClosestPointTo(this Ray3 ray3, Triangle3 triangle3)
		{
			DistanceRay3Triangle3 distanceRay3Triangle = new DistanceRay3Triangle3(ray3, triangle3);
			return distanceRay3Triangle.ClosestPointOnRay;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006460 File Offset: 0x00004660
		public static Segment3 ShortestSegmentTo(this Ray3 ray3, Triangle3 triangle3)
		{
			DistanceRay3Triangle3 distanceRay3Triangle = new DistanceRay3Triangle3(ray3, triangle3);
			return new Segment3(distanceRay3Triangle.ClosestPointOnRay, distanceRay3Triangle.ClosestPointOnTriangle);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000648C File Offset: 0x0000468C
		public static double DistanceTo(this Ray3 ray3, Circle3 circle3)
		{
			Line3 line = new Line3(ray3.Origin, ray3.Direction);
			DistanceLine3Circle3 distanceLine3Circle = new DistanceLine3Circle3(line, circle3);
			if (distanceLine3Circle.LineParameter < 0.0)
			{
				return ray3.Origin.DistanceTo(circle3);
			}
			return distanceLine3Circle.Distance;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000064E0 File Offset: 0x000046E0
		public static Vector3 ClosestPointTo(this Ray3 ray3, Circle3 circle3)
		{
			Line3 line = new Line3(ray3.Origin, ray3.Direction);
			DistanceLine3Circle3 distanceLine3Circle = new DistanceLine3Circle3(line, circle3);
			if (distanceLine3Circle.LineParameter < 0.0)
			{
				return ray3.Origin;
			}
			return distanceLine3Circle.ClosestPointOnLine;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000652C File Offset: 0x0000472C
		public static Segment3 ShortestSegmentTo(this Ray3 ray3, Circle3 circle3)
		{
			Line3 line = new Line3(ray3.Origin, ray3.Direction);
			DistanceLine3Circle3 distanceLine3Circle = new DistanceLine3Circle3(line, circle3);
			if (distanceLine3Circle.LineParameter < 0.0)
			{
				return ray3.Origin.ShortestSegmentTo(circle3);
			}
			return new Segment3(distanceLine3Circle.ClosestPointOnLine, distanceLine3Circle.ClosestPointOnCircle);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000658C File Offset: 0x0000478C
		public static double DistanceTo(this Ray3 ray3, Arc3 arc3)
		{
			Line3 line = new Line3(ray3.Origin, ray3.Direction);
			DistanceLine3Arc3 distanceLine3Arc = new DistanceLine3Arc3(line, arc3);
			if (distanceLine3Arc.LineParameter < 0.0)
			{
				return ray3.Origin.DistanceTo(arc3);
			}
			return distanceLine3Arc.Distance;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000065E0 File Offset: 0x000047E0
		public static Vector3 ClosestPointTo(this Ray3 ray3, Arc3 arc3)
		{
			Line3 line = new Line3(ray3.Origin, ray3.Direction);
			DistanceLine3Arc3 distanceLine3Arc = new DistanceLine3Arc3(line, arc3);
			if (distanceLine3Arc.LineParameter < 0.0)
			{
				return ray3.Origin;
			}
			return distanceLine3Arc.ClosestPointOnLine;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000662C File Offset: 0x0000482C
		public static Segment3 ShortestSegmentTo(this Ray3 ray3, Arc3 arc3)
		{
			Line3 line = new Line3(ray3.Origin, ray3.Direction);
			DistanceLine3Arc3 distanceLine3Arc = new DistanceLine3Arc3(line, arc3);
			if (distanceLine3Arc.LineParameter < 0.0)
			{
				return ray3.Origin.ShortestSegmentTo(arc3);
			}
			return new Segment3(distanceLine3Arc.ClosestPointOnLine, distanceLine3Arc.ClosestPointOnArc);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000668C File Offset: 0x0000488C
		public static double DistanceTo(this Ray3 ray3, Plane3 plane3)
		{
			DistancePlane3Ray3 distancePlane3Ray = new DistancePlane3Ray3(plane3, ray3);
			return distancePlane3Ray.Distance;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000066AC File Offset: 0x000048AC
		public static double SignedDistanceTo(this Ray3 ray3, Plane3 plane3)
		{
			DistancePlane3Ray3 distancePlane3Ray = new DistancePlane3Ray3(plane3, ray3);
			return distancePlane3Ray.SignedDistance;
		}
	}
}
