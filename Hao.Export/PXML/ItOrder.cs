using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x02000034 RID: 52
	public class ItOrder : ItPXMLItem
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600032B RID: 811 RVA: 0x000113AF File Offset: 0x0000F5AF
		// (set) Token: 0x0600032C RID: 812 RVA: 0x000113B7 File Offset: 0x0000F5B7
		[XmlElement(Order = 0)]
		public string OrderNo { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600032D RID: 813 RVA: 0x000113C0 File Offset: 0x0000F5C0
		[XmlIgnore]
		public bool OrderNoSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.OrderNo);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600032E RID: 814 RVA: 0x000113D0 File Offset: 0x0000F5D0
		// (set) Token: 0x0600032F RID: 815 RVA: 0x000113D8 File Offset: 0x0000F5D8
		[XmlElement(Order = 1)]
		public string Component { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000330 RID: 816 RVA: 0x000113E1 File Offset: 0x0000F5E1
		[XmlIgnore]
		public bool ComponentSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.Component);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000331 RID: 817 RVA: 0x000113F1 File Offset: 0x0000F5F1
		// (set) Token: 0x06000332 RID: 818 RVA: 0x000113F9 File Offset: 0x0000F5F9
		[XmlElement(Order = 2)]
		public string Storey { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00011402 File Offset: 0x0000F602
		[XmlIgnore]
		public bool StoreySpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.Storey);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00011412 File Offset: 0x0000F612
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0001141A File Offset: 0x0000F61A
		[XmlElement(Order = 3)]
		public string DrawingNo { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00011423 File Offset: 0x0000F623
		[XmlIgnore]
		public bool DrawingNoSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.DrawingNo);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00011433 File Offset: 0x0000F633
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0001143B File Offset: 0x0000F63B
		[XmlElement(Order = 4)]
		public string DrawingDate { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00011444 File Offset: 0x0000F644
		[XmlIgnore]
		public bool DrawingDateSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.DrawingDate);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00011454 File Offset: 0x0000F654
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0001145C File Offset: 0x0000F65C
		[XmlElement(Order = 5)]
		public string DrawingRevision { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00011465 File Offset: 0x0000F665
		[XmlIgnore]
		public bool DrawingRevisionSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.DrawingRevision);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00011475 File Offset: 0x0000F675
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0001147D File Offset: 0x0000F67D
		[XmlElement(Order = 6)]
		public string DrawingAuthor { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00011486 File Offset: 0x0000F686
		[XmlIgnore]
		public bool DrawingAuthorSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.DrawingAuthor);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00011496 File Offset: 0x0000F696
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0001149E File Offset: 0x0000F69E
		[XmlElement(Order = 7)]
		public string Comment { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000342 RID: 834 RVA: 0x000114A7 File Offset: 0x0000F6A7
		[XmlIgnore]
		public bool CommentSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.Comment);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000343 RID: 835 RVA: 0x000114B7 File Offset: 0x0000F6B7
		// (set) Token: 0x06000344 RID: 836 RVA: 0x000114BF File Offset: 0x0000F6BF
		[XmlElement(Order = 8)]
		public string ImportSource { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000345 RID: 837 RVA: 0x000114C8 File Offset: 0x0000F6C8
		[XmlIgnore]
		public bool ImportSourceSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ImportSource);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000346 RID: 838 RVA: 0x000114D8 File Offset: 0x0000F6D8
		// (set) Token: 0x06000347 RID: 839 RVA: 0x000114E0 File Offset: 0x0000F6E0
		[XmlElement(Order = 9)]
		public string ImportSourceType { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000348 RID: 840 RVA: 0x000114E9 File Offset: 0x0000F6E9
		[XmlIgnore]
		public bool ImportSourceTypeSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ImportSourceType);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000349 RID: 841 RVA: 0x000114F9 File Offset: 0x0000F6F9
		// (set) Token: 0x0600034A RID: 842 RVA: 0x00011501 File Offset: 0x0000F701
		[XmlElement(Order = 10)]
		public string DeliveryDate { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0001150A File Offset: 0x0000F70A
		[XmlIgnore]
		public bool DeliveryDateSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.DeliveryDate);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0001151A File Offset: 0x0000F71A
		// (set) Token: 0x0600034D RID: 845 RVA: 0x00011522 File Offset: 0x0000F722
		[XmlElement(Order = 11)]
		public string GenericOrderInfo01 { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0001152B File Offset: 0x0000F72B
		[XmlIgnore]
		public bool GenericOrderInfo01Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo01);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0001153B File Offset: 0x0000F73B
		// (set) Token: 0x06000350 RID: 848 RVA: 0x00011543 File Offset: 0x0000F743
		[XmlElement(Order = 12)]
		public string GenericOrderInfo02 { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0001154C File Offset: 0x0000F74C
		[XmlIgnore]
		public bool GenericOrderInfo02Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo02);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0001155C File Offset: 0x0000F75C
		// (set) Token: 0x06000353 RID: 851 RVA: 0x00011564 File Offset: 0x0000F764
		[XmlElement(Order = 13)]
		public string GenericOrderInfo03 { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0001156D File Offset: 0x0000F76D
		[XmlIgnore]
		public bool GenericOrderInfo03Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo03);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0001157D File Offset: 0x0000F77D
		// (set) Token: 0x06000356 RID: 854 RVA: 0x00011585 File Offset: 0x0000F785
		[XmlElement(Order = 14)]
		public string GenericOrderInfo04 { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0001158E File Offset: 0x0000F78E
		[XmlIgnore]
		public bool GenericOrderInfo04Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo04);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0001159E File Offset: 0x0000F79E
		// (set) Token: 0x06000359 RID: 857 RVA: 0x000115A6 File Offset: 0x0000F7A6
		[XmlElement(Order = 15)]
		public string GenericOrderInfo05 { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600035A RID: 858 RVA: 0x000115AF File Offset: 0x0000F7AF
		[XmlIgnore]
		public bool GenericOrderInfo05Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo05);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600035B RID: 859 RVA: 0x000115BF File Offset: 0x0000F7BF
		// (set) Token: 0x0600035C RID: 860 RVA: 0x000115C7 File Offset: 0x0000F7C7
		[XmlElement(Order = 16)]
		public string GenericOrderInfo06 { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600035D RID: 861 RVA: 0x000115D0 File Offset: 0x0000F7D0
		[XmlIgnore]
		public bool GenericOrderInfo06Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo06);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600035E RID: 862 RVA: 0x000115E0 File Offset: 0x0000F7E0
		// (set) Token: 0x0600035F RID: 863 RVA: 0x000115E8 File Offset: 0x0000F7E8
		[XmlElement(Order = 17)]
		public string GenericOrderInfo07 { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000360 RID: 864 RVA: 0x000115F1 File Offset: 0x0000F7F1
		[XmlIgnore]
		public bool GenericOrderInfo07Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo07);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00011601 File Offset: 0x0000F801
		// (set) Token: 0x06000362 RID: 866 RVA: 0x00011609 File Offset: 0x0000F809
		[XmlElement(Order = 18)]
		public string GenericOrderInfo08 { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00011612 File Offset: 0x0000F812
		[XmlIgnore]
		public bool GenericOrderInfo08Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo08);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00011622 File Offset: 0x0000F822
		// (set) Token: 0x06000365 RID: 869 RVA: 0x0001162A File Offset: 0x0000F82A
		[XmlElement(Order = 19)]
		public string GenericOrderInfo09 { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00011633 File Offset: 0x0000F833
		[XmlIgnore]
		public bool GenericOrderInfo09Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo09);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00011643 File Offset: 0x0000F843
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0001164B File Offset: 0x0000F84B
		[XmlElement(Order = 20)]
		public string GenericOrderInfo10 { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00011654 File Offset: 0x0000F854
		[XmlIgnore]
		public bool GenericOrderInfo10Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo10);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00011664 File Offset: 0x0000F864
		// (set) Token: 0x0600036B RID: 875 RVA: 0x0001166C File Offset: 0x0000F86C
		[XmlElement(Order = 21)]
		public string GenericOrderInfo11 { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00011675 File Offset: 0x0000F875
		[XmlIgnore]
		public bool GenericOrderInfo11Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo11);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00011685 File Offset: 0x0000F885
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0001168D File Offset: 0x0000F88D
		[XmlElement(Order = 22)]
		public string GenericOrderInfo12 { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00011696 File Offset: 0x0000F896
		[XmlIgnore]
		public bool GenericOrderInfo12Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo12);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000370 RID: 880 RVA: 0x000116A6 File Offset: 0x0000F8A6
		// (set) Token: 0x06000371 RID: 881 RVA: 0x000116AE File Offset: 0x0000F8AE
		[XmlElement(Order = 23)]
		public string GenericOrderInfo13 { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000372 RID: 882 RVA: 0x000116B7 File Offset: 0x0000F8B7
		[XmlIgnore]
		public bool GenericOrderInfo13Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo13);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000373 RID: 883 RVA: 0x000116C7 File Offset: 0x0000F8C7
		// (set) Token: 0x06000374 RID: 884 RVA: 0x000116CF File Offset: 0x0000F8CF
		[XmlElement(Order = 24)]
		public string GenericOrderInfo14 { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000375 RID: 885 RVA: 0x000116D8 File Offset: 0x0000F8D8
		[XmlIgnore]
		public bool GenericOrderInfo14Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo14);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000376 RID: 886 RVA: 0x000116E8 File Offset: 0x0000F8E8
		// (set) Token: 0x06000377 RID: 887 RVA: 0x000116F0 File Offset: 0x0000F8F0
		[XmlElement(Order = 25)]
		public string GenericOrderInfo15 { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000378 RID: 888 RVA: 0x000116F9 File Offset: 0x0000F8F9
		[XmlIgnore]
		public bool GenericOrderInfo15Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo15);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00011709 File Offset: 0x0000F909
		// (set) Token: 0x0600037A RID: 890 RVA: 0x00011711 File Offset: 0x0000F911
		[XmlElement(Order = 26)]
		public string GenericOrderInfo16 { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0001171A File Offset: 0x0000F91A
		[XmlIgnore]
		public bool GenericOrderInfo16Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo16);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0001172A File Offset: 0x0000F92A
		// (set) Token: 0x0600037D RID: 893 RVA: 0x00011732 File Offset: 0x0000F932
		[XmlElement(Order = 27)]
		public string GenericOrderInfo17 { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0001173B File Offset: 0x0000F93B
		[XmlIgnore]
		public bool GenericOrderInfo17Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo17);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0001174B File Offset: 0x0000F94B
		// (set) Token: 0x06000380 RID: 896 RVA: 0x00011753 File Offset: 0x0000F953
		[XmlElement(Order = 28)]
		public string GenericOrderInfo18 { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0001175C File Offset: 0x0000F95C
		[XmlIgnore]
		public bool GenericOrderInfo18Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo18);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0001176C File Offset: 0x0000F96C
		// (set) Token: 0x06000383 RID: 899 RVA: 0x00011774 File Offset: 0x0000F974
		[XmlElement(Order = 29)]
		public string GenericOrderInfo19 { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0001177D File Offset: 0x0000F97D
		[XmlIgnore]
		public bool GenericOrderInfo19Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo19);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0001178D File Offset: 0x0000F98D
		// (set) Token: 0x06000386 RID: 902 RVA: 0x00011795 File Offset: 0x0000F995
		[XmlElement(Order = 30)]
		public string GenericOrderInfo20 { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0001179E File Offset: 0x0000F99E
		[XmlIgnore]
		public bool GenericOrderInfo20Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this.GenericOrderInfo20);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000388 RID: 904 RVA: 0x000117AE File Offset: 0x0000F9AE
		// (set) Token: 0x06000389 RID: 905 RVA: 0x000117B6 File Offset: 0x0000F9B6
		[XmlElement(Order = 31)]
		public string ApplicationName { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600038A RID: 906 RVA: 0x000117BF File Offset: 0x0000F9BF
		[XmlIgnore]
		public bool ApplicationNameSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ApplicationName);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600038B RID: 907 RVA: 0x000117CF File Offset: 0x0000F9CF
		// (set) Token: 0x0600038C RID: 908 RVA: 0x000117D7 File Offset: 0x0000F9D7
		[XmlElement(Order = 32)]
		public string ApplicationGUID { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600038D RID: 909 RVA: 0x000117E0 File Offset: 0x0000F9E0
		[XmlIgnore]
		public bool ApplicationGUIDSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ApplicationGUID);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600038E RID: 910 RVA: 0x000117F0 File Offset: 0x0000F9F0
		// (set) Token: 0x0600038F RID: 911 RVA: 0x000117F8 File Offset: 0x0000F9F8
		[XmlElement(Order = 33)]
		public string ApplicationVersion { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00011801 File Offset: 0x0000FA01
		[XmlIgnore]
		public bool ApplicationVersionSpecified
		{
			get
			{
				return !string.IsNullOrEmpty(this.ApplicationVersion);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00011811 File Offset: 0x0000FA11
		// (set) Token: 0x06000392 RID: 914 RVA: 0x00011819 File Offset: 0x0000FA19
		[XmlElement(Order = 34)]
		public double OrderArea { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00011822 File Offset: 0x0000FA22
		[XmlIgnore]
		public bool OrderAreaSpecified
		{
			get
			{
				return this.OrderArea > ItDocument.epsilon;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00011831 File Offset: 0x0000FA31
		[XmlIgnore]
		public bool productListSpecified
		{
			get
			{
				return this.productList.Count > 0;
			}
		}

		// Token: 0x0400011A RID: 282
		[XmlElement("Product", Order = 35)]
		public List<ItProduct> productList = new List<ItProduct>();
	}
}
