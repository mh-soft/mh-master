using System;
using System.Collections.Generic;
using System.Linq;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x0200000F RID: 15
	public class BSplineCurve2 : ICurve2
	{
		// Token: 0x06000096 RID: 150 RVA: 0x000043B8 File Offset: 0x000025B8
		public BSplineCurve2(IEnumerable<Vector2> poles, IEnumerable<double> knots, IEnumerable<int> multiplicities, int degree)
		{
			this.Poles = new List<Vector2>(poles).AsReadOnly();
			this.Weights = new List<double>().AsReadOnly();
			this.Knots = new List<double>(knots).AsReadOnly();
			this.Multiplicities = new List<int>(multiplicities).AsReadOnly();
			this.Degree = degree;
			this.Rational = false;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004420 File Offset: 0x00002620
		public BSplineCurve2(IEnumerable<Vector2> poles, IEnumerable<double> weights, IEnumerable<double> knots, IEnumerable<int> multiplicities, int degree)
		{
			this.Poles = new List<Vector2>(poles).AsReadOnly();
			this.Weights = new List<double>(weights).AsReadOnly();
			this.Knots = new List<double>(knots).AsReadOnly();
			this.Multiplicities = new List<int>(multiplicities).AsReadOnly();
			this.Degree = degree;
			this.Rational = true;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004487 File Offset: 0x00002687
		// (set) Token: 0x06000099 RID: 153 RVA: 0x0000448F File Offset: 0x0000268F
		public IList<Vector2> Poles { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004498 File Offset: 0x00002698
		// (set) Token: 0x0600009B RID: 155 RVA: 0x000044A0 File Offset: 0x000026A0
		public IList<double> Weights { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000044A9 File Offset: 0x000026A9
		// (set) Token: 0x0600009D RID: 157 RVA: 0x000044B1 File Offset: 0x000026B1
		public IList<double> Knots { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000044BA File Offset: 0x000026BA
		// (set) Token: 0x0600009F RID: 159 RVA: 0x000044C2 File Offset: 0x000026C2
		public IList<int> Multiplicities { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000044CB File Offset: 0x000026CB
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000044D3 File Offset: 0x000026D3
		public int Degree { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000044DC File Offset: 0x000026DC
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000044E4 File Offset: 0x000026E4
		public bool Rational { get; private set; }

		// Token: 0x060000A4 RID: 164 RVA: 0x000044F0 File Offset: 0x000026F0
		public Vector2 EvalAt(double parameter)
		{
			if (parameter.Equals(this.Knots.First<double>()))
			{
				return this.Poles.First<Vector2>();
			}
			if (parameter.Equals(this.Knots.Last<double>()))
			{
				return this.Poles.Last<Vector2>();
			}
			throw new NotImplementedException("BSplineCurve2.EvalAt()");
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004547 File Offset: 0x00002747
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((BSplineCurve2)obj);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004570 File Offset: 0x00002770
		public bool Equals(BSplineCurve2 other)
		{
			return this.Knots.SequenceEqual(other.Knots) && this.Multiplicities.SequenceEqual(other.Multiplicities) && this.Poles.SequenceEqual(other.Poles) && this.Weights.SequenceEqual(other.Weights);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000045C9 File Offset: 0x000027C9
		public override int GetHashCode()
		{
			return this.Knots.GetHashCode() ^ this.Multiplicities.GetHashCode() ^ this.Poles.GetHashCode() ^ this.Weights.GetHashCode();
		}
	}
}
