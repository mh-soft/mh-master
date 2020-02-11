using System;
using System.Collections.Generic;
using System.Linq;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000010 RID: 16
	public class BSplineCurve3 : ICurve3
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x000045FC File Offset: 0x000027FC
		public BSplineCurve3(IEnumerable<Vector3> poles, IEnumerable<double> knots, IEnumerable<int> multiplicities, int degree)
		{
			this.Poles = new List<Vector3>(poles).AsReadOnly();
			this.Weights = new List<double>().AsReadOnly();
			this.Knots = new List<double>(knots).AsReadOnly();
			this.Multiplicities = new List<int>(multiplicities).AsReadOnly();
			this.Degree = degree;
			this.Rational = false;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004664 File Offset: 0x00002864
		public BSplineCurve3(IEnumerable<Vector3> poles, IEnumerable<double> weights, IEnumerable<double> knots, IEnumerable<int> multiplicities, int degree)
		{
			this.Poles = new List<Vector3>(poles).AsReadOnly();
			this.Weights = new List<double>(weights).AsReadOnly();
			this.Knots = new List<double>(knots).AsReadOnly();
			this.Multiplicities = new List<int>(multiplicities).AsReadOnly();
			this.Degree = degree;
			this.Rational = true;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000046CB File Offset: 0x000028CB
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000046D3 File Offset: 0x000028D3
		public IList<Vector3> Poles { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000046DC File Offset: 0x000028DC
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000046E4 File Offset: 0x000028E4
		public IList<double> Weights { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000046ED File Offset: 0x000028ED
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000046F5 File Offset: 0x000028F5
		public IList<double> Knots { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000046FE File Offset: 0x000028FE
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004706 File Offset: 0x00002906
		public IList<int> Multiplicities { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000470F File Offset: 0x0000290F
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00004717 File Offset: 0x00002917
		public int Degree { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004720 File Offset: 0x00002920
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00004728 File Offset: 0x00002928
		public bool Rational { get; private set; }

		// Token: 0x060000B6 RID: 182 RVA: 0x00004731 File Offset: 0x00002931
		public Vector3 EvalAt(double parameter)
		{
			throw new NotImplementedException("BSplineCurve3.EvalAt()");
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000473D File Offset: 0x0000293D
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((BSplineCurve3)obj);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004764 File Offset: 0x00002964
		public bool Equals(BSplineCurve3 other)
		{
			return this.Knots.SequenceEqual(other.Knots) && this.Multiplicities.SequenceEqual(other.Multiplicities) && this.Poles.SequenceEqual(other.Poles) && this.Weights.SequenceEqual(other.Weights);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000047BD File Offset: 0x000029BD
		public override int GetHashCode()
		{
			return this.Knots.GetHashCode() ^ this.Multiplicities.GetHashCode() ^ this.Poles.GetHashCode() ^ this.Weights.GetHashCode();
		}
	}
}
