using System;
using System.Collections.Generic;
using System.Linq;

namespace Hao.Geometry
{
	// Token: 0x02000081 RID: 129
	internal class Polynomial1
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x00017194 File Offset: 0x00015394
		public Polynomial1(IEnumerable<double> coefficients)
		{
			this.degree = -1;
			this.Coeff = null;
			if (coefficients == null || !coefficients.Any<double>())
			{
				return;
			}
			this.Coeff = new double[coefficients.Count<double>()];
			foreach (double num in coefficients)
			{
				double[] coeff = this.Coeff;
				int num2 = this.degree + 1;
				this.degree = num2;
				coeff[num2] = num;
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00017220 File Offset: 0x00015420
		private Polynomial1(int degree)
		{
			if (degree >= 0)
			{
				this.degree = degree;
				this.Coeff = new double[this.degree + 1];
				return;
			}
			this.degree = -1;
			this.Coeff = null;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00017255 File Offset: 0x00015455
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x0001725D File Offset: 0x0001545D
		public int Degree
		{
			get
			{
				return this.degree;
			}
			private set
			{
				this.degree = value;
				this.Coeff = ((this.degree >= 0) ? new double[this.degree + 1] : null);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00017285 File Offset: 0x00015485
		public IEnumerable<double> Coefficients
		{
			get
			{
				return new List<double>(this.Coeff);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00017292 File Offset: 0x00015492
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x0001729A File Offset: 0x0001549A
		private double[] Coeff { get; set; }

		// Token: 0x17000123 RID: 291
		public double this[int i]
		{
			get
			{
				if (i < 0 || i > this.degree)
				{
					throw new KeyNotFoundException("Polynomial1[].get: invalid index");
				}
				return this.Coeff[i];
			}
			private set
			{
				if (i < 0 || i > this.degree)
				{
					throw new KeyNotFoundException("Polynomial1[].set: invalid index");
				}
				this.Coeff[i] = value;
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000172E8 File Offset: 0x000154E8
		public static Polynomial1 operator +(Polynomial1 left, Polynomial1 right)
		{
			MathBase.Assert(left.degree >= 0 && right.degree >= 0, "Polynomial1.operator+(Polynomial1, Polynomial1): invalid degree");
			Polynomial1 polynomial = new Polynomial1(-1);
			if (left.degree > right.degree)
			{
				polynomial.Degree = left.degree;
				for (int i = 0; i <= right.degree; i++)
				{
					polynomial.Coeff[i] = left.Coeff[i] + right.Coeff[i];
				}
				for (int i = right.degree + 1; i <= left.degree; i++)
				{
					polynomial.Coeff[i] = left.Coeff[i];
				}
			}
			else
			{
				polynomial.Degree = right.degree;
				for (int i = 0; i <= left.degree; i++)
				{
					polynomial.Coeff[i] = left.Coeff[i] + right.Coeff[i];
				}
				for (int i = left.degree + 1; i <= right.degree; i++)
				{
					polynomial.Coeff[i] = right.Coeff[i];
				}
			}
			return polynomial;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000173EC File Offset: 0x000155EC
		public static Polynomial1 operator -(Polynomial1 left, Polynomial1 right)
		{
			MathBase.Assert(left.degree >= 0 && right.degree >= 0, "Polynomial1.operator-(Polynomial1, Polynomial1): invalid degree");
			Polynomial1 polynomial = new Polynomial1(-1);
			if (left.degree > right.degree)
			{
				polynomial.Degree = left.degree;
				for (int i = 0; i <= right.degree; i++)
				{
					polynomial.Coeff[i] = left.Coeff[i] - right.Coeff[i];
				}
				for (int i = right.degree + 1; i <= left.degree; i++)
				{
					polynomial.Coeff[i] = left.Coeff[i];
				}
			}
			else
			{
				polynomial.Degree = right.degree;
				for (int i = 0; i <= left.degree; i++)
				{
					polynomial.Coeff[i] = left.Coeff[i] - right.Coeff[i];
				}
				for (int i = left.degree + 1; i <= right.degree; i++)
				{
					polynomial.Coeff[i] = -right.Coeff[i];
				}
			}
			return polynomial;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000174F0 File Offset: 0x000156F0
		public static Polynomial1 operator *(Polynomial1 left, Polynomial1 right)
		{
			MathBase.Assert(left.degree >= 0 && right.degree >= 0, "Polynomial1.operator*(Polynomial1, Polynomial1): invalid degree");
			Polynomial1 polynomial = new Polynomial1(left.degree + right.degree);
			for (int i = 0; i <= left.degree; i++)
			{
				for (int j = 0; j <= right.degree; j++)
				{
					int num = i + j;
					polynomial.Coeff[num] += left.Coeff[i] * right.Coeff[j];
				}
			}
			return polynomial;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001757A File Offset: 0x0001577A
		public static Polynomial1 operator +(Polynomial1 poly, double scalar)
		{
			MathBase.Assert(poly.degree >= 0, "Polynomial1.operator+(Polynomial1, double): invalid degree");
			Polynomial1 polynomial = new Polynomial1(poly.Coeff);
			polynomial.Coeff[0] += scalar;
			return polynomial;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000175AE File Offset: 0x000157AE
		public static Polynomial1 operator -(Polynomial1 poly, double scalar)
		{
			MathBase.Assert(poly.degree >= 0, "Polynomial1 operator -(Polynomial1, double): invalid degree");
			Polynomial1 polynomial = new Polynomial1(poly.Coeff);
			polynomial.Coeff[0] -= scalar;
			return polynomial;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000175E4 File Offset: 0x000157E4
		public static Polynomial1 operator *(Polynomial1 poly, double scalar)
		{
			MathBase.Assert(poly.degree >= 0, "Polynomial1 operator *(Polynomial1, double): invalid degree");
			Polynomial1 polynomial = new Polynomial1(poly.degree);
			for (int i = 0; i <= poly.degree; i++)
			{
				polynomial.Coeff[i] = scalar * poly.Coeff[i];
			}
			return polynomial;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00017638 File Offset: 0x00015838
		public static Polynomial1 operator *(double scalar, Polynomial1 poly)
		{
			MathBase.Assert(poly.degree >= 0, "Polynomial1 operator *(double, Polynomial1): invalid degree");
			Polynomial1 polynomial = new Polynomial1(poly.degree);
			for (int i = 0; i <= poly.degree; i++)
			{
				polynomial.Coeff[i] = scalar * poly.Coeff[i];
			}
			return polynomial;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001768C File Offset: 0x0001588C
		public static Polynomial1 operator /(Polynomial1 poly, double scalar)
		{
			MathBase.Assert(poly.degree >= 0, "Polynomial1 operator /(Polynomial1, double): invalid degree");
			Polynomial1 polynomial = new Polynomial1(poly.degree);
			if (scalar != 0.0)
			{
				double num = 1.0 / scalar;
				for (int i = 0; i <= poly.degree; i++)
				{
					polynomial.Coeff[i] = num * poly.Coeff[i];
				}
			}
			else
			{
				for (int i = 0; i <= poly.degree; i++)
				{
					polynomial.Coeff[i] = double.MaxValue;
				}
			}
			return polynomial;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001771C File Offset: 0x0001591C
		public static Polynomial1 operator -(Polynomial1 poly)
		{
			MathBase.Assert(poly.degree >= 0, "Polynomial1 operator -(Polynomial1): invalid degree");
			Polynomial1 polynomial = new Polynomial1(poly.degree);
			for (int i = 0; i <= poly.degree; i++)
			{
				polynomial.Coeff[i] = -poly.Coeff[i];
			}
			return polynomial;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00017770 File Offset: 0x00015970
		public double Evaluate(double t)
		{
			if (this.degree < 0)
			{
				throw new InvalidOperationException("Polynomial1.Evaluate(double): invalid degree");
			}
			double num = this.Coeff[this.degree];
			for (int i = this.degree - 1; i >= 0; i--)
			{
				num *= t;
				num += this.Coeff[i];
			}
			return num;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000177C4 File Offset: 0x000159C4
		public Polynomial1 GetDerivative()
		{
			if (this.degree > 0)
			{
				Polynomial1 polynomial = new Polynomial1(this.degree - 1);
				int i = 0;
				int num = 1;
				while (i < this.degree)
				{
					polynomial.Coeff[i] = (double)num * this.Coeff[num];
					i++;
					num++;
				}
				return polynomial;
			}
			if (this.degree == 0)
			{
				Polynomial1 polynomial2 = new Polynomial1(0);
				polynomial2.Coeff[0] = 0.0;
				return polynomial2;
			}
			return new Polynomial1(-1);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001783C File Offset: 0x00015A3C
		public Polynomial1 GetInversion()
		{
			Polynomial1 polynomial = new Polynomial1(this.degree);
			for (int i = 0; i <= this.degree; i++)
			{
				polynomial.Coeff[i] = this.Coeff[this.degree - i];
			}
			return polynomial;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00017880 File Offset: 0x00015A80
		public void Compress(double epsilon)
		{
			int i = this.degree;
			while (i >= 0 && Math.Abs(this.Coeff[i]) <= epsilon)
			{
				this.degree--;
				i--;
			}
			if (this.degree >= 0)
			{
				double num = 1.0 / this.Coeff[this.degree];
				this.Coeff[this.degree] = 1.0;
				for (i = 0; i < this.degree; i++)
				{
					this.Coeff[i] *= num;
				}
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00017918 File Offset: 0x00015B18
		public void Divide(Polynomial1 div, out Polynomial1 quot, out Polynomial1 rem, double epsilon)
		{
			quot = new Polynomial1(-1);
			int num = this.degree - div.degree;
			if (num >= 0)
			{
				quot.Degree = num;
				Polynomial1 polynomial = new Polynomial1(this.Coeff);
				double num2 = 1.0 / div[div.degree];
				for (int i = num; i >= 0; i--)
				{
					int j = div.degree + i;
					quot[i] = num2 * polynomial[j];
					for (j--; j >= i; j--)
					{
						Polynomial1 polynomial2 = polynomial;
						int i2 = j;
						polynomial2[i2] -= quot[i] * div[j - i];
					}
				}
				int num3 = div.degree - 1;
				while (num3 > 0 && Math.Abs(polynomial[num3]) < epsilon)
				{
					num3--;
				}
				if (num3 == 0 && Math.Abs(polynomial[0]) < epsilon)
				{
					polynomial[0] = 0.0;
				}
				rem = new Polynomial1(num3);
				for (int k = 0; k <= num3; k++)
				{
					rem[k] = polynomial[k];
				}
				return;
			}
			quot.Degree = 0;
			quot[0] = 0.0;
			rem = new Polynomial1(this.Coeff);
		}

		// Token: 0x04000156 RID: 342
		private int degree;
	}
}
