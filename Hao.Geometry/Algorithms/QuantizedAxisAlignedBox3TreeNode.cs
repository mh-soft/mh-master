using System;
using System.Diagnostics;

namespace Hao.Geometry.Algorithms
{
	// Token: 0x0200000B RID: 11
	[DebuggerDisplay("(Data {widthOrValue}) Min ({quantizedAabb.MinX} {quantizedAabb.MinY} {quantizedAabb.MinZ}) Max ({quantizedAabb.MaxX} {quantizedAabb.MaxY} {quantizedAabb.MaxZ})")]
	public struct QuantizedAxisAlignedBox3TreeNode
	{
		// Token: 0x0600004C RID: 76 RVA: 0x0000336A File Offset: 0x0000156A
		public bool IsDataNode()
		{
			return this.widthOrValue >= 0;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003378 File Offset: 0x00001578
		public QuantizedAxisAlignedBox3 GetQuantizedAabb()
		{
			return this.quantizedAabb;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003380 File Offset: 0x00001580
		public int GetBranchNodeWidth()
		{
			return -this.widthOrValue;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003389 File Offset: 0x00001589
		public int GetDataNodeValue()
		{
			return this.widthOrValue;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003391 File Offset: 0x00001591
		public void SetAsBranch(int width, QuantizedAxisAlignedBox3 aabb)
		{
			this.widthOrValue = -width;
			this.quantizedAabb = aabb;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000033A2 File Offset: 0x000015A2
		public void SetAsDataValue(int dataValue, QuantizedAxisAlignedBox3 aabb)
		{
			this.widthOrValue = dataValue;
			this.quantizedAabb = aabb;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000033B4 File Offset: 0x000015B4
		public bool TestQuantizedBoxOverlap(QuantizedAxisAlignedBox3 aabb)
		{
			return this.quantizedAabb.MinX < aabb.MaxX && this.quantizedAabb.MaxX > aabb.MinX && this.quantizedAabb.MinY < aabb.MaxY && this.quantizedAabb.MaxY > aabb.MinY && this.quantizedAabb.MinZ < aabb.MaxZ && this.quantizedAabb.MaxZ > aabb.MinZ;
		}

		// Token: 0x04000019 RID: 25
		private QuantizedAxisAlignedBox3 quantizedAabb;

		// Token: 0x0400001A RID: 26
		private int widthOrValue;
	}
}
