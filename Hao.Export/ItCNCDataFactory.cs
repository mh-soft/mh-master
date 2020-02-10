using System;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x0200000D RID: 13
	public static class ItCNCDataFactory
	{
		// Token: 0x06000048 RID: 72 RVA: 0x0000407C File Offset: 0x0000227C
		public static ItMachineDataBase getCNCDocCreator(AssemblyInstance assemblyInstance, ICamExportIntOptions options)
		{
			ItMachineDataBase itMachineDataBase = null;
			ProductType productType = assemblyInstance.productType();
			bool flag = ProductType.WallSD == productType;
			if (flag)
			{
				itMachineDataBase = new ItMachineDataWallSD(options);
			}
			bool flag2 = ProductType.WallSW == productType;
			if (flag2)
			{
				itMachineDataBase = new ItMachineDataWallSW(options);
			}
			bool flag3 = ProductType.WallDB == productType;
			if (flag3)
			{
				itMachineDataBase = new ItMachineDataWallDB(options);
			}
			bool flag4 = ProductType.SlabHC == productType;
			if (flag4)
			{
				itMachineDataBase = new ItMachineDataSlabHC(options);
			}
			bool flag5 = ProductType.SlabSD == productType;
			if (flag5)
			{
				itMachineDataBase = new ItMachineDataSlabSD(options);
			}
			bool flag6 = ProductType.SlabGD == productType;
			if (flag6)
			{
				itMachineDataBase = new ItMachineDataSlabGD(options);
			}
			bool flag7 = itMachineDataBase == null;
			if (flag7)
			{
				ItFailures.PostFailure(ItFailures.CAMManualCreatedAssemblyNotSupported, assemblyInstance.Id);
			}
			return itMachineDataBase;
		}
	}
}
