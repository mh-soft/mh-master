using System;

namespace Hao.Geometry
{
	// Token: 0x0200006C RID: 108
	internal struct IntersectionSegment3Triangle3
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x00012D2B File Offset: 0x00010F2B
		public IntersectionSegment3Triangle3(Segment3 segment, Triangle3 triangle)
		{
			this = default(IntersectionSegment3Triangle3);
			this.segment = segment;
			this.triangle = triangle;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00012D42 File Offset: 0x00010F42
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x00012D4A File Offset: 0x00010F4A
		public double SegmentParameter { get; private set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00012D53 File Offset: 0x00010F53
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x00012D5B File Offset: 0x00010F5B
		public double TriB0 { get; private set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00012D64 File Offset: 0x00010F64
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x00012D6C File Offset: 0x00010F6C
		public double TriB1 { get; private set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00012D75 File Offset: 0x00010F75
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x00012D7D File Offset: 0x00010F7D
		public double TriB2 { get; private set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00012D86 File Offset: 0x00010F86
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x00012D8E File Offset: 0x00010F8E
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x06000432 RID: 1074 RVA: 0x00012D98 File Offset: 0x00010F98
		public bool Test()
		{
			Vector3 vector = this.segment.Origin - this.triangle.V0;
			Vector3 vector2 = this.triangle.V1 - this.triangle.V0;
			Vector3 vector3 = this.triangle.V2 - this.triangle.V0;
			Vector3 vector4 = vector2.Cross(vector3);
			double num = this.segment.Direction.Dot(vector4);
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
			double num3 = num2 * this.segment.Direction.Dot(vector.Cross(vector3));
			if (num3 >= 0.0)
			{
				double num4 = num2 * this.segment.Direction.Dot(vector2.Cross(vector));
				if (num4 >= 0.0 && num3 + num4 <= num)
				{
					double num5 = -num2 * vector.Dot(vector4);
					double num6 = this.segment.Extent * num;
					if (-num6 <= num5 && num5 <= num6)
					{
						this.IntersectionType = Intersection.Type.IT_POINT;
						return true;
					}
				}
			}
			this.IntersectionType = Intersection.Type.IT_EMPTY;
			return false;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00012F24 File Offset: 0x00011124
		public bool Find()
		{
			Vector3 vector = this.segment.Origin - this.triangle.V0;
			Vector3 vector2 = this.triangle.V1 - this.triangle.V0;
			Vector3 vector3 = this.triangle.V2 - this.triangle.V0;
			Vector3 vector4 = vector2.Cross(vector3);
			double num = this.segment.Direction.Dot(vector4);
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
			double num3 = num2 * this.segment.Direction.Dot(vector.Cross(vector3));
			if (num3 >= 0.0)
			{
				double num4 = num2 * this.segment.Direction.Dot(vector2.Cross(vector));
				if (num4 >= 0.0 && num3 + num4 <= num)
				{
					double num5 = -num2 * vector.Dot(vector4);
					double num6 = this.segment.Extent * num;
					if (-num6 <= num5 && num5 <= num6)
					{
						double num7 = 1.0 / num;
						this.SegmentParameter = num5 * num7;
						this.TriB1 = num3 * num7;
						this.TriB2 = num4 * num7;
						this.TriB0 = 1.0 - this.TriB1 - this.TriB2;
						this.IntersectionType = Intersection.Type.IT_POINT;
						return true;
					}
				}
			}
			this.IntersectionType = Intersection.Type.IT_EMPTY;
			return false;
		}

		// Token: 0x04000136 RID: 310
		private readonly Segment3 segment;

		// Token: 0x04000137 RID: 311
		private readonly Triangle3 triangle;
	}
}
