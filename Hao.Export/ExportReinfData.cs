using System;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x02000003 RID: 3
	internal struct ExportReinfData
	{
		// Token: 0x04000005 RID: 5
		public ElementId Id;

		// Token: 0x04000006 RID: 6
		public ExportReinfData.ExportType ExportedAs;

		// Token: 0x02000049 RID: 73
		internal enum ExportType
		{
			// Token: 0x040001D2 RID: 466
			None,
			// Token: 0x040001D3 RID: 467
			AsRodstock,
			// Token: 0x040001D4 RID: 468
			AsFreeForm,
			// Token: 0x040001D5 RID: 469
			AsCFS,
			// Token: 0x040001D6 RID: 470
			AsExtIron,
			// Token: 0x040001D7 RID: 471
			AsGirder,
			// Token: 0x040001D8 RID: 472
			AsMountPart,
			// Token: 0x040001D9 RID: 473
			AsFailure
		}
	}
}
