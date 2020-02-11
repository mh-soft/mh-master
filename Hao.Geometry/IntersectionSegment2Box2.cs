using System;

namespace Hao.Geometry
{
	// Token: 0x02000066 RID: 102
	internal struct IntersectionSegment2Box2
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x00011C87 File Offset: 0x0000FE87
		public IntersectionSegment2Box2(Segment2 segment, Box2 box, bool solid)
		{
			this = default(IntersectionSegment2Box2);
			this.segment = segment;
			this.box = box;
			this.solid = solid;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00011CA5 File Offset: 0x0000FEA5
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00011CAD File Offset: 0x0000FEAD
		public int Quantity { get; private set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00011CB6 File Offset: 0x0000FEB6
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00011CBE File Offset: 0x0000FEBE
		public Vector2 Point0 { get; private set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00011CC7 File Offset: 0x0000FEC7
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00011CCF File Offset: 0x0000FECF
		public Vector2 Point1 { get; private set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00011CD8 File Offset: 0x0000FED8
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00011CE0 File Offset: 0x0000FEE0
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x060003FC RID: 1020 RVA: 0x00011CEC File Offset: 0x0000FEEC
		public bool Test()
		{
			Vector2 vector = this.segment.Origin - this.box.Center;
			double[] array = new double[2];
			double[] array2 = new double[2];
			array[0] = Math.Abs(this.segment.Direction.Dot(this.box.Axis0));
			array2[0] = Math.Abs(vector.Dot(this.box.Axis0));
			double num = this.box.Extent0 + this.segment.Extent * array[0];
			if (array2[0] > num + 1E-08)
			{
				return false;
			}
			array[1] = Math.Abs(this.segment.Direction.Dot(this.box.Axis1));
			array2[1] = Math.Abs(vector.Dot(this.box.Axis1));
			num = this.box.Extent1 + this.segment.Extent * array[1];
			if (array2[1] > num + 1E-08)
			{
				return false;
			}
			UnitVector2 unitVector = this.segment.Direction.Perpendicular();
			double num2 = Math.Abs(unitVector.Dot(vector));
			double num3 = Math.Abs(unitVector.Dot(this.box.Axis0));
			double num4 = Math.Abs(unitVector.Dot(this.box.Axis1));
			num = this.box.Extent0 * num3 + this.box.Extent1 * num4;
			return num2 < num + 1E-08;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00011EC8 File Offset: 0x000100C8
		public bool Find()
		{
			double t = -this.segment.Extent;
			double extent = this.segment.Extent;
			int quantity;
			Vector2 point;
			Vector2 point2;
			Intersection.Type intersectionType;
			bool result = Intersection.DoClipping(t, extent, this.segment.Origin, this.segment.Direction, this.box, this.solid, out quantity, out point, out point2, out intersectionType);
			this.Quantity = quantity;
			this.Point0 = point;
			this.Point1 = point2;
			this.IntersectionType = intersectionType;
			return result;
		}

		// Token: 0x0400011D RID: 285
		private readonly Segment2 segment;

		// Token: 0x0400011E RID: 286
		private readonly Box2 box;

		// Token: 0x0400011F RID: 287
		private readonly bool solid;
	}
}
