using System;

namespace Hao.Geometry
{
	// Token: 0x02000086 RID: 134
	internal static class Vector3Factory
	{
		// Token: 0x060004DC RID: 1244 RVA: 0x000194CC File Offset: 0x000176CC
		internal static void GenerateComplementBasis(out UnitVector3 u, out UnitVector3 v, UnitVector3 w)
		{
			Vector3 vector = default(Vector3);
			Vector3 vector2 = default(Vector3);
			if (Math.Abs(w.X) >= Math.Abs(w.Y))
			{
				double num = 1.0 / Math.Sqrt(w.X * w.X + w.Z * w.Z);
				vector.X = -w.Z * num;
				vector.Y = 0.0;
				vector.Z = w.X * num;
				vector2.X = w.Y * vector.Z;
				vector2.Y = w.Z * vector.X - w.X * vector.Z;
				vector2.Z = -w.Y * vector.X;
			}
			else
			{
				double num = 1.0 / Math.Sqrt(w.Y * w.Y + w.Z * w.Z);
				vector.X = 0.0;
				vector.Y = w.Z * num;
				vector.Z = -w.Y * num;
				vector2.X = w.Y * vector.Z - w.Z * vector.Y;
				vector2.Y = -w.X * vector.Z;
				vector2.Z = w.X * vector.Y;
			}
			u = new UnitVector3(vector.X, vector.Y, vector.Z);
			v = new UnitVector3(vector2.X, vector2.Y, vector2.Z);
		}
	}
}
