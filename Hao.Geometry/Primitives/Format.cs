using System;

namespace Hao.Geometry
{
	// Token: 0x02000012 RID: 18
	public static class Format
	{
		// Token: 0x02000028 RID: 40
		public static class DebugView
		{
			// Token: 0x170000FD RID: 253
			// (get) Token: 0x060002AC RID: 684 RVA: 0x0000C8FF File Offset: 0x0000AAFF
			// (set) Token: 0x060002AD RID: 685 RVA: 0x0000C906 File Offset: 0x0000AB06
			public static string DoubleComponent { get; set; } = "{0:0.000}";

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x060002AE RID: 686 RVA: 0x0000C90E File Offset: 0x0000AB0E
			// (set) Token: 0x060002AF RID: 687 RVA: 0x0000C915 File Offset: 0x0000AB15
			public static string VectorComponent { get; set; } = "{0:0.0000}";

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000C91D File Offset: 0x0000AB1D
			// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000C924 File Offset: 0x0000AB24
			public static string UnitVectorComponent { get; set; } = "{0,5:0.000}";
		}
	}
}
