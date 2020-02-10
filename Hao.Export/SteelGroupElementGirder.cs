using System;
using System.Collections.Generic;
using Hao.Export.Geometry;
using Hao.Export.MachineData.PXML;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x02000026 RID: 38
	public class SteelGroupElementGirder : SteelGroupElement
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000BF89 File Offset: 0x0000A189
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000BF91 File Offset: 0x0000A191
		public RevitElement<FamilyInstance> Girder { get; private set; }

		// Token: 0x060001C1 RID: 449 RVA: 0x0000BD1A File Offset: 0x00009F1A
		internal SteelGroupElementGirder(ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette) : base(cncElementData, matWcsToPalette)
		{
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000BF9C File Offset: 0x0000A19C
		internal new static SteelGroupElementGirder GetInstance(RevitElement<FamilyInstance> girder, ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette, List<SteelGroupElement> existingElements)
		{
			bool flag = girder.IsInvalid();
			SteelGroupElementGirder result;
			if (flag)
			{
				result = null;
			}
			else
			{
				SteelGroupElementGirder steelGroupElementGirder = new SteelGroupElementGirder(cncElementData, matWcsToPalette);
				steelGroupElementGirder.Girder = girder;
				steelGroupElementGirder.Host = girder.Element.getHostingPart();
				bool flag2 = steelGroupElementGirder.Host == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					steelGroupElementGirder.Data = SteelGroupElementGirder.GetReinforcementDataInstance(girder, steelGroupElementGirder);
					bool flag3 = !SteelGroupElementGirder.IsGirderData(steelGroupElementGirder.Data);
					if (flag3)
					{
						result = null;
					}
					else
					{
						steelGroupElementGirder.SetMinMaxZ();
						steelGroupElementGirder.SetGroup();
						result = steelGroupElementGirder;
					}
				}
			}
			return result;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000C02C File Offset: 0x0000A22C
		private static ItReinforcement GetReinforcementDataInstance(RevitElement<FamilyInstance> girder, SteelGroupElementGirder result)
		{
			ItReinforcement itReinforcement = girder.load<ItReinforcement>(null, false) ?? SteelGroupElement.GetEmptyData(result);
			bool flag = SteelGroupElementGirder.IsManualGirder(girder, itReinforcement);
			if (flag)
			{
				itReinforcement = SteelGroupElementGirder.GetManualGirderData(girder);
			}
			return itReinforcement;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000C06C File Offset: 0x0000A26C
		private static ItReinforcement GetManualGirderData(RevitElement<FamilyInstance> girder)
		{
			return new ItReinforcementAreaGirder();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000C084 File Offset: 0x0000A284
		private static bool IsManualGirder(RevitElement<FamilyInstance> girder, ItReinforcement data)
		{
			return data == null && SteelGroupElementGirder.IsGirderInstance(girder);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000C0A4 File Offset: 0x0000A2A4
		private static bool IsGirderInstance(RevitElement<FamilyInstance> girder)
		{
			return false;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
		private static bool IsGirderData(ItReinforcement data)
		{
			ItReinforcementAreaGirder itReinforcementAreaGirder = data as ItReinforcementAreaGirder;
			ItReinforcementLintelGirder itReinforcementLintelGirder = data as ItReinforcementLintelGirder;
			return itReinforcementAreaGirder != null || itReinforcementLintelGirder != null;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		private void SetMinMaxZ()
		{
			ItGeBoundBlock3d localExtents = this.Girder.Element.getLocalExtents();
			localExtents.transformBy(base.MatWcsToPalette * this.Girder.Element.ecs());
			base.MinZ = localExtents.minPoint.z;
			base.MaxZ = localExtents.maxPoint.z;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000C14C File Offset: 0x0000A34C
		internal override void WriteToPXML(ItSteel steelBlock, ICollection<ExportReinfData> exportedElements)
		{
			ItBracedGirderData itBracedGirderData = new ItBracedGirderData(this.Girder, base.MatWcsToPalette, 0.0);
			ItBracedGirderData.DataInFeet cncdata = itBracedGirderData.GetCNCData(1, base.MinZ);
			ItGirder itGirder = new ItGirder();
			itGirder.PieceCount = 1;
			itGirder.X = steelBlock.OffsetX(cncdata.X);
			itGirder.Y = steelBlock.OffsetY(cncdata.Y);
			itGirder.Z = steelBlock.OffsetZ(cncdata.InstallationHeight);
			itGirder.GirderName = this.Girder.Element.Name;
			itGirder.Length = cncdata.Length;
			itGirder.AngleToX = Math.Round(ItAngle.toDegree(cncdata.Angle), 10);
			itGirder.Height = cncdata.Height;
			itGirder.Weight = cncdata.Weight;
			itGirder.GirderType = 0;
			itGirder.MountingType = 0;
			itGirder.Width = 0.0;
			steelBlock.girderList.Add(itGirder);
			exportedElements.Add(new ExportReinfData
			{
				Id = this.Girder.Id,
				ExportedAs = ExportReinfData.ExportType.AsGirder
			});
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000C278 File Offset: 0x0000A478
		internal override void WriteToUnitechnik(ICollection<ExportReinfData> exportedElements)
		{
			ItBracedGirderData item = new ItBracedGirderData(this.Girder, base.MatWcsToPalette, 0.0);
			base.CNCElementData.Girders.Add(item);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000051E6 File Offset: 0x000033E6
		public override void SetReinforcementType()
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000C2B4 File Offset: 0x0000A4B4
		public override IEnumerable<ItGePoint3d> GetPoints(bool schematic)
		{
			ItGeBoundBlock3d localExtents = this.Girder.Element.getLocalExtents();
			ItGeMatrix3d mat = base.MatWcsToPalette * this.Girder.Element.ecs();
			ItGeBoundBlock3d itGeBoundBlock3d = localExtents.transformBy(mat);
			return new List<ItGePoint3d>
			{
				itGeBoundBlock3d.minPoint,
				itGeBoundBlock3d.maxPoint
			};
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000C31E File Offset: 0x0000A51E
		public override void TransformBy(ItGeMatrix3d transform)
		{
			base.MatWcsToPalette = transform * base.MatWcsToPalette;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000C334 File Offset: 0x0000A534
		public override double GetSteelVolumeInPart(RevitElement<Part> part, ItSolid partSolid)
		{
			List<ItSolid> solidListFromFamilyInstance = ItSolid.getSolidListFromFamilyInstance(this.Girder.Element, this.Girder.Element.ecs(), false, null, false);
			double num = 0.0;
			foreach (ItSolid itSolid in solidListFromFamilyInstance)
			{
				Solid solid = partSolid.Solid.intersect(itSolid.Solid);
				bool flag = solid != null && solid.Volume.Gt(0.0, -1.0);
				if (flag)
				{
					num += solid.Volume;
				}
			}
			return num;
		}
	}
}
