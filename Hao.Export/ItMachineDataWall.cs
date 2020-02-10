using System;
using System.Collections.Generic;
using System.Linq;
using Hao.Export.Geometry;
using Hao.Export.PrecastWall;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x02000017 RID: 23
	public abstract class ItMachineDataWall : ItMachineDataBase
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00008EC0 File Offset: 0x000070C0
		protected ItMachineDataWall(ICamExportIntOptions options) : base(options)
		{
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00009164 File Offset: 0x00007364
		protected override bool ExportUnitechnik(AssemblyInstance assemblyInstance, IEnumerable<RevitElement<Part>> parts, int iProdNo, CNCProjectData projectData)
		{
			Wall wall;
			if (parts == null)
			{
				wall = null;
			}
			else
			{
				RevitElement<Part> revitElement = parts.FirstOrDefault<RevitElement<Part>>();
				wall = ((revitElement != null) ? revitElement.getWall() : null);
			}
			Wall wall2 = wall;
			bool flag = wall2 == null;
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

		// Token: 0x060000E9 RID: 233 RVA: 0x000091B8 File Offset: 0x000073B8
		protected internal override void InitializeFromAssembly(AssemblyInstance assemblyInstance)
		{
			bool flag = assemblyInstance == null;
			if (!flag)
			{
				Wall wall = assemblyInstance.getMainElement() as Wall;
				ItGeMatrix3d mat = ItGeMatrix3d.rotation(-1.5707963267948966, ItGeVector3d.kXAxis, ItGePoint3d.kOrigin);
				ItGeMatrix3d itGeMatrix3d = ItGeMatrix3d.translation(new ItGeVector3d(0.0, 0.0, wall.thickness()));
				Position position = Position.Undefined;
				RevitElement<Part> part = assemblyInstance.getPart(null);
				bool flag2 = part.isNotNull();
				if (flag2)
				{
					position = part.getViewsidePosition();
				}
				ItGeMatrix3d mat2 = ItGeMatrix3d.kIdentity;
				bool flag3 = position != Position.Right;
				if (flag3)
				{
					ItGeBoundBlock3d localExtents = assemblyInstance.getLocalExtents();
					ItGePoint3d itGePoint3d = localExtents.midPoint;
					itGePoint3d = itGeMatrix3d * mat * itGePoint3d;
					mat2 = ItGeMatrix3d.rotation(3.1415926535897931, ItGeVector3d.kYAxis, itGePoint3d);
				}
				base.MatAssemblyToPalette = mat2 * itGeMatrix3d * mat;
				base.InitializeFromAssembly(assemblyInstance);
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000092AC File Offset: 0x000074AC
		protected override MountingPartData getDoorWindowData(RevitElement<FamilyInstance> familyInstance, RevitElement<Part> part)
		{
			ItGeBoundBlock3d localExtents = familyInstance.Element.getLocalExtents();
			ItGeVector3d viewDirection = base.getViewDirection();
			ItGeMatrix3d ecsToWcs = familyInstance.Element.ecs();
			bool flag;
			List<ItGePoint3d> contourFromElement = ItMachineDataBase.getContourFromElement(familyInstance.To<Element>(), part, ecsToWcs, viewDirection, out flag);
			bool flag2 = contourFromElement.none<ItGePoint3d>() || contourFromElement.Count < 3;
			MountingPartData result;
			if (flag2)
			{
				ItFailures.PostFailure(ItFailures.CAMObjectWithoutValidGeometry, familyInstance.Id);
				result = null;
			}
			else
			{
				contourFromElement.transformBy(base.MatWcsToPalette);
				localExtents.transformBy(base.MatWcsToPalette);
				MountingPartData mountingPartData = new MountingPartData
				{
					ElementId = familyInstance.Id,
					Contour = contourFromElement,
					Height = localExtents.maxPoint.z - localExtents.minPoint.z,
					IsOpeningWithoutGeometry = !flag,
					Type = MountingPartData.Types.DoorWindow,
					InstallationHeight = localExtents.minPoint.z,
					Name = familyInstance.Element.Name,
					UniqueId = familyInstance.UniqueId
				};
				result = mountingPartData;
			}
			return result;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000093D0 File Offset: 0x000075D0
		protected override MountingPartData getOpeningData(RevitElement<Opening> opening, RevitElement<Part> part)
		{
			Wall wall = part.getWall();
			ItGeVector3d viewDirection = wall.yAxis();
			return base.GetOpeningData(opening, part, viewDirection);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000093FC File Offset: 0x000075FC
		protected override ItGeVector3d getShiftingDirection()
		{
			return ItGeVector3d.kYAxis;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00009414 File Offset: 0x00007614
		protected override ItGeVector3d getSpanDirection()
		{
			return ItGeVector3d.kXAxis;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000942C File Offset: 0x0000762C
		public override double getThickness(ItGeBoundBlock3d bb)
		{
			ItDebug.assert(bb.isNotNull(), "input parameter is null");
			return bb.maxPoint.y - bb.minPoint.y;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00009468 File Offset: 0x00007668
		protected override double getAssemblyThickness(ItGeBoundBlock3d bb)
		{
			ItDebug.assert(bb.isNotNull(), "input parameter is null");
			return bb.maxPoint.y - bb.minPoint.y;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000094A4 File Offset: 0x000076A4
		protected override bool IsConnectionFamily(FamilyInstance famInst)
		{
			ItConnectionElementData o = ItMachineDataBase.LoadOrParentLoad<ItConnectionElementData>(famInst, ItBase.guid);
			return o.isNotNull();
		}
	}
}
