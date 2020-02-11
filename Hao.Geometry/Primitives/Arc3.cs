using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Arc3 : IEquatable<Arc3>
	{
		// Token: 0x0600003F RID: 63 RVA: 0x000028C7 File Offset: 0x00000AC7
		public Arc3(Circle3 circle, Angle startAngle, Angle deltaAngle)
		{
			this = default(Arc3);
			this.Circle = circle;
			this.StartAngle = startAngle;
			this.DeltaAngle = deltaAngle;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000028E5 File Offset: 0x00000AE5
		
		public static Arc3 FullUnitCircle
		{
			get
			{
				return new Arc3(Circle3.UnitCircle, Angle.Zero, Angle.FullCircle);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000028FB File Offset: 0x00000AFB
		
		public static Arc3 HalfUnitCircle
		{
			get
			{
				return new Arc3(Circle3.UnitCircle, Angle.Zero, Angle.HalfCircle);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002911 File Offset: 0x00000B11
		
		public static Arc3 UnitCircleQuadrant1
		{
			get
			{
				return new Arc3(Circle3.UnitCircle, Angle.Zero, Angle.Quadrant);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002927 File Offset: 0x00000B27
		
		public static Arc3 UnitCircleQuadrant2
		{
			get
			{
				return new Arc3(Circle3.UnitCircle, Angle.Quadrant, Angle.Quadrant);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000293D File Offset: 0x00000B3D
		
		public static Arc3 UnitCircleQuadrant3
		{
			get
			{
				return new Arc3(Circle3.UnitCircle, Angle.HalfCircle, Angle.Quadrant);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002953 File Offset: 0x00000B53
		
		public static Arc3 UnitCircleQuadrant4
		{
			get
			{
				return new Arc3(Circle3.UnitCircle, Angle.Quadrant * 3.0, Angle.Quadrant);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002977 File Offset: 0x00000B77
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000297F File Offset: 0x00000B7F
		public Circle3 Circle { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002988 File Offset: 0x00000B88
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002990 File Offset: 0x00000B90
		public Angle StartAngle { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002999 File Offset: 0x00000B99
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000029A1 File Offset: 0x00000BA1
		public Angle DeltaAngle { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000029AC File Offset: 0x00000BAC
		public Vector3 StartPoint
		{
			get
			{
				return this.Circle.GetEdgePoint(this.StartAngle);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000029D0 File Offset: 0x00000BD0
		public Vector3 EndPoint
		{
			get
			{
				return this.Circle.GetEdgePoint(this.StartAngle + this.DeltaAngle);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000029FC File Offset: 0x00000BFC
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(S:");
				stringBuilder.Append(this.StartAngle.DebuggerDisplay);
				stringBuilder.Append(" D:");
				stringBuilder.Append(this.DeltaAngle.DebuggerDisplay);
				stringBuilder.Append(" C:");
				stringBuilder.Append(this.Circle.DebuggerDisplay);
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A84 File Offset: 0x00000C84
		public Vector3 GetEdgePoint(Angle angle)
		{
			double num = angle.Radians + this.StartAngle.Radians;
			double num2 = Math.Min(this.StartAngle.Radians, this.StartAngle.Radians + this.DeltaAngle.Radians);
			double num3 = Math.Max(this.StartAngle.Radians, this.StartAngle.Radians + this.DeltaAngle.Radians);
			if (num < num2)
			{
				num = num2;
			}
			if (num > num3)
			{
				num = num3;
			}
			return this.Circle.GetEdgePoint(Angle.FromRadians(num));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B2C File Offset: 0x00000D2C
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Arc3 other = (Arc3)obj;
			return this.Equals(other);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B6C File Offset: 0x00000D6C
		public bool Equals(Arc3 other)
		{
			return this.Circle.Equals(other.Circle) && this.StartAngle.Equals(other.StartAngle) && this.DeltaAngle.Equals(other.DeltaAngle);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public override int GetHashCode()
		{
			return this.Circle.GetHashCode() ^ this.StartAngle.GetHashCode() ^ this.DeltaAngle.GetHashCode();
		}
	}
}
