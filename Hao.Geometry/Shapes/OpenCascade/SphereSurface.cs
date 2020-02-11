using System;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000027 RID: 39
	public class SphereSurface : ISurface
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00006CBA File Offset: 0x00004EBA
		public SphereSurface(Vector3 origin, UnitVector3 unitX, UnitVector3 unitY, bool direct, double radius)
		{
			this.Origin = origin;
			this.UnitX = unitX;
			this.UnitY = unitY;
			this.Direct = direct;
			this.Radius = radius;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00006CE7 File Offset: 0x00004EE7
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00006CEF File Offset: 0x00004EEF
		public Vector3 Origin { get; private set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00006CF8 File Offset: 0x00004EF8
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00006D00 File Offset: 0x00004F00
		public UnitVector3 UnitX { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00006D09 File Offset: 0x00004F09
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00006D11 File Offset: 0x00004F11
		public UnitVector3 UnitY { get; private set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00006D1C File Offset: 0x00004F1C
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

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00006D5A File Offset: 0x00004F5A
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00006D62 File Offset: 0x00004F62
		public bool Direct { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00006D6B File Offset: 0x00004F6B
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00006D73 File Offset: 0x00004F73
		public double Radius { get; private set; }

		// Token: 0x060001CD RID: 461 RVA: 0x00006D7C File Offset: 0x00004F7C
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((SphereSurface)obj);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00006DA4 File Offset: 0x00004FA4
		public bool Equals(SphereSurface other)
		{
			return this.Origin.Equals(other.Origin) && this.UnitX.Equals(other.UnitX) && this.UnitY.Equals(other.UnitY) && this.Radius.Equals(other.Radius) && this.Direct.Equals(other.Direct);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006E20 File Offset: 0x00005020
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.UnitX.GetHashCode() ^ this.UnitY.GetHashCode() ^ this.Radius.GetHashCode() ^ this.Direct.GetHashCode();
		}
	}
}
