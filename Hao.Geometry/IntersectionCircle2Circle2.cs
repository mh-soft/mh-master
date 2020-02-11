using System;

namespace Hao.Geometry
{
	// Token: 0x02000049 RID: 73
	internal struct IntersectionCircle2Circle2
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x0000BDDB File Offset: 0x00009FDB
		public IntersectionCircle2Circle2(Circle2 circle0, Circle2 circle1)
		{
			this = default(IntersectionCircle2Circle2);
			this.circle0 = circle0;
			this.circle1 = circle1;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000BDF2 File Offset: 0x00009FF2
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000BDFA File Offset: 0x00009FFA
		public int Quantity { get; private set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000BE03 File Offset: 0x0000A003
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000BE0B File Offset: 0x0000A00B
		public Vector2 Point0 { get; private set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000BE14 File Offset: 0x0000A014
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0000BE1C File Offset: 0x0000A01C
		public Vector2 Point1 { get; private set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000BE25 File Offset: 0x0000A025
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000BE2D File Offset: 0x0000A02D
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x060002FD RID: 765 RVA: 0x0000BE38 File Offset: 0x0000A038
		public bool Find()
		{
			Vector2 vector = this.circle1.Center - this.circle0.Center;
			double squaredLength = vector.SquaredLength;
			double radius = this.circle0.Radius;
			double radius2 = this.circle1.Radius;
			double num = radius - radius2;
			if (squaredLength < 1E-08 && Math.Abs(num) < 1E-08)
			{
				this.IntersectionType = Intersection.Type.IT_OTHER;
				this.Quantity = 0;
				return true;
			}
			double num2 = num;
			double num3 = num2 * num2;
			if (squaredLength < num3)
			{
				this.IntersectionType = Intersection.Type.IT_EMPTY;
				this.Quantity = 0;
				return false;
			}
			double num4 = radius + radius2;
			double num5 = num4;
			double num6 = num5 * num5;
			if (squaredLength > num6)
			{
				this.IntersectionType = Intersection.Type.IT_EMPTY;
				this.Quantity = 0;
				return false;
			}
			if (squaredLength < num6)
			{
				if (num3 < squaredLength)
				{
					double num7 = 1.0 / squaredLength;
					double num8 = 0.5;
					double num9 = radius;
					double num10 = num9 * num9;
					double num11 = radius2;
					double num12 = num8 * ((num10 - num11 * num11) * num7 + 1.0);
					Vector2 left = this.circle0.Center + num12 * vector;
					double num13 = radius;
					double num14 = num13 * num13 * num7;
					double num15 = num12;
					double num16 = num14 - num15 * num15;
					if (num16 < 0.0)
					{
						num16 = 0.0;
					}
					double scalar = Math.Sqrt(num16);
					Vector2 vector2 = new Vector2(vector.Y, -vector.X);
					this.Point0 = left - scalar * vector2;
					this.Point1 = left + scalar * vector2;
					this.Quantity = 2;
				}
				else
				{
					this.Point0 = this.circle0.Center + radius / num * vector;
					this.Quantity = 1;
				}
			}
			else
			{
				this.Quantity = 1;
				this.Point0 = this.circle0.Center + radius / num4 * vector;
			}
			this.IntersectionType = Intersection.Type.IT_POINT;
			return true;
		}

		// Token: 0x040000D0 RID: 208
		private readonly Circle2 circle0;

		// Token: 0x040000D1 RID: 209
		private readonly Circle2 circle1;
	}
}
