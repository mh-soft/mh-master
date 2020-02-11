using System;

namespace Hao.Geometry
{
	// Token: 0x02000008 RID: 8
	public static class DistanceBox3
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002975 File Offset: 0x00000B75
		public static double DistanceTo(this Box3 box3, Vector3 vector3)
		{
			return vector3.DistanceTo(box3);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002980 File Offset: 0x00000B80
		public static Vector3 ClosestPointTo(this Box3 box3, Vector3 vector3)
		{
			DistanceVector3Box3 distanceVector3Box = new DistanceVector3Box3(vector3, box3);
			return distanceVector3Box.ClosestPointOnBox;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000029A0 File Offset: 0x00000BA0
		public static Segment3 ShortestSegmentTo(this Box3 box3, Vector3 vector3)
		{
			DistanceVector3Box3 distanceVector3Box = new DistanceVector3Box3(vector3, box3);
			return new Segment3(distanceVector3Box.ClosestPointOnBox, distanceVector3Box.ClosestPointOnVector);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000029C9 File Offset: 0x00000BC9
		public static double DistanceTo(this Box3 box3, Segment3 segment)
		{
			return segment.DistanceTo(box3);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029D4 File Offset: 0x00000BD4
		public static Vector3 ClosestPointTo(this Box3 box3, Segment3 segment)
		{
			DistanceSegment3Box3 distanceSegment3Box = new DistanceSegment3Box3(segment, box3);
			return distanceSegment3Box.ClosestPointOnBox;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000029F4 File Offset: 0x00000BF4
		public static Segment3 ShortestSegmentTo(this Box3 box3, Segment3 segment)
		{
			DistanceSegment3Box3 distanceSegment3Box = new DistanceSegment3Box3(segment, box3);
			return new Segment3(distanceSegment3Box.ClosestPointOnBox, distanceSegment3Box.ClosestPointOnSegment);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A1D File Offset: 0x00000C1D
		public static double DistanceTo(this Box3 box3, Line3 line3)
		{
			return line3.DistanceTo(box3);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A28 File Offset: 0x00000C28
		public static Vector3 ClosestPointTo(this Box3 box3, Line3 line3)
		{
			DistanceLine3Box3 distanceLine3Box = new DistanceLine3Box3(line3, box3);
			return distanceLine3Box.ClosestPointOnBox;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A48 File Offset: 0x00000C48
		public static Segment3 ShortestSegmentTo(this Box3 box3, Line3 line3)
		{
			DistanceLine3Box3 distanceLine3Box = new DistanceLine3Box3(line3, box3);
			return new Segment3(distanceLine3Box.ClosestPointOnBox, distanceLine3Box.ClosestPointOnLine);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A71 File Offset: 0x00000C71
		public static double DistanceTo(this Box3 box3, Ray3 ray3)
		{
			return ray3.DistanceTo(box3);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A7C File Offset: 0x00000C7C
		public static Vector3 ClosestPointTo(this Box3 box3, Ray3 ray3)
		{
			DistanceRay3Box3 distanceRay3Box = new DistanceRay3Box3(ray3, box3);
			return distanceRay3Box.ClosestPointOnBox;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A9C File Offset: 0x00000C9C
		public static Segment3 ShortestSegmentTo(this Box3 box3, Ray3 ray3)
		{
			DistanceRay3Box3 distanceRay3Box = new DistanceRay3Box3(ray3, box3);
			return new Segment3(distanceRay3Box.ClosestPointOnBox, distanceRay3Box.ClosestPointOnRay);
		}
	}
}
