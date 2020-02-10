using System;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x0200001A RID: 26
	public class ItMachineDataWallSD : ItMachineDataWallML
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x000095A7 File Offset: 0x000077A7
		public ItMachineDataWallSD(ICamExportIntOptions options) : base(options)
		{
			this.ProductType = 9;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000095BC File Offset: 0x000077BC
		protected override bool writeRebar()
		{
			return true;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000095D0 File Offset: 0x000077D0
		protected override bool IsMountPartOfThisShell(RevitElement<FamilyInstance> mountingPart, RevitElement<Part> revitElement)
		{
			return true;
		}
	}
}
