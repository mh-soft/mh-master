using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Circle2 : IEquatable<Circle2>
	{
	
		public Circle2(Vector2 center, double radius)
		{
			this = default(Circle2);
			this.Center = center;
			this.Radius = radius;
		}

		
		public static Circle2 UnitCircle
		{
			get
			{
				return new Circle2(Vector2.Zero, 1.0);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003E2D File Offset: 0x0000202D
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003E35 File Offset: 0x00002035
		public Vector2 Center { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003E3E File Offset: 0x0000203E
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003E46 File Offset: 0x00002046
		public double Radius { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003E4F File Offset: 0x0000204F
		public double Diameter
		{
			get
			{
				return this.Radius * 2.0;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003E61 File Offset: 0x00002061
		public double Area
		{
			get
			{
				return 3.1415926535897931 * this.Radius * this.Radius;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003E7C File Offset: 0x0000207C
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(C:");
				stringBuilder.Append(this.Center.DebuggerDisplay);
				stringBuilder.Append(" R:");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.Radius
				}));
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003EF8 File Offset: 0x000020F8
		public Vector2 GetEdgePoint(Angle angle)
		{
			return this.Center + this.Radius * this.GetRadiusDirection(angle);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003F17 File Offset: 0x00002117
		public UnitVector2 GetRadiusDirection(Angle angle)
		{
			return new UnitVector2(angle.Cos, angle.Sin);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003F2C File Offset: 0x0000212C
		public Line2 GetTangent(Angle angle)
		{
			return new Line2(this.GetEdgePoint(angle), new UnitVector2(-angle.Sin, angle.Cos));
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003F50 File Offset: 0x00002150
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Circle2 other = (Circle2)obj;
			return this.Equals(other);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003F90 File Offset: 0x00002190
		public bool Equals(Circle2 other)
		{
			return this.Center.Equals(other.Center) && this.Radius == other.Radius;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003FC8 File Offset: 0x000021C8
		public override int GetHashCode()
		{
			return this.Center.GetHashCode() ^ this.Radius.GetHashCode();
		}
	}
}
