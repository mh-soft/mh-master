using System;
using System.Linq;
using Hao.Export.Geometry;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x02000007 RID: 7
	public class CNCDataBase
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002B90 File Offset: 0x00000D90
		public static string padString(string value, int characters)
		{
			return new string(value.Take(characters).ToArray<char>()).PadRight(characters);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002BBC File Offset: 0x00000DBC
		public static string paddedInt(int value, int digits, bool sign = false)
		{
			string str = new string('0', digits);
			string format = (sign ? "+" : string.Empty) + str + (sign ? (";-" + str) : string.Empty);
			return value.ToString(format);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002C0C File Offset: 0x00000E0C
		public static string paddedInt5(int value)
		{
			return CNCDataBase.paddedInt(value, 5, false);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002C28 File Offset: 0x00000E28
		public static string paddedInt3(int value)
		{
			return CNCDataBase.paddedInt(value, 3, false);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002C44 File Offset: 0x00000E44
		public static string toMMString(double value, int digits, bool sign = false)
		{
			int value2 = CNCDataBase.convertToMM(value);
			return CNCDataBase.paddedInt(value2, digits, sign);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002C68 File Offset: 0x00000E68
		public static string toSignedMMString(double value, int digits)
		{
			return CNCDataBase.toMMString(value, digits, true);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002C84 File Offset: 0x00000E84
		public static string toMMString5(double value, bool sign = false)
		{
			return CNCDataBase.toMMString(value, 5, sign);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public static string toSignedMMString5(double value)
		{
			return CNCDataBase.toSignedMMString(value, 5);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002CBC File Offset: 0x00000EBC
		public static string toMMString3(double value)
		{
			return CNCDataBase.toMMString(value, 3, false);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public static string pointAsMillimeter(XYZ point, Func<double, string> toString)
		{
			return string.Concat(new string[]
			{
				toString(point.X),
				" ",
				toString(point.Y),
				" ",
				toString(point.Z)
			});
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002D34 File Offset: 0x00000F34
		public static int convertToMM(double dValue)
		{
			return (int)Math.Round(dValue.convertFeetToUnit((DisplayUnitType)2), 0);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002D54 File Offset: 0x00000F54
		public static int[] toPointArray(ItGePoint3d point)
		{
			return new int[]
			{
				CNCDataBase.convertToMM(point.x),
				CNCDataBase.convertToMM(point.y),
				CNCDataBase.convertToMM(point.z)
			};
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002D98 File Offset: 0x00000F98
		public static int ConvertToMeter(double value)
		{
			return CNCDataBase.convertToMM(value / 1000.0);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002DBC File Offset: 0x00000FBC
		public static double ConvertToSquareMeter(double value, int decimals)
		{
			return Math.Round(value.FeetToMeter().FeetToMeter(), decimals);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public static double ConvertToCubeDecimeters(double volumeInFeet, int decimals)
		{
			return Math.Round(volumeInFeet.FeetToMeter().FeetToMeter().FeetToMeter() * 1000.0, decimals);
		}

		// Token: 0x04000013 RID: 19
		public const string UniDelimiter = " ";

		// Token: 0x04000014 RID: 20
		public const string UniEnd = "END";
	}
}
