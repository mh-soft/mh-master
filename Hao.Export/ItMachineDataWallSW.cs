using System;
using Hao.Export.Geometry;
using Hao.Export.MachineData.PXML;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x0200001B RID: 27
	public class ItMachineDataWallSW : ItMachineDataWallML
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x000095E3 File Offset: 0x000077E3
		public ItMachineDataWallSW(ICamExportIntOptions options) : base(options)
		{
			this.ProductType = 11;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000095F8 File Offset: 0x000077F8
		protected override bool writeRebar()
		{
			return true;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000960C File Offset: 0x0000780C
		protected override void setZPositionOfContour(RevitElement<Part> part, ItOutline outline)
		{
			bool flag = part == null || outline == null;
			if (!flag)
			{
				ItGeBoundBlock3d localExtents = part.getLocalExtents(false);
				ItGeMatrix3d mat = part.ecs(false);
				ItGeBoundBlock3d transformed = localExtents.GetTransformed(base.MatWcsToPalette * mat);
				outline.Z = transformed.minPoint.z;
			}
		}
	}
}
