using System;

namespace Hao.Geometry
{
	// Token: 0x02000002 RID: 2
	internal static class MathBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal static void Assert(bool value, string message)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002050 File Offset: 0x00000250
		internal static void AssertValid(double value)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002052 File Offset: 0x00000252
		internal static bool IsUnitVector(Vector2 v)
		{
			return Math.Abs(v.Length - 1.0) <= 1E-08;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002078 File Offset: 0x00000278
		internal static bool IsUnitVector(Vector3 v)
		{
			return Math.Abs(v.Length - 1.0) <= 1E-08;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000209E File Offset: 0x0000029E
		internal static void AssertUnitVector(Vector2 v)
		{
			if (!MathBase.IsUnitVector(v))
			{
				throw new ArgumentException("must be unit vector");
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020B3 File Offset: 0x000002B3
		internal static void AssertUnitVector(Vector3 v)
		{
			if (!MathBase.IsUnitVector(v))
			{
				throw new ArgumentException("must be unit vector");
			}
		}

		// Token: 0x04000001 RID: 1
		internal const double ZeroTolerance = 1E-08;
	}
}
