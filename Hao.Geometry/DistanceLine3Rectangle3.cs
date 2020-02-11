using System;

namespace Hao.Geometry
{
	// Token: 0x0200001B RID: 27
	internal struct DistanceLine3Rectangle3
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x000052B4 File Offset: 0x000034B4
		public DistanceLine3Rectangle3(Line3 line, Rectangle3 rectangle)
		{
			this = default(DistanceLine3Rectangle3);
			if (Math.Abs(rectangle.Axis0.Cross(rectangle.Axis1).Dot(line.Direction)) > 1E-08)
			{
				Vector3 vector = line.Origin - rectangle.Center;
				UnitVector3 direction = line.Direction;
				AffineTransform3 affineTransform = Transform3Factory.CreateOrthonormalBasis(direction);
				double num = affineTransform.AxisX.Dot(rectangle.Axis0);
				double num2 = affineTransform.AxisX.Dot(rectangle.Axis1);
				double num3 = affineTransform.AxisX.Dot(vector);
				double num4 = affineTransform.AxisY.Dot(rectangle.Axis0);
				double num5 = affineTransform.AxisY.Dot(rectangle.Axis1);
				double num6 = affineTransform.AxisY.Dot(vector);
				double num7 = 1.0 / (num * num5 - num2 * num4);
				double num8 = (num5 * num3 - num2 * num6) * num7;
				double num9 = (num * num6 - num4 * num3) * num7;
				if (Math.Abs(num8) <= rectangle.Extent0 && Math.Abs(num9) <= rectangle.Extent1)
				{
					double num10 = direction.Dot(rectangle.Axis0);
					double num11 = direction.Dot(rectangle.Axis1);
					double num12 = line.Direction.Dot(vector);
					this.LineParameter = num8 * num10 + num9 * num11 - num12;
					this.RectCoord0 = num8;
					this.RectCoord1 = num9;
					this.ClosestPointOnLine = line.Origin + this.LineParameter * line.Direction;
					this.ClosestPointOnRectangle = rectangle.Center + num8 * rectangle.Axis0 + num9 * rectangle.Axis1;
					this.SquaredDistance = 0.0;
					return;
				}
			}
			double num13 = double.MaxValue;
			Vector3[] array = new Vector3[]
			{
				rectangle.Extent0 * rectangle.Axis0,
				rectangle.Extent1 * rectangle.Axis1
			};
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					Vector3 origin = rectangle.Center + (double)(2 * j - 1) * array[i];
					UnitVector3 direction2 = (i == 0) ? rectangle.Axis1 : rectangle.Axis0;
					double extent = (i == 0) ? rectangle.Extent1 : rectangle.Extent0;
					Segment3 segment = new Segment3(origin, direction2, extent);
					DistanceLine3Segment3 distanceLine3Segment = new DistanceLine3Segment3(line, segment);
					double squaredDistance = distanceLine3Segment.SquaredDistance;
					if (squaredDistance < num13)
					{
						this.ClosestPointOnLine = distanceLine3Segment.ClosestPointOnLine;
						this.ClosestPointOnRectangle = distanceLine3Segment.ClosestPointOnSegment;
						num13 = squaredDistance;
						this.LineParameter = distanceLine3Segment.LineParameter;
						double num14 = distanceLine3Segment.SegmentParameter / segment.Extent;
						this.RectCoord0 = rectangle.Extent0 * ((double)((1 - i) * (2 * j - 1)) + (double)i * num14);
						this.RectCoord1 = rectangle.Extent1 * ((double)((1 - j) * (2 * i - 1)) + (double)j * num14);
					}
				}
			}
			this.SquaredDistance = num13;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005630 File Offset: 0x00003830
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00005638 File Offset: 0x00003838
		public double LineParameter { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005641 File Offset: 0x00003841
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00005649 File Offset: 0x00003849
		public double RectCoord0 { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005652 File Offset: 0x00003852
		// (set) Token: 0x060000DF RID: 223 RVA: 0x0000565A File Offset: 0x0000385A
		public double RectCoord1 { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005663 File Offset: 0x00003863
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x0000566B File Offset: 0x0000386B
		public Vector3 ClosestPointOnLine { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005674 File Offset: 0x00003874
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000567C File Offset: 0x0000387C
		public Vector3 ClosestPointOnRectangle { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005685 File Offset: 0x00003885
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000568D File Offset: 0x0000388D
		public double SquaredDistance { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005696 File Offset: 0x00003896
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}
	}
}
