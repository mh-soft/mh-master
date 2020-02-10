using System;
using System.Xml.Serialization;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x02000031 RID: 49
	public class ItFeedback : ItPXMLItem
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00010B93 File Offset: 0x0000ED93
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00010B9B File Offset: 0x0000ED9B
		[XmlElement(Order = 0)]
		public string MessageType { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00010BA4 File Offset: 0x0000EDA4
		[XmlIgnore]
		public bool MessageTypeSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.MessageType);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00010BC4 File Offset: 0x0000EDC4
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x00010BCC File Offset: 0x0000EDCC
		[XmlElement(Order = 1)]
		public string Code { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00010BD8 File Offset: 0x0000EDD8
		[XmlIgnore]
		public bool CodeSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.Code);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00010BF8 File Offset: 0x0000EDF8
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x00010C00 File Offset: 0x0000EE00
		[XmlElement(Order = 2)]
		public string InfoValue { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00010C0C File Offset: 0x0000EE0C
		[XmlIgnore]
		public bool InfoValueSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.InfoValue);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00010C2C File Offset: 0x0000EE2C
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x00010C34 File Offset: 0x0000EE34
		[XmlElement(Order = 3)]
		public int PieceCount { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00010C40 File Offset: 0x0000EE40
		[XmlIgnore]
		public bool PieceCountSpecified
		{
			get
			{
				return this.PieceCount > 0;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00010C5B File Offset: 0x0000EE5B
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x00010C63 File Offset: 0x0000EE63
		[XmlElement(Order = 4)]
		public string MaterialType { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00010C6C File Offset: 0x0000EE6C
		[XmlIgnore]
		public bool MaterialTypeSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.MaterialType);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00010C8C File Offset: 0x0000EE8C
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00010C94 File Offset: 0x0000EE94
		[XmlElement(Order = 5)]
		public string MaterialBatch { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00010CA0 File Offset: 0x0000EEA0
		[XmlIgnore]
		public bool MaterialBatchSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.MaterialBatch);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00010CC0 File Offset: 0x0000EEC0
		// (set) Token: 0x060002DF RID: 735 RVA: 0x00010CC8 File Offset: 0x0000EEC8
		[XmlElement(Order = 6)]
		public double MaterialWeight { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00010CD4 File Offset: 0x0000EED4
		[XmlIgnore]
		public bool MaterialWeightSpecified
		{
			get
			{
				return this.MaterialWeight > ItDocument.epsilon;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00010CF3 File Offset: 0x0000EEF3
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x00010CFB File Offset: 0x0000EEFB
		[XmlElement(Order = 7)]
		public string ProdDate { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00010D04 File Offset: 0x0000EF04
		[XmlIgnore]
		public bool ProdDateSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ProdDate);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00010D24 File Offset: 0x0000EF24
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00010D2C File Offset: 0x0000EF2C
		[XmlElement(Order = 8)]
		public string Machine { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00010D38 File Offset: 0x0000EF38
		[XmlIgnore]
		public bool MachineSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.Machine);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00010D58 File Offset: 0x0000EF58
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x00010D60 File Offset: 0x0000EF60
		[XmlElement(Order = 9)]
		public string Description { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00010D6C File Offset: 0x0000EF6C
		[XmlIgnore]
		public bool DescriptionSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.Description);
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00010D8C File Offset: 0x0000EF8C
		public ItFeedback()
		{
			base.WBI();
		}
	}
}
