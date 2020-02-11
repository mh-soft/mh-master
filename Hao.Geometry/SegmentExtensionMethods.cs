using System;

namespace Hao.Geometry
{
	// Token: 0x0200007D RID: 125
	public static class SegmentExtensionMethods
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x00016B20 File Offset: 0x00014D20
		public static AxisAlignedBox3 ComputeAxisAlignedBoundingBox(this Segment3 segment)
		{
			double minX = Math.Min(segment.NegativeEnd.X, segment.PositiveEnd.X);
			double minY = Math.Min(segment.NegativeEnd.Y, segment.PositiveEnd.Y);
			double minZ = Math.Min(segment.NegativeEnd.Z, segment.PositiveEnd.Z);
			double maxX = Math.Max(segment.NegativeEnd.X, segment.PositiveEnd.X);
			double maxY = Math.Max(segment.NegativeEnd.Y, segment.PositiveEnd.Y);
			double maxZ = Math.Max(segment.NegativeEnd.Z, segment.PositiveEnd.Z);
			return new AxisAlignedBox3(minX, maxX, minY, maxY, minZ, maxZ);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00016C1C File Offset: 0x00014E1C
		public static AxisAlignedBox2 ComputeAxisAlignedBoundingBox(this Segment2 segment)
		{
			double minX = Math.Min(segment.NegativeEnd.X, segment.PositiveEnd.X);
			double minY = Math.Min(segment.NegativeEnd.Y, segment.PositiveEnd.Y);
			double maxX = Math.Max(segment.NegativeEnd.X, segment.PositiveEnd.X);
			double maxY = Math.Max(segment.NegativeEnd.Y, segment.PositiveEnd.Y);
			return new AxisAlignedBox2(minX, maxX, minY, maxY);
		}
	}
}
