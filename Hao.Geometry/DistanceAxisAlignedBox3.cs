using System;

namespace Hao.Geometry
{
	// Token: 0x02000003 RID: 3
	public static class DistanceAxisAlignedBox3
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020C8 File Offset: 0x000002C8
		public static double DistanceTo(this AxisAlignedBox3 axisAlignedBox3, Vector3 vector3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			return vector3.DistanceTo(box);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002110 File Offset: 0x00000310
		public static Vector3 ClosestPointTo(this AxisAlignedBox3 axisAlignedBox3, Vector3 vector3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceVector3Box3 distanceVector3Box = new DistanceVector3Box3(vector3, box);
			return distanceVector3Box.ClosestPointOnBox;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002160 File Offset: 0x00000360
		public static Segment3 ShortestSegmentTo(this AxisAlignedBox3 axisAlignedBox3, Vector3 vector3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceVector3Box3 distanceVector3Box = new DistanceVector3Box3(vector3, box);
			return new Segment3(distanceVector3Box.ClosestPointOnBox, distanceVector3Box.ClosestPointOnVector);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021BC File Offset: 0x000003BC
		public static double DistanceTo(this AxisAlignedBox3 axisAlignedBox3, Segment3 segment)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			return segment.DistanceTo(box);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002204 File Offset: 0x00000404
		public static Vector3 ClosestPointTo(this AxisAlignedBox3 axisAlignedBox3, Segment3 segment)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceSegment3Box3 distanceSegment3Box = new DistanceSegment3Box3(segment, box);
			return distanceSegment3Box.ClosestPointOnBox;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002254 File Offset: 0x00000454
		public static Segment3 ShortestSegmentTo(this AxisAlignedBox3 axisAlignedBox3, Segment3 segment)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceSegment3Box3 distanceSegment3Box = new DistanceSegment3Box3(segment, box);
			return new Segment3(distanceSegment3Box.ClosestPointOnBox, distanceSegment3Box.ClosestPointOnSegment);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022B0 File Offset: 0x000004B0
		public static double DistanceTo(this AxisAlignedBox3 axisAlignedBox3, Line3 line3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			return line3.DistanceTo(box);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022F8 File Offset: 0x000004F8
		public static Vector3 ClosestPointTo(this AxisAlignedBox3 axisAlignedBox3, Line3 line3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceLine3Box3 distanceLine3Box = new DistanceLine3Box3(line3, box);
			return distanceLine3Box.ClosestPointOnBox;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002348 File Offset: 0x00000548
		public static Segment3 ShortestSegmentTo(this AxisAlignedBox3 axisAlignedBox3, Line3 line3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceLine3Box3 distanceLine3Box = new DistanceLine3Box3(line3, box);
			return new Segment3(distanceLine3Box.ClosestPointOnBox, distanceLine3Box.ClosestPointOnLine);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023A4 File Offset: 0x000005A4
		public static double DistanceTo(this AxisAlignedBox3 axisAlignedBox3, Ray3 ray3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			return ray3.DistanceTo(box);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023EC File Offset: 0x000005EC
		public static Vector3 ClosestPointTo(this AxisAlignedBox3 axisAlignedBox3, Ray3 ray3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceRay3Box3 distanceRay3Box = new DistanceRay3Box3(ray3, box);
			return distanceRay3Box.ClosestPointOnBox;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000243C File Offset: 0x0000063C
		public static Segment3 ShortestSegmentTo(this AxisAlignedBox3 axisAlignedBox3, Ray3 ray3)
		{
			Box3 box = new Box3(axisAlignedBox3.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox3.ExtentX, axisAlignedBox3.ExtentY, axisAlignedBox3.ExtentZ);
			DistanceRay3Box3 distanceRay3Box = new DistanceRay3Box3(ray3, box);
			return new Segment3(distanceRay3Box.ClosestPointOnBox, distanceRay3Box.ClosestPointOnRay);
		}
	}
}
