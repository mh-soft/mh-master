using System;

namespace Hao.Geometry
{
	// Token: 0x02000076 RID: 118
	public static class AxisAlignedBoxExtensionMethods
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x00015D20 File Offset: 0x00013F20
		public static AxisAlignedBox2 CreateMergedWith(this AxisAlignedBox2 axisAlignedBox2, AxisAlignedBox2 other)
		{
			return new AxisAlignedBox2((axisAlignedBox2.MinX < other.MinX) ? axisAlignedBox2.MinX : other.MinX, (axisAlignedBox2.MaxX > other.MaxX) ? axisAlignedBox2.MaxX : other.MaxX, (axisAlignedBox2.MinY < other.MinY) ? axisAlignedBox2.MinY : other.MinY, (axisAlignedBox2.MaxY > other.MaxY) ? axisAlignedBox2.MaxY : other.MaxY);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00015DB4 File Offset: 0x00013FB4
		public static AxisAlignedBox3 CreateMergedWith(this AxisAlignedBox3 axisAlignedBox3, AxisAlignedBox3 other)
		{
			return new AxisAlignedBox3((axisAlignedBox3.MinX < other.MinX) ? axisAlignedBox3.MinX : other.MinX, (axisAlignedBox3.MaxX > other.MaxX) ? axisAlignedBox3.MaxX : other.MaxX, (axisAlignedBox3.MinY < other.MinY) ? axisAlignedBox3.MinY : other.MinY, (axisAlignedBox3.MaxY > other.MaxY) ? axisAlignedBox3.MaxY : other.MaxY, (axisAlignedBox3.MinZ < other.MinZ) ? axisAlignedBox3.MinZ : other.MinZ, (axisAlignedBox3.MaxZ > other.MaxZ) ? axisAlignedBox3.MaxZ : other.MaxZ);
		}
	}
}
