using System;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x0200001C RID: 28
	public class HyperbolaCurve3 : ICurve3
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00006289 File Offset: 0x00004489
		public HyperbolaCurve3(Vector3 origin, UnitVector3 majorUnit, UnitVector3 minorUnit, double majorRadius, double minorRadius)
		{
			this.Origin = origin;
			this.MajorUnit = majorUnit;
			this.MinorUnit = minorUnit;
			this.MajorRadius = majorRadius;
			this.MinorRadius = minorRadius;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000062B6 File Offset: 0x000044B6
		// (set) Token: 0x06000166 RID: 358 RVA: 0x000062BE File Offset: 0x000044BE
		public Vector3 Origin { get; private set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000062C7 File Offset: 0x000044C7
		// (set) Token: 0x06000168 RID: 360 RVA: 0x000062CF File Offset: 0x000044CF
		public UnitVector3 MajorUnit { get; private set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000062D8 File Offset: 0x000044D8
		// (set) Token: 0x0600016A RID: 362 RVA: 0x000062E0 File Offset: 0x000044E0
		public UnitVector3 MinorUnit { get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000062EC File Offset: 0x000044EC
		public UnitVector3 Normal
		{
			get
			{
				return this.MajorUnit.UnitCross(this.MinorUnit);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000630D File Offset: 0x0000450D
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00006315 File Offset: 0x00004515
		public double MajorRadius { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000631E File Offset: 0x0000451E
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00006326 File Offset: 0x00004526
		public double MinorRadius { get; private set; }

		// Token: 0x06000170 RID: 368 RVA: 0x00006330 File Offset: 0x00004530
		public Vector3 EvalAt(double parameter)
		{
			return this.Origin + this.MajorRadius * Math.Cosh(parameter) * this.MajorUnit + this.MinorRadius * Math.Sinh(parameter) * this.MinorUnit;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000637D File Offset: 0x0000457D
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((HyperbolaCurve3)obj);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000063A4 File Offset: 0x000045A4
		public bool Equals(HyperbolaCurve3 other)
		{
			return this.Origin.Equals(other.Origin) && this.MajorUnit.Equals(other.MajorUnit) && this.MinorUnit.Equals(other.MinorUnit) && this.MajorRadius.Equals(other.MajorRadius) && this.MinorRadius.Equals(other.MinorRadius);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006420 File Offset: 0x00004620
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.MajorUnit.GetHashCode() ^ this.MinorUnit.GetHashCode() ^ this.MajorRadius.GetHashCode() ^ this.MinorRadius.GetHashCode();
		}
	}
}
