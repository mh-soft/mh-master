using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Hao.Export.Geometry;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x02000035 RID: 53
	public class ItOutline : ItPXMLItem
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00011855 File Offset: 0x0000FA55
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0001185D File Offset: 0x0000FA5D
		[XmlElement(Order = 0)]
		public double X { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00011868 File Offset: 0x0000FA68
		[XmlIgnore]
		public bool XSpecified
		{
			get
			{
				return Math.Abs(this.X) >= ItDocument.epsilon || Math.Abs(this.Y) >= ItDocument.epsilon || Math.Abs(this.Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000399 RID: 921 RVA: 0x000118B6 File Offset: 0x0000FAB6
		// (set) Token: 0x0600039A RID: 922 RVA: 0x000118BE File Offset: 0x0000FABE
		[XmlElement(Order = 1)]
		public double Y { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600039B RID: 923 RVA: 0x000118C8 File Offset: 0x0000FAC8
		[XmlIgnore]
		public bool YSpecified
		{
			get
			{
				return Math.Abs(this.X) >= ItDocument.epsilon || Math.Abs(this.Y) >= ItDocument.epsilon || Math.Abs(this.Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00011916 File Offset: 0x0000FB16
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0001191E File Offset: 0x0000FB1E
		[XmlElement(Order = 2)]
		public double Z { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00011928 File Offset: 0x0000FB28
		[XmlIgnore]
		public bool ZSpecified
		{
			get
			{
				return Math.Abs(this.X) >= ItDocument.epsilon || Math.Abs(this.Y) >= ItDocument.epsilon || Math.Abs(this.Z) >= ItDocument.epsilon;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00011976 File Offset: 0x0000FB76
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0001197E File Offset: 0x0000FB7E
		[XmlElement(Order = 3)]
		public double Height { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00011988 File Offset: 0x0000FB88
		[XmlIgnore]
		public bool HeightSpecified
		{
			get
			{
				return this.Height > ItDocument.epsilon;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x000119A7 File Offset: 0x0000FBA7
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x000119AF File Offset: 0x0000FBAF
		[XmlElement(Order = 4)]
		public string Name { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x000119B8 File Offset: 0x0000FBB8
		[XmlIgnore]
		public bool NameSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.Name);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x000119D8 File Offset: 0x0000FBD8
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x000119E0 File Offset: 0x0000FBE0
		[XmlElement(Order = 5)]
		public string GenericInfo01 { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x000119EC File Offset: 0x0000FBEC
		[XmlIgnore]
		public bool GenericInfo01Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericInfo01);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00011A0C File Offset: 0x0000FC0C
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x00011A14 File Offset: 0x0000FC14
		[XmlElement(Order = 6)]
		public string GenericInfo02 { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00011A20 File Offset: 0x0000FC20
		[XmlIgnore]
		public bool GenericInfo02Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericInfo02);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00011A40 File Offset: 0x0000FC40
		// (set) Token: 0x060003AC RID: 940 RVA: 0x00011A48 File Offset: 0x0000FC48
		[XmlElement(Order = 7)]
		public int MountingInstruction { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00011A54 File Offset: 0x0000FC54
		[XmlIgnore]
		public bool MountingInstructionSpecified
		{
			get
			{
				return this.MountingInstruction > 0;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00011A6F File Offset: 0x0000FC6F
		// (set) Token: 0x060003AF RID: 943 RVA: 0x00011A77 File Offset: 0x0000FC77
		[XmlElement(Order = 8)]
		public string MountPartType { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00011A80 File Offset: 0x0000FC80
		[XmlIgnore]
		public bool MountPartTypeSpecified
		{
			get
			{
				return this.type == "mountpart";
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00011AA2 File Offset: 0x0000FCA2
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x00011AAA File Offset: 0x0000FCAA
		[XmlElement(Order = 9)]
		public string MountPartArticle { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00011AB4 File Offset: 0x0000FCB4
		[XmlIgnore]
		public bool MountPartArticleSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.MountPartArticle);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00011AD4 File Offset: 0x0000FCD4
		[XmlIgnore]
		public bool MountPartIronProjectionSpecified
		{
			get
			{
				return !double.IsNaN(this.MountPartIronProjection);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00011AF4 File Offset: 0x0000FCF4
		[XmlIgnore]
		public bool MountPartDirectionSpecified
		{
			get
			{
				return !double.IsNaN(this.MountPartDirection);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00011B14 File Offset: 0x0000FD14
		[XmlIgnore]
		public bool MountPartLengthSpecified
		{
			get
			{
				return !double.IsNaN(this.MountPartLength);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00011B34 File Offset: 0x0000FD34
		[XmlIgnore]
		public bool MountPartWidthSpecified
		{
			get
			{
				return !double.IsNaN(this.MountPartWidth);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00011B54 File Offset: 0x0000FD54
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x00011B5C File Offset: 0x0000FD5C
		[XmlElement(Order = 14)]
		public string ConcretingMode { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00011B68 File Offset: 0x0000FD68
		[XmlIgnore]
		public bool ConcretingModeSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ConcretingMode);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003BB RID: 955 RVA: 0x00011B88 File Offset: 0x0000FD88
		// (set) Token: 0x060003BC RID: 956 RVA: 0x00011B90 File Offset: 0x0000FD90
		[XmlElement(Order = 15)]
		public string ConcreteQuality { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00011B9C File Offset: 0x0000FD9C
		[XmlIgnore]
		public bool ConcreteQualitySpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ConcreteQuality);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00011BBC File Offset: 0x0000FDBC
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00011BC4 File Offset: 0x0000FDC4
		[XmlElement(Order = 16)]
		public double UnitWeight { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00011BD0 File Offset: 0x0000FDD0
		[XmlIgnore]
		public bool UnitWeightSpecified
		{
			get
			{
				return this.UnitWeight > ItDocument.epsilon;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00011BEF File Offset: 0x0000FDEF
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x00011BF7 File Offset: 0x0000FDF7
		[XmlElement(Order = 17)]
		public double Volume { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00011C00 File Offset: 0x0000FE00
		[XmlIgnore]
		public bool VolumeSpecified
		{
			get
			{
				return this.Volume > ItDocument.epsilon;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00011C1F File Offset: 0x0000FE1F
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00011C27 File Offset: 0x0000FE27
		[XmlElement(Order = 18)]
		public string Layer { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00011C30 File Offset: 0x0000FE30
		[XmlIgnore]
		public bool LayerSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.Layer);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00011C50 File Offset: 0x0000FE50
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x00011C58 File Offset: 0x0000FE58
		[XmlElement(Order = 19)]
		public string ObjectID { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00011C64 File Offset: 0x0000FE64
		[XmlIgnore]
		public bool ObjectIDpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ObjectID);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00011C84 File Offset: 0x0000FE84
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00011C8C File Offset: 0x0000FE8C
		[XmlAttribute(DataType = "string", AttributeName = "Type")]
		public string type { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00011C98 File Offset: 0x0000FE98
		[XmlIgnore]
		public bool shapeListSpecified
		{
			get
			{
				return this.shapeList.Count > 0;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00011CB8 File Offset: 0x0000FEB8
		// (set) Token: 0x060003CE RID: 974 RVA: 0x00011CC0 File Offset: 0x0000FEC0
		[XmlIgnore]
		public bool isMainOutline { get; set; }

		// Token: 0x060003CF RID: 975 RVA: 0x00011CCC File Offset: 0x0000FECC
		public ItOutline()
		{
			base.WBI();
			this.isMainOutline = false;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00011D38 File Offset: 0x0000FF38
		public void TranslateBy(ItGeMatrix3d translation)
		{
			ItGePoint3d itGePoint3d = new ItGePoint3d(this.X, this.Y, this.Z, null);
			itGePoint3d.transformBy(translation);
			this.X = itGePoint3d.x;
			this.Y = itGePoint3d.y;
			this.Z = itGePoint3d.z;
		}

		// Token: 0x04000125 RID: 293
		[XmlElement(Order = 10)]
		public double MountPartIronProjection = double.NaN;

		// Token: 0x04000126 RID: 294
		[XmlElement(Order = 11)]
		public double MountPartDirection = double.NaN;

		// Token: 0x04000127 RID: 295
		[XmlElement(Order = 12)]
		public double MountPartLength = double.NaN;

		// Token: 0x04000128 RID: 296
		[XmlElement(Order = 13)]
		public double MountPartWidth = double.NaN;

		// Token: 0x04000130 RID: 304
		[XmlElement("Shape", Order = 20)]
		public List<ItShape> shapeList = new List<ItShape>();

		// Token: 0x02000082 RID: 130
		public static class Types
		{
			// Token: 0x040002D1 RID: 721
			public const string Lot = "lot";

			// Token: 0x040002D2 RID: 722
			public const string MountPart = "mountpart";
		}

		// Token: 0x02000083 RID: 131
		public enum MountPartTypes
		{
			// Token: 0x040002D4 RID: 724
			NoIdentification,
			// Token: 0x040002D5 RID: 725
			FibreConcreteRaisedRim,
			// Token: 0x040002D6 RID: 726
			SolidConcreteRaisedRim,
			// Token: 0x040002D7 RID: 727
			Insulation,
			// Token: 0x040002D8 RID: 728
			InsulationBasket,
			// Token: 0x040002D9 RID: 729
			WeldedElementJoint,
			// Token: 0x040002DA RID: 730
			CentreForHollowFloor,
			// Token: 0x040002DB RID: 731
			ElectricalInstallation,
			// Token: 0x040002DC RID: 732
			SanitaryInstallation,
			// Token: 0x040002DD RID: 733
			FasteningTechnology,
			// Token: 0x040002DE RID: 734
			Magnet,
			// Token: 0x040002DF RID: 735
			MagnetsSpecialType,
			// Token: 0x040002E0 RID: 736
			FrameOfDoorsOrWindows,
			// Token: 0x040002E1 RID: 737
			CutoutBoxes = 21,
			// Token: 0x040002E2 RID: 738
			WallOutletJoint,
			// Token: 0x040002E3 RID: 739
			PositionOfPrefabricatedReinforcement = 31
		}
	}
}
