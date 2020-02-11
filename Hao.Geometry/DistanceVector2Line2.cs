using System;

namespace Hao.Geometry
{
	/// <summary>
	/// 进行计算
	/// </summary>
	internal struct DistanceVector2Line2
	{
		
		public DistanceVector2Line2(Vector2 vector, Line2 line)
		{
			this = default(DistanceVector2Line2);
			Vector2 vector2 = vector - line.Origin;
			this.LineParameter = line.Direction.Dot(vector2);
			this.ClosestPointOnVector = vector;
			this.ClosestPointOnLine = line.Origin + this.LineParameter * line.Direction;
			this.SquaredDistance = (this.ClosestPointOnLine - this.ClosestPointOnVector).SquaredLength;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000033DA File Offset: 0x000015DA
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000033E2 File Offset: 0x000015E2
		public double SquaredDistance { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000033EB File Offset: 0x000015EB
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000033F8 File Offset: 0x000015F8
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003400 File Offset: 0x00001600
		public double LineParameter { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003409 File Offset: 0x00001609
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003411 File Offset: 0x00001611
		public Vector2 ClosestPointOnVector { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000341A File Offset: 0x0000161A
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003422 File Offset: 0x00001622
		public Vector2 ClosestPointOnLine { get; private set; }
	}
}
