using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Quaternion : IEquatable<Quaternion>
	{

		public Quaternion(double w, double x, double y, double z)
		{
			this = default(Quaternion);
			this.W = w;
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000091D1 File Offset: 0x000073D1
		public Quaternion(double w, Vector3 v)
		{
			this = default(Quaternion);
			this.W = w;
			this.X = v.X;
			this.Y = v.Y;
			this.Z = v.Z;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00009208 File Offset: 0x00007408
		
		public static Quaternion Zero
		{
			get
			{
				return new Quaternion(0.0, 0.0, 0.0, 0.0);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00009233 File Offset: 0x00007433
		
		public static Quaternion Identity
		{
			get
			{
				return new Quaternion(1.0, 0.0, 0.0, 0.0);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000925E File Offset: 0x0000745E
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00009266 File Offset: 0x00007466
		public double W { get; private set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000926F File Offset: 0x0000746F
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00009277 File Offset: 0x00007477
		public double X { get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00009280 File Offset: 0x00007480
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00009288 File Offset: 0x00007488
		public double Y { get; private set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00009291 File Offset: 0x00007491
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00009299 File Offset: 0x00007499
		public double Z { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000092A2 File Offset: 0x000074A2
		public double Length
		{
			get
			{
				return Math.Sqrt(this.SquaredLength);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000092AF File Offset: 0x000074AF
		public double SquaredLength
		{
			get
			{
				return this.W * this.W + this.X * this.X + this.Y * this.Y + this.Z * this.Z;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000092E8 File Offset: 0x000074E8
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.VectorComponent, new object[]
				{
					this.W
				}));
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.VectorComponent, new object[]
				{
					this.X
				}));
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.VectorComponent, new object[]
				{
					this.Y
				}));
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.VectorComponent, new object[]
				{
					this.Z
				}));
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x1700009B RID: 155
		internal double this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.W;
				case 1:
					return this.X;
				case 2:
					return this.Y;
				case 3:
					return this.Z;
				default:
					throw new KeyNotFoundException("Quaternion[].get: invalid index");
				}
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00009428 File Offset: 0x00007628
		public static Quaternion operator +(Quaternion left, Quaternion right)
		{
			return new Quaternion(left.W + right.W, left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00009478 File Offset: 0x00007678
		public static Quaternion operator -(Quaternion left, Quaternion right)
		{
			return new Quaternion(left.W - right.W, left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000094C8 File Offset: 0x000076C8
		public static Quaternion operator *(Quaternion left, Quaternion right)
		{
			double w = left.W * right.W - left.X * right.X - left.Y * right.Y - left.Z * right.Z;
			double x = left.W * right.X + left.X * right.W + left.Y * right.Z - left.Z * right.Y;
			double y = left.W * right.Y + left.Y * right.W + left.Z * right.X - left.X * right.Z;
			double z = left.W * right.Z + left.Z * right.W + left.X * right.Y - left.Y * right.X;
			return new Quaternion(w, x, y, z);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000095DC File Offset: 0x000077DC
		public static Quaternion operator *(Quaternion quaternion, double scalar)
		{
			return new Quaternion(quaternion.W * scalar, quaternion.X * scalar, quaternion.Y * scalar, quaternion.Z * scalar);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00009607 File Offset: 0x00007807
		public static Quaternion operator *(double scalar, Quaternion quaternion)
		{
			return new Quaternion(quaternion.W * scalar, quaternion.X * scalar, quaternion.Y * scalar, quaternion.Z * scalar);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00009634 File Offset: 0x00007834
		public static Quaternion operator /(Quaternion quaternion, double scalar)
		{
			if (scalar != 0.0)
			{
				double scalar2 = 1.0 / scalar;
				return quaternion * scalar2;
			}
			return new Quaternion(double.MaxValue, double.MaxValue, double.MaxValue, double.MaxValue);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000968A File Offset: 0x0000788A
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Quaternion)obj);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000096BA File Offset: 0x000078BA
		public bool Equals(Quaternion other)
		{
			return this.W == other.W && this.X == other.X && this.Y == other.Y && this.Z == other.Z;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000096FC File Offset: 0x000078FC
		public override int GetHashCode()
		{
			return this.W.GetHashCode() ^ this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00009744 File Offset: 0x00007944
		public double Dot(Quaternion quaternion)
		{
			return this.W * quaternion.W + this.X * quaternion.X + this.Y * quaternion.Y + this.Z * quaternion.Z;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009784 File Offset: 0x00007984
		public double TryGetNormalized(out Quaternion quaternion)
		{
			double length = this.Length;
			if (length > 1E-08)
			{
				double num = 1.0 / length;
				quaternion = new Quaternion(this.W * num, this.X * num, this.Y * num, this.Z * num);
				return length;
			}
			quaternion = Quaternion.Zero;
			return 0.0;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000097F4 File Offset: 0x000079F4
		public Quaternion Inverse()
		{
			double num = this.W * this.W + this.X * this.X + this.Y * this.Y + this.Z * this.Z;
			if (num > 0.0)
			{
				double num2 = 1.0 / num;
				return new Quaternion(this.W * num2, this.X * num2, this.Y * num2, this.Z * num2);
			}
			return Quaternion.Zero;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000987C File Offset: 0x00007A7C
		public Quaternion Conjugate()
		{
			return new Quaternion(this.W, -this.X, -this.Y, -this.Z);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000098A0 File Offset: 0x00007AA0
		public Quaternion Exp()
		{
			double num = Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
			double num2 = Math.Sin(num);
			if (Math.Abs(num2) >= 1E-08)
			{
				double num3 = num2 / num;
				return new Quaternion(Math.Cos(num), num3 * this.X, num3 * this.Y, num3 * this.Z);
			}
			return Quaternion.Zero;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00009924 File Offset: 0x00007B24
		public Quaternion Log()
		{
			if (Math.Abs(this.W) < 1.0)
			{
				double num = Math.Acos(this.W);
				double num2 = Math.Sin(num);
				if (Math.Abs(num2) >= 1E-08)
				{
					double num3 = num / num2;
					return new Quaternion(0.0, num3 * this.X, num3 * this.Y, num3 * this.Z);
				}
			}
			return new Quaternion(0.0, this.X, this.Y, this.Z);
		}
	}
}
