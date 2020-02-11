using System;

namespace Hao.Geometry
{
	// Token: 0x02000042 RID: 66
	internal struct IntersectionRay3Torus3
	{
		// Token: 0x0600029C RID: 668 RVA: 0x0000AF62 File Offset: 0x00009162
		public IntersectionRay3Torus3(Ray3 ray, Torus3 torus)
		{
			this = default(IntersectionRay3Torus3);
			this.ray = ray;
			this.torus = torus;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000AF79 File Offset: 0x00009179
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000AF81 File Offset: 0x00009181
		public int Quantity { get; private set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000AF8A File Offset: 0x0000918A
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000AF92 File Offset: 0x00009192
		public Vector3 Point0 { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000AF9B File Offset: 0x0000919B
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000AFA3 File Offset: 0x000091A3
		public Vector3 Point1 { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000AFAC File Offset: 0x000091AC
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000AFB4 File Offset: 0x000091B4
		public Vector3 Point2 { get; private set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000AFBD File Offset: 0x000091BD
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000AFC5 File Offset: 0x000091C5
		public Vector3 Point3 { get; private set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000AFCE File Offset: 0x000091CE
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000AFD6 File Offset: 0x000091D6
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x060002A9 RID: 681 RVA: 0x0000AFE0 File Offset: 0x000091E0
		public bool Find()
		{
			int num;
			double[] roots = IntersectionLine3Torus3.GetRoots(new Line3(this.ray.Origin, this.ray.Direction), this.torus, out num);
			this.Quantity = 0;
			Vector3[] array = new Vector3[4];
			for (int i = 0; i < num; i++)
			{
				if (roots[i] >= 0.0)
				{
					Vector3[] array2 = array;
					int quantity = this.Quantity;
					this.Quantity = quantity + 1;
					array2[quantity] = this.ray.Origin + roots[i] * this.ray.Direction;
				}
			}
			this.Point0 = array[0];
			this.Point1 = array[1];
			this.Point2 = array[2];
			this.Point3 = array[3];
			this.IntersectionType = ((this.Quantity > 0) ? Intersection.Type.IT_POINT : Intersection.Type.IT_EMPTY);
			return this.IntersectionType > Intersection.Type.IT_EMPTY;
		}

		// Token: 0x0400009F RID: 159
		private readonly Ray3 ray;

		// Token: 0x040000A0 RID: 160
		private readonly Torus3 torus;
	}
}
