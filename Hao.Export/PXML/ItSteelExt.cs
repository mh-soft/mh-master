using System;
using System.Xml.Serialization;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x0200003F RID: 63
	public class ItSteelExt : ItPXMLItem
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00014C26 File Offset: 0x00012E26
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x00014C2E File Offset: 0x00012E2E
		[XmlAttribute(DataType = "string", AttributeName = "Type")]
		public string type { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00014C37 File Offset: 0x00012E37
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x00014C3F File Offset: 0x00012E3F
		[XmlElement(Order = 0)]
		public string Info { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00014C48 File Offset: 0x00012E48
		[XmlIgnore]
		public bool InfoSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.Info);
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00014C68 File Offset: 0x00012E68
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x00014C70 File Offset: 0x00012E70
		[XmlElement(ElementName = "I_SheetType", Order = 1)]
		public string SheetType { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00014C7C File Offset: 0x00012E7C
		[XmlIgnore]
		public bool SheetTypeSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.SheetType);
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00014C9C File Offset: 0x00012E9C
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x00014CA4 File Offset: 0x00012EA4
		[XmlElement(ElementName = "I_Count", Order = 2)]
		public int Count { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00014CAD File Offset: 0x00012EAD
		[XmlIgnore]
		public bool CountSpecified
		{
			get
			{
				return this.Count != 0;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00014CB8 File Offset: 0x00012EB8
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x00014CC0 File Offset: 0x00012EC0
		[XmlElement(ElementName = "I_Weight", Order = 3)]
		public double Weight { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00014CC9 File Offset: 0x00012EC9
		[XmlIgnore]
		public bool WeightSpecified
		{
			get
			{
				return this.Weight.Ne(0.0, -1.0);
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00010D8C File Offset: 0x0000EF8C
		public ItSteelExt()
		{
			base.WBI();
		}

		// Token: 0x02000088 RID: 136
		public static class Types
		{
			// Token: 0x040002F6 RID: 758
			public const string StandardMat = "01";

			// Token: 0x040002F7 RID: 759
			public const string Cage = "02";

			// Token: 0x040002F8 RID: 760
			public const string Other = "09";
		}
	}
}
