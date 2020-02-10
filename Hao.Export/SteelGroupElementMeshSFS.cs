using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Hao.Export.Geometry;
using Hao.Export.MachineData.PXML;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;

namespace Hao.Export.MachineData
{
	// Token: 0x02000029 RID: 41
	public class SteelGroupElementMeshSFS : SteelGroupElementMeshBase
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000F54D File Offset: 0x0000D74D
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000F555 File Offset: 0x0000D755
		public int Count { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000F55E File Offset: 0x0000D75E
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000F566 File Offset: 0x0000D766
		public double Weight { get; private set; }

		// Token: 0x06000234 RID: 564 RVA: 0x0000B196 File Offset: 0x00009396
		internal SteelGroupElementMeshSFS(ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette) : base(cncElementData, matWcsToPalette)
		{
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000F570 File Offset: 0x0000D770
		internal new static SteelGroupElementMeshSFS GetInstance(RevitElement<FabricSheet> fabricSheet, ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette)
		{
			bool flag = fabricSheet.IsInvalid() || SteelGroupElementMeshBase.IsCFS(fabricSheet);
			SteelGroupElementMeshSFS result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FabricSheetType sheetType = SteelGroupElementMeshBase.GetSheetType(fabricSheet.Element);
				SteelGroupElementMeshSFS steelGroupElementMeshSFS = new SteelGroupElementMeshSFS(cncElementData, matWcsToPalette);
				steelGroupElementMeshSFS.FabricSheet = fabricSheet;
				steelGroupElementMeshSFS.Host = fabricSheet.Element.getHostingPart();
				bool flag2 = steelGroupElementMeshSFS.Host == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					steelGroupElementMeshSFS.FabricArea = (fabricSheet.Document.GetElement(fabricSheet.Element.FabricAreaOwnerId) as FabricArea);
					SteelGroupElement steelGroupElement = steelGroupElementMeshSFS;
					RevitElement<FabricArea> fabricArea = steelGroupElementMeshSFS.FabricArea;
					steelGroupElement.Data = (((fabricArea != null) ? fabricArea.load<ItReinforcement>(null, false) : null) ?? SteelGroupElement.GetEmptyData(steelGroupElementMeshSFS));
					steelGroupElementMeshSFS.WireItemsMajor = steelGroupElementMeshSFS.getWireItems(fabricSheet.Element, 0, sheetType).ToList<WireItem>();
					steelGroupElementMeshSFS.WireItemsMinor = steelGroupElementMeshSFS.getWireItems(fabricSheet.Element, (WireDistributionDirection)1, sheetType).ToList<WireItem>();
					steelGroupElementMeshSFS._type = 1;
					steelGroupElementMeshSFS.Count = 1;
					steelGroupElementMeshSFS.Weight = SteelGroupElementMeshSFS.GetWeight(fabricSheet.Element);
					steelGroupElementMeshSFS._name = fabricSheet.Element.Name;
					steelGroupElementMeshSFS._typeId = fabricSheet.Element.GetTypeId();
					steelGroupElementMeshSFS.MatWcsToPalette = ItGeMatrix3d.kIdentity;
					steelGroupElementMeshSFS.PosPalette = ItGePoint3d.kOrigin;
					steelGroupElementMeshSFS.SetMinMaxZ();
					steelGroupElementMeshSFS.SetGroup();
					result = steelGroupElementMeshSFS;
				}
			}
			return result;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000F6E0 File Offset: 0x0000D8E0
		private static double GetWeight(FabricSheet sheet)
		{
			return Math.Round(sheet.CutSheetMass, 3);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000F6FE File Offset: 0x0000D8FE
		protected void SetMinMaxZ()
		{
			base.MinZ = 0.0;
			base.MaxZ = 0.0;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000F724 File Offset: 0x0000D924
		internal override void WriteToPXML(ItSteel steelBlock, ICollection<ExportReinfData> exportedElements)
		{
			ItSteelExt itSteelExt = new ItSteelExt();
			itSteelExt.type = "01";
			itSteelExt.Info = CNCDataBase.paddedInt5(this.Count) + " " + this.Weight.ToString("000.000", CultureInfo.InvariantCulture);
			FabricSheetType sheetType = SteelGroupElementMeshBase.GetSheetType(base.FabricSheet.Element);
			itSteelExt.SheetType = (((sheetType != null) ? sheetType.Name : null) ?? string.Empty);
			itSteelExt.Count = this.Count;
			itSteelExt.Weight = Math.Round(this.Weight, 3);
			steelBlock.steelExtList.Add(itSteelExt);
			exportedElements.Add(new ExportReinfData
			{
				Id = base.FabricSheet.Id,
				ExportedAs = ExportReinfData.ExportType.AsExtIron
			});
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000F7FC File Offset: 0x0000D9FC
		public static List<SteelGroupElementMeshSFS> GroupSheets(IEnumerable<SteelGroupElementMeshSFS> sheets)
		{
			List<SteelGroupElementMeshSFS> list = new List<SteelGroupElementMeshSFS>();
			IEnumerable<SteelGroupElementMeshSFS> collection = SteelGroupElementMeshSFS.GroupSheetsImpl(sheets);
			list.AddRange(collection);
			return list;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000F824 File Offset: 0x0000DA24
		private static IEnumerable<SteelGroupElementMeshSFS> GroupSheetsImpl(IEnumerable<SteelGroupElementMeshSFS> sheets)
		{
			Dictionary<ElementId, List<SteelGroupElementMeshSFS>> dictionary = new Dictionary<ElementId, List<SteelGroupElementMeshSFS>>();
			foreach (SteelGroupElementMeshSFS steelGroupElementMeshSFS in sheets)
			{
				SteelGroupElementMeshSFS.AddToSheetDict(dictionary, steelGroupElementMeshSFS._typeId, steelGroupElementMeshSFS);
			}
			bool flag = dictionary.Values.none<List<SteelGroupElementMeshSFS>>();
			IEnumerable<SteelGroupElementMeshSFS> result;
			if (flag)
			{
				result = Enumerable.Empty<SteelGroupElementMeshSFS>();
			}
			else
			{
				IEnumerable<SteelGroupElementMeshSFS> enumerable = SteelGroupElementMeshSFS.AccumulateSheets(dictionary);
				result = enumerable;
			}
			return result;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000F8A8 File Offset: 0x0000DAA8
		private static IEnumerable<SteelGroupElementMeshSFS> AccumulateSheets(Dictionary<ElementId, List<SteelGroupElementMeshSFS>> dict)
		{
			Func<SteelGroupElementMeshSFS, SteelGroupElementMeshSFS, SteelGroupElementMeshSFS> sumSheets = delegate(SteelGroupElementMeshSFS total, SteelGroupElementMeshSFS next)
			{
				total.Count += next.Count;
				total.Weight += next.Weight;
				total.WireItemsMajor.AddRange(next.WireItemsMajor);
				total.WireItemsMinor.AddRange(next.WireItemsMinor);
				return total;
			};
			Func<List<SteelGroupElementMeshSFS>, SteelGroupElementMeshSFS> selector = delegate(List<SteelGroupElementMeshSFS> list)
			{
				SteelGroupElementMeshSFS steelGroupElementMeshSFS = list.FirstOrDefault<SteelGroupElementMeshSFS>();
				ItDebug.assert(steelGroupElementMeshSFS != null, "if a list is added to the dict, at least one sheet must be added to the list!");
				SteelGroupElementMeshSFS instance = SteelGroupElementMeshSFS.GetInstance(steelGroupElementMeshSFS.FabricSheet, steelGroupElementMeshSFS.CNCElementData, steelGroupElementMeshSFS.MatWcsToPalette);
				instance.Weight = 0.0;
				instance.Count = 0;
				instance.WireItemsMajor.Clear();
				instance.WireItemsMinor.Clear();
				return list.Aggregate(instance, sumSheets);
			};
			return dict.Values.Select(selector);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000F900 File Offset: 0x0000DB00
		private static void AddToSheetDict(Dictionary<ElementId, List<SteelGroupElementMeshSFS>> dict, ElementId typeId, SteelGroupElementMeshSFS sheet)
		{
			List<SteelGroupElementMeshSFS> list;
			bool flag = !dict.TryGetValue(typeId, out list);
			if (flag)
			{
				list = new List<SteelGroupElementMeshSFS>();
				dict[typeId] = list;
			}
			list.Add(sheet);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000F938 File Offset: 0x0000DB38
		internal override void WriteToUnitechnik(ICollection<ExportReinfData> exportedElements)
		{
			ItUniWrapperImpl.AddStandardFabricSheet(this._type, this.Count, this.Weight, this._name);
			exportedElements.Add(new ExportReinfData
			{
				Id = base.FabricSheet.Id,
				ExportedAs = ExportReinfData.ExportType.AsExtIron
			});
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000051E6 File Offset: 0x000033E6
		public override void SetReinforcementType()
		{
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000F990 File Offset: 0x0000DB90
		public override IEnumerable<ItGePoint3d> GetPoints(bool schematic)
		{
			return new List<ItGePoint3d>();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000051E6 File Offset: 0x000033E6
		public override void TransformBy(ItGeMatrix3d transform)
		{
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000F9A8 File Offset: 0x0000DBA8
		public override double GetSteelVolumeInPart(RevitElement<Part> part, ItSolid partSolid)
		{
			IEnumerable<double> source = from wire in base.WireItemsMinor.Union(base.WireItemsMajor)
			select wire.GetSteelVolumeInPart(partSolid);
			return source.Sum();
		}

		// Token: 0x04000084 RID: 132
		private int _type;

		// Token: 0x04000087 RID: 135
		private string _name;

		// Token: 0x04000088 RID: 136
		private ElementId _typeId;
	}
}
