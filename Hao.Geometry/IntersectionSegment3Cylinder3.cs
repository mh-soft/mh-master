using System;

namespace Hao.Geometry
{
	// Token: 0x0200006A RID: 106
	internal struct IntersectionSegment3Cylinder3
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x00012934 File Offset: 0x00010B34
		public IntersectionSegment3Cylinder3(Segment3 segment, Cylinder3 cylinder)
		{
			this = default(IntersectionSegment3Cylinder3);
			this.segment = segment;
			this.cylinder = cylinder;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0001294B File Offset: 0x00010B4B
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x00012953 File Offset: 0x00010B53
		public Vector3 Point0 { get; private set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0001295C File Offset: 0x00010B5C
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x00012964 File Offset: 0x00010B64
		public Vector3 Point1 { get; private set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0001296D File Offset: 0x00010B6D
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00012975 File Offset: 0x00010B75
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x0600041F RID: 1055 RVA: 0x00012980 File Offset: 0x00010B80
		public bool Find()
		{
			double[] array = new double[2];
			int num = IntersectionLine3Cylinder3.Find(this.segment.Origin, this.segment.Direction, this.cylinder, array);
			double num2 = this.segment.Extent + 1E-08;
			if (num == 2)
			{
				if (Math.Abs(array[0]) < num2 && Math.Abs(array[1]) < num2)
				{
					this.Point0 = this.segment.Origin + array[0] * this.segment.Direction;
					this.Point1 = this.segment.Origin + array[1] * this.segment.Direction;
					this.IntersectionType = Intersection.Type.IT_SEGMENT;
				}
				else if (Math.Abs(array[0]) < num2)
				{
					this.Point0 = this.segment.Origin + array[0] * this.segment.Direction;
					this.IntersectionType = Intersection.Type.IT_POINT;
				}
				else if (Math.Abs(array[1]) < num2)
				{
					this.Point0 = this.segment.Origin + array[1] * this.segment.Direction;
					this.IntersectionType = Intersection.Type.IT_POINT;
				}
				else
				{
					this.IntersectionType = Intersection.Type.IT_EMPTY;
				}
			}
			else if (num == 1)
			{
				if (Math.Abs(array[0]) < num2)
				{
					this.Point0 = this.segment.Origin + array[0] * this.segment.Direction;
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

		// Token: 0x0400012D RID: 301
		private readonly Segment3 segment;

		// Token: 0x0400012E RID: 302
		private readonly Cylinder3 cylinder;
	}
}
