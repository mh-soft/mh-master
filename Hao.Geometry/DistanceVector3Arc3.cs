using System;

namespace Hao.Geometry
{
	// Token: 0x02000013 RID: 19
	internal struct DistanceVector3Arc3
	{
		// Token: 0x06000074 RID: 116 RVA: 0x0000356C File Offset: 0x0000176C
		public DistanceVector3Arc3(Vector3 vector, Arc3 arc)
		{
			this = default(DistanceVector3Arc3);
			DistanceVector3Circle3 distanceVector3Circle = new DistanceVector3Circle3(vector, arc.Circle);
			this.ClosestPointOnArc = distanceVector3Circle.ClosestPointOnCircle;
			if (!arc.Contains(arc.Circle.GetPointDirectionAngle(this.ClosestPointOnArc)))
			{
				double squaredLength = (vector - arc.StartPoint).SquaredLength;
				double squaredLength2 = (vector - arc.EndPoint).SquaredLength;
				this.ClosestPointOnArc = ((squaredLength2 < squaredLength) ? arc.EndPoint : arc.StartPoint);
			}
			this.SquaredDistance = (vector - this.ClosestPointOnArc).SquaredLength;
			this.ClosestPointOnVector = vector;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000361F File Offset: 0x0000181F
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003627 File Offset: 0x00001827
		public double SquaredDistance { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003630 File Offset: 0x00001830
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000363D File Offset: 0x0000183D
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003645 File Offset: 0x00001845
		public Vector3 ClosestPointOnVector { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000364E File Offset: 0x0000184E
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003656 File Offset: 0x00001856
		public Vector3 ClosestPointOnArc { get; private set; }
	}
}
