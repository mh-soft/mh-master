using System;
using System.Collections.Generic;
using Hao.Export.Geometry;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x0200001E RID: 30
	public class MountingPartData
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00009D7A File Offset: 0x00007F7A
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00009D82 File Offset: 0x00007F82
		public ElementId ElementId { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00009D8B File Offset: 0x00007F8B
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00009D93 File Offset: 0x00007F93
		public List<ItGePoint3d> Contour { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00009D9C File Offset: 0x00007F9C
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00009DA4 File Offset: 0x00007FA4
		public double Height { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00009DAD File Offset: 0x00007FAD
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00009DB5 File Offset: 0x00007FB5
		public bool IsOpeningWithoutGeometry { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00009DBE File Offset: 0x00007FBE
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00009DC6 File Offset: 0x00007FC6
		public MountingPartData.Types Type { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00009DCF File Offset: 0x00007FCF
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00009DD7 File Offset: 0x00007FD7
		public double InstallationHeight { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00009DE0 File Offset: 0x00007FE0
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00009DE8 File Offset: 0x00007FE8
		public string Name { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00009DF1 File Offset: 0x00007FF1
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00009DF9 File Offset: 0x00007FF9
		public string UniqueId { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00009E04 File Offset: 0x00008004
		public double Area
		{
			get
			{
				ItGePolyline3d poly3d = new ItGePolyline3d(this.Contour);
				ItGePolyline2d itGePolyline2d = new ItGePolyline2d(poly3d, null);
				itGePolyline2d.closeIt();
				return itGePolyline2d.area;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00009E38 File Offset: 0x00008038
		public MountingPartData()
		{
			this.ElementId = ElementId.InvalidElementId;
			this.Contour = null;
			this.Height = 0.0;
			this.IsOpeningWithoutGeometry = false;
			this.Type = MountingPartData.Types.Unknown;
			this.InstallationHeight = 0.0;
			this.Name = string.Empty;
			this.UniqueId = null;
		}

		// Token: 0x02000066 RID: 102
		public enum Types
		{
			// Token: 0x0400026C RID: 620
			Unknown,
			// Token: 0x0400026D RID: 621
			DoorWindow,
			// Token: 0x0400026E RID: 622
			Opening,
			// Token: 0x0400026F RID: 623
			CutOut,
			// Token: 0x04000270 RID: 624
			MountPart,
			// Token: 0x04000271 RID: 625
			Rebar
		}
	}
}
