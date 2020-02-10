using System;

namespace Hao.Export.MachineData
{
	// Token: 0x02000004 RID: 4
	public class CamExportException : Exception
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002B7A File Offset: 0x00000D7A
		public CamExportException(string message) : base("CAM export failed with message: " + message)
		{
		}
	}
}
