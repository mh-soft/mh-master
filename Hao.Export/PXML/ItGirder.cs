using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x02000032 RID: 50
	public class ItGirder : ItPXMLItem
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00010D9D File Offset: 0x0000EF9D
		// (set) Token: 0x060002EC RID: 748 RVA: 0x00010DA5 File Offset: 0x0000EFA5
		[XmlElement(Order = 0)]
		public int PieceCount { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00010DB0 File Offset: 0x0000EFB0
		[XmlIgnore]
		public bool PieceCountSpecified
		{
			get
			{
				return this.PieceCount > 0;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00010DCB File Offset: 0x0000EFCB
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00010DD3 File Offset: 0x0000EFD3
		[XmlElement(Order = 1)]
		public double X { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00010DDC File Offset: 0x0000EFDC
		[XmlIgnore]
		public bool XSpecified
		{
			get
			{
				return Math.Abs(this.X) >= ItDocument.epsilon || Math.Abs(this.Y) >= ItDocument.epsilon || Math.Abs(this.Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00010E2A File Offset: 0x0000F02A
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00010E32 File Offset: 0x0000F032
		[XmlElement(Order = 2)]
		public double Y { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00010E3C File Offset: 0x0000F03C
		[XmlIgnore]
		public bool YSpecified
		{
			get
			{
				return Math.Abs(this.X) >= ItDocument.epsilon || Math.Abs(this.Y) >= ItDocument.epsilon || Math.Abs(this.Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00010E8A File Offset: 0x0000F08A
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00010E92 File Offset: 0x0000F092
		[XmlElement(Order = 3)]
		public double Z { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00010E9C File Offset: 0x0000F09C
		[XmlIgnore]
		public bool ZSpecified
		{
			get
			{
				return Math.Abs(this.X) >= ItDocument.epsilon || Math.Abs(this.Y) >= ItDocument.epsilon || Math.Abs(this.Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00010EEA File Offset: 0x0000F0EA
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00010EF2 File Offset: 0x0000F0F2
		[XmlElement(Order = 4)]
		public string GirderName { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00010EFC File Offset: 0x0000F0FC
		[XmlIgnore]
		public bool GirderNameSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GirderName);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00010F1C File Offset: 0x0000F11C
		// (set) Token: 0x060002FB RID: 763 RVA: 0x00010F24 File Offset: 0x0000F124
		[XmlElement(Order = 5)]
		public double Length { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00010F30 File Offset: 0x0000F130
		[XmlIgnore]
		public bool LengthSpecified
		{
			get
			{
				return this.Length > ItDocument.epsilon;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00010F50 File Offset: 0x0000F150
		[XmlIgnore]
		public bool AngleToXSpecified
		{
			get
			{
				return !double.IsNaN(this.AngleToX);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00010F70 File Offset: 0x0000F170
		// (set) Token: 0x060002FF RID: 767 RVA: 0x00010F78 File Offset: 0x0000F178
		[XmlElement(Order = 7)]
		public bool NoAutoProd { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00010F84 File Offset: 0x0000F184
		[XmlIgnore]
		public bool NoAutoProdSpecified
		{
			get
			{
				return this.NoAutoProd;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00010F9C File Offset: 0x0000F19C
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00010FA4 File Offset: 0x0000F1A4
		[XmlElement(Order = 8)]
		public double Height { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00010FB0 File Offset: 0x0000F1B0
		[XmlIgnore]
		public bool HeightSpecified
		{
			get
			{
				return this.Height > ItDocument.epsilon;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00010FD0 File Offset: 0x0000F1D0
		[XmlIgnore]
		public bool TopExcessSpecified
		{
			get
			{
				return this.TopExcess > -ItDocument.epsilon;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00010FF0 File Offset: 0x0000F1F0
		// (set) Token: 0x06000306 RID: 774 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		[XmlElement(Order = 10)]
		public double BottomExcess { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00011004 File Offset: 0x0000F204
		[XmlIgnore]
		public bool BottomExcessSpecified
		{
			get
			{
				return this.BottomExcess > -ItDocument.epsilon;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00011024 File Offset: 0x0000F224
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0001102C File Offset: 0x0000F22C
		[XmlElement(Order = 11)]
		public double Weight { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00011038 File Offset: 0x0000F238
		[XmlIgnore]
		public bool WeightSpecified
		{
			get
			{
				return this.Weight > ItDocument.epsilon;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00011057 File Offset: 0x0000F257
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0001105F File Offset: 0x0000F25F
		[XmlElement(Order = 12)]
		public double TopFlangeDiameter { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00011068 File Offset: 0x0000F268
		[XmlIgnore]
		public bool TopFlangeDiameterSpecified
		{
			get
			{
				return this.TopFlangeDiameter > ItDocument.epsilon;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00011087 File Offset: 0x0000F287
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0001108F File Offset: 0x0000F28F
		[XmlElement(Order = 13)]
		public double BottomFlangeDiameter { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00011098 File Offset: 0x0000F298
		[XmlIgnore]
		public bool BottomFlangeDiameterSpecified
		{
			get
			{
				return this.BottomFlangeDiameter > ItDocument.epsilon;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000311 RID: 785 RVA: 0x000110B8 File Offset: 0x0000F2B8
		[XmlIgnore]
		public bool GirderTypeSpecified
		{
			get
			{
				return this.GirderType > -1;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000312 RID: 786 RVA: 0x000110D4 File Offset: 0x0000F2D4
		[XmlIgnore]
		public bool MountingTypeSpecified
		{
			get
			{
				return this.MountingType > -1;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000313 RID: 787 RVA: 0x000110EF File Offset: 0x0000F2EF
		// (set) Token: 0x06000314 RID: 788 RVA: 0x000110F7 File Offset: 0x0000F2F7
		[XmlElement(Order = 16)]
		public string Machine { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00011100 File Offset: 0x0000F300
		[XmlIgnore]
		public bool MachineSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.Machine);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00011120 File Offset: 0x0000F320
		// (set) Token: 0x06000317 RID: 791 RVA: 0x00011128 File Offset: 0x0000F328
		[XmlElement(Order = 17)]
		public double Period { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00011134 File Offset: 0x0000F334
		[XmlIgnore]
		public bool PeriodSpecified
		{
			get
			{
				return this.Period > ItDocument.epsilon;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00011154 File Offset: 0x0000F354
		[XmlIgnore]
		public bool PeriodOffsetSpecified
		{
			get
			{
				return !double.IsNaN(this.PeriodOffset);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00011174 File Offset: 0x0000F374
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0001117C File Offset: 0x0000F37C
		[XmlElement(Order = 19)]
		public double Width { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00011188 File Offset: 0x0000F388
		[XmlIgnore]
		public bool WidthSpecified
		{
			get
			{
				return this.Width > ItDocument.epsilon;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600031D RID: 797 RVA: 0x000111A8 File Offset: 0x0000F3A8
		[XmlIgnore]
		public bool anchorBarListSpecified
		{
			get
			{
				return this.anchorBarList.Count > 0;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600031E RID: 798 RVA: 0x000111C8 File Offset: 0x0000F3C8
		[XmlIgnore]
		public bool girderExtListSpecified
		{
			get
			{
				return this.girderExtList.Count > 0;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600031F RID: 799 RVA: 0x000111E8 File Offset: 0x0000F3E8
		[XmlIgnore]
		public bool sectionListSpecified
		{
			get
			{
				return this.sectionList.Count > 0;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00011208 File Offset: 0x0000F408
		public ItGirder()
		{
			base.WBI();
		}

		// Token: 0x040000DF RID: 223
		[XmlElement(Order = 6)]
		public double AngleToX = double.NaN;

		// Token: 0x040000E2 RID: 226
		[XmlElement(Order = 9)]
		public double TopExcess = -1.0;

		// Token: 0x040000E7 RID: 231
		[XmlElement(Order = 14)]
		public int GirderType = -1;

		// Token: 0x040000E8 RID: 232
		[XmlElement(Order = 15)]
		public int MountingType = -1;

		// Token: 0x040000EB RID: 235
		[XmlElement(Order = 18)]
		public double PeriodOffset = double.NaN;

		// Token: 0x040000ED RID: 237
		[XmlElement("AnchorBar", Order = 20)]
		public List<ItAnchorBar> anchorBarList = new List<ItAnchorBar>();

		// Token: 0x040000EE RID: 238
		[XmlElement("GirderExt", Order = 21)]
		public List<ItGirderExt> girderExtList = new List<ItGirderExt>();

		// Token: 0x040000EF RID: 239
		[XmlElement("Section", Order = 22)]
		public List<ItSection> sectionList = new List<ItSection>();

		// Token: 0x02000080 RID: 128
		internal enum GirderTypes
		{
			// Token: 0x040002C9 RID: 713
			StandardGirder,
			// Token: 0x040002CA RID: 714
			GeneralBasicReinforcementGirder,
			// Token: 0x040002CB RID: 715
			ShearForceGirder,
			// Token: 0x040002CC RID: 716
			SupplementaryGirder
		}

		// Token: 0x02000081 RID: 129
		internal enum MountingTypes
		{
			// Token: 0x040002CE RID: 718
			NoIndication,
			// Token: 0x040002CF RID: 719
			ToBeFixedManuallyReworkRequired,
			// Token: 0x040002D0 RID: 720
			ToBeFixedManually
		}
	}
}
