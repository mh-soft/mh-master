using System;

namespace Hao.Geometry
{
	// Token: 0x0200001C RID: 28
	internal struct DistanceLine3Segment3
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x000056A4 File Offset: 0x000038A4
		public DistanceLine3Segment3(Line3 line, Segment3 segment)
		{
			this = default(DistanceLine3Segment3);
			Vector3 vector = line.Origin - segment.Origin;
			double num = -line.Direction.Dot(segment.Direction);
			double num2 = vector.Dot(line.Direction);
			double squaredLength = vector.SquaredLength;
			double num3 = 1.0;
			double num4 = num;
			double num5 = Math.Abs(num3 - num4 * num4);
			double num7;
			double num10;
			double value;
			if (num5 >= 1E-08)
			{
				double num6 = -vector.Dot(segment.Direction);
				num7 = num * num2 - num6;
				double num8 = segment.Extent * num5;
				if (num7 >= -num8)
				{
					if (num7 <= num8)
					{
						double num9 = 1.0 / num5;
						num10 = (num * num6 - num2) * num9;
						num7 *= num9;
						double num11 = num10;
						value = num11 * (num11 + num * num7 + 2.0 * num2) + num7 * (num * num10 + num7 + 2.0 * num6) + squaredLength;
					}
					else
					{
						num7 = segment.Extent;
						num10 = -(num * num7 + num2);
						double num12 = -num10 * num10;
						double num13 = num7;
						value = num12 + num13 * (num13 + 2.0 * num6) + squaredLength;
					}
				}
				else
				{
					num7 = -segment.Extent;
					num10 = -(num * num7 + num2);
					double num14 = -num10 * num10;
					double num15 = num7;
					value = num14 + num15 * (num15 + 2.0 * num6) + squaredLength;
				}
			}
			else
			{
				num7 = 0.0;
				num10 = -num2;
				value = num2 * num10 + squaredLength;
			}
			this.ClosestPointOnLine = line.Origin + num10 * line.Direction;
			this.ClosestPointOnSegment = segment.Origin + num7 * segment.Direction;
			this.LineParameter = num10;
			this.SegmentParameter = num7;
			this.SquaredDistance = Math.Abs(value);
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000587D File Offset: 0x00003A7D
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00005885 File Offset: 0x00003A85
		public double LineParameter { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000588E File Offset: 0x00003A8E
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00005896 File Offset: 0x00003A96
		public double SegmentParameter { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000589F File Offset: 0x00003A9F
		// (set) Token: 0x060000ED RID: 237 RVA: 0x000058A7 File Offset: 0x00003AA7
		public Vector3 ClosestPointOnLine { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000058B0 File Offset: 0x00003AB0
		// (set) Token: 0x060000EF RID: 239 RVA: 0x000058B8 File Offset: 0x00003AB8
		public Vector3 ClosestPointOnSegment { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000058C1 File Offset: 0x00003AC1
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x000058C9 File Offset: 0x00003AC9
		public double SquaredDistance { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000058D2 File Offset: 0x00003AD2
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}
	}
}
