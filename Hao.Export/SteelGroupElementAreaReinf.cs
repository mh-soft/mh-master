using System;
using System.Collections.Generic;
using Hao.Export.Geometry;
using Hao.Export.Reinforcement;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;

namespace Hao.Export.MachineData
{
	// Token: 0x02000023 RID: 35
	public class SteelGroupElementAreaReinf : SteelGroupElementRebarBase
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000ACBC File Offset: 0x00008EBC
		// (set) Token: 0x06000189 RID: 393 RVA: 0x0000ACC4 File Offset: 0x00008EC4
		protected RevitElement<RebarInSystem> RebarInSystem { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000ACCD File Offset: 0x00008ECD
		// (set) Token: 0x0600018B RID: 395 RVA: 0x0000ACD5 File Offset: 0x00008ED5
		protected RevitElement<AreaReinforcement> AreaReinforcement { get; set; }

		// Token: 0x0600018C RID: 396 RVA: 0x0000ACDE File Offset: 0x00008EDE
		internal SteelGroupElementAreaReinf(ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette) : base(cncElementData, matWcsToPalette)
		{
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000ACEC File Offset: 0x00008EEC
		internal new static SteelGroupElementAreaReinf GetInstance(RevitElement<RebarInSystem> rebarInSystem, ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette)
		{
			bool flag = rebarInSystem.IsInvalid();
			SteelGroupElementAreaReinf result;
			if (flag)
			{
				result = null;
			}
			else
			{
				SteelGroupElementAreaReinf steelGroupElementAreaReinf = new SteelGroupElementAreaReinf(cncElementData, matWcsToPalette);
				steelGroupElementAreaReinf.RebarInSystem = rebarInSystem;
				steelGroupElementAreaReinf.Host = rebarInSystem.Element.getHostingPart();
				bool flag2 = steelGroupElementAreaReinf.Host == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					steelGroupElementAreaReinf.AreaReinforcement = (rebarInSystem.Document.GetElement(rebarInSystem.Element.SystemId) as AreaReinforcement);
					steelGroupElementAreaReinf.RebarBarType = (rebarInSystem.Document.GetElement(rebarInSystem.Element.GetTypeId()) as RebarBarType);
					steelGroupElementAreaReinf.Data = (steelGroupElementAreaReinf.AreaReinforcement.load<ItReinforcement>(null, false) ?? SteelGroupElement.GetEmptyData(steelGroupElementAreaReinf));
					steelGroupElementAreaReinf.IronSetsSchematic = rebarInSystem.Element.getIronSets(RebarMode.Schematic);
					steelGroupElementAreaReinf.IronSetsRealistic = rebarInSystem.Element.getIronSets(RebarMode.Realistic);
					steelGroupElementAreaReinf.TransformToPalette();
					steelGroupElementAreaReinf.SetMinMaxZ(steelGroupElementAreaReinf.IronSetsRealistic);
					steelGroupElementAreaReinf.SetGroup();
					result = steelGroupElementAreaReinf;
				}
			}
			return result;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000AE00 File Offset: 0x00009000
		internal override void WriteToUnitechnik(ICollection<ExportReinfData> exportedElements)
		{
			bool flag = base.IsMainAxis();
			if (flag)
			{
				base.UnitechnikAddStraightBars(base.IronSetsSchematic, this.AreaReinforcement.Id);
			}
			else
			{
				foreach (List<ItGeCurve3d> item in base.IronSetsSchematic)
				{
					List<List<ItGeCurve3d>> arrayIrons = new List<List<ItGeCurve3d>>
					{
						item
					};
					base.UnitechnikAddStraightBars(arrayIrons, this.AreaReinforcement.Id);
				}
			}
			exportedElements.Add(new ExportReinfData
			{
				Id = this.RebarInSystem.Id,
				ExportedAs = ExportReinfData.ExportType.AsRodstock
			});
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000AECC File Offset: 0x000090CC
		protected override ItGeVector3d GetRebarElementNormal()
		{
			ItGeVector3d itGeVector3d = this.RebarInSystem.Element.Normal.asVector();
			itGeVector3d.transformBy(base.MatWcsToPalette);
			return itGeVector3d;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000AF04 File Offset: 0x00009104
		protected override void SetReinforcementTypeForFloor(int layerIndexInZDir)
		{
			bool flag = base.Data.LayerPosition == PosReinfLayer.Bottom;
			if (flag)
			{
				bool flag2 = layerIndexInZDir == 0;
				if (flag2)
				{
					base.CNCReinfType = SteelGroupElement.CNCReinforcementType.FirstRebarLayer;
				}
				else
				{
					bool flag3 = layerIndexInZDir == 1;
					if (flag3)
					{
						base.CNCReinfType = SteelGroupElement.CNCReinforcementType.SecondRebarLayer;
					}
					else
					{
						base.CNCReinfType = SteelGroupElement.CNCReinforcementType.Other;
					}
				}
			}
			else
			{
				bool flag4 = base.Data.LayerPosition == PosReinfLayer.Top;
				if (flag4)
				{
					bool flag5 = layerIndexInZDir == 0;
					if (flag5)
					{
						base.CNCReinfType = SteelGroupElement.CNCReinforcementType.UpperFirstRebarLayer;
					}
					else
					{
						bool flag6 = layerIndexInZDir == 1;
						if (flag6)
						{
							base.CNCReinfType = SteelGroupElement.CNCReinforcementType.UpperSecondRebarLayer;
						}
						else
						{
							base.CNCReinfType = SteelGroupElement.CNCReinforcementType.UpperOther;
						}
					}
				}
				else
				{
					base.SetReinforcementTypeFromZ();
				}
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000AFA4 File Offset: 0x000091A4
		protected override void SetReinforcementTypeForWall(int layerIndexInZDir)
		{
			bool flag = base.IsLowerReinfInWall();
			if (flag)
			{
				bool flag2 = layerIndexInZDir == 0;
				if (flag2)
				{
					base.CNCReinfType = SteelGroupElement.CNCReinforcementType.FirstRebarLayer;
				}
				else
				{
					bool flag3 = layerIndexInZDir == 1;
					if (flag3)
					{
						base.CNCReinfType = SteelGroupElement.CNCReinforcementType.SecondRebarLayer;
					}
					else
					{
						base.CNCReinfType = SteelGroupElement.CNCReinforcementType.Other;
					}
				}
			}
			else
			{
				bool flag4 = base.IsUpperReinfInWall();
				if (flag4)
				{
					bool flag5 = layerIndexInZDir == 0;
					if (flag5)
					{
						base.CNCReinfType = SteelGroupElement.CNCReinforcementType.UpperFirstRebarLayer;
					}
					else
					{
						bool flag6 = layerIndexInZDir == 1;
						if (flag6)
						{
							base.CNCReinfType = SteelGroupElement.CNCReinforcementType.UpperSecondRebarLayer;
						}
						else
						{
							base.CNCReinfType = SteelGroupElement.CNCReinforcementType.UpperOther;
						}
					}
				}
				else
				{
					bool flag7 = base.Data.LayerPosition == PosReinfLayer.Center;
					if (flag7)
					{
						ItDebug.assert(false, "Reinforcement in center position. Autodesk must make a decision how this should be handled. For now, geometrically added to lower or upper reinforcement.");
						base.SetReinforcementTypeFromZ();
					}
					else
					{
						base.SetReinforcementTypeFromZ();
					}
				}
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000B05E File Offset: 0x0000925E
		protected override ElementId RebarElementId
		{
			get
			{
				RevitElement<AreaReinforcement> areaReinforcement = this.AreaReinforcement;
				return (areaReinforcement != null) ? areaReinforcement.Id : null;
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000B072 File Offset: 0x00009272
		protected override void GetSpacingAndCountForUnitechnik(short angle, out int ironCount, out double ironSpacing)
		{
			ironCount = this.RebarInSystem.Element.Quantity;
			ironSpacing = this.GetActualSpacingForUnitechnik(this.RebarInSystem, angle);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000B098 File Offset: 0x00009298
		protected double GetActualSpacingForUnitechnik(RevitElement<RebarInSystem> rebar, short angle)
		{
			int numberOfBarPositions = rebar.Element.NumberOfBarPositions;
			bool flag = numberOfBarPositions < 2;
			double result;
			if (flag)
			{
				result = 0.0;
			}
			else
			{
				bool flag2 = rebar.Element.DoesBarExistAtPosition(0);
				bool flag3 = !flag2 && (numberOfBarPositions < 3 || !rebar.Element.DoesBarExistAtPosition(2));
				if (flag3)
				{
					result = 0.0;
				}
				else
				{
					Transform barPositionTransform = rebar.Element.GetBarPositionTransform(flag2 ? 0 : 1);
					Transform barPositionTransform2 = rebar.Element.GetBarPositionTransform(flag2 ? 1 : 2);
					result = base.GetActualSpacingForUnitechnik(barPositionTransform, barPositionTransform2, angle);
				}
			}
			return result;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000B13C File Offset: 0x0000933C
		public override double GetSteelVolumeInPart(RevitElement<Part> part, ItSolid partSolid)
		{
			Part hostingPart = this.RebarInSystem.Element.getHostingPart();
			bool flag = hostingPart != null && part.Id == hostingPart.Id;
			double result;
			if (flag)
			{
				result = this.RebarInSystem.Element.Volume;
			}
			else
			{
				result = 0.0;
			}
			return result;
		}
	}
}
