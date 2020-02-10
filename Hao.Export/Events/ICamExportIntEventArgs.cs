using System;
using Hao.Export.Events;

namespace Hao.Export.MachineData.Events
{
	// Token: 0x02000042 RID: 66
	internal interface ICamExportIntEventArgs : IInternalEventArgs, ICancelableIntEventArgs
	{
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600051F RID: 1311
		ICamExportIntOptions Options { get; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000520 RID: 1312
		CamIntExportStatus Status { get; }
	}
}
