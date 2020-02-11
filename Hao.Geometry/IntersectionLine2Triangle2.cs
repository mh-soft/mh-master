using System;

namespace Hao.Geometry
{
	// Token: 0x02000055 RID: 85
	internal struct IntersectionLine2Triangle2
	{
		// Token: 0x06000354 RID: 852 RVA: 0x0000E3D1 File Offset: 0x0000C5D1
		public IntersectionLine2Triangle2(Line2 line, Triangle2 triangle)
		{
			this = default(IntersectionLine2Triangle2);
			this.line = line;
			this.triangle = triangle;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000E3E8 File Offset: 0x0000C5E8
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		public int Quantity { get; private set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000E3F9 File Offset: 0x0000C5F9
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000E401 File Offset: 0x0000C601
		public Vector2 Point0 { get; private set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000E40A File Offset: 0x0000C60A
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000E412 File Offset: 0x0000C612
		public Vector2 Point1 { get; private set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000E41B File Offset: 0x0000C61B
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000E423 File Offset: 0x0000C623
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x0600035D RID: 861 RVA: 0x0000E42C File Offset: 0x0000C62C
		public bool Test()
		{
			double[] dist = new double[3];
			int[] sign = new int[3];
			int num;
			int num2;
			int num3;
			IntersectionLine2Triangle2.TriangleLineRelations(this.line.Origin, this.line.Direction, this.triangle, ref dist, ref sign, out num, out num2, out num3);
			if (num == 3 || num2 == 3)
			{
				this.IntersectionType = Intersection.Type.IT_EMPTY;
			}
			else
			{
				double[] array = new double[2];
				IntersectionLine2Triangle2.GetInterval(this.line.Origin, this.line.Direction, this.triangle, dist, sign, ref array);
				Intersector1 intersector = new Intersector1(array[0], array[1], double.MinValue, double.MaxValue);
				intersector.Find();
				this.Quantity = intersector.Quantity;
				if (this.Quantity == 2)
				{
					this.IntersectionType = Intersection.Type.IT_SEGMENT;
				}
				else if (this.Quantity == 1)
				{
					this.IntersectionType = Intersection.Type.IT_POINT;
				}
				else
				{
					this.IntersectionType = Intersection.Type.IT_EMPTY;
				}
			}
			return this.IntersectionType > Intersection.Type.IT_EMPTY;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000E530 File Offset: 0x0000C730
		public bool Find()
		{
			double[] dist = new double[3];
			int[] sign = new int[3];
			int num;
			int num2;
			int num3;
			IntersectionLine2Triangle2.TriangleLineRelations(this.line.Origin, this.line.Direction, this.triangle, ref dist, ref sign, out num, out num2, out num3);
			if (num == 3 || num2 == 3)
			{
				this.Quantity = 0;
				this.IntersectionType = Intersection.Type.IT_EMPTY;
			}
			else
			{
				double[] array = new double[2];
				IntersectionLine2Triangle2.GetInterval(this.line.Origin, this.line.Direction, this.triangle, dist, sign, ref array);
				Intersector1 intersector = new Intersector1(array[0], array[1], double.MinValue, double.MaxValue);
				intersector.Find();
				this.Quantity = intersector.Quantity;
				if (this.Quantity == 2)
				{
					this.IntersectionType = Intersection.Type.IT_SEGMENT;
					this.Point0 = this.line.Origin + intersector.Overlap0 * this.line.Direction;
					this.Point1 = this.line.Origin + intersector.Overlap1 * this.line.Direction;
				}
				else if (this.Quantity == 1)
				{
					this.IntersectionType = Intersection.Type.IT_POINT;
					this.Point0 = this.line.Origin + intersector.Overlap0 * this.line.Direction;
				}
				else
				{
					this.IntersectionType = Intersection.Type.IT_EMPTY;
				}
			}
			return this.IntersectionType > Intersection.Type.IT_EMPTY;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000E6D8 File Offset: 0x0000C8D8
		internal static void TriangleLineRelations(Vector2 origin, UnitVector2 direction, Triangle2 triangle, ref double[] dist, ref int[] sign, out int positive, out int negative, out int zero)
		{
			positive = 0;
			negative = 0;
			zero = 0;
			for (int i = 0; i < 3; i++)
			{
				Vector2 vector = triangle[i] - origin;
				dist[i] = vector.DotPerpendicular((Vector2)direction);
				if (dist[i] > 1E-08)
				{
					sign[i] = 1;
					positive++;
				}
				else if (dist[i] < -1E-08)
				{
					sign[i] = -1;
					negative++;
				}
				else
				{
					dist[i] = 0.0;
					sign[i] = 0;
					zero++;
				}
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000E774 File Offset: 0x0000C974
		internal static void GetInterval(Vector2 origin, UnitVector2 direction, Triangle2 triangle, double[] dist, int[] sign, ref double[] param)
		{
			double[] array = new double[3];
			for (int i = 0; i < 3; i++)
			{
				Vector2 vector = triangle[i] - origin;
				array[i] = direction.Dot(vector);
			}
			int num = 0;
			int num2 = 2;
			int j = 0;
			while (j < 3)
			{
				if (sign[num2] * sign[j] < 0)
				{
					MathBase.Assert(num < 2, "Line2Triangle2.GetInterval(): iQuantity < 2 failed");
					double num3 = dist[num2] * array[j] - dist[j] * array[num2];
					double num4 = dist[num2] - dist[j];
					param[num++] = num3 / num4;
				}
				num2 = j++;
			}
			if (num < 2)
			{
				int k = 0;
				while (k < 3)
				{
					if (sign[k] == 0)
					{
						MathBase.Assert(num < 2, "Line2Triangle2.GetInterval(): iQuantity < 2 failed");
						param[num++] = array[k];
					}
					int num5 = k++;
				}
			}
			MathBase.Assert(num >= 1, "Line2Triangle2.GetInterval(): iQuantity >= 1 failed");
			if (num == 2)
			{
				if (param[0] > param[1])
				{
					double num6 = param[0];
					param[0] = param[1];
					param[1] = num6;
					return;
				}
			}
			else
			{
				param[1] = param[0];
			}
		}

		// Token: 0x040000E4 RID: 228
		private readonly Line2 line;

		// Token: 0x040000E5 RID: 229
		private readonly Triangle2 triangle;
	}
}
