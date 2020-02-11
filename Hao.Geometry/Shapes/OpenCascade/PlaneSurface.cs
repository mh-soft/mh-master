using System;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000024 RID: 36
	public class PlaneSurface : ISurface
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00006A46 File Offset: 0x00004C46
		public PlaneSurface(Vector3 origin, UnitVector3 unitX, UnitVector3 unitY, bool direct)
		{
			this.Origin = origin;
			this.UnitX = unitX;
			this.UnitY = unitY;
			this.Direct = direct;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00006A6B File Offset: 0x00004C6B
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00006A73 File Offset: 0x00004C73
		public Vector3 Origin { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00006A7C File Offset: 0x00004C7C
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00006A84 File Offset: 0x00004C84
		public UnitVector3 UnitX { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00006A8D File Offset: 0x00004C8D
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00006A95 File Offset: 0x00004C95
		public UnitVector3 UnitY { get; private set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00006AA0 File Offset: 0x00004CA0
		public UnitVector3 Normal
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00006ADE File Offset: 0x00004CDE
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00006AE6 File Offset: 0x00004CE6
		public bool Direct { get; private set; }

		// Token: 0x060001B2 RID: 434 RVA: 0x00006AEF File Offset: 0x00004CEF
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((PlaneSurface)obj);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006B18 File Offset: 0x00004D18
		public bool Equals(PlaneSurface other)
		{
			return this.Origin.Equals(other.Origin) && this.UnitX.Equals(other.UnitX) && this.UnitY.Equals(other.UnitY) && this.Direct.Equals(other.Direct);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006B80 File Offset: 0x00004D80
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.UnitX.GetHashCode() ^ this.UnitY.GetHashCode() ^ this.Direct.GetHashCode();
		}
	}
}
