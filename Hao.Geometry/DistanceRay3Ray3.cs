using System;

namespace Hao.Geometry
{
	// Token: 0x02000021 RID: 33
	internal struct DistanceRay3Ray3
	{
		// Token: 0x0600013D RID: 317 RVA: 0x000067B0 File Offset: 0x000049B0
		public DistanceRay3Ray3(Ray3 ray0, Ray3 ray1)
		{
			this = default(DistanceRay3Ray3);
			Vector3 vector = ray0.Origin - ray1.Origin;
			double num = -ray0.Direction.Dot(ray1.Direction);
			double num2 = vector.Dot(ray0.Direction);
			double squaredLength = vector.SquaredLength;
			double num3 = 1.0;
			double num4 = num;
			double num5 = Math.Abs(num3 - num4 * num4);
			double num7;
			double num8;
			double value;
			if (num5 >= 1E-08)
			{
				double num6 = -vector.Dot(ray1.Direction);
				num7 = num * num6 - num2;
				num8 = num * num2 - num6;
				if (num7 >= 0.0)
				{
					if (num8 >= 0.0)
					{
						double num9 = 1.0 / num5;
						num7 *= num9;
						num8 *= num9;
						double num10 = num7;
						double num11 = num10 * (num10 + num * num8 + 2.0 * num2);
						double num12 = num8;
						value = num11 + num12 * (num12 + num * num7 + 2.0 * num6) + squaredLength;
					}
					else
					{
						num8 = 0.0;
						if (num2 >= 0.0)
						{
							num7 = 0.0;
							value = squaredLength;
						}
						else
						{
							num7 = -num2;
							value = num2 * num7 + squaredLength;
						}
					}
				}
				else if (num8 >= 0.0)
				{
					num7 = 0.0;
					if (num6 >= 0.0)
					{
						num8 = 0.0;
						value = squaredLength;
					}
					else
					{
						num8 = -num6;
						value = num6 * num8 + squaredLength;
					}
				}
				else if (num2 < 0.0)
				{
					num7 = -num2;
					num8 = 0.0;
					value = num2 * num7 + squaredLength;
				}
				else
				{
					num7 = 0.0;
					if (num6 >= 0.0)
					{
						num8 = 0.0;
						value = squaredLength;
					}
					else
					{
						num8 = -num6;
						value = num6 * num8 + squaredLength;
					}
				}
			}
			else if (num > 0.0)
			{
				num8 = 0.0;
				if (num2 >= 0.0)
				{
					num7 = 0.0;
					value = squaredLength;
				}
				else
				{
					num7 = -num2;
					value = num2 * num7 + squaredLength;
				}
			}
			else if (num2 >= 0.0)
			{
				double num6 = -vector.Dot(ray1.Direction);
				num7 = 0.0;
				num8 = -num6;
				value = num6 * num8 + squaredLength;
			}
			else
			{
				num7 = -num2;
				num8 = 0.0;
				value = num2 * num7 + squaredLength;
			}
			this.ClosestPoint0 = ray0.Origin + num7 * ray0.Direction;
			this.ClosestPoint1 = ray1.Origin + num8 * ray1.Direction;
			this.SquaredDistance = Math.Abs(value);
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006A84 File Offset: 0x00004C84
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00006A8C File Offset: 0x00004C8C
		public double SquaredDistance { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006A95 File Offset: 0x00004C95
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00006AA2 File Offset: 0x00004CA2
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00006AAA File Offset: 0x00004CAA
		public Vector3 ClosestPoint0 { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00006AB3 File Offset: 0x00004CB3
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00006ABB File Offset: 0x00004CBB
		public Vector3 ClosestPoint1 { get; private set; }
	}
}
