using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{
	
	[Serializable]
	public struct UnitVector3 : IEquatable<UnitVector3>
	{
		// Token: 0x06000222 RID: 546 RVA: 0x0000B144 File Offset: 0x00009344
		public UnitVector3(double x, double y, double z)
		{
			Vector3 vector = new Vector3(x, y, z);
			MathBase.AssertUnitVector(vector);
			this.v = vector;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000B168 File Offset: 0x00009368
		
		public static UnitVector3 UnitX
		{
			get
			{
				return new UnitVector3(1.0, 0.0, 0.0);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000B18A File Offset: 0x0000938A
		
		public static UnitVector3 UnitY
		{
			get
			{
				return new UnitVector3(0.0, 1.0, 0.0);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000B1AC File Offset: 0x000093AC
		
		public static UnitVector3 UnitZ
		{
			get
			{
				return new UnitVector3(0.0, 0.0, 1.0);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000B1D0 File Offset: 0x000093D0
		public double X
		{
			get
			{
				return this.v.X;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000B1EC File Offset: 0x000093EC
		public double Y
		{
			get
			{
				return this.v.Y;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000B208 File Offset: 0x00009408
		public double Z
		{
			get
			{
				return this.v.Z;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000B224 File Offset: 0x00009424
		public double Error
		{
			get
			{
				return Math.Abs(1.0 - Math.Sqrt(this.v.Length));
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000B254 File Offset: 0x00009454
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.UnitVectorComponent, new object[]
				{
					this.v.X
				}));
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.UnitVectorComponent, new object[]
				{
					this.v.Y
				}));
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.UnitVectorComponent, new object[]
				{
					this.v.Z
				}));
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000B333 File Offset: 0x00009533
		public static explicit operator Vector3(UnitVector3 unitVector3)
		{
			return unitVector3.v;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000B33B File Offset: 0x0000953B
		public static Vector3 operator *(UnitVector3 vector, double scalar)
		{
			return vector.v * scalar;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000B349 File Offset: 0x00009549
		public static Vector3 operator *(double scalar, UnitVector3 vector)
		{
			return scalar * vector.v;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000B357 File Offset: 0x00009557
		public static UnitVector3 operator -(UnitVector3 vector)
		{
			return new UnitVector3(-vector.X, -vector.Y, -vector.Z);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000B378 File Offset: 0x00009578
		public double Dot(Vector3 vector)
		{
			return this.v.Dot(vector);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000B394 File Offset: 0x00009594
		public double Dot(UnitVector3 vector)
		{
			return this.v.Dot(vector);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000B3B0 File Offset: 0x000095B0
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((UnitVector3)obj);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000B3E0 File Offset: 0x000095E0
		public bool Equals(UnitVector3 other)
		{
			return this.v.Equals(other.v);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000B404 File Offset: 0x00009604
		public override int GetHashCode()
		{
			return this.v.GetHashCode();
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000B428 File Offset: 0x00009628
		public Vector3 Cross(Vector3 vector)
		{
			return this.v.Cross(vector);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000B444 File Offset: 0x00009644
		public Vector3 Cross(UnitVector3 vector)
		{
			return this.v.Cross((Vector3)vector);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000B468 File Offset: 0x00009668
		public UnitVector3 UnitCross(Vector3 vector)
		{
			UnitVector3 result;
			if (this.v.Cross(vector).TryGetNormalized(out result))
			{
				return result;
			}
			throw new ArgumentException("UnitCross cannot be calculated.");
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000B49C File Offset: 0x0000969C
		public UnitVector3 UnitCross(UnitVector3 vector)
		{
			return this.UnitCross((Vector3)vector);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000B4AC File Offset: 0x000096AC
		public bool TryGetUnitCross(Vector3 vector, out UnitVector3 result)
		{
			return this.Cross(vector).TryGetNormalized(out result);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000B4CC File Offset: 0x000096CC
		public bool TryGetUnitCross(UnitVector3 vector, out UnitVector3 result)
		{
			return this.Cross(vector).TryGetNormalized(out result);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000B4EC File Offset: 0x000096EC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UnitVector3{X:");
			stringBuilder.Append(this.v.X);
			stringBuilder.Append(" Y:");
			stringBuilder.Append(this.v.Y);
			stringBuilder.Append(" Z:");
			stringBuilder.Append(this.v.Z);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x04000067 RID: 103
		
		private readonly Vector3 v;
	}
}
