using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct UnitVector2
	{

		public UnitVector2(double x, double y)
		{
			Vector2 vector = new Vector2(x, y);
			MathBase.AssertUnitVector(vector);
			this.v = vector;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000AEE3 File Offset: 0x000090E3
		public UnitVector2(Vector2 v)
		{
			MathBase.AssertUnitVector(v);
			this.v = v;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000AEF2 File Offset: 0x000090F2
		
		public static UnitVector2 UnitX
		{
			get
			{
				return new UnitVector2(1.0, 0.0);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000AF0B File Offset: 0x0000910B
		
		public static UnitVector2 UnitY
		{
			get
			{
				return new UnitVector2(0.0, 1.0);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000AF24 File Offset: 0x00009124
		public double X
		{
			get
			{
				return this.v.X;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000AF40 File Offset: 0x00009140
		public double Y
		{
			get
			{
				return this.v.Y;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000AF5C File Offset: 0x0000915C
		
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
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000AFFD File Offset: 0x000091FD
		public static explicit operator Vector2(UnitVector2 unitVector2)
		{
			return unitVector2.v;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000B005 File Offset: 0x00009205
		public static Vector2 operator *(UnitVector2 vector, double scalar)
		{
			return vector.v * scalar;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000B013 File Offset: 0x00009213
		public static Vector2 operator *(double scalar, UnitVector2 vector)
		{
			return scalar * vector.v;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000B021 File Offset: 0x00009221
		public static UnitVector2 operator -(UnitVector2 vector)
		{
			return new UnitVector2(-vector.X, -vector.Y);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000B038 File Offset: 0x00009238
		public double Dot(Vector2 vector)
		{
			return this.v.Dot(vector);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000B054 File Offset: 0x00009254
		public double Dot(UnitVector2 vector)
		{
			return this.v.Dot((Vector2)vector);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000B078 File Offset: 0x00009278
		public UnitVector2 Perpendicular()
		{
			return new UnitVector2(this.v.Y, -this.v.X);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000B0A7 File Offset: 0x000092A7
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((UnitVector2)obj);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000B0D8 File Offset: 0x000092D8
		public bool Equals(UnitVector2 other)
		{
			return this.v.Equals(other.v);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000B0FC File Offset: 0x000092FC
		public override int GetHashCode()
		{
			return this.v.GetHashCode();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000B120 File Offset: 0x00009320
		public override string ToString()
		{
			return this.v.ToString();
		}

		// Token: 0x04000066 RID: 102
		
		private readonly Vector2 v;
	}
}
