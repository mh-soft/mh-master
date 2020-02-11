using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Vector4 : IEquatable<Vector4>
	{
	
		/// <summary>
		/// 构造函数，初始化当前的四维向量
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="w"></param>
		public Vector4(double x, double y, double z, double w)
		{
			MathBase.AssertValid(x);
			MathBase.AssertValid(y);
			MathBase.AssertValid(z);
			MathBase.AssertValid(w);
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000C212 File Offset: 0x0000A412
		
		public static Vector4 Zero
		{
			get
			{
				return new Vector4(0.0, 0.0, 0.0, 0.0);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000C23D File Offset: 0x0000A43D
		
		public static Vector4 UnitX
		{
			get
			{
				return new Vector4(1.0, 0.0, 0.0, 0.0);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000C268 File Offset: 0x0000A468
		
		public static Vector4 UnitY
		{
			get
			{
				return new Vector4(0.0, 1.0, 0.0, 0.0);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000C293 File Offset: 0x0000A493
		
		public static Vector4 UnitZ
		{
			get
			{
				return new Vector4(0.0, 0.0, 1.0, 0.0);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000C2BE File Offset: 0x0000A4BE
		
		public static Vector4 UnitW
		{
			get
			{
				return new Vector4(0.0, 0.0, 0.0, 1.0);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000C2E9 File Offset: 0x0000A4E9
		
		public static Vector4 One
		{
			get
			{
				return new Vector4(1.0, 1.0, 1.0, 1.0);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000C314 File Offset: 0x0000A514
		
		public static Vector4 MinValue
		{
			get
			{
				return new Vector4(double.MinValue, double.MinValue, double.MinValue, double.MinValue);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000C33F File Offset: 0x0000A53F
		
		public static Vector4 MaxValue
		{
			get
			{
				return new Vector4(double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000C36A File Offset: 0x0000A56A
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000C372 File Offset: 0x0000A572
		public double X
		{
			get
			{
				return this.x;
			}
			set
			{
				MathBase.AssertValid(value);
				this.x = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000C381 File Offset: 0x0000A581
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000C389 File Offset: 0x0000A589
		public double Y
		{
			get
			{
				return this.y;
			}
			set
			{
				MathBase.AssertValid(value);
				this.y = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000C398 File Offset: 0x0000A598
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000C3A0 File Offset: 0x0000A5A0
		public double Z
		{
			get
			{
				return this.z;
			}
			set
			{
				MathBase.AssertValid(value);
				this.z = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000C3AF File Offset: 0x0000A5AF
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000C3B7 File Offset: 0x0000A5B7
		public double W
		{
			get
			{
				return this.w;
			}
			set
			{
				MathBase.AssertValid(value);
				this.w = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000C3C6 File Offset: 0x0000A5C6
		public double Length
		{
			get
			{
				return Math.Sqrt(this.SquaredLength);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000C3D3 File Offset: 0x0000A5D3
		public double SquaredLength
		{
			get
			{
				return this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000C40C File Offset: 0x0000A60C
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.VectorComponent, new object[]
				{
					this.x
				}));
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.VectorComponent, new object[]
				{
					this.y
				}));
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.VectorComponent, new object[]
				{
					this.z
				}));
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.VectorComponent, new object[]
				{
					this.w
				}));
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170000FC RID: 252
		public double this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.X;
				case 1:
					return this.Y;
				case 2:
					return this.Z;
				case 3:
					return this.W;
				default:
					throw new KeyNotFoundException("Vector4, index out of range");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.X = value;
					return;
				case 1:
					this.Y = value;
					return;
				case 2:
					this.Z = value;
					return;
				case 3:
					this.W = value;
					return;
				default:
					throw new KeyNotFoundException("Vector4, index out of range");
				}
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000C59C File Offset: 0x0000A79C
		public static Vector4 operator +(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000C5EC File Offset: 0x0000A7EC
		public static Vector4 operator -(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000C63A File Offset: 0x0000A83A
		public static Vector4 operator *(Vector4 vector, double scalar)
		{
			return new Vector4(scalar * vector.X, scalar * vector.Y, scalar * vector.Z, scalar * vector.W);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000C665 File Offset: 0x0000A865
		public static Vector4 operator *(double scalar, Vector4 vector)
		{
			return new Vector4(scalar * vector.X, scalar * vector.Y, scalar * vector.Z, scalar * vector.W);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000C690 File Offset: 0x0000A890
		public static Vector4 operator *(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000C6E0 File Offset: 0x0000A8E0
		public static Vector4 operator /(Vector4 vector, double scalar)
		{
			Vector4 zero = Vector4.Zero;
			if (scalar != 0.0)
			{
				double num = 1.0 / scalar;
				zero.X = num * vector.X;
				zero.Y = num * vector.Y;
				zero.Z = num * vector.Z;
				zero.W = num * vector.W;
			}
			else
			{
				zero.X = double.MaxValue;
				zero.Y = double.MaxValue;
				zero.Z = double.MaxValue;
				zero.W = double.MaxValue;
			}
			return zero;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000C790 File Offset: 0x0000A990
		public static Vector4 operator /(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000C7DE File Offset: 0x0000A9DE
		public static Vector4 operator -(Vector4 vector)
		{
			return new Vector4(-vector.X, -vector.Y, -vector.Z, -vector.W);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000C805 File Offset: 0x0000AA05
		public double Dot(Vector4 vector)
		{
			return vector.X * this.X + vector.Y * this.Y + vector.Z * this.Z + vector.W * this.W;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000C842 File Offset: 0x0000AA42
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Vector4)obj);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000C872 File Offset: 0x0000AA72
		public bool Equals(Vector4 other)
		{
			return this.x == other.x && this.y == other.y && this.z == other.z && this.w == other.w;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000C8AE File Offset: 0x0000AAAE
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() ^ this.z.GetHashCode() ^ this.w.GetHashCode();
		}

		// Token: 0x0400006D RID: 109
		
		private double x;

		// Token: 0x0400006E RID: 110
		
		private double y;

		// Token: 0x0400006F RID: 111
		
		private double z;

		// Token: 0x04000070 RID: 112
		
		private double w;
	}
}
