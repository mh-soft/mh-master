using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Ellipse2 : IEquatable<Ellipse2>
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00004A70 File Offset: 0x00002C70
		public Ellipse2(Vector2 center, UnitVector2 axis0, UnitVector2 axis1, double extent0, double extent1)
		{
			this = default(Ellipse2);
			MathBase.Assert(Math.Abs(axis0.Dot(axis1)) < 1E-08, "Ellipse2 constructor: axes not perpendicular.");
			if (axis0.Dot(UnitVector2.UnitX) >= 0.0)
			{
				MathBase.Assert(axis1.Dot(UnitVector2.UnitY) >= 0.0, "Ellipse2 constructor: axes not right-handed.");
			}
			else
			{
				MathBase.Assert(axis1.Dot(UnitVector2.UnitY) < 0.0, "Ellipse2 constructor: axes not right-handed.");
			}
			this.Center = center;
			this.Axis0 = axis0;
			this.Axis1 = axis1;
			this.Extent0 = extent0;
			this.Extent1 = extent1;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004B29 File Offset: 0x00002D29
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00004B31 File Offset: 0x00002D31
		public Vector2 Center { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004B3A File Offset: 0x00002D3A
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00004B42 File Offset: 0x00002D42
		public UnitVector2 Axis0 { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004B4B File Offset: 0x00002D4B
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00004B53 File Offset: 0x00002D53
		public UnitVector2 Axis1 { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004B5C File Offset: 0x00002D5C
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00004B64 File Offset: 0x00002D64
		public double Extent0 { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004B6D File Offset: 0x00002D6D
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00004B75 File Offset: 0x00002D75
		public double Extent1 { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004B80 File Offset: 0x00002D80
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(C:");
				stringBuilder.Append(this.Center.DebuggerDisplay);
				stringBuilder.Append(" A0:");
				stringBuilder.Append(this.Axis0.DebuggerDisplay);
				stringBuilder.Append(" A1:");
				stringBuilder.Append(this.Axis1.DebuggerDisplay);
				stringBuilder.Append(" E:(");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.Extent0
				}));
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.Extent1
				}));
				stringBuilder.Append("))");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004C74 File Offset: 0x00002E74
		public Vector2 GetEdgePoint(Angle angle)
		{
			return this.Center + (Vector2)this.Axis0 * this.Extent0 * angle.Cos + (Vector2)this.Axis1 * this.Extent1 * angle.Sin;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004CD5 File Offset: 0x00002ED5
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Ellipse2)obj);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004D08 File Offset: 0x00002F08
		public bool Equals(Ellipse2 other)
		{
			return this.Center.Equals(other.Center) && this.Axis0.Equals(other.Axis0) && this.Axis1.Equals(other.Axis1) && this.Extent0.Equals(other.Extent0) && this.Extent1.Equals(other.Extent1);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004D88 File Offset: 0x00002F88
		public override int GetHashCode()
		{
			return this.Center.GetHashCode() ^ this.Axis0.GetHashCode() ^ this.Axis1.GetHashCode() ^ this.Extent0.GetHashCode() ^ this.Extent1.GetHashCode();
		}
	}
}
