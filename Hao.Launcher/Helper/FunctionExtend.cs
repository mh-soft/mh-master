using Autodesk.RevitAddIns;
using Hao.Launcher.Model;
using IniParser;
using IniParser.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

namespace Hao.Launcher.Helper
{
	public static class FunctionExtend
	{
		private readonly static Logger _logger;

		private const string Suffix = ".image";

		private const string Small = ".Small";

		static FunctionExtend()
		{
			FunctionExtend._logger = LogManager.GetCurrentClassLogger();
		}

		public static string BuildXmlString(this BeePCProduct beePCProduct, string version = "2016")
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes");
			xmlDocument.AppendChild(xmlDeclaration);
			XmlElement xmlElement = xmlDocument.CreateElement("RevitAddIns");
			xmlDocument.AppendChild(xmlElement);
			XmlElement xmlElement1 = xmlDocument.CreateElement("AddIn");
			xmlElement.AppendChild(xmlElement1);
			XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("Type");
			xmlAttribute.Value = "Application";
			xmlElement1.Attributes.Append(xmlAttribute);
			XmlElement xmlElement2 = xmlDocument.CreateElement("Name");
			xmlElement1.AppendChild(xmlElement2);
			xmlElement2.InnerText = "BeePC";
			XmlElement xmlElement3 = xmlDocument.CreateElement("Assembly");
			xmlElement1.AppendChild(xmlElement3);
			xmlElement3.InnerText = beePCProduct.CommandDllPath.Replace("2016", version);
			XmlElement guid = xmlDocument.CreateElement("AddInId");
			xmlElement1.AppendChild(guid);
			guid.InnerText = beePCProduct.Guid;
			XmlElement xmlElement4 = xmlDocument.CreateElement("FullClassName");
			xmlElement1.AppendChild(xmlElement4);
			xmlElement4.InnerText = "BeePC_Command.BeePCApplication";
			XmlElement xmlElement5 = xmlDocument.CreateElement("VendorId");
			xmlElement1.AppendChild(xmlElement5);
			xmlElement5.InnerText = "嗡嗡科技有限公司";
			StringWriter stringWriter = new StringWriter();
			xmlDocument.WriteTo(new XmlTextWriter(stringWriter));
			return stringWriter.ToString();
		}

