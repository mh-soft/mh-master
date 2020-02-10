using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x0200003C RID: 60
	public class ItSlab : ItPXMLItem
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00013D0B File Offset: 0x00011F0B
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00013D13 File Offset: 0x00011F13
		[XmlElement(Order = 0)]
		public string SlabNo { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00013D1C File Offset: 0x00011F1C
		[XmlIgnore]
		public bool SlabNoSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.SlabNo);
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00013D3C File Offset: 0x00011F3C
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x00013D44 File Offset: 0x00011F44
		[XmlElement(Order = 1)]
		public string PartType { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00013D50 File Offset: 0x00011F50
		[XmlIgnore]
		public bool PartTypeSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.PartType);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00013D70 File Offset: 0x00011F70
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00013D78 File Offset: 0x00011F78
		[XmlElement(Order = 2)]
		public string ProductAddition { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00013D84 File Offset: 0x00011F84
		[XmlIgnore]
		public bool ProductAdditionSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ProductAddition);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00013DA4 File Offset: 0x00011FA4
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x00013DAC File Offset: 0x00011FAC
		[XmlElement(Order = 3)]
		public string ProductionWay { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00013DB8 File Offset: 0x00011FB8
		[XmlIgnore]
		public bool ProductionWaySpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ProductionWay);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00013DD8 File Offset: 0x00011FD8
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00013DE0 File Offset: 0x00011FE0
		[XmlElement(Order = 4)]
		public string NumberOfMeansOfTransport { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00013DEC File Offset: 0x00011FEC
		[XmlIgnore]
		public bool NumberOfMeansOfTransportSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.NumberOfMeansOfTransport);
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00013E0C File Offset: 0x0001200C
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x00013E14 File Offset: 0x00012014
		[XmlElement(Order = 5)]
		public string TransportSequence { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00013E20 File Offset: 0x00012020
		[XmlIgnore]
		public bool TransportSequenceSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.TransportSequence);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00013E40 File Offset: 0x00012040
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x00013E48 File Offset: 0x00012048
		[XmlElement(Order = 6)]
		public string PileLevel { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00013E54 File Offset: 0x00012054
		[XmlIgnore]
		public bool PileLevelSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.PileLevel);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x00013E74 File Offset: 0x00012074
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x00013E7C File Offset: 0x0001207C
		[XmlElement(Order = 7)]
		public string TypeOfUnloading { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00013E88 File Offset: 0x00012088
		[XmlIgnore]
		public bool TypeOfUnloadingSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.TypeOfUnloading);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00013EA8 File Offset: 0x000120A8
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x00013EB0 File Offset: 0x000120B0
		[XmlElement(Order = 8)]
		public string MeansOfTransport { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00013EBC File Offset: 0x000120BC
		[XmlIgnore]
		public bool MeansOfTransportSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.MeansOfTransport);
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00013EDC File Offset: 0x000120DC
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x00013EE4 File Offset: 0x000120E4
		[XmlElement(Order = 9)]
		public string ExpositionClass { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00013EF0 File Offset: 0x000120F0
		[XmlIgnore]
		public bool ExpositionClassSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ExpositionClass);
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00013F10 File Offset: 0x00012110
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00013F18 File Offset: 0x00012118
		[XmlElement(Order = 10)]
		public double SlabArea { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00013F24 File Offset: 0x00012124
		[XmlIgnore]
		public bool SlabAreaSpecified
		{
			get
			{
				return this.SlabArea > ItDocument.epsilon;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00013F43 File Offset: 0x00012143
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x00013F4B File Offset: 0x0001214B
		[XmlElement(Order = 11)]
		public double SlabWeight { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00013F54 File Offset: 0x00012154
		[XmlIgnore]
		public bool SlabWeightSpecified
		{
			get
			{
				return this.SlabWeight > ItDocument.epsilon;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00013F73 File Offset: 0x00012173
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x00013F7B File Offset: 0x0001217B
		[XmlElement(Order = 12)]
		public double ProductionThickness { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00013F84 File Offset: 0x00012184
		[XmlIgnore]
		public bool ProductionThicknessSpecified
		{
			get
			{
				return this.ProductionThickness > ItDocument.epsilon;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00013FA3 File Offset: 0x000121A3
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00013FAB File Offset: 0x000121AB
		[XmlElement(Order = 13)]
		public double MaxLength { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00013FB4 File Offset: 0x000121B4
		[XmlIgnore]
		public bool MaxLengthSpecified
		{
			get
			{
				return this.MaxLength > ItDocument.epsilon;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00013FD3 File Offset: 0x000121D3
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x00013FDB File Offset: 0x000121DB
		[XmlElement(Order = 14)]
		public double MaxWidth { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00013FE4 File Offset: 0x000121E4
		[XmlIgnore]
		public bool MaxWidthSpecified
		{
			get
			{
				return this.MaxWidth > ItDocument.epsilon;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00014004 File Offset: 0x00012204
		[XmlIgnore]
		public bool IronProjectionLeftSpecified
		{
			get
			{
				return !double.IsNaN(this.IronProjectionLeft);
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00014024 File Offset: 0x00012224
		[XmlIgnore]
		public bool IronProjectionRightSpecified
		{
			get
			{
				return !double.IsNaN(this.IronProjectionRight);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00014044 File Offset: 0x00012244
		[XmlIgnore]
		public bool IronProjectionBottomSpecified
		{
			get
			{
				return !double.IsNaN(this.IronProjectionBottom);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00014064 File Offset: 0x00012264
		[XmlIgnore]
		public bool IronProjectionTopSpecified
		{
			get
			{
				return !double.IsNaN(this.IronProjectionTop);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00014084 File Offset: 0x00012284
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x0001408C File Offset: 0x0001228C
		[XmlElement(Order = 19)]
		public double X { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00014098 File Offset: 0x00012298
		[XmlIgnore]
		public bool XSpecified
		{
			get
			{
				return Math.Abs(this.X) >= ItDocument.epsilon || Math.Abs(this.Y) >= ItDocument.epsilon || Math.Abs(this.Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x000140E6 File Offset: 0x000122E6
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x000140EE File Offset: 0x000122EE
		[XmlElement(Order = 20)]
		public double Y { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x000140F8 File Offset: 0x000122F8
		[XmlIgnore]
		public bool YSpecified
		{
			get
			{
				return Math.Abs(this.X) >= ItDocument.epsilon || Math.Abs(this.Y) >= ItDocument.epsilon || Math.Abs(this.Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00014146 File Offset: 0x00012346
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x0001414E File Offset: 0x0001234E
		[XmlElement(Order = 21)]
		public double Z { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00014158 File Offset: 0x00012358
		[XmlIgnore]
		public bool ZSpecified
		{
			get
			{
				return Math.Abs(this.X) >= ItDocument.epsilon || Math.Abs(this.Y) >= ItDocument.epsilon || Math.Abs(this.Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x000141A6 File Offset: 0x000123A6
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x000141AE File Offset: 0x000123AE
		[XmlElement(Order = 22)]
		public string OrderPosition { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x000141B8 File Offset: 0x000123B8
		[XmlIgnore]
		public bool OrderPositionSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.OrderPosition);
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000141D8 File Offset: 0x000123D8
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x000141E0 File Offset: 0x000123E0
		[XmlElement(Order = 23)]
		public string ProductGroup { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x000141EC File Offset: 0x000123EC
		[XmlIgnore]
		public bool ProductGroupSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ProductGroup);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0001420C File Offset: 0x0001240C
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x00014214 File Offset: 0x00012414
		[XmlElement(Order = 24)]
		public string SlabType { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00014220 File Offset: 0x00012420
		[XmlIgnore]
		public bool SlabTypeSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.SlabType);
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00014240 File Offset: 0x00012440
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00014248 File Offset: 0x00012448
		[XmlElement(Order = 25)]
		public string ItemDesignation { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00014254 File Offset: 0x00012454
		[XmlIgnore]
		public bool ItemDesignationSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ItemDesignation);
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00014274 File Offset: 0x00012474
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x0001427C File Offset: 0x0001247C
		[XmlElement(Order = 26)]
		public string ProjectCoordinates { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00014288 File Offset: 0x00012488
		[XmlIgnore]
		public bool ProjectCoordinatesSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ProjectCoordinates);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x000142A8 File Offset: 0x000124A8
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x000142B0 File Offset: 0x000124B0
		[XmlElement(Order = 27)]
		public double PositionInPileX { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x000142BC File Offset: 0x000124BC
		[XmlIgnore]
		public bool PositionInPileXSpecified
		{
			get
			{
				return Math.Abs(this.PositionInPileX) >= ItDocument.epsilon || Math.Abs(this.PositionInPileY) >= ItDocument.epsilon || Math.Abs(this.PositionInPileZ) >= ItDocument.epsilon;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0001430A File Offset: 0x0001250A
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x00014312 File Offset: 0x00012512
		[XmlElement(Order = 28)]
		public double PositionInPileY { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0001431C File Offset: 0x0001251C
		[XmlIgnore]
		public bool PositionInPileYSpecified
		{
			get
			{
				return Math.Abs(this.PositionInPileX) >= ItDocument.epsilon || Math.Abs(this.PositionInPileY) >= ItDocument.epsilon || Math.Abs(this.PositionInPileZ) >= ItDocument.epsilon;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0001436A File Offset: 0x0001256A
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x00014372 File Offset: 0x00012572
		[XmlElement(Order = 29)]
		public double PositionInPileZ { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0001437C File Offset: 0x0001257C
		[XmlIgnore]
		public bool PositionInPileZSpecified
		{
			get
			{
				return Math.Abs(this.PositionInPileX) >= ItDocument.epsilon || Math.Abs(this.PositionInPileY) >= ItDocument.epsilon || Math.Abs(this.PositionInPileZ) >= ItDocument.epsilon;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x000143CC File Offset: 0x000125CC
		[XmlIgnore]
		public bool AngleInPileSpecified
		{
			get
			{
				return !double.IsNaN(this.AngleInPile);
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000143EC File Offset: 0x000125EC
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x000143F4 File Offset: 0x000125F4
		[XmlElement(Order = 31)]
		public string GenericInfo01 { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00014400 File Offset: 0x00012600
		[XmlIgnore]
		public bool GenericInfo01Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericInfo01);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00014420 File Offset: 0x00012620
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x00014428 File Offset: 0x00012628
		[XmlElement(Order = 32)]
		public string GenericInfo02 { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00014434 File Offset: 0x00012634
		[XmlIgnore]
		public bool GenericInfo02Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericInfo02);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00014454 File Offset: 0x00012654
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x0001445C File Offset: 0x0001265C
		[XmlElement(Order = 33)]
		public string GenericInfo03 { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00014468 File Offset: 0x00012668
		[XmlIgnore]
		public bool GenericInfo03Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericInfo03);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00014488 File Offset: 0x00012688
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x00014490 File Offset: 0x00012690
		[XmlElement(Order = 34)]
		public string GenericInfo04 { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0001449C File Offset: 0x0001269C
		[XmlIgnore]
		public bool GenericInfo04Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericInfo04);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000144BC File Offset: 0x000126BC
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x000144C4 File Offset: 0x000126C4
		[XmlElement(Order = 35)]
		public string ReforcemInfo { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000144D0 File Offset: 0x000126D0
		[XmlIgnore]
		public bool ReforcemInfoSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ReforcemInfo);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x000144F0 File Offset: 0x000126F0
		[XmlIgnore]
		public bool outlineListSpecified
		{
			get
			{
				return this.outlineList.Count > 0;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00014510 File Offset: 0x00012710
		[XmlIgnore]
		public bool steelListSpecified
		{
			get
			{
				return this.steelList.Count > 0;
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00014530 File Offset: 0x00012730
		public ItSlab()
		{
			base.WBI();
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x000145AD File Offset: 0x000127AD
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x000145B5 File Offset: 0x000127B5
		[XmlIgnore]
		internal double NetConcreteVolume { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x000145BE File Offset: 0x000127BE
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x000145C6 File Offset: 0x000127C6
		[XmlIgnore]
		internal double Density { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x000145CF File Offset: 0x000127CF
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x000145D7 File Offset: 0x000127D7
		[XmlIgnore]
		internal string Material { get; set; }

		// Token: 0x0400016E RID: 366
		[XmlElement(Order = 15)]
		public double IronProjectionLeft = double.NaN;

		// Token: 0x0400016F RID: 367
		[XmlElement(Order = 16)]
		public double IronProjectionRight = double.NaN;

		// Token: 0x04000170 RID: 368
		[XmlElement(Order = 17)]
		public double IronProjectionBottom = double.NaN;

		// Token: 0x04000171 RID: 369
		[XmlElement(Order = 18)]
		public double IronProjectionTop = double.NaN;

		// Token: 0x0400017D RID: 381
		[XmlElement(Order = 30)]
		public double AngleInPile = double.NaN;

		// Token: 0x04000183 RID: 387
		[XmlElement("Outline", Order = 36, IsNullable = true)]
		public List<ItOutline> outlineList = new List<ItOutline>();

		// Token: 0x04000184 RID: 388
		[XmlElement("Steel", Order = 37, IsNullable = true)]
		public List<ItSteel> steelList = new List<ItSteel>();
	}
}
