using System;

namespace Hao.Geometry
{
	// Token: 0x02000035 RID: 53
	internal struct DistanceVector3Triangle3
	{
		// Token: 0x06000244 RID: 580 RVA: 0x00009A3E File Offset: 0x00007C3E
		public DistanceVector3Triangle3(Vector3 vector, Triangle3 triangle)
		{
			this = default(DistanceVector3Triangle3);
			this.CalcSquared(ref vector, ref triangle);
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00009A51 File Offset: 0x00007C51
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00009A59 File Offset: 0x00007C59
		public double SquaredDistance { get; private set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00009A62 File Offset: 0x00007C62
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00009A6F File Offset: 0x00007C6F
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00009A77 File Offset: 0x00007C77
		public double TriangleBary0 { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00009A80 File Offset: 0x00007C80
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00009A88 File Offset: 0x00007C88
		public double TriangleBary1 { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00009A91 File Offset: 0x00007C91
		// (set) Token: 0x0600024D RID: 589 RVA: 0x00009A99 File Offset: 0x00007C99
		public double TriangleBary2 { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00009AA2 File Offset: 0x00007CA2
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00009AAA File Offset: 0x00007CAA
		public Vector3 ClosestPointOnVector { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00009AB3 File Offset: 0x00007CB3
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00009ABB File Offset: 0x00007CBB
		public Vector3 ClosestPointOnTriangle { get; private set; }

		// Token: 0x06000252 RID: 594 RVA: 0x00009AC4 File Offset: 0x00007CC4
		private void CalcSquared(ref Vector3 vector, ref Triangle3 triangle)
		{
			Vector3 vector2 = triangle.V0 - vector;
			Vector3 vector3 = triangle.V1 - triangle.V0;
			Vector3 vector4 = triangle.V2 - triangle.V0;
			double squaredLength = vector3.SquaredLength;
			double num = vector3.Dot(vector4);
			double squaredLength2 = vector4.SquaredLength;
			double num2 = vector2.Dot(vector3);
			double num3 = vector2.Dot(vector4);
			double squaredLength3 = vector2.SquaredLength;
			double num4 = squaredLength * squaredLength2;
			double num5 = num;
			double num6 = Math.Abs(num4 - num5 * num5);
			double num7 = num * num3 - squaredLength2 * num2;
			double num8 = num * num2 - squaredLength * num3;
			double num9;
			if (num7 + num8 <= num6)
			{
				if (num7 < 0.0)
				{
					if (num8 < 0.0)
					{
						if (num2 < 0.0)
						{
							num8 = 0.0;
							if (-num2 >= squaredLength)
							{
								num7 = 1.0;
								num9 = squaredLength + 2.0 * num2 + squaredLength3;
							}
							else
							{
								num7 = -num2 / squaredLength;
								num9 = num2 * num7 + squaredLength3;
							}
						}
						else
						{
							num7 = 0.0;
							if (num3 >= 0.0)
							{
								num8 = 0.0;
								num9 = squaredLength3;
							}
							else if (-num3 >= squaredLength2)
							{
								num8 = 1.0;
								num9 = squaredLength2 + 2.0 * num3 + squaredLength3;
							}
							else
							{
								num8 = -num3 / squaredLength2;
								num9 = num3 * num8 + squaredLength3;
							}
						}
					}
					else
					{
						num7 = 0.0;
						if (num3 >= 0.0)
						{
							num8 = 0.0;
							num9 = squaredLength3;
						}
						else if (-num3 >= squaredLength2)
						{
							num8 = 1.0;
							num9 = squaredLength2 + 2.0 * num3 + squaredLength3;
						}
						else
						{
							num8 = -num3 / squaredLength2;
							num9 = num3 * num8 + squaredLength3;
						}
					}
				}
				else if (num8 < 0.0)
				{
					num8 = 0.0;
					if (num2 >= 0.0)
					{
						num7 = 0.0;
						num9 = squaredLength3;
					}
					else if (-num2 >= squaredLength)
					{
						num7 = 1.0;
						num9 = squaredLength + 2.0 * num2 + squaredLength3;
					}
					else
					{
						num7 = -num2 / squaredLength;
						num9 = num2 * num7 + squaredLength3;
					}
				}
				else
				{
					if (num6 == 0.0)
					{
						Vector3[] array = new Vector3[]
						{
							triangle.V1 - triangle.V0,
							triangle.V2 - triangle.V1,
							triangle.V0 - triangle.V2
						};
						int num10 = IntersectionUtility3.MaxIndex(array[0].SquaredLength, array[1].SquaredLength, array[2].SquaredLength);
						Segment3 segment = new Segment3(triangle[num10], triangle[(num10 + 1) % 3]);
						DistanceVector3Segment3 distanceVector3Segment = new DistanceVector3Segment3(vector, segment);
						this.ClosestPointOnVector = vector;
						this.ClosestPointOnTriangle = distanceVector3Segment.ClosestPointOnSegment;
						this.SquaredDistance = distanceVector3Segment.SquaredDistance;
						return;
					}
					double num11 = 1.0 / num6;
					num7 *= num11;
					num8 *= num11;
					num9 = num7 * (squaredLength * num7 + num * num8 + 2.0 * num2) + num8 * (num * num7 + squaredLength2 * num8 + 2.0 * num3) + squaredLength3;
				}
			}
			else if (num7 < 0.0)
			{
				double num12 = num + num2;
				double num13 = squaredLength2 + num3;
				if (num13 > num12)
				{
					double num14 = num13 - num12;
					double num15 = squaredLength - 2.0 * num + squaredLength2;
					if (num14 >= num15)
					{
						num7 = 1.0;
						num8 = 0.0;
						num9 = squaredLength + 2.0 * num2 + squaredLength3;
					}
					else
					{
						num7 = num14 / num15;
						num8 = 1.0 - num7;
						num9 = num7 * (squaredLength * num7 + num * num8 + 2.0 * num2) + num8 * (num * num7 + squaredLength2 * num8 + 2.0 * num3) + squaredLength3;
					}
				}
				else
				{
					num7 = 0.0;
					if (num13 <= 0.0)
					{
						num8 = 1.0;
						num9 = squaredLength2 + 2.0 * num3 + squaredLength3;
					}
					else if (num3 >= 0.0)
					{
						num8 = 0.0;
						num9 = squaredLength3;
					}
					else
					{
						num8 = -num3 / squaredLength2;
						num9 = num3 * num8 + squaredLength3;
					}
				}
			}
			else if (num8 < 0.0)
			{
				double num16 = num + num3;
				double num17 = squaredLength + num2;
				if (num17 > num16)
				{
					double num14 = num17 - num16;
					double num15 = squaredLength - 2.0 * num + squaredLength2;
					if (num14 >= num15)
					{
						num8 = 1.0;
						num7 = 0.0;
						num9 = squaredLength2 + 2.0 * num3 + squaredLength3;
					}
					else
					{
						num8 = num14 / num15;
						num7 = 1.0 - num8;
						num9 = num7 * (squaredLength * num7 + num * num8 + 2.0 * num2) + num8 * (num * num7 + squaredLength2 * num8 + 2.0 * num3) + squaredLength3;
					}
				}
				else
				{
					num8 = 0.0;
					if (num17 <= 0.0)
					{
						num7 = 1.0;
						num9 = squaredLength + 2.0 * num2 + squaredLength3;
					}
					else if (num2 >= 0.0)
					{
						num7 = 0.0;
						num9 = squaredLength3;
					}
					else
					{
						num7 = -num2 / squaredLength;
						num9 = num2 * num7 + squaredLength3;
					}
				}
			}
			else
			{
				double num14 = squaredLength2 + num3 - num - num2;
				if (num14 <= 0.0)
				{
					num7 = 0.0;
					num8 = 1.0;
					num9 = squaredLength2 + 2.0 * num3 + squaredLength3;
				}
				else
				{
					double num15 = squaredLength - 2.0 * num + squaredLength2;
					if (num14 >= num15)
					{
						num7 = 1.0;
						num8 = 0.0;
						num9 = squaredLength + 2.0 * num2 + squaredLength3;
					}
					else
					{
						num7 = num14 / num15;
						num8 = 1.0 - num7;
						num9 = num7 * (squaredLength * num7 + num * num8 + 2.0 * num2) + num8 * (num * num7 + squaredLength2 * num8 + 2.0 * num3) + squaredLength3;
					}
				}
			}
			if (num9 < 0.0)
			{
				num9 = 0.0;
			}
			this.ClosestPointOnVector = vector;
			this.ClosestPointOnTriangle = triangle.V0 + num7 * vector3 + num8 * vector4;
			this.TriangleBary1 = num7;
			this.TriangleBary2 = num8;
			this.TriangleBary0 = 1.0 - num7 - num8;
			this.SquaredDistance = num9;
		}
	}
}
