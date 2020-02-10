using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Hao.Export.Config;
using Hao.Export.MachineData.Events;
using Hao.Export.MachineData.PXML;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{


    /// <summary>
    /// 用于创建PXML文档
    /// </summary>
	public class ItCreatePXML
	{
		/// <summary>
        /// 获取当前的版本信息
        /// </summary>
        /// <returns></returns>
		private static string GetFileVersion()
		{
			bool camexportUseZeroDate = DbgModesContainer.Instance().CAMExportUseZeroDate;
			string result;
			if (camexportUseZeroDate)
			{
				result = "19.0.0.0";
			}
			else
			{
				Assembly assembly = typeof(ItCreatePXML).Assembly;
				try
				{
					string codeBase = assembly.CodeBase;
					Uri uri = new Uri(codeBase);
					string localPath = uri.LocalPath;
					FileInfo fileInfo = new FileInfo(localPath);
					bool flag = !fileInfo.Exists;
					if (flag)
					{
						result = assembly.GetName().Version.ToString();
					}
					else
					{
						FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(localPath);
						result = versionInfo.ProductVersion;
					}
				}
				catch
				{
					result = assembly.GetName().Version.ToString();
				}
			}
			return result;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00004310 File Offset: 0x00002510
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00004318 File Offset: 0x00002518
		public bool UserCanceled { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00004321 File Offset: 0x00002521
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00004329 File Offset: 0x00002529
		public List<string> CreatedFileNames { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00004332 File Offset: 0x00002532
		// (set) Token: 0x06000052 RID: 82 RVA: 0x0000433A File Offset: 0x0000253A
		public int Successes { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00004343 File Offset: 0x00002543
		// (set) Token: 0x06000054 RID: 84 RVA: 0x0000434B File Offset: 0x0000254B
		public int Failures { get; private set; }

        /// <summary>
        /// 数据的导出类
        /// </summary>
        /// <param name="assemblies"></param>
        /// <param name="options"></param>
		public ItCreatePXML(IEnumerable<AssemblyInstance> assemblies, ICamExportIntOptions options)
		{
			this._assemblies = assemblies.ToList<AssemblyInstance>();
			this._options = options;
			AssemblyInstance assemblyInstance = this._assemblies.FirstOrDefault<AssemblyInstance>();
			this._rvtDoc = ((assemblyInstance != null) ? assemblyInstance.Document : null);
		}

		
        /// <summary>
        /// 执行导出操作
        /// </summary>
        /// <param name="projectData"></param>
		public void run(CNCProjectData projectData)
		{

			XmlRootAttribute root = new XmlRootAttribute("PXML_Document")
			{
				Namespace = "http://progress-m.com/ProgressXML/Version1"
			};

			XmlSerializer serializer = new XmlSerializer(typeof(ItDocument), root);
			List<ItDocument> list = new List<ItDocument>();
			ItDocument itDocument = null;
			this._options.TargetDirectory.Create();

            //读取所有的部件
			foreach (AssemblyInstance assemblyInstance in this._assemblies)
			{
				ProductType productType = assemblyInstance.productType();
           

                bool flag = productType == ProductType.None;
				if (flag)
				{
					ItFailures.PostFailure(ItFailures.CAMManualCreatedAssemblyNotSupported, assemblyInstance.Id);
				}
				else
				{
					bool flag2 = this.IsAlreadyWritten(assemblyInstance, list);
					if (!flag2)
					{
						string fileName = ItCreatePXML.GetFileName(assemblyInstance, projectData, this._options, itDocument);
						bool flag3;
						CamIntEvents.OnExportingAssembly(this._rvtDoc, assemblyInstance.Id, this._options, productType, fileName, out flag3);
						bool flag4 = flag3;
						if (flag4)
						{
							CamIntEvents.OnExportedAssembly(this._rvtDoc, assemblyInstance.Id, this._options, productType, fileName, CamIntExportStatus.EventCanceled);
						}
						else
						{
							bool flag5 = !this._options.MultipleElementsInOneFile || itDocument == null;
							if (flag5)
							{
								bool userCanceled;
								itDocument = ItCreatePXML.GetDocument(projectData, assemblyInstance, this._options, fileName, out userCanceled);
								this.UserCanceled = userCanceled;
								bool userCanceled2 = this.UserCanceled;
								if (userCanceled2)
								{
									CamIntEvents.OnExportedAssembly(this._rvtDoc, assemblyInstance.Id, this._options, productType, fileName, CamIntExportStatus.UserCanceled);
									return;
								}
								bool flag6 = itDocument == null;
								if (flag6)
								{
									CamIntEvents.OnExportedAssembly(this._rvtDoc, assemblyInstance.Id, this._options, productType, fileName, CamIntExportStatus.Skipped);
									bool multipleElementsInOneFile = this._options.MultipleElementsInOneFile;
									if (multipleElementsInOneFile)
									{
										return;
									}
									continue;
								}
								else
								{
									list.Add(itDocument);
									ItOrder item = ItCreatePXML.CreateOrderInstance(this._rvtDoc, projectData);
									List<ItOrder> orders = itDocument.Orders;
									if (orders != null)
									{
										orders.Add(item);
									}
								}
							}
							this.AddProduct(itDocument);
							ItMachineDataBase cncdocCreator = ItCNCDataFactory.getCNCDocCreator(assemblyInstance, this._options);
							RevitElement<Part> partOfShellType = assemblyInstance.getPartOfShellType(ShellType.FirstShell);
							itDocument.IsValid = cncdocCreator.CreateMachineDataPxml(assemblyInstance, partOfShellType, itDocument, projectData);
							CamIntExportStatus status = CamIntExportStatus.None;
							bool flag7 = !this._options.MultipleElementsInOneFile;
							if (flag7)
							{
								string text;
								bool flag8 = this.WriteFile(itDocument, serializer, out text);
								bool flag9 = flag8;
								if (flag9)
								{
									int num = this.Successes;
									this.Successes = num + 1;
									status = CamIntExportStatus.Success;
								}
								else
								{
									int num = this.Failures;
									this.Failures = num + 1;
									status = CamIntExportStatus.Failed;
								}
							}
							CamIntEvents.OnExportedAssembly(this._rvtDoc, assemblyInstance.Id, this._options, productType, fileName, status);
						}
					}
				}
			}
			bool flag10 = this._options.MultipleElementsInOneFile && itDocument != null;
			if (flag10)
			{
				string fileName2;
				bool flag11 = this.WriteFile(itDocument, serializer, out fileName2);
				bool flag12 = flag11;
				CamIntExportStatus status2;
				if (flag12)
				{
					int num = this.Successes;
					this.Successes = num + 1;
					status2 = CamIntExportStatus.Success;
				}
				else
				{
					int num = this.Failures;
					this.Failures = num + 1;
					status2 = CamIntExportStatus.Failed;
				}
				CamIntEvents.OnExportedAssembly(this._rvtDoc, ElementId.InvalidElementId, this._options, ProductType.None, fileName2, status2);
			}
		}

		/// <summary>
        /// 写入文件之中
        /// </summary>
        /// <param name="cncDoc"></param>
        /// <param name="serializer"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
		private bool WriteFile(ItDocument cncDoc, XmlSerializer serializer, out string fileName)
		{
			cncDoc.convertUnits();
			fileName = Path.Combine(cncDoc.FilePath, cncDoc.FileName);
			bool flag = !cncDoc.IsValid;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				try
				{
					using (TextWriter textWriter = new StreamWriter(fileName))
					{
						serializer.Serialize(textWriter, cncDoc, cncDoc.Namespaces);
						return true;
					}
				}
				catch (Exception o)
				{
					o.unused();
				}
				result = false;
			}
			return result;
		}

		/// <summary>
        /// 添加一个产品
        /// </summary>
        /// <param name="cncDoc"></param>
		private void AddProduct(ItDocument cncDoc)
		{
			bool flag = cncDoc.isNull();
			if (!flag)
			{
				ItOrder itOrder = cncDoc.Orders.LastOrDefault<ItOrder>();
				bool flag2 = itOrder.isNull();
				if (!flag2)
				{
					List<ItProduct> productList = itOrder.productList;
					if (productList != null)
					{
						productList.Add(new ItProduct());
					}
				}
			}
		}


        /// <summary>
        /// 创建一个订单
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="projectData"></param>
        /// <returns></returns>
		private static ItOrder CreateOrderInstance(Document doc, CNCProjectData projectData)
		{
			bool flag = doc.isNull();
			ItOrder result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ProjectInfo projectInformation = doc.ProjectInformation;
				ItOrder itOrder = new ItOrder
				{
					OrderNo = projectData.General.OrderNumber,
					Component = projectData.General.Component,
					Storey = projectData.General.Storey,
					DrawingNo = projectData.General.DrawingNumber,
					DrawingDate = projectData.DrawingData.Date,
					DrawingRevision = projectData.DrawingData.Revision,
					DrawingAuthor = projectData.DrawingData.Author,
					Comment = projectData.CommentPXML
				};
				ItCreatePXML.FillProjectDescription(itOrder, projectData);
				ItCreatePXML.FillBuildingSite(itOrder, projectData);
				ItCreatePXML.FillBuildingOwner(itOrder, projectData);
				ItCreatePXML.FillGenericOrderInfo(itOrder, projectData);
				ItCreatePXML.FillApplicationData(itOrder, doc);
				result = itOrder;
			}
			return result;
		}


        /// <summary>
        /// 填充订单信息
        /// </summary>
        /// <param name="order"></param>
        /// <param name="projectData"></param>
		private static void FillGenericOrderInfo(ItOrder order, CNCProjectData projectData)
		{
			bool flag = !string.IsNullOrEmpty(projectData.GenericOrderInfo.Line13);
			if (flag)
			{
				order.GenericOrderInfo13 = projectData.GenericOrderInfo.Line13;
			}
			bool flag2 = !string.IsNullOrEmpty(projectData.GenericOrderInfo.Line14);
			if (flag2)
			{
				order.GenericOrderInfo14 = projectData.GenericOrderInfo.Line14;
			}
			bool flag3 = !string.IsNullOrEmpty(projectData.GenericOrderInfo.Line15);
			if (flag3)
			{
				order.GenericOrderInfo15 = projectData.GenericOrderInfo.Line15;
			}
			bool flag4 = !string.IsNullOrEmpty(projectData.GenericOrderInfo.Line16);
			if (flag4)
			{
				order.GenericOrderInfo16 = projectData.GenericOrderInfo.Line16;
			}
			bool flag5 = !string.IsNullOrEmpty(projectData.GenericOrderInfo.Line17);
			if (flag5)
			{
				order.GenericOrderInfo17 = projectData.GenericOrderInfo.Line17;
			}
			bool flag6 = !string.IsNullOrEmpty(projectData.GenericOrderInfo.Line18);
			if (flag6)
			{
				order.GenericOrderInfo18 = projectData.GenericOrderInfo.Line18;
			}
			bool flag7 = !string.IsNullOrEmpty(projectData.GenericOrderInfo.Line19);
			if (flag7)
			{
				order.GenericOrderInfo19 = projectData.GenericOrderInfo.Line19;
			}
			bool flag8 = !string.IsNullOrEmpty(projectData.GenericOrderInfo.Line20);
			if (flag8)
			{
				order.GenericOrderInfo20 = projectData.GenericOrderInfo.Line20;
			}
		}

		/// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="doc"></param>
		private static void FillApplicationData(ItOrder order, Document doc)
		{
			order.ApplicationName = doc.Application.ActiveAddInId.GetAddInNameFromDocument(doc);
			order.ApplicationGUID = doc.Application.ActiveAddInId.GetGUID().ToString();
			order.ApplicationVersion = ItCreatePXML.PrecastApplicationVersion;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004A64 File Offset: 0x00002C64
		private static void FillBuildingOwner(ItOrder order, CNCProjectData projectData)
		{
			order.GenericOrderInfo09 = projectData.BuildingOwner.Name;
			order.GenericOrderInfo10 = projectData.BuildingOwner.Street;
			order.GenericOrderInfo11 = projectData.BuildingOwner.ZipCode;
			order.GenericOrderInfo12 = projectData.BuildingOwner.Place;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004ABC File Offset: 0x00002CBC
		private static void FillBuildingSite(ItOrder order, CNCProjectData projectData)
		{
			order.GenericOrderInfo05 = projectData.BuildingSite.Name;
			order.GenericOrderInfo06 = projectData.BuildingSite.Street;
			order.GenericOrderInfo07 = projectData.BuildingSite.ZipCode;
			order.GenericOrderInfo08 = projectData.BuildingSite.Place;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004B14 File Offset: 0x00002D14
		private static void FillProjectDescription(ItOrder order, CNCProjectData projectData)
		{
			bool flag = !string.IsNullOrEmpty(projectData.ProjectDescription.Line1);
			if (flag)
			{
				order.GenericOrderInfo01 = projectData.ProjectDescription.Line1;
			}
			bool flag2 = !string.IsNullOrEmpty(projectData.ProjectDescription.Line2);
			if (flag2)
			{
				order.GenericOrderInfo02 = projectData.ProjectDescription.Line2;
			}
			bool flag3 = !string.IsNullOrEmpty(projectData.ProjectDescription.Line3);
			if (flag3)
			{
				order.GenericOrderInfo03 = projectData.ProjectDescription.Line3;
			}
			bool flag4 = !string.IsNullOrEmpty(projectData.ProjectDescription.Line4);
			if (flag4)
			{
				order.GenericOrderInfo04 = projectData.ProjectDescription.Line4;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004BC8 File Offset: 0x00002DC8
		private bool IsAlreadyWritten(AssemblyInstance instance, IEnumerable<ItDocument> queue)
		{
			return false;
		}

		/// <summary>
        /// 获取当前的文档对象
        /// </summary>
        /// <param name="projectData"></param>
        /// <param name="instance"></param>
        /// <param name="options"></param>
        /// <param name="fullFilePath"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
		private static ItDocument GetDocument(CNCProjectData projectData, AssemblyInstance instance, ICamExportIntOptions options, string fullFilePath, out bool cancel)
		{
			cancel = false;
			bool flag = instance == null;
			ItDocument result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = ItCNCFileWriter.dontOverwriteExistingFile(fullFilePath, options.OverwriteMode, out cancel);
				bool flag3 = flag2 | cancel;
				if (flag3)
				{
					result = null;
				}
				else
				{
					string fileName = Path.GetFileName(fullFilePath);
					string directoryName = Path.GetDirectoryName(fullFilePath);
					ItDocument itDocument = new ItDocument
					{
						productType = instance.productType(),
						FilePath = directoryName,
						FileName = fileName
					};
					itDocument.prepare(projectData);
					result = itDocument;
				}
			}
			return result;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004C64 File Offset: 0x00002E64
		public static string GetFileName(AssemblyInstance instance, CNCProjectData projectData, ICamExportIntOptions options, ItDocument cncDoc)
		{
			bool flag = cncDoc != null && options.MultipleElementsInOneFile;
			string result;
			if (flag)
			{
				result = Path.Combine(cncDoc.FilePath, cncDoc.FileName);
			}
			else
			{
				ItCfgNode itCfgNode;
				if (instance == null)
				{
					itCfgNode = null;
				}
				else
				{
					Document document = instance.Document;
					if (document == null)
					{
						itCfgNode = null;
					}
					else
					{
						ItXmlConfig config = document.getConfig();
						itCfgNode = ((config != null) ? config.CamRootNode : null);
					}
				}
				ItCfgNode itCfgNode2 = itCfgNode;
				string text;
				if (itCfgNode2 == null)
				{
					text = null;
				}
				else
				{
					ItCfgNode itCfgNode3 = itCfgNode2["PXML"];
					text = ((itCfgNode3 != null) ? itCfgNode3["FileNameRules"].value : null);
				}
				string text2 = text;
				bool flag2 = string.IsNullOrWhiteSpace(text2);
				if (flag2)
				{
					text2 = "Element_\"ProdNo\".pxml";
				}
				bool flag3 = projectData.isNull();
				if (flag3)
				{
					result = text2;
				}
				else
				{
					ProductType productType = (instance != null) ? instance.productType() : ProductType.None;
					DirectoryInfo directoryInfo = options.TargetDirectory;
					bool subdirectoryPerProductType = options.SubdirectoryPerProductType;
					if (subdirectoryPerProductType)
					{
						directoryInfo = directoryInfo.CreateSubdirectory(productType.ToString());
					}
					string path = CNCFileNameParser.Parse(text2, instance, projectData, true);
					string text3 = Path.Combine(directoryInfo.FullName, path);
					result = text3;
				}
			}
			return result;
		}

		// Token: 0x04000033 RID: 51
		private static readonly string PrecastApplicationVersion = ItCreatePXML.GetFileVersion();

		// Token: 0x04000034 RID: 52
		private readonly IEnumerable<AssemblyInstance> _assemblies;

		// Token: 0x04000035 RID: 53
		private readonly ICamExportIntOptions _options;

		// Token: 0x04000036 RID: 54
		private readonly Document _rvtDoc;
	}
}
