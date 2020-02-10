using System;
using System.Collections.Generic;
using Hao.Export.Geometry;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x02000019 RID: 25
	public abstract class ItMachineDataWallML : ItMachineDataWallLF
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x000094D3 File Offset: 0x000076D3
		public ItMachineDataWallML(ICamExportIntOptions options) : base(options)
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000094E0 File Offset: 0x000076E0
		protected override bool IsMountPartOfThisShell(RevitElement<FamilyInstance> mountingPart, RevitElement<Part> part)
		{
			bool flag = mountingPart.Element.getHostId() == part.Id;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				List<ItSolid> solids = ItSolid.getSolids(mountingPart, ItGeMatrix3d.kIdentity, false);
				bool flag2 = solids.none<ItSolid>();
				if (flag2)
				{
					ItFailures.PostFailure(ItFailures.CAMMountPartInUnknownShell, mountingPart.Id);
					result = (part.shellType() == ShellType.FirstShell);
				}
				else
				{
					result = this.hasSolidIntersection(part, solids);
				}
			}
			return result;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00009554 File Offset: 0x00007754
		private bool hasSolidIntersection(RevitElement<Part> part, List<ItSolid> solids)
		{
			ItSolid partSolid = part.getSolid(true, Transform.Identity, false, false);
			return solids.any((ItSolid solid) => solid.Solid.hasIntersection(partSolid.Solid));
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00009592 File Offset: 0x00007792
		protected static void GetShells(AssemblyInstance assemblyInst, out RevitElement<Part> firstShell, out RevitElement<Part> secondShell)
		{
			firstShell = assemblyInst.getPartOfShellType(ShellType.FirstShell);
			secondShell = assemblyInst.getPartOfShellType(ShellType.SecondShell);
		}
	}
}
