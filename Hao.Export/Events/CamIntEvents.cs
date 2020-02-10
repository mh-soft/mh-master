using System;
using System.Collections.Generic;
using System.Diagnostics;
using Hao.Export.Events;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData.Events
{
	// Token: 0x02000045 RID: 69
	internal static class CamIntEvents
	{

		internal static event EventHandler<CamExportingIntEventArgs> CamExporting;

	
		internal static event EventHandler<CamExportingIntEventArgs> CamExported;

		
		internal static event EventHandler<CamExportingAssemblyIntEventArgs> CamExportingAssembly;

		internal static event EventHandler<CamExportingAssemblyIntEventArgs> CamExportedAssembly;

		// Token: 0x0600053B RID: 1339 RVA: 0x000151C8 File Offset: 0x000133C8
		internal static void OnExporting(Document document, IEnumerable<ElementId> assemblyInstanceIds, ICamExportIntOptions options, out bool cancel)
		{
			CamExportingIntEventArgs eventArgs = new CamExportingIntEventArgs(document, options, assemblyInstanceIds, CamIntExportStatus.None, true, false);
			EventHelper.Invoke<CamExportingIntEventArgs>(CamIntEvents.CamExporting, eventArgs, CamIntEvents.TrueCondition, out cancel);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000151F4 File Offset: 0x000133F4
		internal static void OnExported(Document document, IEnumerable<ElementId> assemblyInstanceIds, ICamExportIntOptions options, CamIntExportStatus status)
		{
			CamExportingIntEventArgs eventArgs = new CamExportingIntEventArgs(document, options, assemblyInstanceIds, status, false, false);
			bool flag;
			EventHelper.Invoke<CamExportingIntEventArgs>(CamIntEvents.CamExported, eventArgs, CamIntEvents.TrueCondition, out flag);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00015224 File Offset: 0x00013424
		internal static void OnExportingAssembly(Document document, ElementId assemblyInstanceId, ICamExportIntOptions options, ProductType product, string fileName, out bool cancel)
		{
			CamExportingAssemblyIntEventArgs eventArgs = new CamExportingAssemblyIntEventArgs(document, options, assemblyInstanceId, product, fileName, CamIntExportStatus.None, true, false);
			EventHelper.Invoke<CamExportingAssemblyIntEventArgs>(CamIntEvents.CamExportingAssembly, eventArgs, CamIntEvents.TrueCondition, out cancel);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00015254 File Offset: 0x00013454
		internal static void OnExportedAssembly(Document document, ElementId assemblyInstanceId, ICamExportIntOptions options, ProductType product, string fileName, CamIntExportStatus status)
		{
			CamExportingAssemblyIntEventArgs eventArgs = new CamExportingAssemblyIntEventArgs(document, options, assemblyInstanceId, product, fileName, status, false, false);
			bool flag;
			EventHelper.Invoke<CamExportingAssemblyIntEventArgs>(CamIntEvents.CamExportedAssembly, eventArgs, CamIntEvents.TrueCondition, out flag);
		}

		// Token: 0x040001BF RID: 447
		internal static readonly Func<object, bool> TrueCondition = (object o) => true;

		// Token: 0x040001C4 RID: 452
		internal static Func<CamExportingIntEventArgs, bool> ExportingPostCondition = CamIntEvents.TrueCondition;

		// Token: 0x040001C5 RID: 453
		internal static Func<CamExportingIntEventArgs, bool> ExportedPostCondition = CamIntEvents.TrueCondition;

		// Token: 0x040001C6 RID: 454
		internal static Func<CamExportingAssemblyIntEventArgs, bool> ExportingAssemblyPostCondition = CamIntEvents.TrueCondition;

		// Token: 0x040001C7 RID: 455
		internal static Func<CamExportingAssemblyIntEventArgs, bool> ExportedAssemblyPostCondition = CamIntEvents.TrueCondition;
	}
}
