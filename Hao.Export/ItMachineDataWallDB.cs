using System;
using System.Collections.Generic;
using Hao.Export.Geometry;
using Hao.Export.MachineData.PXML;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x02000011 RID: 17
	public class ItMachineDataWallDB : ItMachineDataWallML
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000051A4 File Offset: 0x000033A4
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000051AC File Offset: 0x000033AC
		private ItMachineDataWallDB.DoubleWallMode Mode { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000051B8 File Offset: 0x000033B8
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000051E6 File Offset: 0x000033E6
		protected override int ProductType
		{
			get
			{
				ItMachineDataWallDB.DoubleWallMode mode = this.Mode;
				int result;
				if (mode != ItMachineDataWallDB.DoubleWallMode.FirstShell)
				{
					if (mode != ItMachineDataWallDB.DoubleWallMode.SecondShell)
					{
						result = 0;
					}
					else
					{
						result = 2;
					}
				}
				else
				{
					result = 1;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000051EC File Offset: 0x000033EC
		protected override string PxmlPartType
		{
			get
			{
				return this.ProductType.ToString("D2");
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000520C File Offset: 0x0000340C
		public override bool ClearDocument
		{
			get
			{
				return this.Mode == ItMachineDataWallDB.DoubleWallMode.FirstShell;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005217 File Offset: 0x00003417
		public ItMachineDataWallDB(ICamExportIntOptions options) : base(options)
		{
			this.Mode = ItMachineDataWallDB.DoubleWallMode.Undefined;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000522C File Offset: 0x0000342C
		protected override bool ExportPxml(AssemblyInstance assemblyInstance, IEnumerable<RevitElement<Part>> part, ItDocument cncDoc, CNCProjectData projectData)
		{
			RevitElement<Part> firstShell;
			RevitElement<Part> secondShell;
			ItMachineDataWallML.GetShells(assemblyInstance, out firstShell, out secondShell);
			ItMachineDataWallDB.DbwElementData dbwElementData = new ItMachineDataWallDB.DbwElementData(base.MatWcsToPalette, firstShell, secondShell);
			ItMachineDataWallDB.DetermineCombinedBoundingBox(dbwElementData);
			ItMachineDataWallDB.DetermineDoubleWallGap(dbwElementData);
			int doubleWallGapInMM = (int)Math.Round(dbwElementData.Gap.FeetToMeter() * 1000.0);
			ItMachineDataWallDB.DetermineShellTransformations(dbwElementData);
			bool flag = this.ExportShellToPxml(cncDoc, assemblyInstance, projectData, dbwElementData.FirstShell, doubleWallGapInMM);
			bool flag2 = !flag;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				flag = this.ExportShellToPxml(cncDoc, assemblyInstance, projectData, dbwElementData.SecondShell, doubleWallGapInMM);
				result = flag;
			}
			return result;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000052C4 File Offset: 0x000034C4
		protected override bool ExportUnitechnik(AssemblyInstance assemblyInstance, IEnumerable<RevitElement<Part>> parts, int iProdNo, CNCProjectData projectData)
		{
			this.Mode = ItMachineDataWallDB.DoubleWallMode.Undefined;
			RevitElement<Part> firstShell;
			RevitElement<Part> secondShell;
			ItMachineDataWallML.GetShells(assemblyInstance, out firstShell, out secondShell);
			ItMachineDataWallDB.DbwElementData dbwElementData = new ItMachineDataWallDB.DbwElementData(base.MatWcsToPalette, firstShell, secondShell);
			ItMachineDataWallDB.DetermineCombinedBoundingBox(dbwElementData);
			ItMachineDataWallDB.DetermineDoubleWallGap(dbwElementData);
			int doubleWallGapInMM = (int)Math.Round(dbwElementData.Gap.FeetToMeter() * 1000.0);
			ItMachineDataWallDB.DetermineShellTransformations(dbwElementData);
			bool flag = this.ExportShellToUnitechnik(assemblyInstance, iProdNo, projectData, dbwElementData.FirstShell, doubleWallGapInMM);
			bool flag2 = !flag;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				flag = this.ExportShellToUnitechnik(assemblyInstance, iProdNo, projectData, dbwElementData.SecondShell, doubleWallGapInMM);
				result = flag;
			}
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00005364 File Offset: 0x00003564
		private static void DetermineShellTransformations(ItMachineDataWallDB.DbwElementData data)
		{
			Action<ItMachineDataWallDB.DbwElementData.Shell, ItGePoint3d, ItGeMatrix3d> action = delegate(ItMachineDataWallDB.DbwElementData.Shell shellData, ItGePoint3d absoluteZero, ItGeMatrix3d matWcsToPalette)
			{
				ItGePoint3d minPoint = shellData.BoxOnPalette.minPoint;
				shellData.ZeroOffset = absoluteZero - minPoint;
				shellData.Transformation = ItGeMatrix3d.translation(shellData.ZeroOffset) * matWcsToPalette;
			};
			action(data.FirstShell, data.BoxOnPalette.minPoint, data.MatWcsToPalette);
			action(data.SecondShell, data.BoxOnPalette.minPoint, data.MatWcsToPalette);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000053D0 File Offset: 0x000035D0
		private bool ExportShellToUnitechnik(AssemblyInstance assemblyInstance, int iProdNo, CNCProjectData projectData, ItMachineDataWallDB.DbwElementData.Shell shell, int doubleWallGapInMM)
		{
			ItGeMatrix3d matWcsToPalette = base.MatWcsToPalette;
			base.MatWcsToPalette = shell.Transformation;
			this.Mode = shell.Mode;
			bool flag = base.ExportUnitechnik(assemblyInstance, new RevitElement<Part>[]
			{
				shell.Part
			}, iProdNo, projectData);
			ItUniWrapperImpl.SetDoubleWallGap(doubleWallGapInMM);
			bool flag2 = !flag;
			bool result;
			if (flag2)
			{
				result = flag;
			}
			else
			{
				base.MatWcsToPalette = matWcsToPalette;
				result = true;
			}
			return result;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000543C File Offset: 0x0000363C
		private bool ExportShellToPxml(ItDocument cncDoc, AssemblyInstance assemblyInstance, CNCProjectData projectData, ItMachineDataWallDB.DbwElementData.Shell shell, int doubleWallGapInMM)
		{
			ItGeMatrix3d matWcsToPalette = base.MatWcsToPalette;
			base.MatWcsToPalette = shell.Transformation;
			this.Mode = shell.Mode;
			bool flag = base.ExportPxml(assemblyInstance, new RevitElement<Part>[]
			{
				shell.Part
			}, cncDoc, projectData);
			bool flag2 = !flag;
			bool result;
			if (flag2)
			{
				result = flag;
			}
			else
			{
				base.MatWcsToPalette = matWcsToPalette;
				result = true;
			}
			return result;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000054A0 File Offset: 0x000036A0
		private static void DetermineDoubleWallGap(ItMachineDataWallDB.DbwElementData data)
		{
			double num = data.FirstShell.Part.thickness();
			double num2 = data.SecondShell.Part.thickness();
			double num3 = data.BoxOnPalette.maxPoint.z - data.BoxOnPalette.minPoint.z;
			double gap = num3 - num - num2;
			data.Gap = gap;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005500 File Offset: 0x00003700
		private static void DetermineCombinedBoundingBox(ItMachineDataWallDB.DbwElementData data)
		{
			ItDebug.assert(data != null, "Failure in: ");
			data.FirstShell.BoxOnPalette = data.FirstShell.Part.getWorldExtents(false).GetTransformed(data.MatWcsToPalette);
			data.SecondShell.BoxOnPalette = data.SecondShell.Part.getWorldExtents(false).GetTransformed(data.MatWcsToPalette);
			ItGeBoundBlock3d boxOnPalette = data.FirstShell.BoxOnPalette;
			ItDebug.assert(boxOnPalette != null && data.SecondShell.BoxOnPalette != null, "Failure in: ");
			boxOnPalette.extend(data.SecondShell.BoxOnPalette.minPoint);
			boxOnPalette.extend(data.SecondShell.BoxOnPalette.maxPoint);
			data.BoxOnPalette = boxOnPalette;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000055D0 File Offset: 0x000037D0
		protected override bool writeRebar()
		{
			return true;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000055E4 File Offset: 0x000037E4
		protected override Func<FamilyInstance, bool> GetFilterForValidMountingParts()
		{
			Func<FamilyInstance, bool> filterForValidMountingParts = base.GetFilterForValidMountingParts();
			return filterForValidMountingParts.And(new Func<FamilyInstance, bool>(this.IsCorrectShell));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005610 File Offset: 0x00003810
		protected override Func<Element, bool> GetReinforcementFilter()
		{
			Func<Element, bool> reinforcementFilter = base.GetReinforcementFilter();
			return reinforcementFilter.And(new Func<Element, bool>(this.IsCorrectShell));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000563C File Offset: 0x0000383C
		private bool IsCorrectShell(Element instance)
		{
			RevitElement<Part> part = instance.getHostingPart();
			return part.shellType() == ((this.Mode == ItMachineDataWallDB.DoubleWallMode.SecondShell) ? ShellType.SecondShell : ShellType.FirstShell);
		}

		// Token: 0x02000050 RID: 80
		private enum DoubleWallMode
		{
			// Token: 0x040001FC RID: 508
			Undefined,
			// Token: 0x040001FD RID: 509
			FirstShell,
			// Token: 0x040001FE RID: 510
			SecondShell
		}

		// Token: 0x02000051 RID: 81
		private sealed class DbwElementData
		{
			// Token: 0x0600056C RID: 1388 RVA: 0x000156F8 File Offset: 0x000138F8
			public DbwElementData(ItGeMatrix3d matWcsToPalette, RevitElement<Part> firstShell, RevitElement<Part> secondShell)
			{
				ItDebug.assert(matWcsToPalette != null && firstShell != null && secondShell != null, "Failure in: ");
				this.MatWcsToPalette = matWcsToPalette;
				this.FirstShell = new ItMachineDataWallDB.DbwElementData.Shell(ItMachineDataWallDB.DoubleWallMode.FirstShell, firstShell);
				this.SecondShell = new ItMachineDataWallDB.DbwElementData.Shell(ItMachineDataWallDB.DoubleWallMode.SecondShell, secondShell);
			}

			// Token: 0x17000244 RID: 580
			// (get) Token: 0x0600056D RID: 1389 RVA: 0x00015755 File Offset: 0x00013955
			public ItGeMatrix3d MatWcsToPalette { get; }

			// Token: 0x17000245 RID: 581
			// (get) Token: 0x0600056E RID: 1390 RVA: 0x0001575D File Offset: 0x0001395D
			public ItMachineDataWallDB.DbwElementData.Shell FirstShell { get; }

			// Token: 0x17000246 RID: 582
			// (get) Token: 0x0600056F RID: 1391 RVA: 0x00015765 File Offset: 0x00013965
			public ItMachineDataWallDB.DbwElementData.Shell SecondShell { get; }

			// Token: 0x17000247 RID: 583
			// (get) Token: 0x06000570 RID: 1392 RVA: 0x0001576D File Offset: 0x0001396D
			// (set) Token: 0x06000571 RID: 1393 RVA: 0x00015775 File Offset: 0x00013975
			public double Gap { get; set; }

			// Token: 0x17000248 RID: 584
			// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001577E File Offset: 0x0001397E
			// (set) Token: 0x06000573 RID: 1395 RVA: 0x00015786 File Offset: 0x00013986
			public ItGeBoundBlock3d BoxOnPalette { get; set; }

			// Token: 0x0200008A RID: 138
			public class Shell
			{
				// Token: 0x0600062B RID: 1579 RVA: 0x000160E6 File Offset: 0x000142E6
				public Shell(ItMachineDataWallDB.DoubleWallMode mode, RevitElement<Part> part)
				{
					this.Mode = mode;
					this.Part = part;
				}

				// Token: 0x17000257 RID: 599
				// (get) Token: 0x0600062C RID: 1580 RVA: 0x000160FE File Offset: 0x000142FE
				public ItMachineDataWallDB.DoubleWallMode Mode { get; }

				// Token: 0x17000258 RID: 600
				// (get) Token: 0x0600062D RID: 1581 RVA: 0x00016106 File Offset: 0x00014306
				public RevitElement<Part> Part { get; }

				// Token: 0x17000259 RID: 601
				// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001610E File Offset: 0x0001430E
				// (set) Token: 0x0600062F RID: 1583 RVA: 0x00016116 File Offset: 0x00014316
				public ItGeMatrix3d Transformation { get; set; }

				// Token: 0x1700025A RID: 602
				// (get) Token: 0x06000630 RID: 1584 RVA: 0x0001611F File Offset: 0x0001431F
				// (set) Token: 0x06000631 RID: 1585 RVA: 0x00016127 File Offset: 0x00014327
				public ItGeVector3d ZeroOffset { get; set; }

				// Token: 0x1700025B RID: 603
				// (get) Token: 0x06000632 RID: 1586 RVA: 0x00016130 File Offset: 0x00014330
				// (set) Token: 0x06000633 RID: 1587 RVA: 0x00016138 File Offset: 0x00014338
				public ItGeBoundBlock3d BoxOnPalette { get; set; }
			}
		}
	}
}
