using System;

namespace Hao.Geometry
{
	// Token: 0x0200004C RID: 76
	internal struct IntersectionAxisAlignedBox2AxisAlignedBox2
	{
		// Token: 0x06000309 RID: 777 RVA: 0x0000C497 File Offset: 0x0000A697
		public IntersectionAxisAlignedBox2AxisAlignedBox2(AxisAlignedBox2 box0, AxisAlignedBox2 box1)
		{
			this.box0 = box0;
			this.box1 = box1;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
		public bool HasXOverlap()
		{
			return this.box0.MaxX >= this.box1.MinX && this.box0.MinX <= this.box1.MaxX;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000C4F8 File Offset: 0x0000A6F8
		public bool HasYOverlap()
		{
			return this.box0.MaxY >= this.box1.MinY && this.box0.MinY <= this.box1.MaxY;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000C548 File Offset: 0x0000A748
		public bool Test()
		{
			return this.box0.MaxX >= this.box1.MinX && this.box0.MinX <= this.box1.MaxX && this.box0.MaxY >= this.box1.MinY && this.box0.MinY <= this.box1.MaxY;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000C5D4 File Offset: 0x0000A7D4
		public bool Find(out AxisAlignedBox2 intersection)
		{
			if (this.box0.MaxX < this.box1.MinX || this.box0.MinX > this.box1.MaxX)
			{
				intersection = new AxisAlignedBox2(0.0, 0.0, 0.0, 0.0);
				return false;
			}
			if (this.box0.MaxY < this.box1.MinY || this.box0.MinY > this.box1.MaxY)
			{
				intersection = new AxisAlignedBox2(0.0, 0.0, 0.0, 0.0);
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
			intersection = new AxisAlignedBox2(minX, maxX, minY, maxY);
			return true;
		}

		// Token: 0x040000D6 RID: 214
		private readonly AxisAlignedBox2 box0;

		// Token: 0x040000D7 RID: 215
		private readonly AxisAlignedBox2 box1;
	}
}
