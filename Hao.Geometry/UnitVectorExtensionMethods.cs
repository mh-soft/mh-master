using System;

namespace Hao.Geometry
{
	/// <summary>
	/// 叉乘计算
	/// </summary>
	public static class UnitVectorExtensionMethods
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="unitVector"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static double DotPerpendicular(this UnitVector2 unitVector, UnitVector2 other)
		{
			return unitVector.X * other.Y - unitVector.Y * other.X;
		}
	}
}
