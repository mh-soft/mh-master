using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Arc2 : IEquatable<Arc2>
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002595 File Offset: 0x00000795
		public Arc2(Circle2 circle, Angle startAngle, Angle deltaAngle)
		{
			this = default(Arc2);
			this.Circle = circle;
			this.StartAngle = startAngle;
			this.DeltaAngle = deltaAngle;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000025B3 File Offset: 0x000007B3
		
		public static Arc2 FullUnitCircle
		{
			get
			{
				return new Arc2(Circle2.UnitCircle, Angle.Zero, Angle.FullCircle);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000025C9 File Offset: 0x000007C9
		
		public static Arc2 HalfUnitCircle
		{
			get
			{
				return new Arc2(Circle2.UnitCircle, Angle.Zero, Angle.HalfCircle);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000025DF File Offset: 0x000007DF
		
		public static Arc2 UnitCircleQuadrant1
		{
			get
			{
				return new Arc2(Circle2.UnitCircle, Angle.Zero, Angle.Quadrant);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000025F5 File Offset: 0x000007F5
		
		public static Arc2 UnitCircleQuadrant2
		{
			get
			{
				return new Arc2(Circle2.UnitCircle, Angle.Quadrant, Angle.Quadrant);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000260B File Offset: 0x0000080B
		
		public static Arc2 UnitCircleQuadrant3
		{
			get
			{
				return new Arc2(Circle2.UnitCircle, Angle.HalfCircle, Angle.Quadrant);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002621 File Offset: 0x00000821
		
		public static Arc2 UnitCircleQuadrant4
		{
			get
			{
				return new Arc2(Circle2.UnitCircle, Angle.Quadrant * 3.0, Angle.Quadrant);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002645 File Offset: 0x00000845
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000264D File Offset: 0x0000084D
		public Circle2 Circle { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002656 File Offset: 0x00000856
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000265E File Offset: 0x0000085E
		public Angle StartAngle { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002667 File Offset: 0x00000867
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000266F File Offset: 0x0000086F
		public Angle DeltaAngle { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002678 File Offset: 0x00000878
		public Vector2 StartPoint
		{
			get
			{
				return this.Circle.GetEdgePoint(this.StartAngle);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000269C File Offset: 0x0000089C
		public Vector2 EndPoint
		{
			get
			{
				return this.Circle.GetEdgePoint(this.StartAngle + this.DeltaAngle);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000026C8 File Offset: 0x000008C8
		
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

		// Token: 0x0600003B RID: 59 RVA: 0x00002750 File Offset: 0x00000950
		public Vector2 GetEdgePoint(Angle angle)
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

		// Token: 0x0600003C RID: 60 RVA: 0x000027F7 File Offset: 0x000009F7
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Arc2)obj);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002828 File Offset: 0x00000A28
		public bool Equals(Arc2 other)
		{
			return this.Circle.Equals(other.Circle) && this.StartAngle.Equals(other.StartAngle) && this.DeltaAngle.Equals(other.DeltaAngle);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000287C File Offset: 0x00000A7C
		public override int GetHashCode()
		{
			return this.Circle.GetHashCode() ^ this.StartAngle.GetHashCode() ^ this.DeltaAngle.GetHashCode();
		}
	}
}
