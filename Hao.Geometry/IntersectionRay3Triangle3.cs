using System;

namespace Hao.Geometry
{
	// Token: 0x02000064 RID: 100
	internal struct IntersectionRay3Triangle3
	{
		// Token: 0x060003DC RID: 988 RVA: 0x000116B6 File Offset: 0x0000F8B6
		public IntersectionRay3Triangle3(Ray3 ray, Triangle3 triangle)
		{
			this = default(IntersectionRay3Triangle3);
			this.ray = ray;
			this.triangle = triangle;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003DD RID: 989 RVA: 0x000116CD File Offset: 0x0000F8CD
		// (set) Token: 0x060003DE RID: 990 RVA: 0x000116D5 File Offset: 0x0000F8D5
		public double RayParameter { get; private set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003DF RID: 991 RVA: 0x000116DE File Offset: 0x0000F8DE
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x000116E6 File Offset: 0x0000F8E6
		public double TriB0 { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x000116EF File Offset: 0x0000F8EF
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x000116F7 File Offset: 0x0000F8F7
		public double TriB1 { get; private set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00011700 File Offset: 0x0000F900
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x00011708 File Offset: 0x0000F908
		public double TriB2 { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00011711 File Offset: 0x0000F911
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x00011719 File Offset: 0x0000F919
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x060003E7 RID: 999 RVA: 0x00011724 File Offset: 0x0000F924
		public bool Find()
		{
			Vector3 vector = this.ray.Origin - this.triangle.V0;
			Vector3 vector2 = this.triangle.V1 - this.triangle.V0;
			Vector3 vector3 = this.triangle.V2 - this.triangle.V0;
			Vector3 vector4 = vector2.Cross(vector3);
			double num = this.ray.Direction.Dot(vector4);
			double num2;
			if (num > 1E-08)
			{
				num2 = 1.0;
			}
			else
			{
				if (num >= -1E-08)
				{
					this.IntersectionType = Intersection.Type.IT_EMPTY;
					return false;
				}
				num2 = -1.0;
				num = -num;
			}
			double num3 = num2 * this.ray.Direction.Dot(vector.Cross(vector3));
			if (num3 >= 0.0)
			{
				double num4 = num2 * this.ray.Direction.Dot(vector2.Cross(vector));
				if (num4 >= 0.0 && num3 + num4 <= num)
				{
					double num5 = -num2 * vector.Dot(vector4);
					if (num5 >= 0.0)
					{
						double num6 = 1.0 / num;
						this.RayParameter = num5 * num6;
						this.TriB1 = num3 * num6;
						this.TriB2 = num4 * num6;
						this.TriB0 = 1.0 - this.TriB1 - this.TriB2;
						this.IntersectionType = Intersection.Type.IT_POINT;
						return true;
					}
				}
			}
			this.IntersectionType = Intersection.Type.IT_EMPTY;
			return false;
		}

		// Token: 0x04000116 RID: 278
		private readonly Ray3 ray;

		// Token: 0x04000117 RID: 279
		private readonly Triangle3 triangle;
	}
}
