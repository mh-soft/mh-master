using System;
using System.Collections.Generic;
using System.IO;
using Hao.Export.Geometry;
using Hao.Export.Reinforcement;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;

namespace Hao.Export.MachineData
{
	// Token: 0x0200002B RID: 43
	public class WireItem : CNCDataBase
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000FA1B File Offset: 0x0000DC1B
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000FA23 File Offset: 0x0000DC23
		public ItGePoint3d EndPoint { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000FA2C File Offset: 0x0000DC2C
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000FA34 File Offset: 0x0000DC34
		public ItGePoint3d StartPoint { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000FA3D File Offset: 0x0000DC3D
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000FA45 File Offset: 0x0000DC45
		public double BendDiameter { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000FA4E File Offset: 0x0000DC4E
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000FA56 File Offset: 0x0000DC56
		public double WireDiameter { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000FA5F File Offset: 0x0000DC5F
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000FA67 File Offset: 0x0000DC67
		public WireDistributionDirection Direction { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000FA70 File Offset: 0x0000DC70
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000FA78 File Offset: 0x0000DC78
		public SteelGroupElementMeshBase ParentMesh { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000FA81 File Offset: 0x0000DC81
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000FA89 File Offset: 0x0000DC89
		public string SteelQuality { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000FA92 File Offset: 0x0000DC92
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000FA9A File Offset: 0x0000DC9A
		public int NumberOfIrons { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000FAA3 File Offset: 0x0000DCA3
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000FAAB File Offset: 0x0000DCAB
		public double Length { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000FAB4 File Offset: 0x0000DCB4
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000FABC File Offset: 0x0000DCBC
		public int Pitch { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000FAC5 File Offset: 0x0000DCC5
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000FACD File Offset: 0x0000DCCD
		public int Angle { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000FAD6 File Offset: 0x0000DCD6
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000FADE File Offset: 0x0000DCDE
		public string ArticleNumber { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000FAE7 File Offset: 0x0000DCE7
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000FAEF File Offset: 0x0000DCEF
		public int AutomaticProduction { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000FAF8 File Offset: 0x0000DCF8
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000FB00 File Offset: 0x0000DD00
		public int SpacerType { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000FB09 File Offset: 0x0000DD09
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000FB11 File Offset: 0x0000DD11
		public int SpacerStartPoint { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000FB1A File Offset: 0x0000DD1A
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000FB22 File Offset: 0x0000DD22
		public int SpacerPitch { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000FB2B File Offset: 0x0000DD2B
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000FB33 File Offset: 0x0000DD33
		public int Bending { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000FB3C File Offset: 0x0000DD3C
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000FB44 File Offset: 0x0000DD44
		public int Spacers { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000FB4D File Offset: 0x0000DD4D
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000FB55 File Offset: 0x0000DD55
		public int WeldingPoints { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000FB5E File Offset: 0x0000DD5E
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000FB66 File Offset: 0x0000DD66
		public int ReinforcementType { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000FB70 File Offset: 0x0000DD70
		public int AdditionalStore
		{
			get
			{
				ItDebug.assert(this.Bending == 0, "Bending export not yet supported.");
				ItDebug.assert(this.Spacers == 0, "Spacers export not yet supported.");
				ItDebug.assert(this.WeldingPoints == 0, "WeldingPoints export not yet supported.");
				int num = this.WeldingPoints * 100 + this.Spacers * 10 + this.Bending;
				ItDebug.assert(num < 1000, "Invalid values for Beding, Spacers, WeldingPoints");
				return num;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000FBF0 File Offset: 0x0000DDF0
		public WireItem(Document doc, Curve curve, FabricSheetType type, int wireIndex, WireDistributionDirection direction, SteelGroupElementMeshBase parentMesh)
		{
			this._curve = curve;
			this.Direction = direction;
			this.ParentMesh = parentMesh;
			this.StartPoint = curve.GetEndPoint(0).asPoint();
			this.EndPoint = curve.GetEndPoint(1).asPoint();
			this.StartPoint.transformBy(parentMesh.MatWcsToPalette);
			this.EndPoint.transformBy(parentMesh.MatWcsToPalette);
			this.status = WireItem.Status.RelativeToWorld;
			this.SetValuesFromFabricType(doc, type, wireIndex);
			this.NumberOfIrons = 1;
			this.Pitch = 0;
			this.Angle = this.getAngle();
			this.ArticleNumber = "Rodstock";
			this.AutomaticProduction = 0;
			this.SpacerType = 0;
			this.SpacerStartPoint = 0;
			this.SpacerPitch = 0;
			this.Bending = 0;
			this.Spacers = 0;
			this.WeldingPoints = 0;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		private void SetValuesFromFabricType(Document doc, FabricSheetType type, int wireIndex)
		{
			bool flag = type.IsCustom();
			if (flag)
			{
				this._wireItem = type.GetWireItem(wireIndex, this.Direction);
				FabricWireType fabricWireType = doc.GetElement(this._wireItem.WireType) as FabricWireType;
				this.BendDiameter = fabricWireType.BendDiameter;
				this.WireDiameter = fabricWireType.WireDiameter;
				this.Length = this._wireItem.WireLength;
				Material material = doc.GetElement(type.Material) as Material;
				double value = 500.0;
				double defValue = value.NperMM2ToRevitYieldStrength();
				this.SteelQuality = (((material != null) ? material.GetMinimumYieldStress(defValue).RevitYieldStrengthToNperMM2().ToString() : null) ?? value.ToString());
			}
			else
			{
				this._wireItem = null;
				this.BendDiameter = 0.0;
				double num;
				double num2;
				type.getDiameters(out num, out num2);
				this.WireDiameter = ((this.Direction == null) ? num : num2);
				this.Length = (this.EndPoint - this.StartPoint).length();
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000FE00 File Offset: 0x0000E000
		public void transformToSteelmat()
		{
			ItGeVector3d vec = -this.ParentMesh.PosPalette.asVector();
			ItGeMatrix3d mat = ItGeMatrix3d.translation(vec);
			this.StartPoint.transformBy(mat);
			this.EndPoint.transformBy(mat);
			this.status = WireItem.Status.RelativeToSteelmat;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000FE4C File Offset: 0x0000E04C
		private int getAngle()
		{
			ItGeVector3d vec = this.EndPoint - this.StartPoint;
			double num = ItGeVector3d.kXAxis.angleTo(vec, ItGeVector3d.kZAxis);
			return (int)Math.Round(num * 57.295779513082323);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000FE94 File Offset: 0x0000E094
		public void sendToUniwrapper()
		{
			ItDebug.assert(this.status == WireItem.Status.RelativeToSteelmat, "Can't write wire items before their coordinates are transformed to be relative to steelmats origin");
			int diameter = CNCDataBase.convertToMM(this.WireDiameter);
			int length = CNCDataBase.convertToMM(this.Length);
			int[] startpoint = CNCDataBase.toPointArray(this.StartPoint);
			int pitch = CNCDataBase.convertToMM((double)this.Pitch);
			int spacerStartpoint = CNCDataBase.convertToMM((double)this.SpacerStartPoint);
			int spacerPitch = CNCDataBase.convertToMM((double)this.SpacerPitch);
			ItUniWrapperImpl.AddCFSRodstock(this.ReinforcementType, this.SteelQuality, this.NumberOfIrons, diameter, length, startpoint, pitch, this.Angle, this.ArticleNumber, this.AutomaticProduction, this.SpacerType, spacerStartpoint, spacerPitch, this.AdditionalStore);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000FF44 File Offset: 0x0000E144
		public void writeToUnitechnik(TextWriter writer, int ironNumber)
		{
			ItDebug.assert(this.status == WireItem.Status.RelativeToSteelmat, "Can't write wire items before their coordinates are transformed to be relative to steelmats origin");
			writer.Write(CNCDataBase.paddedInt3(ironNumber) + " ");
			writer.Write("000 ");
			writer.Write(CNCDataBase.paddedInt(this.ReinforcementType, 1, false) + " ");
			writer.Write(this.SteelQuality + " ");
			writer.Write(CNCDataBase.paddedInt(this.NumberOfIrons, 5, false) + " ");
			writer.Write(CNCDataBase.toMMString3(this.WireDiameter) + " ");
			writer.Write(CNCDataBase.toMMString5(this.Length, false) + " ");
			writer.Write(CNCDataBase.toSignedMMString5(this.StartPoint.x) + " ");
			writer.Write(CNCDataBase.toSignedMMString5(this.StartPoint.y) + " ");
			writer.Write(CNCDataBase.toSignedMMString5((double)this.Pitch) + " ");
			writer.Write(CNCDataBase.toMMString((double)this.Angle, 3, true) + " ");
			writer.Write(CNCDataBase.padString(this.ArticleNumber, 10) + " ");
			writer.Write(this.AutomaticProduction + writer.NewLine);
			writer.Write(CNCDataBase.paddedInt3(this.SpacerType) + " ");
			writer.Write("000 ");
			writer.Write("000 ");
			writer.Write(CNCDataBase.toMMString5((double)this.SpacerStartPoint, false) + " ");
			writer.Write(CNCDataBase.toMMString5((double)this.SpacerPitch, false) + " ");
			writer.Write(CNCDataBase.toMMString5(this.StartPoint.z, false) + " ");
			writer.Write(this.AdditionalStore + writer.NewLine);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0001017D File Offset: 0x0000E37D
		public static void writeHeaderToUnitechnik(TextWriter writer, int version, int numberOfIronSets)
		{
			writer.WriteLine("RODSTOCK");
			writer.WriteLine(CNCDataBase.paddedInt(version, 3, false));
			writer.WriteLine(CNCDataBase.paddedInt3(numberOfIronSets));
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000101A8 File Offset: 0x0000E3A8
		public double GetSteelVolumeInPart(ItSolid partSolid)
		{
			Solid wcssolid = this.GetWCSSolid();
			Solid solid = wcssolid.intersect(partSolid.Solid);
			bool flag = solid != null && solid.Volume.Gt(0.0, -1.0);
			double result;
			if (flag)
			{
				result = solid.Volume;
			}
			else
			{
				result = 0.0;
			}
			return result;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00010210 File Offset: 0x0000E410
		private Solid GetWCSSolid()
		{
			XYZ endPoint = this._curve.GetEndPoint(0);
			XYZ endPoint2 = this._curve.GetEndPoint(1);
			XYZ xyz = endPoint2 - endPoint;
			IList<CurveLoop> extrusionProfile = this.GetExtrusionProfile(xyz, endPoint);
			return GeometryCreationUtilities.CreateExtrusionGeometry(extrusionProfile, xyz, xyz.GetLength());
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00010260 File Offset: 0x0000E460
		private IList<CurveLoop> GetExtrusionProfile(XYZ extrusionDirWCS, XYZ startPointWCS)
		{
			Plane plane = Plane.CreateByNormalAndOrigin(extrusionDirWCS, startPointWCS);
			Arc item = Arc.Create(plane, this.WireDiameter / 2.0, 0.0, 3.1415926535897931);
			Arc item2 = Arc.Create(plane, this.WireDiameter / 2.0, 3.1415926535897931, 6.2831853071795862);
			IList<CurveLoop> list = new List<CurveLoop>();
			CurveLoop item3 = CurveLoop.Create(new List<Curve>
			{
				item,
				item2
			});
			list.Add(item3);
			return list;
		}

		// Token: 0x0400008C RID: 140
		private const string ReservedFieldThree0s = "000";

		// Token: 0x0400008D RID: 141
		private const string UnitechnikName = "RODSTOCK";

		// Token: 0x0400008E RID: 142
		private readonly Curve _curve;

		// Token: 0x0400008F RID: 143
		private FabricWireItem _wireItem;

		// Token: 0x04000090 RID: 144
		private WireItem.Status status;

		// Token: 0x0200007E RID: 126
		private enum Status
		{
			// Token: 0x040002C0 RID: 704
			RelativeToWorld,
			// Token: 0x040002C1 RID: 705
			RelativeToSteelmat
		}
	}
}
