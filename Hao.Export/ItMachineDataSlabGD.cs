using System;

namespace Hao.Export.MachineData
{
	// Token: 0x02000014 RID: 20
	public class ItMachineDataSlabGD : ItMachineDataSlab
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00009083 File Offset: 0x00007283
		public ItMachineDataSlabGD(ICamExportIntOptions options) : base(options)
		{
			this.ProductType = 0;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00009098 File Offset: 0x00007298
		protected override bool writeRebar()
		{
			return true;
		}
	}
}
