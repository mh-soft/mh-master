using System;
using System.Collections.Generic;
using Hao.Export.Geometry;
using Hao.Export.MachineData.PXML;
using Hao.Export.Reinforcement;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;

namespace Hao.Export.MachineData
{
	// Token: 0x02000022 RID: 34
	public abstract class SteelGroupElement
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000A997 File Offset: 0x00008B97
		// (set) Token: 0x06000167 RID: 359 RVA: 0x0000A99F File Offset: 0x00008B9F
		public ItReinforcement Data { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000A9A8 File Offset: 0x00008BA8
		// (set) Token: 0x06000169 RID: 361 RVA: 0x0000A9B0 File Offset: 0x00008BB0
		public RevitElement<Part> Host { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000A9B9 File Offset: 0x00008BB9
		// (set) Token: 0x0600016B RID: 363 RVA: 0x0000A9C1 File Offset: 0x00008BC1
		public ItGeMatrix3d MatWcsToPalette { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000A9CA File Offset: 0x00008BCA
		// (set) Token: 0x0600016D RID: 365 RVA: 0x0000A9D2 File Offset: 0x00008BD2
		public double MaxZ { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000A9DB File Offset: 0x00008BDB
		// (set) Token: 0x0600016F RID: 367 RVA: 0x0000A9E3 File Offset: 0x00008BE3
		public double MinZ { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000A9EC File Offset: 0x00008BEC
		// (set) Token: 0x06000171 RID: 369 RVA: 0x0000A9F4 File Offset: 0x00008BF4
		public SteelGroupElement.CNCReinforcementType CNCReinfType { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000A9FD File Offset: 0x00008BFD
		public int CNCReinfTypeValue
		{
			get
			{
				return (int)this.CNCReinfType;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000AA05 File Offset: 0x00008C05
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000AA0D File Offset: 0x00008C0D
		public SteelGroup Group { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000AA16 File Offset: 0x00008C16
		internal ItMachineDataBase.CNCElementData CNCElementData { get; }

		// Token: 0x06000176 RID: 374 RVA: 0x0000AA1E File Offset: 0x00008C1E
		internal SteelGroupElement(ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette)
		{
			this.CNCElementData = cncElementData;
			this.MatWcsToPalette = matWcsToPalette;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000AA38 File Offset: 0x00008C38
		internal static SteelGroupElement GetInstance(RevitElement<FamilyInstance> instance, ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette, List<SteelGroupElement> steelGroupElements)
		{
			return SteelGroupElementGirder.GetInstance(instance, cncElementData, matWcsToPalette, steelGroupElements);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000AA58 File Offset: 0x00008C58
		internal static SteelGroupElement GetInstance(RevitElement<Rebar> rebar, ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette, List<SteelGroupElement> steelGroupElements)
		{
			return SteelGroupElementRebar.GetInstance(rebar, cncElementData, matWcsToPalette, steelGroupElements);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000AA78 File Offset: 0x00008C78
		internal static SteelGroupElement GetInstance(RevitElement<RebarInSystem> rebarInSystem, ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette)
		{
			return SteelGroupElementAreaReinf.GetInstance(rebarInSystem, cncElementData, matWcsToPalette);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000AA94 File Offset: 0x00008C94
		internal static SteelGroupElement GetInstance(RevitElement<FabricSheet> sheet, ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette)
		{
			bool flag = SteelGroupElementMeshBase.IsCFS(sheet);
			SteelGroupElement instance;
			if (flag)
			{
				instance = SteelGroupElementMeshCFS.GetInstance(sheet, cncElementData, matWcsToPalette);
			}
			else
			{
				instance = SteelGroupElementMeshSFS.GetInstance(sheet, cncElementData, matWcsToPalette);
			}
			return instance;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		protected static ItReinforcement GetEmptyData(SteelGroupElement steelGroupElement)
		{
			return new ItReinforcement
			{
				reinforcementType = ReinforcementType.NotDefined,
				LayerPosition = PosReinfLayer.Undefined,
				insertPoint = null,
				owner = -1,
				uniqueOwner = null,
				isIdatCreated = false,
				productType = steelGroupElement.Host.productType(),
				resetWithPart = false,
				hostMayNotBeNull = true
			};
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000AB34 File Offset: 0x00008D34
		protected void SetGroup()
		{
			string ownerUid = null;
			PosReinfLayer posReinfLayer = PosReinfLayer.Undefined;
			ReinforcementType precastReinfType = ReinforcementType.NotDefined;
			bool flag = this.Data != null;
			if (flag)
			{
				ownerUid = this.Data.uniqueOwner;
				posReinfLayer = this.Data.LayerPosition;
				precastReinfType = this.Data.reinforcementType;
			}
			this.Group = new SteelGroup(this.Host, ownerUid, posReinfLayer, precastReinfType);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000AB98 File Offset: 0x00008D98
		internal bool TestZPosition(double minZ, double maxZ)
		{
			return minZ.Le(this.MaxZ, -1.0) && maxZ.Ge(this.MinZ, -1.0);
		}

		// Token: 0x0600017E RID: 382
		internal abstract void WriteToPXML(ItSteel steelBlock, ICollection<ExportReinfData> exportedElements);

		// Token: 0x0600017F RID: 383
		internal abstract void WriteToUnitechnik(ICollection<ExportReinfData> exportedElements);

		// Token: 0x06000180 RID: 384
		public abstract void SetReinforcementType();

		// Token: 0x06000181 RID: 385 RVA: 0x0000ABD9 File Offset: 0x00008DD9
		protected void SetReinforcementTypeUnknown()
		{
			this.CNCReinfType = SteelGroupElement.CNCReinforcementType.NoDefinition;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000ABE4 File Offset: 0x00008DE4
		protected bool IsLowerReinfInWall()
		{
			Position viewsidePosition = this.Host.getViewsidePosition();
			PosReinfLayer layerPosition = this.Data.LayerPosition;
			return (viewsidePosition == Position.Left && layerPosition == PosReinfLayer.Inside) || (viewsidePosition == Position.Right && layerPosition == PosReinfLayer.Outside);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000AC28 File Offset: 0x00008E28
		protected bool IsUpperReinfInWall()
		{
			Position viewsidePosition = this.Host.getViewsidePosition();
			PosReinfLayer layerPosition = this.Data.LayerPosition;
			return (viewsidePosition == Position.Right && layerPosition == PosReinfLayer.Inside) || (viewsidePosition == Position.Left && layerPosition == PosReinfLayer.Outside);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000AC6C File Offset: 0x00008E6C
		protected static double FixAngleRange(double angle, bool flipFutureYs = false)
		{
			bool flag = angle.Gt(3.1415926535897931, -1.0);
			if (flag)
			{
				angle = -6.2831853071795862 + angle;
			}
			if (flipFutureYs)
			{
				angle *= -1.0;
			}
			return angle;
		}

		// Token: 0x06000185 RID: 389
		public abstract IEnumerable<ItGePoint3d> GetPoints(bool schematic);

		// Token: 0x06000186 RID: 390
		public abstract void TransformBy(ItGeMatrix3d transform);

		// Token: 0x06000187 RID: 391
		public abstract double GetSteelVolumeInPart(RevitElement<Part> part, ItSolid partSolid);

		// Token: 0x04000066 RID: 102
		protected const int conversionRevitToSteelQuality = 304800;

		// Token: 0x0200006E RID: 110
		public enum CNCReinforcementType
		{
			// Token: 0x0400027F RID: 639
			NoDefinition,
			// Token: 0x04000280 RID: 640
			FirstRebarLayer,
			// Token: 0x04000281 RID: 641
			SecondRebarLayer,
			// Token: 0x04000282 RID: 642
			Spears,
			// Token: 0x04000283 RID: 643
			Other,
			// Token: 0x04000284 RID: 644
			UpperFirstRebarLayer,
			// Token: 0x04000285 RID: 645
			UpperSecondRebarLayer,
			// Token: 0x04000286 RID: 646
			UpperOther,
			// Token: 0x04000287 RID: 647
			LooseRebars,
			// Token: 0x04000288 RID: 648
			CageStirrups = 1,
			// Token: 0x04000289 RID: 649
			CageLongitudinal
		}
	}
}
