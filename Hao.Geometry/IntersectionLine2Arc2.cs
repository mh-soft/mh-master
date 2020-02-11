using System;

namespace Hao.Geometry
{
	// Token: 0x02000046 RID: 70
	internal struct IntersectionLine2Arc2
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x0000B882 File Offset: 0x00009A82
		public IntersectionLine2Arc2(Line2 line, Arc2 arc)
		{
			this = default(IntersectionLine2Arc2);
			this.line = line;
			this.arc = arc;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000B899 File Offset: 0x00009A99
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000B8A1 File Offset: 0x00009AA1
		public int Quantity { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000B8AA File Offset: 0x00009AAA
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000B8B2 File Offset: 0x00009AB2
		public Vector2 Point0 { get; private set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000B8BB File Offset: 0x00009ABB
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000B8C3 File Offset: 0x00009AC3
		public Vector2 Point1 { get; private set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000B8CC File Offset: 0x00009ACC
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000B8D4 File Offset: 0x00009AD4
		public double Parameter0 { get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000B8DD File Offset: 0x00009ADD
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000B8E5 File Offset: 0x00009AE5
		public double Parameter1 { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000B8EE File Offset: 0x00009AEE
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000B8F6 File Offset: 0x00009AF6
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x060002DD RID: 733 RVA: 0x0000B900 File Offset: 0x00009B00
		public bool Find()
		{
			int num;
			double[] array;
			bool flag = IntersectionLine2Circle2.Find(this.line.Origin, this.line.Direction, this.arc.Circle.Center, this.arc.Circle.Radius, out num, out array);
			this.Quantity = 0;
			if (flag)
			{
				Vector2 vector = this.line.Origin + this.line.Direction * array[0];
				Vector2 vector2 = this.line.Origin + this.line.Direction * array[1];
				if (IntersectionArc2Arc2.Contains(this.arc, vector))
				{
					this.Point0 = vector;
					this.Parameter0 = array[0];
					int quantity = this.Quantity;
					this.Quantity = quantity + 1;
					if (num == 2 && IntersectionArc2Arc2.Contains(this.arc, vector2))
					{
						this.Point1 = vector2;
						this.Parameter1 = array[1];
						quantity = this.Quantity;
						this.Quantity = quantity + 1;
					}
				}
				else if (num == 2 && IntersectionArc2Arc2.Contains(this.arc, vector2))
				{
					this.Point0 = vector2;
					this.Parameter0 = array[1];
					int quantity = this.Quantity;
					this.Quantity = quantity + 1;
				}
			}
			this.IntersectionType = ((this.Quantity > 0) ? Intersection.Type.IT_POINT : Intersection.Type.IT_EMPTY);
			return this.IntersectionType > Intersection.Type.IT_EMPTY;
		}

		// Token: 0x040000BC RID: 188
		private readonly Line2 line;

		// Token: 0x040000BD RID: 189
		private readonly Arc2 arc;
	}
}
