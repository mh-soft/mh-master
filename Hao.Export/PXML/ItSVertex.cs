using System;
using System.Xml.Serialization;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x02000040 RID: 64
	public class ItSVertex : ItPXMLItem
	{
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00014CE8 File Offset: 0x00012EE8
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x00014CF0 File Offset: 0x00012EF0
		[XmlElement(Order = 0)]
		public double X { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x00014CFC File Offset: 0x00012EFC
		[XmlIgnore]
		public bool XSpecified
		{
			get
			{
				return Math.Abs(this.X) >= ItDocument.epsilon || Math.Abs(this.Y) >= ItDocument.epsilon;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00014D38 File Offset: 0x00012F38
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x00014D40 File Offset: 0x00012F40
		[XmlElement(Order = 1)]
		public double Y { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00014D4C File Offset: 0x00012F4C
		[XmlIgnore]
		public bool YSpecified
		{
			get
			{
				return Math.Abs(this.X) >= ItDocument.epsilon || Math.Abs(this.Y) >= ItDocument.epsilon;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00014D88 File Offset: 0x00012F88
		[XmlIgnore]
		public bool BulgeSpecified
		{
			get
			{
				return !double.IsNaN(this.Bulge);
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00014DA8 File Offset: 0x00012FA8
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x00014DB0 File Offset: 0x00012FB0
		[XmlElement(Order = 3)]
		public string LineAttribute { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00014DBC File Offset: 0x00012FBC
		[XmlIgnore]
		public bool LineAttributeSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.LineAttribute);
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00014DDC File Offset: 0x00012FDC
		[XmlIgnore]
		public bool DXSpecified
		{
			get
			{
				return !double.IsNaN(this.DX);
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00014DFC File Offset: 0x00012FFC
		[XmlIgnore]
		public bool DYSpecified
		{
			get
			{
				return !double.IsNaN(this.DY);
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00014E1C File Offset: 0x0001301C
		public ItSVertex()
		{
			base.WBI();
		}

		// Token: 0x040001A9 RID: 425
		[XmlElement(Order = 2)]
		public double Bulge = double.NaN;

		// Token: 0x040001AB RID: 427
		[XmlElement(Order = 4)]
		public double DX = double.NaN;

		// Token: 0x040001AC RID: 428
		[XmlElement(Order = 5)]
		public double DY = double.NaN;
	}
}
