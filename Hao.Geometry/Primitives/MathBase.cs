using System;

namespace Hao.Geometry
{
	// Token: 0x02000015 RID: 21
	internal static class MathBase
	{
		// Token: 0x0600011D RID: 285 RVA: 0x0000539D File Offset: 0x0000359D
		internal static void Assert(bool value, string message)
		{
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000539D File Offset: 0x0000359D
		internal static void AssertValid(double value)
		{
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000539F File Offset: 0x0000359F
		internal static bool IsUnitVector(Vector2 v)
		{
			return Math.Abs(v.Length - 1.0) <= 1E-08;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000053C5 File Offset: 0x000035C5
		internal static bool IsUnitVector(Vector3 v)
		{
			return Math.Abs(v.Length - 1.0) <= 1E-08;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000053EB File Offset: 0x000035EB
		internal static void AssertUnitVector(Vector2 v)
		{
			if (!MathBase.IsUnitVector(v))
			{
				throw new ArgumentException("must be unit vector");
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005400 File Offset: 0x00003600
		internal static void AssertUnitVector(Vector3 v)
		{
			if (!MathBase.IsUnitVector(v))
			{
				throw new ArgumentException("must be unit vector");
			}
		}

		// Token: 0x0400003E RID: 62
		internal const double ZeroTolerance = 1E-08;
	}
}
