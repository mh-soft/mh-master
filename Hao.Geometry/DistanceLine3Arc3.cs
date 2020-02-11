using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x02000014 RID: 20
	internal struct DistanceLine3Arc3
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003660 File Offset: 0x00001860
		public DistanceLine3Arc3(Line3 line, Arc3 arc)
		{
			this = default(DistanceLine3Arc3);
			Circle3 circle = arc.Circle;
			IEnumerable<double> polynomialRoots = DistanceLine3Circle3.GetPolynomialRoots(line, circle);
			double num = double.MaxValue;
			foreach (double num2 in polynomialRoots)
			{
				Vector3 vector = line.Origin + num2 * line.Direction;
				DistanceVector3Arc3 distanceVector3Arc = new DistanceVector3Arc3(vector, arc);
				double squaredDistance = distanceVector3Arc.SquaredDistance;
				if (squaredDistance < num)
				{
					num = squaredDistance;
					this.ClosestPointOnLine = distanceVector3Arc.ClosestPointOnVector;
					this.ClosestPointOnArc = distanceVector3Arc.ClosestPointOnArc;
					this.LineParameter = num2;
				}
			}
			this.SquaredDistance = num;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003720 File Offset: 0x00001920
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003728 File Offset: 0x00001928
		public double LineParameter { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003731 File Offset: 0x00001931
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003739 File Offset: 0x00001939
		public double SquaredDistance { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003742 File Offset: 0x00001942
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000374F File Offset: 0x0000194F
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003757 File Offset: 0x00001957
		public Vector3 ClosestPointOnLine { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003760 File Offset: 0x00001960
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003768 File Offset: 0x00001968
		public Vector3 ClosestPointOnArc { get; private set; }
	}
}
