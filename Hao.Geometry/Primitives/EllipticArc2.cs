using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct EllipticArc2 : IEquatable<EllipticArc2>
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00004DF1 File Offset: 0x00002FF1
		public EllipticArc2(Ellipse2 ellipse, Angle startAngle, Angle deltaAngle)
		{
			this = default(EllipticArc2);
			this.Ellipse = ellipse;
			this.StartAngle = startAngle;
			this.DeltaAngle = deltaAngle;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004E0F File Offset: 0x0000300F
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00004E17 File Offset: 0x00003017
		public Ellipse2 Ellipse { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004E20 File Offset: 0x00003020
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00004E28 File Offset: 0x00003028
		public Angle StartAngle { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004E31 File Offset: 0x00003031
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00004E39 File Offset: 0x00003039
		public Angle DeltaAngle { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004E44 File Offset: 0x00003044
		public Vector2 StartPoint
		{
			get
			{
				return this.Ellipse.GetEdgePoint(this.StartAngle);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004E68 File Offset: 0x00003068
		public Vector2 EndPoint
		{
			get
			{
				return this.Ellipse.GetEdgePoint(this.StartAngle + this.DeltaAngle);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004E94 File Offset: 0x00003094
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(S:");
				stringBuilder.Append(this.StartAngle.DebuggerDisplay);
				stringBuilder.Append(" D:");
				stringBuilder.Append(this.DeltaAngle.DebuggerDisplay);
				stringBuilder.Append(" E:");
				stringBuilder.Append(this.Ellipse.DebuggerDisplay);
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004F1A File Offset: 0x0000311A
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Ellipse2)obj);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004F58 File Offset: 0x00003158
		public bool Equals(EllipticArc2 other)
		{
			return this.Ellipse.Equals(other.Ellipse) && this.StartAngle.Equals(other.StartAngle) && this.DeltaAngle.Equals(other.DeltaAngle);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004FAC File Offset: 0x000031AC
		public override int GetHashCode()
		{
			return this.Ellipse.GetHashCode() ^ this.StartAngle.GetHashCode() ^ this.DeltaAngle.GetHashCode();
		}
	}
}
