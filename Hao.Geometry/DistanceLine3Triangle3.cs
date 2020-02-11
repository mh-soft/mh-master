using System;

namespace Hao.Geometry
{
	// Token: 0x0200001D RID: 29
	internal struct DistanceLine3Triangle3
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x000058E0 File Offset: 0x00003AE0
		public DistanceLine3Triangle3(Line3 line, Triangle3 triangle)
		{
			this = default(DistanceLine3Triangle3);
			Vector3 vector = triangle.V1 - triangle.V0;
			Vector3 vector2 = triangle.V2 - triangle.V0;
			UnitVector3 unitVector;
			if (vector.TryGetUnitCross(vector2, out unitVector) && Math.Abs(unitVector.Dot(line.Direction)) > 1E-08)
			{
				Vector3 vector3 = line.Origin - triangle.V0;
				UnitVector3 direction = line.Direction;
				AffineTransform3 affineTransform = Transform3Factory.CreateOrthonormalBasis(direction);
				double num = affineTransform.AxisX.Dot(vector);
				double num2 = affineTransform.AxisX.Dot(vector2);
				double num3 = affineTransform.AxisX.Dot(vector3);
				double num4 = affineTransform.AxisY.Dot(vector);
				double num5 = affineTransform.AxisY.Dot(vector2);
				double num6 = affineTransform.AxisY.Dot(vector3);
				double num7 = 1.0 / (num * num5 - num2 * num4);
				double num8 = (num5 * num3 - num2 * num6) * num7;
				double num9 = (num * num6 - num4 * num3) * num7;
				double num10 = 1.0 - num8 - num9;
				if (num10 >= 0.0 && num8 >= 0.0 && num9 >= 0.0)
				{
					double num11 = direction.Dot(vector);
					double num12 = direction.Dot(vector2);
					double num13 = line.Direction.Dot(vector3);
					this.LineParameter = num8 * num11 + num9 * num12 - num13;
					this.TriangleBary0 = num10;
					this.TriangleBary1 = num8;
					this.TriangleBary2 = num9;
					this.ClosestPointOnLine = line.Origin + this.LineParameter * line.Direction;
					this.ClosestPointOnTriangle = triangle.V0 + num8 * vector + num9 * vector2;
					this.SquaredDistance = 0.0;
					return;
				}
			}
			Segment3 segment = new Segment3(triangle.V2, triangle.V0);
			DistanceLine3Segment3 distanceLine3Segment = new DistanceLine3Segment3(line, segment);
			this.ClosestPointOnLine = distanceLine3Segment.ClosestPointOnLine;
			this.ClosestPointOnTriangle = distanceLine3Segment.ClosestPointOnSegment;
			this.SquaredDistance = distanceLine3Segment.SquaredDistance;
			this.LineParameter = distanceLine3Segment.LineParameter;
			this.TriangleBary2 = 0.5 * (1.0 - distanceLine3Segment.SegmentParameter / segment.Extent);
			this.TriangleBary0 = 1.0 - this.TriangleBary2;
			this.TriangleBary1 = 0.0;
			Segment3 segment2 = new Segment3(triangle.V0, triangle.V1);
			DistanceLine3Segment3 distanceLine3Segment2 = new DistanceLine3Segment3(line, segment2);
			if (distanceLine3Segment2.SquaredDistance < this.SquaredDistance)
			{
				this.ClosestPointOnLine = distanceLine3Segment2.ClosestPointOnLine;
				this.ClosestPointOnTriangle = distanceLine3Segment2.ClosestPointOnSegment;
				this.SquaredDistance = distanceLine3Segment2.SquaredDistance;
				this.LineParameter = distanceLine3Segment2.LineParameter;
				this.TriangleBary0 = 0.5 * (1.0 - distanceLine3Segment2.SegmentParameter / segment2.Extent);
				this.TriangleBary1 = 1.0 - this.TriangleBary0;
				this.TriangleBary2 = 0.0;
			}
			Segment3 segment3 = new Segment3(triangle.V1, triangle.V2);
			DistanceLine3Segment3 distanceLine3Segment3 = new DistanceLine3Segment3(line, segment3);
			if (distanceLine3Segment3.SquaredDistance < this.SquaredDistance)
			{
				this.ClosestPointOnLine = distanceLine3Segment3.ClosestPointOnLine;
				this.ClosestPointOnTriangle = distanceLine3Segment3.ClosestPointOnSegment;
				this.SquaredDistance = distanceLine3Segment3.SquaredDistance;
				this.LineParameter = distanceLine3Segment3.LineParameter;
				this.TriangleBary1 = 0.5 * (1.0 - distanceLine3Segment3.SegmentParameter / segment3.Extent);
				this.TriangleBary2 = 1.0 - this.TriangleBary1;
				this.TriangleBary0 = 0.0;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005D14 File Offset: 0x00003F14
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00005D1C File Offset: 0x00003F1C
		public double SquaredDistance { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005D25 File Offset: 0x00003F25
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005D32 File Offset: 0x00003F32
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00005D3A File Offset: 0x00003F3A
		public double LineParameter { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005D43 File Offset: 0x00003F43
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00005D4B File Offset: 0x00003F4B
		public double TriangleBary0 { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00005D54 File Offset: 0x00003F54
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00005D5C File Offset: 0x00003F5C
		public double TriangleBary1 { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00005D65 File Offset: 0x00003F65
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00005D6D File Offset: 0x00003F6D
		public double TriangleBary2 { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00005D76 File Offset: 0x00003F76
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00005D7E File Offset: 0x00003F7E
		public Vector3 ClosestPointOnLine { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00005D87 File Offset: 0x00003F87
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00005D8F File Offset: 0x00003F8F
		public Vector3 ClosestPointOnTriangle { get; private set; }
	}
}
