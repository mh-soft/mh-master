using System;

namespace Hao.Geometry
{
	// Token: 0x02000059 RID: 89
	internal struct IntersectionLine3Plane3
	{
		// Token: 0x0600037B RID: 891 RVA: 0x0000F526 File Offset: 0x0000D726
		public IntersectionLine3Plane3(Line3 line, Plane3 plane)
		{
			this = default(IntersectionLine3Plane3);
			this.line = line;
			this.plane = plane;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000F53D File Offset: 0x0000D73D
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0000F545 File Offset: 0x0000D745
		public double LineParameter { get; private set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000F54E File Offset: 0x0000D74E
		// (set) Token: 0x0600037F RID: 895 RVA: 0x0000F556 File Offset: 0x0000D756
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x06000380 RID: 896 RVA: 0x0000F560 File Offset: 0x0000D760
		public bool Test()
		{
			if (Math.Abs(this.line.Direction.Dot(this.plane.Normal)) > 1E-08)
			{
				this.IntersectionType = Intersection.Type.IT_POINT;
				return true;
			}
			if (Math.Abs(this.plane.Normal.Dot(this.line.Origin) - this.plane.Constant) <= 1E-08)
			{
				this.IntersectionType = Intersection.Type.IT_LINE;
				return true;
			}
			this.IntersectionType = Intersection.Type.IT_EMPTY;
			return false;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000F600 File Offset: 0x0000D800
		public bool Find()
		{
			double num = this.line.Direction.Dot(this.plane.Normal);
			double num2 = this.plane.Normal.Dot(this.line.Origin) - this.plane.Constant;
			if (Math.Abs(num) > 1E-08)
			{
				this.LineParameter = -num2 / num;
				this.IntersectionType = Intersection.Type.IT_POINT;
				return true;
			}
			if (Math.Abs(num2) <= 1E-08)
			{
				this.LineParameter = 0.0;
				this.IntersectionType = Intersection.Type.IT_LINE;
				return true;
			}
			this.IntersectionType = Intersection.Type.IT_EMPTY;
			return false;
		}

		// Token: 0x040000F1 RID: 241
		private readonly Line3 line;

		// Token: 0x040000F2 RID: 242
		private readonly Plane3 plane;
	}
}
