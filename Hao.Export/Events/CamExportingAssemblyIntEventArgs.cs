using System;
using System.IO;
using Hao.Export.Events;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData.Events
{
    // Token: 0x02000043 RID: 67
    internal class CamExportingAssemblyIntEventArgs : EventArgs, ICamExportIntEventArgs, IInternalEventArgs, ICancelableIntEventArgs
	{
		// Token: 0x06000521 RID: 1313 RVA: 0x00014F10 File Offset: 0x00013110
		public CamExportingAssemblyIntEventArgs(Document document, ICamExportIntOptions options, ElementId assemblyInstanceId, ProductType product, string fileName, CamIntExportStatus status, bool isCancelable = false, bool cancel = false)
		{
			this.Document = document;
			this.Options = options;
			this.AssemblyInstanceId = assemblyInstanceId;
			this.Product = product;
			this.ExportedFile = new FileInfo(fileName);
			this.IsCancelable = isCancelable;
			this.Cancel = cancel;
			this.Status = status;
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00014F68 File Offset: 0x00013168
		public bool IsCancelable { get; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00014F70 File Offset: 0x00013170
		public Document Document { get; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00014F78 File Offset: 0x00013178
		public ICamExportIntOptions Options { get; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00014F80 File Offset: 0x00013180
		public ElementId AssemblyInstanceId { get; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00014F88 File Offset: 0x00013188
		public ProductType Product { get; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00014F90 File Offset: 0x00013190
		public FileInfo ExportedFile { get; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00014F98 File Offset: 0x00013198
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x00014FA0 File Offset: 0x000131A0
		public bool Cancel { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00014FA9 File Offset: 0x000131A9
		public CamIntExportStatus Status { get; }
	}
}
