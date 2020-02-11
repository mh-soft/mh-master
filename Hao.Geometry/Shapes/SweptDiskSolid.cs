using System;
using Hao.Geometry.Topology;

namespace Hao.Geometry.Shapes
{
	// Token: 0x02000007 RID: 7
	public class SweptDiskSolid
	{
		// Token: 0x06000038 RID: 56 RVA: 0x0000272F File Offset: 0x0000092F
		public SweptDiskSolid(Wire3 wire, double radius)
		{
			this.Wire = wire;
			this.Radius = radius;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002745 File Offset: 0x00000945
		// (set) Token: 0x0600003A RID: 58 RVA: 0x0000274D File Offset: 0x0000094D
		public Wire3 Wire { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002756 File Offset: 0x00000956
		// (set) Token: 0x0600003C RID: 60 RVA: 0x0000275E File Offset: 0x0000095E
		public double Radius { get; private set; }
	}
}
