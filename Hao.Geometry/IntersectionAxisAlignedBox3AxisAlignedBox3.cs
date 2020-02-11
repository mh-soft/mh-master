using System;

namespace Hao.Geometry
{
	// Token: 0x0200004E RID: 78
	internal struct IntersectionAxisAlignedBox3AxisAlignedBox3
	{
		// Token: 0x0600031C RID: 796 RVA: 0x0000C899 File Offset: 0x0000AA99
		public IntersectionAxisAlignedBox3AxisAlignedBox3(AxisAlignedBox3 box0, AxisAlignedBox3 box1)
		{
			this.box0 = box0;
			this.box1 = box1;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000C8AC File Offset: 0x0000AAAC
		public bool HasXOverlap()
		{
			return this.box0.Max.X >= this.box1.Min.X && this.box0.Min.X <= this.box1.Max.X;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000C91C File Offset: 0x0000AB1C
		public bool HasYOverlap()
		{
			return this.box0.Max.Y >= this.box1.Min.Y && this.box0.Min.Y <= this.box1.Max.Y;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000C98C File Offset: 0x0000AB8C
		public bool HasZOverlap()
		{
			return this.box0.Max.Z >= this.box1.Min.Z && this.box0.Min.Z <= this.box1.Max.Z;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000C9FC File Offset: 0x0000ABFC
		public bool Test()
		{
			return this.box0.Max.X >= this.box1.Min.X && this.box0.Min.X <= this.box1.Max.X && this.box0.Max.Y >= this.box1.Min.Y && this.box0.Min.Y <= this.box1.Max.Y && this.box0.Max.Z >= this.box1.Min.Z && this.box0.Min.Z <= this.box1.Max.Z;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000CB24 File Offset: 0x0000AD24
		public bool Find(out AxisAlignedBox3 intersection)
		{
			if (this.box0.Max.X < this.box1.Min.X || this.box0.Min.X > this.box1.Max.X)
			{
				intersection = new AxisAlignedBox3(0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
				return false;
			}
			if (this.box0.Max.Y < this.box1.Min.Y || this.box0.Min.Y > this.box1.Max.Y)
			{
				intersection = new AxisAlignedBox3(0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
				return false;
			}
			if (this.box0.Max.Z < this.box1.Min.Z || this.box0.Min.Z > this.box1.Max.Z)
			{
				intersection = new AxisAlignedBox3(0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
				return false;
			}
			double maxX;
			if (this.box0.MaxX <= this.box1.MaxX)
			{
				maxX = this.box0.MaxX;
			}
			else
			{
				maxX = this.box1.MaxX;
			}
			double minX;
			if (this.box0.MinX <= this.box1.MinX)
			{
				minX = this.box1.MinX;
			}
			else
			{
				minX = this.box0.MinX;
			}
			double maxY;
			if (this.box0.MaxY <= this.box1.MaxY)
			{
				maxY = this.box0.MaxY;
			}
			else
			{
				maxY = this.box1.MaxY;
			}
			double minY;
			if (this.box0.MinY <= this.box1.MinY)
			{
				minY = this.box1.MinY;
			}
			else
			{
				minY = this.box0.MinY;
			}
			double maxZ;
			if (this.box0.MaxZ <= this.box1.MaxZ)
			{
				maxZ = this.box0.MaxZ;
			}
			else
			{
				maxZ = this.box1.MaxZ;
			}
			double minZ;
			if (this.box0.MinZ <= this.box1.MinZ)
			{
				minZ = this.box1.MinZ;
			}
			else
			{
				minZ = this.box0.MinZ;
			}
			intersection = new AxisAlignedBox3(minX, maxX, minY, maxY, minZ, maxZ);
			return true;
		}

		// Token: 0x040000D8 RID: 216
		private readonly AxisAlignedBox3 box0;

		// Token: 0x040000D9 RID: 217
		private readonly AxisAlignedBox3 box1;
	}
}
