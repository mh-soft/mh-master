using System;

namespace Hao.Geometry
{
	// Token: 0x02000045 RID: 69
	internal struct IntersectionArc2Arc2
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x0000B4C7 File Offset: 0x000096C7
		public IntersectionArc2Arc2(Arc2 arc0, Arc2 arc1)
		{
			this = default(IntersectionArc2Arc2);
			this.arc0 = arc0;
			this.arc1 = arc1;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000B4DE File Offset: 0x000096DE
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000B4E6 File Offset: 0x000096E6
		public int Quantity { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000B4EF File Offset: 0x000096EF
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0000B4F7 File Offset: 0x000096F7
		public Vector2 Point0 { get; private set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000B500 File Offset: 0x00009700
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0000B508 File Offset: 0x00009708
		public Vector2 Point1 { get; private set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000B511 File Offset: 0x00009711
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0000B519 File Offset: 0x00009719
		public Arc2 IntersectionArc { get; private set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000B522 File Offset: 0x00009722
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000B52A File Offset: 0x0000972A
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x060002CE RID: 718 RVA: 0x0000B534 File Offset: 0x00009734
		public bool Find()
		{
			this.Quantity = 0;
			Circle2 circle = this.arc0.Circle;
			Circle2 circle2 = this.arc1.Circle;
			IntersectionCircle2Circle2 intersectionCircle2Circle = new IntersectionCircle2Circle2(circle, circle2);
			if (!intersectionCircle2Circle.Find())
			{
				this.IntersectionType = Intersection.Type.IT_EMPTY;
				return false;
			}
			if (intersectionCircle2Circle.IntersectionType != Intersection.Type.IT_OTHER)
			{
				Vector2[] array = new Vector2[]
				{
					intersectionCircle2Circle.Point0,
					intersectionCircle2Circle.Point1
				};
				for (int i = 0; i < intersectionCircle2Circle.Quantity; i++)
				{
					if (IntersectionArc2Arc2.Contains(this.arc0, array[i]) && IntersectionArc2Arc2.Contains(this.arc1, array[i]))
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
			if (IntersectionArc2Arc2.Contains(this.arc1, this.arc0.StartPoint))
			{
				if (IntersectionArc2Arc2.Contains(this.arc1, this.arc0.EndPoint))
				{
					this.IntersectionType = Intersection.Type.IT_OTHER;
					this.IntersectionArc = this.arc0;
				}
				else if (!this.arc0.StartPoint.Equals(this.arc1.EndPoint))
				{
					this.IntersectionType = Intersection.Type.IT_OTHER;
					this.IntersectionArc = new Arc2(this.arc0.Circle, this.arc0.StartAngle, this.arc1.StartAngle + this.arc1.DeltaAngle - this.arc0.StartAngle);
				}
				else
				{
					this.IntersectionType = Intersection.Type.IT_POINT;
					this.Quantity = 1;
					this.Point0 = this.arc0.StartPoint;
				}
				return true;
			}
			if (IntersectionArc2Arc2.Contains(this.arc1, this.arc0.EndPoint))
			{
				if (!this.arc0.EndPoint.Equals(this.arc1.StartPoint))
				{
					this.IntersectionType = Intersection.Type.IT_OTHER;
					this.IntersectionArc = new Arc2(this.arc0.Circle, this.arc1.StartAngle, this.arc0.StartAngle + this.arc0.DeltaAngle - this.arc1.StartAngle);
				}
				else
				{
					this.IntersectionType = Intersection.Type.IT_POINT;
					this.Quantity = 1;
					this.Point0 = this.arc1.StartPoint;
				}
				return true;
			}
			if (IntersectionArc2Arc2.Contains(this.arc0, this.arc1.StartPoint))
			{
				this.IntersectionType = Intersection.Type.IT_OTHER;
				this.IntersectionArc = this.arc1;
				return true;
			}
			this.IntersectionType = Intersection.Type.IT_EMPTY;
			return false;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000B860 File Offset: 0x00009A60
		internal static bool Contains(Arc2 arc, Vector2 point)
		{
			Angle pointDirectionAngle = arc.Circle.GetPointDirectionAngle(point);
			return arc.Contains(pointDirectionAngle);
		}

		// Token: 0x040000B5 RID: 181
		private readonly Arc2 arc0;

		// Token: 0x040000B6 RID: 182
		private readonly Arc2 arc1;
	}
}
