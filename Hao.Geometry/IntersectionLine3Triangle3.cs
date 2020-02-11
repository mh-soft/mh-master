using System;

namespace Hao.Geometry
{
	// Token: 0x0200005A RID: 90
	internal struct IntersectionLine3Triangle3
	{
		// Token: 0x06000382 RID: 898 RVA: 0x0000F6BE File Offset: 0x0000D8BE
		public IntersectionLine3Triangle3(Line3 line, Triangle3 triangle)
		{
			this = default(IntersectionLine3Triangle3);
			this.line = line;
			this.triangle = triangle;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000F6D5 File Offset: 0x0000D8D5
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000F6DD File Offset: 0x0000D8DD
		public double LineParameter { get; private set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000F6E6 File Offset: 0x0000D8E6
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000F6EE File Offset: 0x0000D8EE
		public double TriB0 { get; private set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000F6F7 File Offset: 0x0000D8F7
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000F6FF File Offset: 0x0000D8FF
		public double TriB1 { get; private set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000F708 File Offset: 0x0000D908
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000F710 File Offset: 0x0000D910
		public double TriB2 { get; private set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000F719 File Offset: 0x0000D919
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000F721 File Offset: 0x0000D921
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x0600038D RID: 909 RVA: 0x0000F72C File Offset: 0x0000D92C
		public bool Test()
		{
			Vector3 vector = this.line.Origin - this.triangle.V0;
			Vector3 vector2 = this.triangle.V1 - this.triangle.V0;
			Vector3 vector3 = this.triangle.V2 - this.triangle.V0;
			Vector3 vector4 = vector2.Cross(vector3);
			double num = this.line.Direction.Dot(vector4);
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
			double num3 = num2 * this.line.Direction.Dot(vector.Cross(vector3));
			if (num3 >= 0.0)
			{
				double num4 = num2 * this.line.Direction.Dot(vector2.Cross(vector));
				if (num4 >= 0.0 && num3 + num4 <= num)
				{
					this.IntersectionType = Intersection.Type.IT_POINT;
					return true;
				}
			}
			this.IntersectionType = Intersection.Type.IT_EMPTY;
			return false;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000F88C File Offset: 0x0000DA8C
		public bool Find()
		{
			Vector3 vector = this.line.Origin - this.triangle.V0;
			Vector3 vector2 = this.triangle.V1 - this.triangle.V0;
			Vector3 vector3 = this.triangle.V2 - this.triangle.V0;
			Vector3 vector4 = vector2.Cross(vector3);
			double num = this.line.Direction.Dot(vector4);
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
			double num3 = num2 * this.line.Direction.Dot(vector.Cross(vector3));
			if (num3 >= 0.0)
			{
				double num4 = num2 * this.line.Direction.Dot(vector2.Cross(vector));
				if (num4 >= 0.0 && num3 + num4 <= num)
				{
					double num5 = -num2 * vector.Dot(vector4);
					double num6 = 1.0 / num;
					this.LineParameter = num5 * num6;
					this.TriB1 = num3 * num6;
					this.TriB2 = num4 * num6;
					this.TriB0 = 1.0 - this.TriB1 - this.TriB2;
					this.IntersectionType = Intersection.Type.IT_POINT;
					return true;
				}
			}
			this.IntersectionType = Intersection.Type.IT_EMPTY;
			return false;
		}

		// Token: 0x040000F5 RID: 245
		private readonly Line3 line;

		// Token: 0x040000F6 RID: 246
		private readonly Triangle3 triangle;
	}
}
