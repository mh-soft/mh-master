using System;

namespace Hao.Geometry
{
	// Token: 0x02000040 RID: 64
	internal struct IntersectionRay2Circle2
	{
		// Token: 0x0600028A RID: 650 RVA: 0x0000AB92 File Offset: 0x00008D92
		public IntersectionRay2Circle2(Ray2 ray, Circle2 circle)
		{
			this = default(IntersectionRay2Circle2);
			this.ray = ray;
			this.circle = circle;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000ABA9 File Offset: 0x00008DA9
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000ABB1 File Offset: 0x00008DB1
		public int Quantity { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000ABBA File Offset: 0x00008DBA
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000ABC2 File Offset: 0x00008DC2
		public Vector2 Point0 { get; private set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000ABCB File Offset: 0x00008DCB
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000ABD3 File Offset: 0x00008DD3
		public Vector2 Point1 { get; private set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000ABDC File Offset: 0x00008DDC
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000ABE4 File Offset: 0x00008DE4
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x06000293 RID: 659 RVA: 0x0000ABF0 File Offset: 0x00008DF0
		public bool Find()
		{
			this.Quantity = 0;
			IntersectionLine2Circle2 intersectionLine2Circle = new IntersectionLine2Circle2(new Line2(this.ray.Origin, this.ray.Direction), this.circle);
			if (intersectionLine2Circle.Find())
			{
				if (intersectionLine2Circle.Quantity == 2)
				{
					if (intersectionLine2Circle.Parameter0 >= 0.0 && intersectionLine2Circle.Parameter1 >= 0.0)
					{
						this.Point0 = intersectionLine2Circle.Point0;
						this.Point1 = intersectionLine2Circle.Point1;
						this.Quantity = 2;
					}
					else if (intersectionLine2Circle.Parameter0 < 0.0 && intersectionLine2Circle.Parameter1 >= 0.0)
					{
						this.Point0 = intersectionLine2Circle.Point1;
						this.Quantity = 1;
					}
				}
				else if (intersectionLine2Circle.Parameter0 >= 0.0)
				{
					this.Point0 = intersectionLine2Circle.Point0;
					this.Quantity = 1;
				}
			}
			this.IntersectionType = ((this.Quantity > 0) ? Intersection.Type.IT_POINT : Intersection.Type.IT_EMPTY);
			return this.IntersectionType > Intersection.Type.IT_EMPTY;
		}

		// Token: 0x04000094 RID: 148
		private readonly Ray2 ray;

		// Token: 0x04000095 RID: 149
		private readonly Circle2 circle;
	}
}
