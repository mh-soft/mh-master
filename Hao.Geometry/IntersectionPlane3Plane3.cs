using System;

namespace Hao.Geometry
{
	// Token: 0x0200005D RID: 93
	internal struct IntersectionPlane3Plane3
	{
		// Token: 0x060003A5 RID: 933 RVA: 0x0000FF86 File Offset: 0x0000E186
		public IntersectionPlane3Plane3(Plane3 plane0, Plane3 plane1)
		{
			this = default(IntersectionPlane3Plane3);
			this.plane0 = plane0;
			this.plane1 = plane1;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000FF9D File Offset: 0x0000E19D
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x0000FFA5 File Offset: 0x0000E1A5
		public Line3 IntersectionLine { get; private set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000FFAE File Offset: 0x0000E1AE
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x0000FFB6 File Offset: 0x0000E1B6
		public Plane3 IntersectionPlane { get; private set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000FFBF File Offset: 0x0000E1BF
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0000FFC7 File Offset: 0x0000E1C7
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x060003AC RID: 940 RVA: 0x0000FFD0 File Offset: 0x0000E1D0
		public bool Test()
		{
			return Math.Abs(this.plane0.Normal.Dot(this.plane1.Normal)) < 0.99999999;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00010014 File Offset: 0x0000E214
		public bool Find()
		{
			double num = this.plane0.Normal.Dot(this.plane1.Normal);
			if (Math.Abs(num) < 0.99999999)
			{
				double num2 = 1.0;
				double num3 = 1.0;
				double num4 = num;
				double num5 = num2 / (num3 - num4 * num4);
				double scalar = (this.plane0.Constant - num * this.plane1.Constant) * num5;
				double scalar2 = (this.plane1.Constant - num * this.plane0.Constant) * num5;
				this.IntersectionType = Intersection.Type.IT_LINE;
				Vector3 origin = scalar * this.plane0.Normal + scalar2 * this.plane1.Normal;
				UnitVector3 direction = this.plane0.Normal.UnitCross(this.plane1.Normal);
				this.IntersectionLine = new Line3(origin, direction);
				return true;
			}
			double value;
			if (num >= 0.0)
			{
				value = this.plane0.Constant - this.plane1.Constant;
			}
			else
			{
				value = this.plane0.Constant + this.plane1.Constant;
			}
			if (Math.Abs(value) < 1E-08)
			{
				this.IntersectionType = Intersection.Type.IT_PLANE;
				this.IntersectionPlane = this.plane0;
				return true;
			}
			this.IntersectionType = Intersection.Type.IT_EMPTY;
			return false;
		}

		// Token: 0x040000FE RID: 254
		private readonly Plane3 plane0;

		// Token: 0x040000FF RID: 255
		private readonly Plane3 plane1;
	}
}
