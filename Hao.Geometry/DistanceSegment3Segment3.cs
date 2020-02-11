using System;

namespace Hao.Geometry
{
	// Token: 0x0200002A RID: 42
	internal struct DistanceSegment3Segment3
	{
		// Token: 0x060001BE RID: 446 RVA: 0x00007E98 File Offset: 0x00006098
		public DistanceSegment3Segment3(Segment3 segment0, Segment3 segment1)
		{
			this = default(DistanceSegment3Segment3);
			Vector3 vector = segment0.Origin - segment1.Origin;
			double num = -segment0.Direction.Dot(segment1.Direction);
			double num2 = vector.Dot(segment0.Direction);
			double num3 = -vector.Dot(segment1.Direction);
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
				double num9 = segment0.Extent * num6;
				double num10 = segment1.Extent * num6;
				if (num7 >= -num9)
				{
					if (num7 <= num9)
					{
						if (num8 >= -num10)
						{
							if (num8 <= num10)
							{
								double num11 = 1.0 / num6;
								num7 *= num11;
								num8 *= num11;
								double num12 = num7;
								double num13 = num12 * (num12 + num * num8 + 2.0 * num2);
								double num14 = num8;
								value = num13 + num14 * (num14 + num * num7 + 2.0 * num3) + squaredLength;
							}
							else
							{
								num8 = segment1.Extent;
								double num15 = -(num * num8 + num2);
								if (num15 < -segment0.Extent)
								{
									num7 = -segment0.Extent;
									double num16 = num7;
									double num17 = num16 * (num16 - 2.0 * num15);
									double num18 = num8;
									value = num17 + num18 * (num18 + 2.0 * num3) + squaredLength;
								}
								else if (num15 <= segment0.Extent)
								{
									num7 = num15;
									double num19 = -num7 * num7;
									double num20 = num8;
									value = num19 + num20 * (num20 + 2.0 * num3) + squaredLength;
								}
								else
								{
									num7 = segment0.Extent;
									double num21 = num7;
									double num22 = num21 * (num21 - 2.0 * num15);
									double num23 = num8;
									value = num22 + num23 * (num23 + 2.0 * num3) + squaredLength;
								}
							}
						}
						else
						{
							num8 = -segment1.Extent;
							double num15 = -(num * num8 + num2);
							if (num15 < -segment0.Extent)
							{
								num7 = -segment0.Extent;
								double num24 = num7;
								double num25 = num24 * (num24 - 2.0 * num15);
								double num26 = num8;
								value = num25 + num26 * (num26 + 2.0 * num3) + squaredLength;
							}
							else if (num15 <= segment0.Extent)
							{
								num7 = num15;
								double num27 = -num7 * num7;
								double num28 = num8;
								value = num27 + num28 * (num28 + 2.0 * num3) + squaredLength;
							}
							else
							{
								num7 = segment0.Extent;
								double num29 = num7;
								double num30 = num29 * (num29 - 2.0 * num15);
								double num31 = num8;
								value = num30 + num31 * (num31 + 2.0 * num3) + squaredLength;
							}
						}
					}
					else if (num8 >= -num10)
					{
						if (num8 <= num10)
						{
							num7 = segment0.Extent;
							double num32 = -(num * num7 + num3);
							if (num32 < -segment1.Extent)
							{
								num8 = -segment1.Extent;
								double num33 = num8;
								double num34 = num33 * (num33 - 2.0 * num32);
								double num35 = num7;
								value = num34 + num35 * (num35 + 2.0 * num2) + squaredLength;
							}
							else if (num32 <= segment1.Extent)
							{
								num8 = num32;
								double num36 = -num8 * num8;
								double num37 = num7;
								value = num36 + num37 * (num37 + 2.0 * num2) + squaredLength;
							}
							else
							{
								num8 = segment1.Extent;
								double num38 = num8;
								double num39 = num38 * (num38 - 2.0 * num32);
								double num40 = num7;
								value = num39 + num40 * (num40 + 2.0 * num2) + squaredLength;
							}
						}
						else
						{
							num8 = segment1.Extent;
							double num15 = -(num * num8 + num2);
							if (num15 < -segment0.Extent)
							{
								num7 = -segment0.Extent;
								double num41 = num7;
								double num42 = num41 * (num41 - 2.0 * num15);
								double num43 = num8;
								value = num42 + num43 * (num43 + 2.0 * num3) + squaredLength;
							}
							else if (num15 <= segment0.Extent)
							{
								num7 = num15;
								double num44 = -num7 * num7;
								double num45 = num8;
								value = num44 + num45 * (num45 + 2.0 * num3) + squaredLength;
							}
							else
							{
								num7 = segment0.Extent;
								double num32 = -(num * num7 + num3);
								if (num32 < -segment1.Extent)
								{
									num8 = -segment1.Extent;
									double num46 = num8;
									double num47 = num46 * (num46 - 2.0 * num32);
									double num48 = num7;
									value = num47 + num48 * (num48 + 2.0 * num2) + squaredLength;
								}
								else if (num32 <= segment1.Extent)
								{
									num8 = num32;
									double num49 = -num8 * num8;
									double num50 = num7;
									value = num49 + num50 * (num50 + 2.0 * num2) + squaredLength;
								}
								else
								{
									num8 = segment1.Extent;
									double num51 = num8;
									double num52 = num51 * (num51 - 2.0 * num32);
									double num53 = num7;
									value = num52 + num53 * (num53 + 2.0 * num2) + squaredLength;
								}
							}
						}
					}
					else
					{
						num8 = -segment1.Extent;
						double num15 = -(num * num8 + num2);
						if (num15 < -segment0.Extent)
						{
							num7 = -segment0.Extent;
							double num54 = num7;
							double num55 = num54 * (num54 - 2.0 * num15);
							double num56 = num8;
							value = num55 + num56 * (num56 + 2.0 * num3) + squaredLength;
						}
						else if (num15 <= segment0.Extent)
						{
							num7 = num15;
							double num57 = -num7 * num7;
							double num58 = num8;
							value = num57 + num58 * (num58 + 2.0 * num3) + squaredLength;
						}
						else
						{
							num7 = segment0.Extent;
							double num32 = -(num * num7 + num3);
							if (num32 > segment1.Extent)
							{
								num8 = segment1.Extent;
								double num59 = num8;
								double num60 = num59 * (num59 - 2.0 * num32);
								double num61 = num7;
								value = num60 + num61 * (num61 + 2.0 * num2) + squaredLength;
							}
							else if (num32 >= -segment1.Extent)
							{
								num8 = num32;
								double num62 = -num8 * num8;
								double num63 = num7;
								value = num62 + num63 * (num63 + 2.0 * num2) + squaredLength;
							}
							else
							{
								num8 = -segment1.Extent;
								double num64 = num8;
								double num65 = num64 * (num64 - 2.0 * num32);
								double num66 = num7;
								value = num65 + num66 * (num66 + 2.0 * num2) + squaredLength;
							}
						}
					}
				}
				else if (num8 >= -num10)
				{
					if (num8 <= num10)
					{
						num7 = -segment0.Extent;
						double num32 = -(num * num7 + num3);
						if (num32 < -segment1.Extent)
						{
							num8 = -segment1.Extent;
							double num67 = num8;
							double num68 = num67 * (num67 - 2.0 * num32);
							double num69 = num7;
							value = num68 + num69 * (num69 + 2.0 * num2) + squaredLength;
						}
						else if (num32 <= segment1.Extent)
						{
							num8 = num32;
							double num70 = -num8 * num8;
							double num71 = num7;
							value = num70 + num71 * (num71 + 2.0 * num2) + squaredLength;
						}
						else
						{
							num8 = segment1.Extent;
							double num72 = num8;
							double num73 = num72 * (num72 - 2.0 * num32);
							double num74 = num7;
							value = num73 + num74 * (num74 + 2.0 * num2) + squaredLength;
						}
					}
					else
					{
						num8 = segment1.Extent;
						double num15 = -(num * num8 + num2);
						if (num15 > segment0.Extent)
						{
							num7 = segment0.Extent;
							double num75 = num7;
							double num76 = num75 * (num75 - 2.0 * num15);
							double num77 = num8;
							value = num76 + num77 * (num77 + 2.0 * num3) + squaredLength;
						}
						else if (num15 >= -segment0.Extent)
						{
							num7 = num15;
							double num78 = -num7 * num7;
							double num79 = num8;
							value = num78 + num79 * (num79 + 2.0 * num3) + squaredLength;
						}
						else
						{
							num7 = -segment0.Extent;
							double num32 = -(num * num7 + num3);
							if (num32 < -segment1.Extent)
							{
								num8 = -segment1.Extent;
								double num80 = num8;
								double num81 = num80 * (num80 - 2.0 * num32);
								double num82 = num7;
								value = num81 + num82 * (num82 + 2.0 * num2) + squaredLength;
							}
							else if (num32 <= segment1.Extent)
							{
								num8 = num32;
								double num83 = -num8 * num8;
								double num84 = num7;
								value = num83 + num84 * (num84 + 2.0 * num2) + squaredLength;
							}
							else
							{
								num8 = segment1.Extent;
								double num85 = num8;
								double num86 = num85 * (num85 - 2.0 * num32);
								double num87 = num7;
								value = num86 + num87 * (num87 + 2.0 * num2) + squaredLength;
							}
						}
					}
				}
				else
				{
					num8 = -segment1.Extent;
					double num15 = -(num * num8 + num2);
					if (num15 > segment0.Extent)
					{
						num7 = segment0.Extent;
						double num88 = num7;
						double num89 = num88 * (num88 - 2.0 * num15);
						double num90 = num8;
						value = num89 + num90 * (num90 + 2.0 * num3) + squaredLength;
					}
					else if (num15 >= -segment0.Extent)
					{
						num7 = num15;
						double num91 = -num7 * num7;
						double num92 = num8;
						value = num91 + num92 * (num92 + 2.0 * num3) + squaredLength;
					}
					else
					{
						num7 = -segment0.Extent;
						double num32 = -(num * num7 + num3);
						if (num32 < -segment1.Extent)
						{
							num8 = -segment1.Extent;
							double num93 = num8;
							double num94 = num93 * (num93 - 2.0 * num32);
							double num95 = num7;
							value = num94 + num95 * (num95 + 2.0 * num2) + squaredLength;
						}
						else if (num32 <= segment1.Extent)
						{
							num8 = num32;
							double num96 = -num8 * num8;
							double num97 = num7;
							value = num96 + num97 * (num97 + 2.0 * num2) + squaredLength;
						}
						else
						{
							num8 = segment1.Extent;
							double num98 = num8;
							double num99 = num98 * (num98 - 2.0 * num32);
							double num100 = num7;
							value = num99 + num100 * (num100 + 2.0 * num2) + squaredLength;
						}
					}
				}
			}
			else
			{
				double num101 = segment0.Extent + segment1.Extent;
				double num102 = (num > 0.0) ? -1.0 : 1.0;
				double num103 = 0.5 * (num2 - num102 * num3);
				double num104 = -num103;
				if (num104 < -num101)
				{
					num104 = -num101;
				}
				else if (num104 > num101)
				{
					num104 = num101;
				}
				num8 = -num102 * num104 * segment1.Extent / num101;
				num7 = num104 + num102 * num8;
				double num105 = num104;
				value = num105 * (num105 + 2.0 * num103) + squaredLength;
			}
			this.ClosestPoint0 = segment0.Origin + num7 * segment0.Direction;
			this.ClosestPoint1 = segment1.Origin + num8 * segment1.Direction;
			this.Segment0Parameter = num7;
			this.Segment1Parameter = num8;
			this.SquaredDistance = Math.Abs(value);
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000088B3 File Offset: 0x00006AB3
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x000088BB File Offset: 0x00006ABB
		public double SquaredDistance { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000088C4 File Offset: 0x00006AC4
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000088D1 File Offset: 0x00006AD1
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x000088D9 File Offset: 0x00006AD9
		public double Segment0Parameter { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000088E2 File Offset: 0x00006AE2
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x000088EA File Offset: 0x00006AEA
		public double Segment1Parameter { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x000088F3 File Offset: 0x00006AF3
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x000088FB File Offset: 0x00006AFB
		public Vector3 ClosestPoint0 { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00008904 File Offset: 0x00006B04
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x0000890C File Offset: 0x00006B0C
		public Vector3 ClosestPoint1 { get; private set; }
	}
}
