using System;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000022 RID: 34
	public class ParabolaCurve2 : ICurve2
	{
		// Token: 0x0600018D RID: 397 RVA: 0x000066E2 File Offset: 0x000048E2
		public ParabolaCurve2(Vector2 origin, UnitVector2 unitX, UnitVector2 unitY, double focal)
		{
			this.Origin = origin;
			this.UnitX = unitX;
			this.UnitY = unitY;
			this.Focal = focal;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00006707 File Offset: 0x00004907
		// (set) Token: 0x0600018F RID: 399 RVA: 0x0000670F File Offset: 0x0000490F
		public Vector2 Origin { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00006718 File Offset: 0x00004918
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00006720 File Offset: 0x00004920
		public UnitVector2 UnitX { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00006729 File Offset: 0x00004929
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00006731 File Offset: 0x00004931
		public UnitVector2 UnitY { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000673A File Offset: 0x0000493A
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00006742 File Offset: 0x00004942
		public double Focal { get; private set; }

		// Token: 0x06000196 RID: 406 RVA: 0x0000674C File Offset: 0x0000494C
		public Vector2 EvalAt(double parameter)
		{
			return this.Origin + this.Focal * (parameter * parameter * this.UnitX + 2.0 * parameter * this.UnitY);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006798 File Offset: 0x00004998
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((ParabolaCurve2)obj);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000067C0 File Offset: 0x000049C0
		public bool Equals(ParabolaCurve2 other)
		{
			return this.Origin.Equals(other.Origin) && this.UnitX.Equals(other.UnitX) && this.UnitY.Equals(other.UnitY) && this.Focal.Equals(other.Focal);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006828 File Offset: 0x00004A28
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.UnitX.GetHashCode() ^ this.UnitY.GetHashCode() ^ this.Focal.GetHashCode();
		}
	}
}
