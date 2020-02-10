using System;
using System.Xml.Serialization;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x0200002D RID: 45
	public class ItAnchorBar : ItPXMLItem
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00010374 File Offset: 0x0000E574
		[XmlIgnore]
		public bool TypeSpecified
		{
			get
			{
				return this.Type > -1;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0001038F File Offset: 0x0000E58F
		// (set) Token: 0x0600027D RID: 637 RVA: 0x00010397 File Offset: 0x0000E597
		[XmlElement(Order = 1)]
		public double Length { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600027E RID: 638 RVA: 0x000103A0 File Offset: 0x0000E5A0
		[XmlIgnore]
		public bool LengthSpecified
		{
			get
			{
				return this.Length > ItDocument.epsilon;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600027F RID: 639 RVA: 0x000103C0 File Offset: 0x0000E5C0
		[XmlIgnore]
		public bool PositionSpecified
		{
			get
			{
				return this.Position > -1.0 + ItDocument.epsilon;
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000103E9 File Offset: 0x0000E5E9
		public ItAnchorBar()
		{
			base.WBI();
		}

		// Token: 0x040000A8 RID: 168
		[XmlElement(Order = 0)]
		public int Type = -1;

		// Token: 0x040000AA RID: 170
		[XmlElement(Order = 2)]
		public double Position = -1.0;
	}
}
