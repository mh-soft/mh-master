using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Hao.Export.Geometry;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x02000030 RID: 48
	[XmlRoot(Namespace = "http://progress-m.com/ProgressXML/Version1", ElementName = "PXML_Document")]
	public class ItDocument
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0001087E File Offset: 0x0000EA7E
		[XmlIgnore]
		public bool OrdersSpecified
		{
			get
			{
				return this.Orders.Count > 0;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0001088E File Offset: 0x0000EA8E
		[XmlIgnore]
		public bool FeedbacksSpecified
		{
			get
			{
				return this.Feedbacks.Count > 0;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0001089E File Offset: 0x0000EA9E
		// (set) Token: 0x060002BB RID: 699 RVA: 0x000108A6 File Offset: 0x0000EAA6
		[XmlIgnore]
		public string FilePath { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002BC RID: 700 RVA: 0x000108AF File Offset: 0x0000EAAF
		// (set) Token: 0x060002BD RID: 701 RVA: 0x000108B7 File Offset: 0x0000EAB7
		[XmlIgnore]
		public string FileName { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002BE RID: 702 RVA: 0x000108C0 File Offset: 0x0000EAC0
		// (set) Token: 0x060002BF RID: 703 RVA: 0x000108C8 File Offset: 0x0000EAC8
		[XmlIgnore]
		public ProductType productType { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x000108D1 File Offset: 0x0000EAD1
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x000108D9 File Offset: 0x0000EAD9
		[XmlIgnore]
		public bool IsValid { get; set; } = true;

		// Token: 0x060002C2 RID: 706 RVA: 0x000108E4 File Offset: 0x0000EAE4
		public bool prepare(CNCProjectData projData)
		{
			bool flag = this.DocInfo == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.DocInfo.GlobalId = projData.DocumentGlobalId;
				result = true;
			}
			return result;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0001091A File Offset: 0x0000EB1A
		[XmlNamespaceDeclarations]
		public XmlSerializerNamespaces Namespaces
		{
			get
			{
				return ItDocument.StaticNamespaces;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00010921 File Offset: 0x0000EB21
		public static XmlSerializerNamespaces StaticNamespaces { get; } = new XmlSerializerNamespaces(new XmlQualifiedName[]
		{
			new XmlQualifiedName(string.Empty, "http://progress-m.com/ProgressXML/Version1")
		});

		// Token: 0x060002C5 RID: 709 RVA: 0x00010928 File Offset: 0x0000EB28
		public void convertUnits()
		{
			foreach (ItOrder itOrder in this.Orders)
			{
				bool orderAreaSpecified = itOrder.OrderAreaSpecified;
				if (orderAreaSpecified)
				{
					itOrder.OrderArea = ItDocument.convertToSquareMeter(itOrder.OrderArea);
				}
				foreach (ItProduct itProduct in itOrder.productList)
				{
					itProduct.ConvertUnits();
				}
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000109E0 File Offset: 0x0000EBE0
		public static double convertToSquareMeter(double d)
		{
			return Math.Round(d.FeetToMeter().FeetToMeter() * 1000000.0) / 1000000.0;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00010A18 File Offset: 0x0000EC18
		public static double convertToWeightPerCubeMeter(double d)
		{
			return Math.Round(d / ItConstants.CubicFeetToLiter * 1000000.0) / 1000000.0;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00010A4C File Offset: 0x0000EC4C
		public static double convertToMM(double d)
		{
			return Math.Round(d.FeetToMeter() * 1000.0 * 1000000.0) / 1000000.0;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00010A88 File Offset: 0x0000EC88
		public static ItGePoint3d convertToMM(ItGePoint3d point)
		{
			double x = Math.Round(point.x.FeetToMeter() * 1000.0 * 1000000.0) / 1000000.0;
			double y = Math.Round(point.y.FeetToMeter() * 1000.0 * 1000000.0) / 1000000.0;
			double z = Math.Round(point.z.FeetToMeter() * 1000.0 * 1000000.0) / 1000000.0;
			return new ItGePoint3d(x, y, z, null);
		}

		// Token: 0x040000C5 RID: 197
		public const string m_defaultNamespace = "http://progress-m.com/ProgressXML/Version1";

		// Token: 0x040000C6 RID: 198
		[XmlElement("DocInfo", Order = 0)]
		public ItDocument.ItDocInfo DocInfo = new ItDocument.ItDocInfo();

		// Token: 0x040000C7 RID: 199
		[XmlElement("Order", Order = 1)]
		public List<ItOrder> Orders = new List<ItOrder>();

		// Token: 0x040000C8 RID: 200
		[XmlElement("Feedback", Order = 2)]
		public List<ItFeedback> Feedbacks = new List<ItFeedback>();

		// Token: 0x040000CE RID: 206
		public static readonly double epsilon = 1E-08;

		// Token: 0x0200007F RID: 127
		public class ItDocInfo
		{
			// Token: 0x1700024D RID: 589
			// (get) Token: 0x06000615 RID: 1557 RVA: 0x0001600D File Offset: 0x0001420D
			// (set) Token: 0x06000616 RID: 1558 RVA: 0x00016015 File Offset: 0x00014215
			[XmlAttribute]
			public string GlobalId { get; set; }

			// Token: 0x1700024E RID: 590
			// (get) Token: 0x06000617 RID: 1559 RVA: 0x0001601E File Offset: 0x0001421E
			[XmlIgnore]
			public bool GlobalIdSpecified
			{
				get
				{
					return !string.IsNullOrEmpty(this.Comment);
				}
			}

			// Token: 0x1700024F RID: 591
			// (get) Token: 0x06000618 RID: 1560 RVA: 0x0001602E File Offset: 0x0001422E
			// (set) Token: 0x06000619 RID: 1561 RVA: 0x00016036 File Offset: 0x00014236
			[XmlElement(Order = 0)]
			public int MajorVersion { get; set; }

			// Token: 0x17000250 RID: 592
			// (get) Token: 0x0600061A RID: 1562 RVA: 0x0001603F File Offset: 0x0001423F
			// (set) Token: 0x0600061B RID: 1563 RVA: 0x00016047 File Offset: 0x00014247
			[XmlElement(Order = 1)]
			public int MinorVersion { get; set; }

			// Token: 0x17000251 RID: 593
			// (get) Token: 0x0600061C RID: 1564 RVA: 0x00016050 File Offset: 0x00014250
			// (set) Token: 0x0600061D RID: 1565 RVA: 0x00016058 File Offset: 0x00014258
			[XmlElement(Order = 2)]
			public string Comment { get; set; }

			// Token: 0x17000252 RID: 594
			// (get) Token: 0x0600061E RID: 1566 RVA: 0x0001601E File Offset: 0x0001421E
			[XmlIgnore]
			public bool CommentSpecified
			{
				get
				{
					return !string.IsNullOrEmpty(this.Comment);
				}
			}

			// Token: 0x17000253 RID: 595
			// (get) Token: 0x0600061F RID: 1567 RVA: 0x00016061 File Offset: 0x00014261
			// (set) Token: 0x06000620 RID: 1568 RVA: 0x00016069 File Offset: 0x00014269
			[XmlElement(Order = 3)]
			public string ConvertConventions { get; set; }

			// Token: 0x17000254 RID: 596
			// (get) Token: 0x06000621 RID: 1569 RVA: 0x00016072 File Offset: 0x00014272
			[XmlIgnore]
			public bool ConvertConventionsSpecified
			{
				get
				{
					return !string.IsNullOrEmpty(this.ConvertConventions);
				}
			}

			// Token: 0x17000255 RID: 597
			// (get) Token: 0x06000622 RID: 1570 RVA: 0x00016082 File Offset: 0x00014282
			// (set) Token: 0x06000623 RID: 1571 RVA: 0x0001608A File Offset: 0x0001428A
			[XmlElement(Order = 4)]
			public List<ItDocument.ItDocInfo.Mode> Modes { get; set; }

			// Token: 0x17000256 RID: 598
			// (get) Token: 0x06000624 RID: 1572 RVA: 0x00016093 File Offset: 0x00014293
			public bool ModesSpecified
			{
				get
				{
					return this.Modes.any<ItDocument.ItDocInfo.Mode>();
				}
			}

			// Token: 0x06000625 RID: 1573 RVA: 0x000160A0 File Offset: 0x000142A0
			public ItDocInfo()
			{
				this.MajorVersion = 1;
				this.MinorVersion = 3;
				this.Modes = new List<ItDocument.ItDocInfo.Mode>();
			}

			// Token: 0x0200008B RID: 139
			public class Mode
			{
				// Token: 0x06000634 RID: 1588 RVA: 0x00016141 File Offset: 0x00014341
				public Mode()
				{
				}

				// Token: 0x1700025C RID: 604
				// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001614B File Offset: 0x0001434B
				// (set) Token: 0x06000636 RID: 1590 RVA: 0x00016153 File Offset: 0x00014353
				[XmlElement(Order = 0)]
				public string ID { get; set; }

				// Token: 0x1700025D RID: 605
				// (get) Token: 0x06000637 RID: 1591 RVA: 0x000056A2 File Offset: 0x000038A2
				[XmlIgnore]
				public bool IDSpecified
				{
					get
					{
						return true;
					}
				}

				// Token: 0x1700025E RID: 606
				// (get) Token: 0x06000638 RID: 1592 RVA: 0x0001615C File Offset: 0x0001435C
				// (set) Token: 0x06000639 RID: 1593 RVA: 0x00016164 File Offset: 0x00014364
				[XmlElement(Order = 1)]
				public string Val { get; set; }

				// Token: 0x1700025F RID: 607
				// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001616D File Offset: 0x0001436D
				[XmlIgnore]
				public bool ValSpecified
				{
					get
					{
						return !string.IsNullOrEmpty(this.Val);
					}
				}

				// Token: 0x0600063B RID: 1595 RVA: 0x0001617D File Offset: 0x0001437D
				public Mode(string id, string val)
				{
					this.ID = id;
					this.Val = val;
				}

				// Token: 0x040002FF RID: 767
				public const string ProdLayout = "ProdLayout";

				// Token: 0x04000300 RID: 768
				public const string RequestedCulture = "RequestedCulture";

				// Token: 0x04000301 RID: 769
				public const string EstimateProdTime = "EstimateProdTime";
			}
		}
	}
}
