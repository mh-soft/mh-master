using System;

namespace Hao.Geometry
{
	// Token: 0x02000075 RID: 117
	public static class IsInsideAxisAlignedBox2
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x00015CC2 File Offset: 0x00013EC2
		public static bool IsInsideOf(this AxisAlignedBox2 axisAlignedBox2, Segment2 segment2)
		{
			return axisAlignedBox2.IsInsideOf(segment2.NegativeEnd) && axisAlignedBox2.IsInsideOf(segment2.PositiveEnd);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00015CE2 File Offset: 0x00013EE2
		public static bool IsInsideOf(this AxisAlignedBox2 axisAlignedBox2, Vector2 vector2)
		{
			//vector2 - axisAlignedBox2.Center;
			return Math.Abs(vector2.X) < axisAlignedBox2.ExtentX && Math.Abs(vector2.Y) < axisAlignedBox2.ExtentY;
		}
	}
}
