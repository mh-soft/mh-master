using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Hao.Export.Config;
using Hao.Export.Exceptions;
using Hao.Export.Geometry;
using Hao.Export.MachineData.Events;
using Hao.Export.MachineData.PXML;
using AdskLocalisation;
using AdskRevitUI;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x02000002 RID: 2
	internal static class CamExporter
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal static CmdResult Export(Document document, ref string message, IEnumerable<AssemblyInstance> assemblies, ICamExportIntOptions options, bool variedFormats, out bool bCancelCommand)
		{
			bCancelCommand = false;
			assemblies = ((assemblies != null) ? assemblies.ToList<AssemblyInstance>() : null);
			bool flag = assemblies == null || assemblies.none<AssemblyInstance>();
			CmdResult result;
			if (flag)
			{
				result = CmdResult.Succeeded;
			}
			else
			{
				IEnumerable<ElementId> assemblyInstanceIds = (from element in assemblies
				select element.Id).ToList<ElementId>();
				bool flag2;
				CamIntEvents.OnExporting(document, assemblyInstanceIds, options, out flag2);
				CmdResult cmdResult = CmdResult.Succeeded;
				bool flag3 = !flag2;
				if (flag3)
				{
					CNCProjectData cncprojectData = CamExporter.GetCNCProjectData(document, options.FileFormat);
					switch (options.FileFormat)
					{
					case FileFormat.Unitechnik52:
					case FileFormat.Unitechnik60:
						cmdResult = CamExporter.executeUniCAM(ref message, assemblies, options, cncprojectData, out bCancelCommand);
						break;
					case FileFormat.PXML13:
						cmdResult = CamExporter.executePXML(ref message, assemblies, options, cncprojectData, out bCancelCommand);
						break;
					default:
						throw new InvalidOperationException("Unknown file format for CAM export.");
					}
					bool flag4 = cmdResult == CmdResult.Failed;
					if (flag4)
					{
						CamExporter.showError(options.FileFormat);
					}
				}
				CamIntExportStatus status = CamExporter.DetermineStatus(cmdResult, bCancelCommand, flag2);
				CamIntEvents.OnExported(document, assemblyInstanceIds, options, status);
				result = cmdResult;
			}
			return result;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002158 File Offset: 0x00000358
		private static CamIntExportStatus DetermineStatus(CmdResult cmdResult, bool bCancelCommand, bool eventCanceled)
		{
			CamIntExportStatus result;
			if (eventCanceled)
			{
				result = CamIntExportStatus.EventCanceled;
			}
			else if (bCancelCommand)
			{
				result = CamIntExportStatus.UserCanceled;
			}
			else
			{
				bool flag = cmdResult == CmdResult.Cancelled;
				if (flag)
				{
					result = CamIntExportStatus.UserCanceled;
				}
				else
				{
					bool flag2 = cmdResult == CmdResult.Succeeded;
					if (flag2)
					{
						result = CamIntExportStatus.Success;
					}
					else
					{
						bool flag3 = cmdResult == CmdResult.Failed;
						if (flag3)
						{
							result = CamIntExportStatus.Failed;
						}
						else
						{
							ItDebug.assert(false, "Unexpected result of cam export!");
							result = CamIntExportStatus.None;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021B4 File Offset: 0x000003B4
		internal static string ExportReinforcementSnippet(AssemblyInstance assembly, FileFormat fileFormat, ICollection<ExportReinfData> exportedElements, Func<Element, bool> filter = null)
		{
			bool flag = filter == null;
			if (flag)
			{
				filter = ((Element element) => true);
			}
			string result;
			switch (fileFormat)
			{
			case FileFormat.Unitechnik52:
			case FileFormat.Unitechnik60:
				result = CamExporter.ExportReinforcementToUnitechnik(assembly, fileFormat, exportedElements, filter);
				break;
			case FileFormat.PXML13:
				result = CamExporter.ExportReinforcementToPXML(assembly, exportedElements, filter);
				break;
			default:
				throw new InvalidOperationException("Unknown file format for CAM export.");
			}
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002228 File Offset: 0x00000428
		private static string ExportReinforcementToPXML(AssemblyInstance assembly, ICollection<ExportReinfData> exportedElements, Func<Element, bool> filter)
		{
			ICamExportIntOptions options = new CamExporter.CamExportOptionsOnlyFileFormat(FileFormat.PXML13);
			ItMachineDataBase cncdocCreator = ItCNCDataFactory.getCNCDocCreator(assembly, options);
			cncdocCreator.InitializeFromAssembly(assembly);
			ItGeMatrix3d matWcsToPalette = cncdocCreator.MatWcsToPalette;
			ILookup<SteelGroup, SteelGroupElement> steelGroups;
			using (ItTransaction itTransaction = ItTransactionManager.Instance.start(assembly.Document, "RevitPrecast Internal Transaction", null))
			{
				ReinfSorter reinfSorter = new ReinfSorter(assembly, new ItMachineDataBase.CNCElementData(), matWcsToPalette, filter);
				reinfSorter.SortElements();
				steelGroups = reinfSorter.GetSteelGroups();
				itTransaction.rollback("Planned rollback.");
			}
			IEnumerable<ItSteel> collection = from steelGroup in steelGroups
			select steelGroup.Key.ToPXML(exportedElements);
			ItSlab itSlab = new ItSlab();
			itSlab.steelList.AddRange(collection);
			XmlRootAttribute root = new XmlRootAttribute("Slab")
			{
				Namespace = "http://progress-m.com/ProgressXML/Version1"
			};
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ItSlab), root);
			StringWriter stringWriter = new StringWriter();
			XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
			{
				Indent = false,
				OmitXmlDeclaration = true,
				NewLineHandling = NewLineHandling.None
			});
			xmlSerializer.Serialize(xmlWriter, itSlab, ItDocument.StaticNamespaces);
			string text = stringWriter.ToString();
			text = text.Replace("</Slab>", string.Empty);
			int startIndex = text.IndexOf("<Steel ", StringComparison.Ordinal);
			return text.Substring(startIndex);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000023A8 File Offset: 0x000005A8
		private static string ExportReinforcementToUnitechnik(AssemblyInstance assembly, FileFormat fileFormat, ICollection<ExportReinfData> exportedElements, Func<Element, bool> filter)
		{
			ICamExportIntOptions options = new CamExporter.CamExportOptionsOnlyFileFormat(fileFormat);
			ItMachineDataBase cncdocCreator = ItCNCDataFactory.getCNCDocCreator(assembly, options);
			cncdocCreator.InitializeFromAssembly(assembly);
			ItGeMatrix3d matWcsToPalette = cncdocCreator.MatWcsToPalette;
			ItMachineDataBase.CNCElementData cncelementData = new ItMachineDataBase.CNCElementData();
			ILookup<SteelGroup, SteelGroupElement> steelGroups;
			using (ItTransaction itTransaction = ItTransactionManager.Instance.start(assembly.Document, "RevitPrecast Internal Transaction", null))
			{
				ReinfSorter reinfSorter = new ReinfSorter(assembly, cncelementData, matWcsToPalette, filter);
				reinfSorter.SortElements();
				steelGroups = reinfSorter.GetSteelGroups();
				itTransaction.rollback("Planned rollback.");
			}
			short iVersion = (fileFormat == FileFormat.Unitechnik52) ? (short)502 : (short)600;
			ItUniWrapperImpl.StartDocument(1, 1, ProjectCoordinates.Empty, iVersion, "", 1.0, true);
			ItUniWrapperImpl.AddLayer(0, 1.0, 1.0, 1.0, "x");
			foreach (IGrouping<SteelGroup, SteelGroupElement> grouping in steelGroups)
			{
				grouping.Key.WriteUnitechnik(exportedElements);
			}
			ItMachineDataBase.UnitechnikWriteMountingPartData(cncelementData);
			ItMachineDataBase.UnitechnikWriteBRGirdersData(cncelementData);
			string fullReinforcementString = ItUniWrapperImpl.GetFullReinforcementString();
			bool flag = fullReinforcementString.Contains("ERROR");
			if (flag)
			{
				throw new CamExportException(fullReinforcementString);
			}
			return fullReinforcementString;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002514 File Offset: 0x00000714
		private static CNCProjectData GetCNCProjectData(Document document, FileFormat format)
		{
			ItXmlConfig configData = new ItConfig(document).getConfigData();
			string formatNodeName = CamExporter.GetFormatNodeName(format);
			bool flag = string.IsNullOrEmpty(formatNodeName);
			CNCProjectData result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ItCfgNode node = configData.CamRootNode[formatNodeName];
				CNCProjectData cncprojectData = CNCProjectData.Create(document, node);
				result = cncprojectData;
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002564 File Offset: 0x00000764
		private static string GetFormatNodeName(FileFormat format)
		{
			string result;
			switch (format)
			{
			case FileFormat.Unitechnik52:
				result = "Unitechnik52";
				break;
			case FileFormat.Unitechnik60:
				result = "Unitechnik60";
				break;
			case FileFormat.PXML13:
				result = "PXML";
				break;
			default:
				ItDebug.assert(false, "New CAM-Formats must define in which config node their Project Data will be found.");
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000025B3 File Offset: 0x000007B3
		private static void showError(FileFormat format)
		{
			ModalDialog.Show(CamExporter._lclError, CamExporter.errorCodes[format].Localise(), ModalDialogButtons.Ok, ModalDialogResult.Ok);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000025D4 File Offset: 0x000007D4
		public static CmdResult executeUniCAM(ref string message, IEnumerable<AssemblyInstance> assemblies, ICamExportIntOptions options, CNCProjectData projectData, out bool bExit)
		{
			assemblies = assemblies.ToList<AssemblyInstance>();
			bool flag = assemblies.none<AssemblyInstance>();
			CmdResult result;
			if (flag)
			{
				bExit = true;
				result = CmdResult.Failed;
			}
			else
			{
				Document document = assemblies.First<AssemblyInstance>().Document;
				bExit = false;
				bool flag2 = projectData == null;
				if (flag2)
				{
					ItFailures.PostFailure(ItFailures.CAMMissingProjectData, ElementId.InvalidElementId);
				}
				ItTransactionManager.Instance.reset(document);
				using (ItTransaction itTransaction = ItTransactionManager.Instance.start(document, CamExporter._lclExportCNCTransaction, null))
				{
					try
					{
						int val = 0;
						int val2 = 0;
						foreach (AssemblyInstance assemblyInstance in assemblies)
						{
							Parameter parameter = assemblyInstance.getParameter(ItGlobals.PRODNUMBERPARAM, true, true);
							bool flag3 = parameter.isNull();
							if (!flag3)
							{
								bool hasValue = parameter.HasValue;
								if (hasValue)
								{
									RevitElement<Part> part = assemblyInstance.getPart(null);
									bool flag4 = part == null;
									if (!flag4)
									{
										Element mainElement = part.getMainElement();
										int val3;
										int.TryParse(parameter.AsString(), out val3);
										bool flag5 = mainElement is Wall;
										if (flag5)
										{
											val = Math.Max(val, val3);
										}
										else
										{
											bool flag6 = mainElement is Floor;
											if (flag6)
											{
												val2 = Math.Max(val2, val3);
											}
										}
									}
								}
							}
						}
						options.TargetDirectory.Create();
						DirectoryInfo targetDirectory = options.TargetDirectory;
						foreach (AssemblyInstance assemblyInstance2 in assemblies)
						{
							try
							{
								Parameter parameter2 = assemblyInstance2.getParameter(ItGlobals.PRODNUMBERPARAM, true, true);
								bool flag7 = parameter2 == null;
								if (!flag7)
								{
									RevitElement<Part> part2 = assemblyInstance2.getPart(null);
									bool flag8 = part2 == null;
									if (!flag8)
									{
										Element mainElement2 = part2.getMainElement();
										ProductType product = part2.productType();
										bool subdirectoryPerProductType = options.SubdirectoryPerProductType;
										if (subdirectoryPerProductType)
										{
											options.TargetDirectory = targetDirectory.CreateSubdirectory(product.ToString());
										}
										ItMachineDataBase cncdocCreator = ItCNCDataFactory.getCNCDocCreator(assemblyInstance2, options);
										bool flag9 = cncdocCreator.isNull();
										if (!flag9)
										{
											bool flag10 = mainElement2 is Wall;
											if (flag10)
											{
												CamExporter.FixProdNumber(parameter2, ref val);
											}
											else
											{
												bool flag11 = mainElement2 is Floor;
												if (flag11)
												{
													CamExporter.FixProdNumber(parameter2, ref val2);
												}
											}
											int iProdNo = Convert.ToInt32(parameter2.AsString());
											string unitechnikFilename = ItMachineDataBase.GetUnitechnikFilename(assemblyInstance2, options, projectData);
											bool flag12;
											CamIntEvents.OnExportingAssembly(document, assemblyInstance2.Id, options, product, unitechnikFilename, out flag12);
											bool flag13 = flag12;
											if (!flag13)
											{
												bool flag14 = CamExporter.CheckUnitechnikFilename(options, unitechnikFilename, out bExit);
												bool success = false;
												bool flag15 = flag14 && !bExit;
												if (flag15)
												{
													success = cncdocCreator.createMachineDataUnitechnik(assemblyInstance2, iProdNo, projectData, unitechnikFilename);
												}
												CamIntExportStatus status = CamExporter.DetermineStatus(success, bExit, flag14);
												CamIntEvents.OnExportedAssembly(document, assemblyInstance2.Id, options, product, unitechnikFilename, status);
												bool flag16 = bExit;
												if (flag16)
												{
													break;
												}
											}
										}
									}
								}
							}
							catch (SplitByOpeningNotSupportedException ex)
							{
								ItFailures.PostFailure(ItFailures.SplitByOpeningNotSupported, ex.ElementId);
							}
						}
						itTransaction.commit();
					}
					catch (Exception ex2)
					{
						message = ex2.Message;
						ItLogging.print("Exception in ItCmdCreateMaschData");
						ItLogging.printException(ex2);
						itTransaction.rollback("Exception");
						return CmdResult.Failed;
					}
				}
				result = (bExit ? CmdResult.Cancelled : CmdResult.Succeeded);
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000029B4 File Offset: 0x00000BB4
		private static CamIntExportStatus DetermineStatus(bool success, bool userCanceled, bool isFileNameOk)
		{
			CamIntExportStatus result;
			if (success)
			{
				result = CamIntExportStatus.Success;
			}
			else if (userCanceled)
			{
				result = CamIntExportStatus.UserCanceled;
			}
			else if (isFileNameOk)
			{
				result = CamIntExportStatus.Failed;
			}
			else
			{
				result = CamIntExportStatus.Skipped;
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000029E4 File Offset: 0x00000BE4
		private static bool CheckUnitechnikFilename(ICamExportIntOptions options, string fileName, out bool bExit)
		{
			bool flag = ItCNCFileWriter.dontOverwriteExistingFile(fileName, options.OverwriteMode, out bExit);
			return !flag;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002A08 File Offset: 0x00000C08
		private static void FixProdNumber(Parameter paramProdNo, ref int iMaxProdNo)
		{
			bool flag = !paramProdNo.HasValue;
			if (flag)
			{
				int num = iMaxProdNo;
				iMaxProdNo = num + 1;
				paramProdNo.Set(iMaxProdNo.ToString());
			}
		}

		/// <summary>
        /// 执行数据的导出
        /// </summary>
        /// <param name="message"></param>
        /// <param name="assemblies"></param>
        /// <param name="options"></param>
        /// <param name="projectData"></param>
        /// <param name="bExit"></param>
        /// <returns></returns>
		public static CmdResult executePXML(ref string message, IEnumerable<AssemblyInstance> assemblies, ICamExportIntOptions options, CNCProjectData projectData, out bool bExit)
		{
			assemblies = assemblies.ToList<AssemblyInstance>();
			bool flag = assemblies.none<AssemblyInstance>();
			CmdResult result;
			if (flag)
			{
				bExit = true;
				result = CmdResult.Failed;
			}
			else
			{
				bExit = false;
				Document document = assemblies.First<AssemblyInstance>().Document;
				ItTransactionManager.Instance.reset(document);
				using (ItTransaction itTransaction = ItTransactionManager.Instance.start(document, "export CNC data", null))
				{
					try
					{
						ItCreatePXML itCreatePXML = new ItCreatePXML(assemblies, options);
						itCreatePXML.run(projectData);
						bExit = itCreatePXML.UserCanceled;
						itTransaction.commit();
					}
					catch (Exception ex)
					{
						message = ex.Message;
						ItLogging.print("Exception in ItCmdCreateMaschData");
						ItLogging.printException(ex);
						itTransaction.rollback("Exception");
						return CmdResult.Failed;
					}
				}
				result = CmdResult.Succeeded;
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		private static readonly string _lclExportCNCTransaction = "transNameExportCNC".Localise();

		// Token: 0x04000002 RID: 2
		private static readonly string _lclError = "error".Localise();

		// Token: 0x04000003 RID: 3
		private const string strException = "Exception";

		// Token: 0x04000004 RID: 4
		private static readonly Dictionary<FileFormat, string> errorCodes = new Dictionary<FileFormat, string>
		{
			{
				FileFormat.Unitechnik52,
				"errorUnitechnik52Failed"
			},
			{
				FileFormat.Unitechnik60,
				"errorUnitechnik6Failed"
			},
			{
				FileFormat.PXML13,
				"errorPXMLFailed"
			}
		};

		// Token: 0x02000046 RID: 70
		private class CamExportOptionsOnlyFileFormat : ICamExportIntOptions
		{
			// Token: 0x06000540 RID: 1344 RVA: 0x000152C4 File Offset: 0x000134C4
			public CamExportOptionsOnlyFileFormat(FileFormat fileFormat)
			{
				this.FileFormat = fileFormat;
			}

			// Token: 0x17000235 RID: 565
			// (get) Token: 0x06000541 RID: 1345 RVA: 0x000152F1 File Offset: 0x000134F1
			public FileFormat FileFormat { get; }

			// Token: 0x17000236 RID: 566
			// (get) Token: 0x06000542 RID: 1346 RVA: 0x000152F9 File Offset: 0x000134F9
			public CamIntOverwriteMode OverwriteMode { get; } = CamIntOverwriteMode.Ask;

			// Token: 0x17000237 RID: 567
			// (get) Token: 0x06000543 RID: 1347 RVA: 0x00015301 File Offset: 0x00013501
			// (set) Token: 0x06000544 RID: 1348 RVA: 0x00015309 File Offset: 0x00013509
			public DirectoryInfo TargetDirectory { get; set; } = null;

			// Token: 0x17000238 RID: 568
			// (get) Token: 0x06000545 RID: 1349 RVA: 0x00015312 File Offset: 0x00013512
			public bool SubdirectoryPerProductType { get; } = false;

			// Token: 0x17000239 RID: 569
			// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001531A File Offset: 0x0001351A
			public bool MultipleElementsInOneFile { get; } = false;
		}
	}
}
