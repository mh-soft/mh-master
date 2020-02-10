using System;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x0200001D RID: 29
	internal class LayerData
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00009D03 File Offset: 0x00007F03
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00009D0B File Offset: 0x00007F0B
		internal double Thickness { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00009D14 File Offset: 0x00007F14
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00009D1C File Offset: 0x00007F1C
		internal double Volume { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00009D25 File Offset: 0x00007F25
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00009D2D File Offset: 0x00007F2D
		internal double UnitWeight { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00009D36 File Offset: 0x00007F36
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00009D3E File Offset: 0x00007F3E
		internal string MaterialName { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00009D47 File Offset: 0x00007F47
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00009D4F File Offset: 0x00007F4F
		internal int PartType { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00009D58 File Offset: 0x00007F58
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00009D60 File Offset: 0x00007F60
		public bool IsInvalid { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00009D69 File Offset: 0x00007F69
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00009D71 File Offset: 0x00007F71
		public RevitElement<Part> Part { get; set; }
	}
}
