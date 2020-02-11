using System;

namespace Hao.Geometry
{
	// Token: 0x02000054 RID: 84
	internal struct IntersectionLine2Box2
	{
		// Token: 0x06000349 RID: 841 RVA: 0x0000E23F File Offset: 0x0000C43F
		public IntersectionLine2Box2(Line2 line, Box2 box)
		{
			this = default(IntersectionLine2Box2);
			this.line = line;
			this.box = box;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000E256 File Offset: 0x0000C456
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0000E25E File Offset: 0x0000C45E
		public int Quantity { get; private set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000E267 File Offset: 0x0000C467
		// (set) Token: 0x0600034D RID: 845 RVA: 0x0000E26F File Offset: 0x0000C46F
		public Vector2 Point0 { get; private set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000E278 File Offset: 0x0000C478
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000E280 File Offset: 0x0000C480
		public Vector2 Point1 { get; private set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000E289 File Offset: 0x0000C489
		// (set) Token: 0x06000351 RID: 849 RVA: 0x0000E291 File Offset: 0x0000C491
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x06000352 RID: 850 RVA: 0x0000E29C File Offset: 0x0000C49C
		public bool Test()
		{
			Vector2 vector = this.line.Origin - this.box.Center;
			UnitVector2 unitVector = this.line.Direction.Perpendicular();
			double num = Math.Abs(unitVector.Dot(vector));
			double num2 = Math.Abs(unitVector.Dot(this.box.Axis0));
			double num3 = Math.Abs(unitVector.Dot(this.box.Axis1));
			double num4 = this.box.Extent0 * num2 + this.box.Extent1 * num3;
			return num < num4 + 1E-08;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000E360 File Offset: 0x0000C560
		public bool Find()
		{
			double minValue = double.MinValue;
			double maxValue = double.MaxValue;
			int quantity;
			Vector2 point;
			Vector2 point2;
			Intersection.Type intersectionType;
			bool result = Intersection.DoClipping(minValue, maxValue, this.line.Origin, this.line.Direction, this.box, true, out quantity, out point, out point2, out intersectionType);
			this.Quantity = quantity;
			this.Point0 = point;
			this.Point1 = point2;
			this.IntersectionType = intersectionType;
			return result;
		}

		// Token: 0x040000DE RID: 222
		private readonly Line2 line;

		// Token: 0x040000DF RID: 223
		private readonly Box2 box;
	}
}
