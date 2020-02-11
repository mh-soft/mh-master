using System;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x0200001B RID: 27
	public class HyperbolaCurve2 : ICurve2
	{
		// Token: 0x06000155 RID: 341 RVA: 0x000060AD File Offset: 0x000042AD
		public HyperbolaCurve2(Vector2 origin, UnitVector2 majorUnit, UnitVector2 minorUnit, double majorRadius, double minorRadius)
		{
			this.Origin = origin;
			this.MajorUnit = majorUnit;
			this.MinorUnit = minorUnit;
			this.MajorRadius = majorRadius;
			this.MinorRadius = minorRadius;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000060DA File Offset: 0x000042DA
		// (set) Token: 0x06000157 RID: 343 RVA: 0x000060E2 File Offset: 0x000042E2
		public Vector2 Origin { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000060EB File Offset: 0x000042EB
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000060F3 File Offset: 0x000042F3
		public UnitVector2 MajorUnit { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000060FC File Offset: 0x000042FC
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00006104 File Offset: 0x00004304
		public UnitVector2 MinorUnit { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000610D File Offset: 0x0000430D
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00006115 File Offset: 0x00004315
		public double MajorRadius { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000611E File Offset: 0x0000431E
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00006126 File Offset: 0x00004326
		public double MinorRadius { get; private set; }

		// Token: 0x06000160 RID: 352 RVA: 0x00006130 File Offset: 0x00004330
		public Vector2 EvalAt(double parameter)
		{
			return this.Origin + this.MajorRadius * Math.Cosh(parameter) * this.MajorUnit + this.MinorRadius * Math.Sinh(parameter) * this.MinorUnit;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000617D File Offset: 0x0000437D
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((HyperbolaCurve2)obj);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000061A4 File Offset: 0x000043A4
		public bool Equals(HyperbolaCurve2 other)
		{
			return this.Origin.Equals(other.Origin) && this.MajorUnit.Equals(other.MajorUnit) && this.MinorUnit.Equals(other.MinorUnit) && this.MajorRadius.Equals(other.MajorRadius) && this.MinorRadius.Equals(other.MinorRadius);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006220 File Offset: 0x00004420
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.MajorUnit.GetHashCode() ^ this.MinorUnit.GetHashCode() ^ this.MajorRadius.GetHashCode() ^ this.MinorRadius.GetHashCode();
		}
	}
}
