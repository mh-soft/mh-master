using System;

namespace Hao.Geometry
{
	// Token: 0x02000048 RID: 72
	internal struct IntersectionLine2Circle2
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x0000BBE9 File Offset: 0x00009DE9
		public IntersectionLine2Circle2(Line2 line, Circle2 circle)
		{
			this = default(IntersectionLine2Circle2);
			this.line = line;
			this.circle = circle;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000BC00 File Offset: 0x00009E00
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0000BC08 File Offset: 0x00009E08
		public int Quantity { get; private set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000BC11 File Offset: 0x00009E11
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000BC19 File Offset: 0x00009E19
		public Vector2 Point0 { get; private set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000BC22 File Offset: 0x00009E22
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000BC2A File Offset: 0x00009E2A
		public Vector2 Point1 { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000BC33 File Offset: 0x00009E33
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000BC3B File Offset: 0x00009E3B
		public double Parameter0 { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000BC44 File Offset: 0x00009E44
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000BC4C File Offset: 0x00009E4C
		public double Parameter1 { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000BC55 File Offset: 0x00009E55
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000BC5D File Offset: 0x00009E5D
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x060002F2 RID: 754 RVA: 0x0000BC68 File Offset: 0x00009E68
		public bool Find()
		{
			int quantity;
			double[] array;
			bool flag = IntersectionLine2Circle2.Find(this.line.Origin, this.line.Direction, this.circle.Center, this.circle.Radius, out quantity, out array);
			this.Quantity = quantity;
			if (flag)
			{
				this.Point0 = this.line.Origin + array[0] * this.line.Direction;
				this.Parameter0 = array[0];
				if (this.Quantity == 2)
				{
					this.Point1 = this.line.Origin + array[1] * this.line.Direction;
					this.Parameter1 = array[1];
				}
			}
			this.IntersectionType = ((this.Quantity > 0) ? Intersection.Type.IT_POINT : Intersection.Type.IT_EMPTY);
			return this.IntersectionType > Intersection.Type.IT_EMPTY;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000BD54 File Offset: 0x00009F54
		internal static bool Find(Vector2 origin, UnitVector2 direction, Vector2 center, double radius, out int rootCount, out double[] t)
		{
			t = new double[2];
			Vector2 vector = origin - center;
			double num = vector.SquaredLength - radius * radius;
			double num2 = direction.Dot(vector);
			double num3 = num2;
			double num4 = num3 * num3 - num;
			if (num4 > 1E-08)
			{
				rootCount = 2;
				num4 = Math.Sqrt(num4);
				t[0] = -num2 - num4;
				t[1] = -num2 + num4;
			}
			else if (num4 < -1E-08)
			{
				rootCount = 0;
			}
			else
			{
				rootCount = 1;
				t[0] = -num2;
			}
			return rootCount != 0;
		}

		// Token: 0x040000C8 RID: 200
		private readonly Line2 line;

		// Token: 0x040000C9 RID: 201
		private readonly Circle2 circle;
	}
}
