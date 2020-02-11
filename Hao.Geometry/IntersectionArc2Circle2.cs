using System;

namespace Hao.Geometry
{
	// Token: 0x02000044 RID: 68
	internal struct IntersectionArc2Circle2
	{
		// Token: 0x060002B9 RID: 697 RVA: 0x0000B37E File Offset: 0x0000957E
		public IntersectionArc2Circle2(Arc2 arc, Circle2 circle)
		{
			this = default(IntersectionArc2Circle2);
			this.arc = arc;
			this.circle = circle;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000B395 File Offset: 0x00009595
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000B39D File Offset: 0x0000959D
		public int Quantity { get; private set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000B3A6 File Offset: 0x000095A6
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000B3AE File Offset: 0x000095AE
		public Vector2 Point0 { get; private set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000B3B7 File Offset: 0x000095B7
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000B3BF File Offset: 0x000095BF
		public Vector2 Point1 { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000B3C8 File Offset: 0x000095C8
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000B3D0 File Offset: 0x000095D0
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x060002C2 RID: 706 RVA: 0x0000B3DC File Offset: 0x000095DC
		public bool Find()
		{
			this.Quantity = 0;
			IntersectionCircle2Circle2 intersectionCircle2Circle = new IntersectionCircle2Circle2(this.circle, this.arc.Circle);
			if (!intersectionCircle2Circle.Find())
			{
				this.IntersectionType = Intersection.Type.IT_EMPTY;
				return false;
			}
			if (intersectionCircle2Circle.IntersectionType == Intersection.Type.IT_OTHER)
			{
				this.IntersectionType = Intersection.Type.IT_OTHER;
				return true;
			}
			Vector2[] array = new Vector2[]
			{
				intersectionCircle2Circle.Point0,
				intersectionCircle2Circle.Point1
			};
			for (int i = 0; i < intersectionCircle2Circle.Quantity; i++)
			{
				if (IntersectionArc2Arc2.Contains(this.arc, array[i]))
				{
					int quantity = this.Quantity;
					this.Quantity = quantity + 1;
					if (quantity == 0)
					{
						this.Point0 = array[i];
					}
					else
					{
						this.Point1 = array[i];
					}
				}
			}
			this.IntersectionType = ((this.Quantity > 0) ? Intersection.Type.IT_POINT : Intersection.Type.IT_EMPTY);
			return this.IntersectionType > Intersection.Type.IT_EMPTY;
		}

		// Token: 0x040000AF RID: 175
		private readonly Arc2 arc;

		// Token: 0x040000B0 RID: 176
		private readonly Circle2 circle;
	}
}
