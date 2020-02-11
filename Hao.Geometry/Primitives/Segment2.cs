using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{
	// Token: 0x0200001E RID: 30
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[Guid("1F889EFE-7702-478c-BB29-C362DC6E36AE")]
	[Serializable]
	public struct Segment2 : IEquatable<Segment2>
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x0000A0C3 File Offset: 0x000082C3
		public Segment2(Vector2 origin, UnitVector2 direction, double extent)
		{
			this = default(Segment2);
			MathBase.AssertValid(extent);
			this.Origin = origin;
			this.Direction = direction;
			this.Extent = extent;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000A0E8 File Offset: 0x000082E8
		public Segment2(Vector2 end0, Vector2 end1)
		{
			this = default(Segment2);
			this.Origin = 0.5 * (end0 + end1);
			Vector2 vector = end1 - end0;
			this.Extent = 0.5 * vector.Length;
			UnitVector2 unitX;
			if (!vector.TryGetNormalized(out unitX))
			{
				unitX = UnitVector2.UnitX;
				this.Extent = 0.0;
			}
			this.Direction = unitX;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000A15D File Offset: 0x0000835D
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000A165 File Offset: 0x00008365
		public Vector2 Origin { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000A16E File Offset: 0x0000836E
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000A176 File Offset: 0x00008376
		public UnitVector2 Direction { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000A17F File Offset: 0x0000837F
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000A187 File Offset: 0x00008387
		public double Extent { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000A190 File Offset: 0x00008390
		public Vector2 PositiveEnd
		{
			get
			{
				return this.Origin + this.Direction * this.Extent;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000A1AE File Offset: 0x000083AE
		public Vector2 NegativeEnd
		{
			get
			{
				return this.Origin - this.Direction * this.Extent;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000A1CC File Offset: 0x000083CC
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(NE:");
				stringBuilder.Append(this.NegativeEnd.DebuggerDisplay);
				stringBuilder.Append(" PE:");
				stringBuilder.Append(this.PositiveEnd.DebuggerDisplay);
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000A234 File Offset: 0x00008434
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Segment2 other = (Segment2)obj;
			return this.Equals(other);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000A274 File Offset: 0x00008474
		public bool Equals(Segment2 other)
		{
			return this.Origin.Equals(other.Origin) && this.Direction.Equals(other.Direction) && this.Extent == other.Extent;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000A2C0 File Offset: 0x000084C0
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.Direction.GetHashCode() ^ this.Extent.GetHashCode();
		}
	}
}