		public static bool CheckLocalImages(this ImageLinkData data)
		{
			bool flag = true;
			if (data == null)
			{
				flag = false;
			}
			else if (data.Version > 0)
			{
				foreach (NavbarsItem navbar in data.Navbars)
				{
					if (flag)
					{
						string fullFolder = ConstData.FullFolder;
						char directorySeparatorChar = Path.DirectorySeparatorChar;
						string str = string.Concat(fullFolder, directorySeparatorChar.ToString(), navbar.NavbarName);
						foreach (ImagesItem image in navbar.Images)
						{
							directorySeparatorChar = Path.DirectorySeparatorChar;
							string str1 = string.Concat(str, directorySeparatorChar.ToString(), Path.GetFileName(image.BigImage), ".image");
							directorySeparatorChar = Path.DirectorySeparatorChar;
							string str2 = string.Concat(str, directorySeparatorChar.ToString(), Path.GetFileName(image.BigImage), ".Small.image");
							flag = (!File.Exists(str1) ? false : File.Exists(str2));
							if (!flag)
							{
								break;
							}
						}
					}
					else
					{
						break;
					}
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public static void ClearBeePCProduct(this IEnumerable<RevitProduct> revitProducts)
		{
			foreach (RevitProduct revitProduct in revitProducts)
			{
				try
				{
					string str = string.Concat(Directory.GetParent(revitProduct.AllUsersAddInFolder).FullName, "\\", revitProduct.IntVersion());
					string str1 = string.Concat(str, "\\BeePC.addin");
					if (File.Exists(str1))
					{
						File.Delete(str1);
					}
				}
				catch (Exception exception)
				{
					FunctionExtend._logger.Error<Exception>(exception);
				}
			}
		}

		public static bool ConvertServer2Local(this ImageLinkData serverData)
		{
			bool flag;
			try
			{
				serverData.Navbars.ForEach((NavbarsItem navbar) =>
				{
					string str4 = string.Concat(ConstData.FullFolder, Path.DirectorySeparatorChar.ToString(), navbar.NavbarName);
					if (!Directory.Exists(str4))
					{
						Directory.CreateDirectory(str4);
					}
					navbar.Images.ForEach((ImagesItem dataItem) =>
					{
						string str = str4;
						char directorySeparatorChar = Path.DirectorySeparatorChar;
						string str1 = string.Concat(str, directorySeparatorChar.ToString(), Path.GetFileName(dataItem.BigImage), ".image");
						string str2 = str4;
						directorySeparatorChar = Path.DirectorySeparatorChar;
						string str3 = string.Concat(str2, directorySeparatorChar.ToString(), Path.GetFileName(dataItem.BigImage), ".Small.image");
						dataItem.BigImage = str1;
						dataItem.SmallImage = str3;
					});
				});
				if (serverData.FestivalImg != null)
				{
					string str5 = string.Concat(new object[] { ConstData.FullFolder, Path.DirectorySeparatorChar.ToString(), serverData.Version, Path.DirectorySeparatorChar.ToString(), "FestivalImg" });
					char chr = Path.DirectorySeparatorChar;
					string str6 = string.Concat(str5, chr.ToString(), serverData.FestivalImg.FestivalName, ".image");
					if (File.Exists(str6))
					{
						serverData.FestivalImg.ImgUrl = str6;
					}
				}
				flag = true;
				return flag;
			}
			catch (Exception exception)
			{
				FunctionExtend._logger.Error<Exception>(exception);
			}
			flag = false;
			return flag;
		}

		internal static void DeleteImages(this ImageLinkData data)
		{
			try
			{
				int version = data.Version;
				List<string> list = Directory.GetDirectories(ConstData.FullFolder).ToList<string>();
				foreach (string str in list)
				{
					string fullFolder = ConstData.FullFolder;
					char directorySeparatorChar = Path.DirectorySeparatorChar;
					string str1 = string.Concat(fullFolder, directorySeparatorChar.ToString(), version - list.IndexOf(str) - 1);
					if (Directory.Exists(str1))
					{
						Directory.Delete(str1, true);
					}
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
		}

		public static void DownLoadImages(this ImageLinkData serverData)
		{
			serverData.Navbars.ForEach((NavbarsItem navbar) =>
			{
				string str4 = string.Concat(ConstData.FullFolder, Path.DirectorySeparatorChar.ToString(), navbar.NavbarName);
				if (!Directory.Exists(str4))
				{
					Directory.CreateDirectory(str4);
				}
				navbar.Images.ForEach((ImagesItem dataItem) =>
				{
					string str = str4;
					char directorySeparatorChar = Path.DirectorySeparatorChar;
					string str1 = string.Concat(str, directorySeparatorChar.ToString(), Path.GetFileName(dataItem.BigImage), ".image");
					string str2 = str4;
					directorySeparatorChar = Path.DirectorySeparatorChar;
					string str3 = string.Concat(str2, directorySeparatorChar.ToString(), Path.GetFileName(dataItem.BigImage), ".Small.image");
					string bigImage = dataItem.BigImage;
					string smallImage = dataItem.SmallImage;
					if (!File.Exists(str1))
					{
						FunctionExtend.SaveImage2Local(bigImage, str1, null);
					}
					if (!File.Exists(str3))
					{
						FunctionExtend.SaveImage2Local(smallImage, str3, null);
					}
				});
			});
			if (serverData.FestivalImg != null)
			{
				string str5 = string.Concat(new object[] { ConstData.FullFolder, Path.DirectorySeparatorChar.ToString(), serverData.Version, Path.DirectorySeparatorChar.ToString(), "FestivalImg" });
				char chr = Path.DirectorySeparatorChar;
				string str6 = string.Concat(str5, chr.ToString(), serverData.FestivalImg.FestivalName, ".image");
				if (!Directory.Exists(str5))
				{
					Directory.CreateDirectory(str5);
				}
				FunctionExtend.SaveImage2Local(serverData.FestivalImg.ImgUrl, str6, "gif");
			}
		}

		public static void InitBeePCProduct(this string path, Action<BeePCProduct, Exception> callback)
		{
			string str = string.Concat(new object[] { path, "\\Main\\", 2016, "\\BeePC_Qiang_UI.dll" });
			string str1 = string.Concat(new object[] { path, "\\Main\\", 2016, "\\BeePC_Command.dll" });
			string empty = string.Empty;
			if ((!File.Exists(str) ? true : !File.Exists(str1)))
			{
				callback(null, new Exception(string.Concat("DLL未发现！\r\n", str, "\r\n", str1)));
			}
			else
			{
				try
				{
					BeePCProduct beePCProduct = new BeePCProduct();
					if (File.Exists(str))
					{
						empty = FileVersionInfo.GetVersionInfo(str).FileVersion;
					}
					string str2 = "24F33F14-1AAE-4E54-8D85-64C7109216E9";
					beePCProduct.InstallPath = path;
					beePCProduct.Version = empty;
					beePCProduct.Guid = str2;
					beePCProduct.CommandDllPath = str1;
					callback(beePCProduct, null);
				}
				catch (Exception exception)
				{
					callback(null, exception);
				}
			}
		}

		public static string IntVersion(this RevitProduct revitProduct)
		{
			string empty = string.Empty;
			if (revitProduct != null)
			{
				empty = revitProduct.Name;
				empty = empty.Substring(empty.Length - 4, 4);
			}
			return empty;
		}

		public static List<HistoryFile> ReadRecentFiles(this RevitProduct revitProduct)
		{
			List<HistoryFile> historyFiles = new List<HistoryFile>();
			string str = string.Concat(Path.GetDirectoryName(Path.GetDirectoryName(revitProduct.CurrentUserAddInFolder)), "\\Autodesk ", revitProduct.Name, "\\Revit.ini");
			IniData iniDatum = (new FileIniDataParser()).ReadFile(str);
			bool flag = true;
			for (int i = 1; i < 5; i++)
			{
				if (flag)
				{
					string empty = string.Empty;
					object[] objArray = new object[] { "Recent File List", null, null, null };
					objArray[1] = iniDatum.SectionKeySeparator.ToString();
					objArray[2] = "File";
					objArray[3] = i;
					if ((!iniDatum.TryGetKey(string.Concat(objArray), out empty) ? false : !string.IsNullOrWhiteSpace(empty)))
					{
						historyFiles.Add(new HistoryFile()
						{
							FilePath = empty,
							FileName = Path.GetFileName(empty)
						});
					}
				}
			}
			return historyFiles;
		}

		private static void SaveImage2Local(string imageUrl, string localPath, string imageFormat = null)
		{
			if (!HttpRequestHelper.HttpDownload(imageUrl, localPath))
			{
				File.Delete(localPath);
			}
		}

		public static bool SetIsNew(this ImageLinkData data, ImageLinkData newData)
		{
			newData.Navbars.ForEach((NavbarsItem item) =>
			{
				NavbarsItem navbarsItem = data.Navbars.FirstOrDefault<NavbarsItem>((NavbarsItem q) => q.NavbarName == item.NavbarName);
				if (navbarsItem != null)
				{
					FunctionExtend.SetIsNew(item, navbarsItem);
				}
			});
			return true;
		}

		private static void SetIsNew(NavbarsItem newNavItem, NavbarsItem navItem)
		{
			newNavItem.Images.ForEach((ImagesItem item) =>
			{
				ImagesItem imagesItem = navItem.Images.FirstOrDefault<ImagesItem>((ImagesItem q) => q.BigImage == item.BigImage);
				if (imagesItem != null)
				{
					item.IsNew = imagesItem.IsNew;
				}
			});
		}
	}
}