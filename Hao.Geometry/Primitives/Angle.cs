using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Angle
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000231E File Offset: 0x0000051E
		
		public static Angle HalfCircle
		{
			get
			{
				return Angle.FromRadians(3.1415926535897931);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000232E File Offset: 0x0000052E
		
		public static Angle FullCircle
		{
			get
			{
				return Angle.FromRadians(6.2831853071795862);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000233E File Offset: 0x0000053E
		
		public static Angle Quadrant
		{
			get
			{
				return Angle.FromRadians(1.5707963267948966);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000234E File Offset: 0x0000054E
		
		public static Angle Zero
		{
			get
			{
				return Angle.FromRadians(0.0);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000235E File Offset: 0x0000055E
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002366 File Offset: 0x00000566
		public double Radians { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000236F File Offset: 0x0000056F
		public double Degrees
		{
			get
			{
				return this.Radians * 57.295779513082323;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002381 File Offset: 0x00000581
		public double Cos
		{
			get
			{
				return Math.Cos(this.Radians);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000238E File Offset: 0x0000058E
		public double Sin
		{
			get
			{
				return Math.Sin(this.Radians);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000239B File Offset: 0x0000059B
		public double Tan
		{
			get
			{
				return Math.Tan(this.Radians);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023A8 File Offset: 0x000005A8
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.Degrees
				}));
				stringBuilder.Append("°)");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002404 File Offset: 0x00000604
		public static Angle FromRadians(double radians)
		{
			return new Angle
			{
				Radians = radians
			};
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002422 File Offset: 0x00000622
		public static Angle FromDegrees(double degrees)
		{
			return Angle.FromRadians(degrees / 180.0 * 3.1415926535897931);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000243E File Offset: 0x0000063E
		public static Angle Abs(Angle angle)
		{
			return Angle.FromRadians(Math.Abs(angle.Radians));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002451 File Offset: 0x00000651
		public static Angle operator +(Angle left, Angle right)
		{
			return Angle.FromRadians(left.Radians + right.Radians);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002467 File Offset: 0x00000667
		public static Angle operator -(Angle left, Angle right)
		{
			return Angle.FromRadians(left.Radians - right.Radians);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000247D File Offset: 0x0000067D
		public static Angle operator -(Angle angle)
		{
			return Angle.FromRadians(-angle.Radians);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000248C File Offset: 0x0000068C
		public static Angle operator *(Angle angle, double scalar)
		{
			return Angle.FromRadians(angle.Radians * scalar);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000249C File Offset: 0x0000069C
		public static Angle operator *(double scalar, Angle angle)
		{
			return Angle.FromRadians(angle.Radians * scalar);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024AC File Offset: 0x000006AC
		public static Angle operator /(Angle angle, double scalar)
		{
			return Angle.FromRadians(angle.Radians / scalar);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024BC File Offset: 0x000006BC
		public static double operator /(Angle angle, Angle divisor)
		{
			return angle.Radians / divisor.Radians;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024CD File Offset: 0x000006CD
		public static Angle operator %(Angle angle, Angle divisor)
		{
			return Angle.FromRadians(angle.Radians % divisor.Radians);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024E3 File Offset: 0x000006E3
		public static bool operator <(Angle angle, Angle other)
		{
			return angle.Radians < other.Radians;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024F5 File Offset: 0x000006F5
		public static bool operator >(Angle angle, Angle other)
		{
			return angle.Radians > other.Radians;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002507 File Offset: 0x00000707
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Angle)obj);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002537 File Offset: 0x00000737
		public bool Equals(Angle other)
		{
			return this.Radians == other.Radians;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002548 File Offset: 0x00000748
		public override int GetHashCode()
		{
			return this.Radians.GetHashCode();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002563 File Offset: 0x00000763
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Degrees);
			stringBuilder.Append("°, ");
			stringBuilder.Append(this.Radians);
			return stringBuilder.ToString();
		}
	}
}
