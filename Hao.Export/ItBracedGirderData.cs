using System;
using Hao.Export.Geometry;
using AdskLocalisation;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x0200000C RID: 12
	public class ItBracedGirderData
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003A39 File Offset: 0x00001C39
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00003A41 File Offset: 0x00001C41
		public RevitElement<FamilyInstance> famInst { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003A4A File Offset: 0x00001C4A
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00003A52 File Offset: 0x00001C52
		public ItGeLineSeg3d midline { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003A5B File Offset: 0x00001C5B
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00003A63 File Offset: 0x00001C63
		private double height { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003A6C File Offset: 0x00001C6C
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00003A74 File Offset: 0x00001C74
		private double linealWeight { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003A7D File Offset: 0x00001C7D
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00003A85 File Offset: 0x00001C85
		private double upperChordDiameter { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003A8E File Offset: 0x00001C8E
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00003A96 File Offset: 0x00001C96
		private double installationHeight { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003A9F File Offset: 0x00001C9F
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00003AA7 File Offset: 0x00001CA7
		public bool isGrouped { get; set; }

		// Token: 0x06000042 RID: 66 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public ItBracedGirderData(RevitElement<FamilyInstance> famInst, ItGeMatrix3d matWcsToPalette, double minZOfPartOnPalette)
		{
			bool flag = famInst.IsInvalid() || famInst.Element.Symbol.isNull() || matWcsToPalette.isNull();
			if (flag)
			{
				ItDebug.assert(false, "Constructor");
			}
			else
			{
				this.famInst = famInst;
				LocationCurve locationCurve = famInst.isNotNull() ? (famInst.Element.Location as LocationCurve) : null;
				bool flag2 = locationCurve.isNull() || locationCurve.Curve.isNull() || !locationCurve.Curve.IsBound || locationCurve.Curve.Length.Eq(0.0, -1.0);
				if (flag2)
				{
					ItDebug.assert(false, "Location of the girder is not a Curve Type or other geometric inconsistency");
				}
				else
				{
					ItGeMatrix3d mat = matWcsToPalette * famInst.Element.ecs();
					ItGeBoundBlock3d localExtents = famInst.Element.getLocalExtents();
					ItGeBoundBlock3d itGeBoundBlock3d = localExtents.transformBy(mat);
					localExtents.transformBy(mat);
					this.installationHeight = itGeBoundBlock3d.minPoint.z - minZOfPartOnPalette;
					this.height = itGeBoundBlock3d.maxPoint.z - itGeBoundBlock3d.minPoint.z;
					XYZ point = locationCurve.Curve.Evaluate(0.0, true);
					XYZ point2 = locationCurve.Curve.Evaluate(1.0, true);
					this.midline = new ItGeLineSeg3d(point.asPoint(), point2.asPoint());
					this.midline.transformBy(matWcsToPalette);
					bool flag3 = this.midline.startPoint.x.Eq(this.midline.endPoint.x, -1.0);
					if (flag3)
					{
						bool flag4 = this.midline.startPoint.y > this.midline.endPoint.y;
						if (flag4)
						{
							this.midline.reverseParam();
						}
					}
					else
					{
						bool flag5 = this.midline.startPoint.y.Eq(this.midline.endPoint.y, -1.0);
						if (flag5)
						{
							bool flag6 = this.midline.startPoint.x > this.midline.endPoint.x;
							if (flag6)
							{
								this.midline.reverseParam();
							}
						}
					}
					Parameter parameter = famInst.Element.Symbol.getParameter(ItBracedGirderData._lclParamHeight, true);
					Parameter parameter2 = famInst.Element.Symbol.getParameter(ItBracedGirderData._lclParamLinealWeight, true);
					Parameter parameter3 = famInst.Element.Symbol.getParameter(ItBracedGirderData._lclParamUpperChordDiam, true);
					bool flag7 = parameter.isNotNull();
					if (flag7)
					{
						this.height = parameter.AsDouble();
					}
					bool flag8 = parameter2.isNotNull();
					if (flag8)
					{
						this.linealWeight = parameter2.AsDouble();
					}
					bool flag9 = parameter3.isNotNull();
					if (flag9)
					{
						this.upperChordDiameter = parameter3.AsDouble();
					}
				}
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003DA9 File Offset: 0x00001FA9
		public ItBracedGirderData(ItBracedGirderData girderData)
		{
			this.midline = girderData.midline;
			this.height = girderData.height;
			this.linealWeight = this.linealWeight;
			this.upperChordDiameter = this.upperChordDiameter;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003DE8 File Offset: 0x00001FE8
		public void setParametersForUnitechnik(int[] iParameters, int count, double offset, double x, double y)
		{
			bool flag = iParameters.isNull() || iParameters.Length != 10;
			if (flag)
			{
				ItDebug.assert(false, "Input parameter ");
			}
			else
			{
				ItBracedGirderData.DataInFeet cncdata = this.GetCNCData(count, offset);
				iParameters[0] = cncdata.Count;
				iParameters[1] = CNCDataBase.convertToMM(cncdata.Length);
				iParameters[2] = CNCDataBase.convertToMM(x);
				iParameters[3] = CNCDataBase.convertToMM(y);
				iParameters[4] = CNCDataBase.convertToMM(cncdata.Offset);
				iParameters[5] = (int)ItAngle.toDegree(cncdata.Angle);
				iParameters[6] = CNCDataBase.convertToMM(cncdata.Height);
				iParameters[7] = CNCDataBase.convertToMM(cncdata.Diameter);
				iParameters[8] = (int)(cncdata.Weight * 1000.0);
				iParameters[9] = CNCDataBase.convertToMM(cncdata.InstallationHeight);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003EB4 File Offset: 0x000020B4
		internal ItBracedGirderData.DataInFeet GetCNCData(int count, double offset)
		{
			ItBracedGirderData.DataInFeet dataInFeet = new ItBracedGirderData.DataInFeet();
			dataInFeet.Count = count;
			dataInFeet.Offset = offset;
			dataInFeet.X = this.midline.startPoint.x;
			dataInFeet.Y = this.midline.startPoint.y;
			dataInFeet.Length = this.midline.startPoint.distanceTo(this.midline.endPoint);
			dataInFeet.Angle = this.midline.direction().angleTo(ItGeVector3d.kXAxis);
			dataInFeet.Height = this.height;
			dataInFeet.Diameter = this.upperChordDiameter;
			dataInFeet.Weight = dataInFeet.Length * this.linealWeight;
			dataInFeet.InstallationHeight = this.installationHeight;
			return dataInFeet;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003F84 File Offset: 0x00002184
		public bool isSimilarTypeAs(ItBracedGirderData otherGirderData)
		{
			bool flag = otherGirderData.isNull() || this == otherGirderData;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.famInst.Element.Symbol.Id != otherGirderData.famInst.Element.Symbol.Id || !this.midline.isParallelTo(otherGirderData.midline, null) || this.midline.length().Ne(otherGirderData.midline.length(), -1.0);
				if (flag2)
				{
					result = false;
				}
				else
				{
					ItGeLineSeg3d line = new ItGeLineSeg3d(this.midline.midPoint, otherGirderData.midline.midPoint);
					result = this.midline.isPerpendicularTo(line, null);
				}
			}
			return result;
		}

		// Token: 0x04000025 RID: 37
		public static readonly string _lclParamHeight = "paramHeight".Localise();

		// Token: 0x04000026 RID: 38
		public static readonly string _lclParamLinealWeight = "paramLinealWeight".Localise();

		// Token: 0x04000027 RID: 39
		public static readonly string _lclParamUpperChordDiam = "paramUpperChordDiam".Localise();

		// Token: 0x0200004F RID: 79
		internal class DataInFeet
		{
			// Token: 0x1700023A RID: 570
			// (get) Token: 0x06000557 RID: 1367 RVA: 0x0001564B File Offset: 0x0001384B
			// (set) Token: 0x06000558 RID: 1368 RVA: 0x00015653 File Offset: 0x00013853
			internal int Count { get; set; }

			// Token: 0x1700023B RID: 571
			// (get) Token: 0x06000559 RID: 1369 RVA: 0x0001565C File Offset: 0x0001385C
			// (set) Token: 0x0600055A RID: 1370 RVA: 0x00015664 File Offset: 0x00013864
			internal double Length { get; set; }

			// Token: 0x1700023C RID: 572
			// (get) Token: 0x0600055B RID: 1371 RVA: 0x0001566D File Offset: 0x0001386D
			// (set) Token: 0x0600055C RID: 1372 RVA: 0x00015675 File Offset: 0x00013875
			internal double X { get; set; }

			// Token: 0x1700023D RID: 573
			// (get) Token: 0x0600055D RID: 1373 RVA: 0x0001567E File Offset: 0x0001387E
			// (set) Token: 0x0600055E RID: 1374 RVA: 0x00015686 File Offset: 0x00013886
			internal double Y { get; set; }

			// Token: 0x1700023E RID: 574
			// (get) Token: 0x0600055F RID: 1375 RVA: 0x0001568F File Offset: 0x0001388F
			// (set) Token: 0x06000560 RID: 1376 RVA: 0x00015697 File Offset: 0x00013897
			internal double Offset { get; set; }

			// Token: 0x1700023F RID: 575
			// (get) Token: 0x06000561 RID: 1377 RVA: 0x000156A0 File Offset: 0x000138A0
			// (set) Token: 0x06000562 RID: 1378 RVA: 0x000156A8 File Offset: 0x000138A8
			internal double Angle { get; set; }

			// Token: 0x17000240 RID: 576
			// (get) Token: 0x06000563 RID: 1379 RVA: 0x000156B1 File Offset: 0x000138B1
			// (set) Token: 0x06000564 RID: 1380 RVA: 0x000156B9 File Offset: 0x000138B9
			internal double Height { get; set; }

			// Token: 0x17000241 RID: 577
			// (get) Token: 0x06000565 RID: 1381 RVA: 0x000156C2 File Offset: 0x000138C2
			// (set) Token: 0x06000566 RID: 1382 RVA: 0x000156CA File Offset: 0x000138CA
			internal double Diameter { get; set; }

			// Token: 0x17000242 RID: 578
			// (get) Token: 0x06000567 RID: 1383 RVA: 0x000156D3 File Offset: 0x000138D3
			// (set) Token: 0x06000568 RID: 1384 RVA: 0x000156DB File Offset: 0x000138DB
			internal double Weight { get; set; }

			// Token: 0x17000243 RID: 579
			// (get) Token: 0x06000569 RID: 1385 RVA: 0x000156E4 File Offset: 0x000138E4
			// (set) Token: 0x0600056A RID: 1386 RVA: 0x000156EC File Offset: 0x000138EC
			internal double InstallationHeight { get; set; }
		}
	}
}
