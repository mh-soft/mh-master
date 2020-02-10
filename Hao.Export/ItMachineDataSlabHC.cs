using System;
using Hao.Export.MachineData.PXML;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x02000015 RID: 21
	public class ItMachineDataSlabHC : ItMachineDataSlab
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x000090AB File Offset: 0x000072AB
		public ItMachineDataSlabHC(ICamExportIntOptions options) : base(options)
		{
			this.ProductType = 10;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000090C0 File Offset: 0x000072C0
		protected override bool writeRebar()
		{
			return false;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000090D4 File Offset: 0x000072D4
		protected override void WriteAdditionalSlabFields(ItSlab slab, AssemblyInstance instance)
		{
			string strandsTypeName = this.GetStrandsTypeName(instance);
			slab.GenericInfo01 = strandsTypeName;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000090F4 File Offset: 0x000072F4
		private string GetStrandsTypeName(AssemblyInstance instance)
		{
			return instance.getHCSlabCNCTag();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00009110 File Offset: 0x00007310
		protected override void UnitechnikWriteAdditionalSlabData(AssemblyInstance assemblyInstance)
		{
			string strandsTypeName = this.GetStrandsTypeName(assemblyInstance);
			ItUniWrapperImpl.SetSlabInfoFields(strandsTypeName, string.Empty, string.Empty, string.Empty);
		}
	}
}
