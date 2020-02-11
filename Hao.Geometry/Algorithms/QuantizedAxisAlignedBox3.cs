using System;

namespace Hao.Geometry.Algorithms
{
	// Token: 0x02000006 RID: 6
	public struct QuantizedAxisAlignedBox3
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000023B4 File Offset: 0x000005B4
		public QuantizedAxisAlignedBox3(ushort minX, ushort maxX, ushort minY, ushort maxY, ushort minZ, ushort maxZ)
		{
			this = default(QuantizedAxisAlignedBox3);
			this.MinX = minX;
			this.MaxX = maxX;
			this.MinY = minY;
			this.MaxY = maxY;
			this.MinZ = minZ;
			this.MaxZ = maxZ;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023EA File Offset: 0x000005EA
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000023F2 File Offset: 0x000005F2
		public ushort MinX { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000023FB File Offset: 0x000005FB
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002403 File Offset: 0x00000603
		public ushort MaxX { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000240C File Offset: 0x0000060C
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002414 File Offset: 0x00000614
		public ushort MinY { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000241D File Offset: 0x0000061D
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002425 File Offset: 0x00000625
		public ushort MaxY { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000242E File Offset: 0x0000062E
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002436 File Offset: 0x00000636
		public ushort MinZ { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000243F File Offset: 0x0000063F
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002447 File Offset: 0x00000647
		public ushort MaxZ { get; private set; }

		// Token: 0x06000025 RID: 37 RVA: 0x00002450 File Offset: 0x00000650
		public QuantizedAxisAlignedBox3 CreateMergedWith(QuantizedAxisAlignedBox3 other)
		{
			return new QuantizedAxisAlignedBox3(Math.Min(this.MinX, other.MinX), Math.Max(this.MaxX, other.MaxX), Math.Min(this.MinY, other.MinY), Math.Max(this.MaxY, other.MaxY), Math.Min(this.MinZ, other.MinZ), Math.Max(this.MaxZ, other.MaxZ));
		}
	}
}
