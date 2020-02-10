using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x0200002A RID: 42
	public class VoidsAndMountParts
	{
		// Token: 0x06000242 RID: 578 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		public VoidsAndMountParts()
		{
			this.openings = new List<RevitElement<Opening>>();
			this.doorsAndWindows = new List<RevitElement<FamilyInstance>>();
			this.mountingParts = new List<RevitElement<FamilyInstance>>();
		}

		// Token: 0x04000089 RID: 137
		public List<RevitElement<Opening>> openings;

		// Token: 0x0400008A RID: 138
		public List<RevitElement<FamilyInstance>> doorsAndWindows;

		// Token: 0x0400008B RID: 139
		public List<RevitElement<FamilyInstance>> mountingParts;
	}
}
