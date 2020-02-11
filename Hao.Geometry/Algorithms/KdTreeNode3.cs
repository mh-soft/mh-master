using System;

namespace Hao.Geometry.Algorithms
{
	// Token: 0x02000004 RID: 4
	public class KdTreeNode3
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000236A File Offset: 0x0000056A
		public KdTreeNode3(int index)
		{
			this.Index = index;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002379 File Offset: 0x00000579
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002381 File Offset: 0x00000581
		public KdTreeNode3 Front { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000238A File Offset: 0x0000058A
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002392 File Offset: 0x00000592
		public KdTreeNode3 Back { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000239B File Offset: 0x0000059B
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000023A3 File Offset: 0x000005A3
		public int Index { get; private set; }
	}
}
