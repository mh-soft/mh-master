using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Hao.Export.Geometry;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x0200002F RID: 47
	public class ItBar : ItPXMLItem
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00010410 File Offset: 0x0000E610
		[XmlIgnore]
		public bool ShapeModeSpecified
		{
			get
			{
				return this.ShapeMode != ShapeMode.unset;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0001042E File Offset: 0x0000E62E
		// (set) Token: 0x06000283 RID: 643 RVA: 0x00010436 File Offset: 0x0000E636
		[XmlElement(Order = 1)]
		public int ReinforcementType { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00010440 File Offset: 0x0000E640
		[XmlIgnore]
		public bool ReinforcementTypeSpecified
		{
			get
			{
				return this.ReinforcementType > -1;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0001045B File Offset: 0x0000E65B
		// (set) Token: 0x06000286 RID: 646 RVA: 0x00010463 File Offset: 0x0000E663
		[XmlElement(Order = 2)]
		public string SteelQuality { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0001046C File Offset: 0x0000E66C
		[XmlIgnore]
		public bool SteelQualitySpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.SteelQuality);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0001048C File Offset: 0x0000E68C
		// (set) Token: 0x06000289 RID: 649 RVA: 0x00010494 File Offset: 0x0000E694
		[XmlElement(Order = 3)]
		public int PieceCount { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600028A RID: 650 RVA: 0x000104A0 File Offset: 0x0000E6A0
		[XmlIgnore]
		public bool PieceCountSpecified
		{
			get
			{
				return this.PieceCount > 0;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600028B RID: 651 RVA: 0x000104BB File Offset: 0x0000E6BB
		// (set) Token: 0x0600028C RID: 652 RVA: 0x000104C3 File Offset: 0x0000E6C3
		[XmlElement(Order = 4)]
		public double Diameter { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000104CC File Offset: 0x0000E6CC
		[XmlIgnore]
		public bool DiameterSpecified
		{
			get
			{
				return this.Diameter > ItDocument.epsilon;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600028E RID: 654 RVA: 0x000104EB File Offset: 0x0000E6EB
		// (set) Token: 0x0600028F RID: 655 RVA: 0x000104F3 File Offset: 0x0000E6F3
		[XmlElement(Order = 5)]
		public double X { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000290 RID: 656 RVA: 0x000104FC File Offset: 0x0000E6FC
		[XmlIgnore]
		public bool XSpecified
		{
			get
			{
				return Math.Abs(this.X) >= ItDocument.epsilon || Math.Abs(this.Y) >= ItDocument.epsilon || Math.Abs(this.Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0001054A File Offset: 0x0000E74A
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00010552 File Offset: 0x0000E752
		[XmlElement(Order = 6)]
		public double Y { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0001055C File Offset: 0x0000E75C
		[XmlIgnore]
		public bool YSpecified
		{
			get
			{
				return Math.Abs(this.X) >= ItDocument.epsilon || Math.Abs(this.Y) >= ItDocument.epsilon || Math.Abs(this.Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000294 RID: 660 RVA: 0x000105AA File Offset: 0x0000E7AA
		// (set) Token: 0x06000295 RID: 661 RVA: 0x000105B2 File Offset: 0x0000E7B2
		[XmlElement(Order = 7)]
		public double Z { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000105BC File Offset: 0x0000E7BC
		[XmlIgnore]
		public bool ZSpecified
		{
			get
			{
				return Math.Abs(this.X) >= ItDocument.epsilon || Math.Abs(this.Y) >= ItDocument.epsilon || Math.Abs(this.Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0001060A File Offset: 0x0000E80A
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00010612 File Offset: 0x0000E812
		[XmlElement(Order = 8)]
		public double RotZ { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0001061C File Offset: 0x0000E81C
		[XmlIgnore]
		public bool RotZSpecified
		{
			get
			{
				return Math.Abs(this.RotZ) > ItDocument.epsilon;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00010640 File Offset: 0x0000E840
		// (set) Token: 0x0600029B RID: 667 RVA: 0x00010648 File Offset: 0x0000E848
		[XmlElement(Order = 9)]
		public string ArticleNo { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00010654 File Offset: 0x0000E854
		[XmlIgnore]
		public bool ArticleNoSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ArticleNo);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00010674 File Offset: 0x0000E874
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0001067C File Offset: 0x0000E87C
		[XmlElement(Order = 10)]
		public bool NoAutoProd { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00010688 File Offset: 0x0000E888
		[XmlIgnore]
		public bool NoAutoProdSpecified
		{
			get
			{
				return this.NoAutoProd;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x000106A0 File Offset: 0x0000E8A0
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x000106A8 File Offset: 0x0000E8A8
		[XmlElement(Order = 11)]
		public double ExtIronWeight { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x000106B4 File Offset: 0x0000E8B4
		[XmlIgnore]
		public bool ExtIronWeightSpecified
		{
			get
			{
				return this.ExtIronWeight > ItDocument.epsilon;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x000106D3 File Offset: 0x0000E8D3
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x000106DB File Offset: 0x0000E8DB
		[XmlElement(Order = 12)]
		public string Bin { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x000106E4 File Offset: 0x0000E8E4
		[XmlIgnore]
		public bool BinSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.Bin);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00010704 File Offset: 0x0000E904
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0001070C File Offset: 0x0000E90C
		[XmlElement(Order = 13)]
		public string Pos { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00010718 File Offset: 0x0000E918
		[XmlIgnore]
		public bool PosSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.Pos);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00010738 File Offset: 0x0000E938
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00010740 File Offset: 0x0000E940
		[XmlElement(Order = 14)]
		public string Note { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0001074C File Offset: 0x0000E94C
		[XmlIgnore]
		public bool NoteSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.Note);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0001076C File Offset: 0x0000E96C
		// (set) Token: 0x060002AD RID: 685 RVA: 0x00010774 File Offset: 0x0000E974
		[XmlElement(Order = 15)]
		public string Machine { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00010780 File Offset: 0x0000E980
		[XmlIgnore]
		public bool MachineSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.Machine);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002AF RID: 687 RVA: 0x000107A0 File Offset: 0x0000E9A0
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x000107A8 File Offset: 0x0000E9A8
		[XmlElement(Order = 16)]
		public string BendingDevice { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x000107B4 File Offset: 0x0000E9B4
		[XmlIgnore]
		public bool BendingDeviceSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.BendingDevice);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x000107D4 File Offset: 0x0000E9D4
		[XmlIgnore]
		public bool spacerListSpecified
		{
			get
			{
				return this.spacerList.Count > 0;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000107F4 File Offset: 0x0000E9F4
		[XmlIgnore]
		public bool weldingPointListSpecified
		{
			get
			{
				return this.weldingPointList.Count > 0;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00010814 File Offset: 0x0000EA14
		[XmlIgnore]
		public bool segmentListSpecified
		{
			get
			{
				return this.segmentList.Count > 0;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00010834 File Offset: 0x0000EA34
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0001083C File Offset: 0x0000EA3C
		[XmlIgnore]
		public ItGeVector3d CurrentNormal { get; set; }

		// Token: 0x060002B7 RID: 695 RVA: 0x00010845 File Offset: 0x0000EA45
		public ItBar()
		{
			base.WBI();
		}

		// Token: 0x040000B0 RID: 176
		[XmlElement(Order = 0)]
		public ShapeMode ShapeMode = ShapeMode.unset;

		// Token: 0x040000C1 RID: 193
		[XmlElement("Spacer", Order = 17)]
		public List<ItSpacer> spacerList = new List<ItSpacer>();

		// Token: 0x040000C2 RID: 194
		[XmlElement("WeldingPoint", Order = 18)]
		public List<ItWeldingPoint> weldingPointList = new List<ItWeldingPoint>();

		// Token: 0x040000C3 RID: 195
		[XmlElement("Segment", Order = 19)]
		public List<ItSegment> segmentList = new List<ItSegment>();
	}
}
