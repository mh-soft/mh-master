using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Hao.Export.Geometry;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x0200003E RID: 62
	[XmlType("Steel")]
	public class ItSteel : ItPXMLItem
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00014643 File Offset: 0x00012843
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0001464B File Offset: 0x0001284B
		[XmlAttribute(AttributeName = "Type")]
		public string SteelType { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00014654 File Offset: 0x00012854
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0001465C File Offset: 0x0001285C
		[XmlElement(Order = 0)]
		public double X { get; set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00014668 File Offset: 0x00012868
		[XmlIgnore]
		public bool XSpecified
		{
			get
			{
				return Math.Abs(this.X) >= ItDocument.epsilon || Math.Abs(this.Y) >= ItDocument.epsilon || Math.Abs(this.Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x000146B6 File Offset: 0x000128B6
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x000146BE File Offset: 0x000128BE
		[XmlElement(Order = 1)]
		public double Y { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x000146C8 File Offset: 0x000128C8
		[XmlIgnore]
		public bool YSpecified
		{
			get
			{
				return Math.Abs(this.X) >= ItDocument.epsilon || Math.Abs(this.Y) >= ItDocument.epsilon || Math.Abs(this.Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00014716 File Offset: 0x00012916
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0001471E File Offset: 0x0001291E
		[XmlElement(Order = 2)]
		public double Z { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00014728 File Offset: 0x00012928
		[XmlIgnore]
		public bool ZSpecified
		{
			get
			{
				return Math.Abs(this.X) >= ItDocument.epsilon || Math.Abs(this.Y) >= ItDocument.epsilon || Math.Abs(this.Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00014776 File Offset: 0x00012976
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x0001477E File Offset: 0x0001297E
		[XmlElement(Order = 3)]
		public bool ToTurn { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00014788 File Offset: 0x00012988
		[XmlIgnore]
		public bool ToTurnSpecified
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001479B File Offset: 0x0001299B
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x000147A3 File Offset: 0x000129A3
		[XmlElement(Order = 4)]
		public bool StopOnTurningSide { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x000147AC File Offset: 0x000129AC
		[XmlIgnore]
		public bool StopOnTurningSideSpecified
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x000147BF File Offset: 0x000129BF
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x000147C7 File Offset: 0x000129C7
		[XmlElement(Order = 5)]
		public string Name { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x000147D0 File Offset: 0x000129D0
		[XmlIgnore]
		public bool NameSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.Name);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x000147F0 File Offset: 0x000129F0
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x000147F8 File Offset: 0x000129F8
		[XmlElement(Order = 6)]
		public string GenericInfo01 { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00014804 File Offset: 0x00012A04
		[XmlIgnore]
		public bool GenericInfo01Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericInfo01);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00014824 File Offset: 0x00012A24
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x0001482C File Offset: 0x00012A2C
		[XmlElement(Order = 7)]
		public string GenericInfo02 { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00014838 File Offset: 0x00012A38
		[XmlIgnore]
		public bool GenericInfo02Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericInfo02);
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00014858 File Offset: 0x00012A58
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x00014860 File Offset: 0x00012A60
		[XmlElement(Order = 8)]
		public string GenericInfo03 { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0001486C File Offset: 0x00012A6C
		[XmlIgnore]
		public bool GenericInfo03Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericInfo03);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0001488C File Offset: 0x00012A8C
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00014894 File Offset: 0x00012A94
		[XmlElement(Order = 9)]
		public string GenericInfo04 { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x000148A0 File Offset: 0x00012AA0
		[XmlIgnore]
		public bool GenericInfo04Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericInfo04);
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x000148C0 File Offset: 0x00012AC0
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x000148C8 File Offset: 0x00012AC8
		[XmlElement(Order = 10)]
		public string GenericInfo05 { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x000148D4 File Offset: 0x00012AD4
		[XmlIgnore]
		public bool GenericInfo05Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericInfo05);
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x000148F4 File Offset: 0x00012AF4
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x000148FC File Offset: 0x00012AFC
		[XmlElement(Order = 11)]
		public string GenericInfo06 { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00014908 File Offset: 0x00012B08
		[XmlIgnore]
		public bool GenericInfo06Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericInfo06);
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00014928 File Offset: 0x00012B28
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x00014930 File Offset: 0x00012B30
		[XmlElement(Order = 12)]
		public string MeshType { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0001493C File Offset: 0x00012B3C
		[XmlIgnore]
		public bool MeshTypeSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.MeshType);
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0001495C File Offset: 0x00012B5C
		[XmlIgnore]
		public bool WeldingDensitySpecified
		{
			get
			{
				return this.WeldingDensity > -1;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x00014978 File Offset: 0x00012B78
		[XmlIgnore]
		public bool BorderStrengthSpecified
		{
			get
			{
				return this.BorderStrength > -1;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00014994 File Offset: 0x00012B94
		[XmlIgnore]
		public bool ProdRotXSpecified
		{
			get
			{
				return !double.IsNaN(this.ProdRotX);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x000149B4 File Offset: 0x00012BB4
		[XmlIgnore]
		public bool ProdRotYSpecified
		{
			get
			{
				return !double.IsNaN(this.ProdRotY);
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x000149D4 File Offset: 0x00012BD4
		[XmlIgnore]
		public bool ProdRotZSpecified
		{
			get
			{
				return !double.IsNaN(this.ProdRotZ);
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x000149F4 File Offset: 0x00012BF4
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x000149FC File Offset: 0x00012BFC
		[XmlElement(Order = 18)]
		public string Layer { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00014A08 File Offset: 0x00012C08
		[XmlIgnore]
		public bool LayerSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.Layer);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00014A28 File Offset: 0x00012C28
		[XmlIgnore]
		public bool barListSpecified
		{
			get
			{
				return this.barList.Count > 0;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00014A48 File Offset: 0x00012C48
		[XmlIgnore]
		public bool girderListSpecified
		{
			get
			{
				return this.girderList.Count > 0;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00014A68 File Offset: 0x00012C68
		[XmlIgnore]
		public bool allocListSpecified
		{
			get
			{
				return this.allocList.Count > 0;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00014A88 File Offset: 0x00012C88
		[XmlIgnore]
		public bool steelExtListSpecified
		{
			get
			{
				return this.steelExtList.Count > 0;
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00014AA8 File Offset: 0x00012CA8
		public ItSteel()
		{
			base.WBI();
			this.SteelType = "none";
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00014B38 File Offset: 0x00012D38
		public void TranslateBy(ItGeMatrix3d translation)
		{
			ItGePoint3d itGePoint3d = new ItGePoint3d(this.X, this.Y, this.Z, null);
			itGePoint3d.transformBy(translation);
			this.X = itGePoint3d.x;
			this.Y = itGePoint3d.y;
			this.Z = itGePoint3d.z;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00014B90 File Offset: 0x00012D90
		public ItGePoint3d Offset(ItGePoint3d point)
		{
			return new ItGePoint3d(point.x - this.X, point.y - this.Y, point.z - this.Z, null);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00014BD4 File Offset: 0x00012DD4
		public double OffsetX(double xCoordinate)
		{
			return xCoordinate - this.X;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00014BF0 File Offset: 0x00012DF0
		public double OffsetY(double yCoordinate)
		{
			return yCoordinate - this.Y;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00014C0C File Offset: 0x00012E0C
		public double OffsetZ(double zCoordinate)
		{
			return zCoordinate - this.Z;
		}

		// Token: 0x04000198 RID: 408
		[XmlElement(Order = 13)]
		public int WeldingDensity = -1;

		// Token: 0x04000199 RID: 409
		[XmlElement(Order = 14)]
		public int BorderStrength = -1;

		// Token: 0x0400019A RID: 410
		[XmlElement(Order = 15)]
		public double ProdRotX = double.NaN;

		// Token: 0x0400019B RID: 411
		[XmlElement(Order = 16)]
		public double ProdRotY = double.NaN;

		// Token: 0x0400019C RID: 412
		[XmlElement(Order = 17)]
		public double ProdRotZ = double.NaN;

		// Token: 0x0400019E RID: 414
		[XmlElement("Bar", Order = 19)]
		public List<ItBar> barList = new List<ItBar>();

		// Token: 0x0400019F RID: 415
		[XmlElement("Girder", Order = 20)]
		public List<ItGirder> girderList = new List<ItGirder>();

		// Token: 0x040001A0 RID: 416
		[XmlElement("Alloc", Order = 21)]
		public List<ItAlloc> allocList = new List<ItAlloc>();

		// Token: 0x040001A1 RID: 417
		[XmlElement("SteelExt", Order = 22)]
		public List<ItSteelExt> steelExtList = new List<ItSteelExt>();

		// Token: 0x02000085 RID: 133
		public static class SteelTypes
		{
			// Token: 0x040002E5 RID: 741
			public const string SingleBars = "none";

			// Token: 0x040002E6 RID: 742
			public const string Mesh = "mesh";

			// Token: 0x040002E7 RID: 743
			public const string Cage = "cage";

			// Token: 0x040002E8 RID: 744
			public const string ExtIron = "extIron";
		}

		// Token: 0x02000086 RID: 134
		public static class MeshTypes
		{
			// Token: 0x040002E9 RID: 745
			public const string Standard = "0";

			// Token: 0x040002EA RID: 746
			public const string Bent2d = "1";

			// Token: 0x040002EB RID: 747
			public const string Module = "2";

			// Token: 0x040002EC RID: 748
			public const string Bent3d = "3";

			// Token: 0x040002ED RID: 749
			public const string Cover = "4";

			// Token: 0x040002EE RID: 750
			public const string Cover2d = "5";

			// Token: 0x040002EF RID: 751
			public const string Cover3d = "6";

			// Token: 0x040002F0 RID: 752
			public const string Loose = "8";

			// Token: 0x040002F1 RID: 753
			public const string ApplicationSpecific = "9";
		}

		// Token: 0x02000087 RID: 135
		public enum BorderStrengths
		{
			// Token: 0x040002F3 RID: 755
			Default,
			// Token: 0x040002F4 RID: 756
			OneRow,
			// Token: 0x040002F5 RID: 757
			TwoRows
		}
	}
}
