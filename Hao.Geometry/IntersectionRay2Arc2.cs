using System;

namespace Hao.Geometry
{
	// Token: 0x0200003F RID: 63
	internal struct IntersectionRay2Arc2
	{
		// Token: 0x06000280 RID: 640 RVA: 0x0000AA19 File Offset: 0x00008C19
		public IntersectionRay2Arc2(Ray2 ray, Arc2 arc)
		{
			this = default(IntersectionRay2Arc2);
			this.ray = ray;
			this.arc = arc;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000AA30 File Offset: 0x00008C30
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000AA38 File Offset: 0x00008C38
		public int Quantity { get; private set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000AA41 File Offset: 0x00008C41
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000AA49 File Offset: 0x00008C49
		public Vector2 Point0 { get; private set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000AA52 File Offset: 0x00008C52
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000AA5A File Offset: 0x00008C5A
		public Vector2 Point1 { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000AA63 File Offset: 0x00008C63
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000AA6B File Offset: 0x00008C6B
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x06000289 RID: 649 RVA: 0x0000AA74 File Offset: 0x00008C74
		public bool Find()
		{
			this.Quantity = 0;
			IntersectionLine2Arc2 intersectionLine2Arc = new IntersectionLine2Arc2(new Line2(this.ray.Origin, this.ray.Direction), this.arc);
			if (intersectionLine2Arc.Find())
			{
				if (intersectionLine2Arc.Quantity == 2)
				{
					if (intersectionLine2Arc.Parameter0 >= 0.0 && intersectionLine2Arc.Parameter1 >= 0.0)
					{
						this.Point0 = intersectionLine2Arc.Point0;
						this.Point1 = intersectionLine2Arc.Point1;
						this.Quantity = 2;
					}
					else if (intersectionLine2Arc.Parameter0 < 0.0 && intersectionLine2Arc.Parameter1 >= 0.0)
					{
						this.Point0 = intersectionLine2Arc.Point1;
						this.Quantity = 1;
					}
				}
				else if (intersectionLine2Arc.Parameter0 >= 0.0)
				{
					this.Point0 = intersectionLine2Arc.Point0;
					this.Quantity = 1;
				}
			}
			this.IntersectionType = ((this.Quantity > 0) ? Intersection.Type.IT_POINT : Intersection.Type.IT_EMPTY);
			return this.IntersectionType > Intersection.Type.IT_EMPTY;
		}

		// Token: 0x0400008E RID: 142
		private readonly Ray2 ray;

		// Token: 0x0400008F RID: 143
		private readonly Arc2 arc;
	}
}
