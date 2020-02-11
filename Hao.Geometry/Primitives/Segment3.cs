using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Segment3 : IEquatable<Segment3>
	{

		public Segment3(Vector3 origin, UnitVector3 direction, double extent)
		{
			this = default(Segment3);
			this.Origin = origin;
			this.Direction = direction;
			MathBase.AssertValid(extent);
			this.Extent = extent;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000A32C File Offset: 0x0000852C
		public Segment3(Vector3 negativeEnd, Vector3 positiveEnd)
		{
			this = default(Segment3);
			this.Origin = 0.5 * (negativeEnd + positiveEnd);
			Vector3 vector = positiveEnd - negativeEnd;
			this.Extent = 0.5 * vector.Length;
			UnitVector3 unitX;
			if (!vector.TryGetNormalized(out unitX))
			{
				unitX = UnitVector3.UnitX;
				this.Extent = 0.0;
			}
			this.Direction = unitX;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000A3A1 File Offset: 0x000085A1
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000A3A9 File Offset: 0x000085A9
		public double Extent { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000A3B2 File Offset: 0x000085B2
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000A3BA File Offset: 0x000085BA
		public Vector3 Origin { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000A3C3 File Offset: 0x000085C3
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000A3CB File Offset: 0x000085CB
		public UnitVector3 Direction { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000A3D4 File Offset: 0x000085D4
		public Vector3 PositiveEnd
		{
			get
			{
				return this.Origin + this.Direction * this.Extent;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000A3F2 File Offset: 0x000085F2
		public Vector3 NegativeEnd
		{
			get
			{
				return this.Origin - this.Direction * this.Extent;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000A410 File Offset: 0x00008610
		
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

		// Token: 0x060001E0 RID: 480 RVA: 0x0000A478 File Offset: 0x00008678
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Segment3 other = (Segment3)obj;
			return this.Equals(other);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000A4B8 File Offset: 0x000086B8
		public bool Equals(Segment3 other)
		{
			return this.Origin.Equals(other.Origin) && this.Direction.Equals(other.Direction) && this.Extent == other.Extent;
		}


		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.Direction.GetHashCode() ^ this.Extent.GetHashCode();
		}


		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Segment3{NE");
			stringBuilder.Append("{X:");
			stringBuilder.Append(this.NegativeEnd.X);
			stringBuilder.Append(" Y:");
			stringBuilder.Append(this.NegativeEnd.Y);
			stringBuilder.Append(" Z:");
			stringBuilder.Append(this.NegativeEnd.Z);
			stringBuilder.Append("}");
			stringBuilder.Append(" PE");
			stringBuilder.Append("{X:");
			stringBuilder.Append(this.PositiveEnd.X);
			stringBuilder.Append(" Y:");
			stringBuilder.Append(this.PositiveEnd.Y);
			stringBuilder.Append(" Z:");
			stringBuilder.Append(this.PositiveEnd.Z);
			stringBuilder.Append("}");
			stringBuilder.Append("}}");
			return stringBuilder.ToString();
		}
	}
}
