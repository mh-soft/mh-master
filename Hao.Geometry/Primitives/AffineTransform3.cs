using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct AffineTransform3 : IEquatable<AffineTransform3>
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x0000A668 File Offset: 0x00008868
		public AffineTransform3(UnitVector3 axisX, UnitVector3 axisY, UnitVector3 axisZ, Vector3 origin, double scale)
		{
			this = default(AffineTransform3);
			MathBase.Assert(Math.Abs(1.0 - axisX.Cross(axisY).Dot(axisZ)) < 1E-08, "AffineTransform3 constructor: axes must be perpendicular and right-handed.");
			this.AxisX = axisX;
			this.AxisY = axisY;
			this.AxisZ = axisZ;
			this.Scale = scale;
			this.Origin = origin;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000A6D6 File Offset: 0x000088D6
		public AffineTransform3(UnitVector3 axisX, UnitVector3 axisY, UnitVector3 axisZ, Vector3 origin)
		{
			this = new AffineTransform3(axisX, axisY, axisZ, origin, 1.0);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000A6EC File Offset: 0x000088EC
		public AffineTransform3(UnitVector3 axisX, UnitVector3 axisY, UnitVector3 axisZ)
		{
			this = new AffineTransform3(axisX, axisY, axisZ, Vector3.Zero);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000A6FC File Offset: 0x000088FC
		public AffineTransform3(UnitVector3 axisX, UnitVector3 axisY)
		{
			this = new AffineTransform3(axisX, axisY, axisX.UnitCross(axisY));
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000A70E File Offset: 0x0000890E
		public static AffineTransform3 Identity
		{
			get
			{
				return new AffineTransform3(UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, Vector3.Zero, 1.0);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000A732 File Offset: 0x00008932
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000A73A File Offset: 0x0000893A
		public UnitVector3 AxisX { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000A743 File Offset: 0x00008943
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000A74B File Offset: 0x0000894B
		public UnitVector3 AxisY { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000A754 File Offset: 0x00008954
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000A75C File Offset: 0x0000895C
		public UnitVector3 AxisZ { get; private set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000A765 File Offset: 0x00008965
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000A76D File Offset: 0x0000896D
		public Vector3 Origin { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000A776 File Offset: 0x00008976
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000A77E File Offset: 0x0000897E
		public double Scale { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000A788 File Offset: 0x00008988
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(X:");
				stringBuilder.Append(this.AxisX.DebuggerDisplay);
				stringBuilder.Append(" Y:");
				stringBuilder.Append(this.AxisY.DebuggerDisplay);
				stringBuilder.Append(" Z:");
				stringBuilder.Append(this.AxisZ.DebuggerDisplay);
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

		// Token: 0x060001F4 RID: 500 RVA: 0x0000A867 File Offset: 0x00008A67
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((AffineTransform3)obj);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000A898 File Offset: 0x00008A98
		public bool Equals(AffineTransform3 other)
		{
			return this.AxisX.Equals(other.AxisX) && this.AxisY.Equals(other.AxisY) && this.AxisZ.Equals(other.AxisZ) && this.Scale == other.Scale && this.Origin.Equals(other.Origin);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000A910 File Offset: 0x00008B10
		public override int GetHashCode()
		{
			return this.AxisX.GetHashCode() ^ this.AxisY.GetHashCode() ^ this.AxisZ.GetHashCode() ^ this.Scale.GetHashCode() ^ this.Origin.GetHashCode();
		}
	}
}
