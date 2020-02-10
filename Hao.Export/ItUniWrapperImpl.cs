using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Hao.Export.MachineData
{
	// Token: 0x0200001C RID: 28
	public static class ItUniWrapperImpl
	{
		// Token: 0x060000FC RID: 252
		[DllImport("UniWrapper.dll", CharSet = CharSet.Unicode)]
		private static extern int startOrder(int iProduct, int iElementId, double[] dProjectCoordinates, short iVersion, string strElemName, double dTotalArea, [MarshalAs(UnmanagedType.I1)] bool clearDocument);

		// Token: 0x060000FD RID: 253
		[DllImport("UniWrapper.dll", CharSet = CharSet.Unicode)]
		private static extern int endDocument(string strFilename);

		// Token: 0x060000FE RID: 254
		[DllImport("UniWrapper.dll", CharSet = CharSet.Unicode)]
		private static extern int addSet(int iType, string strName, int iCount, int[] iParameter);

		// Token: 0x060000FF RID: 255
		[DllImport("UniWrapper.dll", CharSet = CharSet.Unicode)]
		private static extern int addSteelmat(string matName, int maxLen, int maxWidth, int toTurn, int stopOnTurn, int matType, int[] pos);

		// Token: 0x06000100 RID: 256
		[DllImport("UniWrapper.dll", CharSet = CharSet.Unicode)]
		private static extern int addCFSRodstock(int reinfType, string steelQuality, int numberOfIrons, int diameter, int length, int[] startpoint, int pitch, int angle, string articleNumber, int automaticProduction, int spacerType, int spacerStartpoint, int spacerPitch, int additionalStore);

		// Token: 0x06000101 RID: 257
		[DllImport("UniWrapper.dll", CharSet = CharSet.Unicode, EntryPoint = "addExtIron")]
		private static extern int addStandardFabricSheet(int type, int count, double weightKg, string sheetName);

		// Token: 0x06000102 RID: 258
		[DllImport("UniWrapper.dll", CharSet = CharSet.Unicode)]
		private static extern int addFreeFormRodstock(ItUniWrapperImpl.RodstockData data, ItUniWrapperImpl.FreeFormSegment[] segments);

		// Token: 0x06000103 RID: 259
		[DllImport("UniWrapper.dll", CharSet = CharSet.Unicode)]
		private static extern void addCutout(ItUniWrapperImpl.CutoutData cutoutData, ItUniWrapperImpl.Point2D[] points);

		// Token: 0x06000104 RID: 260
		[DllImport("UniWrapper.dll", CharSet = CharSet.Unicode)]
		private static extern void addContour(int countOfPoints, ItUniWrapperImpl.Point2D[] points);

		// Token: 0x06000105 RID: 261
		[DllImport("UniWrapper.dll", CharSet = CharSet.Unicode)]
		private static extern void addStraightRodstock(ItUniWrapperImpl.RodstockData rsData);

		// Token: 0x06000106 RID: 262
		[DllImport("UniWrapper.dll", CharSet = CharSet.Unicode)]
		private static extern void setSlabInfoFields(string info1, string info2, string info3, string info4);

		// Token: 0x06000107 RID: 263
		[DllImport("UniWrapper.dll", CharSet = CharSet.Unicode)]
		private static extern void setProjectData(CNCProjectData data);

		// Token: 0x06000108 RID: 264
		[DllImport("UniWrapper.dll", CharSet = CharSet.Unicode)]
		private static extern void addLayer(int layerType, double dThickness, double dUnitWeight, double dVolume, string sConcreteQuality);

		// Token: 0x06000109 RID: 265
		[DllImport("UniWrapper.dll", CharSet = CharSet.Unicode)]
		private static extern void setThicknesses(int totalThickness, int productionThickness);

		// Token: 0x0600010A RID: 266
		[DllImport("UniWrapper.dll", CharSet = CharSet.Unicode)]
		private static extern void setDoubleWallGap(int gap);

		// Token: 0x0600010B RID: 267
		[DllImport("UniWrapper.dll", CharSet = CharSet.Unicode)]
		private static extern string getFullReinforcementString();

		// Token: 0x0600010C RID: 268 RVA: 0x00009668 File Offset: 0x00007868
		internal static int StartDocument(int iProduct, int iElementId, ProjectCoordinates projectCoordinates, short iVersion, string strElemName, double dTotalArea, bool clearDocument)
		{
			ItDebug.assert(iProduct >= 0 && iProduct < 100, "product type must be between 0 and 99");
			ItDebug.assert(iElementId >= 0 || iElementId < 999, "elementId must be between 0 and 999");
			bool flag = iProduct < 0 || iProduct >= 100;
			if (flag)
			{
				throw new ArgumentOutOfRangeException();
			}
			bool flag2 = iElementId < 0 && iElementId >= 999;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException();
			}
			strElemName = ((strElemName.Length > 30) ? strElemName.Substring(0, 30) : strElemName);
			double[] array = projectCoordinates.getArray();
			return ItUniWrapperImpl.startOrder(iProduct, iElementId, array, iVersion, strElemName, dTotalArea, clearDocument);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00009714 File Offset: 0x00007914
		internal static int EndDocument(string strFilename)
		{
			ItDebug.assert(!string.IsNullOrWhiteSpace(strFilename), "Failure in: ");
			bool flag = string.IsNullOrWhiteSpace(strFilename);
			if (flag)
			{
				throw new ArgumentException();
			}
			return ItUniWrapperImpl.endDocument(strFilename);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00009750 File Offset: 0x00007950
		internal static int AddMountPart(string mountPartName, int[] iParameter)
		{
			ItDebug.assert(mountPartName != null, "cant handle null as mountPartName");
			ItDebug.assert(iParameter.Length % 4 == 2, "parameter array for mount parts must be array of pairs of 4 elements, plus two (the first two)");
			bool flag = mountPartName == null;
			if (flag)
			{
				throw new ArgumentException();
			}
			bool flag2 = iParameter.Length % 4 != 2;
			if (flag2)
			{
				throw new ArgumentException();
			}
			mountPartName = ((mountPartName.Length > 40) ? mountPartName.Substring(0, 40) : mountPartName);
			int iCount = iParameter.Length;
			return ItUniWrapperImpl.addSet(3, mountPartName, iCount, iParameter);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000097CC File Offset: 0x000079CC
		internal static int AddGirder(string girderName, int[] iParameter)
		{
			ItDebug.assert(girderName != null, "cant handle null as mountPartName");
			ItDebug.assert(iParameter.Length % 4 == 2, "parameter array for mount parts must be array of pairs of 4 elements, plus two (the first two)");
			bool flag = girderName == null;
			if (flag)
			{
				throw new ArgumentException();
			}
			bool flag2 = iParameter.Length % 4 != 2;
			if (flag2)
			{
				throw new ArgumentException();
			}
			girderName = ((girderName.Length > 10) ? girderName.Substring(0, 10) : girderName);
			int iCount = iParameter.Length;
			return ItUniWrapperImpl.addSet(5, girderName, iCount, iParameter);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00009848 File Offset: 0x00007A48
		internal static int AddSteelmat(string matName, int maxLen, int maxWidth, int toTurn, int stopOnTurn, int matType, int[] pos)
		{
			ItDebug.assert(matName != null, "cant handle null as steelmat name");
			ItDebug.assert(maxLen > 0 && maxWidth > 0, "cant have negative length or width");
			ItDebug.assert(toTurn == 0 || toTurn == 1, "toTurn must be 0 or 1");
			ItDebug.assert(stopOnTurn == 0 || stopOnTurn == 1, "stopOnTurn must be 0 or 1");
			ItDebug.assert((matType >= 0 && matType <= 4) || matType == 8, "invalid mattype");
			ItDebug.assert(pos.Length == 3, "position must be a 3d-point");
			bool flag = matName == null;
			if (flag)
			{
				throw new ArgumentException();
			}
			bool flag2 = maxLen <= 0 || maxWidth <= 0;
			if (flag2)
			{
				throw new ArgumentException();
			}
			bool flag3 = toTurn != 0 && toTurn != 1;
			if (flag3)
			{
				throw new ArgumentException();
			}
			bool flag4 = stopOnTurn != 0 && stopOnTurn != 1;
			if (flag4)
			{
				throw new ArgumentException();
			}
			bool flag5 = (matType < 0 || matType > 4) && matType != 8;
			if (flag5)
			{
				throw new ArgumentException();
			}
			bool flag6 = pos.Length != 3;
			if (flag6)
			{
				throw new ArgumentException();
			}
			matName = ((matName.Length > 20) ? matName.Substring(0, 20) : matName);
			return ItUniWrapperImpl.addSteelmat(matName, maxLen, maxWidth, toTurn, stopOnTurn, matType, pos);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00009990 File Offset: 0x00007B90
		internal static int AddCFSRodstock(int reinfType, string steelQuality, int numberOfIrons, int diameter, int length, int[] startpoint, int pitch, int angle, string articleNumber, int automaticProduction, int spacerType, int spacerStartpoint, int spacerPitch, int additionalStore)
		{
			ItDebug.assert(reinfType >= 0 && reinfType < 9, "invalid reinfType");
			ItDebug.assert(steelQuality != null, "cant handle null as steel quality");
			ItDebug.assert(numberOfIrons > 0, "cant handle 0 or negative iron number");
			ItDebug.assert(diameter > 0, "cant handle 0 or negative diameter");
			ItDebug.assert(length > 0, "cant handle 0 or negative iron length");
			ItDebug.assert(startpoint.Length == 3, "Startpoint must be a 3d point");
			ItDebug.assert(articleNumber != null, "cant handle null as article number");
			ItDebug.assert(automaticProduction == 0 || automaticProduction == 1, "automaticProduction must be 0 or 1");
			ItDebug.assert(additionalStore >= 0 && additionalStore <= 112, "additional store contains invalid flags");
			bool flag = reinfType < 0 || reinfType >= 9;
			if (flag)
			{
				throw new ArgumentException();
			}
			bool flag2 = steelQuality == null;
			if (flag2)
			{
				throw new ArgumentException();
			}
			bool flag3 = numberOfIrons <= 0;
			if (flag3)
			{
				throw new ArgumentException();
			}
			bool flag4 = diameter <= 0;
			if (flag4)
			{
				throw new ArgumentException();
			}
			bool flag5 = length <= 0;
			if (flag5)
			{
				throw new ArgumentException();
			}
			bool flag6 = startpoint.Length != 3;
			if (flag6)
			{
				throw new ArgumentException();
			}
			bool flag7 = articleNumber == null;
			if (flag7)
			{
				throw new ArgumentException();
			}
			bool flag8 = automaticProduction != 0 && automaticProduction != 1;
			if (flag8)
			{
				throw new ArgumentException();
			}
			bool flag9 = additionalStore < 0 || additionalStore > 112;
			if (flag9)
			{
				throw new ArgumentException();
			}
			return ItUniWrapperImpl.addCFSRodstock(reinfType, steelQuality, numberOfIrons, diameter, length, startpoint, pitch, angle, articleNumber, automaticProduction, spacerType, spacerStartpoint, spacerPitch, additionalStore);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00009B24 File Offset: 0x00007D24
		internal static int AddStandardFabricSheet(int type, int count, double weightKg, string sheetName)
		{
			ItDebug.assert(sheetName != null, "cant handle null as sheetname");
			bool flag = sheetName == null;
			if (flag)
			{
				throw new ArgumentException();
			}
			return ItUniWrapperImpl.addStandardFabricSheet(type, count, weightKg, sheetName);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00009B5C File Offset: 0x00007D5C
		internal static void AddFreeFormRodstock(ItUniWrapperImpl.RodstockData data, IEnumerable<ItUniWrapperImpl.FreeFormSegment> segments)
		{
			ItDebug.assert(segments.any<ItUniWrapperImpl.FreeFormSegment>(), "Can not handle empty List of FreeFormSegments");
			ItUniWrapperImpl.FreeFormSegment[] array = segments.ToArray<ItUniWrapperImpl.FreeFormSegment>();
			data.numberOfFreeFormSegments = (ushort)array.Length;
			ItUniWrapperImpl.addFreeFormRodstock(data, array);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00009B98 File Offset: 0x00007D98
		internal static void AddCutout(ItUniWrapperImpl.CutoutData data, IEnumerable<ItUniWrapperImpl.Point2D> points)
		{
			ItDebug.assert(points.any<ItUniWrapperImpl.Point2D>(), "Can not handle empty List of Points");
			ItUniWrapperImpl.Point2D[] array = points.ToArray<ItUniWrapperImpl.Point2D>();
			data.countOfPoints = (ushort)array.Length;
			ItUniWrapperImpl.addCutout(data, array);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00009BD4 File Offset: 0x00007DD4
		internal static void AddContour(List<ItUniWrapperImpl.Point2D> points)
		{
			ItDebug.assert(points.any<ItUniWrapperImpl.Point2D>(), "Can not handle empty List of Points");
			ItUniWrapperImpl.Point2D[] array = points.ToArray();
			int countOfPoints = array.Length;
			ItUniWrapperImpl.addContour(countOfPoints, array);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00009C06 File Offset: 0x00007E06
		internal static void AddStraightRodstock(ItUniWrapperImpl.RodstockData data)
		{
			ItUniWrapperImpl.addStraightRodstock(data);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00009C10 File Offset: 0x00007E10
		public static void SetSlabInfoFields(string info1, string info2, string info3, string info4)
		{
			info1 = (info1 ?? string.Empty);
			info2 = (info2 ?? string.Empty);
			info3 = (info3 ?? string.Empty);
			info4 = (info4 ?? string.Empty);
			ItUniWrapperImpl.setSlabInfoFields(info1, info2, info3, info4);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00009C4D File Offset: 0x00007E4D
		internal static void SetProjectData(CNCProjectData data)
		{
			ItDebug.assert(data.CheckNoNullFields(), "no fields must be null in the CNCProjectData, use string.Empty instead");
			ItUniWrapperImpl.setProjectData(data);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00009C68 File Offset: 0x00007E68
		public static void AddLayer(int layerType, double dThickness, double dUnitWeight, double dVolume, string sConcreteQuality)
		{
			ItDebug.assert(sConcreteQuality != null, "No strings should be null when being sent to UniWrapper");
			sConcreteQuality = (sConcreteQuality ?? string.Empty);
			ItUniWrapperImpl.addLayer(layerType, dThickness, dUnitWeight, dVolume, sConcreteQuality);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00009C94 File Offset: 0x00007E94
		public static void WriteThicknesses(int totalThickness, int productionThickness)
		{
			ItDebug.assert(totalThickness.Ge(0), "total thickness can not be negative");
			ItDebug.assert(productionThickness.Ge(0), "production thickness can not be negative");
			ItDebug.assert(productionThickness.Ge(totalThickness), "production thickness can not be smaller than total thickness");
			ItUniWrapperImpl.setThicknesses(totalThickness, productionThickness);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009CE0 File Offset: 0x00007EE0
		public static void SetDoubleWallGap(int doubleWallGap)
		{
			ItUniWrapperImpl.setDoubleWallGap(doubleWallGap);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00009CEC File Offset: 0x00007EEC
		public static string GetFullReinforcementString()
		{
			return ItUniWrapperImpl.getFullReinforcementString();
		}

		// Token: 0x04000048 RID: 72
		private const string dllname = "UniWrapper.dll";

		// Token: 0x0200005E RID: 94
		[StructLayout(LayoutKind.Sequential, Pack = 8, Size = 64)]
		public struct RodstockData
		{
			// Token: 0x04000247 RID: 583
			[MarshalAs(UnmanagedType.LPWStr)]
			public string artNr;

			// Token: 0x04000248 RID: 584
			[MarshalAs(UnmanagedType.LPWStr)]
			public string steelQuality;

			// Token: 0x04000249 RID: 585
			public uint startingPointSpacer;

			// Token: 0x0400024A RID: 586
			public uint pitchSpacer;

			// Token: 0x0400024B RID: 587
			public uint installationHeight;

			// Token: 0x0400024C RID: 588
			public uint length;

			// Token: 0x0400024D RID: 589
			public int xcoord;

			// Token: 0x0400024E RID: 590
			public int ycoord;

			// Token: 0x0400024F RID: 591
			public int pitch;

			// Token: 0x04000250 RID: 592
			public ushort number;

			// Token: 0x04000251 RID: 593
			public ushort diameter;

			// Token: 0x04000252 RID: 594
			public short startAngle;

			// Token: 0x04000253 RID: 595
			public ushort spacerType;

			// Token: 0x04000254 RID: 596
			public ushort flexFormNumber;

			// Token: 0x04000255 RID: 597
			public ushort numberOfFreeFormSegments;

			// Token: 0x04000256 RID: 598
			public byte reinfType;

			// Token: 0x04000257 RID: 599
			public byte autoProd;

			// Token: 0x04000258 RID: 600
			public byte hasSpacers;

			// Token: 0x04000259 RID: 601
			public byte hasWielding;

			// Token: 0x0400025A RID: 602
			public byte formType;
		}

		// Token: 0x0200005F RID: 95
		public static class AutoProd
		{
			// Token: 0x0400025B RID: 603
			public const int Yes = 0;

			// Token: 0x0400025C RID: 604
			public const int No = 1;
		}

		// Token: 0x02000060 RID: 96
		public static class FormType
		{
			// Token: 0x0400025D RID: 605
			public const int NoBending = 0;

			// Token: 0x0400025E RID: 606
			public const int WithHooks = 1;

			// Token: 0x0400025F RID: 607
			public const int FreeForm = 2;
		}

		// Token: 0x02000061 RID: 97
		public static class Spacers
		{
			// Token: 0x04000260 RID: 608
			public const int Yes = 1;

			// Token: 0x04000261 RID: 609
			public const int No = 0;
		}

		// Token: 0x02000062 RID: 98
		public static class WeldingPoints
		{
			// Token: 0x04000262 RID: 610
			public const int Yes = 1;

			// Token: 0x04000263 RID: 611
			public const int No = 0;
		}

		// Token: 0x02000063 RID: 99
		[StructLayout(LayoutKind.Sequential, Pack = 8, Size = 8)]
		public struct FreeFormSegment
		{
			// Token: 0x04000264 RID: 612
			public uint length;

			// Token: 0x04000265 RID: 613
			public short angle;
		}

		// Token: 0x02000064 RID: 100
		[StructLayout(LayoutKind.Sequential, Pack = 8, Size = 24)]
		public struct CutoutData
		{
			// Token: 0x060005C3 RID: 1475 RVA: 0x00015B59 File Offset: 0x00013D59
			public CutoutData(string name, double area)
			{
				this = default(ItUniWrapperImpl.CutoutData);
				this.name = name;
				this.area = area;
			}

			// Token: 0x04000266 RID: 614
			[MarshalAs(UnmanagedType.LPWStr)]
			public readonly string name;

			// Token: 0x04000267 RID: 615
			public readonly double area;

			// Token: 0x04000268 RID: 616
			internal ushort countOfPoints;
		}

		// Token: 0x02000065 RID: 101
		[StructLayout(LayoutKind.Sequential, Pack = 8, Size = 8)]
		public struct Point2D
		{
			// Token: 0x060005C4 RID: 1476 RVA: 0x00015B71 File Offset: 0x00013D71
			public Point2D(int x, int y)
			{
				this.x = x;
				this.y = y;
			}

			// Token: 0x04000269 RID: 617
			public readonly int x;

			// Token: 0x0400026A RID: 618
			public readonly int y;
		}
	}
}
