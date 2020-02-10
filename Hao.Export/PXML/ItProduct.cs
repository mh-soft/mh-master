using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Hao.Export.Geometry;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x02000036 RID: 54
	public class ItProduct : ItPXMLItem
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00011D8E File Offset: 0x0000FF8E
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x00011D96 File Offset: 0x0000FF96
		[XmlElement(Order = 0)]
		public string ElementNo { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00011DA0 File Offset: 0x0000FFA0
		[XmlIgnore]
		public bool ElementNoSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ElementNo);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00011DC0 File Offset: 0x0000FFC0
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x00011DC8 File Offset: 0x0000FFC8
		[XmlElement(Order = 1)]
		public string ProductType { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00011DD4 File Offset: 0x0000FFD4
		[XmlIgnore]
		public bool ProductTypeSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ProductType);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00011DF4 File Offset: 0x0000FFF4
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x00011DFC File Offset: 0x0000FFFC
		[XmlElement(Order = 2)]
		public double TotalThickness { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00011E08 File Offset: 0x00010008
		[XmlIgnore]
		public bool TotalThicknessSpecified
		{
			get
			{
				return this.TotalThickness > ItDocument.epsilon;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00011E27 File Offset: 0x00010027
		// (set) Token: 0x060003DB RID: 987 RVA: 0x00011E2F File Offset: 0x0001002F
		[XmlElement(Order = 3)]
		public double DoubleWallsGap { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00011E38 File Offset: 0x00010038
		[XmlIgnore]
		public bool DoubleWallsGapSpecified
		{
			get
			{
				return this.DoubleWallsGap > ItDocument.epsilon;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00011E57 File Offset: 0x00010057
		// (set) Token: 0x060003DE RID: 990 RVA: 0x00011E5F File Offset: 0x0001005F
		[XmlElement(Order = 4)]
		public int PieceCount { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00011E68 File Offset: 0x00010068
		[XmlIgnore]
		public bool PieceCountSpecified
		{
			get
			{
				return this.PieceCount > 0;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00011E84 File Offset: 0x00010084
		[XmlIgnore]
		public bool TurnWidthSpecified
		{
			get
			{
				return !double.IsNaN(this.TurnWidth);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00011EA4 File Offset: 0x000100A4
		[XmlIgnore]
		public bool TurnMoveXSpecified
		{
			get
			{
				return !double.IsNaN(this.TurnMoveX);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00011EC4 File Offset: 0x000100C4
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x00011ECC File Offset: 0x000100CC
		[XmlElement(Order = 7)]
		public string Comment { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00011ED8 File Offset: 0x000100D8
		[XmlIgnore]
		public bool CommentSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.Comment);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00011EF8 File Offset: 0x000100F8
		[XmlIgnore]
		public bool RotationPositionSpecified
		{
			get
			{
				return !double.IsNaN(this.RotationPosition);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00011F18 File Offset: 0x00010118
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x00011F20 File Offset: 0x00010120
		[XmlElement(Order = 9)]
		public string StackNo { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00011F2C File Offset: 0x0001012C
		[XmlIgnore]
		public bool StackNoSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.StackNo);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00011F4C File Offset: 0x0001014C
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x00011F54 File Offset: 0x00010154
		[XmlElement(Order = 10)]
		public string StackingSequence { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00011F60 File Offset: 0x00010160
		[XmlIgnore]
		public bool StackingSequenceSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.StackingSequence);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00011F80 File Offset: 0x00010180
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x00011F88 File Offset: 0x00010188
		[XmlElement(Order = 11)]
		public string StackingLevel { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00011F94 File Offset: 0x00010194
		[XmlIgnore]
		public bool StackingLevelSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.StackingLevel);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00011FB4 File Offset: 0x000101B4
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x00011FBC File Offset: 0x000101BC
		[XmlElement(Order = 12)]
		public double StackingX { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00011FC8 File Offset: 0x000101C8
		[XmlIgnore]
		public bool StackingXSpecified
		{
			get
			{
				return Math.Abs(this.StackingX) >= ItDocument.epsilon || Math.Abs(this.StackingY) >= ItDocument.epsilon || Math.Abs(this.StackingZ) >= ItDocument.epsilon;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00012016 File Offset: 0x00010216
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0001201E File Offset: 0x0001021E
		[XmlElement(Order = 13)]
		public double StackingY { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00012028 File Offset: 0x00010228
		[XmlIgnore]
		public bool StackingYSpecified
		{
			get
			{
				return Math.Abs(this.StackingX) >= ItDocument.epsilon || Math.Abs(this.StackingY) >= ItDocument.epsilon || Math.Abs(this.StackingZ) >= ItDocument.epsilon;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00012076 File Offset: 0x00010276
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0001207E File Offset: 0x0001027E
		[XmlElement(Order = 14)]
		public double StackingZ { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00012088 File Offset: 0x00010288
		[XmlIgnore]
		public bool StackingZSpecified
		{
			get
			{
				return Math.Abs(this.StackingX) >= ItDocument.epsilon || Math.Abs(this.StackingY) >= ItDocument.epsilon || Math.Abs(this.StackingZ) >= ItDocument.epsilon;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x000120D8 File Offset: 0x000102D8
		[XmlIgnore]
		public bool StackingAngleSpecified
		{
			get
			{
				return !double.IsNaN(this.StackingAngle);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x000120F8 File Offset: 0x000102F8
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x00012140 File Offset: 0x00010340
		[XmlElement(Order = 16)]
		public double P1X
		{
			get
			{
				return (this.ProjectCoordinates != null && this.ProjectCoordinates.Origin != null) ? this.ProjectCoordinates.Origin.x : 0.0;
			}
			set
			{
				bool flag = this.ProjectCoordinates == null;
				if (flag)
				{
					this.ProjectCoordinates = new ProjectCoordinates();
				}
				bool flag2 = this.ProjectCoordinates.Origin == null;
				if (flag2)
				{
					this.ProjectCoordinates.Origin = new ItGePoint3d(value, 0.0, 0.0, null);
				}
				else
				{
					this.ProjectCoordinates.Origin.x = value;
				}
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x000121B8 File Offset: 0x000103B8
		[XmlIgnore]
		public bool P1XSpecified
		{
			get
			{
				return Math.Abs(this.P1X) >= ItDocument.epsilon || Math.Abs(this.P1Y) >= ItDocument.epsilon || Math.Abs(this.P1Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00012208 File Offset: 0x00010408
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00012250 File Offset: 0x00010450
		[XmlElement(Order = 17)]
		public double P1Y
		{
			get
			{
				return (this.ProjectCoordinates != null && this.ProjectCoordinates.Origin != null) ? this.ProjectCoordinates.Origin.y : 0.0;
			}
			set
			{
				bool flag = this.ProjectCoordinates == null;
				if (flag)
				{
					this.ProjectCoordinates = new ProjectCoordinates();
				}
				bool flag2 = this.ProjectCoordinates.Origin == null;
				if (flag2)
				{
					this.ProjectCoordinates.Origin = new ItGePoint3d(0.0, value, 0.0, null);
				}
				else
				{
					this.ProjectCoordinates.Origin.y = value;
				}
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x000122C8 File Offset: 0x000104C8
		[XmlIgnore]
		public bool P1YSpecified
		{
			get
			{
				return Math.Abs(this.P1X) >= ItDocument.epsilon || Math.Abs(this.P1Y) >= ItDocument.epsilon || Math.Abs(this.P1Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00012318 File Offset: 0x00010518
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x00012360 File Offset: 0x00010560
		[XmlElement(Order = 18)]
		public double P1Z
		{
			get
			{
				return (this.ProjectCoordinates != null && this.ProjectCoordinates.Origin != null) ? this.ProjectCoordinates.Origin.z : 0.0;
			}
			set
			{
				bool flag = this.ProjectCoordinates == null;
				if (flag)
				{
					this.ProjectCoordinates = new ProjectCoordinates();
				}
				bool flag2 = this.ProjectCoordinates.Origin == null;
				if (flag2)
				{
					this.ProjectCoordinates.Origin = new ItGePoint3d(0.0, 0.0, value, null);
				}
				else
				{
					this.ProjectCoordinates.Origin.z = value;
				}
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x000123D8 File Offset: 0x000105D8
		[XmlIgnore]
		public bool P1ZSpecified
		{
			get
			{
				return Math.Abs(this.P1X) >= ItDocument.epsilon || Math.Abs(this.P1Y) >= ItDocument.epsilon || Math.Abs(this.P1Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00012428 File Offset: 0x00010628
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x00012470 File Offset: 0x00010670
		[XmlElement(Order = 19)]
		public double P2X
		{
			get
			{
				return (this.ProjectCoordinates != null && this.ProjectCoordinates.YPoint != null) ? this.ProjectCoordinates.YPoint.x : 0.0;
			}
			set
			{
				bool flag = this.ProjectCoordinates == null;
				if (flag)
				{
					this.ProjectCoordinates = new ProjectCoordinates();
				}
				bool flag2 = this.ProjectCoordinates.YPoint == null;
				if (flag2)
				{
					this.ProjectCoordinates.YPoint = new ItGePoint3d(value, 0.0, 0.0, null);
				}
				else
				{
					this.ProjectCoordinates.YPoint.x = value;
				}
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x000124E8 File Offset: 0x000106E8
		[XmlIgnore]
		public bool P2XSpecified
		{
			get
			{
				return Math.Abs(this.P2X) >= ItDocument.epsilon || Math.Abs(this.P2Y) >= ItDocument.epsilon || Math.Abs(this.P2Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00012538 File Offset: 0x00010738
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x00012580 File Offset: 0x00010780
		[XmlElement(Order = 20)]
		public double P2Y
		{
			get
			{
				return (this.ProjectCoordinates != null && this.ProjectCoordinates.YPoint != null) ? this.ProjectCoordinates.YPoint.y : 0.0;
			}
			set
			{
				bool flag = this.ProjectCoordinates == null;
				if (flag)
				{
					this.ProjectCoordinates = new ProjectCoordinates();
				}
				bool flag2 = this.ProjectCoordinates.YPoint == null;
				if (flag2)
				{
					this.ProjectCoordinates.YPoint = new ItGePoint3d(0.0, value, 0.0, null);
				}
				else
				{
					this.ProjectCoordinates.YPoint.y = value;
				}
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x000125F8 File Offset: 0x000107F8
		[XmlIgnore]
		public bool P2YSpecified
		{
			get
			{
				return Math.Abs(this.P2X) >= ItDocument.epsilon || Math.Abs(this.P2Y) >= ItDocument.epsilon || Math.Abs(this.P2Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00012648 File Offset: 0x00010848
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x00012690 File Offset: 0x00010890
		[XmlElement(Order = 21)]
		public double P2Z
		{
			get
			{
				return (this.ProjectCoordinates != null && this.ProjectCoordinates.YPoint != null) ? this.ProjectCoordinates.YPoint.z : 0.0;
			}
			set
			{
				bool flag = this.ProjectCoordinates == null;
				if (flag)
				{
					this.ProjectCoordinates = new ProjectCoordinates();
				}
				bool flag2 = this.ProjectCoordinates.YPoint == null;
				if (flag2)
				{
					this.ProjectCoordinates.YPoint = new ItGePoint3d(0.0, 0.0, value, null);
				}
				else
				{
					this.ProjectCoordinates.YPoint.z = value;
				}
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00012708 File Offset: 0x00010908
		[XmlIgnore]
		public bool P2ZSpecified
		{
			get
			{
				return Math.Abs(this.P2X) >= ItDocument.epsilon || Math.Abs(this.P2Y) >= ItDocument.epsilon || Math.Abs(this.P2Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00012758 File Offset: 0x00010958
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x000127A0 File Offset: 0x000109A0
		[XmlElement(Order = 22)]
		public double P3X
		{
			get
			{
				return (this.ProjectCoordinates != null && this.ProjectCoordinates.XPoint != null) ? this.ProjectCoordinates.XPoint.x : 0.0;
			}
			set
			{
				bool flag = this.ProjectCoordinates == null;
				if (flag)
				{
					this.ProjectCoordinates = new ProjectCoordinates();
				}
				bool flag2 = this.ProjectCoordinates.XPoint == null;
				if (flag2)
				{
					this.ProjectCoordinates.XPoint = new ItGePoint3d(value, 0.0, 0.0, null);
				}
				else
				{
					this.ProjectCoordinates.XPoint.x = value;
				}
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00012818 File Offset: 0x00010A18
		[XmlIgnore]
		public bool P3XSpecified
		{
			get
			{
				return Math.Abs(this.P3X) >= ItDocument.epsilon || Math.Abs(this.P3Y) >= ItDocument.epsilon || Math.Abs(this.P3Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00012868 File Offset: 0x00010A68
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x000128B0 File Offset: 0x00010AB0
		[XmlElement(Order = 23)]
		public double P3Y
		{
			get
			{
				return (this.ProjectCoordinates != null && this.ProjectCoordinates.XPoint != null) ? this.ProjectCoordinates.XPoint.y : 0.0;
			}
			set
			{
				bool flag = this.ProjectCoordinates == null;
				if (flag)
				{
					this.ProjectCoordinates = new ProjectCoordinates();
				}
				bool flag2 = this.ProjectCoordinates.XPoint == null;
				if (flag2)
				{
					this.ProjectCoordinates.XPoint = new ItGePoint3d(0.0, value, 0.0, null);
				}
				else
				{
					this.ProjectCoordinates.XPoint.y = value;
				}
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00012928 File Offset: 0x00010B28
		[XmlIgnore]
		public bool P3YSpecified
		{
			get
			{
				return Math.Abs(this.P3X) >= ItDocument.epsilon || Math.Abs(this.P3Y) >= ItDocument.epsilon || Math.Abs(this.P3Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00012978 File Offset: 0x00010B78
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x000129C0 File Offset: 0x00010BC0
		[XmlElement(Order = 24)]
		public double P3Z
		{
			get
			{
				return (this.ProjectCoordinates != null && this.ProjectCoordinates.XPoint != null) ? this.ProjectCoordinates.XPoint.z : 0.0;
			}
			set
			{
				bool flag = this.ProjectCoordinates == null;
				if (flag)
				{
					this.ProjectCoordinates = new ProjectCoordinates();
				}
				bool flag2 = this.ProjectCoordinates.XPoint == null;
				if (flag2)
				{
					this.ProjectCoordinates.XPoint = new ItGePoint3d(0.0, 0.0, value, null);
				}
				else
				{
					this.ProjectCoordinates.XPoint.z = value;
				}
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00012A38 File Offset: 0x00010C38
		[XmlIgnore]
		public bool P3ZSpecified
		{
			get
			{
				return Math.Abs(this.P3X) >= ItDocument.epsilon || Math.Abs(this.P3Y) >= ItDocument.epsilon || Math.Abs(this.P3Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00012A86 File Offset: 0x00010C86
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x00012A8E File Offset: 0x00010C8E
		[XmlElement(Order = 25)]
		public string AdditionInfo { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00012A98 File Offset: 0x00010C98
		[XmlIgnore]
		public bool AdditionInfoSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.AdditionInfo);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00012AB8 File Offset: 0x00010CB8
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x00012AC0 File Offset: 0x00010CC0
		[XmlElement(Order = 26)]
		public string UnloadingInfo { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00012ACC File Offset: 0x00010CCC
		[XmlIgnore]
		public bool UnloadingInfoSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.UnloadingInfo);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00012AEC File Offset: 0x00010CEC
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x00012AF4 File Offset: 0x00010CF4
		[XmlElement(Order = 27)]
		public string TransportInfo { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00012B00 File Offset: 0x00010D00
		[XmlIgnore]
		public bool TransportInfoSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.TransportInfo);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00012B20 File Offset: 0x00010D20
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00012B28 File Offset: 0x00010D28
		[XmlElement(Order = 28)]
		public string ItemPosition { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00012B34 File Offset: 0x00010D34
		[XmlIgnore]
		public bool ItemPositionSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ItemPosition);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00012B54 File Offset: 0x00010D54
		[XmlIgnore]
		public bool slabListSpecified
		{
			get
			{
				return this.slabList.Count > 0;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00012B74 File Offset: 0x00010D74
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00012B7C File Offset: 0x00010D7C
		[XmlIgnore]
		internal ProjectCoordinates ProjectCoordinates { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00012B85 File Offset: 0x00010D85
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00012B8D File Offset: 0x00010D8D
		[XmlIgnore]
		public bool IsInitialized { get; private set; }

		// Token: 0x06000425 RID: 1061 RVA: 0x00012B98 File Offset: 0x00010D98
		public ItProduct()
		{
			base.WBI();
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00012BFC File Offset: 0x00010DFC
		public void Initialize(ItMachineDataBase machineDataBase, AssemblyInstance assemblyInstance, IEnumerable<RevitElement<Part>> partsList, ItGeMatrix3d matTranslateToOrigin, ItGePoint3d startPoint, ItGeVector3d spanDirection, ItGeVector3d shiftingDirection, Parameter paramProdNo)
		{
			double totalThickness = partsList.isNotNull() ? partsList.Sum((RevitElement<Part> part) => machineDataBase.getThickness(part.getLocalExtents(false))) : -1.0;
			startPoint.transformBy(matTranslateToOrigin);
			ItGeBoundBlock3d buildingExtents = machineDataBase.getBuildingExtents(assemblyInstance.Document);
			ProjectCoordinates projectCoordinates = new ProjectCoordinates();
			ItGeMatrix3d mat = assemblyInstance.ecs();
			ItGeVector3d spanDirection2 = mat * spanDirection;
			ItGeVector3d shiftingDirection2 = mat * shiftingDirection;
			ItGePoint3d startPoint2 = mat * startPoint;
			machineDataBase.setProjectCoordinates(projectCoordinates, startPoint2, buildingExtents, spanDirection2, shiftingDirection2);
			this.ProductType = machineDataBase.ProductTypeString;
			this.ElementNo = paramProdNo.AsString();
			this.TotalThickness = totalThickness;
			this.PieceCount = assemblyInstance.assemblyType().instances().Count;
			this.ProjectCoordinates = projectCoordinates;
			this.IsInitialized = true;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00012CEC File Offset: 0x00010EEC
		public void ConvertUnits()
		{
			this.ProjectCoordinates.Origin = ItDocument.convertToMM(this.ProjectCoordinates.Origin);
			this.ProjectCoordinates.YPoint = ItDocument.convertToMM(this.ProjectCoordinates.YPoint);
			this.ProjectCoordinates.XPoint = ItDocument.convertToMM(this.ProjectCoordinates.XPoint);
			bool totalThicknessSpecified = this.TotalThicknessSpecified;
			if (totalThicknessSpecified)
			{
				this.TotalThickness = ItDocument.convertToMM(this.TotalThickness);
			}
			bool doubleWallsGapSpecified = this.DoubleWallsGapSpecified;
			if (doubleWallsGapSpecified)
			{
				this.DoubleWallsGap = ItDocument.convertToMM(this.DoubleWallsGap);
			}
			bool turnMoveXSpecified = this.TurnMoveXSpecified;
			if (turnMoveXSpecified)
			{
				this.TurnMoveX = ItDocument.convertToMM(this.TurnMoveX);
			}
			bool turnWidthSpecified = this.TurnWidthSpecified;
			if (turnWidthSpecified)
			{
				this.TurnWidth = ItDocument.convertToMM(this.TurnWidth);
			}
			foreach (ItSlab itSlab in this.slabList)
			{
				bool xspecified = itSlab.XSpecified;
				if (xspecified)
				{
					itSlab.X = ItDocument.convertToMM(itSlab.X);
				}
				bool yspecified = itSlab.YSpecified;
				if (yspecified)
				{
					itSlab.Y = ItDocument.convertToMM(itSlab.Y);
				}
				bool zspecified = itSlab.ZSpecified;
				if (zspecified)
				{
					itSlab.Z = ItDocument.convertToMM(itSlab.Z);
				}
				bool maxLengthSpecified = itSlab.MaxLengthSpecified;
				if (maxLengthSpecified)
				{
					itSlab.MaxLength = ItDocument.convertToMM(itSlab.MaxLength);
				}
				bool maxWidthSpecified = itSlab.MaxWidthSpecified;
				if (maxWidthSpecified)
				{
					itSlab.MaxWidth = ItDocument.convertToMM(itSlab.MaxWidth);
				}
				bool ironProjectionBottomSpecified = itSlab.IronProjectionBottomSpecified;
				if (ironProjectionBottomSpecified)
				{
					itSlab.IronProjectionBottom = ItDocument.convertToMM(itSlab.IronProjectionBottom);
				}
				bool ironProjectionLeftSpecified = itSlab.IronProjectionLeftSpecified;
				if (ironProjectionLeftSpecified)
				{
					itSlab.IronProjectionLeft = ItDocument.convertToMM(itSlab.IronProjectionLeft);
				}
				bool ironProjectionRightSpecified = itSlab.IronProjectionRightSpecified;
				if (ironProjectionRightSpecified)
				{
					itSlab.IronProjectionRight = ItDocument.convertToMM(itSlab.IronProjectionRight);
				}
				bool ironProjectionTopSpecified = itSlab.IronProjectionTopSpecified;
				if (ironProjectionTopSpecified)
				{
					itSlab.IronProjectionTop = ItDocument.convertToMM(itSlab.IronProjectionTop);
				}
				bool slabAreaSpecified = itSlab.SlabAreaSpecified;
				if (slabAreaSpecified)
				{
					itSlab.SlabArea = ItDocument.convertToSquareMeter(itSlab.SlabArea);
				}
				bool productionThicknessSpecified = itSlab.ProductionThicknessSpecified;
				if (productionThicknessSpecified)
				{
					itSlab.ProductionThickness = ItDocument.convertToMM(itSlab.ProductionThickness);
				}
				foreach (ItOutline itOutline in itSlab.outlineList)
				{
					bool xspecified2 = itOutline.XSpecified;
					if (xspecified2)
					{
						itOutline.X = ItDocument.convertToMM(itOutline.X);
					}
					bool yspecified2 = itOutline.YSpecified;
					if (yspecified2)
					{
						itOutline.Y = ItDocument.convertToMM(itOutline.Y);
					}
					bool zspecified2 = itOutline.ZSpecified;
					if (zspecified2)
					{
						itOutline.Z = ItDocument.convertToMM(itOutline.Z);
					}
					bool mountPartLengthSpecified = itOutline.MountPartLengthSpecified;
					if (mountPartLengthSpecified)
					{
						itOutline.MountPartLength = ItDocument.convertToMM(itOutline.MountPartLength);
					}
					bool mountPartWidthSpecified = itOutline.MountPartWidthSpecified;
					if (mountPartWidthSpecified)
					{
						itOutline.MountPartWidth = ItDocument.convertToMM(itOutline.MountPartWidth);
					}
					bool unitWeightSpecified = itOutline.UnitWeightSpecified;
					if (unitWeightSpecified)
					{
						itOutline.UnitWeight = ItDocument.convertToWeightPerCubeMeter(itOutline.UnitWeight);
					}
					bool volumeSpecified = itOutline.VolumeSpecified;
					if (volumeSpecified)
					{
						itOutline.Volume = this.ConvertToCubeMeter(itOutline.Volume);
					}
					bool heightSpecified = itOutline.HeightSpecified;
					if (heightSpecified)
					{
						itOutline.Height = ItDocument.convertToMM(itOutline.Height);
					}
					foreach (ItShape itShape in itOutline.shapeList)
					{
						bool refHeightSpecified = itShape.RefHeightSpecified;
						if (refHeightSpecified)
						{
							itShape.RefHeight = ItDocument.convertToMM(itShape.RefHeight);
						}
						foreach (ItSVertex itSVertex in itShape.sVertexList)
						{
							bool bulgeSpecified = itSVertex.BulgeSpecified;
							if (bulgeSpecified)
							{
								itSVertex.Bulge = ItDocument.convertToMM(itSVertex.Bulge);
							}
							bool xspecified3 = itSVertex.XSpecified;
							if (xspecified3)
							{
								itSVertex.X = ItDocument.convertToMM(itSVertex.X);
							}
							bool yspecified3 = itSVertex.YSpecified;
							if (yspecified3)
							{
								itSVertex.Y = ItDocument.convertToMM(itSVertex.Y);
							}
						}
					}
				}
				foreach (ItSteel itSteel in itSlab.steelList)
				{
					bool xspecified4 = itSteel.XSpecified;
					if (xspecified4)
					{
						itSteel.X = ItDocument.convertToMM(itSteel.X);
					}
					bool yspecified4 = itSteel.YSpecified;
					if (yspecified4)
					{
						itSteel.Y = ItDocument.convertToMM(itSteel.Y);
					}
					bool zspecified3 = itSteel.ZSpecified;
					if (zspecified3)
					{
						itSteel.Z = ItDocument.convertToMM(itSteel.Z);
					}
					foreach (ItBar itBar in itSteel.barList)
					{
						bool xspecified5 = itBar.XSpecified;
						if (xspecified5)
						{
							itBar.X = ItDocument.convertToMM(itBar.X);
						}
						bool yspecified5 = itBar.YSpecified;
						if (yspecified5)
						{
							itBar.Y = ItDocument.convertToMM(itBar.Y);
						}
						bool zspecified4 = itBar.ZSpecified;
						if (zspecified4)
						{
							itBar.Z = ItDocument.convertToMM(itBar.Z);
						}
						bool diameterSpecified = itBar.DiameterSpecified;
						if (diameterSpecified)
						{
							itBar.Diameter = ItDocument.convertToMM(itBar.Diameter);
						}
						foreach (ItSegment itSegment in itBar.segmentList)
						{
							bool lspecified = itSegment.LSpecified;
							if (lspecified)
							{
								itSegment.L = ItDocument.convertToMM(itSegment.L);
							}
							bool rspecified = itSegment.RSpecified;
							if (rspecified)
							{
								itSegment.R = ItDocument.convertToMM(itSegment.R);
							}
						}
						foreach (ItWeldingPoint itWeldingPoint in itBar.weldingPointList)
						{
							bool positionSpecified = itWeldingPoint.PositionSpecified;
							if (positionSpecified)
							{
								itWeldingPoint.Position = ItDocument.convertToMM(itWeldingPoint.Position);
							}
						}
						foreach (ItSpacer itSpacer in itBar.spacerList)
						{
							bool positionSpecified2 = itSpacer.PositionSpecified;
							if (positionSpecified2)
							{
								itSpacer.Position = ItDocument.convertToMM(itSpacer.Position);
							}
						}
					}
					foreach (ItGirder itGirder in itSteel.girderList)
					{
						bool xspecified6 = itGirder.XSpecified;
						if (xspecified6)
						{
							itGirder.X = ItDocument.convertToMM(itGirder.X);
						}
						bool yspecified6 = itGirder.YSpecified;
						if (yspecified6)
						{
							itGirder.Y = ItDocument.convertToMM(itGirder.Y);
						}
						bool zspecified5 = itGirder.ZSpecified;
						if (zspecified5)
						{
							itGirder.Z = ItDocument.convertToMM(itGirder.Z);
						}
						bool lengthSpecified = itGirder.LengthSpecified;
						if (lengthSpecified)
						{
							itGirder.Length = ItDocument.convertToMM(itGirder.Length);
						}
						bool heightSpecified2 = itGirder.HeightSpecified;
						if (heightSpecified2)
						{
							itGirder.Height = ItDocument.convertToMM(itGirder.Height);
						}
						bool topExcessSpecified = itGirder.TopExcessSpecified;
						if (topExcessSpecified)
						{
							itGirder.TopExcess = ItDocument.convertToMM(itGirder.TopExcess);
						}
						bool bottomExcessSpecified = itGirder.BottomExcessSpecified;
						if (bottomExcessSpecified)
						{
							itGirder.BottomExcess = ItDocument.convertToMM(itGirder.BottomExcess);
						}
						bool topFlangeDiameterSpecified = itGirder.TopFlangeDiameterSpecified;
						if (topFlangeDiameterSpecified)
						{
							itGirder.TopFlangeDiameter = ItDocument.convertToMM(itGirder.TopFlangeDiameter);
						}
						bool bottomFlangeDiameterSpecified = itGirder.BottomFlangeDiameterSpecified;
						if (bottomFlangeDiameterSpecified)
						{
							itGirder.BottomFlangeDiameter = ItDocument.convertToMM(itGirder.BottomFlangeDiameter);
						}
						bool widthSpecified = itGirder.WidthSpecified;
						if (widthSpecified)
						{
							itGirder.Width = ItDocument.convertToMM(itGirder.Width);
						}
						bool periodSpecified = itGirder.PeriodSpecified;
						if (periodSpecified)
						{
							itGirder.Period = ItDocument.convertToMM(itGirder.Period);
						}
						bool periodOffsetSpecified = itGirder.PeriodOffsetSpecified;
						if (periodOffsetSpecified)
						{
							itGirder.PeriodOffset = ItDocument.convertToMM(itGirder.PeriodOffset);
						}
						foreach (ItAnchorBar itAnchorBar in itGirder.anchorBarList)
						{
							bool lengthSpecified2 = itAnchorBar.LengthSpecified;
							if (lengthSpecified2)
							{
								itAnchorBar.Length = ItDocument.convertToMM(itAnchorBar.Length);
							}
							bool positionSpecified3 = itAnchorBar.PositionSpecified;
							if (positionSpecified3)
							{
								itAnchorBar.Position = ItDocument.convertToMM(itAnchorBar.Position);
							}
						}
						foreach (ItGirderExt itGirderExt in itGirder.girderExtList)
						{
							bool positionSpecified4 = itGirderExt.PositionSpecified;
							if (positionSpecified4)
							{
								itGirderExt.Position = ItDocument.convertToMM(itGirderExt.Position);
							}
						}
						foreach (ItSection itSection in itGirder.sectionList)
						{
							bool lspecified2 = itSection.LSpecified;
							if (lspecified2)
							{
								itSection.L = ItDocument.convertToMM(itSection.L);
							}
							bool sspecified = itSection.SSpecified;
							if (sspecified)
							{
								itSection.S = ItDocument.convertToMM(itSection.S);
							}
						}
					}
					foreach (ItAlloc itAlloc in itSteel.allocList)
					{
						foreach (ItRegion itRegion in itAlloc.regionList)
						{
							bool pitchSpecified = itRegion.PitchSpecified;
							if (pitchSpecified)
							{
								itRegion.Pitch = ItDocument.convertToMM(itRegion.Pitch);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001391C File Offset: 0x00011B1C
		private double ConvertToCubeMeter(double outlineVolume)
		{
			return outlineVolume.FeetToMeter().FeetToMeter().FeetToMeter();
		}

		// Token: 0x04000137 RID: 311
		[XmlElement(Order = 5)]
		public double TurnWidth = double.NaN;

		// Token: 0x04000138 RID: 312
		[XmlElement(Order = 6)]
		public double TurnMoveX = double.NaN;

		// Token: 0x0400013A RID: 314
		[XmlElement(Order = 8)]
		public double RotationPosition = double.NaN;

		// Token: 0x04000141 RID: 321
		[XmlElement(Order = 15)]
		public double StackingAngle = double.NaN;

		// Token: 0x04000146 RID: 326
		[XmlElement("Slab", Order = 29)]
		public List<ItSlab> slabList = new List<ItSlab>();
	}
}
