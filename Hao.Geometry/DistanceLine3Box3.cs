using System;

namespace Hao.Geometry
{
	// Token: 0x02000017 RID: 23
	internal struct DistanceLine3Box3
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00003CA9 File Offset: 0x00001EA9
		public DistanceLine3Box3(Line3 line, Box3 box)
		{
			this = default(DistanceLine3Box3);
			this.LineParameter = 0.0;
			this.CalcSquared(ref line, ref box);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003CCB File Offset: 0x00001ECB
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00003CD3 File Offset: 0x00001ED3
		public double LineParameter { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003CDC File Offset: 0x00001EDC
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003CE4 File Offset: 0x00001EE4
		public Vector3 ClosestPointOnLine { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003CED File Offset: 0x00001EED
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00003CF5 File Offset: 0x00001EF5
		public Vector3 ClosestPointOnBox { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003CFE File Offset: 0x00001EFE
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003D06 File Offset: 0x00001F06
		public double SquaredDistance { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003D0F File Offset: 0x00001F0F
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003D1C File Offset: 0x00001F1C
		private static void Case000(ref Box3 box, ref double[] point, ref double squaredDistance)
		{
			if (point[0] < -box.Extent0)
			{
				double num = point[0] + box.Extent0;
				double num2 = squaredDistance;
				double num3 = num;
				squaredDistance = num2 + num3 * num3;
				point[0] = -box.Extent0;
			}
			else if (point[0] > box.Extent0)
			{
				double num4 = point[0] - box.Extent0;
				double num5 = squaredDistance;
				double num6 = num4;
				squaredDistance = num5 + num6 * num6;
				point[0] = box.Extent0;
			}
			if (point[1] < -box.Extent1)
			{
				double num7 = point[1] + box.Extent1;
				double num8 = squaredDistance;
				double num9 = num7;
				squaredDistance = num8 + num9 * num9;
				point[1] = -box.Extent1;
			}
			else if (point[1] > box.Extent1)
			{
				double num10 = point[1] - box.Extent1;
				double num11 = squaredDistance;
				double num12 = num10;
				squaredDistance = num11 + num12 * num12;
				point[1] = box.Extent1;
			}
			if (point[2] < -box.Extent2)
			{
				double num13 = point[2] + box.Extent2;
				double num14 = squaredDistance;
				double num15 = num13;
				squaredDistance = num14 + num15 * num15;
				point[2] = -box.Extent2;
				return;
			}
			if (point[2] > box.Extent2)
			{
				double num16 = point[2] - box.Extent2;
				double num17 = squaredDistance;
				double num18 = num16;
				squaredDistance = num17 + num18 * num18;
				point[2] = box.Extent2;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003E34 File Offset: 0x00002034
		private void CalcSquared(ref Line3 line, ref Box3 box)
		{
			Vector3 vector = line.Origin - box.Center;
			double[] array = new double[]
			{
				vector.Dot(box.Axis0),
				vector.Dot(box.Axis1),
				vector.Dot(box.Axis2)
			};
			double[] array2 = new double[]
			{
				line.Direction.Dot(box.Axis0),
				line.Direction.Dot(box.Axis1),
				line.Direction.Dot(box.Axis2)
			};
			bool[] array3 = new bool[3];
			for (int i = 0; i < 3; i++)
			{
				if (array2[i] < 0.0)
				{
					array[i] = -array[i];
					array2[i] = -array2[i];
					array3[i] = true;
				}
				else
				{
					array3[i] = false;
				}
			}
			double squaredDistance = 0.0;
			this.LineParameter = 0.0;
			if (array2[0] > 0.0)
			{
				if (array2[1] > 0.0)
				{
					if (array2[2] > 0.0)
					{
						this.CaseNoZeros(ref box, ref array, ref array2, ref squaredDistance);
					}
					else
					{
						this.Case0(ref box, 0, 1, 2, ref array, ref array2, ref squaredDistance);
					}
				}
				else if (array2[2] > 0.0)
				{
					this.Case0(ref box, 0, 2, 1, ref array, ref array2, ref squaredDistance);
				}
				else
				{
					this.Case00(ref box, 0, 1, 2, ref array, ref array2, ref squaredDistance);
				}
			}
			else if (array2[1] > 0.0)
			{
				if (array2[2] > 0.0)
				{
					this.Case0(ref box, 1, 2, 0, ref array, ref array2, ref squaredDistance);
				}
				else
				{
					this.Case00(ref box, 1, 0, 2, ref array, ref array2, ref squaredDistance);
				}
			}
			else if (array2[2] > 0.0)
			{
				this.Case00(ref box, 2, 0, 1, ref array, ref array2, ref squaredDistance);
			}
			else
			{
				DistanceLine3Box3.Case000(ref box, ref array, ref squaredDistance);
			}
			for (int i = 0; i < 3; i++)
			{
				if (array3[i])
				{
					array[i] = -array[i];
				}
			}
			this.ClosestPointOnLine = line.Origin + this.LineParameter * line.Direction;
			this.ClosestPointOnBox = box.Center + array[0] * box.Axis0 + array[1] * box.Axis1 + array[2] * box.Axis2;
			this.SquaredDistance = squaredDistance;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000040B4 File Offset: 0x000022B4
		private void Face(ref Box3 box, int i0, int i1, int i2, ref double[] point, ref double[] direction, ref double[] pme, ref double squaredDistance)
		{
			double[] array = new double[3];
			double[] array2 = new double[]
			{
				box.Extent0,
				box.Extent1,
				box.Extent2
			};
			array[i1] = point[i1] + array2[i1];
			array[i2] = point[i2] + array2[i2];
			if (direction[i0] * array[i1] >= direction[i1] * pme[i0])
			{
				if (direction[i0] * array[i2] >= direction[i2] * pme[i0])
				{
					point[i0] = array2[i0];
					double num = 1.0 / direction[i0];
					point[i1] -= direction[i1] * pme[i0] * num;
					point[i2] -= direction[i2] * pme[i0] * num;
					this.LineParameter = -pme[i0] * num;
					return;
				}
				double num2 = direction[i0] * direction[i0] + direction[i2] * direction[i2];
				double num3 = num2 * array[i1] - direction[i1] * (direction[i0] * pme[i0] + direction[i2] * array[i2]);
				if (num3 <= 2.0 * num2 * array2[i1])
				{
					double num4 = num3 / num2;
					num2 += direction[i1] * direction[i1];
					num3 = array[i1] - num4;
					double num5 = direction[i0] * pme[i0] + direction[i1] * num3 + direction[i2] * array[i2];
					double num6 = -num5 / num2;
					double num7 = squaredDistance;
					double num8 = pme[i0] * pme[i0];
					double num9 = num3;
					squaredDistance = num7 + (num8 + num9 * num9 + array[i2] * array[i2] + num5 * num6);
					this.LineParameter = num6;
					point[i0] = array2[i0];
					point[i1] = num4 - array2[i1];
					point[i2] = -array2[i2];
					return;
				}
				num2 += direction[i1] * direction[i1];
				double num10 = direction[i0] * pme[i0] + direction[i1] * pme[i1] + direction[i2] * array[i2];
				double num11 = -num10 / num2;
				squaredDistance += pme[i0] * pme[i0] + pme[i1] * pme[i1] + array[i2] * array[i2] + num10 * num11;
				this.LineParameter = num11;
				point[i0] = array2[i0];
				point[i1] = array2[i1];
				point[i2] = -array2[i2];
				return;
			}
			else if (direction[i0] * array[i2] >= direction[i2] * pme[i0])
			{
				double num12 = direction[i0] * direction[i0] + direction[i1] * direction[i1];
				double num13 = num12 * array[i2] - direction[i2] * (direction[i0] * pme[i0] + direction[i1] * array[i1]);
				if (num13 <= 2.0 * num12 * array2[i2])
				{
					double num14 = num13 / num12;
					num12 += direction[i2] * direction[i2];
					num13 = array[i2] - num14;
					double num15 = direction[i0] * pme[i0] + direction[i1] * array[i1] + direction[i2] * num13;
					double num16 = -num15 / num12;
					double num17 = squaredDistance;
					double num18 = pme[i0] * pme[i0] + array[i1] * array[i1];
					double num19 = num13;
					squaredDistance = num17 + (num18 + num19 * num19 + num15 * num16);
					this.LineParameter = num16;
					point[i0] = array2[i0];
					point[i1] = -array2[i1];
					point[i2] = num14 - array2[i2];
					return;
				}
				num12 += direction[i2] * direction[i2];
				double num20 = direction[i0] * pme[i0] + direction[i1] * array[i1] + direction[i2] * pme[i2];
				double num21 = -num20 / num12;
				squaredDistance += pme[i0] * pme[i0] + array[i1] * array[i1] + pme[i2] * pme[i2] + num20 * num21;
				this.LineParameter = num21;
				point[i0] = array2[i0];
				point[i1] = -array2[i1];
				point[i2] = array2[i2];
				return;
			}
			else
			{
				double num22 = direction[i0] * direction[i0] + direction[i2] * direction[i2];
				double num23 = num22 * array[i1] - direction[i1] * (direction[i0] * pme[i0] + direction[i2] * array[i2]);
				if (num23 >= 0.0)
				{
					if (num23 <= 2.0 * num22 * array2[i1])
					{
						double num14 = num23 / num22;
						num22 += direction[i1] * direction[i1];
						num23 = array[i1] - num14;
						double num24 = direction[i0] * pme[i0] + direction[i1] * num23 + direction[i2] * array[i2];
						double num25 = -num24 / num22;
						double num26 = squaredDistance;
						double num27 = pme[i0] * pme[i0];
						double num28 = num23;
						squaredDistance = num26 + (num27 + num28 * num28 + array[i2] * array[i2] + num24 * num25);
						this.LineParameter = num25;
						point[i0] = array2[i0];
						point[i1] = num14 - array2[i1];
						point[i2] = -array2[i2];
						return;
					}
					num22 += direction[i1] * direction[i1];
					double num29 = direction[i0] * pme[i0] + direction[i1] * pme[i1] + direction[i2] * array[i2];
					double num30 = -num29 / num22;
					squaredDistance += pme[i0] * pme[i0] + pme[i1] * pme[i1] + array[i2] * array[i2] + num29 * num30;
					this.LineParameter = num30;
					point[i0] = array2[i0];
					point[i1] = array2[i1];
					point[i2] = -array2[i2];
					return;
				}
				else
				{
					num22 = direction[i0] * direction[i0] + direction[i1] * direction[i1];
					num23 = num22 * array[i2] - direction[i2] * (direction[i0] * pme[i0] + direction[i1] * array[i1]);
					if (num23 < 0.0)
					{
						num22 += direction[i2] * direction[i2];
						double num31 = direction[i0] * pme[i0] + direction[i1] * array[i1] + direction[i2] * array[i2];
						double num32 = -num31 / num22;
						squaredDistance += pme[i0] * pme[i0] + array[i1] * array[i1] + array[i2] * array[i2] + num31 * num32;
						this.LineParameter = num32;
						point[i0] = array2[i0];
						point[i1] = -array2[i1];
						point[i2] = -array2[i2];
						return;
					}
					if (num23 <= 2.0 * num22 * array2[i2])
					{
						double num14 = num23 / num22;
						num22 += direction[i2] * direction[i2];
						num23 = array[i2] - num14;
						double num33 = direction[i0] * pme[i0] + direction[i1] * array[i1] + direction[i2] * num23;
						double num34 = -num33 / num22;
						double num35 = squaredDistance;
						double num36 = pme[i0] * pme[i0] + array[i1] * array[i1];
						double num37 = num23;
						squaredDistance = num35 + (num36 + num37 * num37 + num33 * num34);
						this.LineParameter = num34;
						point[i0] = array2[i0];
						point[i1] = -array2[i1];
						point[i2] = num14 - array2[i2];
						return;
					}
					num22 += direction[i2] * direction[i2];
					double num38 = direction[i0] * pme[i0] + direction[i1] * array[i1] + direction[i2] * pme[i2];
					double num39 = -num38 / num22;
					squaredDistance += pme[i0] * pme[i0] + array[i1] * array[i1] + pme[i2] * pme[i2] + num38 * num39;
					this.LineParameter = num39;
					point[i0] = array2[i0];
					point[i1] = -array2[i1];
					point[i2] = array2[i2];
					return;
				}
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004840 File Offset: 0x00002A40
		private void CaseNoZeros(ref Box3 box, ref double[] point, ref double[] dir, ref double squaredDistance)
		{
			double[] array = new double[]
			{
				point[0] - box.Extent0,
				point[1] - box.Extent1,
				point[2] - box.Extent2
			};
			double num = dir[0] * array[1];
			if (dir[1] * array[0] >= num)
			{
				double num2 = dir[2] * array[0];
				double num3 = dir[0] * array[2];
				if (num2 >= num3)
				{
					this.Face(ref box, 0, 1, 2, ref point, ref dir, ref array, ref squaredDistance);
					return;
				}
				this.Face(ref box, 2, 0, 1, ref point, ref dir, ref array, ref squaredDistance);
				return;
			}
			else
			{
				double num4 = dir[2] * array[1];
				double num5 = dir[1] * array[2];
				if (num4 >= num5)
				{
					this.Face(ref box, 1, 2, 0, ref point, ref dir, ref array, ref squaredDistance);
					return;
				}
				this.Face(ref box, 2, 0, 1, ref point, ref dir, ref array, ref squaredDistance);
				return;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004900 File Offset: 0x00002B00
		private void Case0(ref Box3 box, int i0, int i1, int i2, ref double[] point, ref double[] dir, ref double squaredDistance)
		{
			double[] array = new double[]
			{
				box.Extent0,
				box.Extent1,
				box.Extent2
			};
			double num = point[i0] - array[i0];
			double num2 = point[i1] - array[i1];
			double num3 = dir[i1] * num;
			double num4 = dir[i0] * num2;
			if (num3 >= num4)
			{
				point[i0] = array[i0];
				double num5 = point[i1] + array[i1];
				double num6 = num3 - dir[i0] * num5;
				if (num6 >= 0.0)
				{
					double num7 = 1.0 / (dir[i0] * dir[i0] + dir[i1] * dir[i1]);
					double num8 = squaredDistance;
					double num9 = num6;
					squaredDistance = num8 + num9 * num9 * num7;
					point[i1] = -array[i1];
					this.LineParameter = -(dir[i0] * num + dir[i1] * num5) * num7;
				}
				else
				{
					double num10 = 1.0 / dir[i0];
					point[i1] -= num3 * num10;
					this.LineParameter = -num * num10;
				}
			}
			else
			{
				point[i1] = array[i1];
				double num11 = point[i0] + array[i0];
				double num12 = num4 - dir[i1] * num11;
				if (num12 >= 0.0)
				{
					double num13 = 1.0 / (dir[i0] * dir[i0] + dir[i1] * dir[i1]);
					double num14 = squaredDistance;
					double num15 = num12;
					squaredDistance = num14 + num15 * num15 * num13;
					point[i0] = -array[i0];
					this.LineParameter = -(dir[i0] * num11 + dir[i1] * num2) * num13;
				}
				else
				{
					double num16 = 1.0 / dir[i1];
					point[i0] -= num4 * num16;
					this.LineParameter = -num2 * num16;
				}
			}
			if (point[i2] < -array[i2])
			{
				double num17 = point[i2] + array[i2];
				double num18 = squaredDistance;
				double num19 = num17;
				squaredDistance = num18 + num19 * num19;
				point[i2] = -array[i2];
				return;
			}
			if (point[i2] > array[i2])
			{
				double num20 = point[i2] - array[i2];
				double num21 = squaredDistance;
				double num22 = num20;
				squaredDistance = num21 + num22 * num22;
				point[i2] = array[i2];
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004B20 File Offset: 0x00002D20
		private void Case00(ref Box3 box, int i0, int i1, int i2, ref double[] point, ref double[] dir, ref double squaredDistance)
		{
			double[] array = new double[]
			{
				box.Extent0,
				box.Extent1,
				box.Extent2
			};
			this.LineParameter = (array[i0] - point[i0]) / dir[i0];
			point[i0] = array[i0];
			if (point[i1] < -array[i1])
			{
				double num = point[i1] + array[i1];
				double num2 = squaredDistance;
				double num3 = num;
				squaredDistance = num2 + num3 * num3;
				point[i1] = -array[i1];
			}
			else if (point[i1] > array[i1])
			{
				double num4 = point[i1] - array[i1];
				double num5 = squaredDistance;
				double num6 = num4;
				squaredDistance = num5 + num6 * num6;
				point[i1] = array[i1];
			}
			if (point[i2] < -array[i2])
			{
				double num7 = point[i2] + array[i2];
				double num8 = squaredDistance;
				double num9 = num7;
				squaredDistance = num8 + num9 * num9;
				point[i2] = -array[i2];
				return;
			}
			if (point[i2] > array[i2])
			{
				double num10 = point[i2] - array[i2];
				double num11 = squaredDistance;
				double num12 = num10;
				squaredDistance = num11 + num12 * num12;
				point[i2] = array[i2];
			}
		}
	}
}
