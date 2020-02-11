using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct AffineTransform2
	{

		public AffineTransform2(UnitVector2 axisX, UnitVector2 axisY, Vector2 origin, double scale)
		{
			this = default(AffineTransform2);
			MathBase.Assert(Math.Abs(axisX.Dot(axisY)) < 1E-08, "AffineTransform2 constructor: axes not perpendicular.");
			if (axisX.Dot(UnitVector2.UnitX) >= 0.0)
			{
				MathBase.Assert(axisY.Dot(UnitVector2.UnitY) >= 0.0, "AffineTransform2 constructor: axes not right-handed.");
			}
			else
			{
				MathBase.Assert(axisY.Dot(UnitVector2.UnitY) < 0.0, "AffineTransform2 constructor: axes not right-handed.");
			}
			this.AxisX = axisX;
			this.AxisY = axisY;
			this.Scale = scale;
			this.Origin = origin;
		}

		public static AffineTransform2 Identity
		{
			get
			{
				return new AffineTransform2(UnitVector2.UnitX, UnitVector2.UnitY, Vector2.Zero, 1.0);
			}
		}


		public UnitVector2 AxisX { get; private set; }


		public UnitVector2 AxisY { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002142 File Offset: 0x00000342
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000214A File Offset: 0x0000034A
		public Vector2 Origin { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002153 File Offset: 0x00000353
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000215B File Offset: 0x0000035B
		public double Scale { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002164 File Offset: 0x00000364
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(X:");
				stringBuilder.Append(this.AxisX.DebuggerDisplay);
				stringBuilder.Append(" Y:");
				stringBuilder.Append(this.AxisY.DebuggerDisplay);
				stringBuilder.Append(" O:");
				stringBuilder.Append(this.Origin.DebuggerDisplay);
				stringBuilder.Append(" S:");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.Scale
				}));
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002222 File Offset: 0x00000422
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((AffineTransform3)obj);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002260 File Offset: 0x00000460
		public bool Equals(AffineTransform2 other)
		{
			return this.AxisX.Equals(other.AxisX) && this.AxisY.Equals(other.AxisY) && this.Scale == other.Scale && this.Origin.Equals(other.Origin);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022C4 File Offset: 0x000004C4
		public override int GetHashCode()
		{
			return this.AxisX.GetHashCode() ^ this.AxisY.GetHashCode() ^ this.Scale.GetHashCode() ^ this.Origin.GetHashCode();
		}
	}
}
