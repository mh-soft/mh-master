using System;

namespace Hao.Geometry
{
	// Token: 0x02000043 RID: 67
	internal struct IntersectionLine3Torus3
	{
		// Token: 0x060002AA RID: 682 RVA: 0x0000B0DD File Offset: 0x000092DD
		public IntersectionLine3Torus3(Line3 line, Torus3 torus)
		{
			this = default(IntersectionLine3Torus3);
			this.line = line;
			this.torus = torus;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000B0F4 File Offset: 0x000092F4
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000B0FC File Offset: 0x000092FC
		public int Quantity { get; private set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000B105 File Offset: 0x00009305
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000B10D File Offset: 0x0000930D
		public Vector3 Point0 { get; private set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000B116 File Offset: 0x00009316
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000B11E File Offset: 0x0000931E
		public Vector3 Point1 { get; private set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000B127 File Offset: 0x00009327
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000B12F File Offset: 0x0000932F
		public Vector3 Point2 { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000B138 File Offset: 0x00009338
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000B140 File Offset: 0x00009340
		public Vector3 Point3 { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000B149 File Offset: 0x00009349
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000B151 File Offset: 0x00009351
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x060002B7 RID: 695 RVA: 0x0000B15C File Offset: 0x0000935C
		public static double[] GetRoots(Line3 line, Torus3 torus, out int quantity)
		{
			double num = torus.OuterRadius * torus.OuterRadius;
			double num2 = torus.InnerRadius * torus.InnerRadius;
			double num3 = line.Direction.Dot(line.Direction);
			double num4 = line.Origin.Dot(line.Direction);
			double num5 = line.Origin.Dot(line.Origin) - (num + num2);
			double[] array = new double[5];
			double z = line.Origin.Z;
			double z2 = line.Direction.Z;
			double[] array2 = array;
			int num6 = 0;
			double num7 = num5;
			double num8 = num7 * num7;
			double num9 = 4.0 * num;
			double num10 = num2;
			double num11 = z;
			array2[num6] = num8 - num9 * (num10 - num11 * num11);
			array[1] = 4.0 * num4 * num5 + 8.0 * num * z2 * z;
			array[2] = 2.0 * num3 * num5 + 4.0 * num4 * num4 + 4.0 * num * z2 * z2;
			array[3] = 4.0 * num3 * num4;
			double[] array3 = array;
			int num12 = 4;
			double num13 = num3;
			array3[num12] = num13 * num13;
			PolynomialRoots polynomialRoots = new PolynomialRoots(1E-08);
			polynomialRoots.FindB(new Polynomial1(array), 6);
			quantity = polynomialRoots.Count;
			return polynomialRoots.Roots;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B2BC File Offset: 0x000094BC
		public bool Find()
		{
			int quantity;
			double[] roots = IntersectionLine3Torus3.GetRoots(this.line, this.torus, out quantity);
			this.Quantity = quantity;
			Vector3[] array = new Vector3[4];
			for (int i = 0; i < this.Quantity; i++)
			{
				array[i] = this.line.Origin + roots[i] * this.line.Direction;
			}
			this.Point0 = array[0];
			this.Point1 = array[1];
			this.Point2 = array[2];
			this.Point3 = array[3];
			this.IntersectionType = ((this.Quantity > 0) ? Intersection.Type.IT_POINT : Intersection.Type.IT_EMPTY);
			return this.IntersectionType > Intersection.Type.IT_EMPTY;
		}

		// Token: 0x040000A7 RID: 167
		private readonly Line3 line;

		// Token: 0x040000A8 RID: 168
		private readonly Torus3 torus;
	}
}
