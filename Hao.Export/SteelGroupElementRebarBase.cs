using System;
using System.Collections.Generic;
using System.Linq;
using Hao.Export.Geometry;
using Hao.Export.MachineData.PXML;
using Hao.Export.Reinforcement;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;

namespace Hao.Export.MachineData
{
	// Token: 0x02000028 RID: 40
	public abstract class SteelGroupElementRebarBase : SteelGroupElement
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000DCA9 File Offset: 0x0000BEA9
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000DCB1 File Offset: 0x0000BEB1
		protected List<List<ItGeCurve3d>> IronSetsRealistic { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000DCBA File Offset: 0x0000BEBA
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000DCC2 File Offset: 0x0000BEC2
		protected List<List<ItGeCurve3d>> IronSetsSchematic { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000DCCB File Offset: 0x0000BECB
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000DCD3 File Offset: 0x0000BED3
		protected bool IronSetsRealisticAreInvalid { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000DCDC File Offset: 0x0000BEDC
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000DCE4 File Offset: 0x0000BEE4
		protected bool IronSetsSchematicAreInvalid { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000DCED File Offset: 0x0000BEED
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000DCF5 File Offset: 0x0000BEF5
		protected RebarBarType RebarBarType { get; set; }

		// Token: 0x060001FE RID: 510 RVA: 0x0000BD1A File Offset: 0x00009F1A
		internal SteelGroupElementRebarBase(ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette) : base(cncElementData, matWcsToPalette)
		{
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000DD00 File Offset: 0x0000BF00
		protected double diameter()
		{
			return this.RebarBarType.BarDiameter;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000DD20 File Offset: 0x0000BF20
		protected void SetMinMaxZ(List<List<ItGeCurve3d>> ironSet)
		{
			bool flag = ironSet.none<List<ItGeCurve3d>>();
			if (!flag)
			{
				base.MinZ = ironSet.Min((List<ItGeCurve3d> list) => list.Min((ItGeCurve3d curve) => curve.boundBlock().minPoint.z));
				base.MaxZ = ironSet.Max((List<ItGeCurve3d> list) => list.Max((ItGeCurve3d curve) => curve.boundBlock().maxPoint.z));
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000DD94 File Offset: 0x0000BF94
		public List<ItBar> ToBarList(ItSteel steelBlock, List<List<ItGeCurve3d>> ironSet)
		{
			List<ItBar> list = new List<ItBar>();
			bool flag = ironSet.none<List<ItGeCurve3d>>();
			List<ItBar> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				bool flag2 = this.RebarBarType == null;
				if (flag2)
				{
					result = list;
				}
				else
				{
					foreach (List<ItGeCurve3d> list2 in ironSet)
					{
						bool flag3 = list2.none<ItGeCurve3d>();
						if (!flag3)
						{
							double rotZ = 0.0;
							ItGeCurve3d firstSeg = list2.FirstOrDefault<ItGeCurve3d>();
							ItGePoint3d rebarStartPoint = SteelGroupElementRebarBase.GetRebarStartPoint(firstSeg);
							ItGePoint3d startPoint = steelBlock.Offset(rebarStartPoint);
							ItBar itBar = this.CreateBarElement(startPoint, rotZ);
							this.FillSegments(itBar, list2);
							list.Add(itBar);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000DE68 File Offset: 0x0000C068
		private static ItGePoint3d GetRebarStartPoint(ItGeCurve3d firstSeg)
		{
			ItGePoint3d result;
			firstSeg.hasStartPoint(out result);
			return result;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000DE84 File Offset: 0x0000C084
		private ItBar CreateBarElement(ItGePoint3d startPoint, double rotZ)
		{
			return new ItBar
			{
				ShapeMode = ShapeMode.realistic,
				ReinforcementType = base.CNCReinfTypeValue,
				SteelQuality = SteelGroupElementRebarBase.GetSteelQuality(this.RebarBarType),
				PieceCount = 1,
				Diameter = this.RebarBarType.BarDiameter,
				X = startPoint.x,
				Y = startPoint.y,
				Z = startPoint.z,
				RotZ = rotZ,
				CurrentNormal = ItGeVector3d.kYAxis
			};
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000DF1C File Offset: 0x0000C11C
		private void FillSegments(ItBar xmlBar, List<ItGeCurve3d> rebarList)
		{
			bool flag = rebarList.Count == 0;
			if (!flag)
			{
				bool flag2 = true;
				ItSegment itSegment = this.AddFirstSegment(xmlBar, rebarList, ref flag2);
				ItSegment prevSegment = itSegment;
				int num;
				for (int i = 1; i < rebarList.Count; i = num + 1)
				{
					prevSegment = this.AddSegment(xmlBar, rebarList, i, prevSegment, ref flag2);
					num = i;
				}
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000DF78 File Offset: 0x0000C178
		private ItSegment AddFirstSegment(ItBar xmlBar, List<ItGeCurve3d> rebarList, ref bool flipFutureYs)
		{
			bool flag = rebarList.none<ItGeCurve3d>();
			ItSegment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ItGeCurve3d firstCurve = rebarList[0];
				ItGeCurve3d nextCurve = (rebarList.Count > 1) ? rebarList[1] : null;
				ItSegment itSegment2;
				ItSegment itSegment = this.CreateFirstSegment(rebarList, xmlBar, firstCurve, nextCurve, ref flipFutureYs, out itSegment2);
				bool flag2 = itSegment == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					bool flag3 = itSegment2 != null;
					if (flag3)
					{
						xmlBar.segmentList.Add(itSegment2);
					}
					xmlBar.segmentList.Add(itSegment);
					result = itSegment;
				}
			}
			return result;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000DFFC File Offset: 0x0000C1FC
		private ItSegment AddSegment(ItBar xmlBar, List<ItGeCurve3d> rebarList, int index, ItSegment prevSegment, ref bool flipFutureYs)
		{
			bool flag = index >= rebarList.Count;
			ItSegment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ItGeCurve3d curve = rebarList[index];
				ItGeCurve3d nextCurve = (index < rebarList.Count - 1) ? rebarList[index + 1] : null;
				ItSegment itSegment2;
				ItSegment itSegment = this.CreateSegment(xmlBar, prevSegment, curve, nextCurve, ref flipFutureYs, out itSegment2);
				bool flag2 = itSegment == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					bool flag3 = itSegment2 != null;
					if (flag3)
					{
						xmlBar.segmentList.Add(itSegment2);
					}
					xmlBar.segmentList.Add(itSegment);
					result = itSegment;
				}
			}
			return result;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000E08C File Offset: 0x0000C28C
		private ItSegment CreateFirstSegment(List<ItGeCurve3d> rebarList, ItBar xmlBar, ItGeCurve3d firstCurve, ItGeCurve3d nextCurve, ref bool flipFutureYs, out ItSegment additionalSegment)
		{
			additionalSegment = null;
			ItGeVector3d rebarElementNormal = this.GetRebarElementNormal();
			ItGeVector3d normalFromFirstTwoSegments = SteelGroupElementRebarBase.GetNormalFromFirstTwoSegments(rebarList, rebarElementNormal);
			ItGeLineSeg3d itGeLineSeg3d = firstCurve as ItGeLineSeg3d;
			ItGeCircArc3d itGeCircArc3d = firstCurve as ItGeCircArc3d;
			bool flag = itGeLineSeg3d != null;
			ItSegment result;
			if (flag)
			{
				result = this.GetFirstSegFromLineSeg(xmlBar, itGeLineSeg3d, normalFromFirstTwoSegments, nextCurve, ref flipFutureYs);
			}
			else
			{
				bool flag2 = itGeCircArc3d != null;
				if (flag2)
				{
					result = this.GetFirstSegFromCircArc(xmlBar, itGeCircArc3d, normalFromFirstTwoSegments, ref flipFutureYs, out additionalSegment);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000208 RID: 520
		protected abstract ItGeVector3d GetRebarElementNormal();

		// Token: 0x06000209 RID: 521 RVA: 0x0000E100 File Offset: 0x0000C300
		private ItSegment CreateSegment(ItBar xmlBar, ItSegment prevSegment, ItGeCurve3d curve, ItGeCurve3d nextCurve, ref bool flipFutureYs, out ItSegment additionalSegment)
		{
			additionalSegment = null;
			ItGeLineSeg3d itGeLineSeg3d = curve as ItGeLineSeg3d;
			ItGeCircArc3d itGeCircArc3d = curve as ItGeCircArc3d;
			bool flag = itGeLineSeg3d != null;
			ItSegment result;
			if (flag)
			{
				result = this.GetSegmentFromLineSeg(xmlBar, prevSegment, itGeLineSeg3d, nextCurve, ref flipFutureYs);
			}
			else
			{
				bool flag2 = itGeCircArc3d != null;
				if (flag2)
				{
					result = this.GetSegmentFromCircArc(xmlBar, prevSegment, itGeCircArc3d, ref flipFutureYs, out additionalSegment);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000E160 File Offset: 0x0000C360
		private ItSegment GetFirstSegFromCircArc(ItBar xmlBar, ItGeCircArc3d firstCirc, ItGeVector3d xzPlaneNormal, ref bool flipFutureYs, out ItSegment additionalSegment)
		{
			ItGeLine3d itGeLine3d;
			firstCirc.tangent(firstCirc.startPoint(), out itGeLine3d, null);
			ItGeVector3d itGeVector3d = itGeLine3d.direction().negate();
			ItSegment startSegment = this.GetStartSegment(xmlBar);
			double num;
			double num2;
			this.GetRotationsInDegree(xzPlaneNormal, startSegment, itGeVector3d, ref flipFutureYs, out num, out num2);
			ItGeVector3d itGeVector3d2;
			ItGeVector3d yAxis;
			ItGeVector3d zAxis;
			this.GetNewDirections(num, num2, startSegment, out itGeVector3d2, out yAxis, out zAxis);
			this.SetCurrentNormal(xmlBar, num, num2, startSegment);
			ItDebug.assert(itGeVector3d2.isCodirectionalTo(itGeVector3d, null), "Failure in: ");
			double l = 0.0;
			double r = 0.0;
			double rotX = Math.Round(num, 10);
			double bendY = Math.Round(num2, 10);
			ItGeMatrix3d kIdentity = ItGeMatrix3d.kIdentity;
			kIdentity.setCoordSystem(ItGePoint3d.kOrigin, itGeVector3d, yAxis, zAxis);
			additionalSegment = new ItSegment(l, r, rotX, bendY, kIdentity, "normal");
			double circleSign = this.GetCircleSign(xmlBar, firstCirc);
			double num3;
			double num4;
			this.GetRotationsInDegree(xzPlaneNormal, additionalSegment, itGeVector3d, ref flipFutureYs, out num3, out num4);
			ItGeVector3d itGeVector3d3;
			ItGeVector3d itGeVector3d4;
			ItGeVector3d itGeVector3d5;
			this.GetNewDirections(num3, 0.0, startSegment, out itGeVector3d3, out itGeVector3d4, out itGeVector3d5);
			this.SetCurrentNormal(xmlBar, num3, 0.0, additionalSegment);
			double num5 = this.GetCentralAngleWithSign(firstCirc, circleSign);
			num5 = ItAngle.toDegree(num5);
			ItGeLine3d itGeLine3d2;
			firstCirc.tangent(firstCirc.startPoint(), out itGeLine3d2, null);
			ItGeVector3d itGeVector3d6 = itGeLine3d2.direction();
			ItGeVector3d zAxis2 = itGeVector3d6.crossProduct(xzPlaneNormal);
			double l2 = 0.0;
			double r2 = firstCirc.radius();
			double rotX2 = Math.Round(num3, 10);
			double bendY2 = Math.Round(num5, 10);
			kIdentity = ItGeMatrix3d.kIdentity;
			kIdentity.setCoordSystem(ItGePoint3d.kOrigin, itGeVector3d6, xzPlaneNormal, zAxis2);
			return new ItSegment(l2, r2, rotX2, bendY2, kIdentity, "normal");
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000E310 File Offset: 0x0000C510
		private ItSegment GetFirstSegFromLineSeg(ItBar xmlBar, ItGeLineSeg3d firstSeg, ItGeVector3d xzPlaneNormal, ItGeCurve3d nextCurve, ref bool flipFutureYs)
		{
			ItGeVector3d direction = firstSeg.direction();
			ItSegment startSegment = this.GetStartSegment(xmlBar);
			double num;
			double num2;
			this.GetRotationsInDegree(xzPlaneNormal, startSegment, direction, ref flipFutureYs, out num, out num2);
			this.SetCurrentNormal(xmlBar, num, num2, startSegment);
			ItGeVector3d xAxis;
			ItGeVector3d yAxis;
			ItGeVector3d zAxis;
			this.GetNewDirections(num, num2, startSegment, out xAxis, out yAxis, out zAxis);
			double len = firstSeg.len;
			ItGeMatrix3d kIdentity = ItGeMatrix3d.kIdentity;
			kIdentity.setCoordSystem(ItGePoint3d.kOrigin, xAxis, yAxis, zAxis);
			ItSegment itSegment = new ItSegment(len, 0.0, Math.Round(num, 10), Math.Round(num2, 10), kIdentity, "normal");
			this.FixSegmentLength(itSegment, nextCurve);
			return itSegment;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
		private void GetRotationsInDegree(ItGeVector3d rebarPlaneNormal, ItSegment prevSegment, ItGeVector3d direction, ref bool flipFutureYs, out double rotX, out double bendY)
		{
			double a = rebarPlaneNormal.dotProduct(prevSegment.XVector);
			bool flag = a.Eq(0.0, -1.0);
			if (flag)
			{
				this.GetRotationsSimple(rebarPlaneNormal, prevSegment, direction, ref flipFutureYs, out rotX, out bendY);
			}
			else
			{
				this.GetRotationComplex(rebarPlaneNormal, prevSegment, direction, ref flipFutureYs, out rotX, out bendY);
			}
			rotX = ItAngle.toDegree(rotX);
			bendY = ItAngle.toDegree(bendY);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000E42C File Offset: 0x0000C62C
		private void GetRotationComplex(ItGeVector3d rebarPlaneNormal, ItSegment prevSegment, ItGeVector3d direction, ref bool flipFutureYs, out double rotX, out double bendY)
		{
			ItGeVector3d xvector = prevSegment.XVector;
			ItGeVector3d yvector = prevSegment.YVector;
			ItGeVector3d itGeVector3d = yvector.orthoProject(rebarPlaneNormal);
			ItGeVector3d itGeVector3d2 = direction.crossProduct(rebarPlaneNormal);
			rotX = itGeVector3d.angleTo(itGeVector3d2, xvector);
			rotX = SteelGroupElementRebarBase.FixRotX(rotX, ref flipFutureYs);
			bendY = xvector.angleTo(direction, itGeVector3d2);
			bendY = SteelGroupElement.FixAngleRange(bendY, flipFutureYs);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000E48C File Offset: 0x0000C68C
		private void GetRotationsSimple(ItGeVector3d rebarPlaneNormal, ItSegment prevSegment, ItGeVector3d direction, ref bool flipFutureYs, out double rotX, out double bendY)
		{
			ItGeVector3d xvector = prevSegment.XVector;
			ItGeVector3d yvector = prevSegment.YVector;
			rotX = yvector.angleTo(rebarPlaneNormal, xvector);
			rotX = SteelGroupElementRebarBase.FixRotX(rotX, ref flipFutureYs);
			ItGeVector3d refVec = new ItGeVector3d(yvector).rotateBy(rotX, xvector);
			bendY = xvector.angleTo(direction, refVec);
			bendY = SteelGroupElement.FixAngleRange(bendY, flipFutureYs);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		private ItSegment GetSegmentFromCircArc(ItBar xmlBar, ItSegment prevSegment, ItGeCircArc3d circArc, ref bool flipFutureYs, out ItSegment additionalSegment)
		{
			additionalSegment = null;
			ItGeLine3d itGeLine3d=null;
			ItGeLine3d itGeLine3d2=null;
			bool flag = !circArc.tangent(circArc.endPoint(), out itGeLine3d, null) || !circArc.tangent(circArc.startPoint(), out itGeLine3d2, null);
			ItSegment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ItGeVector3d itGeVector3d = (itGeLine3d2 != null) ? itGeLine3d2.direction().negate() : prevSegment.XVector;
				this.GetAdditionalSegment(xmlBar, prevSegment, out additionalSegment, itGeVector3d, ref flipFutureYs);
				ItGeVector3d rebarPlaneNormal = circArc.normal();
				double num;
				double num2;
				this.GetRotationsInDegree(rebarPlaneNormal, additionalSegment ?? prevSegment, itGeVector3d, ref flipFutureYs, out num, out num2);
				this.SetCurrentNormal(xmlBar, num, 0.0, additionalSegment ?? prevSegment);
				double circleSign = this.GetCircleSign(xmlBar, circArc);
				double num3 = this.GetCentralAngleWithSign(circArc, circleSign);
				num3 = ItAngle.toDegree(num3);
				ItGeVector3d itGeVector3d2 = -1.0 * itGeLine3d.direction();
				ItGeVector3d itGeVector3d3 = circleSign * circArc.normal();
				ItGeVector3d zAxis = itGeVector3d2.crossProduct(itGeVector3d3);
				double l = 0.0;
				double r = circArc.radius();
				num = Math.Round(num, 10);
				double bendY = Math.Round(num3, 10);
				ItGeMatrix3d kIdentity = ItGeMatrix3d.kIdentity;
				kIdentity.setCoordSystem(ItGePoint3d.kOrigin, itGeVector3d2, itGeVector3d3, zAxis);
				ItSegment itSegment = new ItSegment(l, r, num, bendY, kIdentity, "normal");
				result = itSegment;
			}
			return result;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000E648 File Offset: 0x0000C848
		private ItSegment GetSegmentFromLineSeg(ItBar xmlBar, ItSegment prevSegment, ItGeLineSeg3d lineSeg, ItGeCurve3d nextCurve, ref bool flipFutureYs)
		{
			ItGeVector3d itGeVector3d = lineSeg.direction();
			ItGeVector3d xvector = prevSegment.XVector;
			ItGeVector3d itGeVector3d2 = itGeVector3d.orthoProject(xvector);
			double num;
			double num2;
			this.GetRotationsInDegree(xmlBar.CurrentNormal, prevSegment, itGeVector3d, ref flipFutureYs, out num, out num2);
			this.SetCurrentNormal(xmlBar, num, num2, prevSegment);
			ItGeVector3d xAxis;
			ItGeVector3d yAxis;
			ItGeVector3d zAxis;
			this.GetNewDirections(num, num2, prevSegment, out xAxis, out yAxis, out zAxis);
			double len = lineSeg.len;
			double r = 0.0;
			num = Math.Round(num, 10);
			num2 = Math.Round(num2, 10);
			ItGeMatrix3d kIdentity = ItGeMatrix3d.kIdentity;
			kIdentity.setCoordSystem(ItGePoint3d.kOrigin, xAxis, yAxis, zAxis);
			ItSegment itSegment = new ItSegment(len, r, num, num2, kIdentity, "normal");
			this.FixSegmentLength(itSegment, nextCurve);
			return itSegment;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000E708 File Offset: 0x0000C908
		private void GetNewDirections(double rotX, double bendY, ItSegment prevSegment, out ItGeVector3d newPrevX, out ItGeVector3d newPrevY, out ItGeVector3d newPrevZ)
		{
			rotX = ItAngle.toRad(rotX);
			bendY = ItAngle.toRad(bendY);
			newPrevY = new ItGeVector3d(prevSegment.YVector).rotateBy(rotX, prevSegment.XVector);
			newPrevX = new ItGeVector3d(prevSegment.XVector).rotateBy(bendY, -1.0 * newPrevY);
			newPrevZ = new ItGeVector3d(prevSegment.ZVector).rotateBy(rotX, prevSegment.XVector).rotateBy(bendY, -1.0 * newPrevY);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000E798 File Offset: 0x0000C998
		private void SetCurrentNormal(ItBar xmlBar, double rotX, double bendY, ItSegment prevSegment)
		{
			rotX = ItAngle.toRad(rotX);
			bendY = ItAngle.toRad(bendY);
			ItGeVector3d vec = new ItGeVector3d(prevSegment.YVector).rotateBy(rotX, prevSegment.XVector);
			xmlBar.CurrentNormal = xmlBar.CurrentNormal.rotateBy(rotX, prevSegment.XVector).rotateBy(bendY, -1.0 * vec);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000E800 File Offset: 0x0000CA00
		public static ItGeVector3d GetNormalFromFirstTwoSegments(List<ItGeCurve3d> rebarList, ItGeVector3d defaultValue)
		{
			bool flag = rebarList.Count < 2;
			ItGeVector3d result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				ItGeCircArc3d itGeCircArc3d = rebarList[0] as ItGeCircArc3d;
				ItGeLineSeg3d firstCurve = rebarList[0] as ItGeLineSeg3d;
				bool flag2 = itGeCircArc3d != null;
				if (flag2)
				{
					result = itGeCircArc3d.normal();
				}
				else
				{
					int num;
					for (int i = 1; i < rebarList.Count; i = num + 1)
					{
						ItGeCurve3d nextCurve = rebarList[i];
						ItGeVector3d normalFromTwoSegments = SteelGroupElementRebarBase.GetNormalFromTwoSegments(firstCurve, nextCurve);
						bool flag3 = normalFromTwoSegments != null;
						if (flag3)
						{
							return normalFromTwoSegments;
						}
						num = i;
					}
					result = ItGeVector3d.kYAxis;
				}
			}
			return result;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000E8A4 File Offset: 0x0000CAA4
		public static ItGeVector3d GetNormalFromTwoSegments(ItGeCurve3d firstCurve, ItGeCurve3d nextCurve)
		{
			ItGeCircArc3d itGeCircArc3d = firstCurve as ItGeCircArc3d;
			ItGeLineSeg3d itGeLineSeg3d = firstCurve as ItGeLineSeg3d;
			bool flag = itGeCircArc3d != null;
			ItGeVector3d result;
			if (flag)
			{
				result = itGeCircArc3d.normal();
			}
			else
			{
				ItGeVector3d itGeVector3d = itGeLineSeg3d.direction();
				ItGeCircArc3d itGeCircArc3d2 = nextCurve as ItGeCircArc3d;
				ItGeLineSeg3d itGeLineSeg3d2 = nextCurve as ItGeLineSeg3d;
				bool flag2 = itGeCircArc3d2 != null;
				if (flag2)
				{
					ItGeVector3d itGeVector3d2 = itGeCircArc3d2.normal();
					ItGePlane itGePlane = new ItGePlane(ItGePoint3d.kOrigin, itGeVector3d2);
					ItGePoint3d itGePoint3d;
					bool flag3 = itGePlane.intersectWith(itGeLineSeg3d, out itGePoint3d, null);
					if (flag3)
					{
						result = ItGeVector3d.kYAxis;
					}
					else
					{
						result = itGeVector3d2;
					}
				}
				else
				{
					bool flag4 = itGeLineSeg3d2 != null;
					if (flag4)
					{
						ItGeVector3d vec = itGeLineSeg3d2.direction();
						ItGeVector3d itGeVector3d3 = itGeVector3d.crossProduct(vec);
						bool flag5 = itGeVector3d3.length().Gt(0.0, -1.0);
						if (flag5)
						{
							return itGeVector3d3;
						}
					}
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000E988 File Offset: 0x0000CB88
		private ItSegment GetStartSegment(ItBar xmlBar)
		{
			ItGeMatrix3d mat = ItGeMatrix3d.rotation(xmlBar.RotZ * 3.1415926535897931 / 180.0, ItGeVector3d.kZAxis, ItGePoint3d.kOrigin);
			ItGeVector3d xAxis = mat * ItGeVector3d.kXAxis;
			ItGeVector3d yAxis = mat * ItGeVector3d.kYAxis;
			ItGeVector3d zAxis = mat * ItGeVector3d.kZAxis;
			double l = 0.0;
			double r = 0.0;
			double rotX = 0.0;
			double bendY = 0.0;
			ItGeMatrix3d kIdentity = ItGeMatrix3d.kIdentity;
			kIdentity.setCoordSystem(ItGePoint3d.kOrigin, xAxis, yAxis, zAxis);
			return new ItSegment(l, r, rotX, bendY, kIdentity, "normal");
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000EA48 File Offset: 0x0000CC48
		private void FixSegmentLength(ItSegment segment, ItGeCurve3d nextCurve)
		{
			ItGeCircArc3d itGeCircArc3d = nextCurve as ItGeCircArc3d;
			bool flag = itGeCircArc3d == null;
			if (!flag)
			{
				double num = Math.Abs(this.GetCentralAngleWithSign(itGeCircArc3d, 1.0));
				bool flag2 = num.Ge(1.5707963267948966, -1.0);
				if (flag2)
				{
					segment.L += itGeCircArc3d.radius();
				}
				else
				{
					double num2 = itGeCircArc3d.radius();
					double num3 = Math.Sin(num);
					double value = num3 * num2;
					segment.L += Math.Round(value, 5);
				}
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		private double GetCircleSign(ItBar xmlBar, ItGeCircArc3d circArc)
		{
			ItGeVector3d vec = circArc.normal();
			bool flag = xmlBar.CurrentNormal.isCodirectionalTo(vec, null);
			double result;
			if (flag)
			{
				result = -1.0;
			}
			else
			{
				result = 1.0;
			}
			return result;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000EB30 File Offset: 0x0000CD30
		private double GetCentralAngleWithSign(ItGeCircArc3d circArc, double circleSign)
		{
			double num = circArc.endAng() - circArc.startAng();
			return circleSign * num;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000EB58 File Offset: 0x0000CD58
		private void GetAdditionalSegment(ItBar xmlBar, ItSegment prevSegment, out ItSegment additionalSegment, ItGeVector3d startDirection, ref bool flipFutureYs)
		{
			additionalSegment = null;
			bool flag = startDirection.isCodirectionalTo(prevSegment.XVector, null);
			if (!flag)
			{
				double num;
				double num2;
				this.GetRotationsInDegree(xmlBar.CurrentNormal, prevSegment, startDirection, ref flipFutureYs, out num, out num2);
				this.SetCurrentNormal(xmlBar, num, 0.0, prevSegment);
				ItGeVector3d itGeVector3d;
				ItGeVector3d yAxis;
				ItGeVector3d zAxis;
				this.GetNewDirections(num, num2, prevSegment, out itGeVector3d, out yAxis, out zAxis);
				ItDebug.assert(itGeVector3d.isCodirectionalTo(startDirection, null), "Failure in: ");
				double l = 0.0;
				double r = 0.0;
				num = Math.Round(num, 10);
				num2 = Math.Round(num2, 10);
				ItGeMatrix3d kIdentity = ItGeMatrix3d.kIdentity;
				kIdentity.setCoordSystem(ItGePoint3d.kOrigin, startDirection, yAxis, zAxis);
				additionalSegment = new ItSegment(l, r, num, num2, kIdentity, "normal");
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000EC24 File Offset: 0x0000CE24
		private static double FixRotX(double angle, ref bool flipFutureYs)
		{
			angle = SteelGroupElement.FixAngleRange(angle, false);
			bool flag = Math.Abs(angle).Gt(1.5707963267948966, -1.0);
			if (flag)
			{
				angle -= (double)Math.Sign(angle) * 3.1415926535897931;
				flipFutureYs = !flipFutureYs;
			}
			return angle;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000EC80 File Offset: 0x0000CE80
		public static string GetSteelQuality(RebarBarType revitReinfType)
		{
			double defValue = 500.0.NperMM2ToRevitYieldStrength();
			double num = Math.Round(revitReinfType.GetSteelQuality(defValue).RevitYieldStrengthToNperMM2(), 5);
			return string.Empty + num;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000ECC4 File Offset: 0x0000CEC4
		public override void SetReinforcementType()
		{
			ReinforcementType reinforcementType = base.Data.reinforcementType;
			bool flag = reinforcementType == ReinforcementType.LintelReinf;
			if (flag)
			{
				this.SetReinforcementTypeForLintel(this.IronSetsRealistic);
			}
			else
			{
				IEqualityComparer<Tuple<double, double>> equalComparer = ItComparerFactory.getEqualComparer<Tuple<double, double>>((Tuple<double, double> t1, Tuple<double, double> t2) => t1.Item1.Eq(t2.Item1, -1.0), (Tuple<double, double> t1) => t1.Item1.GetHashCode());
				IEnumerable<Tuple<double, double>> source = (from element in base.Group.Elements
				select Tuple.Create<double, double>(element.MinZ, element.MaxZ)).Distinct(equalComparer);
				List<Tuple<double, double>> list = (from t in source
				orderby t.Item1
				select t).ToList<Tuple<double, double>>();
				int num = list.FindIndex((Tuple<double, double> t) => base.MinZ.Eq(t.Item1, -1.0));
				bool flag2 = num == -1;
				if (flag2)
				{
					base.SetReinforcementTypeUnknown();
				}
				else
				{
					bool flag3 = base.Host.productType().isWall();
					if (flag3)
					{
						this.SetReinforcementTypeForWall(num);
					}
					else
					{
						bool flag4 = base.Host.productType().isFloor();
						if (flag4)
						{
							this.SetReinforcementTypeForFloor(num);
						}
						else
						{
							base.SetReinforcementTypeUnknown();
						}
					}
				}
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000EE18 File Offset: 0x0000D018
		private void SetReinforcementTypeForLintel(List<List<ItGeCurve3d>> ironSet)
		{
			bool flag = ironSet.First<List<ItGeCurve3d>>().Count > 1;
			if (flag)
			{
				base.CNCReinfType = SteelGroupElement.CNCReinforcementType.FirstRebarLayer;
			}
			else
			{
				base.CNCReinfType = SteelGroupElement.CNCReinforcementType.SecondRebarLayer;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000EE4C File Offset: 0x0000D04C
		protected void SetReinforcementTypeFromZ()
		{
			double dY = base.Host.thickness() / 2.0;
			bool flag = base.MaxZ.Le(dY, -1.0);
			if (flag)
			{
				base.CNCReinfType = SteelGroupElement.CNCReinforcementType.Other;
			}
			else
			{
				base.CNCReinfType = SteelGroupElement.CNCReinforcementType.UpperOther;
			}
		}

		// Token: 0x0600021F RID: 543
		protected abstract void SetReinforcementTypeForFloor(int layerIndexInZDir);

		// Token: 0x06000220 RID: 544
		protected abstract void SetReinforcementTypeForWall(int layerIndexInZDir);

		// Token: 0x06000221 RID: 545 RVA: 0x0000EEA0 File Offset: 0x0000D0A0
		protected void UnitechnikAddStraightBars(List<List<ItGeCurve3d>> arrayIrons, ElementId id)
		{
			List<ItGeCurve3d> list = arrayIrons.FirstOrDefault<List<ItGeCurve3d>>();
			bool flag = list == null;
			if (flag)
			{
				ItFailures.PostFailure(ItFailures.CAMObjectWithoutValidGeometry, id);
			}
			else
			{
				ItGeCurve3d itGeCurve3d = list.FirstOrDefault<ItGeCurve3d>();
				bool flag2 = itGeCurve3d == null;
				if (flag2)
				{
					ItFailures.PostFailure(ItFailures.CAMObjectWithoutValidGeometry, id);
				}
				else
				{
					ItGePoint3d itGePoint3d;
					itGeCurve3d.hasStartPoint(out itGePoint3d);
					ItGeVector3d itGeVector3d;
					short unitechnikAngleOfFreeForm = this.GetUnitechnikAngleOfFreeForm(itGeCurve3d, out itGeVector3d);
					bool flag3 = arrayIrons.Count > 1;
					int num;
					double num2;
					if (flag3)
					{
						this.GetSpacingAndCountForUnitechnik(unitechnikAngleOfFreeForm, out num, out num2);
					}
					else
					{
						ItGeVector3d kOrigin = ItGeVector3d.kOrigin;
						num2 = 0.0;
						num = 1;
					}
					uint length = (uint)CNCDataBase.convertToMM(itGeCurve3d.len);
					ItUniWrapperImpl.RodstockData data = new ItUniWrapperImpl.RodstockData
					{
						length = length,
						startAngle = unitechnikAngleOfFreeForm,
						artNr = string.Empty,
						autoProd = 0,
						diameter = (ushort)CNCDataBase.convertToMM(this.RebarBarType.BarDiameter),
						flexFormNumber = 0,
						formType = 0,
						hasSpacers = 0,
						hasWielding = 0,
						pitchSpacer = 0u,
						startingPointSpacer = 0u,
						reinfType = (byte)base.CNCReinfTypeValue,
						steelQuality = SteelGroupElementRebarBase.GetSteelQuality(this.RebarBarType),
						xcoord = CNCDataBase.convertToMM(itGePoint3d.x),
						ycoord = CNCDataBase.convertToMM(itGePoint3d.y),
						installationHeight = (uint)CNCDataBase.convertToMM(itGePoint3d.z),
						number = (ushort)(num2.Ne(0.0, -1.0) ? num : 1),
						pitch = CNCDataBase.convertToMM(num2)
					};
					ItUniWrapperImpl.AddStraightRodstock(data);
					bool flag4 = num2.Eq(0.0, -1.0) && num > 1;
					if (flag4)
					{
						int num3;
						for (int i = 1; i < arrayIrons.Count; i = num3 + 1)
						{
							itGeCurve3d = arrayIrons[i].FirstOrDefault<ItGeCurve3d>();
							bool flag5 = itGeCurve3d == null;
							if (!flag5)
							{
								itGeCurve3d.hasStartPoint(out itGePoint3d);
								data.xcoord = CNCDataBase.convertToMM(itGePoint3d.x);
								data.ycoord = CNCDataBase.convertToMM(itGePoint3d.y);
								ItUniWrapperImpl.AddStraightRodstock(data);
							}
							num3 = i;
						}
					}
				}
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000F0FC File Offset: 0x0000D2FC
		internal override void WriteToPXML(ItSteel steelBlock, ICollection<ExportReinfData> exportedElements)
		{
			bool flag = this.IronSetsRealistic.none<List<ItGeCurve3d>>();
			if (flag)
			{
				ItFailures.PostFailure(ItFailures.CAMInvalidRebarShape, this.RebarElementId);
				exportedElements.Add(new ExportReinfData
				{
					Id = this.RebarElementId,
					ExportedAs = ExportReinfData.ExportType.AsFailure
				});
			}
			else
			{
				bool ironSetsRealisticAreInvalid = this.IronSetsRealisticAreInvalid;
				if (ironSetsRealisticAreInvalid)
				{
					ItFailures.PostFailure(ItFailures.CAMUnsupportedRebarShapeExportedAsStraightBar, this.RebarElementId);
				}
				List<ItBar> collection = this.ToBarList(steelBlock, this.IronSetsRealistic);
				steelBlock.barList.AddRange(collection);
				exportedElements.Add(new ExportReinfData
				{
					Id = this.RebarElementId,
					ExportedAs = ExportReinfData.ExportType.AsRodstock
				});
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000223 RID: 547
		protected abstract ElementId RebarElementId { get; }

		// Token: 0x06000224 RID: 548
		protected abstract void GetSpacingAndCountForUnitechnik(short angle, out int ironCount, out double ironSpacing);

		// Token: 0x06000225 RID: 549 RVA: 0x0000F1B0 File Offset: 0x0000D3B0
		protected double GetActualSpacingForUnitechnik(Transform t1, Transform t2, short angle)
		{
			ItGeVector3d distributionLengthVector = this.GetDistributionLengthVector(t1, t2);
			double distributionDirectionSign = SteelGroupElementRebarBase.GetDistributionDirectionSign(distributionLengthVector, angle);
			return distributionLengthVector.length() * distributionDirectionSign;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000F1DC File Offset: 0x0000D3DC
		private static double GetDistributionDirectionSign(ItGeVector3d distributionDirection, short angle)
		{
			bool flag = distributionDirection.isCodirectionalTo(ItGeVector3d.kXAxis, null) || distributionDirection.isCodirectionalTo(ItGeVector3d.kYAxis, null);
			double num;
			if (flag)
			{
				num = 1.0;
			}
			else
			{
				bool flag2 = distributionDirection.isCodirectionalTo(-ItGeVector3d.kXAxis, null) || distributionDirection.isCodirectionalTo(-ItGeVector3d.kYAxis, null);
				if (flag2)
				{
					num = -1.0;
				}
				else
				{
					num = 0.0;
				}
			}
			return num * ((angle == 0) ? 1.0 : ((double)Math.Sign(angle)));
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000F27C File Offset: 0x0000D47C
		private ItGeVector3d GetDistributionLengthVector(Transform t1, Transform t2)
		{
			ItGePoint3d pnt = t1.Origin.asPoint().transformBy(base.MatWcsToPalette);
			ItGePoint3d pnt2 = t2.Origin.asPoint().transformBy(base.MatWcsToPalette);
			return pnt2 - pnt;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
		protected ItGePoint3d GetStartPoint(List<List<ItGeCurve3d>> rebarCurves)
		{
			bool flag = rebarCurves.none<List<ItGeCurve3d>>() || rebarCurves[0].none<ItGeCurve3d>();
			ItGePoint3d result;
			if (flag)
			{
				result = ItGePoint3d.kOrigin;
			}
			else
			{
				ItGePoint3d itGePoint3d;
				bool flag2 = !rebarCurves[0][0].hasStartPoint(out itGePoint3d);
				if (flag2)
				{
					result = ItGePoint3d.kOrigin;
				}
				else
				{
					result = itGePoint3d;
				}
			}
			return result;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000F321 File Offset: 0x0000D521
		protected void TransformToPalette()
		{
			this.TransformBy(base.MatWcsToPalette);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000F334 File Offset: 0x0000D534
		public override IEnumerable<ItGePoint3d> GetPoints(bool schematic)
		{
			List<ItGePoint3d> list = new List<ItGePoint3d>();
			List<List<ItGeCurve3d>> list2 = schematic ? this.IronSetsSchematic : this.IronSetsRealistic;
			foreach (List<ItGeCurve3d> source in list2)
			{
				IEnumerable<ItGePoint3d> collection = source.SelectMany((ItGeCurve3d c) => c.getSamplePoints(3));
				list.AddRange(collection);
			}
			return list;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000F3D0 File Offset: 0x0000D5D0
		public override void TransformBy(ItGeMatrix3d transform)
		{
			this.IronSetsRealistic.ForEach(delegate(List<ItGeCurve3d> list)
			{
				list.transformBy(transform);
			});
			this.IronSetsSchematic.ForEach(delegate(List<ItGeCurve3d> list)
			{
				list.transformBy(transform);
			});
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000F41C File Offset: 0x0000D61C
		protected bool IsMainAxis()
		{
			bool flag = this.IronSetsSchematic.none<List<ItGeCurve3d>>() || this.IronSetsSchematic[0].none<ItGeCurve3d>();
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				ItGeVector3d directionFromCurve = SteelGroupElementRebarBase.GetDirectionFromCurve(this.IronSetsSchematic[0][0]);
				result = (directionFromCurve.isParallelTo(ItGeVector3d.kXAxis, null) || directionFromCurve.isParallelTo(ItGeVector3d.kYAxis, null));
			}
			return result;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000F48C File Offset: 0x0000D68C
		protected static ItGeVector3d GetDirectionFromCurve(ItGeCurve3d curve)
		{
			ItGeLineSeg3d itGeLineSeg3d = curve as ItGeLineSeg3d;
			ItGeCircArc3d itGeCircArc3d = curve as ItGeCircArc3d;
			bool flag = itGeLineSeg3d != null;
			ItGeVector3d result;
			if (flag)
			{
				result = itGeLineSeg3d.direction();
			}
			else
			{
				ItGeLine3d itGeLine3d;
				itGeCircArc3d.tangent(itGeCircArc3d.startPoint(), out itGeLine3d, null);
				result = itGeLine3d.direction();
			}
			return result;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000F4DC File Offset: 0x0000D6DC
		protected short GetUnitechnikAngleOfFreeForm(ItGeCurve3d first, out ItGeVector3d normal)
		{
			ItGeVector3d directionFromCurve = SteelGroupElementRebarBase.GetDirectionFromCurve(first);
			double num = ItGeVector3d.kXAxis.angleTo(directionFromCurve, ItGeVector3d.kZAxis);
			normal = ItGeVector3d.kYAxis.rotateBy(num, ItGeVector3d.kZAxis);
			num = SteelGroupElement.FixAngleRange(num, false);
			num = ItAngle.toDegree(num);
			return (short)Math.Round(num, 0);
		}
	}
}
