using System;

namespace Hao.Geometry
{
	// Token: 0x02000069 RID: 105
	internal struct IntersectionSegment3Box3
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x00012666 File Offset: 0x00010866
		public IntersectionSegment3Box3(Segment3 segment, Box3 box, bool solid)
		{
			this = default(IntersectionSegment3Box3);
			this.segment = segment;
			this.box = box;
			this.solid = solid;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00012684 File Offset: 0x00010884
		public bool Test()
		{
			Vector3 vector = this.segment.Origin - this.box.Center;
			double num = Math.Abs(this.segment.Direction.Dot(this.box.Axis0));
			double num2 = Math.Abs(vector.Dot(this.box.Axis0));
			double num3 = this.box.Extent0 + this.segment.Extent * num;
			if (num2 > num3 + 1E-08)
			{
				return false;
			}
			double num4 = Math.Abs(this.segment.Direction.Dot(this.box.Axis1));
			double num5 = Math.Abs(vector.Dot(this.box.Axis1));
			double num6 = this.box.Extent1 + this.segment.Extent * num4;
			if (num5 > num6 + 1E-08)
			{
				return false;
			}
			double num7 = Math.Abs(this.segment.Direction.Dot(this.box.Axis2));
			double num8 = Math.Abs(vector.Dot(this.box.Axis2));
			double num9 = this.box.Extent2 + this.segment.Extent * num7;
			if (num8 > num9 + 1E-08)
			{
				return false;
			}
			Vector3 vector2 = this.segment.Direction.Cross(vector);
			double num10 = Math.Abs(vector2.Dot(this.box.Axis0));
			double num11 = this.box.Extent1 * num7 + this.box.Extent2 * num4;
			if (num10 > num11 + 1E-08)
			{
				return false;
			}
			double num12 = Math.Abs(vector2.Dot(this.box.Axis1));
			double num13 = this.box.Extent0 * num7 + this.box.Extent2 * num;
			if (num12 > num13 + 1E-08)
			{
				return false;
			}
			double num14 = Math.Abs(vector2.Dot(this.box.Axis2));
			double num15 = this.box.Extent0 * num4 + this.box.Extent1 * num;
			return num14 <= num15 + 1E-08;
		}

		// Token: 0x0400012A RID: 298
		private readonly Segment3 segment;

		// Token: 0x0400012B RID: 299
		private readonly Box3 box;

		// Token: 0x0400012C RID: 300
		private readonly bool solid;
	}
}
