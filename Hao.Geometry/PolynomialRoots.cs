using System;

namespace Hao.Geometry
{
	// Token: 0x02000087 RID: 135
	internal struct PolynomialRoots
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x000196B4 File Offset: 0x000178B4
		public PolynomialRoots(double epsilon)
		{
			this = default(PolynomialRoots);
			MathBase.Assert(epsilon >= 0.0, "PolynomialRoots(): epsilon was < 0");
			this.epsilon = epsilon;
			this.Count = 0;
			this.maxRoot = 4;
			this.Roots = new double[this.maxRoot];
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00019707 File Offset: 0x00017907
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x0001970F File Offset: 0x0001790F
		public int Count { get; private set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00019718 File Offset: 0x00017918
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00019720 File Offset: 0x00017920
		public double[] Roots { get; private set; }

		// Token: 0x060004E2 RID: 1250 RVA: 0x00019729 File Offset: 0x00017929
		public double GetRoot(int i)
		{
			MathBase.Assert(0 <= i && i < this.Count, "PolynomialRoots.GetRoot(): invalid argument");
			if (0 <= i && i < this.Count)
			{
				return this.Roots[i];
			}
			return double.MaxValue;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00019764 File Offset: 0x00017964
		public bool FindA(double fC0, double fC1)
		{
			if (Math.Abs(fC1) >= this.epsilon)
			{
				this.Roots[0] = -fC0 / fC1;
				this.Count = 1;
				return true;
			}
			this.Count = 0;
			return false;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00019794 File Offset: 0x00017994
		public double GetBound(double fC0, double fC1)
		{
			if (Math.Abs(fC1) <= this.epsilon)
			{
				return -1.0;
			}
			double num = 1.0 / fC1;
			double num2 = Math.Abs(fC0) * num;
			return 1.0 + num2;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000197DC File Offset: 0x000179DC
		public bool FindA(double fC0, double fC1, double fC2)
		{
			if (Math.Abs(fC2) <= this.epsilon)
			{
				return this.FindA(fC0, fC1);
			}
			double num = fC1 * fC1 - 4.0 * fC0 * fC2;
			if (Math.Abs(num) <= this.epsilon)
			{
				num = 0.0;
			}
			if (num < 0.0)
			{
				this.Count = 0;
				return false;
			}
			double num2 = 0.5 / fC2;
			if (num > 0.0)
			{
				num = Math.Sqrt(num);
				this.Roots[0] = num2 * (-fC1 - num);
				this.Roots[1] = num2 * (-fC1 + num);
				this.Count = 2;
			}
			else
			{
				this.Roots[0] = -num2 * fC1;
				this.Count = 1;
			}
			return true;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00019898 File Offset: 0x00017A98
		public double GetBound(double fC0, double fC1, double fC2)
		{
			if (Math.Abs(fC2) > this.epsilon)
			{
				double num = 1.0 / fC2;
				double num2 = Math.Abs(fC0) * num;
				double num3 = Math.Abs(fC1) * num;
				double num4 = (num2 >= num3) ? num2 : num3;
				return 1.0 + num4;
			}
			if (!this.FindA(fC0, fC1))
			{
				return double.MaxValue;
			}
			return this.Roots[0];
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00019904 File Offset: 0x00017B04
		public bool FindA(double fC0, double fC1, double fC2, double fC3)
		{
			if (Math.Abs(fC3) <= this.epsilon)
			{
				return this.FindA(fC0, fC1, fC2);
			}
			double num = 1.0 / fC3;
			fC0 *= num;
			fC1 *= num;
			fC2 *= num;
			double num2 = 0.33333333333333331 * fC2;
			double num3 = fC1 - fC2 * num2;
			double num4 = fC0 + fC2 * (2.0 * fC2 * fC2 - 9.0 * fC1) * 0.037037037037037035;
			double num5 = 0.5 * num4;
			double num6 = num5;
			double num7 = num6 * num6;
			double num8 = num3;
			double num9 = num7 + num8 * num8 * num3 * 0.037037037037037035;
			if (Math.Abs(num9) <= this.epsilon)
			{
				num9 = 0.0;
			}
			if (num9 > 0.0)
			{
				num9 = Math.Sqrt(num9);
				double num10 = -num5 + num9;
				if (num10 >= 0.0)
				{
					this.Roots[0] = Math.Pow(num10, 0.33333333333333331);
				}
				else
				{
					this.Roots[0] = -Math.Pow(-num10, 0.33333333333333331);
				}
				num10 = -num5 - num9;
				if (num10 >= 0.0)
				{
					this.Roots[0] += Math.Pow(num10, 0.33333333333333331);
				}
				else
				{
					this.Roots[0] -= Math.Pow(-num10, 0.33333333333333331);
				}
				this.Roots[0] -= num2;
				this.Count = 1;
			}
			else if (num9 < 0.0)
			{
				double num11 = Math.Sqrt(3.0);
				double num12 = Math.Sqrt(-0.33333333333333331 * num3);
				double num13 = 0.33333333333333331 * Math.Atan2(Math.Sqrt(-num9), -num5);
				double num14 = Math.Cos(num13);
				double num15 = Math.Sin(num13);
				this.Roots[0] = 2.0 * num12 * num14 - num2;
				this.Roots[1] = -num12 * (num14 + num11 * num15) - num2;
				this.Roots[2] = -num12 * (num14 - num11 * num15) - num2;
				this.Count = 3;
			}
			else
			{
				double num16;
				if (num5 >= 0.0)
				{
					num16 = -Math.Pow(num5, 0.33333333333333331);
				}
				else
				{
					num16 = Math.Pow(-num5, 0.33333333333333331);
				}
				this.Roots[0] = 2.0 * num16 - num2;
				this.Roots[1] = -num16 - num2;
				this.Roots[2] = this.Roots[1];
				this.Count = 3;
			}
			return true;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00019BB0 File Offset: 0x00017DB0
		public double GetBound(double fC0, double fC1, double fC2, double fC3)
		{
			if (Math.Abs(fC3) <= this.epsilon)
			{
				return this.GetBound(fC0, fC1, fC2);
			}
			double num = 1.0 / fC3;
			double num2 = Math.Abs(fC0) * num;
			double num3 = Math.Abs(fC1) * num;
			if (num3 > num2)
			{
				num2 = num3;
			}
			num3 = Math.Abs(fC2) * num;
			if (num3 > num2)
			{
				num2 = num3;
			}
			return 1.0 + num2;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00019C18 File Offset: 0x00017E18
		public double SpecialCubic(double fA, double fB, double fC)
		{
			double num = Math.Sqrt(1.3333333333333333 * fB / fA);
			double num2 = 4.0 * fC / (fA * num * num * num);
			double num3 = num2;
			double num4 = num2;
			double num5 = Math.Pow(num3 + Math.Sqrt(num4 * num4 + 1.0), 0.33333333333333331);
			return 0.5 * num * (num5 - 1.0 / num5);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00019C88 File Offset: 0x00017E88
		public bool FindA(double fC0, double fC1, double fC2, double fC3, double fC4)
		{
			if (Math.Abs(fC4) <= this.epsilon)
			{
				return this.FindA(fC0, fC1, fC2, fC3);
			}
			double num = 1.0 / fC4;
			fC0 *= num;
			fC1 *= num;
			fC2 *= num;
			fC3 *= num;
			double num2 = -fC3 * fC3 * fC0 + 4.0 * fC2 * fC0;
			double num3 = fC1;
			double fC5 = num2 - num3 * num3;
			double fC6 = fC3 * fC1 - 4.0 * fC0;
			double fC7 = -fC2;
			this.FindA(fC5, fC6, fC7, 1.0);
			double num4 = this.Roots[0];
			this.Count = 0;
			double num5 = 0.25 * fC3 * fC3 - fC2 + num4;
			if (Math.Abs(num5) <= this.epsilon)
			{
				num5 = 0.0;
			}
			if (num5 > 0.0)
			{
				double num6 = Math.Sqrt(num5);
				double num7 = 0.75 * fC3 * fC3;
				double num8 = num6;
				double num9 = num7 - num8 * num8 - 2.0 * fC2;
				double num10 = 4.0 * fC3 * fC2 - 8.0 * fC1;
				double num11 = fC3;
				double num12 = (num10 - num11 * num11 * fC3) / (4.0 * num6);
				double num13 = num9 + num12;
				double num14 = num9 - num12;
				if (Math.Abs(num13) <= this.epsilon)
				{
					num13 = 0.0;
				}
				if (Math.Abs(num14) <= this.epsilon)
				{
					num14 = 0.0;
				}
				if (num13 >= 0.0)
				{
					double num15 = Math.Sqrt(num13);
					this.Roots[0] = -0.25 * fC3 + 0.5 * (num6 + num15);
					this.Roots[1] = -0.25 * fC3 + 0.5 * (num6 - num15);
					this.Count += 2;
				}
				if (num14 >= 0.0)
				{
					double num16 = Math.Sqrt(num14);
					double[] roots = this.Roots;
					int count = this.Count;
					this.Count = count + 1;
					roots[count] = -0.25 * fC3 + 0.5 * (num16 - num6);
					double[] roots2 = this.Roots;
					count = this.Count;
					this.Count = count + 1;
					roots2[count] = -0.25 * fC3 - 0.5 * (num16 + num6);
				}
			}
			else if (num5 < 0.0)
			{
				this.Count = 0;
			}
			else
			{
				double num17 = num4;
				double num18 = num17 * num17 - 4.0 * fC0;
				if (num18 >= -this.epsilon)
				{
					if (num18 < 0.0)
					{
						num18 = 0.0;
					}
					num18 = 2.0 * Math.Sqrt(num18);
					double num19 = 0.75 * fC3 * fC3 - 2.0 * fC2;
					if (num19 + num18 >= this.epsilon)
					{
						double num20 = Math.Sqrt(num19 + num18);
						this.Roots[0] = -0.25 * fC3 + 0.5 * num20;
						this.Roots[1] = -0.25 * fC3 - 0.5 * num20;
						this.Count += 2;
					}
					if (num19 - num18 >= this.epsilon)
					{
						double num21 = Math.Sqrt(num19 - num18);
						double[] roots3 = this.Roots;
						int count = this.Count;
						this.Count = count + 1;
						roots3[count] = -0.25 * fC3 + 0.5 * num21;
						double[] roots4 = this.Roots;
						count = this.Count;
						this.Count = count + 1;
						roots4[count] = -0.25 * fC3 - 0.5 * num21;
					}
				}
			}
			return this.Count > 0;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001A06C File Offset: 0x0001826C
		public double GetBound(double fC0, double fC1, double fC2, double fC3, double fC4)
		{
			if (Math.Abs(fC4) <= this.epsilon)
			{
				return this.GetBound(fC0, fC1, fC2, fC3);
			}
			double num = 1.0 / fC4;
			double num2 = Math.Abs(fC0) * num;
			double num3 = Math.Abs(fC1) * num;
			if (num3 > num2)
			{
				num2 = num3;
			}
			num3 = Math.Abs(fC2) * num;
			if (num3 > num2)
			{
				num2 = num3;
			}
			num3 = Math.Abs(fC3) * num;
			if (num3 > num2)
			{
				num2 = num3;
			}
			return 1.0 + num2;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001A0E4 File Offset: 0x000182E4
		public bool FindB(Polynomial1 poly, int digits)
		{
			double bound = this.GetBound(poly);
			return this.FindB(poly, -bound, bound, digits);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001A104 File Offset: 0x00018304
		public double GetBound(Polynomial1 poly)
		{
			Polynomial1 polynomial = new Polynomial1(poly.Coefficients);
			polynomial.Compress(this.epsilon);
			int degree = polynomial.Degree;
			if (degree < 1)
			{
				return -1.0;
			}
			double num = 1.0 / polynomial[degree];
			double num2 = 0.0;
			for (int i = 0; i < degree; i++)
			{
				double num3 = Math.Abs(polynomial[i]) * num;
				if (num3 > num2)
				{
					num2 = num3;
				}
			}
			return 1.0 + num2;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001A190 File Offset: 0x00018390
		public bool FindB(Polynomial1 poly, double minX, double maxX, int digits)
		{
			if (poly.Degree > this.maxRoot)
			{
				this.maxRoot = poly.Degree;
				this.Roots = new double[this.maxRoot];
			}
			double num2;
			if (poly.Degree != 1)
			{
				Polynomial1 derivative = poly.GetDerivative();
				this.FindB(derivative, minX, maxX, digits);
				int num = 0;
				double[] array = new double[this.Count + 1];
				if (this.Count > 0)
				{
					if (this.Bisection(poly, minX, this.Roots[0], digits, out num2))
					{
						array[num++] = num2;
					}
					for (int i = 0; i <= this.Count - 2; i++)
					{
						if (this.Bisection(poly, this.Roots[i], this.Roots[i + 1], digits, out num2))
						{
							array[num++] = num2;
						}
					}
					if (this.Bisection(poly, this.Roots[this.Count - 1], maxX, digits, out num2))
					{
						array[num++] = num2;
					}
				}
				else if (this.Bisection(poly, minX, maxX, digits, out num2))
				{
					array[num++] = num2;
				}
				if (num > 0)
				{
					this.Count = 1;
					this.Roots[0] = array[0];
					for (int i = 1; i < num; i++)
					{
						if (Math.Abs(array[i] - array[i - 1]) > this.epsilon)
						{
							double[] roots = this.Roots;
							int count = this.Count;
							this.Count = count + 1;
							roots[count] = array[i];
						}
					}
				}
				else
				{
					this.Count = 0;
				}
				return this.Count > 0;
			}
			if (this.Bisection(poly, minX, maxX, digits, out num2))
			{
				this.Count = 1;
				this.Roots[0] = num2;
				return true;
			}
			this.Count = 0;
			return false;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001A334 File Offset: 0x00018534
		public bool AllRealPartsNegative(Polynomial1 poly)
		{
			int degree = poly.Degree;
			double[] array = new double[degree + 1];
			int num = 0;
			foreach (double num2 in poly.Coefficients)
			{
				array[num++] = num2;
			}
			if (array[degree] != 1.0)
			{
				double num3 = 1.0 / array[degree];
				for (int i = 0; i < degree; i++)
				{
					array[i] *= num3;
				}
				array[degree] = 1.0;
			}
			return PolynomialRoots.AllRealPartsNegative(degree, array);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001A3E4 File Offset: 0x000185E4
		public bool AllRealPartsPositive(Polynomial1 poly)
		{
			int degree = poly.Degree;
			double[] array = new double[degree + 1];
			int num = 0;
			foreach (double num2 in poly.Coefficients)
			{
				array[num++] = num2;
			}
			int i;
			if (array[degree] != 1.0)
			{
				double num3 = 1.0 / array[degree];
				for (i = 0; i < degree; i++)
				{
					array[i] *= num3;
				}
				array[degree] = 1.0;
			}
			int num4 = -1;
			i = degree - 1;
			while (i >= 0)
			{
				array[i] *= (double)num4;
				i--;
				num4 = -num4;
			}
			return PolynomialRoots.AllRealPartsNegative(degree, array);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001A4B8 File Offset: 0x000186B8
		private static bool AllRealPartsNegative(int degree, double[] coeff)
		{
			if (coeff[degree - 1] <= 0.0)
			{
				return false;
			}
			if (degree == 1)
			{
				return true;
			}
			double[] array = new double[degree];
			array[0] = 2.0 * coeff[0] * coeff[degree - 1];
			for (int i = 1; i <= degree - 2; i++)
			{
				array[i] = coeff[degree - 1] * coeff[i];
				if ((degree - i) % 2 == 0)
				{
					array[i] -= coeff[i - 1];
				}
				array[i] *= 2.0;
			}
			array[degree - 1] = 2.0 * coeff[degree - 1] * coeff[degree - 1];
			int num = degree - 1;
			while (num >= 0 && array[num] == 0.0)
			{
				num--;
			}
			for (int i = 0; i <= num - 1; i++)
			{
				coeff[i] = array[i] / array[num];
			}
			return PolynomialRoots.AllRealPartsNegative(num, coeff);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001A594 File Offset: 0x00018794
		private bool Bisection(Polynomial1 poly, double minX, double maxX, int digits, out double root)
		{
			double num = poly.Evaluate(minX);
			if (Math.Abs(num) <= this.epsilon)
			{
				root = minX;
				return true;
			}
			double num2 = poly.Evaluate(maxX);
			if (Math.Abs(num2) <= this.epsilon)
			{
				root = maxX;
				return true;
			}
			root = 0.0;
			if (num * num2 > 0.0)
			{
				return false;
			}
			double num3 = Math.Log(maxX - minX);
			double num4 = (double)digits * Math.Log(10.0);
			int num5 = (int)((num3 + num4) / Math.Log(2.0) + 0.5);
			for (int i = 0; i < num5; i++)
			{
				root = 0.5 * (minX + maxX);
				double num6 = poly.Evaluate(root);
				double num7 = num6 * num;
				if (num7 < 0.0)
				{
					maxX = root;
				}
				else
				{
					if (num7 <= 0.0)
					{
						break;
					}
					minX = root;
					num = num6;
				}
			}
			return true;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001A68C File Offset: 0x0001888C
		private bool IsBalancedCompanion3(double a10, double a21, double a02, double a12, double a22)
		{
			if (Math.Abs(1.0 - a10 / a02) > 0.001)
			{
				return false;
			}
			double num = (a10 >= a12) ? a10 : a12;
			if (Math.Abs(1.0 - a21 / num) > 0.001)
			{
				return false;
			}
			num = ((a21 >= a22) ? a21 : a22);
			double num2 = (a02 >= a12) ? a02 : a12;
			if (a22 > num2)
			{
				num2 = a22;
			}
			return Math.Abs(1.0 - num2 / num) <= 0.001;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001A728 File Offset: 0x00018928
		private bool IsBalancedCompanion4(double a10, double a21, double a32, double a03, double a13, double a23, double a33)
		{
			if (Math.Abs(1.0 - a10 / a03) > 0.001)
			{
				return false;
			}
			double num = (a10 >= a13) ? a10 : a13;
			if (Math.Abs(1.0 - a21 / num) > 0.001)
			{
				return false;
			}
			num = ((a21 >= a23) ? a21 : a23);
			if (Math.Abs(1.0 - a32 / num) > 0.001)
			{
				return false;
			}
			num = ((a32 >= a33) ? a32 : a33);
			double num2 = (a03 >= a13) ? a03 : a13;
			if (a23 > num2)
			{
				num2 = a23;
			}
			if (a33 > num2)
			{
				num2 = a33;
			}
			return Math.Abs(1.0 - num2 / num) <= 0.001;
		}

		// Token: 0x04000158 RID: 344
		private readonly double epsilon;

		// Token: 0x04000159 RID: 345
		private int maxRoot;
	}
}
