using System;

namespace Hao.Geometry
{
	// Token: 0x02000062 RID: 98
	internal struct IntersectionRay3Cylinder3
	{
		// Token: 0x060003CD RID: 973 RVA: 0x0001142E File Offset: 0x0000F62E
		public IntersectionRay3Cylinder3(Ray3 ray, Cylinder3 cylinder)
		{
			this = default(IntersectionRay3Cylinder3);
			this.ray = ray;
			this.cylinder = cylinder;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00011445 File Offset: 0x0000F645
		// (set) Token: 0x060003CF RID: 975 RVA: 0x0001144D File Offset: 0x0000F64D
		public Vector3 Point0 { get; private set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00011456 File Offset: 0x0000F656
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x0001145E File Offset: 0x0000F65E
		public Vector3 Point1 { get; private set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00011467 File Offset: 0x0000F667
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0001146F File Offset: 0x0000F66F
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x060003D4 RID: 980 RVA: 0x00011478 File Offset: 0x0000F678
		public bool Find()
		{
			double[] array = new double[2];
			int num = IntersectionLine3Cylinder3.Find(this.ray.Origin, this.ray.Direction, this.cylinder, array);
			if (num == 2)
			{
				if (array[0] >= 0.0 && array[1] >= 0.0)
				{
					this.Point0 = this.ray.Origin + array[0] * this.ray.Direction;
					this.Point1 = this.ray.Origin + array[1] * this.ray.Direction;
					this.IntersectionType = Intersection.Type.IT_SEGMENT;
				}
				else if (array[1] >= 0.0)
				{
					this.Point0 = this.ray.Origin + array[1] * this.ray.Direction;
					this.IntersectionType = Intersection.Type.IT_POINT;
				}
				else
				{
					this.IntersectionType = Intersection.Type.IT_EMPTY;
				}
			}
			else if (num == 1)
			{
				if (array[0] >= 0.0)
				{
					this.Point0 = this.ray.Origin + array[0] * this.ray.Direction;
					this.IntersectionType = Intersection.Type.IT_POINT;
				}
				else
				{
					this.IntersectionType = Intersection.Type.IT_EMPTY;
				}
			}
			else
			{
				this.IntersectionType = Intersection.Type.IT_EMPTY;
			}
			return this.IntersectionType > Intersection.Type.IT_EMPTY;
		}

		// Token: 0x0400010D RID: 269
		private readonly Ray3 ray;

		// Token: 0x0400010E RID: 270
		private readonly Cylinder3 cylinder;
	}
}
