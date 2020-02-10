using System;

namespace Hao.Export.MachineData
{
	// Token: 0x02000016 RID: 22
	public class ItMachineDataSlabSD : ItMachineDataSlab
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x0000913C File Offset: 0x0000733C
		public ItMachineDataSlabSD(ICamExportIntOptions options) : base(options)
		{
			this.ProductType = 6;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00009150 File Offset: 0x00007350
		protected override bool writeRebar()
		{
			return true;
		}
	}
}
