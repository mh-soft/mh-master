using System;
using System.Collections.Generic;
using System.Linq;
using Hao.Export.Geometry;
using Hao.Export.Reinforcement;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;

namespace Hao.Export.MachineData
{
	// Token: 0x02000027 RID: 39
	public class SteelGroupElementRebar : SteelGroupElementRebarBase
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000C408 File Offset: 0x0000A608
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000C410 File Offset: 0x0000A610
		protected RevitElement<Rebar> Rebar { get; set; }

		// Token: 0x060001D1 RID: 465 RVA: 0x0000ACDE File Offset: 0x00008EDE
		internal SteelGroupElementRebar(ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette) : base(cncElementData, matWcsToPalette)
		{
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000C41C File Offset: 0x0000A61C
		internal new static SteelGroupElementRebar GetInstance(RevitElement<Rebar> rebar, ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette, List<SteelGroupElement> existingElements)
		{
			bool flag = rebar.IsInvalid() || !SteelGroupElementRebar.CanBeExported(rebar);
			SteelGroupElementRebar result;
			if (flag)
			{
				result = null;
			}
			else
			{
				SteelGroupElementRebar steelGroupElementRebar = new SteelGroupElementRebar(cncElementData, matWcsToPalette);
				steelGroupElementRebar.Rebar = rebar;
				steelGroupElementRebar.Host = rebar.Element.getHostingPart();
				bool flag2 = steelGroupElementRebar.Host == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					steelGroupElementRebar.RebarBarType = (rebar.Document.GetElement(rebar.Element.GetTypeId()) as RebarBarType);
					steelGroupElementRebar.Data = (rebar.load<ItReinforcement>(null, false) ?? SteelGroupElement.GetEmptyData(steelGroupElementRebar));
					bool flag3;
					steelGroupElementRebar.IronSetsSchematic = rebar.Element.getIronSets(out flag3, RebarMode.Schematic, false);
					steelGroupElementRebar.IronSetsSchematicAreInvalid = flag3;
					steelGroupElementRebar.IronSetsRealistic = rebar.Element.getIronSets(out flag3, RebarMode.Realistic, false);
					steelGroupElementRebar.IronSetsRealisticAreInvalid = flag3;
					steelGroupElementRebar.TransformToPalette();
					steelGroupElementRebar.SetMinMaxZ(steelGroupElementRebar.IronSetsRealistic);
					steelGroupElementRebar.SetGroup(existingElements);
					result = steelGroupElementRebar;
				}
			}
			return result;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000C52C File Offset: 0x0000A72C
		private static bool CanBeExported(RevitElement<Rebar> rebar)
		{
			bool flag = !rebar.Element.IsRebarShapeDriven();
			bool result;
			if (flag)
			{
				ElementId[] elementIdsForAdd = new ElementId[]
				{
					rebar.Id,
					rebar.AssemblyInstanceId
				};
				ItFailures.postFailure(ItFailures.InvalidRebarTypeInCamExport, elementIdsForAdd);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000C57C File Offset: 0x0000A77C
		private void SetGroup(List<SteelGroupElement> existingElements)
		{
			bool flag = base.Data.LayerPosition == PosReinfLayer.Undefined && base.Data.reinforcementType != ReinforcementType.LintelReinf;
			if (flag)
			{
				bool flag2 = this.IsValidReinfType() || !this.IsStirrup();
				if (flag2)
				{
					IEnumerable<SteelGroupElement> source = (from e in existingElements
					where e is SteelGroupElementAreaReinf
					select e).Union(from e in existingElements
					where e is SteelGroupElementMeshCFS
					select e);
					IEnumerable<SteelGroupElement> source2 = from e in source
					where e.TestZPosition(base.MinZ, base.MaxZ)
					select e;
					List<SteelGroup> list = (from elem in source2
					select elem.Group).Distinct<SteelGroup>().ToList<SteelGroup>();
					bool flag3 = list.Count > 1;
					if (flag3)
					{
						ItFailures.PostFailure(ItFailures.CAMAmbiguousRebarParentArea, this.Rebar.Id);
					}
					SteelGroup steelGroup = list.FirstOrDefault((SteelGroup group) => group.Elements.OfType<SteelGroupElementMeshCFS>().Any<SteelGroupElementMeshCFS>());
					bool flag4 = steelGroup != null;
					if (flag4)
					{
						base.Data.LayerPosition = steelGroup.ReinfLayer;
					}
					else
					{
						SteelGroup steelGroup2 = list.FirstOrDefault((SteelGroup group) => group.Elements.OfType<SteelGroupElementAreaReinf>().Any<SteelGroupElementAreaReinf>());
						bool flag5 = steelGroup2 != null;
						if (flag5)
						{
							base.Data.LayerPosition = steelGroup2.ReinfLayer;
						}
						else
						{
							base.Data.LayerPosition = PosReinfLayer.Undefined;
						}
					}
				}
				else
				{
					base.Data.LayerPosition = PosReinfLayer.Undefined;
					base.Data.reinforcementType = ReinforcementType.NotDefined;
				}
			}
			base.SetGroup();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000C760 File Offset: 0x0000A960
		private bool IsStirrup()
		{
			RebarShape rebarShape = this.Rebar.Document.GetElement(this.Rebar.Element.GetShapeId()) as RebarShape;
			bool flag = rebarShape == null;
			return flag || rebarShape.RebarStyle == (RebarStyle)1;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		private bool IsValidReinfType()
		{
			return base.Data.reinforcementType == ReinforcementType.AreaReinfSlab || base.Data.reinforcementType == ReinforcementType.AreaReinfWall || base.Data.reinforcementType == ReinforcementType.EdgeReinfWall || base.Data.reinforcementType == ReinforcementType.OpeningReinf || base.Data.reinforcementType == ReinforcementType.DoorReinf;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000C80C File Offset: 0x0000AA0C
		internal override void WriteToUnitechnik(ICollection<ExportReinfData> exportedElements)
		{
			bool flag = this.Rebar == null;
			if (!flag)
			{
				bool flag2 = base.IronSetsSchematic.none<List<ItGeCurve3d>>();
				if (flag2)
				{
					ItFailures.PostFailure(ItFailures.CAMInvalidRebarShape, this.Rebar.Id);
					exportedElements.Add(new ExportReinfData
					{
						Id = this.Rebar.Id,
						ExportedAs = ExportReinfData.ExportType.AsFailure
					});
				}
				else
				{
					bool ironSetsSchematicAreInvalid = base.IronSetsSchematicAreInvalid;
					if (ironSetsSchematicAreInvalid)
					{
						ItFailures.PostFailure(ItFailures.CAMUnsupportedRebarShapeExportedAsStraightBar, this.Rebar.Id);
					}
					bool flag3 = SteelGroupElementRebar.IsMultiplanar(this.Rebar);
					if (flag3)
					{
						this.UnitechnikWriteRebarAsMountPart();
						exportedElements.Add(new ExportReinfData
						{
							Id = this.Rebar.Id,
							ExportedAs = ExportReinfData.ExportType.AsMountPart
						});
					}
					else
					{
						bool flag4 = base.IronSetsSchematic.any((List<ItGeCurve3d> ls) => ls.OfType<ItGeCircArc3d>().any<ItGeCircArc3d>());
						if (flag4)
						{
							this.UnitechnikWriteRebarAsMountPart();
							exportedElements.Add(new ExportReinfData
							{
								Id = this.Rebar.Id,
								ExportedAs = ExportReinfData.ExportType.AsMountPart
							});
						}
						else
						{
							bool flag5 = SteelGroupElementRebar.IsStraightBarsInXYPlane(base.IronSetsSchematic);
							if (flag5)
							{
								this.UnitechnikWriteRebars();
								exportedElements.Add(new ExportReinfData
								{
									Id = this.Rebar.Id,
									ExportedAs = ExportReinfData.ExportType.AsRodstock
								});
							}
							else
							{
								RebarShape rebarShape = this.Rebar.Document.GetElement(this.Rebar.Element.GetShapeId()) as RebarShape;
								bool flag6 = rebarShape.RebarStyle ==(RebarStyle) 1;
								if (flag6)
								{
									this.UnitechnikWriteRebarAsMountPart();
									exportedElements.Add(new ExportReinfData
									{
										Id = this.Rebar.Id,
										ExportedAs = ExportReinfData.ExportType.AsMountPart
									});
								}
								else
								{
									bool flag7 = SteelGroupElementRebar.IsValidBendingFormForUnitechnikFreeForm(base.IronSetsSchematic);
									if (flag7)
									{
										this.UnitechnikWriteRebarAsFreeForm();
										exportedElements.Add(new ExportReinfData
										{
											Id = this.Rebar.Id,
											ExportedAs = ExportReinfData.ExportType.AsFreeForm
										});
									}
									else
									{
										this.UnitechnikWriteRebarAsMountPart();
										exportedElements.Add(new ExportReinfData
										{
											Id = this.Rebar.Id,
											ExportedAs = ExportReinfData.ExportType.AsMountPart
										});
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000CA88 File Offset: 0x0000AC88
		private void UnitechnikWriteRebarAsFreeForm()
		{
			ItDebug.assert(base.IronSetsSchematic.any<List<ItGeCurve3d>>() && base.IronSetsSchematic[0].Count > 1, "Do not call this method for anything but bending forms.");
			ItUniWrapperImpl.RodstockData data = default(ItUniWrapperImpl.RodstockData);
			List<ItGeCurve3d> list = base.IronSetsSchematic[0];
			ItGeVector3d normal;
			short unitechnikAngleOfFreeForm = base.GetUnitechnikAngleOfFreeForm(list[0], out normal);
			int num;
			double dValue;
			this.GetSpacingAndCountForUnitechnik(unitechnikAngleOfFreeForm, out num, out dValue);
			int freeFormLength = SteelGroupElementRebar.GetFreeFormLength(list);
			ItGePoint3d startPoint = base.GetStartPoint(base.IronSetsSchematic);
			data.length = (uint)freeFormLength;
			data.startAngle = unitechnikAngleOfFreeForm;
			data.artNr = string.Empty;
			data.autoProd = 0;
			data.diameter = (ushort)CNCDataBase.convertToMM(base.RebarBarType.BarDiameter);
			data.flexFormNumber = 0;
			data.formType = 2;
			data.hasSpacers = 0;
			data.hasWielding = 0;
			data.installationHeight = (uint)CNCDataBase.convertToMM(startPoint.z);
			data.number = (ushort)num;
			data.pitch = CNCDataBase.convertToMM(dValue);
			data.pitchSpacer = 0u;
			data.startingPointSpacer = 0u;
			data.reinfType = (byte)base.CNCReinfTypeValue;
			data.steelQuality = SteelGroupElementRebarBase.GetSteelQuality(base.RebarBarType);
			data.xcoord = CNCDataBase.convertToMM(startPoint.x);
			data.ycoord = CNCDataBase.convertToMM(startPoint.y);
			List<ItUniWrapperImpl.FreeFormSegment> list2 = new List<ItUniWrapperImpl.FreeFormSegment>();
			int num2;
			for (int i = 0; i < list.Count; i = num2 + 1)
			{
				ItGeCurve3d itGeCurve3d = list[i];
				ItGeCurve3d nextCurve = list.ElementAtOrDefault(i + 1);
				short unitechnikAngleOfNextSegment = this.GetUnitechnikAngleOfNextSegment(itGeCurve3d, nextCurve, normal);
				list2.Add(new ItUniWrapperImpl.FreeFormSegment
				{
					length = (uint)CNCDataBase.convertToMM(itGeCurve3d.len),
					angle = unitechnikAngleOfNextSegment
				});
				num2 = i;
			}
			ItUniWrapperImpl.AddFreeFormRodstock(data, list2);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000CC6C File Offset: 0x0000AE6C
		private short GetUnitechnikAngleOfNextSegment(ItGeCurve3d thisCurve, ItGeCurve3d nextCurve, ItGeVector3d normal)
		{
			bool flag = nextCurve == null;
			short result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				ItGeVector3d directionFromCurve = SteelGroupElementRebarBase.GetDirectionFromCurve(thisCurve);
				ItGeVector3d directionFromCurve2 = SteelGroupElementRebarBase.GetDirectionFromCurve(nextCurve);
				double num = directionFromCurve.angleTo(directionFromCurve2, normal);
				num = SteelGroupElement.FixAngleRange(num, false);
				num = ItAngle.toDegree(num);
				short num2 = (short)Math.Round(num, 0);
				result = num2;
			}
			return result;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000CCC4 File Offset: 0x0000AEC4
		private static int GetFreeFormLength(List<ItGeCurve3d> firstSet)
		{
			double dValue = firstSet.Sum((ItGeCurve3d curve) => curve.len);
			return CNCDataBase.convertToMM(dValue);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000CD04 File Offset: 0x0000AF04
		private void UnitechnikWriteRebars()
		{
			bool flag = SteelGroupElementRebar.HasEqualIrons(base.IronSetsSchematic);
			bool flag2 = this.HasZDistribution();
			bool flag3 = base.IsMainAxis();
			bool flag4 = flag && !flag2 && flag3;
			if (flag4)
			{
				base.UnitechnikAddStraightBars(base.IronSetsSchematic, this.Rebar.Id);
			}
			else
			{
				foreach (List<ItGeCurve3d> item in base.IronSetsSchematic)
				{
					List<List<ItGeCurve3d>> arrayIrons = new List<List<ItGeCurve3d>>
					{
						item
					};
					base.UnitechnikAddStraightBars(arrayIrons, this.Rebar.Id);
				}
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000CDC8 File Offset: 0x0000AFC8
		private bool HasZDistribution()
		{
			bool result = false;
			bool flag = this.Rebar.Element.LayoutRule > 0;
			if (flag)
			{
				RebarShapeDrivenAccessor rebarShapeDrivenAccessor = null;
				bool flag2 = !this.Rebar.Element.GetShapeAccessor(out rebarShapeDrivenAccessor);
				if (flag2)
				{
					return result;
				}
				ItGeVector3d itGeVector3d = rebarShapeDrivenAccessor.GetDistributionPath().Direction.asVector();
				itGeVector3d.transformBy(base.MatWcsToPalette);
				double dX = itGeVector3d.dotProduct(ItGeVector3d.kZAxis);
				bool flag3 = dX.Ne(0.0, -1.0);
				if (flag3)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000CE68 File Offset: 0x0000B068
		private static bool HasEqualIrons(List<List<ItGeCurve3d>> arrayIrons)
		{
			bool result = true;
			bool flag = arrayIrons.Count > 1;
			if (flag)
			{
				double len1 = arrayIrons[0].Sum((ItGeCurve3d c) => c.len);
				result = arrayIrons.All((List<ItGeCurve3d> list) => list.Sum((ItGeCurve3d c) => c.len).Eq(len1, -1.0));
			}
			return result;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000CED8 File Offset: 0x0000B0D8
		private void UnitechnikWriteRebarAsMountPart()
		{
			bool flag = base.IronSetsSchematic.none<List<ItGeCurve3d>>();
			if (!flag)
			{
				ItGeBoundBlock3d boundBlockOfIrons = this.GetBoundBlockOfIrons();
				bool flag2 = boundBlockOfIrons.isNull();
				if (!flag2)
				{
					List<ItGePoint3d> list = new List<ItGePoint3d>();
					ItGePoint3d item = new ItGePoint3d(boundBlockOfIrons.minPoint);
					ItGePoint3d item2 = new ItGePoint3d(boundBlockOfIrons.minPoint.x, boundBlockOfIrons.maxPoint.y, boundBlockOfIrons.minPoint.z, null);
					ItGePoint3d item3 = new ItGePoint3d(boundBlockOfIrons.maxPoint.x, boundBlockOfIrons.maxPoint.y, boundBlockOfIrons.minPoint.z, null);
					ItGePoint3d item4 = new ItGePoint3d(boundBlockOfIrons.maxPoint.x, boundBlockOfIrons.minPoint.y, boundBlockOfIrons.minPoint.z, null);
					list.Add(item);
					list.Add(item2);
					list.Add(item3);
					list.Add(item4);
					double height = boundBlockOfIrons.maxPoint.z - boundBlockOfIrons.minPoint.z + base.RebarBarType.BarDiameter;
					double installationHeight = boundBlockOfIrons.minPoint.z - base.RebarBarType.BarDiameter / 2.0;
					MountingPartData item5 = new MountingPartData
					{
						Contour = list,
						Height = height,
						InstallationHeight = installationHeight,
						Name = this.Rebar.Element.Name,
						Type = MountingPartData.Types.Rebar,
						ElementId = this.Rebar.Id,
						IsOpeningWithoutGeometry = false,
						UniqueId = this.Rebar.UniqueId
					};
					base.CNCElementData.MountingParts.Add(item5);
				}
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000D098 File Offset: 0x0000B298
		private ItGeBoundBlock3d GetBoundBlockOfIrons()
		{
			ItGeBoundBlock3d itGeBoundBlock3d = null;
			foreach (List<ItGeCurve3d> list in base.IronSetsSchematic)
			{
				foreach (ItGeCurve3d itGeCurve3d in list)
				{
					ItGeBoundBlock3d itGeBoundBlock3d2 = itGeCurve3d.orthoBoundBlock();
					bool flag = itGeBoundBlock3d == null;
					if (flag)
					{
						itGeBoundBlock3d = itGeBoundBlock3d2;
					}
					else
					{
						itGeBoundBlock3d.extend(itGeBoundBlock3d2.minPoint).extend(itGeBoundBlock3d2.maxPoint);
					}
				}
			}
			return itGeBoundBlock3d;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000D164 File Offset: 0x0000B364
		private static bool IsValidBendingFormForUnitechnikFreeForm(List<List<ItGeCurve3d>> arrayIrons)
		{
			bool flag = arrayIrons.any<List<ItGeCurve3d>>();
			bool flag2 = !flag;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				ItGeCurve3d curve;
				flag = arrayIrons.All(delegate(List<ItGeCurve3d> curves)
				{
					bool result2=false;
					if (curves.any<ItGeCurve3d>())
					{
					//	result2 = curves.All((ItGeCurve3d curve) => curve is ItGeLineSeg3d);
					}
					else
					{
						result2 = false;
					}
					return result2;
				});
				bool flag3 = !flag;
				if (flag3)
				{
					result = false;
				}
				else
				{
					flag = !SteelGroupElementRebar.IsSingleSegment(arrayIrons);
					bool flag4 = !flag;
					if (flag4)
					{
						result = false;
					}
					else
					{
						bool flag5 = arrayIrons.Count > 1;
						if (flag5)
						{
							//SteelGroupElementRebar.<>c__DisplayClass19_1 CS$<>8__locals1 = new SteelGroupElementRebar.<>c__DisplayClass19_1();
							//List<List<double>> list4 = (from curves in arrayIrons
							//select (from curve in curves
							//select curve.len).ToList<double>()).ToList<List<double>>();
							//CS$<>8__locals1.firstLengths = list4[0];
							//int j;
							//int k;
							//for (k = 0; k < CS$<>8__locals1.firstLengths.Count; k = j + 1)
							//{
							//	flag = list4.All((List<double> list) => list[k].Eq(CS$<>8__locals1.firstLengths[k], -1.0));
							//	bool flag6 = !flag;
							//	if (flag6)
							//	{
							//		return false;
							//	}
							//	j = k;
							//}
							//List<List<ItGeVector3d>> list2 = (from curves in arrayIrons
							//select curves.Select(new Func<ItGeCurve3d, ItGeVector3d>(SteelGroupElementRebarBase.GetDirectionFromCurve)).ToList<ItGeVector3d>()).ToList<List<ItGeVector3d>>();
							//CS$<>8__locals1.firstDirections = list2[0];
							//int i;
							//for (i = 0; i < CS$<>8__locals1.firstDirections.Count; i = j + 1)
							//{
							//	flag = list2.All((List<ItGeVector3d> list) => list[i].isCodirectionalTo(CS$<>8__locals1.firstDirections[i], null));
							//	bool flag7 = !flag;
							//	if (flag7)
							//	{
							//		return false;
							//	}
							//	j = i;
							//}
						}
						ItGeVector3d itGeVector3d = null;
						itGeVector3d = SteelGroupElementRebarBase.GetNormalFromFirstTwoSegments(arrayIrons.First<List<ItGeCurve3d>>(), ItGeVector3d.kZAxis);
						flag = itGeVector3d.z.Eq(0.0, -1.0);
						bool flag8 = !flag;
						if (flag8)
						{
							result = false;
						}
						else
						{
							foreach (List<ItGeCurve3d> list3 in arrayIrons)
							{
								ItGeCurve3d firstCurve = list3[0];
								foreach (ItGeCurve3d itGeCurve3d in list3)
								{
									ItGeCurve3d itGeCurve3d2 = itGeCurve3d;
									ItGeVector3d normalFromTwoSegments = SteelGroupElementRebarBase.GetNormalFromTwoSegments(firstCurve, itGeCurve3d2);
									flag = (normalFromTwoSegments == null || normalFromTwoSegments.isParallelTo(itGeVector3d, null));
									bool flag9 = !flag;
									if (flag9)
									{
										return false;
									}
									firstCurve = itGeCurve3d2;
								}
							}
							curve = arrayIrons[0][0];
							ItGeVector3d directionFromCurve = SteelGroupElementRebarBase.GetDirectionFromCurve(curve);
							bool flag10 = directionFromCurve.dotProduct(ItGeVector3d.kZAxis).Ne(0.0, -1.0);
							if (flag10)
							{
								ItGeCurve3d curve2 = arrayIrons[0].Last<ItGeCurve3d>();
								directionFromCurve = SteelGroupElementRebarBase.GetDirectionFromCurve(curve2);
								bool flag11 = directionFromCurve.dotProduct(ItGeVector3d.kZAxis).Ne(0.0, -1.0);
								if (flag11)
								{
									return false;
								}
								arrayIrons.ForEach(delegate(List<ItGeCurve3d> list)
								{
									list.Reverse();
								});
								arrayIrons.ForEach(delegate(List<ItGeCurve3d> list)
								{
									//list.ForEach(delegate(ItGeCurve3d curve)
									//{
									//	curve.reverseParam();
									//});
								});
							}
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000D544 File Offset: 0x0000B744
		private static bool IsSingleSegment(List<List<ItGeCurve3d>> arrayIrons)
		{
			return arrayIrons[0].Count == 1;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000D568 File Offset: 0x0000B768
		private static bool IsMultiplanar(RevitElement<Rebar> rebar)
		{
			bool result;
			try
			{
				RebarShapeDrivenAccessor rebarShapeDrivenAccessor = null;
				bool flag = !rebar.Element.GetShapeAccessor(out rebarShapeDrivenAccessor);
				if (flag)
				{
					result = false;
				}
				else
				{
					double multiplanarDepth = rebarShapeDrivenAccessor.MultiplanarDepth;
					result = multiplanarDepth.Ne(0.0, -1.0);
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000D5CC File Offset: 0x0000B7CC
		private static bool IsStraightBarsInXYPlane(List<List<ItGeCurve3d>> arrayIrons)
		{
			List<ItGeCurve3d> list = arrayIrons.First<List<ItGeCurve3d>>();
			bool flag = list.none<ItGeCurve3d>();
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = list.Count > 1;
				result = (!flag2 && SteelGroupElementRebar.IsSingleIronInXYPlane(list));
			}
			return result;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000D60C File Offset: 0x0000B80C
		private static bool IsSingleIronInXYPlane(List<ItGeCurve3d> firstList)
		{
			ItGeVector3d directionFromCurve = SteelGroupElementRebarBase.GetDirectionFromCurve(firstList[0]);
			ItGeVector3d refVec = directionFromCurve.crossProduct(ItGeVector3d.kZAxis);
			ItGeVector3d vec = directionFromCurve.orthoProject(ItGeVector3d.kZAxis);
			double a = directionFromCurve.angleTo(vec, refVec);
			return a.Eq(0.0, -1.0);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000D668 File Offset: 0x0000B868
		protected override ItGeVector3d GetRebarElementNormal()
		{
			RebarShapeDrivenAccessor rebarShapeDrivenAccessor = null;
			bool flag = !this.Rebar.Element.GetShapeAccessor(out rebarShapeDrivenAccessor);
			ItGeVector3d result;
			if (flag)
			{
				result = ItGeVector3d.kOrigin;
			}
			else
			{
				ItGeVector3d itGeVector3d = rebarShapeDrivenAccessor.Normal.asVector();
				itGeVector3d.transformBy(base.MatWcsToPalette);
				result = itGeVector3d;
			}
			return result;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000D6B8 File Offset: 0x0000B8B8
		protected override void SetReinforcementTypeForFloor(int layerIndexInZDir)
		{
			bool flag = base.Data.LayerPosition == PosReinfLayer.Bottom;
			if (flag)
			{
				base.CNCReinfType = SteelGroupElement.CNCReinforcementType.Other;
			}
			else
			{
				bool flag2 = base.Data.LayerPosition == PosReinfLayer.Top;
				if (flag2)
				{
					base.CNCReinfType = SteelGroupElement.CNCReinforcementType.UpperOther;
				}
				else
				{
					base.SetReinforcementTypeFromZ();
				}
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000D70C File Offset: 0x0000B90C
		protected override void SetReinforcementTypeForWall(int layerIndexInZDir)
		{
			bool flag = base.IsLowerReinfInWall();
			if (flag)
			{
				base.CNCReinfType = SteelGroupElement.CNCReinforcementType.Other;
			}
			else
			{
				bool flag2 = base.IsUpperReinfInWall();
				if (flag2)
				{
					base.CNCReinfType = SteelGroupElement.CNCReinforcementType.UpperOther;
				}
				else
				{
					bool flag3 = base.Data.LayerPosition == PosReinfLayer.Center;
					if (flag3)
					{
						ItDebug.assert(false, "Reinforcement in center position. Autodesk must make a decision how this should be handled. For now, geometrically added to lower or upper reinforcement.");
						base.SetReinforcementTypeFromZ();
					}
					else
					{
						base.SetReinforcementTypeFromZ();
					}
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000D778 File Offset: 0x0000B978
		protected override ElementId RebarElementId
		{
			get
			{
				RevitElement<Rebar> rebar = this.Rebar;
				return (rebar != null) ? rebar.Id : null;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000D78C File Offset: 0x0000B98C
		protected override void GetSpacingAndCountForUnitechnik(short angle, out int ironCount, out double ironSpacing)
		{
			ironSpacing = 0.0;
			ironCount = 1;
			bool flag = this.Rebar.Element.LayoutRule > 0;
			if (flag)
			{
				ironSpacing = this.GetActualSpacingForUnitechnik(this.Rebar, angle);
				ironCount = this.Rebar.Element.Quantity;
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000D7E4 File Offset: 0x0000B9E4
		protected double GetActualSpacingForUnitechnik(RevitElement<Rebar> rebar, short angle)
		{
			RebarShapeDrivenAccessor rebarShapeDrivenAccessor = null;
			bool flag = !rebar.Element.GetShapeAccessor(out rebarShapeDrivenAccessor);
			double result;
			if (flag)
			{
				result = 0.0;
			}
			else
			{
				int numberOfBarPositions = rebar.Element.NumberOfBarPositions;
				bool flag2 = numberOfBarPositions < 2;
				if (flag2)
				{
					result = 0.0;
				}
				else
				{
					bool flag3 = rebar.Element.DoesBarExistAtPosition(0);
					bool flag4 = !flag3 && (numberOfBarPositions < 3 || !rebar.Element.DoesBarExistAtPosition(2));
					if (flag4)
					{
						result = 0.0;
					}
					else
					{
						Transform barPositionTransform = rebarShapeDrivenAccessor.GetBarPositionTransform(flag3 ? 0 : 1);
						Transform barPositionTransform2 = rebarShapeDrivenAccessor.GetBarPositionTransform(flag3 ? 1 : 2);
						result = base.GetActualSpacingForUnitechnik(barPositionTransform, barPositionTransform2, angle);
					}
				}
			}
			return result;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		public override double GetSteelVolumeInPart(RevitElement<Part> part, ItSolid partSolid)
		{
			Rebar element = this.Rebar.Element;
			double radius = base.RebarBarType.BarDiameter / 2.0;
			int num;
			int num2;
			SteelGroupElementRebar.GetRebarIndices(element, out num, out num2);
			double num3 = 0.0;
			int num4;
			for (int i = num; i < num2; i = num4 + 1)
			{
				List<Curve> centerLineCurvesForVolumeCalculation = SteelGroupElementRebar.GetCenterLineCurvesForVolumeCalculation(this.Rebar, i);
				List<CurveLoop> sweepProfile = SteelGroupElementRebar.GetSweepProfile(centerLineCurvesForVolumeCalculation, radius);
				bool flag = sweepProfile.none<CurveLoop>();
				if (!flag)
				{
					Solid solid = SteelGroupElementRebar.GetSolid(centerLineCurvesForVolumeCalculation, sweepProfile);
					bool flag2 = solid == null;
					if (!flag2)
					{
						Solid solid2 = solid.intersect(partSolid.Solid);
						bool flag3 = solid2 == null || solid2.Volume.Lt(0.0, -1.0);
						if (!flag3)
						{
							num3 += solid2.Volume;
						}
					}
				}
				num4 = i;
			}
			return num3;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000D9AC File Offset: 0x0000BBAC
		private static List<CurveLoop> GetSweepProfile(List<Curve> centerlineCurves, double radius)
		{
			List<CurveLoop> list = new List<CurveLoop>();
			Plane rebarStartProfilePlane = SteelGroupElementRebar.GetRebarStartProfilePlane(centerlineCurves);
			bool flag = rebarStartProfilePlane == null;
			List<CurveLoop> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				CurveLoop rebarSweepProfile = SteelGroupElementRebar.GetRebarSweepProfile(rebarStartProfilePlane, centerlineCurves, radius);
				bool flag2 = rebarSweepProfile == null;
				if (flag2)
				{
					result = list;
				}
				else
				{
					list.Add(rebarSweepProfile);
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000D9FC File Offset: 0x0000BBFC
		private static void GetRebarIndices(Rebar rebar, out int startIndex, out int numberOfBars)
		{
			startIndex = 0;
			numberOfBars = rebar.NumberOfBarPositions;
			bool flag = numberOfBars > 1 && !rebar.IncludeFirstBar;
			if (flag)
			{
				int num = startIndex;
				startIndex = num + 1;
			}
			bool flag2 = numberOfBars > 1 && !rebar.IncludeLastBar;
			if (flag2)
			{
				int num = numberOfBars;
				numberOfBars = num - 1;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000DA50 File Offset: 0x0000BC50
		private static List<Curve> GetCenterLineCurvesForVolumeCalculation(RevitElement<Rebar> rebar, int i)
		{
			List<Curve> list = rebar.Element.GetCenterlineCurves(true, false, false, 0, i).ToList<Curve>();
			RebarShapeDrivenAccessor rebarShapeDrivenAccessor = null;
			bool flag = !rebar.Element.GetShapeAccessor(out rebarShapeDrivenAccessor);
			List<Curve> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				bool flag2 = i > 0;
				if (flag2)
				{
					Transform transform = rebarShapeDrivenAccessor.GetBarPositionTransform(i);
					list = (from c in list
					select c.CreateTransformed(transform)).ToList<Curve>();
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000DAD0 File Offset: 0x0000BCD0
		private static Solid GetSolid(List<Curve> centerLineCurve, List<CurveLoop> profile)
		{
			bool flag = centerLineCurve.none<Curve>();
			Solid result;
			if (flag)
			{
				result = null;
			}
			else
			{
				CurveLoop curveLoop = CurveLoop.Create(centerLineCurve);
				double endParameter = centerLineCurve[0].GetEndParameter(0);
				Solid solid = GeometryCreationUtilities.CreateSweptGeometry(curveLoop, 0, endParameter, profile);
				result = solid;
			}
			return result;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000DB14 File Offset: 0x0000BD14
		private static CurveLoop GetRebarSweepProfile(Plane plane, List<Curve> centerLineCurves, double radius)
		{
			bool flag = centerLineCurves.none<Curve>();
			CurveLoop result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XYZ xvec = plane.XVec;
				XYZ yvec = plane.YVec;
				XYZ endPoint = centerLineCurves[0].GetEndPoint(0);
				Arc item = Arc.Create(endPoint, radius, 0.0, 3.1415926535897931, xvec, yvec);
				Arc item2 = Arc.Create(endPoint, radius, 3.1415926535897931, 6.2831853071795862, xvec, yvec);
				CurveLoop curveLoop = CurveLoop.Create(new List<Curve>
				{
					item,
					item2
				});
				result = curveLoop;
			}
			return result;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
		private static Plane GetRebarStartProfilePlane(List<Curve> centerLineCurves)
		{
			bool flag = centerLineCurves.none<Curve>();
			Plane result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XYZ endPoint = centerLineCurves[0].GetEndPoint(0);
				XYZ rebarStartProfilePlaneNormal = SteelGroupElementRebar.GetRebarStartProfilePlaneNormal(centerLineCurves, endPoint);
				bool flag2 = rebarStartProfilePlaneNormal == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					Plane plane = Plane.CreateByNormalAndOrigin(rebarStartProfilePlaneNormal, endPoint);
					result = plane;
				}
			}
			return result;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000DC08 File Offset: 0x0000BE08
		private static XYZ GetRebarStartProfilePlaneNormal(List<Curve> rebarCurve, XYZ startPoint)
		{
			bool flag = rebarCurve.none<Curve>();
			XYZ result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Curve curve = rebarCurve[0];
				Arc arc = curve as Arc;
				Line line = curve as Line;
				bool flag2 = arc != null;
				XYZ xyz2;
				if (flag2)
				{
					XYZ normal = arc.Normal;
					XYZ center = arc.Center;
					XYZ xyz = startPoint - center;
					xyz2 = xyz.CrossProduct(normal);
				}
				else
				{
					bool flag3 = line != null;
					if (flag3)
					{
						xyz2 = line.Direction;
					}
					else
					{
						xyz2 = null;
					}
				}
				result = xyz2;
			}
			return result;
		}
	}
}
