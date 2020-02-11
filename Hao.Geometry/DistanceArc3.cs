using System;

namespace Hao.Geometry
{
	// Token: 0x02000007 RID: 7
	public static class DistanceArc3
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000025FD File Offset: 0x000007FD
		public static double DistanceTo(this Arc3 arc3, Line3 line3)
		{
			return line3.DistanceTo(arc3);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002608 File Offset: 0x00000808
		public static Vector3 ClosestPointTo(this Arc3 arc3, Line3 line3)
		{
			DistanceLine3Arc3 distanceLine3Arc = new DistanceLine3Arc3(line3, arc3);
			return distanceLine3Arc.ClosestPointOnArc;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002628 File Offset: 0x00000828
		public static Segment3 ShortestSegmentTo(this Arc3 arc3, Line3 line3)
		{
			DistanceLine3Arc3 distanceLine3Arc = new DistanceLine3Arc3(line3, arc3);
			return new Segment3(distanceLine3Arc.ClosestPointOnArc, distanceLine3Arc.ClosestPointOnLine);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002651 File Offset: 0x00000851
		public static double DistanceTo(this Arc3 arc3, Ray3 ray3)
		{
			return ray3.DistanceTo(arc3);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000265C File Offset: 0x0000085C
		public static Vector3 ClosestPointTo(this Arc3 arc3, Ray3 ray3)
		{
			Line3 line = new Line3(ray3.Origin, ray3.Direction);
			DistanceLine3Arc3 distanceLine3Arc = new DistanceLine3Arc3(line, arc3);
			if (distanceLine3Arc.LineParameter < 0.0)
			{
				return arc3.ClosestPointTo(ray3.Origin);
			}
			return distanceLine3Arc.ClosestPointOnArc;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026B0 File Offset: 0x000008B0
		public static Segment3 ShortestSegmentTo(this Arc3 arc3, Ray3 ray3)
		{
			Line3 line = new Line3(ray3.Origin, ray3.Direction);
			DistanceLine3Arc3 distanceLine3Arc = new DistanceLine3Arc3(line, arc3);
			if (distanceLine3Arc.LineParameter < 0.0)
			{
				return arc3.ShortestSegmentTo(ray3.Origin);
			}
			return new Segment3(distanceLine3Arc.ClosestPointOnArc, distanceLine3Arc.ClosestPointOnLine);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002710 File Offset: 0x00000910
		public static double DistanceTo(this Arc3 arc3, Segment3 segment3)
		{
			Line3 line = new Line3(segment3.NegativeEnd, segment3.Direction);
			DistanceLine3Arc3 distanceLine3Arc = new DistanceLine3Arc3(line, arc3);
			if (distanceLine3Arc.LineParameter < 0.0)
			{
				return arc3.DistanceTo(segment3.NegativeEnd);
			}
			Line3 line2 = new Line3(segment3.PositiveEnd, -segment3.Direction);
			DistanceLine3Arc3 distanceLine3Arc2 = new DistanceLine3Arc3(line2, arc3);
			if (distanceLine3Arc2.LineParameter < 0.0)
			{
				return arc3.DistanceTo(segment3.PositiveEnd);
			}
			return distanceLine3Arc2.Distance;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027A8 File Offset: 0x000009A8
		public static Vector3 ClosestPointTo(this Arc3 arc3, Segment3 segment3)
		{
			Line3 line = new Line3(segment3.NegativeEnd, segment3.Direction);
			DistanceLine3Arc3 distanceLine3Arc = new DistanceLine3Arc3(line, arc3);
			if (distanceLine3Arc.LineParameter < 0.0)
			{
				return arc3.ClosestPointTo(segment3.NegativeEnd);
			}
			Line3 line2 = new Line3(segment3.PositiveEnd, -segment3.Direction);
			DistanceLine3Arc3 distanceLine3Arc2 = new DistanceLine3Arc3(line2, arc3);
			if (distanceLine3Arc2.LineParameter < 0.0)
			{
				return arc3.ClosestPointTo(segment3.PositiveEnd);
			}
			return distanceLine3Arc2.ClosestPointOnArc;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002840 File Offset: 0x00000A40
		public static Segment3 ShortestSegmentTo(this Arc3 arc3, Segment3 segment3)
		{
			Line3 line = new Line3(segment3.NegativeEnd, segment3.Direction);
			DistanceLine3Arc3 distanceLine3Arc = new DistanceLine3Arc3(line, arc3);
			if (distanceLine3Arc.LineParameter < 0.0)
			{
				return arc3.ShortestSegmentTo(segment3.NegativeEnd);
			}
			Line3 line2 = new Line3(segment3.PositiveEnd, -segment3.Direction);
			DistanceLine3Arc3 distanceLine3Arc2 = new DistanceLine3Arc3(line2, arc3);
			if (distanceLine3Arc2.LineParameter < 0.0)
			{
				return arc3.ShortestSegmentTo(segment3.PositiveEnd);
			}
			return new Segment3(distanceLine3Arc2.ClosestPointOnArc, distanceLine3Arc2.ClosestPointOnLine);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000028E1 File Offset: 0x00000AE1
		public static double DistanceTo(this Arc3 arc3, Vector3 vector3)
		{
			return vector3.DistanceTo(arc3);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000028EC File Offset: 0x00000AEC
		public static Vector3 ClosestPointTo(this Arc3 arc3, Vector3 vector3)
		{
			DistanceVector3Arc3 distanceVector3Arc = new DistanceVector3Arc3(vector3, arc3);
			return distanceVector3Arc.ClosestPointOnArc;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000290C File Offset: 0x00000B0C
		public static Segment3 ShortestSegmentTo(this Arc3 arc3, Vector3 vector3)
		{
			DistanceVector3Arc3 distanceVector3Arc = new DistanceVector3Arc3(vector3, arc3);
			return new Segment3(distanceVector3Arc.ClosestPointOnArc, distanceVector3Arc.ClosestPointOnVector);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002938 File Offset: 0x00000B38
		public static double DistanceTo(this Arc3 arc3, Plane3 plane3)
		{
			DistancePlane3Arc3 distancePlane3Arc = new DistancePlane3Arc3(plane3, arc3);
			return distancePlane3Arc.Distance;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002958 File Offset: 0x00000B58
		public static double SignedDistanceTo(this Arc3 arc3, Plane3 plane3)
		{
			DistancePlane3Arc3 distancePlane3Arc = new DistancePlane3Arc3(plane3, arc3);
			return distancePlane3Arc.SignedDistance;
		}
	}
}
