using System;

namespace Hao.Geometry
{
	// Token: 0x02000016 RID: 22
	public static class DistanceLine3
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00003771 File Offset: 0x00001971
		public static double DistanceTo(this Line3 line3, Vector3 vector3)
		{
			return vector3.DistanceTo(line3);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000377C File Offset: 0x0000197C
		public static Vector3 ClosestPointTo(this Line3 line3, Vector3 vector3)
		{
			Vector3 vector4 = vector3 - line3.Origin;
			double scalar = line3.Direction.Dot(vector4);
			return line3.Origin + scalar * line3.Direction;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000037C4 File Offset: 0x000019C4
		public static Segment3 ShortestSegmentTo(this Line3 line3, Vector3 vector3)
		{
			Vector3 vector4 = vector3 - line3.Origin;
			double scalar = line3.Direction.Dot(vector4);
			return new Segment3(line3.Origin + scalar * line3.Direction, vector3);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003810 File Offset: 0x00001A10
		public static double DistanceTo(this Line3 line3, Box3 box3)
		{
			DistanceLine3Box3 distanceLine3Box = new DistanceLine3Box3(line3, box3);
			return distanceLine3Box.Distance;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003830 File Offset: 0x00001A30
		public static Vector3 ClosestPointTo(this Line3 line3, Box3 box3)
		{
			DistanceLine3Box3 distanceLine3Box = new DistanceLine3Box3(line3, box3);
			return distanceLine3Box.ClosestPointOnLine;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003850 File Offset: 0x00001A50
		public static Segment3 ShortestSegmentTo(this Line3 line3, Box3 box3)
		{
			DistanceLine3Box3 distanceLine3Box = new DistanceLine3Box3(line3, box3);
			return new Segment3(distanceLine3Box.ClosestPointOnLine, distanceLine3Box.ClosestPointOnBox);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000387C File Offset: 0x00001A7C
		public static double DistanceTo(this Line3 line3, AxisAlignedBox3 axisAlignedBox3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceLine3Box3 distanceLine3Box = new DistanceLine3Box3(line3, box);
			return distanceLine3Box.Distance;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000038CC File Offset: 0x00001ACC
		public static Vector3 ClosestPointTo(this Line3 line3, AxisAlignedBox3 axisAlignedBox3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceLine3Box3 distanceLine3Box = new DistanceLine3Box3(line3, box);
			return distanceLine3Box.ClosestPointOnLine;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000391C File Offset: 0x00001B1C
		public static Segment3 ShortestSegmentTo(this Line3 line3, AxisAlignedBox3 axisAlignedBox3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceLine3Box3 distanceLine3Box = new DistanceLine3Box3(line3, box);
			return new Segment3(distanceLine3Box.ClosestPointOnLine, distanceLine3Box.ClosestPointOnBox);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003978 File Offset: 0x00001B78
		public static double DistanceTo(this Line3 line3, Line3 other)
		{
			DistanceLine3Line3 distanceLine3Line = new DistanceLine3Line3(line3, other);
			return distanceLine3Line.Distance;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003998 File Offset: 0x00001B98
		public static Vector3 ClosestPointTo(this Line3 line3, Line3 other)
		{
			DistanceLine3Line3 distanceLine3Line = new DistanceLine3Line3(line3, other);
			return distanceLine3Line.ClosestPoint0;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000039B8 File Offset: 0x00001BB8
		public static Segment3 ShortestSegmentTo(this Line3 line3, Line3 other)
		{
			DistanceLine3Line3 distanceLine3Line = new DistanceLine3Line3(line3, other);
			return new Segment3(distanceLine3Line.ClosestPoint0, distanceLine3Line.ClosestPoint1);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000039E4 File Offset: 0x00001BE4
		public static double DistanceTo(this Line3 line3, Ray3 ray3)
		{
			DistanceLine3Ray3 distanceLine3Ray = new DistanceLine3Ray3(line3, ray3);
			return distanceLine3Ray.Distance;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003A04 File Offset: 0x00001C04
		public static Vector3 ClosestPointTo(this Line3 line3, Ray3 ray3)
		{
			DistanceLine3Ray3 distanceLine3Ray = new DistanceLine3Ray3(line3, ray3);
			return distanceLine3Ray.ClosestPointOnLine;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003A24 File Offset: 0x00001C24
		public static Segment3 ShortestSegmentTo(this Line3 line3, Ray3 ray3)
		{
			DistanceLine3Ray3 distanceLine3Ray = new DistanceLine3Ray3(line3, ray3);
			return new Segment3(distanceLine3Ray.ClosestPointOnLine, distanceLine3Ray.ClosestPointOnRay);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003A50 File Offset: 0x00001C50
		public static double DistanceTo(this Line3 line3, Rectangle3 rectangle3)
		{
			DistanceLine3Rectangle3 distanceLine3Rectangle = new DistanceLine3Rectangle3(line3, rectangle3);
			return distanceLine3Rectangle.Distance;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003A70 File Offset: 0x00001C70
		public static Vector3 ClosestPointTo(this Line3 line3, Rectangle3 rectangle3)
		{
			DistanceLine3Rectangle3 distanceLine3Rectangle = new DistanceLine3Rectangle3(line3, rectangle3);
			return distanceLine3Rectangle.ClosestPointOnLine;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003A90 File Offset: 0x00001C90
		public static Segment3 ShortestSegmentTo(this Line3 line3, Rectangle3 rectangle3)
		{
			DistanceLine3Rectangle3 distanceLine3Rectangle = new DistanceLine3Rectangle3(line3, rectangle3);
			return new Segment3(distanceLine3Rectangle.ClosestPointOnLine, distanceLine3Rectangle.ClosestPointOnRectangle);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003ABC File Offset: 0x00001CBC
		public static double DistanceTo(this Line3 line3, Segment3 segment3)
		{
			DistanceLine3Segment3 distanceLine3Segment = new DistanceLine3Segment3(line3, segment3);
			return distanceLine3Segment.Distance;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003ADC File Offset: 0x00001CDC
		public static Vector3 ClosestPointTo(this Line3 line3, Segment3 segment3)
		{
			DistanceLine3Segment3 distanceLine3Segment = new DistanceLine3Segment3(line3, segment3);
			return distanceLine3Segment.ClosestPointOnLine;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003AFC File Offset: 0x00001CFC
		public static Segment3 ShortestSegmentTo(this Line3 line3, Segment3 segment3)
		{
			DistanceLine3Segment3 distanceLine3Segment = new DistanceLine3Segment3(line3, segment3);
			return new Segment3(distanceLine3Segment.ClosestPointOnLine, distanceLine3Segment.ClosestPointOnSegment);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003B28 File Offset: 0x00001D28
		public static double DistanceTo(this Line3 line3, Triangle3 triangle3)
		{
			DistanceLine3Triangle3 distanceLine3Triangle = new DistanceLine3Triangle3(line3, triangle3);
			return distanceLine3Triangle.Distance;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003B48 File Offset: 0x00001D48
		public static Vector3 ClosestPointTo(this Line3 line3, Triangle3 triangle3)
		{
			DistanceLine3Triangle3 distanceLine3Triangle = new DistanceLine3Triangle3(line3, triangle3);
			return distanceLine3Triangle.ClosestPointOnLine;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003B68 File Offset: 0x00001D68
		public static Segment3 ShortestSegmentTo(this Line3 line3, Triangle3 triangle3)
		{
			DistanceLine3Triangle3 distanceLine3Triangle = new DistanceLine3Triangle3(line3, triangle3);
			return new Segment3(distanceLine3Triangle.ClosestPointOnLine, distanceLine3Triangle.ClosestPointOnTriangle);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003B94 File Offset: 0x00001D94
		public static double DistanceTo(this Line3 line3, Circle3 circle3)
		{
			DistanceLine3Circle3 distanceLine3Circle = new DistanceLine3Circle3(line3, circle3);
			return distanceLine3Circle.Distance;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public static Vector3 ClosestPointTo(this Line3 line3, Circle3 circle3)
		{
			DistanceLine3Circle3 distanceLine3Circle = new DistanceLine3Circle3(line3, circle3);
			return distanceLine3Circle.ClosestPointOnLine;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public static Segment3 ShortestSegmentTo(this Line3 line3, Circle3 circle3)
		{
			DistanceLine3Circle3 distanceLine3Circle = new DistanceLine3Circle3(line3, circle3);
			return new Segment3(distanceLine3Circle.ClosestPointOnLine, distanceLine3Circle.ClosestPointOnCircle);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003C00 File Offset: 0x00001E00
		public static double DistanceTo(this Line3 line3, Arc3 arc3)
		{
			DistanceLine3Arc3 distanceLine3Arc = new DistanceLine3Arc3(line3, arc3);
			return distanceLine3Arc.Distance;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003C20 File Offset: 0x00001E20
		public static Vector3 ClosestPointTo(this Line3 line3, Arc3 arc3)
		{
			DistanceLine3Arc3 distanceLine3Arc = new DistanceLine3Arc3(line3, arc3);
			return distanceLine3Arc.ClosestPointOnLine;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003C40 File Offset: 0x00001E40
		public static Segment3 ShortestSegmentTo(this Line3 line3, Arc3 arc3)
		{
			DistanceLine3Arc3 distanceLine3Arc = new DistanceLine3Arc3(line3, arc3);
			return new Segment3(distanceLine3Arc.ClosestPointOnLine, distanceLine3Arc.ClosestPointOnArc);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003C6C File Offset: 0x00001E6C
		public static double DistanceTo(this Line3 line3, Plane3 plane3)
		{
			DistancePlane3Line3 distancePlane3Line = new DistancePlane3Line3(plane3, line3);
			return distancePlane3Line.Distance;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003C8C File Offset: 0x00001E8C
		public static double SignedDistanceTo(this Line3 line3, Plane3 plane3)
		{
			DistancePlane3Line3 distancePlane3Line = new DistancePlane3Line3(plane3, line3);
			return distancePlane3Line.SignedDistance;
		}
	}
}
