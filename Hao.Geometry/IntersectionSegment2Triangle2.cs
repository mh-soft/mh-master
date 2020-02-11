using System;

namespace Hao.Geometry
{
	// Token: 0x02000067 RID: 103
	internal struct IntersectionSegment2Triangle2
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x00011F4B File Offset: 0x0001014B
		public IntersectionSegment2Triangle2(Segment2 segment, Triangle2 triangle)
		{
			this = default(IntersectionSegment2Triangle2);
			this.segment = segment;
			this.triangle = triangle;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00011F62 File Offset: 0x00010162
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x00011F6A File Offset: 0x0001016A
		public int Quantity { get; private set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x00011F73 File Offset: 0x00010173
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x00011F7B File Offset: 0x0001017B
		public Vector2 Point0 { get; private set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00011F84 File Offset: 0x00010184
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x00011F8C File Offset: 0x0001018C
		public Vector2 Point1 { get; private set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00011F95 File Offset: 0x00010195
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x00011F9D File Offset: 0x0001019D
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x06000407 RID: 1031 RVA: 0x00011FA8 File Offset: 0x000101A8
		public bool Test()
		{
			double[] dist = new double[3];
			int[] sign = new int[3];
			int num;
			int num2;
			int num3;
			IntersectionLine2Triangle2.TriangleLineRelations(this.segment.Origin, this.segment.Direction, this.triangle, ref dist, ref sign, out num, out num2, out num3);
			if (num == 3 || num2 == 3)
			{
				this.IntersectionType = Intersection.Type.IT_EMPTY;
			}
			else
			{
				double[] array = new double[2];
				IntersectionLine2Triangle2.GetInterval(this.segment.Origin, this.segment.Direction, this.triangle, dist, sign, ref array);
				Intersector1 intersector = new Intersector1(array[0], array[1], -this.segment.Extent, this.segment.Extent);
				intersector.Find();
				this.Quantity = intersector.Quantity;
				if (this.Quantity == 2)
				{
					this.IntersectionType = Intersection.Type.IT_SEGMENT;
				}
				else if (this.Quantity == 1)
				{
					this.IntersectionType = Intersection.Type.IT_POINT;
				}
				else
				{
					this.IntersectionType = Intersection.Type.IT_EMPTY;
				}
			}
			return this.IntersectionType > Intersection.Type.IT_EMPTY;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000120B8 File Offset: 0x000102B8
		public bool Find()
		{
			double[] dist = new double[3];
			int[] sign = new int[3];
			int num;
			int num2;
			int num3;
			IntersectionLine2Triangle2.TriangleLineRelations(this.segment.Origin, this.segment.Direction, this.triangle, ref dist, ref sign, out num, out num2, out num3);
			if (num == 3 || num2 == 3)
			{
				this.Quantity = 0;
				this.IntersectionType = Intersection.Type.IT_EMPTY;
			}
			else
			{
				double[] array = new double[2];
				IntersectionLine2Triangle2.GetInterval(this.segment.Origin, this.segment.Direction, this.triangle, dist, sign, ref array);
				Intersector1 intersector = new Intersector1(array[0], array[1], -this.segment.Extent, this.segment.Extent);
				intersector.Find();
				this.Quantity = intersector.Quantity;
				if (this.Quantity == 2)
				{
					this.IntersectionType = Intersection.Type.IT_SEGMENT;
					this.Point0 = this.segment.Origin + intersector.Overlap0 * this.segment.Direction;
					this.Point1 = this.segment.Origin + intersector.Overlap1 * this.segment.Direction;
				}
				else if (this.Quantity == 1)
				{
					this.IntersectionType = Intersection.Type.IT_POINT;
					this.Point0 = this.segment.Origin + intersector.Overlap0 * this.segment.Direction;
				}
				else
				{
					this.IntersectionType = Intersection.Type.IT_EMPTY;
				}
			}
			return this.IntersectionType > Intersection.Type.IT_EMPTY;
		}

		// Token: 0x04000124 RID: 292
		private readonly Segment2 segment;

		// Token: 0x04000125 RID: 293
		private readonly Triangle2 triangle;
	}
}
