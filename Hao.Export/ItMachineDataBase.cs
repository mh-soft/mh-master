using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Hao.Export.Config;
using Hao.Export.Geometry;
using Hao.Export.MachineData.PXML;
using Hao.Export.PrecastWall;
using AdskLocalisation;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x02000012 RID: 18
	public abstract class ItMachineDataBase
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000566F File Offset: 0x0000386F
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00005677 File Offset: 0x00003877
		protected ItGeMatrix3d MatAssemblyToPalette { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00005680 File Offset: 0x00003880
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00005688 File Offset: 0x00003888
		internal ItGeMatrix3d MatWcsToPalette { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00005691 File Offset: 0x00003891
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00005699 File Offset: 0x00003899
		protected virtual int ProductType { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000056A2 File Offset: 0x000038A2
		public virtual bool ClearDocument
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000056A8 File Offset: 0x000038A8
		public string ProductTypeString
		{
			get
			{
				int productType = this.ProductType;
				string result;
				if (productType != 1 && productType != 2)
				{
					if (productType != 20)
					{
						result = this.ProductType.ToString("00");
					}
					else
					{
						result = "TW";
					}
				}
				else
				{
					result = "DW";
				}
				return result;
			}
		}

		// Token: 0x06000081 RID: 129
		protected abstract bool writeRebar();

		// Token: 0x06000082 RID: 130
		protected abstract MountingPartData getDoorWindowData(RevitElement<FamilyInstance> familyInstance, RevitElement<Part> part);

		// Token: 0x06000083 RID: 131
		protected abstract MountingPartData getOpeningData(RevitElement<Opening> opening, RevitElement<Part> part);

		// Token: 0x06000084 RID: 132
		protected abstract ItGeVector3d getShiftingDirection();

		// Token: 0x06000085 RID: 133
		protected abstract ItGeVector3d getSpanDirection();

		// Token: 0x06000086 RID: 134
		public abstract double getThickness(ItGeBoundBlock3d bb);

		// Token: 0x06000087 RID: 135
		protected abstract double getAssemblyThickness(ItGeBoundBlock3d bb);

		// Token: 0x06000088 RID: 136 RVA: 0x000056F7 File Offset: 0x000038F7
		protected ItMachineDataBase(ICamExportIntOptions options)
		{
			this._options = options;
			this.ProductType = 0;
			this.MatAssemblyToPalette = ItGeMatrix3d.kIdentity;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000571C File Offset: 0x0000391C
		internal bool createMachineDataUnitechnik(AssemblyInstance assemblyInstance, int iProdNo, CNCProjectData projectData, string fileName)
		{
			this.InitializeFromAssembly(assemblyInstance);
			List<RevitElement<Part>> corePartsFromAssembly = this.GetCorePartsFromAssembly(assemblyInstance);
			ItDebug.assert(corePartsFromAssembly.isNotNull() && corePartsFromAssembly.Count > 0, "Unexpected parts count");
			bool flag = this.ExportUnitechnik(assemblyInstance, corePartsFromAssembly, iProdNo, projectData);
			return flag && ItUniWrapperImpl.EndDocument(fileName) != 0;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000577C File Offset: 0x0000397C
		private List<RevitElement<Part>> GetCorePartsFromAssembly(AssemblyInstance assemblyInstance)
		{
			Func<RevitElement<Part>, CompoundStructure, bool> isCoreLayer = delegate(RevitElement<Part> part, CompoundStructure structure)
			{
				int num;
				part.getLayer(out num);
				return structure.IsCoreLayer(num);
			};
			CompoundStructure compoundStructure = this.GetCompoundStructure(assemblyInstance);
			return (from part in assemblyInstance.getParts()
			where isCoreLayer(part, compoundStructure)
			select part).ToList<RevitElement<Part>>();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000057E4 File Offset: 0x000039E4
		private CompoundStructure GetCompoundStructure(AssemblyInstance assemblyInstance)
		{
			Element mainElement = assemblyInstance.getMainElement();
			Wall wall = mainElement as Wall;
			Floor floor = mainElement as Floor;
			bool flag = wall != null;
			CompoundStructure result;
			if (flag)
			{
				result = wall.WallType.GetCompoundStructure();
			}
			else
			{
				result = ((floor != null) ? floor.FloorType.GetCompoundStructure() : null);
			}
			return result;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005835 File Offset: 0x00003A35
		protected internal virtual void InitializeFromAssembly(AssemblyInstance assemblyInstance)
		{
			this.MatWcsToPalette = this.MatAssemblyToPalette * assemblyInstance.ecsInverse();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000051E6 File Offset: 0x000033E6
		protected virtual void UnitechnikWriteAdditionalSlabData(AssemblyInstance assemblyInstance)
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005850 File Offset: 0x00003A50
		protected virtual bool IsConnectionFamily(FamilyInstance famInst)
		{
			return false;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005864 File Offset: 0x00003A64
		protected virtual bool ExportUnitechnik(AssemblyInstance assemblyInstance, IEnumerable<RevitElement<Part>> parts, int iProdNo, CNCProjectData projectData)
		{
			bool flag = assemblyInstance == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				parts = parts.ToList<RevitElement<Part>>();
				bool flag2 = parts.any((RevitElement<Part> part) => part.ignorePart());
				if (flag2)
				{
					ItDebug.assert(false, "No part containing sub-parts should be member of an assembly");
					ItLogging.print("MachineData: assemblyInst:" + assemblyInstance.Id.IntegerValue + " --> Element ignored");
					result = false;
				}
				else
				{
					ItGeBoundBlock3d buildingExtents = this.getBuildingExtents(assemblyInstance.Document);
					ItGeVector3d viewDirection = this.getViewDirection();
					ItMachineDataBase.CNCElementData cncelementData = new ItMachineDataBase.CNCElementData();
					bool flag3 = !this.getPartData(parts, viewDirection, cncelementData);
					if (flag3)
					{
						result = false;
					}
					else
					{
						ItGeMatrix3d mat = assemblyInstance.ecs() * this.MatAssemblyToPalette.inverse();
						ItGePoint3d itGePoint3d = ItGePoint3d.kOrigin.transformBy(this.MatAssemblyToPalette.inverse());
						ItGeVector3d shiftingDirection = this.getShiftingDirection();
						shiftingDirection.transformBy(mat);
						ItGeVector3d spanDirection = this.getSpanDirection();
						spanDirection.transformBy(mat);
						this.GetAllMountingPartData(assemblyInstance, parts, cncelementData);
						ReinfSorter reinfSorter = new ReinfSorter(assemblyInstance, cncelementData, this.MatWcsToPalette, this.GetReinforcementFilter());
						reinfSorter.SortElements();
						ItGeMatrix3d mat2 = this.moveToOrigin(assemblyInstance, cncelementData, reinfSorter);
						itGePoint3d.transformBy(mat2);
						ItGePoint3d startPoint = assemblyInstance.ecs() * itGePoint3d;
						ProjectCoordinates projectCoordinates = new ProjectCoordinates();
						this.setProjectCoordinates(projectCoordinates, startPoint, buildingExtents, spanDirection, shiftingDirection);
						ItMachineDataBase.convertProjectCoordinates(projectCoordinates);
						List<LayerData> layerData = this.GetLayerData(parts, reinfSorter);
						bool flag4;
						if (!layerData.none<LayerData>())
						{
							flag4 = layerData.any((LayerData data) => data.IsInvalid);
						}
						else
						{
							flag4 = true;
						}
						bool flag5 = flag4;
						if (flag5)
						{
							ItFailures.PostFailure(ItFailures.CAMCantExportInvalidLayerStructure, assemblyInstance.Id);
							result = false;
						}
						else
						{
							double totalArea = this.getTotalArea(assemblyInstance, parts);
							double num = Math.Round(totalArea.FeetToMeter().FeetToMeter(), 3);
							bool flag6 = num.Gt(99.999, -1.0);
							if (flag6)
							{
								ItFailures.PostFailure(ItFailures.CAMValueOutsideAllowedLimits, assemblyInstance.Id);
								num = 99.999;
							}
							FileFormat fileFormat = this._options.FileFormat;
							short num2;
							if (fileFormat != FileFormat.Unitechnik52)
							{
								if (fileFormat != FileFormat.Unitechnik60)
								{
									num2 = -1;
								}
								else
								{
									num2 = 600;
								}
							}
							else
							{
								num2 = 502;
							}
							bool flag7 = num2 == -1;
							if (flag7)
							{
								throw new InvalidOperationException("Bad file format for CAM export.");
							}
							ItUniWrapperImpl.StartDocument(this.ProductType, iProdNo, projectCoordinates, num2, assemblyInstance.Name, num, this.ClearDocument);
							bool flag8 = projectData.isNotNull();
							if (flag8)
							{
								bool flag9 = string.IsNullOrEmpty(projectData.General.OrderNumber);
								if (flag9)
								{
									ItFailures.PostFailure(ItFailures.CAMNoOrderNumber, assemblyInstance.Id);
								}
								ItUniWrapperImpl.SetProjectData(projectData);
							}
							this.UnitechnikWriteLayers(layerData);
							this.UnitechnikWriteThicknesses(assemblyInstance);
							this.UnitechnikWriteShellContour(assemblyInstance, cncelementData);
							this.UnitechnikWriteCutoutData(cncelementData);
							ItMachineDataBase.UnitechnikWriteReinforcements(reinfSorter);
							ItMachineDataBase.UnitechnikWriteMountingPartData(cncelementData);
							ItMachineDataBase.UnitechnikWriteBRGirdersData(cncelementData);
							this.UnitechnikWriteAdditionalSlabData(assemblyInstance);
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005B88 File Offset: 0x00003D88
		protected virtual Func<Element, bool> GetReinforcementFilter()
		{
			return (Element element) => true;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005BBC File Offset: 0x00003DBC
		private void UnitechnikWriteThicknesses(AssemblyInstance instance)
		{
			double totalThicknessFeet = this.GetTotalThicknessFeet(instance);
			int num = (int)Math.Round(totalThicknessFeet.FeetToMeter() * 1000.0);
			ItUniWrapperImpl.WriteThicknesses(num, num);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005BF4 File Offset: 0x00003DF4
		private double GetTotalThicknessFeet(AssemblyInstance instance)
		{
			IEnumerable<Element> source = instance.GetMemberIds().Select(new Func<ElementId, Element>(instance.Document.GetElement));
			List<Tuple<double, double>> list = (from t in source.Select(new Func<Element, Tuple<double, double>>(this.GetMinMaxZCoordinates))
			where t != null
			select t).ToList<Tuple<double, double>>();
			ReinfSorter reinfSorter = new ReinfSorter(instance, new ItMachineDataBase.CNCElementData(), this.MatWcsToPalette, (Element element) => true);
			reinfSorter.SortElements();
			IEnumerable<ItGePoint3d> source2 = reinfSorter.GetPoints(true).ToList<ItGePoint3d>();
			bool flag = source2.any<ItGePoint3d>();
			if (flag)
			{
				double item = source2.Min((ItGePoint3d p) => p.z);
				double item2 = source2.Max((ItGePoint3d p) => p.z);
				list.Add(new Tuple<double, double>(item, item2));
			}
			double num = list.Min((Tuple<double, double> t) => t.Item1);
			double num2 = list.Max((Tuple<double, double> t) => t.Item2);
			return num2 - num;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005D6C File Offset: 0x00003F6C
		private Tuple<double, double> GetMinMaxZCoordinates(Element element)
		{
			ItSolid solid = ItSolid.getSolid(element, this.MatWcsToPalette.inverse(), false);
			bool flag = solid == null;
			Tuple<double, double> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IEnumerable<ItGePoint3d> source = solid.Faces.SelectMany((ItFace face) => face.EdgeArrays.SelectMany((ItEdgeArray edgeArray) => edgeArray.Edges.SelectMany((ItEdge edge) => edge.LocalPoints))).ToList<ItGePoint3d>();
				double item = source.Min((ItGePoint3d p) => p.z);
				double item2 = source.Max((ItGePoint3d p) => p.z);
				Tuple<double, double> tuple = new Tuple<double, double>(item, item2);
				result = tuple;
			}
			return result;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005E2C File Offset: 0x0000402C
		internal static string GetUnitechnikFilename(AssemblyInstance assemblyInstance, ICamExportIntOptions options, CNCProjectData projectData)
		{
			ItCfgNode itCfgNode;
			if (assemblyInstance == null)
			{
				itCfgNode = null;
			}
			else
			{
				Document document = assemblyInstance.Document;
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
			string sXPath = (options.FileFormat == FileFormat.Unitechnik60) ? "Unitechnik60" : "Unitechnik52";
			string text;
			if (itCfgNode2 == null)
			{
				text = null;
			}
			else
			{
				ItCfgNode itCfgNode3 = itCfgNode2[sXPath];
				text = ((itCfgNode3 != null) ? itCfgNode3["FileNameRules"].value : null);
			}
			string text2 = text;
			bool flag = string.IsNullOrWhiteSpace(text2);
			if (flag)
			{
				text2 = "\"FileName:9\".\"ProdNo:3:3:0:r\"";
			}
			bool flag2 = projectData == null;
			string result;
			if (flag2)
			{
				result = text2;
			}
			else
			{
				string path = CNCFileNameParser.Parse(text2, assemblyInstance, projectData, true);
				string text3 = Path.Combine(options.TargetDirectory.FullName, path);
				result = text3;
			}
			return result;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005EE0 File Offset: 0x000040E0
		private List<LayerData> GetLayerData(IEnumerable<RevitElement<Part>> partsList, ReinfSorter reinfSorter)
		{
			List<LayerData> list = new List<LayerData>();
			foreach (RevitElement<Part> revitElement in partsList)
			{
				LayerData layerData = new LayerData();
				layerData.Part = revitElement;
				layerData.Thickness = this.getThickness(revitElement.getLocalExtents(false)) * ItConstants.FeetToMeter;
				Material material = revitElement.material();
				layerData.MaterialName = (((material != null) ? material.Name : null) ?? string.Empty);
				double netConcreteVolume = ItMachineDataBase.GetNetConcreteVolume(revitElement, reinfSorter);
				layerData.Volume = CNCDataBase.ConvertToCubeDecimeters(netConcreteVolume, 1);
				layerData.UnitWeight = ItMachineDataBase.GetLayerUnitWeight(material, revitElement.Id);
				PartType type = revitElement.getType();
				ItDebug.assert(type == PartType.Insulation || type == PartType.AirLayer || type == PartType.LoadBearingLayer || type == PartType.FacingLayer, "Unsupported part type");
				bool flag = type == PartType.Undefined;
				if (flag)
				{
					layerData.IsInvalid = true;
				}
				else
				{
					bool flag2 = type != PartType.Insulation && type != PartType.LoadBearingLayer && type != PartType.FacingLayer;
					if (flag2)
					{
						continue;
					}
				}
				bool flag3 = type == PartType.Insulation;
				if (flag3)
				{
					layerData.PartType = 2;
				}
				else
				{
					layerData.PartType = 0;
				}
				list.Add(layerData);
			}
			return list;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006044 File Offset: 0x00004244
		private void UnitechnikWriteLayers(List<LayerData> layerDatas)
		{
			foreach (LayerData layerData in layerDatas)
			{
				ItUniWrapperImpl.AddLayer(layerData.PartType, layerData.Thickness, layerData.UnitWeight, layerData.Volume, layerData.MaterialName);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000060B4 File Offset: 0x000042B4
		private static double GetLayerUnitWeight(Material material, ElementId id)
		{
			return ItMachineDataBase.GetLayerUnitWeightInKgPerCubeFeet(material, id) / ItConstants.CubicFeetToLiter;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000060D4 File Offset: 0x000042D4
		private static double GetLayerUnitWeightInKgPerCubeFeet(Material material, ElementId id)
		{
			double num = (material != null) ? material.density() : 0.0;
			bool flag = num.Le(0.0, -1.0);
			if (flag)
			{
				ItFailures.PostFailure(ItFailures.CAMMaterialWithoutDensitySet, id);
				num = 67.960431820800011;
			}
			return num;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00006130 File Offset: 0x00004330
		private static double GetNetConcreteVolume(RevitElement<Part> part, ReinfSorter reinfSorter)
		{
			double elementVolume = ItMachineDataBase.GetElementVolume(part);
			double steelVolumeInPart = reinfSorter.GetSteelVolumeInPart(part);
			return elementVolume - steelVolumeInPart;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006154 File Offset: 0x00004354
		private static double GetElementVolume(RevitElement<Part> part)
		{
			return part.volumeInFeet();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000616C File Offset: 0x0000436C
		private VoidsAndMountParts GetAllMountingPartData(AssemblyInstance assemblyInst, IEnumerable<RevitElement<Part>> parts, ItMachineDataBase.CNCElementData data)
		{
			VoidsAndMountParts voidsAndMountParts = new VoidsAndMountParts();
			this.getHostedElements(assemblyInst, voidsAndMountParts);
			using (IEnumerator<RevitElement<Part>> enumerator = parts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RevitElement<Part> part = enumerator.Current;
					List<ItGePolyline2d> cutouts = this.GetCutouts(part);
					data.MountingParts.AddRange(from opening in voidsAndMountParts.openings
					select this.getOpeningData(opening, part));
					data.MountingParts.AddRange(from cutout in cutouts
					select this.GetCutoutData(cutout, part));
					data.MountingParts.AddRange(from elem in voidsAndMountParts.doorsAndWindows
					select this.getDoorWindowData(elem, part));
					data.MountingParts.AddRange(voidsAndMountParts.mountingParts.Select(new Func<RevitElement<FamilyInstance>, MountingPartData>(this.getMountingPartData)));
				}
			}
			data.MountingParts.RemoveAll(new Predicate<MountingPartData>(ItBaseExtensions.isNull));
			this.RemoveDuplicateContours(data.MountingParts);
			return voidsAndMountParts;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000629C File Offset: 0x0000449C
		private static void UnitechnikWriteReinforcements(ReinfSorter reinfSorter)
		{
			ILookup<SteelGroup, SteelGroupElement> steelGroups = reinfSorter.GetSteelGroups();
			List<ExportReinfData> exportedElements = new List<ExportReinfData>();
			foreach (IGrouping<SteelGroup, SteelGroupElement> grouping in steelGroups)
			{
				grouping.Key.WriteUnitechnik(exportedElements);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000062FC File Offset: 0x000044FC
		private static void convertProjectCoordinates(ProjectCoordinates projectCoordinates)
		{
			projectCoordinates.Origin *= ItConstants.FeetToMeter * 1000.0;
			projectCoordinates.YPoint *= ItConstants.FeetToMeter * 1000.0;
			projectCoordinates.XPoint *= ItConstants.FeetToMeter * 1000.0;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00006374 File Offset: 0x00004574
		internal void setProjectCoordinates(ProjectCoordinates projectCoordinates, ItGePoint3d startPoint, ItGeBoundBlock3d extBuilding, ItGeVector3d spanDirection, ItGeVector3d shiftingDirection)
		{
			projectCoordinates.Origin = (startPoint - extBuilding.minPoint).asPoint();
			projectCoordinates.YPoint = projectCoordinates.Origin + shiftingDirection;
			projectCoordinates.XPoint = projectCoordinates.Origin + spanDirection;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000063C4 File Offset: 0x000045C4
		private double getTotalArea(AssemblyInstance assembly, IEnumerable<RevitElement<Part>> parts)
		{
			ItGeVector3d vec = ItGeVector3d.kZAxis.negate();
			ItGeVector3d viewDirection = this.MatWcsToPalette.inverse() * vec;
			ItGeMatrix3d transform = ItGeMatrix3d.kIdentity;
			List<ItGePolyline2d> list = parts.SelectMany((RevitElement<Part> part) => part.getContours(viewDirection, true, true, false, transform, false)).ToList<ItGePolyline2d>();
			ItGePoint3d location = assembly.getLocation();
			ItGePlane projPlane = new ItGePlane(location, viewDirection);
			list = list.merge(projPlane);
			bool flag = list.none<ItGePolyline2d>();
			double result;
			if (flag)
			{
				result = 0.0;
			}
			else
			{
				ItGePolyline2d basePolyline = list[0];
				foreach (ItGePolyline2d polyLineToMove in list)
				{
					ItMachineDataBase.MovePolylineToBaseElevation(basePolyline, polyLineToMove);
				}
				List<ItGePolyline2d> list2 = list.First<ItGePolyline2d>() + list;
				ItDebug.assert(list2.Count == 1, "Elements should not be splitted into pieces");
				double num = list2.Sum((ItGePolyline2d pl) => pl.area);
				result = num;
			}
			return result;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00006500 File Offset: 0x00004700
		private static void MovePolylineToBaseElevation(ItGePolyline2d basePolyline, ItGePolyline2d polyLineToMove)
		{
			double num = basePolyline.elevation - polyLineToMove.elevation;
			bool flag = num.Ne(0.0, -1.0);
			if (flag)
			{
				ItGeVector3d vec = basePolyline.normal.normal() * num;
				ItGeMatrix3d mat = ItGeMatrix3d.translation(vec);
				polyLineToMove.transformBy(mat);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000655C File Offset: 0x0000475C
		private bool getPartData(IEnumerable<RevitElement<Part>> parts, ItGeVector3d viewDirection, ItMachineDataBase.CNCElementData data)
		{
			data.Height = 0.0;
			data.Contour.Clear();
			RevitElement<Part> revitElement = parts.FirstOrDefault<RevitElement<Part>>();
			bool flag = revitElement.isNull() || viewDirection.isNull();
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				AssemblyInstance assembly = revitElement.Element.getAssembly();
				bool flag2 = assembly == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					data.Height = this.getThickness(revitElement.getLocalExtents(false));
					ItGeVector3d itGeVector3d = new ItGeVector3d(viewDirection);
					itGeVector3d.transformBy(revitElement.ecs(false));
					ItGePolyline2d singleContour = revitElement.getSingleContour(itGeVector3d, true, true, null);
					bool flag3 = singleContour.numCurves <= 0;
					if (flag3)
					{
						result = false;
					}
					else
					{
						List<ItGePoint3d> list;
						singleContour.getPoints(out list);
						list.transformBy(assembly.ecsInverse());
						list.transformBy(this.MatAssemblyToPalette);
						data.Contour.AddRange(list);
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006650 File Offset: 0x00004850
		internal bool UnitechnikWriteShellContour(AssemblyInstance instance, ItMachineDataBase.CNCElementData data)
		{
			bool flag = data.Contour.Count < 3;
			bool result;
			if (flag)
			{
				ItFailures.PostFailure(ItFailures.CAMObjectWithoutValidGeometry, instance.Id);
				result = false;
			}
			else
			{
				this.UnitechnikVerifyMaxDimensions(instance, data.Contour);
				List<ItUniWrapperImpl.Point2D> list = data.Contour.Select(new Func<ItGePoint3d, ItUniWrapperImpl.Point2D>(ItMachineDataBase.ConvertToPoint2D)).ToList<ItUniWrapperImpl.Point2D>();
				list.Add(ItMachineDataBase.ConvertToPoint2D(data.Contour.First<ItGePoint3d>()));
				ItUniWrapperImpl.AddContour(list);
				result = true;
			}
			return result;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000066D4 File Offset: 0x000048D4
		private void UnitechnikVerifyMaxDimensions(AssemblyInstance instance, List<ItGePoint3d> contourPart)
		{
			bool flag = instance.isInvalid();
			if (!flag)
			{
				double num = contourPart.Min((ItGePoint3d p) => p.x).FeetToMeter() * 1000.0;
				double num2 = contourPart.Max((ItGePoint3d p) => p.x).FeetToMeter() * 1000.0;
				double num3 = contourPart.Min((ItGePoint3d p) => p.y).FeetToMeter() * 1000.0;
				double num4 = contourPart.Max((ItGePoint3d p) => p.y).FeetToMeter() * 1000.0;
				bool flag2 = num2 - num > 32767.0 || num4 - num3 > 32767.0;
				if (flag2)
				{
					ItFailures.PostFailure(ItFailures.CAMMaxLengthOrWidthExceeded, instance.Id);
				}
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006800 File Offset: 0x00004A00
		protected bool UnitechnikWriteCutoutData(ItMachineDataBase.CNCElementData data)
		{
			IEnumerable<MountingPartData> enumerable = from mountPart in data.MountingParts
			where mountPart.IsOpeningWithoutGeometry
			select mountPart;
			foreach (MountingPartData mountingPartData in enumerable)
			{
				bool flag = mountingPartData.Contour.none<ItGePoint3d>() || mountingPartData.Contour.Count < 3;
				if (!flag)
				{
					double area = CNCDataBase.ConvertToSquareMeter(mountingPartData.Area, 3);
					ItUniWrapperImpl.CutoutData data2 = new ItUniWrapperImpl.CutoutData(mountingPartData.Name, area);
					List<ItUniWrapperImpl.Point2D> list = new List<ItUniWrapperImpl.Point2D>();
					foreach (ItGePoint3d point in mountingPartData.Contour)
					{
						ItUniWrapperImpl.Point2D item = ItMachineDataBase.ConvertToPoint2D(point);
						list.Add(item);
					}
					list.Add(ItMachineDataBase.ConvertToPoint2D(mountingPartData.Contour.First<ItGePoint3d>()));
					ItUniWrapperImpl.AddCutout(data2, list);
				}
			}
			return true;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006940 File Offset: 0x00004B40
		private static ItUniWrapperImpl.Point2D ConvertToPoint2D(ItGePoint3d point)
		{
			int x = CNCDataBase.convertToMM(point.x);
			int y = CNCDataBase.convertToMM(point.y);
			ItUniWrapperImpl.Point2D result = new ItUniWrapperImpl.Point2D(x, y);
			return result;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006974 File Offset: 0x00004B74
		internal ItGeBoundBlock3d getBuildingExtents(Document doc)
		{
			ElementClassFilter elementClassFilter = new ElementClassFilter(typeof(Wall));
			ElementClassFilter elementClassFilter2 = new ElementClassFilter(typeof(Floor));
			LogicalOrFilter logicalOrFilter = new LogicalOrFilter(elementClassFilter, elementClassFilter2);
			FilteredElementCollector filteredElementCollector = new FilteredElementCollector(doc);
			IList<Element> list = filteredElementCollector.WherePasses(logicalOrFilter).WhereElementIsNotElementType().ToElements();
			bool flag = true;
			ItGeBoundBlock3d itGeBoundBlock3d = new ItGeBoundBlock3d();
			foreach (Element element in list)
			{
				BoundingBoxXYZ boundingBoxXYZ = element.get_BoundingBox(null);
				bool flag2 = flag;
				if (flag2)
				{
					flag = false;
					itGeBoundBlock3d.set(boundingBoxXYZ.Min.asPoint(), boundingBoxXYZ.Max.asPoint(), true);
				}
				else
				{
					itGeBoundBlock3d.extend(boundingBoxXYZ.Min.asPoint());
					itGeBoundBlock3d.extend(boundingBoxXYZ.Max.asPoint());
				}
			}
			return itGeBoundBlock3d;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00006A7C File Offset: 0x00004C7C
		private void getHostedElements(AssemblyInstance assembly, VoidsAndMountParts voidsAndMountParts)
		{
			bool flag = assembly == null;
			if (!flag)
			{
				List<Element> source = assembly.GetMemberIds().Select(new Func<ElementId, Element>(assembly.Document.GetElement)).ToList<Element>();
				this.sortFamilyInstances(assembly, source.OfType<FamilyInstance>().ToList<FamilyInstance>(), voidsAndMountParts.doorsAndWindows, voidsAndMountParts.mountingParts);
				this.findOpenings(assembly, voidsAndMountParts.openings);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006AE4 File Offset: 0x00004CE4
		private void findOpenings(AssemblyInstance assemblyInstance, List<RevitElement<Opening>> openings)
		{
			Element mainElement = assemblyInstance.getMainElement();
			bool flag = mainElement.isNull();
			if (!flag)
			{
				FilteredElementCollector filteredElementCollector = new FilteredElementCollector(assemblyInstance.Document);
				BoundingBoxIntersectsFilter boundBoxFilter = this.getBoundBoxFilter(assemblyInstance);
				ElementClassFilter elementClassFilter = new ElementClassFilter(typeof(Opening));
				filteredElementCollector.WherePasses(elementClassFilter);
				filteredElementCollector.WherePasses(boundBoxFilter);
				IEnumerable<Opening> source = filteredElementCollector.OfType<Opening>();
				IEnumerable<Opening> source2 = source.Where(delegate(Opening opening)
				{
					Element host = opening.Host;
					return ((host != null) ? host.Id : null) == mainElement.Id;
				});
				IEnumerable<RevitElement<Opening>> collection = from o in source2
				select new RevitElement<Opening>(o);
				openings.AddRange(collection);
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00006B9C File Offset: 0x00004D9C
		private BoundingBoxIntersectsFilter getBoundBoxFilter(AssemblyInstance assemblyInstance)
		{
			List<RevitElement<Part>> parts = assemblyInstance.getParts();
			IEnumerable<BoundingBoxXYZ> source = from part in parts
			select part.Element.get_BoundingBox(null);
			BoundingBoxXYZ boundingBoxXYZ = source.Aggregate((BoundingBoxXYZ box1, BoundingBoxXYZ box2) => box1.combine(box2));
			Outline outline = new Outline(boundingBoxXYZ.Min, boundingBoxXYZ.Max);
			return new BoundingBoxIntersectsFilter(outline);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006C20 File Offset: 0x00004E20
		private void sortFamilyInstances(AssemblyInstance assembly, List<FamilyInstance> familyInstances, List<RevitElement<FamilyInstance>> doorsAndWindows, List<RevitElement<FamilyInstance>> mountingParts)
		{
            Category catDoors = null;// assembly.Document.Settings.Categories[BuiltInCategory.OST_Doors];
            Category catWindows = null;// assembly.Document.Settings.Categories[-2000014];
            Category catGenericModel = null;// assembly.Document.Settings.Categories[-2000151];
			Func<FamilyInstance, bool> original = (FamilyInstance famInst) => famInst.Category.Id == catDoors.Id;
			Func<FamilyInstance, bool> newPredicate = (FamilyInstance famInst) => famInst.Category.Id == catWindows.Id;
			Func<FamilyInstance, bool> original2 = (FamilyInstance famInst) => famInst.Category.Id == catGenericModel.Id;
			Func<FamilyInstance, bool> newPredicate2 = (FamilyInstance famInst) => famInst.load<ItBase>(new Guid?(ItBase.guid), false) != null;
			Func<FamilyInstance, bool> func = original.Or(newPredicate);
			Func<FamilyInstance, bool> newPredicate3 = original2.And(newPredicate2);
			Func<FamilyInstance, bool> filterForValidMountingParts = this.GetFilterForValidMountingParts();
			Func<FamilyInstance, bool> predicate = ItBaseExtensions.Not<FamilyInstance>(func.Or(newPredicate3)).And(filterForValidMountingParts);
			doorsAndWindows.AddRange(from e in familyInstances.Where(func)
			select new RevitElement<FamilyInstance>(e));
			mountingParts.AddRange(from e in familyInstances.Where(predicate)
			select new RevitElement<FamilyInstance>(e));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006D78 File Offset: 0x00004F78
		protected virtual Func<FamilyInstance, bool> GetFilterForValidMountingParts()
		{
			Func<FamilyInstance, bool> original = new Func<FamilyInstance, bool>(this.IsConnectionFamily);
			Func<FamilyInstance, bool> original2 = delegate(FamilyInstance famInst)
			{
				ItConnectionElementData itConnectionElementData = ItMachineDataBase.LoadOrParentLoad<ItConnectionElementData>(famInst, ItBase.guid);
				return itConnectionElementData != null && itConnectionElementData.IgnoreInUnitechnik;
			};
			Func<FamilyInstance, bool> isNested = (FamilyInstance famInst) => (((famInst != null) ? famInst.SuperComponent : null) as FamilyInstance).isNotNull();
			Func<FamilyInstance, bool> original3 = (FamilyInstance famInst) => ((famInst != null) ? famInst.GetSubComponentIds() : null).any<ElementId>();
			Func<FamilyInstance, bool> original4 = ItBaseExtensions.Not<FamilyInstance>(original3).And(ItBaseExtensions.Not<FamilyInstance>(isNested));
            Func<FamilyInstance, bool> newPredicate = null;// ItBaseExtensions.Not<FamilyInstance>((FamilyInstance famInst) => isNested(famInst.SuperComponent as FamilyInstance));
			Func<FamilyInstance, bool> newPredicate2 = isNested.And(newPredicate);
			Func<FamilyInstance, bool> original5 = original.And(ItBaseExtensions.Not<FamilyInstance>(original2)).And(original4.Or(newPredicate2));
			Func<FamilyInstance, bool> newPredicate3 = ItBaseExtensions.Not<FamilyInstance>(original).And(ItBaseExtensions.Not<FamilyInstance>(isNested));
			return original5.Or(newPredicate3);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006E80 File Offset: 0x00005080
		protected static T LoadOrParentLoad<T>(FamilyInstance famInst, Guid guid) where T : ItBase, IExtensibleDataObject, new()
		{
			T result;
			if ((result = ((famInst != null) ? famInst.load<T>(new Guid?(guid), false) : null)) == null)
			{
				if (famInst == null)
				{
					result = null;
				}
				else
				{
					Element superComponent = famInst.SuperComponent;
					result = ((superComponent != null) ? superComponent.load<T>(new Guid?(guid), false) : null);
				}
			}
			return result;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006ED0 File Offset: 0x000050D0
		protected MountingPartData getMountingPartData(RevitElement<FamilyInstance> mountingPart)
		{
			bool flag = this.ignoreMountingPart(mountingPart);
			MountingPartData result;
			if (flag)
			{
				result = null;
			}
			else
			{
				double height;
				double installationHeight;
				List<ItGePoint3d> contour;
				bool mountingPartDataImpl = ItMachineDataBase.getMountingPartDataImpl(mountingPart, this.MatWcsToPalette, out height, out installationHeight, out contour);
				bool flag2 = !mountingPartDataImpl;
				if (flag2)
				{
					result = null;
				}
				else
				{
					MountingPartData mountingPartData = new MountingPartData
					{
						Contour = contour,
						ElementId = mountingPart.Id,
						Height = height,
						InstallationHeight = installationHeight,
						IsOpeningWithoutGeometry = false,
						Name = mountingPart.Element.Name,
						Type = MountingPartData.Types.MountPart,
						UniqueId = mountingPart.UniqueId
					};
					result = mountingPartData;
				}
			}
			return result;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006F84 File Offset: 0x00005184
		private static bool getMountingPartDataImpl(RevitElement<FamilyInstance> mountingPart, ItGeMatrix3d matWcsToPalette, out double dThickness, out double dDistance, out List<ItGePoint3d> contour)
		{
			dThickness = 0.0;
			dDistance = 0.0;
			contour = null;
			ItGeBoundBlock3d localExtents = mountingPart.Element.getLocalExtents();
			bool flag = localExtents == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				ItGeMatrix3d mat = mountingPart.Element.ecs();
				ItGeMatrix3d transform = matWcsToPalette * mat;
				ItGeBoundBlock3d transformed = localExtents.GetTransformed(transform);
				double z = transformed.minPoint.z;
				double z2 = transformed.maxPoint.z;
				bool flag2 = z.Lt(0.0, -1.0);
				if (flag2)
				{
					ItFailures.PostFailure(ItFailures.CAMMountPartBelowElementOnTable, mountingPart.Id);
					result = false;
				}
				else
				{
					dThickness = Math.Abs(z2 - z);
					dDistance = Math.Max(z, 0.0);
					contour = new List<ItGePoint3d>();
					ItGePoint3d item = new ItGePoint3d(transformed.minPoint);
					ItGePoint3d item2 = new ItGePoint3d(transformed.minPoint.x, transformed.maxPoint.y, transformed.minPoint.z, null);
					ItGePoint3d item3 = new ItGePoint3d(transformed.maxPoint.x, transformed.maxPoint.y, transformed.minPoint.z, null);
					ItGePoint3d item4 = new ItGePoint3d(transformed.maxPoint.x, transformed.minPoint.y, transformed.minPoint.z, null);
					contour.Add(item);
					contour.Add(item2);
					contour.Add(item3);
					contour.Add(item4);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000711C File Offset: 0x0000531C
		private bool ignoreMountingPart(RevitElement<FamilyInstance> familyInstance)
		{
			ItBase itBase = familyInstance.load<ItBase>(new Guid?(ItBase.guid), false);
			FamilyInstance familyInstance2 = familyInstance.Element.SuperComponent as FamilyInstance;
			bool flag = itBase == null && familyInstance2 != null;
			bool result;
			if (flag)
			{
				result = this.ignoreMountingPart(familyInstance2);
			}
			else
			{
				ItConnectionElementData itConnectionElementData = itBase as ItConnectionElementData;
				bool flag2 = (itConnectionElementData != null && itConnectionElementData.IgnoreInUnitechnik) || itBase is ItIdentityData;
				result = flag2;
			}
			return result;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000719C File Offset: 0x0000539C
		protected internal static bool UnitechnikWriteMountingPartData(ItMachineDataBase.CNCElementData data)
		{
			bool result = true;
			IEnumerable<MountingPartData> enumerable = from mountPart in data.MountingParts
			where !mountPart.IsOpeningWithoutGeometry
			select mountPart;
			foreach (MountingPartData mountingPartData in enumerable)
			{
				List<ItGePoint3d> contour = mountingPartData.Contour;
				bool flag = contour.Count < 3;
				if (!flag)
				{
					int valueInMM = CNCDataBase.convertToMM(mountingPartData.Height);
					int valueInMM2 = CNCDataBase.convertToMM(mountingPartData.InstallationHeight);
					int[] array = new int[4 * contour.Count + 2];
					array[0] = ItMachineDataBase.TrimValueToMaxAllowed(valueInMM, 999, mountingPartData.ElementId, false);
					array[1] = ItMachineDataBase.TrimValueToMaxAllowed(valueInMM2, 999, mountingPartData.ElementId, false);
					int num3;
					for (int i = 0; i < contour.Count; i = num3 + 1)
					{
						ItDebug.assert(4 * i + 5 < array.Length, "Invalid Array Length");
						int num = i + 1;
						bool flag2 = num >= contour.Count;
						if (flag2)
						{
							num = 0;
						}
						int num2 = CNCDataBase.convertToMM(contour[i].x);
						num2 = ItMachineDataBase.TrimValueToMaxAllowed(num2, 99999, mountingPartData.ElementId, true);
						array[4 * i + 2] = num2;
						num2 = CNCDataBase.convertToMM(contour[i].y);
						num2 = ItMachineDataBase.TrimValueToMaxAllowed(num2, 99999, mountingPartData.ElementId, true);
						array[4 * i + 3] = num2;
						num2 = CNCDataBase.convertToMM(contour[num].x);
						num2 = ItMachineDataBase.TrimValueToMaxAllowed(num2, 99999, mountingPartData.ElementId, true);
						array[4 * i + 4] = num2;
						num2 = CNCDataBase.convertToMM(contour[num].y);
						num2 = ItMachineDataBase.TrimValueToMaxAllowed(num2, 99999, mountingPartData.ElementId, true);
						array[4 * i + 5] = num2;
						num3 = i;
					}
					int num4 = ItUniWrapperImpl.AddMountPart(mountingPartData.Name, array);
					bool flag3 = num4 <= 0;
					if (flag3)
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000073F0 File Offset: 0x000055F0
		private static int TrimValueToMaxAllowed(int valueInMM, int maxAllowedValue, ElementId id, bool withSign)
		{
			bool flag = !withSign && valueInMM < 0;
			int result;
			if (flag)
			{
				ItFailures.PostFailure(ItFailures.CAMValueOutsideAllowedLimits, id);
				result = 0;
			}
			else
			{
				bool flag2 = valueInMM > maxAllowedValue;
				if (flag2)
				{
					valueInMM = maxAllowedValue;
					ItFailures.PostFailure(ItFailures.CAMValueOutsideAllowedLimits, id);
				}
				else
				{
					bool flag3 = valueInMM < -1 * maxAllowedValue;
					if (flag3)
					{
						valueInMM = -maxAllowedValue;
						ItFailures.PostFailure(ItFailures.CAMValueOutsideAllowedLimits, id);
					}
				}
				result = valueInMM;
			}
			return result;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000745C File Offset: 0x0000565C
		protected void getBRGirdersData(RevitElement<FamilyInstance> famInst, List<ItBracedGirderData> girderList)
		{
			bool flag = famInst.isNull();
			if (!flag)
			{
				girderList.Add(new ItBracedGirderData(famInst, this.MatWcsToPalette, 0.0));
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00007494 File Offset: 0x00005694
		protected internal static bool UnitechnikWriteBRGirdersData(ItMachineDataBase.CNCElementData data)
		{
			bool flag = data.Girders.none<ItBracedGirderData>();
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				List<List<ItBracedGirderData>> list = new List<List<ItBracedGirderData>>();
				int num;
				for (int i = 0; i < data.Girders.Count; i = num + 1)
				{
					ItBracedGirderData itBracedGirderData = data.Girders.ElementAt(i);
					bool isGrouped = itBracedGirderData.isGrouped;
					if (!isGrouped)
					{
						itBracedGirderData.isGrouped = true;
						List<ItBracedGirderData> list2 = new List<ItBracedGirderData>
						{
							itBracedGirderData
						};
						for (int j = i + 1; j < data.Girders.Count; j = num + 1)
						{
							bool flag2 = !data.Girders[j].isGrouped && itBracedGirderData.isSimilarTypeAs(data.Girders[j]);
							if (flag2)
							{
								list2.Add(data.Girders[j]);
								data.Girders[j].isGrouped = true;
							}
							num = j;
						}
						list.Add(list2);
					}
					num = i;
				}
				bool flag3 = true;
				foreach (List<ItBracedGirderData> list3 in list)
				{
					int[] array = new int[10];
					bool flag4 = list3.Count == 1;
					if (flag4)
					{
						ItBracedGirderData itBracedGirderData2 = list3.First<ItBracedGirderData>();
						itBracedGirderData2.setParametersForUnitechnik(array, 1, 0.0, itBracedGirderData2.midline.startPoint.x, itBracedGirderData2.midline.startPoint.y);
						int num2 = ItUniWrapperImpl.AddGirder(itBracedGirderData2.famInst.Element.Symbol.Name, array);
						bool flag5 = num2 <= 0;
						if (flag5)
						{
							flag3 = false;
						}
					}
					else
					{
						bool xToBeSet = list3.First<ItBracedGirderData>().midline.direction().isParallelTo(ItGeVector3d.kYAxis, null);
						List<ItBracedGirderData> list4 = new List<ItBracedGirderData>(list3);
						list4.Sort((ItBracedGirderData left, ItBracedGirderData right) => ItMachineDataBase.CompareGirderPositions(left, right, xToBeSet));
						Dictionary<double, int> dictionary = new Dictionary<double, int>(ItComparerFactory.doubleEqualityComparer);
						double offset = 0.0;
						int num3 = 0;
						for (int k = 1; k < list4.Count; k = num + 1)
						{
							double num4 = ItMachineDataBase.GetGirderCoordinate(list4[k], xToBeSet) - ItMachineDataBase.GetGirderCoordinate(list4[k - 1], xToBeSet);
							bool flag6 = num4.Eq(0.0, -1.0);
							if (flag6)
							{
								List<ElementId> elementIdsForAdd = new List<ElementId>
								{
									list4[k].famInst.Id,
									list4[k - 1].famInst.Id
								};
								ItFailures.postFailure(ItFailures.GirderCollisionWarning, elementIdsForAdd);
							}
							else
							{
								int num5;
								bool flag7 = dictionary.TryGetValue(num4, out num5);
								if (flag7)
								{
									num = num5;
									num5 = num + 1;
									bool flag8 = num5 > num3;
									if (flag8)
									{
										num3 = num5;
										offset = num4;
									}
									dictionary[num4] = num5;
								}
								else
								{
									dictionary.Add(num4, 1);
									num3 = 1;
									offset = num4;
								}
							}
							num = k;
						}
						bool flag9 = dictionary.Count == 1;
						if (flag9)
						{
							ItBracedGirderData itBracedGirderData3 = list4.First<ItBracedGirderData>();
							double girderCoordinate = ItMachineDataBase.GetGirderCoordinate(itBracedGirderData3, true);
							double girderCoordinate2 = ItMachineDataBase.GetGirderCoordinate(itBracedGirderData3, false);
							itBracedGirderData3.setParametersForUnitechnik(array, num3 + 1, offset, girderCoordinate, girderCoordinate2);
							int num2 = ItUniWrapperImpl.AddGirder(itBracedGirderData3.famInst.Element.Symbol.Name, array);
							bool flag10 = num2 <= 0;
							if (flag10)
							{
								flag3 = false;
							}
						}
						else
						{
							foreach (ItBracedGirderData itBracedGirderData4 in list3)
							{
								itBracedGirderData4.setParametersForUnitechnik(array, 1, 0.0, itBracedGirderData4.midline.startPoint.x, itBracedGirderData4.midline.startPoint.y);
								int num2 = ItUniWrapperImpl.AddGirder(itBracedGirderData4.famInst.Element.Symbol.Name, array);
								bool flag11 = num2 <= 0;
								if (flag11)
								{
									flag3 = false;
								}
							}
						}
					}
				}
				result = flag3;
			}
			return result;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00007948 File Offset: 0x00005B48
		private static double GetGirderCoordinate(ItBracedGirderData girderData, bool getXCoord)
		{
			return getXCoord ? girderData.midline.startPoint.x : girderData.midline.startPoint.y;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00007980 File Offset: 0x00005B80
		private static int CompareGirderPositions(ItBracedGirderData left, ItBracedGirderData right, bool xToBeSet)
		{
			double girderCoordinate = ItMachineDataBase.GetGirderCoordinate(left, xToBeSet);
			double girderCoordinate2 = ItMachineDataBase.GetGirderCoordinate(right, xToBeSet);
			return girderCoordinate.CompareTo(girderCoordinate2);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000079AC File Offset: 0x00005BAC
		private ItGeMatrix3d moveToOrigin(AssemblyInstance assembly, ItMachineDataBase.CNCElementData data, ReinfSorter reinfSorter)
		{
			ItGeMatrix3d translateToOrigin = this.GetTranslateToOrigin(assembly, data, reinfSorter, true);
			data.Contour.transformBy(translateToOrigin);
			data.MountingParts.transformBy(translateToOrigin);
			reinfSorter.TransformBy(translateToOrigin);
			data.Girders.transformBy(translateToOrigin);
			return translateToOrigin;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000079FC File Offset: 0x00005BFC
		internal ItGeMatrix3d GetTranslateToOrigin(AssemblyInstance assembly, ItMachineDataBase.CNCElementData data, ReinfSorter reinfSorter, bool schematic)
		{
			ItGeMatrix3d kIdentity = ItGeMatrix3d.kIdentity;
			ItGeBoundBlock3d localExtents = assembly.getLocalExtents();
			ItDebug.assert(localExtents != null, "assembly has no extents");
			bool flag = localExtents == null;
			ItGeMatrix3d result;
			if (flag)
			{
				result = kIdentity;
			}
			else
			{
				localExtents.setToBox(false);
				localExtents.transformBy(this.MatAssemblyToPalette);
				ItGePoint3d minPoint = localExtents.minPoint;
				List<ItGePoint3d> list = new List<ItGePoint3d>
				{
					minPoint
				};
				list.AddRange(data.MountingParts.SelectMany((MountingPartData mp) => mp.Contour));
				list.AddRange(reinfSorter.GetPoints(schematic));
				list.AddRange(data.Girders.SelectMany((ItBracedGirderData girder) => girder.midline.Points));
				double num = list.Min((ItGePoint3d p) => p.x);
				double num2 = list.Min((ItGePoint3d p) => p.y);
				double num3 = list.Min((ItGePoint3d p) => p.z);
				bool flag2 = num3.Lt(0.0, -1.0);
				if (flag2)
				{
					ItFailures.PostFailure(ItFailures.CAMMountPartBelowElementOnTable, assembly.Id);
					num3 = 0.0;
				}
				ItGeVector3d toTranslation = new ItGeVector3d(-num, -num2, -num3);
				kIdentity.setToTranslation(toTranslation);
				result = kIdentity;
			}
			return result;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00007BAC File Offset: 0x00005DAC
		protected List<ItGePolyline2d> GetCutouts(RevitElement<Part> part)
		{
			ItSolid itSolid = part.isNotNull() ? ItSolid.getSolid(part, part.ecs(false), true) : null;
			bool flag = itSolid == null;
			List<ItGePolyline2d> result;
			if (flag)
			{
				result = new List<ItGePolyline2d>();
			}
			else
			{
				List<ItGePolyline2d> list = itSolid.asPolyList(part.Document);
				bool flag2 = list.none<ItGePolyline2d>();
				if (flag2)
				{
					result = new List<ItGePolyline2d>();
				}
				else
				{
					ItGeVector3d itGeVector3d = this.MatWcsToPalette.inverse() * ItGeVector3d.kZAxis;
					List<ItGePolyline2d> contour = list.getContour(-itGeVector3d, true, false, false, null, null);
					List<ItGePolyline2d> contour2 = list.getContour(itGeVector3d, true, false, false, null, null);
					contour.markHoles(null);
					contour2.markHoles(null);
					List<ItGePolyline2d> arrayPoly = (from polyline in contour
					where polyline.isHole
					select polyline).ToList<ItGePolyline2d>();
					List<ItGePolyline2d> list2 = (from polyline in contour2
					where polyline.isHole
					select polyline).ToList<ItGePolyline2d>();
					ItGeMatrix3d transformSecondSideToFirstSide = this.GetTransformSecondSideToFirstSide(part, itGeVector3d);
					list2.transformBy(transformSecondSideToFirstSide);
					List<ItGePolyline2d> list3 = arrayPoly.intersect(list2);
					result = (list3 ?? new List<ItGePolyline2d>());
				}
			}
			return result;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00007CE8 File Offset: 0x00005EE8
		protected ItGeMatrix3d GetTransformSecondSideToFirstSide(RevitElement<Part> part, ItGeVector3d normal)
		{
			return ItGeMatrix3d.translation(normal * part.thickness());
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00007D10 File Offset: 0x00005F10
		private void RemoveDuplicateContours(List<MountingPartData> mountPartList)
		{
			IEnumerable<List<ItGePoint3d>> source = from mountPart in mountPartList
			select mountPart.Contour;
			List<ItGePolyline2d> list = source.Select(new Func<List<ItGePoint3d>, ItGePolyline2d>(ItMachineDataBase.GetPolyline2DFromPointList)).ToList<ItGePolyline2d>();
			int num;
			for (int i = 0; i < list.Count; i = num + 1)
			{
				for (int j = list.Count - 1; j > i; j = num - 1)
				{
					bool flag = ItMachineDataBase.ArePolylineRegionsEqual(list[i], list[j]);
					if (flag)
					{
						mountPartList.RemoveAt(j);
						list.RemoveAt(j);
					}
					num = j;
				}
				num = i;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00007DC4 File Offset: 0x00005FC4
		private static ItGePolyline2d GetPolyline2DFromPointList(List<ItGePoint3d> pointList)
		{
			ItGePolyline3d poly3d = new ItGePolyline3d(pointList);
			return new ItGePolyline2d(poly3d, null);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00007DE8 File Offset: 0x00005FE8
		private static bool ArePolylineRegionsEqual(ItGePolyline2d pl1, ItGePolyline2d pl2)
		{
			bool flag = pl1.area.Eq(0.0, -1.0) || pl2.area.Eq(0.0, -1.0);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = pl1.numCurves != pl2.numCurves;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = pl1.area.Ne(pl2.area, -1.0);
					if (flag3)
					{
						result = false;
					}
					else
					{
						ItGePolyline2d itGePolyline2d = new ItGePolyline2d(pl2);
						ItMachineDataBase.MovePolylineToBaseElevation(pl1, itGePolyline2d);
						bool flag4 = pl1.elevation.Ne(itGePolyline2d.elevation, -1.0);
						result = (!flag4 && (pl1 - itGePolyline2d).none<ItGePolyline2d>() && (itGePolyline2d - pl1).none<ItGePolyline2d>());
					}
				}
			}
			return result;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00007ED4 File Offset: 0x000060D4
		protected MountingPartData GetCutoutData(ItGePolyline2d contour, RevitElement<Part> part)
		{
			List<ItGePoint3d> list;
			contour.getPoints(out list);
			bool flag = list.Count < 3;
			MountingPartData result;
			if (flag)
			{
				ItFailures.PostFailure(ItFailures.CAMObjectWithoutValidGeometry, part.Id);
				result = null;
			}
			else
			{
				list.transformBy(this.MatWcsToPalette);
				MountingPartData mountingPartData = new MountingPartData
				{
					Contour = list,
					ElementId = ElementId.InvalidElementId,
					Height = part.thickness(),
					InstallationHeight = 0.0,
					IsOpeningWithoutGeometry = true,
					Name = string.Empty,
					Type = MountingPartData.Types.CutOut,
					UniqueId = null
				};
				result = mountingPartData;
			}
			return result;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00007F88 File Offset: 0x00006188
		protected MountingPartData GetOpeningData(RevitElement<Opening> opening, RevitElement<Part> part, ItGeVector3d viewDirection)
		{
			List<ItGePoint3d> contourFromOpening = ItMachineDataBase.getContourFromOpening(opening, part, viewDirection);
			bool flag = contourFromOpening.none<ItGePoint3d>() || contourFromOpening.Count < 3;
			MountingPartData result;
			if (flag)
			{
				ItFailures.PostFailure(ItFailures.CAMObjectWithoutValidGeometry, opening.Id);
				result = null;
			}
			else
			{
				contourFromOpening.transformBy(this.MatWcsToPalette);
				MountingPartData mountingPartData = new MountingPartData
				{
					Contour = contourFromOpening,
					InstallationHeight = 0.0,
					ElementId = opening.Id,
					Height = part.thickness(),
					IsOpeningWithoutGeometry = true,
					Name = opening.Element.Name,
					Type = MountingPartData.Types.Opening,
					UniqueId = opening.UniqueId
				};
				result = mountingPartData;
			}
			return result;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00008058 File Offset: 0x00006258
		protected ItGeVector3d getViewDirection()
		{
			ItGeVector3d vec = -ItGeVector3d.kZAxis;
			return this.MatAssemblyToPalette.inverse() * vec;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00008088 File Offset: 0x00006288
		public bool CreateMachineDataPxml(AssemblyInstance assemblyInstance, RevitElement<Part> part, ItDocument cncDoc, CNCProjectData projectData)
		{
			this.InitializeFromAssembly(assemblyInstance);
			List<RevitElement<Part>> corePartsFromAssembly = this.GetCorePartsFromAssembly(assemblyInstance);
			ItDebug.assert(corePartsFromAssembly.isNotNull() && corePartsFromAssembly.Count > 0, "Unexpected parts count");
			return this.ExportPxml(assemblyInstance, corePartsFromAssembly, cncDoc, projectData);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000080D8 File Offset: 0x000062D8
		protected virtual bool ExportPxml(AssemblyInstance assemblyInstance, IEnumerable<RevitElement<Part>> partsList, ItDocument cncDoc, CNCProjectData projectData)
		{
			bool flag = false;
			partsList = ((partsList != null) ? partsList.ToList<RevitElement<Part>>() : null);
			bool flag2 = assemblyInstance.isNull() || partsList.none<RevitElement<Part>>();
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				Parameter parameter = assemblyInstance.getParameter(ItGlobals.PRODNUMBERPARAM, true, true);
				double num;
				ItGePoint3d startPoint;
				ItGeVector3d shiftingDirection;
				ItGeVector3d spanDirection;
				ItGeVector3d viewDirection;
				bool flag3 = this.exGetAssemblyData(assemblyInstance, out num, out startPoint, out shiftingDirection, out spanDirection, out viewDirection);
				bool flag4 = flag3 && parameter.isNotNull();
				if (flag4)
				{
					ItMachineDataBase.CNCElementData cncelementData = new ItMachineDataBase.CNCElementData();
					VoidsAndMountParts allMountingPartData = this.GetAllMountingPartData(assemblyInstance, partsList, cncelementData);
					ReinfSorter reinfSorter = new ReinfSorter(assemblyInstance, cncelementData, this.MatWcsToPalette, this.GetReinforcementFilter());
					reinfSorter.SortElements();
					Dictionary<RevitElement<Part>, ItSolid> mapPartSolid;
					flag = this.GetSolidsFromEachPartOfAssembly(assemblyInstance, allMountingPartData, partsList, out mapPartSolid);
					bool flag5 = !flag;
					if (flag5)
					{
						ItFailures.PostFailure(ItFailures.CAMObjectWithoutValidGeometry, assemblyInstance.Id);
						return false;
					}
					ItOrder itOrder = cncDoc.Orders.Last<ItOrder>();
					ItProduct itProduct = itOrder.productList.Last<ItProduct>();
					ItGeMatrix3d translateToOrigin = this.GetTranslateToOrigin(assemblyInstance, cncelementData, reinfSorter, false);
					bool flag6 = !itProduct.IsInitialized;
					if (flag6)
					{
						itProduct.Initialize(this, assemblyInstance, partsList, translateToOrigin, startPoint, spanDirection, shiftingDirection, parameter);
					}
					IEnumerable<ItGeBoundBlock3d> source = from part in partsList
					select part.getWorldExtents(false).transformBy(this.MatWcsToPalette);
					ItGeBoundBlock3d itGeBoundBlock3d = source.Aggregate(null, delegate(ItGeBoundBlock3d seed, ItGeBoundBlock3d box)
					{
						bool flag7 = seed == null;
						ItGeBoundBlock3d result2;
						if (flag7)
						{
							result2 = box;
						}
						else
						{
							seed.extend(box.minPoint);
							seed.extend(box.maxPoint);
							result2 = seed;
						}
						return result2;
					});
					string pxmlPartType = this.PxmlPartType;
					RevitElement<Part> revitElement = partsList.First<RevitElement<Part>>();
					Material material = revitElement.material();
					double layerUnitWeightInKgPerCubeFeet = ItMachineDataBase.GetLayerUnitWeightInKgPerCubeFeet(material, revitElement.Id);
					double netConcreteVolume = ItMachineDataBase.GetNetConcreteVolume(revitElement, reinfSorter);
					double totalArea = this.getTotalArea(assemblyInstance, new RevitElement<Part>[]
					{
						revitElement
					});
					double totalThicknessFeet = this.GetTotalThicknessFeet(assemblyInstance);
					ItSlab itSlab = new ItSlab
					{
						PartType = pxmlPartType,
						SlabArea = totalArea,
						SlabWeight = netConcreteVolume * layerUnitWeightInKgPerCubeFeet,
						MaxLength = itGeBoundBlock3d.maxPoint.x - itGeBoundBlock3d.minPoint.x,
						MaxWidth = itGeBoundBlock3d.maxPoint.y - itGeBoundBlock3d.minPoint.y,
						ProductionThickness = totalThicknessFeet,
						NetConcreteVolume = netConcreteVolume,
						Material = (material.Name ?? string.Empty),
						Density = layerUnitWeightInKgPerCubeFeet
					};
					itProduct.slabList.Add(itSlab);
					this.WriteAdditionalSlabFields(itSlab, assemblyInstance);
					flag = this.WriteLotsToSlab(itSlab, parameter.AsString(), mapPartSolid, viewDirection, cncelementData, reinfSorter);
					this.StoreMountingPartsData(cncelementData, itSlab);
					foreach (RevitElement<Part> part2 in partsList)
					{
						IEnumerable<ItSteel> steelElements = this.GetSteelElements(reinfSorter, part2);
						itSlab.steelList.AddRange(steelElements);
					}
					this.exMoveToOrigin(itProduct, assemblyInstance, translateToOrigin);
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000083D8 File Offset: 0x000065D8
		protected virtual string PxmlPartType
		{
			get
			{
				return "01";
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000083E0 File Offset: 0x000065E0
		private void exMoveToOrigin(ItProduct product, AssemblyInstance assembly, ItGeMatrix3d matMovetoOrigin)
		{
			foreach (ItSlab itSlab in product.slabList)
			{
				foreach (ItOutline itOutline in itSlab.outlineList)
				{
					itOutline.TranslateBy(matMovetoOrigin);
				}
				foreach (ItSteel itSteel in itSlab.steelList)
				{
					itSteel.TranslateBy(matMovetoOrigin);
				}
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000084C8 File Offset: 0x000066C8
		private bool WriteLotsToSlab(ItSlab slab, string productionNo, Dictionary<RevitElement<Part>, ItSolid> mapPartSolid, ItGeVector3d viewDirection, ItMachineDataBase.CNCElementData cncElementData, ReinfSorter reinfSorter)
		{
			bool result = false;
			foreach (KeyValuePair<RevitElement<Part>, ItSolid> keyValuePair in mapPartSolid)
			{
				RevitElement<Part> key = keyValuePair.Key;
				ItSolid value = keyValuePair.Value;
				List<ItGePolyline2d> list;
				bool flag = this.exGetPartData(key, value, viewDirection, out list);
				bool flag2 = !flag || list.none<ItGePolyline2d>();
				if (flag2)
				{
					ItFailures.PostFailure(ItFailures.CAMObjectWithoutValidGeometry, key.Id);
					return result;
				}
				ItGeRectangle itGeRectangle = list.extents();
				bool flag3 = itGeRectangle == null || itGeRectangle.area.Eq(0.0, -1.0);
				if (flag3)
				{
					ItFailures.PostFailure(ItFailures.CAMObjectWithoutValidGeometry, key.Id);
					return false;
				}
				double thickness = this.getThickness(key.getLocalExtents(false));
				ItOutline itOutline = new ItOutline
				{
					Name = key.shellType().ToString(),
					ConcreteQuality = slab.Material,
					Height = thickness,
					UnitWeight = slab.Density,
					type = "lot",
					ObjectID = (DbgModesContainer.Instance().CAMStablePXMLGuids ? ItMachineDataBase._testGuid : key.UniqueId),
					isMainOutline = true,
					Volume = slab.NetConcreteVolume
				};
				this.setZPositionOfContour(key, itOutline);
				foreach (ItGePolyline2d itGePolyline2d in list)
				{
					ItShape itShape = new ItShape
					{
						Cutout = false
					};
					List<ItGePoint3d> list2;
					bool flag4 = itGePolyline2d.getPoints(out list2) < 3;
					if (!flag4)
					{
						foreach (ItGePoint3d itGePoint3d in list2)
						{
							ItSVertex item = new ItSVertex
							{
								X = itGePoint3d.x,
								Y = itGePoint3d.y
							};
							itShape.sVertexList.Add(item);
						}
						itOutline.shapeList.Add(itShape);
					}
				}
				slab.outlineList.Add(itOutline);
				result = true;
			}
			return result;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000051E6 File Offset: 0x000033E6
		protected virtual void WriteAdditionalSlabFields(ItSlab slab, AssemblyInstance instance)
		{
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000879C File Offset: 0x0000699C
		private IEnumerable<ItSteel> GetSteelElements(ReinfSorter reinfSorter, RevitElement<Part> part)
		{
			List<ItSteel> list = new List<ItSteel>();
			IEnumerable<IGrouping<SteelGroup, SteelGroupElement>> enumerable = from @group in reinfSorter.GetSteelGroups()
			where @group.Key.HostPart.Id == part.Id
			select @group;
			ICollection<ExportReinfData> exportedElements = new List<ExportReinfData>();
			foreach (IGrouping<SteelGroup, SteelGroupElement> grouping in enumerable)
			{
				ItSteel item = grouping.Key.ToPXML(exportedElements);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00008838 File Offset: 0x00006A38
		protected virtual void setZPositionOfContour(RevitElement<Part> part, ItOutline outline)
		{
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00008840 File Offset: 0x00006A40
		private bool exGetAssemblyData(AssemblyInstance assemblyInstance, out double dHeight, out ItGePoint3d startPoint, out ItGeVector3d shiftingDirection, out ItGeVector3d spanDirection, out ItGeVector3d viewDirection)
		{
			startPoint = null;
			shiftingDirection = null;
			spanDirection = null;
			viewDirection = this.getViewDirection();
			bool flag = this.exGetAssemblyViewDirAndHeight(assemblyInstance, viewDirection, out dHeight);
			bool flag2 = flag;
			if (flag2)
			{
				ItGeMatrix3d mat = this.MatAssemblyToPalette.inverse();
				startPoint = ItGePoint3d.kOrigin.transformBy(mat);
				shiftingDirection = this.getShiftingDirection();
				shiftingDirection.transformBy(mat);
				spanDirection = this.getSpanDirection();
				spanDirection.transformBy(mat);
			}
			return flag;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000088BC File Offset: 0x00006ABC
		internal bool exGetAssemblyViewDirAndHeight(AssemblyInstance assembly, ItGeVector3d viewDirection, out double dHeight)
		{
			dHeight = 0.0;
			bool flag = assembly.isNull() || viewDirection.isNull();
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				dHeight = this.getAssemblyThickness(assembly.getLocalExtents());
				ItGeVector3d itGeVector3d = new ItGeVector3d(viewDirection);
				itGeVector3d.transformBy(assembly.ecs());
				result = true;
			}
			return result;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00008918 File Offset: 0x00006B18
		internal bool exGetPartData(RevitElement<Part> part, ItSolid rawPartSolid, ItGeVector3d viewDirection, out List<ItGePolyline2d> contourPart)
		{
			bool flag = false;
			contourPart = new List<ItGePolyline2d>();
			bool flag2 = part.isNull() || viewDirection.isNull();
			bool result;
			if (flag2)
			{
				result = flag;
			}
			else
			{
				ItGeVector3d itGeVector3d = new ItGeVector3d(viewDirection);
				itGeVector3d.transformBy(part.ecs(false));
				ItGePolyline2d itGePolyline2d = null;
				List<ItGePolyline2d> list = rawPartSolid.asPolyList(null);
				bool flag3 = list.any<ItGePolyline2d>();
				if (flag3)
				{
					itGePolyline2d = list.getSingleKontur(itGeVector3d, true, false, null, null);
				}
				bool flag4 = itGePolyline2d.isNotNull() && itGePolyline2d.len.Gt(0.0, -1.0);
				if (flag4)
				{
					contourPart.Add(itGePolyline2d);
					contourPart.transformBy(this.MatWcsToPalette);
					flag = true;
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x060000CB RID: 203
		protected abstract bool IsMountPartOfThisShell(RevitElement<FamilyInstance> mountingPart, RevitElement<Part> revitElement);

		// Token: 0x060000CC RID: 204 RVA: 0x000089DC File Offset: 0x00006BDC
		private void StoreMountingPartsData(ItMachineDataBase.CNCElementData data, ItSlab slab)
		{
			foreach (MountingPartData mountingPartData in data.MountingParts)
			{
				bool isOpeningWithoutGeometry = mountingPartData.IsOpeningWithoutGeometry;
				if (isOpeningWithoutGeometry)
				{
					this.StoreVoidData(mountingPartData, slab);
				}
				else
				{
					this.StoreMountingPartData(mountingPartData, slab);
				}
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00008A50 File Offset: 0x00006C50
		private void StoreMountingPartData(MountingPartData data, ItSlab slab)
		{
			bool flag = data.Contour.Count < 3;
			if (flag)
			{
				ItFailures.PostFailure(ItFailures.CAMObjectWithoutValidGeometry, data.ElementId);
			}
			else
			{
				string mountPartType = 0.ToString("D2");
				ItOutline itOutline = new ItOutline
				{
					Name = data.Name,
					MountPartType = mountPartType,
					type = "mountpart",
					Height = data.Height,
					MountingInstruction = 0,
					ObjectID = (DbgModesContainer.Instance().CAMStablePXMLGuids ? ItMachineDataBase._testGuid : data.UniqueId),
					Z = data.InstallationHeight
				};
				ItShape itShape = new ItShape
				{
					Cutout = false
				};
				foreach (ItGePoint3d itGePoint3d in data.Contour)
				{
					ItSVertex item = new ItSVertex
					{
						X = itGePoint3d.x,
						Y = itGePoint3d.y
					};
					itShape.sVertexList.Add(item);
				}
				itOutline.shapeList.Add(itShape);
				slab.outlineList.Add(itOutline);
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00008BB8 File Offset: 0x00006DB8
		private void StoreVoidData(MountingPartData data, ItSlab slab)
		{
			ItOutline itOutline = slab.outlineList.FirstOrDefault((ItOutline outline) => outline.isMainOutline);
			bool flag = itOutline == null;
			if (!flag)
			{
				ItShape itShape = new ItShape
				{
					Cutout = true
				};
				foreach (ItGePoint3d itGePoint3d in data.Contour)
				{
					ItSVertex item = new ItSVertex
					{
						X = itGePoint3d.x,
						Y = itGePoint3d.y
					};
					itShape.sVertexList.Add(item);
				}
				itOutline.shapeList.Add(itShape);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00008C98 File Offset: 0x00006E98
		private bool GetSolidsFromEachPartOfAssembly(AssemblyInstance instance, VoidsAndMountParts voidsData, IEnumerable<RevitElement<Part>> parts, out Dictionary<RevitElement<Part>, ItSolid> mapPartSolid)
		{
			List<ElementId> list = (from opening in voidsData.openings
			select opening.Id).ToList<ElementId>();
			list.AddRange(from doorWin in voidsData.doorsAndWindows
			select doorWin.Id);
			list.AddRange(from mountingPart in voidsData.mountingParts
			select mountingPart.Id);
			return instance.getSolidsFromEachPartOfAssembly(out mapPartSolid, list, parts);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00008D48 File Offset: 0x00006F48
		protected static List<ItGePoint3d> getContourFromOpening(RevitElement<Opening> opening, RevitElement<Part> part, ItGeVector3d viewDirection)
		{
			Solid solidFromPart = opening.To<Element>().getSolidFromPart(part);
			bool flag = solidFromPart == null || solidFromPart.Volume.Eq(0.0, -1.0);
			List<ItGePoint3d> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<ItGePoint3d> list = (from p in solidFromPart.getContour(viewDirection)
				select new ItGePoint3d(p)).ToList<ItGePoint3d>();
				result = list;
			}
			return result;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00008DD0 File Offset: 0x00006FD0
		protected static List<ItGePoint3d> getContourFromElement(RevitElement<Element> element, RevitElement<Part> part, ItGeMatrix3d ecsToWcs, ItGeVector3d viewDirection, out bool hasGeometry)
		{
			ItSolid solid = ItSolid.getSolid(element, ecsToWcs, false);
			bool flag = solid != null;
			Solid solid2;
			if (flag)
			{
				hasGeometry = true;
				solid2 = solid.Solid;
			}
			else
			{
				hasGeometry = false;
				solid2 = element.getSolidFromPart(part);
			}
			bool flag2 = solid2 == null || solid2.Volume.Eq(0.0, -1.0);
			List<ItGePoint3d> result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				List<ItGePoint3d> list = (from p in solid2.getContour(viewDirection)
				select new ItGePoint3d(p)).ToList<ItGePoint3d>();
				result = list;
			}
			return result;
		}

		// Token: 0x0400003C RID: 60
		private static readonly string _lclMsgProjNoError = "msgProjectNoError".Localise();

		// Token: 0x0400003D RID: 61
		private static readonly string _lclCaptionProjNoError = "captionProjectNoError".Localise();

		// Token: 0x0400003E RID: 62
		private const string _strErrorUnsupportedPartType = "Unsupported part type";

		// Token: 0x0400003F RID: 63
		private const string _strProductionNumber = "Production number: {0}.";

		// Token: 0x04000040 RID: 64
		private const string _strAssemblyTypeId = "Assembly type ID {0}";

		// Token: 0x04000041 RID: 65
		public const int UnitechnikMaxSegmentCoordLength = 99999;

		// Token: 0x04000042 RID: 66
		private const double DefaultConreteUnitWeightInFeet = 67.960431820800011;

		// Token: 0x04000043 RID: 67
		private static string _testGuid = "8F7199D6-9EEF-4988-ACAA-A5B5826BA053";

		// Token: 0x04000044 RID: 68
		private readonly ICamExportIntOptions _options;

		// Token: 0x02000053 RID: 83
		protected internal class CNCElementData
		{
			// Token: 0x17000249 RID: 585
			// (get) Token: 0x06000577 RID: 1399 RVA: 0x000157DE File Offset: 0x000139DE
			public List<ItGePoint3d> Contour { get; } = new List<ItGePoint3d>();

			// Token: 0x1700024A RID: 586
			// (get) Token: 0x06000578 RID: 1400 RVA: 0x000157E6 File Offset: 0x000139E6
			public List<MountingPartData> MountingParts { get; } = new List<MountingPartData>();

			// Token: 0x1700024B RID: 587
			// (get) Token: 0x06000579 RID: 1401 RVA: 0x000157EE File Offset: 0x000139EE
			public List<ItBracedGirderData> Girders { get; } = new List<ItBracedGirderData>();

			// Token: 0x1700024C RID: 588
			// (get) Token: 0x0600057A RID: 1402 RVA: 0x000157F6 File Offset: 0x000139F6
			// (set) Token: 0x0600057B RID: 1403 RVA: 0x000157FE File Offset: 0x000139FE
			public double Height { get; set; }
		}
	}
}
