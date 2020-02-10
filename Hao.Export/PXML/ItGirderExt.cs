using System;
using System.Xml.Serialization;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x02000033 RID: 51
	public class ItGirderExt : ItPXMLItem
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00011280 File Offset: 0x0000F480
		// (set) Token: 0x06000322 RID: 802 RVA: 0x00011288 File Offset: 0x0000F488
		[XmlAttribute(DataType = "string", AttributeName = "Type")]
		public string type { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00011294 File Offset: 0x0000F494
		[XmlIgnore]
		public bool PositionSpecified
		{
			get
			{
				return this.Position > -ItDocument.epsilon;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000324 RID: 804 RVA: 0x000112B4 File Offset: 0x0000F4B4
		// (set) Token: 0x06000325 RID: 805 RVA: 0x000112BC File Offset: 0x0000F4BC
		[XmlElement(Order = 1)]
		public int Flags { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000326 RID: 806 RVA: 0x000112C8 File Offset: 0x0000F4C8
		[XmlIgnore]
		public bool Val0Specified
		{
			get
			{
				return !double.IsNaN(this.Val0);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000327 RID: 807 RVA: 0x000112E8 File Offset: 0x0000F4E8
		[XmlIgnore]
		public bool Val1Specified
		{
			get
			{
				return !double.IsNaN(this.Val1);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00011308 File Offset: 0x0000F508
		[XmlIgnore]
		public bool Val2Specified
		{
			get
			{
				return !double.IsNaN(this.Val2);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00011328 File Offset: 0x0000F528
		[XmlIgnore]
		public bool Val3Specified
		{
			get
			{
				return !double.IsNaN(this.Val3);
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00011348 File Offset: 0x0000F548
		public ItGirderExt()
		{
			base.WBI();
		}

		// Token: 0x040000F1 RID: 241
		[XmlElement(Order = 0)]
		public double Position = -1.0;

		// Token: 0x040000F3 RID: 243
		[XmlElement(Order = 2)]
		public double Val0 = double.NaN;

		// Token: 0x040000F4 RID: 244
		[XmlElement(Order = 3)]
		public double Val1 = double.NaN;

		// Token: 0x040000F5 RID: 245
		[XmlElement(Order = 4)]
		public double Val2 = double.NaN;

		// Token: 0x040000F6 RID: 246
		[XmlElement(Order = 5)]
		public double Val3 = double.NaN;
	}
}
