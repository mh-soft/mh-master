using System;

namespace Hao.Geometry
{
	// Token: 0x0200006E RID: 110
	internal struct IntersectionTriangle2Triangle2
	{
		// Token: 0x0600043C RID: 1084 RVA: 0x000131A8 File Offset: 0x000113A8
		public IntersectionTriangle2Triangle2(Triangle2 triangle0, Triangle2 triangle1)
		{
			this = default(IntersectionTriangle2Triangle2);
			this.Points = new Vector2[6];
			this.triangle0 = triangle0;
			this.triangle1 = triangle1;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x000131CB File Offset: 0x000113CB
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x000131D3 File Offset: 0x000113D3
		public int Quantity { get; private set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x000131DC File Offset: 0x000113DC
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x000131E4 File Offset: 0x000113E4
		public Vector2[] Points { get; private set; }

		// Token: 0x06000441 RID: 1089 RVA: 0x000131F0 File Offset: 0x000113F0
		public bool Test()
		{
			int i = 0;
			int index = 2;
			while (i < 3)
			{
				Vector2 rkD = new Vector2(this.triangle0[i].Y - this.triangle0[index].Y, this.triangle0[index].X - this.triangle0[i].X);
				if (IntersectionTriangle2Triangle2.WhichSide(this.triangle1, this.triangle0[index], rkD) > 0)
				{
					return false;
				}
				index = i;
				i++;
			}
			i = 0;
			index = 2;
			while (i < 3)
			{
				Vector2 rkD2 = new Vector2(this.triangle1[i].Y - this.triangle1[index].Y, this.triangle1[index].X - this.triangle1[i].X);
				if (IntersectionTriangle2Triangle2.WhichSide(this.triangle0, this.triangle1[index], rkD2) > 0)
				{
					return false;
				}
				index = i;
				i++;
			}
			return true;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00013340 File Offset: 0x00011540
		public bool Find()
		{
			this.Quantity = 3;
			for (int i = 0; i < 3; i++)
			{
				this.Points[i] = this.triangle1[i];
			}
			int index = 2;
			for (int j = 0; j < 3; j++)
			{
				Vector2 rkN = new Vector2(this.triangle0[index].Y - this.triangle0[j].Y, this.triangle0[j].X - this.triangle0[index].X);
				double fC = rkN.Dot(this.triangle0[index]);
				int quantity = this.Quantity;
				Vector2[] points = this.Points;
				IntersectionTriangle2Triangle2.ClipConvexPolygonAgainstLine(rkN, fC, ref quantity, ref points);
				this.Points = points;
				this.Quantity = quantity;
				if (this.Quantity == 0)
				{
					return false;
				}
				index = j;
			}
			return true;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001344C File Offset: 0x0001164C
		private static int WhichSide(Triangle2 akV, Vector2 rkP, Vector2 rkD)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < 3; i++)
			{
				double num4 = rkD.Dot(akV[i] - rkP);
				if (num4 > 0.0)
				{
					num++;
				}
				else if (num4 < 0.0)
				{
					num2++;
				}
				else
				{
					num3++;
				}
				if (num > 0 && num2 > 0)
				{
					return 0;
				}
			}
			if (num3 != 0)
			{
				return 0;
			}
			if (num <= 0)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000134C4 File Offset: 0x000116C4
		private static void ClipConvexPolygonAgainstLine(Vector2 rkN, double fC, ref int quantity, ref Vector2[] akV)
		{
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			double[] array = new double[6];
			for (int i = 0; i < quantity; i++)
			{
				array[i] = rkN.Dot(akV[i]) - fC;
				if (array[i] > 0.0)
				{
					num++;
					if (num3 < 0)
					{
						num3 = i;
					}
				}
				else if (array[i] < 0.0)
				{
					num2++;
				}
			}
			if (num > 0)
			{
				if (num2 > 0)
				{
					Vector2[] array2 = new Vector2[6];
					int num4 = 0;
					if (num3 > 0)
					{
						int j = num3;
						int num5 = j - 1;
						double scalar = array[j] / (array[j] - array[num5]);
						array2[num4++] = akV[j] + scalar * (akV[num5] - akV[j]);
						while (j < quantity && array[j] > 0.0)
						{
							array2[num4++] = akV[j++];
						}
						if (j < quantity)
						{
							num5 = j - 1;
						}
						else
						{
							j = 0;
							num5 = quantity - 1;
						}
						scalar = array[j] / (array[j] - array[num5]);
						array2[num4++] = akV[j] + scalar * (akV[num5] - akV[j]);
					}
					else
					{
						int j = 0;
						while (j < quantity && array[j] > 0.0)
						{
							array2[num4++] = akV[j++];
						}
						int num5 = j - 1;
						double scalar = array[j] / (array[j] - array[num5]);
						array2[num4++] = akV[j] + scalar * (akV[num5] - akV[j]);
						while (j < quantity && array[j] <= 0.0)
						{
							j++;
						}
						if (j < quantity)
						{
							num5 = j - 1;
							scalar = array[j] / (array[j] - array[num5]);
							array2[num4++] = akV[j] + scalar * (akV[num5] - akV[j]);
							while (j < quantity)
							{
								if (array[j] <= 0.0)
								{
									break;
								}
								array2[num4++] = akV[j++];
							}
						}
						else
						{
							num5 = quantity - 1;
							scalar = array[0] / (array[0] - array[num5]);
							array2[num4++] = akV[0] + scalar * (akV[num5] - akV[0]);
						}
					}
					quantity = num4;
					akV = array2;
					return;
				}
			}
			else
			{
				quantity = 0;
			}
		}

		// Token: 0x0400013D RID: 317
		private readonly Triangle2 triangle0;

		// Token: 0x0400013E RID: 318
		private readonly Triangle2 triangle1;
	}
}
