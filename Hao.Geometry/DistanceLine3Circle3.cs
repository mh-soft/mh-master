using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x02000018 RID: 24
	internal struct DistanceLine3Circle3
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00004C18 File Offset: 0x00002E18
		public DistanceLine3Circle3(Line3 line, Circle3 circle)
		{
			this = default(DistanceLine3Circle3);
			IEnumerable<double> polynomialRoots = DistanceLine3Circle3.GetPolynomialRoots(line, circle);
			double num = double.MaxValue;
			foreach (double num2 in polynomialRoots)
			{
				Vector3 vector = line.Origin + num2 * line.Direction;
				DistanceVector3Circle3 distanceVector3Circle = new DistanceVector3Circle3(vector, circle);
				double squaredDistance = distanceVector3Circle.SquaredDistance;
				if (squaredDistance < num)
				{
					num = squaredDistance;
					this.ClosestPointOnLine = distanceVector3Circle.ClosestPointOnVector;
					this.ClosestPointOnCircle = distanceVector3Circle.ClosestPointOnCircle;
					this.LineParameter = num2;
				}
			}
			this.SquaredDistance = num;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004CCC File Offset: 0x00002ECC
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00004CD4 File Offset: 0x00002ED4
		public double LineParameter { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004CDD File Offset: 0x00002EDD
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00004CE5 File Offset: 0x00002EE5
		public double SquaredDistance { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004CEE File Offset: 0x00002EEE
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004CFB File Offset: 0x00002EFB
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00004D03 File Offset: 0x00002F03
		public Vector3 ClosestPointOnLine { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004D0C File Offset: 0x00002F0C
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00004D14 File Offset: 0x00002F14
		public Vector3 ClosestPointOnCircle { get; private set; }

		// Token: 0x060000C0 RID: 192 RVA: 0x00004D20 File Offset: 0x00002F20
		internal static IEnumerable<double> GetPolynomialRoots(Line3 line, Circle3 circle)
		{
			Vector3 vector = circle.Center - line.Origin;
			double num = line.Direction.Dot(vector);
			Vector3 origin = line.Origin + num * line.Direction;
			line = new Line3(origin, line.Direction);
			Vector3 vector2 = line.Origin - circle.Center;
			double squaredLength = vector2.SquaredLength;
			double num2 = vector2.Dot(line.Direction);
			double num3 = circle.Normal.Dot(line.Direction);
			double num4 = vector2.Dot(circle.Normal);
			double num5 = num2;
			double num6 = num2 - num3 * num4;
			double num7 = 1.0;
			double num8 = num3;
			double num9 = num7 - num8 * num8;
			double num10 = num4;
			double num11 = squaredLength - num10 * num10;
			double num12 = num6;
			double num13 = num9;
			double num14 = circle.Radius * circle.Radius;
			double num15 = num5;
			double num16 = num15 * num15;
			double num17 = 2.0 * num5 * 1.0;
			double num18 = num6;
			double num19 = num18 * num18;
			double num20 = num9;
			double num21 = num20 * num20;
			double num22 = 2.0 * num6 * num9;
			double num23 = 2.0 * num12;
			double[] coefficients = new double[]
			{
				num16 * num11 - num19 * num14,
				num17 * num11 + num16 * num23 - num22 * num14,
				1.0 * num11 + num17 * num23 + num16 * num13 - num21 * num14,
				1.0 * num23 + num17 * num13,
				1.0 * num13
			};
			PolynomialRoots polynomialRoots = new PolynomialRoots(1E-05);
			polynomialRoots.FindB(new Polynomial1(coefficients), 6);
			List<double> list = new List<double>();
			for (int i = 0; i < polynomialRoots.Count; i++)
			{
				list.Add(polynomialRoots.GetRoot(i) + num);
			}
			return list;
		}
	}
}
