using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x02000008 RID: 8
	internal static class CNCFileNameParser
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002E1C File Offset: 0x0000101C
		public static string Parse(string ruleString, AssemblyInstance assemblyInstance, CNCProjectData projectData, bool allowsSpaces = true)
		{
			bool flag = projectData.isNull();
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				string text;
				if (assemblyInstance == null)
				{
					text = null;
				}
				else
				{
					Document document = assemblyInstance.Document;
					text = ((document != null) ? document.PathName : null);
				}
				string path = text;
				string text2 = projectData.DrawingData.Date;
				DateTime dateTime;
				bool flag2 = DateTime.TryParseExact(text2, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTime);
				if (flag2)
				{
					text2 = dateTime.ToString("yyyyMMdd");
				}
				Dictionary<string, string> valuesDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
				{
					{
						CNCFilenameTokens.FileName.ToString(),
						Path.GetFileNameWithoutExtension(path)
					},
					{
						CNCFilenameTokens.Date.ToString(),
						DateTime.Now.ToString("yyyyMMdd")
					},
					{
						CNCFilenameTokens.Time.ToString(),
						DateTime.Now.ToString("hhmmss")
					},
					{
						CNCFilenameTokens.ProdNo.ToString(),
						assemblyInstance.prodNo().ToString()
					},
					{
						CNCFilenameTokens.OrderNumber.ToString(),
						projectData.General.OrderNumber
					},
					{
						CNCFilenameTokens.Component.ToString(),
						projectData.General.Component
					},
					{
						CNCFilenameTokens.Storey.ToString(),
						projectData.General.Storey
					},
					{
						CNCFilenameTokens.DrawingNumber.ToString(),
						projectData.General.DrawingNumber
					},
					{
						CNCFilenameTokens.ProjectDescriptionLine1.ToString(),
						projectData.ProjectDescription.Line1
					},
					{
						CNCFilenameTokens.ProjectDescriptionLine2.ToString(),
						projectData.ProjectDescription.Line2
					},
					{
						CNCFilenameTokens.ProjectDescriptionLine3.ToString(),
						projectData.ProjectDescription.Line3
					},
					{
						CNCFilenameTokens.ProjectDescriptionLine4.ToString(),
						projectData.ProjectDescription.Line4
					},
					{
						CNCFilenameTokens.BuildingSiteName.ToString(),
						projectData.BuildingSite.Name
					},
					{
						CNCFilenameTokens.BuildingSiteStreet.ToString(),
						projectData.BuildingSite.Street
					},
					{
						CNCFilenameTokens.BuildingSitePostCode.ToString(),
						projectData.BuildingSite.ZipCode
					},
					{
						CNCFilenameTokens.BuildingSitePlace.ToString(),
						projectData.BuildingSite.Place
					},
					{
						CNCFilenameTokens.BuildingOwnerName.ToString(),
						projectData.BuildingOwner.Name
					},
					{
						CNCFilenameTokens.BuildingOwnerStreet.ToString(),
						projectData.BuildingOwner.Street
					},
					{
						CNCFilenameTokens.BuildingOwnerPostCode.ToString(),
						projectData.BuildingOwner.ZipCode
					},
					{
						CNCFilenameTokens.BuildingOwnerPlace.ToString(),
						projectData.BuildingOwner.Place
					},
					{
						CNCFilenameTokens.DrawingDate.ToString(),
						text2
					},
					{
						CNCFilenameTokens.DrawingRevision.ToString(),
						projectData.DrawingData.Revision
					},
					{
						CNCFilenameTokens.DrawingAuthor.ToString(),
						projectData.DrawingData.Author
					},
					{
						CNCFilenameTokens.GenericOrderInfo01.ToString(),
						projectData.ProjectDescription.Line1
					},
					{
						CNCFilenameTokens.GenericOrderInfo02.ToString(),
						projectData.ProjectDescription.Line2
					},
					{
						CNCFilenameTokens.GenericOrderInfo03.ToString(),
						projectData.ProjectDescription.Line3
					},
					{
						CNCFilenameTokens.GenericOrderInfo04.ToString(),
						projectData.ProjectDescription.Line4
					},
					{
						CNCFilenameTokens.GenericOrderInfo05.ToString(),
						projectData.BuildingSite.Name
					},
					{
						CNCFilenameTokens.GenericOrderInfo06.ToString(),
						projectData.BuildingSite.Street
					},
					{
						CNCFilenameTokens.GenericOrderInfo07.ToString(),
						projectData.BuildingSite.ZipCode
					},
					{
						CNCFilenameTokens.GenericOrderInfo08.ToString(),
						projectData.BuildingSite.Place
					},
					{
						CNCFilenameTokens.GenericOrderInfo09.ToString(),
						projectData.BuildingOwner.Name
					},
					{
						CNCFilenameTokens.GenericOrderInfo10.ToString(),
						projectData.BuildingOwner.Street
					},
					{
						CNCFilenameTokens.GenericOrderInfo11.ToString(),
						projectData.BuildingOwner.ZipCode
					},
					{
						CNCFilenameTokens.GenericOrderInfo12.ToString(),
						projectData.BuildingOwner.Place
					},
					{
						CNCFilenameTokens.GenericOrderInfo13.ToString(),
						projectData.GenericOrderInfo.Line13
					},
					{
						CNCFilenameTokens.GenericOrderInfo14.ToString(),
						projectData.GenericOrderInfo.Line14
					},
					{
						CNCFilenameTokens.GenericOrderInfo15.ToString(),
						projectData.GenericOrderInfo.Line15
					},
					{
						CNCFilenameTokens.GenericOrderInfo16.ToString(),
						projectData.GenericOrderInfo.Line16
					},
					{
						CNCFilenameTokens.GenericOrderInfo17.ToString(),
						projectData.GenericOrderInfo.Line17
					},
					{
						CNCFilenameTokens.GenericOrderInfo18.ToString(),
						projectData.GenericOrderInfo.Line18
					},
					{
						CNCFilenameTokens.GenericOrderInfo19.ToString(),
						projectData.GenericOrderInfo.Line19
					},
					{
						CNCFilenameTokens.GenericOrderInfo20.ToString(),
						projectData.GenericOrderInfo.Line20
					}
				};
				string text3 = FileNameParser.Parse(ruleString, valuesDict, true);
				result = text3;
			}
			return result;
		}

		// Token: 0x04000015 RID: 21
		private const string _DateTimeFormatFromConfig = "dd.MM.yyyy";

		// Token: 0x04000016 RID: 22
		private const string _DateFormatForFileName = "yyyyMMdd";

		// Token: 0x04000017 RID: 23
		private const string _TimeFormatForFileName = "hhmmss";
	}
}
