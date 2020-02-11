using System;

namespace Hao.Geometry
{
	// Token: 0x02000012 RID: 18
	internal struct DistanceVector2Triangle2
	{
		// Token: 0x0600006C RID: 108 RVA: 0x0000342C File Offset: 0x0000162C
		public DistanceVector2Triangle2(Vector2 vector, Triangle2 triangle)
		{
			this = default(DistanceVector2Triangle2);
			DistanceVector3Triangle3 distanceVector3Triangle = new DistanceVector3Triangle3(new Vector3(vector.X, vector.Y, 0.0), new Triangle3(new Vector3(triangle.V0.X, triangle.V0.Y, 0.0), new Vector3(triangle.V1.X, triangle.V1.Y, 0.0), new Vector3(triangle.V2.X, triangle.V2.Y, 0.0)));
			this.SquaredDistance = distanceVector3Triangle.SquaredDistance;
			this.ClosestPointOnVector = vector;
			this.ClosestPointOnTriangle = new Vector2(distanceVector3Triangle.ClosestPointOnTriangle.X, distanceVector3Triangle.ClosestPointOnTriangle.Y);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003529 File Offset: 0x00001729
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003531 File Offset: 0x00001731
		public double SquaredDistance { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000353A File Offset: 0x0000173A
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003547 File Offset: 0x00001747
		// (set) Token: 0x06000071 RID: 113 RVA: 0x0000354F File Offset: 0x0000174F
		public Vector2 ClosestPointOnVector { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003558 File Offset: 0x00001758
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00003560 File Offset: 0x00001760
		public Vector2 ClosestPointOnTriangle { get; private set; }
	}
}
