using System;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000018 RID: 24
	public class EllipseCurve2 : ICurve2
	{
		// Token: 0x06000125 RID: 293 RVA: 0x0000583E File Offset: 0x00003A3E
		public EllipseCurve2(Vector2 origin, UnitVector2 majorUnit, UnitVector2 minorUnit, double majorRadius, double minorRadius)
		{
			this.Origin = origin;
			this.MajorUnit = majorUnit;
			this.MinorUnit = minorUnit;
			this.MajorRadius = majorRadius;
			this.MinorRadius = minorRadius;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000586B File Offset: 0x00003A6B
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00005873 File Offset: 0x00003A73
		public Vector2 Origin { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000587C File Offset: 0x00003A7C
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00005884 File Offset: 0x00003A84
		public UnitVector2 MajorUnit { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000588D File Offset: 0x00003A8D
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00005895 File Offset: 0x00003A95
		public UnitVector2 MinorUnit { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000589E File Offset: 0x00003A9E
		// (set) Token: 0x0600012D RID: 301 RVA: 0x000058A6 File Offset: 0x00003AA6
		public double MajorRadius { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000058AF File Offset: 0x00003AAF
		// (set) Token: 0x0600012F RID: 303 RVA: 0x000058B7 File Offset: 0x00003AB7
		public double MinorRadius { get; private set; }

		// Token: 0x06000130 RID: 304 RVA: 0x000058C0 File Offset: 0x00003AC0
		public Vector2 EvalAt(double parameter)
		{
			return this.Origin + this.MajorRadius * Math.Cos(parameter) * this.MajorUnit + this.MinorRadius * Math.Sin(parameter) * this.MinorUnit;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000590D File Offset: 0x00003B0D
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((EllipseCurve2)obj);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005934 File Offset: 0x00003B34
		public bool Equals(EllipseCurve2 other)
		{
			return this.Origin.Equals(other.Origin) && this.MajorUnit.Equals(other.MajorUnit) && this.MinorUnit.Equals(other.MinorUnit) && this.MajorRadius.Equals(other.MajorRadius) && this.MinorRadius.Equals(other.MinorRadius);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000059B0 File Offset: 0x00003BB0
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.MajorUnit.GetHashCode() ^ this.MinorUnit.GetHashCode() ^ this.MajorRadius.GetHashCode() ^ this.MinorRadius.GetHashCode();
		}
	}
}
