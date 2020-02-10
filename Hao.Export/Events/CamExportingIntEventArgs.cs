using System;
using System.Collections.Generic;
using Hao.Export.Events;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData.Events
{
    // Token: 0x02000044 RID: 68
    internal class CamExportingIntEventArgs : EventArgs, ICamExportIntEventArgs, IInternalEventArgs, ICancelableIntEventArgs
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x00014FB1 File Offset: 0x000131B1
		public CamExportingIntEventArgs(Document document, ICamExportIntOptions options, IEnumerable<ElementId> assemblyInstanceIds, CamIntExportStatus status, bool isCancelable = false, bool cancel = false)
		{
			this.Document = document;
			this.Options = options;
			this.AssemblyInstanceIds = new List<ElementId>(assemblyInstanceIds);
			this.IsCancelable = isCancelable;
			this.Cancel = cancel;
			this.Status = status;
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00014FEE File Offset: 0x000131EE
		public bool IsCancelable { get; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00014FF6 File Offset: 0x000131F6
		public Document Document { get; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00014FFE File Offset: 0x000131FE
		public ICamExportIntOptions Options { get; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00015006 File Offset: 0x00013206
		public CamIntExportStatus Status { get; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001500E File Offset: 0x0001320E
		public IEnumerable<ElementId> AssemblyInstanceIds { get; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00015016 File Offset: 0x00013216
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x0001501E File Offset: 0x0001321E
		public bool Cancel { get; set; }
	}
}
