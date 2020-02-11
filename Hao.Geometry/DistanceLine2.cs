using System;

namespace Hao.Geometry
{
	
	/// <summary>
	/// 线之间的距离
	/// </summary>
	public static class DistanceLine2
	{

		/// <summary>
		/// 计算线和点的距离
		/// </summary>
		/// <param name="line"></param>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static double DistanceTo(this Line2 line, Vector2 vector)
		{
			return vector.DistanceTo(line);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="line"></param>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static Vector2 ClosestPointTo(this Line2 line, Vector2 vector)
		{
			DistanceVector2Line2 distanceVector2Line = new DistanceVector2Line2(vector, line);
			return distanceVector2Line.ClosestPointOnLine;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="line"></param>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static Segment2 ShortestSegmentTo(this Line2 line, Vector2 vector)
		{
			DistanceVector2Line2 distanceVector2Line = new DistanceVector2Line2(vector, line);
			return new Segment2(distanceVector2Line.ClosestPointOnLine, distanceVector2Line.ClosestPointOnVector);
		}
	}
}
