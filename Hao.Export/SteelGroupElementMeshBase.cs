using System;
using System.Collections.Generic;
using System.Linq;
using Hao.Export.Geometry;
using Hao.Export.MachineData.PXML;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;

namespace Hao.Export.MachineData
{
	// Token: 0x02000025 RID: 37
	public abstract class SteelGroupElementMeshBase : SteelGroupElement
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000BCD6 File Offset: 0x00009ED6
		// (set) Token: 0x060001AD RID: 429 RVA: 0x0000BCDE File Offset: 0x00009EDE
		public List<WireItem> WireItemsMajor { get; protected set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000BCE7 File Offset: 0x00009EE7
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000BCEF File Offset: 0x00009EEF
		public List<WireItem> WireItemsMinor { get; protected set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000BCF8 File Offset: 0x00009EF8
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000BD00 File Offset: 0x00009F00
		public AssemblyInstance Assembly { get; protected set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000BD09 File Offset: 0x00009F09
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000BD11 File Offset: 0x00009F11
		public ItGePoint3d PosPalette { get; protected set; }

		// Token: 0x060001B4 RID: 436 RVA: 0x0000BD1A File Offset: 0x00009F1A
		internal SteelGroupElementMeshBase(ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette) : base(cncElementData, matWcsToPalette)
		{
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000BD26 File Offset: 0x00009F26
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x0000BD2E File Offset: 0x00009F2E
		protected RevitElement<FabricSheet> FabricSheet { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000BD37 File Offset: 0x00009F37
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x0000BD3F File Offset: 0x00009F3F
		protected RevitElement<FabricArea> FabricArea { get; set; }

		// Token: 0x060001B9 RID: 441 RVA: 0x0000BD48 File Offset: 0x00009F48
		public IEnumerable<WireItem> getWireItems(FabricSheet sheet, WireDistributionDirection direction, FabricSheetType type)
		{
			bool flag = sheet == null || type == null;
			IEnumerable<WireItem> result;
			if (flag)
			{
				result = new List<WireItem>();
			}
			else
			{
				Document doc = sheet.Document;
				List<Curve> source = sheet.GetWireCenterlines(direction).ToList<Curve>();
				IEnumerable<WireItem> enumerable = source.Select((Curve curve, int index) => new WireItem(doc, curve, type, index, direction, this));
				result = enumerable;
			}
			return result;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000BDC8 File Offset: 0x00009FC8
		public static FabricSheetType GetSheetType(FabricSheet sheet)
		{
			ElementId typeId = sheet.GetTypeId();
			return sheet.Document.GetElement(typeId) as FabricSheetType;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000BDF4 File Offset: 0x00009FF4
		public static bool IsCFS(RevitElement<FabricSheet> sheet)
		{
			FabricSheetType sheetType = SteelGroupElementMeshBase.GetSheetType(sheet.Element);
			return sheetType.IsCustom();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000BE18 File Offset: 0x0000A018
		protected static void AddWireToSteelBlock(ItSteel steelBlock, WireItem wire)
		{
			ItGePoint3d startPoint = wire.StartPoint;
			ItGePoint3d point = startPoint + wire.ParentMesh.PosPalette.asVector();
			ItGePoint3d itGePoint3d = steelBlock.Offset(point);
			ItBar itBar = new ItBar();
			itBar.ShapeMode = ShapeMode.realistic;
			itBar.ReinforcementType = wire.ReinforcementType;
			itBar.SteelQuality = wire.SteelQuality;
			itBar.PieceCount = 1;
			itBar.Diameter = wire.WireDiameter;
			itBar.X = itGePoint3d.x;
			itBar.Y = itGePoint3d.y;
			itBar.Z = itGePoint3d.z;
			itBar.RotZ = SteelGroupElementMeshBase.GetRotZ(wire);
			itBar.ArticleNo = wire.ArticleNumber;
			itBar.NoAutoProd = false;
			SteelGroupElementMeshBase.AddSegmentsToBar(itBar, wire);
			steelBlock.barList.Add(itBar);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000BEE8 File Offset: 0x0000A0E8
		private static void AddSegmentsToBar(ItBar bar, WireItem wire)
		{
			double length = wire.Length;
			double r = 0.0;
			double rotX = 0.0;
			double bendY = 0.0;
			ItGeMatrix3d kIdentity = ItGeMatrix3d.kIdentity;
			ItSegment item = new ItSegment(length, r, rotX, bendY, kIdentity, "normal");
			bar.segmentList.Add(item);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000BF44 File Offset: 0x0000A144
		private static double GetRotZ(WireItem wire)
		{
			ItGeVector3d vec = wire.EndPoint - wire.StartPoint;
			double num = ItGeVector3d.kXAxis.angleTo(vec, ItGeVector3d.kZAxis);
			num = SteelGroupElement.FixAngleRange(num, false);
			return ItAngle.toDegree(num);
		}
	}
}
