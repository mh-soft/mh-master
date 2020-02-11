using System;

namespace Hao.Geometry
{
	// Token: 0x0200001A RID: 26
	internal struct DistanceLine3Ray3
	{
		// Token: 0x060000CD RID: 205 RVA: 0x000050D0 File Offset: 0x000032D0
		public DistanceLine3Ray3(Line3 line, Ray3 ray)
		{
			this = default(DistanceLine3Ray3);
			Vector3 vector = line.Origin - ray.Origin;
			double num = -line.Direction.Dot(ray.Direction);
			double num2 = vector.Dot(line.Direction);
			double squaredLength = vector.SquaredLength;
			double num3 = 1.0;
			double num4 = num;
			double num5 = Math.Abs(num3 - num4 * num4);
			double num7;
			double num9;
			double value;
			if (num5 >= 1E-08)
			{
				double num6 = -vector.Dot(ray.Direction);
				num7 = num * num2 - num6;
				if (num7 >= 0.0)
				{
					double num8 = 1.0 / num5;
					num9 = (num * num6 - num2) * num8;
					num7 *= num8;
					double num10 = num9;
					value = num10 * (num10 + num * num7 + 2.0 * num2) + num7 * (num * num9 + num7 + 2.0 * num6) + squaredLength;
				}
				else
				{
					num9 = -num2;
					num7 = 0.0;
					value = num2 * num9 + squaredLength;
				}
			}
			else
			{
				num9 = -num2;
				num7 = 0.0;
				value = num2 * num9 + squaredLength;
			}
			this.ClosestPointOnLine = line.Origin + num9 * line.Direction;
			this.ClosestPointOnRay = ray.Origin + num7 * ray.Direction;
			this.LineParameter = num9;
			this.RayParameter = num7;
			this.SquaredDistance = Math.Abs(value);
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00005252 File Offset: 0x00003452
		// (set) Token: 0x060000CF RID: 207 RVA: 0x0000525A File Offset: 0x0000345A
		public double SquaredDistance { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005263 File Offset: 0x00003463
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00005270 File Offset: 0x00003470
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00005278 File Offset: 0x00003478
		public double LineParameter { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005281 File Offset: 0x00003481
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00005289 File Offset: 0x00003489
		public double RayParameter { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005292 File Offset: 0x00003492
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000529A File Offset: 0x0000349A
		public Vector3 ClosestPointOnLine { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000052A3 File Offset: 0x000034A3
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x000052AB File Offset: 0x000034AB
		public Vector3 ClosestPointOnRay { get; private set; }
	}
}
