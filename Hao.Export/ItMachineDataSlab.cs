using System;
using System.Collections.Generic;
using System.Linq;
using Hao.Export.Geometry;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x02000013 RID: 19
	public abstract class ItMachineDataSlab : ItMachineDataBase
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00008EC0 File Offset: 0x000070C0
		protected ItMachineDataSlab(ICamExportIntOptions options) : base(options)
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00008ECC File Offset: 0x000070CC
		protected override bool ExportUnitechnik(AssemblyInstance assemblyInstance, IEnumerable<RevitElement<Part>> parts, int iProdNo, CNCProjectData projectData)
		{
			Floor floor;
			if (parts == null)
			{
				floor = null;
			}
			else
			{
				RevitElement<Part> revitElement = parts.FirstOrDefault<RevitElement<Part>>();
				floor = ((revitElement != null) ? revitElement.getFloor() : null);
			}
			Floor floor2 = floor;
			bool flag = floor2 == null;
			bool result;
			if (flag)
			{
				ItFailures.PostFailure(ItFailures.UnitechnikFailure, assemblyInstance.Id);
				result = false;
			}
			else
			{
				result = base.ExportUnitechnik(assemblyInstance, parts, iProdNo, projectData);
			}
			return result;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00008F20 File Offset: 0x00007120
		protected internal override void InitializeFromAssembly(AssemblyInstance assemblyInstance)
		{
			ItGeMatrix3d itGeMatrix3d = ItGeMatrix3d.rotation(-1.5707963267948966, ItGeVector3d.kZAxis, ItGePoint3d.kOrigin);
			ItGeBoundBlock3d itGeBoundBlock3d = assemblyInstance.getLocalExtents().transformBy(itGeMatrix3d);
			base.MatAssemblyToPalette = ItGeMatrix3d.translation(itGeBoundBlock3d.minPoint.asVector().negate()) * itGeMatrix3d;
			base.InitializeFromAssembly(assemblyInstance);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00008F80 File Offset: 0x00007180
		protected override MountingPartData getDoorWindowData(RevitElement<FamilyInstance> familyInstance, RevitElement<Part> part)
		{
			return null;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00008F98 File Offset: 0x00007198
		protected override MountingPartData getOpeningData(RevitElement<Opening> opening, RevitElement<Part> part)
		{
			Floor floor = part.getFloor();
			ItGeVector3d viewDirection = -floor.normal(false);
			return base.GetOpeningData(opening, part, viewDirection);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00008FC8 File Offset: 0x000071C8
		protected override ItGeVector3d getShiftingDirection()
		{
			return ItGeVector3d.kYAxis;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00008FE0 File Offset: 0x000071E0
		protected override ItGeVector3d getSpanDirection()
		{
			return ItGeVector3d.kXAxis;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00008FF8 File Offset: 0x000071F8
		public override double getThickness(ItGeBoundBlock3d bb)
		{
			ItDebug.assert(bb.isNotNull(), "input parameter is null");
			return bb.maxPoint.z - bb.minPoint.z;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00009034 File Offset: 0x00007234
		protected override double getAssemblyThickness(ItGeBoundBlock3d bb)
		{
			ItDebug.assert(bb.isNotNull(), "input parameter is null");
			return bb.maxPoint.z - bb.minPoint.z;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00009070 File Offset: 0x00007270
		protected override bool IsMountPartOfThisShell(RevitElement<FamilyInstance> mountingPart, RevitElement<Part> revitElement)
		{
			return true;
		}
	}
}
