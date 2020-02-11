using System;

namespace Hao.Geometry
{
	// Token: 0x02000023 RID: 35
	internal struct DistanceRay3Segment3
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00006C2C File Offset: 0x00004E2C
		public DistanceRay3Segment3(Ray3 ray, Segment3 segment)
		{
			this = default(DistanceRay3Segment3);
			Vector3 vector = ray.Origin - segment.Origin;
			double num = -ray.Direction.Dot(segment.Direction);
			double num2 = vector.Dot(ray.Direction);
			double num3 = -vector.Dot(segment.Direction);
			double squaredLength = vector.SquaredLength;
			double num4 = 1.0;
			double num5 = num;
			double num6 = Math.Abs(num4 - num5 * num5);
			double num7;
			double num8;
			double value;
			if (num6 >= 1E-08)
			{
				num7 = num * num3 - num2;
				num8 = num * num2 - num3;
				double num9 = segment.Extent * num6;
				if (num7 >= 0.0)
				{
					if (num8 >= -num9)
					{
						if (num8 <= num9)
						{
							double num10 = 1.0 / num6;
							num7 *= num10;
							num8 *= num10;
							double num11 = num7;
							value = num11 * (num11 + num * num8 + 2.0 * num2) + num8 * (num * num7 + num8 + 2.0 * num3) + squaredLength;
						}
						else
						{
							num8 = segment.Extent;
							num7 = -(num * num8 + num2);
							if (num7 > 0.0)
							{
								double num12 = -num7 * num7;
								double num13 = num8;
								value = num12 + num13 * (num13 + 2.0 * num3) + squaredLength;
							}
							else
							{
								num7 = 0.0;
								double num14 = num8;
								value = num14 * (num14 + 2.0 * num3) + squaredLength;
							}
						}
					}
					else
					{
						num8 = -segment.Extent;
						num7 = -(num * num8 + num2);
						if (num7 > 0.0)
						{
							double num15 = -num7 * num7;
							double num16 = num8;
							value = num15 + num16 * (num16 + 2.0 * num3) + squaredLength;
						}
						else
						{
							num7 = 0.0;
							double num17 = num8;
							value = num17 * (num17 + 2.0 * num3) + squaredLength;
						}
					}
				}
				else if (num8 <= -num9)
				{
					num7 = -(-num * segment.Extent + num2);
					if (num7 > 0.0)
					{
						num8 = -segment.Extent;
						double num18 = -num7 * num7;
						double num19 = num8;
						value = num18 + num19 * (num19 + 2.0 * num3) + squaredLength;
					}
					else
					{
						num7 = 0.0;
						num8 = -num3;
						if (num8 < -segment.Extent)
						{
							num8 = -segment.Extent;
						}
						else if (num8 > segment.Extent)
						{
							num8 = segment.Extent;
						}
						double num20 = num8;
						value = num20 * (num20 + 2.0 * num3) + squaredLength;
					}
				}
				else if (num8 <= num9)
				{
					num7 = 0.0;
					num8 = -num3;
					if (num8 < -segment.Extent)
					{
						num8 = -segment.Extent;
					}
					else if (num8 > segment.Extent)
					{
						num8 = segment.Extent;
					}
					double num21 = num8;
					value = num21 * (num21 + 2.0 * num3) + squaredLength;
				}
				else
				{
					num7 = -(num * segment.Extent + num2);
					if (num7 > 0.0)
					{
						num8 = segment.Extent;
						double num22 = -num7 * num7;
						double num23 = num8;
						value = num22 + num23 * (num23 + 2.0 * num3) + squaredLength;
					}
					else
					{
						num7 = 0.0;
						num8 = -num3;
						if (num8 < -segment.Extent)
						{
							num8 = -segment.Extent;
						}
						else if (num8 > segment.Extent)
						{
							num8 = segment.Extent;
						}
						double num24 = num8;
						value = num24 * (num24 + 2.0 * num3) + squaredLength;
					}
				}
			}
			else
			{
				if (num > 0.0)
				{
					num8 = -segment.Extent;
				}
				else
				{
					num8 = segment.Extent;
				}
				num7 = -(num * num8 + num2);
				if (num7 > 0.0)
				{
					double num25 = -num7 * num7;
					double num26 = num8;
					value = num25 + num26 * (num26 + 2.0 * num3) + squaredLength;
				}
				else
				{
					num7 = 0.0;
					double num27 = num8;
					value = num27 * (num27 + 2.0 * num3) + squaredLength;
				}
			}
			this.ClosestPointOnRay = ray.Origin + num7 * ray.Direction;
			this.ClosestPointOnSegment = segment.Origin + num8 * segment.Direction;
			this.RayParameter = num7;
			this.SegmentParameter = num8;
			this.SquaredDistance = Math.Abs(value);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000707A File Offset: 0x0000527A
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00007082 File Offset: 0x00005282
		public double SquaredDistance { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000708B File Offset: 0x0000528B
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00007098 File Offset: 0x00005298
		// (set) Token: 0x06000158 RID: 344 RVA: 0x000070A0 File Offset: 0x000052A0
		public double RayParameter { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000070A9 File Offset: 0x000052A9
		// (set) Token: 0x0600015A RID: 346 RVA: 0x000070B1 File Offset: 0x000052B1
		public double SegmentParameter { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000070BA File Offset: 0x000052BA
		// (set) Token: 0x0600015C RID: 348 RVA: 0x000070C2 File Offset: 0x000052C2
		public Vector3 ClosestPointOnRay { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000070CB File Offset: 0x000052CB
		// (set) Token: 0x0600015E RID: 350 RVA: 0x000070D3 File Offset: 0x000052D3
		public Vector3 ClosestPointOnSegment { get; private set; }
	}
}
