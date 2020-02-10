using System;
using System.IO;

namespace Hao.Export.MachineData
{
	// Token: 0x0200000B RID: 11
	public interface ICamExportIntOptions
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002E RID: 46
		FileFormat FileFormat { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002F RID: 47
		CamIntOverwriteMode OverwriteMode { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000030 RID: 48
		// (set) Token: 0x06000031 RID: 49
		DirectoryInfo TargetDirectory { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000032 RID: 50
		bool SubdirectoryPerProductType { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000033 RID: 51
		bool MultipleElementsInOneFile { get; }
	}
}
