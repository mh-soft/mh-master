using System;

namespace Hao.Geometry
{
	// Token: 0x02000019 RID: 25
	internal struct DistanceLine3Line3
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00004F18 File Offset: 0x00003118
		public DistanceLine3Line3(Line3 line0, Line3 line1)
		{
			this = default(DistanceLine3Line3);
			Vector3 vector = line0.Origin - line1.Origin;
			double num = -line0.Direction.Dot(line1.Direction);
			double num2 = vector.Dot(line0.Direction);
			double squaredLength = vector.SquaredLength;
			double num3 = 1.0;
			double num4 = num;
			double num5 = Math.Abs(num3 - num4 * num4);
			double num8;
			double num9;
			double value;
			if (num5 >= 1E-08)
			{
				double num6 = -vector.Dot(line1.Direction);
				double num7 = 1.0 / num5;
				num8 = (num * num6 - num2) * num7;
				num9 = (num * num2 - num6) * num7;
				double num10 = num8;
				value = num10 * (num10 + num * num9 + 2.0 * num2) + num9 * (num * num8 + num9 + 2.0 * num6) + squaredLength;
			}
			else
			{
				num8 = -num2;
				num9 = 0.0;
				value = num2 * num8 + squaredLength;
			}
			this.ClosestPoint0 = line0.Origin + num8 * line0.Direction;
			this.ClosestPoint1 = line1.Origin + num9 * line1.Direction;
			this.Line0Parameter = num8;
			this.Line1Parameter = num9;
			this.SquaredDistance = Math.Abs(value);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000506D File Offset: 0x0000326D
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00005075 File Offset: 0x00003275
		public double Line0Parameter { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000507E File Offset: 0x0000327E
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00005086 File Offset: 0x00003286
		public double Line1Parameter { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000508F File Offset: 0x0000328F
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00005097 File Offset: 0x00003297
		public Vector3 ClosestPoint0 { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000050A0 File Offset: 0x000032A0
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x000050A8 File Offset: 0x000032A8
		public Vector3 ClosestPoint1 { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000050B1 File Offset: 0x000032B1
		// (set) Token: 0x060000CB RID: 203 RVA: 0x000050B9 File Offset: 0x000032B9
		public double SquaredDistance { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000050C2 File Offset: 0x000032C2
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}
	}
}
