using System;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000028 RID: 40
	public class TorusSurface : ISurface
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00006E89 File Offset: 0x00005089
		public TorusSurface(Vector3 origin, UnitVector3 unitX, UnitVector3 unitY, bool direct, double majorRadius, double minorRadius)
		{
			this.Origin = origin;
			this.UnitX = unitX;
			this.UnitY = unitY;
			this.Direct = direct;
			this.MajorRadius = majorRadius;
			this.MinorRadius = minorRadius;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00006EBE File Offset: 0x000050BE
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00006EC6 File Offset: 0x000050C6
		public Vector3 Origin { get; private set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00006ECF File Offset: 0x000050CF
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00006ED7 File Offset: 0x000050D7
		public UnitVector3 UnitX { get; private set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00006EE0 File Offset: 0x000050E0
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00006EE8 File Offset: 0x000050E8
		public UnitVector3 UnitY { get; private set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00006EF4 File Offset: 0x000050F4
		public UnitVector3 UnitZ
		{
			get
			{
				if (this.Direct)
				{
					return this.UnitX.UnitCross(this.UnitY);
				}
				return this.UnitY.UnitCross(this.UnitX);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00006F32 File Offset: 0x00005132
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00006F3A File Offset: 0x0000513A
		public bool Direct { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00006F43 File Offset: 0x00005143
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00006F4B File Offset: 0x0000514B
		public double MajorRadius { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00006F54 File Offset: 0x00005154
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00006F5C File Offset: 0x0000515C
		public double MinorRadius { get; private set; }

		// Token: 0x060001DE RID: 478 RVA: 0x00006F65 File Offset: 0x00005165
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((TorusSurface)obj);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006F8C File Offset: 0x0000518C
		public bool Equals(TorusSurface other)
		{
			return this.Origin.Equals(other.Origin) && this.UnitX.Equals(other.UnitX) && this.UnitY.Equals(other.UnitY) && this.MajorRadius.Equals(other.MajorRadius) && this.MinorRadius.Equals(other.MinorRadius) && this.Direct.Equals(other.Direct);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007020 File Offset: 0x00005220
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.UnitX.GetHashCode() ^ this.UnitY.GetHashCode() ^ this.MajorRadius.GetHashCode() ^ this.MinorRadius.GetHashCode() ^ this.Direct.GetHashCode();
		}
	}
}
