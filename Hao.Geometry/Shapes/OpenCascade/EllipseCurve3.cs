using System;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000019 RID: 25
	public class EllipseCurve3 : ICurve3
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00005A19 File Offset: 0x00003C19
		public EllipseCurve3(Vector3 origin, UnitVector3 majorUnit, UnitVector3 minorUnit, double majorRadius, double minorRadius)
		{
			this.Origin = origin;
			this.MajorUnit = majorUnit;
			this.MinorUnit = minorUnit;
			this.MajorRadius = majorRadius;
			this.MinorRadius = minorRadius;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00005A46 File Offset: 0x00003C46
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00005A4E File Offset: 0x00003C4E
		public Vector3 Origin { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005A57 File Offset: 0x00003C57
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00005A5F File Offset: 0x00003C5F
		public UnitVector3 MajorUnit { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005A68 File Offset: 0x00003C68
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00005A70 File Offset: 0x00003C70
		public UnitVector3 MinorUnit { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00005A7C File Offset: 0x00003C7C
		public UnitVector3 Normal
		{
			get
			{
				return this.MajorUnit.UnitCross(this.MinorUnit);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00005A9D File Offset: 0x00003C9D
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00005AA5 File Offset: 0x00003CA5
		public double MajorRadius { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00005AAE File Offset: 0x00003CAE
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00005AB6 File Offset: 0x00003CB6
		public double MinorRadius { get; private set; }

		// Token: 0x06000140 RID: 320 RVA: 0x00005AC0 File Offset: 0x00003CC0
		public Vector3 EvalAt(double parameter)
		{
			return this.Origin + this.MajorRadius * Math.Cos(parameter) * this.MajorUnit + this.MinorRadius * Math.Sin(parameter) * this.MinorUnit;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005B0D File Offset: 0x00003D0D
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((EllipseCurve3)obj);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005B34 File Offset: 0x00003D34
		public bool Equals(EllipseCurve3 other)
		{
			return this.Origin.Equals(other.Origin) && this.MajorUnit.Equals(other.MajorUnit) && this.MinorUnit.Equals(other.MinorUnit) && this.MajorRadius.Equals(other.MajorRadius) && this.MinorRadius.Equals(other.MinorRadius);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005BB0 File Offset: 0x00003DB0
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.MajorUnit.GetHashCode() ^ this.MinorUnit.GetHashCode() ^ this.MajorRadius.GetHashCode() ^ this.MinorRadius.GetHashCode();
		}
	}
}
