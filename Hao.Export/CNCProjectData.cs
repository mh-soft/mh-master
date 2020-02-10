using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Hao.Export.Config;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x02000009 RID: 9
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
	public class CNCProjectData
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000034B6 File Offset: 0x000016B6
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000034BE File Offset: 0x000016BE
		public string CommentPXML { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000034C7 File Offset: 0x000016C7
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000034CF File Offset: 0x000016CF
		public string DocumentGlobalId { get; set; }

		// Token: 0x06000025 RID: 37 RVA: 0x000034D8 File Offset: 0x000016D8
		public bool CheckNoNullFields()
		{
			return this.General.CheckNoNullFields() && this.ProjectDescription.CheckNoNullFields() && this.BuildingSite.CheckNoNullFields() && this.BuildingOwner.CheckNoNullFields() && this.DrawingData.CheckNoNullFields() && this.GenericOrderInfo.CheckNoNullFields() && this.CommentPXML.isNotNull() && this.DocumentGlobalId.isNotNull();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003554 File Offset: 0x00001754
		public static CNCProjectData Create(Document doc, ItCfgNode node)
		{
			CNCProjectData.GeneralDataClass generalData = CNCProjectData.GetGeneralData(node);
			CNCProjectData.ProjectDescriptionClass projectDescription = CNCProjectData.GetProjectDescription(node["ProjectDescription"]);
			CNCProjectData.Address address = CNCProjectData.GetAddress(node["BuildingSite"]);
			CNCProjectData.Address address2 = CNCProjectData.GetAddress(node["BuildingOwner"]);
			CNCProjectData.DrawingDataClass drawingData = CNCProjectData.GetDrawingData(node);
			CNCProjectData.GenericOrderInfoClass genericOrderInfo = CNCProjectData.GetGenericOrderInfo(node);
			ItCfgNode itCfgNode = node["Comment"];
			string commentPxml = ((itCfgNode != null) ? itCfgNode.value : null) ?? string.Empty;
			string docId = CNCProjectData.GetDocId(doc);
			return new CNCProjectData(generalData, projectDescription, address, address2, drawingData, genericOrderInfo, commentPxml, docId);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000035F4 File Offset: 0x000017F4
		private CNCProjectData(CNCProjectData.GeneralDataClass generalData, CNCProjectData.ProjectDescriptionClass projectDescription, CNCProjectData.Address site, CNCProjectData.Address owner, CNCProjectData.DrawingDataClass drawingData, CNCProjectData.GenericOrderInfoClass genericOrderInfo, string commentPxml, string documentGlobalId)
		{
			this.General = generalData;
			this.ProjectDescription = projectDescription;
			this.BuildingSite = site;
			this.BuildingOwner = owner;
			this.DrawingData = drawingData;
			this.GenericOrderInfo = genericOrderInfo;
			this.CommentPXML = commentPxml;
			this.DocumentGlobalId = documentGlobalId;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003648 File Offset: 0x00001848
		private static CNCProjectData.GenericOrderInfoClass GetGenericOrderInfo(ItCfgNode node)
		{
			ItCfgNode itCfgNode = node["GenericOrderInfo13"];
			string line = ((itCfgNode != null) ? itCfgNode.value : null) ?? string.Empty;
			ItCfgNode itCfgNode2 = node["GenericOrderInfo14"];
			string line2 = ((itCfgNode2 != null) ? itCfgNode2.value : null) ?? string.Empty;
			ItCfgNode itCfgNode3 = node["GenericOrderInfo15"];
			string line3 = ((itCfgNode3 != null) ? itCfgNode3.value : null) ?? string.Empty;
			ItCfgNode itCfgNode4 = node["GenericOrderInfo16"];
			string line4 = ((itCfgNode4 != null) ? itCfgNode4.value : null) ?? string.Empty;
			ItCfgNode itCfgNode5 = node["GenericOrderInfo17"];
			string line5 = ((itCfgNode5 != null) ? itCfgNode5.value : null) ?? string.Empty;
			ItCfgNode itCfgNode6 = node["GenericOrderInfo18"];
			string line6 = ((itCfgNode6 != null) ? itCfgNode6.value : null) ?? string.Empty;
			ItCfgNode itCfgNode7 = node["GenericOrderInfo19"];
			string line7 = ((itCfgNode7 != null) ? itCfgNode7.value : null) ?? string.Empty;
			ItCfgNode itCfgNode8 = node["GenericOrderInfo20"];
			string line8 = ((itCfgNode8 != null) ? itCfgNode8.value : null) ?? string.Empty;
			return new CNCProjectData.GenericOrderInfoClass(line, line2, line3, line4, line5, line6, line7, line8);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003780 File Offset: 0x00001980
		private static CNCProjectData.DrawingDataClass GetDrawingData(ItCfgNode node)
		{
			ItCfgNode itCfgNode = node["DrawingDate"];
			string text = ((itCfgNode != null) ? itCfgNode.value : null) ?? string.Empty;
			bool flag = string.IsNullOrEmpty(text);
			if (flag)
			{
				text = DateTime.Now.ToString("dd.MM.yyyy");
			}
			DateTime date = DateTime.ParseExact(text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
			ItCfgNode itCfgNode2 = node["DrawingRevision"];
			string revision = ((itCfgNode2 != null) ? itCfgNode2.value : null) ?? string.Empty;
			ItCfgNode itCfgNode3 = node["DrawingAuthor"];
			string author = ((itCfgNode3 != null) ? itCfgNode3.value : null) ?? string.Empty;
			return new CNCProjectData.DrawingDataClass(date, revision, author);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003834 File Offset: 0x00001A34
		private static CNCProjectData.GeneralDataClass GetGeneralData(ItCfgNode node)
		{
			ItCfgNode itCfgNode = node["OrderNumber"];
			string orderNumber = ((itCfgNode != null) ? itCfgNode.value : null) ?? string.Empty;
			ItCfgNode itCfgNode2 = node["Component"];
			string component = ((itCfgNode2 != null) ? itCfgNode2.value : null) ?? string.Empty;
			ItCfgNode itCfgNode3 = node["Storey"];
			string storey = ((itCfgNode3 != null) ? itCfgNode3.value : null) ?? string.Empty;
			ItCfgNode itCfgNode4 = node["DrawingNumber"];
			string drawingNumber = ((itCfgNode4 != null) ? itCfgNode4.value : null) ?? string.Empty;
			return new CNCProjectData.GeneralDataClass(orderNumber, component, storey, drawingNumber);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000038DC File Offset: 0x00001ADC
		private static CNCProjectData.ProjectDescriptionClass GetProjectDescription(ItCfgNode descriptionNode)
		{
			return new CNCProjectData.ProjectDescriptionClass
			{
				Line1 = (descriptionNode["Line1"].value ?? string.Empty),
				Line2 = (descriptionNode["Line2"].value ?? string.Empty),
				Line3 = (descriptionNode["Line3"].value ?? string.Empty),
				Line4 = (descriptionNode["Line4"].value ?? string.Empty)
			};
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003974 File Offset: 0x00001B74
		private static CNCProjectData.Address GetAddress(ItCfgNode node)
		{
			ItCfgNode itCfgNode = node["Name"];
			string name = ((itCfgNode != null) ? itCfgNode.value : null) ?? string.Empty;
			ItCfgNode itCfgNode2 = node["Street"];
			string street = ((itCfgNode2 != null) ? itCfgNode2.value : null) ?? string.Empty;
			ItCfgNode itCfgNode3 = node["PostCode"];
			string zipCode = ((itCfgNode3 != null) ? itCfgNode3.value : null) ?? string.Empty;
			ItCfgNode itCfgNode4 = node["Place"];
			string place = ((itCfgNode4 != null) ? itCfgNode4.value : null) ?? string.Empty;
			return new CNCProjectData.Address(name, street, zipCode, place);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003A1C File Offset: 0x00001C1C
		private static string GetDocId(Document doc)
		{
			return doc.ProjectInformation.UniqueId;
		}

		// Token: 0x04000018 RID: 24
		private const string SketchDateFormat = "dd.MM.yyyy";

		// Token: 0x04000019 RID: 25
		public CNCProjectData.GeneralDataClass General;

		// Token: 0x0400001A RID: 26
		public CNCProjectData.ProjectDescriptionClass ProjectDescription;

		// Token: 0x0400001B RID: 27
		public CNCProjectData.Address BuildingSite;

		// Token: 0x0400001C RID: 28
		public CNCProjectData.Address BuildingOwner;

		// Token: 0x0400001D RID: 29
		public CNCProjectData.DrawingDataClass DrawingData;

		// Token: 0x0400001E RID: 30
		public CNCProjectData.GenericOrderInfoClass GenericOrderInfo;

		// Token: 0x0200004A RID: 74
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
		public class GeneralDataClass
		{
			// Token: 0x0600054D RID: 1357 RVA: 0x00015349 File Offset: 0x00013549
			public GeneralDataClass(string orderNumber, string component, string storey, string drawingNumber)
			{
				this.OrderNumber = orderNumber;
				this.Component = component;
				this.Storey = storey;
				this.DrawingNumber = drawingNumber;
			}

			// Token: 0x0600054E RID: 1358 RVA: 0x00015370 File Offset: 0x00013570
			public bool CheckNoNullFields()
			{
				return this.OrderNumber.isNotNull() && this.Component.isNotNull() && this.Storey.isNotNull() && this.DrawingNumber.isNotNull();
			}

			// Token: 0x040001DA RID: 474
			public string OrderNumber;

			// Token: 0x040001DB RID: 475
			public string Component;

			// Token: 0x040001DC RID: 476
			public string Storey;

			// Token: 0x040001DD RID: 477
			public string DrawingNumber;
		}

		// Token: 0x0200004B RID: 75
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
		public class Address
		{
			// Token: 0x0600054F RID: 1359 RVA: 0x000153B7 File Offset: 0x000135B7
			public Address(string name, string street, string zipCode, string place)
			{
				this.Name = name;
				this.Street = street;
				this.ZipCode = zipCode;
				this.Place = place;
			}

			// Token: 0x06000550 RID: 1360 RVA: 0x000153E0 File Offset: 0x000135E0
			public bool CheckNoNullFields()
			{
				return this.Name.isNotNull() && this.Street.isNotNull() && this.ZipCode.isNotNull() && this.Place.isNotNull();
			}

			// Token: 0x040001DE RID: 478
			public string Name;

			// Token: 0x040001DF RID: 479
			public string Street;

			// Token: 0x040001E0 RID: 480
			public string ZipCode;

			// Token: 0x040001E1 RID: 481
			public string Place;
		}

		// Token: 0x0200004C RID: 76
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
		public class ProjectDescriptionClass
		{
			// Token: 0x06000551 RID: 1361 RVA: 0x00015427 File Offset: 0x00013627
			public ProjectDescriptionClass()
			{
				this.Line1 = string.Empty;
				this.Line2 = string.Empty;
				this.Line3 = string.Empty;
				this.Line4 = string.Empty;
			}

			// Token: 0x06000552 RID: 1362 RVA: 0x00015460 File Offset: 0x00013660
			public bool CheckNoNullFields()
			{
				return this.Line1.isNotNull() && this.Line2.isNotNull() && this.Line3.isNotNull() && this.Line4.isNotNull();
			}

			// Token: 0x040001E2 RID: 482
			public string Line1;

			// Token: 0x040001E3 RID: 483
			public string Line2;

			// Token: 0x040001E4 RID: 484
			public string Line3;

			// Token: 0x040001E5 RID: 485
			public string Line4;
		}

		// Token: 0x0200004D RID: 77
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
		public class DrawingDataClass
		{
			// Token: 0x06000553 RID: 1363 RVA: 0x000154A8 File Offset: 0x000136A8
			public DrawingDataClass(DateTime date, string revision, string author)
			{
				bool camexportUseZeroDate = DbgModesContainer.Instance().CAMExportUseZeroDate;
				if (camexportUseZeroDate)
				{
					this.Date = "00.00.0000";
				}
				else
				{
					this.Date = date.ToString("dd.MM.yyyy");
				}
				this.Revision = revision;
				this.Author = author;
			}

			// Token: 0x06000554 RID: 1364 RVA: 0x000154F8 File Offset: 0x000136F8
			public bool CheckNoNullFields()
			{
				return this.Date.isNotNull() && this.Revision.isNotNull() && this.Author.isNotNull();
			}

			// Token: 0x040001E6 RID: 486
			public string Date;

			// Token: 0x040001E7 RID: 487
			public string Revision;

			// Token: 0x040001E8 RID: 488
			public string Author;
		}

		// Token: 0x0200004E RID: 78
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
		public class GenericOrderInfoClass
		{
			// Token: 0x06000555 RID: 1365 RVA: 0x00015534 File Offset: 0x00013734
			public GenericOrderInfoClass(string line13, string line14, string line15, string line16, string line17, string line18, string line19, string line20)
			{
				this.Line13 = (line13 ?? string.Empty);
				this.Line14 = (line14 ?? string.Empty);
				this.Line15 = (line15 ?? string.Empty);
				this.Line16 = (line16 ?? string.Empty);
				this.Line17 = (line17 ?? string.Empty);
				this.Line18 = (line18 ?? string.Empty);
				this.Line19 = (line19 ?? string.Empty);
				this.Line20 = (line20 ?? string.Empty);
			}

			// Token: 0x06000556 RID: 1366 RVA: 0x000155D0 File Offset: 0x000137D0
			public bool CheckNoNullFields()
			{
				return this.Line13.isNotNull() && this.Line14.isNotNull() && this.Line15.isNotNull() && this.Line16.isNotNull() && this.Line17.isNotNull() && this.Line18.isNotNull() && this.Line19.isNotNull() && this.Line20.isNotNull();
			}

			// Token: 0x040001E9 RID: 489
			public string Line13;

			// Token: 0x040001EA RID: 490
			public string Line14;

			// Token: 0x040001EB RID: 491
			public string Line15;

			// Token: 0x040001EC RID: 492
			public string Line16;

			// Token: 0x040001ED RID: 493
			public string Line17;

			// Token: 0x040001EE RID: 494
			public string Line18;

			// Token: 0x040001EF RID: 495
			public string Line19;

			// Token: 0x040001F0 RID: 496
			public string Line20;
		}
	}
}
