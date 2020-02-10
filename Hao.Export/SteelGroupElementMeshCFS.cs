using System;
using System.Collections.Generic;
using System.Linq;
using Hao.Export.Geometry;
using Hao.Export.MachineData.PXML;
using Hao.Export.Reinforcement;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;

namespace Hao.Export.MachineData
{
	// Token: 0x02000024 RID: 36
	public class SteelGroupElementMeshCFS : SteelGroupElementMeshBase
	{
		// Token: 0x06000196 RID: 406 RVA: 0x0000B196 File Offset: 0x00009396
		internal SteelGroupElementMeshCFS(ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette) : base(cncElementData, matWcsToPalette)
		{
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000B1A4 File Offset: 0x000093A4
		internal new static SteelGroupElementMeshCFS GetInstance(RevitElement<FabricSheet> fabricSheet, ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette)
		{
			bool flag = fabricSheet.IsInvalid() || !SteelGroupElementMeshBase.IsCFS(fabricSheet);
			SteelGroupElementMeshCFS result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FabricSheetType sheetType = SteelGroupElementMeshBase.GetSheetType(fabricSheet.Element);
				SteelGroupElementMeshCFS steelGroupElementMeshCFS = new SteelGroupElementMeshCFS(cncElementData, matWcsToPalette);
				steelGroupElementMeshCFS.FabricSheet = fabricSheet;
				steelGroupElementMeshCFS.Host = fabricSheet.Element.getHostingPart();
				bool flag2 = steelGroupElementMeshCFS.Host == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					steelGroupElementMeshCFS.FabricArea = (fabricSheet.Document.GetElement(fabricSheet.Element.FabricAreaOwnerId) as FabricArea);
					SteelGroupElement steelGroupElement = steelGroupElementMeshCFS;
					RevitElement<FabricArea> fabricArea = steelGroupElementMeshCFS.FabricArea;
					steelGroupElement.Data = (((fabricArea != null) ? fabricArea.load<ItReinforcement>(null, false) : null) ?? SteelGroupElement.GetEmptyData(steelGroupElementMeshCFS));
					steelGroupElementMeshCFS._cfsName = sheetType.Name;
					steelGroupElementMeshCFS.WireItemsMajor = steelGroupElementMeshCFS.getWireItems(fabricSheet.Element, (WireDistributionDirection)0, sheetType).ToList<WireItem>();
					steelGroupElementMeshCFS.WireItemsMinor = steelGroupElementMeshCFS.getWireItems(fabricSheet.Element, (WireDistributionDirection)1, sheetType).ToList<WireItem>();
					ItGeBoundBlock3d itGeBoundBlock3d = SteelGroupElementMeshCFS.sheetLocalExtents(steelGroupElementMeshCFS.WireItemsMajor, steelGroupElementMeshCFS.WireItemsMinor);
					bool flag3 = itGeBoundBlock3d == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						steelGroupElementMeshCFS.PosPalette = itGeBoundBlock3d.minPoint;
						steelGroupElementMeshCFS._maxLength = itGeBoundBlock3d.length;
						steelGroupElementMeshCFS._maxWidth = itGeBoundBlock3d.width;
						steelGroupElementMeshCFS._toTurn = 0;
						steelGroupElementMeshCFS._stopOnTurn = 0;
						steelGroupElementMeshCFS._matType = 0;
						steelGroupElementMeshCFS.WireItemsMajor.ForEach(delegate(WireItem item)
						{
							item.transformToSteelmat();
						});
						steelGroupElementMeshCFS.WireItemsMinor.ForEach(delegate(WireItem item)
						{
							item.transformToSteelmat();
						});
						bool flag4 = steelGroupElementMeshCFS.Data.LayerPosition == PosReinfLayer.Undefined;
						if (flag4)
						{
							steelGroupElementMeshCFS.SetLayerPositionFromCFS();
						}
						steelGroupElementMeshCFS.SetMinMaxZ();
						steelGroupElementMeshCFS.SetGroup();
						result = steelGroupElementMeshCFS;
					}
				}
			}
			return result;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000B398 File Offset: 0x00009598
		private static ItGeBoundBlock3d sheetLocalExtents(List<WireItem> wireItemsMajor, List<WireItem> wireItemsMinor)
		{
			ItGeBoundBlock3d result = null;
			ItGeVector3d xVector;
			ItGeVector3d xVector2;
			ItGeVector3d normal;
			bool directions = SteelGroupElementMeshCFS.getDirections(wireItemsMajor, wireItemsMinor, out xVector, out xVector2, out normal);
			if (directions)
			{
				foreach (WireItem wireItem in wireItemsMajor)
				{
					SteelGroupElementMeshCFS.addWireToBoundBlock(ref result, wireItem, xVector, normal);
				}
				foreach (WireItem wireItem2 in wireItemsMinor)
				{
					SteelGroupElementMeshCFS.addWireToBoundBlock(ref result, wireItem2, xVector2, normal);
				}
			}
			return result;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000B458 File Offset: 0x00009658
		private static void addWireToBoundBlock(ref ItGeBoundBlock3d result, WireItem wireItem, ItGeVector3d xVector, ItGeVector3d normal)
		{
			double scl = wireItem.WireDiameter / 2.0;
			ItGePoint3d[] array = new ItGePoint3d[]
			{
				wireItem.StartPoint + xVector * scl,
				wireItem.StartPoint - xVector * scl,
				wireItem.StartPoint + normal * scl,
				wireItem.StartPoint - normal * scl,
				wireItem.EndPoint + xVector * scl,
				wireItem.EndPoint - xVector * scl,
				wireItem.EndPoint + normal * scl,
				wireItem.EndPoint - normal * scl
			};
			foreach (ItGePoint3d point in array)
			{
				SteelGroupElementMeshCFS.addPointToBoundBlock(ref result, point);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000B548 File Offset: 0x00009748
		private static void addPointToBoundBlock(ref ItGeBoundBlock3d result, ItGePoint3d point)
		{
			bool flag = result == null;
			if (flag)
			{
				result = new ItGeBoundBlock3d(point, point);
				result.setToBox(false);
			}
			else
			{
				result.extend(point);
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000B580 File Offset: 0x00009780
		private static bool getDirections(List<WireItem> wireItemsMajor, List<WireItem> wireItemsMinor, out ItGeVector3d xVector, out ItGeVector3d yVector, out ItGeVector3d normal)
		{
			xVector = ItGeVector3d.kOrigin;
			yVector = ItGeVector3d.kOrigin;
			normal = ItGeVector3d.kOrigin;
			bool flag = wireItemsMajor.none<WireItem>() || wireItemsMinor.none<WireItem>();
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				ItGePoint3d startPoint = wireItemsMajor[0].StartPoint;
				ItGePoint3d endPoint = wireItemsMajor[0].EndPoint;
				ItGePoint3d startPoint2 = wireItemsMinor[0].StartPoint;
				ItGePoint3d endPoint2 = wireItemsMinor[0].EndPoint;
				xVector = (endPoint2 - startPoint2).normalize();
				yVector = (endPoint - startPoint).normalize();
				normal = xVector.crossProduct(yVector);
				result = true;
			}
			return result;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000B624 File Offset: 0x00009824
		public double GetDiameterMajor()
		{
			return base.WireItemsMajor.Max((WireItem item) => item.WireDiameter);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000B664 File Offset: 0x00009864
		public double GetDiameterMinor()
		{
			return base.WireItemsMinor.Max((WireItem item) => item.WireDiameter);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000B6A4 File Offset: 0x000098A4
		private void SetLayerPositionFromCFS()
		{
			bool flag = base.FabricSheet.Element.FabricLocation == (FabricLocation)1;
			if (flag)
			{
				bool flag2 = base.Host.Element.productType().isWall();
				if (flag2)
				{
					base.Data.LayerPosition = PosReinfLayer.Inside;
				}
				else
				{
					base.Data.LayerPosition = PosReinfLayer.Bottom;
				}
			}
			else
			{
				bool flag3 = base.Host.Element.productType().isWall();
				if (flag3)
				{
					base.Data.LayerPosition = PosReinfLayer.Outside;
				}
				else
				{
					base.Data.LayerPosition = PosReinfLayer.Top;
				}
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000B739 File Offset: 0x00009939
		protected void SetMinMaxZ()
		{
			base.MinZ = base.PosPalette.z;
			base.MaxZ = base.MinZ + this.GetDiameterMajor() + this.GetDiameterMinor();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000B76C File Offset: 0x0000996C
		internal override void WriteToPXML(ItSteel steelBlock, ICollection<ExportReinfData> exportedElements)
		{
			IEnumerable<WireItem> enumerable = base.WireItemsMajor.Union(base.WireItemsMinor);
			foreach (WireItem wire in enumerable)
			{
				SteelGroupElementMeshBase.AddWireToSteelBlock(steelBlock, wire);
			}
			exportedElements.Add(new ExportReinfData
			{
				Id = base.FabricSheet.Id,
				ExportedAs = ExportReinfData.ExportType.AsCFS
			});
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000B7F8 File Offset: 0x000099F8
		internal override void WriteToUnitechnik(ICollection<ExportReinfData> exportedElements)
		{
			int[] pos = CNCDataBase.toPointArray(base.PosPalette);
			int maxLen = CNCDataBase.convertToMM(this._maxLength);
			int maxWidth = CNCDataBase.convertToMM(this._maxWidth);
			ItUniWrapperImpl.AddSteelmat(this._cfsName, maxLen, maxWidth, this._toTurn, this._stopOnTurn, this._matType, pos);
			foreach (WireItem wireItem in base.WireItemsMajor.Union(base.WireItemsMinor))
			{
				wireItem.sendToUniwrapper();
			}
			exportedElements.Add(new ExportReinfData
			{
				Id = base.FabricSheet.Id,
				ExportedAs = ExportReinfData.ExportType.AsCFS
			});
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		public override void SetReinforcementType()
		{
			bool flag = base.Host.productType().isWall();
			if (flag)
			{
				this.SetReinforcementTypeFromWall();
			}
			else
			{
				bool flag2 = base.Host.productType().isFloor();
				if (flag2)
				{
					this.SetReinforcementTypeFromFloor();
				}
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000B914 File Offset: 0x00009B14
		private void SetReinforcementTypeFromFloor()
		{
			bool flag = base.Data.LayerPosition == PosReinfLayer.Bottom;
			if (flag)
			{
				this.SetReinforcementTypeFromPosition(base.Data.LayerPosition, SteelGroupElement.CNCReinforcementType.FirstRebarLayer, SteelGroupElement.CNCReinforcementType.SecondRebarLayer);
			}
			else
			{
				bool flag2 = base.Data.LayerPosition == PosReinfLayer.Top;
				if (flag2)
				{
					this.SetReinforcementTypeFromPosition(base.Data.LayerPosition, SteelGroupElement.CNCReinforcementType.UpperFirstRebarLayer, SteelGroupElement.CNCReinforcementType.UpperSecondRebarLayer);
				}
				else
				{
					this.SetReinforcementTypeFromZ();
				}
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000B97C File Offset: 0x00009B7C
		private void SetReinforcementTypeFromWall()
		{
			bool flag = base.IsLowerReinfInWall();
			if (flag)
			{
				this.SetReinforcementTypeFromPosition(PosReinfLayer.Bottom, SteelGroupElement.CNCReinforcementType.FirstRebarLayer, SteelGroupElement.CNCReinforcementType.SecondRebarLayer);
			}
			else
			{
				bool flag2 = base.IsUpperReinfInWall();
				if (flag2)
				{
					this.SetReinforcementTypeFromPosition(PosReinfLayer.Top, SteelGroupElement.CNCReinforcementType.UpperFirstRebarLayer, SteelGroupElement.CNCReinforcementType.UpperSecondRebarLayer);
				}
				else
				{
					bool flag3 = base.Data.LayerPosition == PosReinfLayer.Center;
					if (flag3)
					{
						ItDebug.assert(false, "Reinforcement in center position. Autodesk must make a decision how this should be handled. For now, geometrically added to lower or upper reinforcement.");
						this.SetReinforcementTypeFromZ();
					}
					else
					{
						this.SetReinforcementTypeFromZ();
					}
				}
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		private void SetReinforcementTypeFromPosition(PosReinfLayer position, SteelGroupElement.CNCReinforcementType outerType, SteelGroupElement.CNCReinforcementType innerType)
		{
			bool flag = base.WireItemsMajor.none<WireItem>() && base.WireItemsMinor.none<WireItem>();
			if (!flag)
			{
				List<WireItem> list;
				List<WireItem> list2;
				this.GetWireSets(position, out list, out list2);
				list.ForEach(delegate(WireItem wire)
				{
					wire.ReinforcementType = (int)outerType;
				});
				list2.ForEach(delegate(WireItem wire)
				{
					wire.ReinforcementType = (int)innerType;
				});
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000BA60 File Offset: 0x00009C60
		private void GetWireSets(PosReinfLayer position, out List<WireItem> outsideWires, out List<WireItem> insideWires)
		{
			double num = double.NaN;
			double num2 = double.NaN;
			bool flag = base.WireItemsMajor.any<WireItem>();
			if (flag)
			{
				num = base.WireItemsMajor.First<WireItem>().StartPoint.z;
			}
			bool flag2 = base.WireItemsMinor.any<WireItem>();
			if (flag2)
			{
				num2 = base.WireItemsMinor.First<WireItem>().StartPoint.z;
			}
			bool flag3 = double.IsNaN(num);
			if (flag3)
			{
				num = ((position == PosReinfLayer.Bottom) ? double.MaxValue : double.MinValue);
			}
			bool flag4 = double.IsNaN(num2);
			if (flag4)
			{
				num2 = ((position == PosReinfLayer.Bottom) ? double.MaxValue : double.MinValue);
			}
			bool flag5 = (num.Gt(num2, -1.0) && position == PosReinfLayer.Top) || (num.Lt(num2, -1.0) && position == PosReinfLayer.Bottom);
			if (flag5)
			{
				outsideWires = base.WireItemsMajor;
				insideWires = base.WireItemsMinor;
			}
			else
			{
				outsideWires = base.WireItemsMinor;
				insideWires = base.WireItemsMajor;
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000BB74 File Offset: 0x00009D74
		private void SetReinforcementTypeFromZ()
		{
			double dY = base.Host.thickness() / 2.0;
			bool flag = base.MaxZ.Le(dY, -1.0);
			if (flag)
			{
				this.SetReinforcementTypeFromPosition(PosReinfLayer.Bottom, SteelGroupElement.CNCReinforcementType.FirstRebarLayer, SteelGroupElement.CNCReinforcementType.SecondRebarLayer);
			}
			else
			{
				this.SetReinforcementTypeFromPosition(PosReinfLayer.Bottom, SteelGroupElement.CNCReinforcementType.UpperFirstRebarLayer, SteelGroupElement.CNCReinforcementType.UpperSecondRebarLayer);
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000BBCC File Offset: 0x00009DCC
		public override IEnumerable<ItGePoint3d> GetPoints(bool schematic)
		{
			List<ItGePoint3d> list = new List<ItGePoint3d>();
			foreach (WireItem wireItem in base.WireItemsMajor.Union(base.WireItemsMinor))
			{
				list.Add(this.TransformToPalette(wireItem.StartPoint));
				list.Add(this.TransformToPalette(wireItem.EndPoint));
			}
			return list;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000BC54 File Offset: 0x00009E54
		private ItGePoint3d TransformToPalette(ItGePoint3d point)
		{
			ItGeVector3d vec = base.PosPalette.asVector();
			return point + vec;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000BC79 File Offset: 0x00009E79
		public override void TransformBy(ItGeMatrix3d transform)
		{
			base.PosPalette.transformBy(transform);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000BC8C File Offset: 0x00009E8C
		public override double GetSteelVolumeInPart(RevitElement<Part> part, ItSolid partSolid)
		{
			IEnumerable<WireItem> source = base.WireItemsMajor.Union(base.WireItemsMinor);
			IEnumerable<double> source2 = from item in source
			select item.GetSteelVolumeInPart(partSolid);
			return source2.Sum();
		}

		// Token: 0x04000071 RID: 113
		private double _maxLength;

		// Token: 0x04000072 RID: 114
		private double _maxWidth;

		// Token: 0x04000073 RID: 115
		private int _toTurn;

		// Token: 0x04000074 RID: 116
		private int _stopOnTurn;

		// Token: 0x04000075 RID: 117
		private int _matType;

		// Token: 0x04000076 RID: 118
		private string _cfsName;
	}
}
