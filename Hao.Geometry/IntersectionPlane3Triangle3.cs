using System;

namespace Hao.Geometry
{
	// Token: 0x0200005E RID: 94
	internal struct IntersectionPlane3Triangle3
	{
		// Token: 0x060003AE RID: 942 RVA: 0x000101AA File Offset: 0x0000E3AA
		public IntersectionPlane3Triangle3(Plane3 plane, Triangle3 triangle, double epsilon)
		{
			this = default(IntersectionPlane3Triangle3);
			this.plane = plane;
			this.triangle = triangle;
			if (epsilon >= 0.0)
			{
				this.epsilon = epsilon;
				return;
			}
			this.epsilon = 0.0;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000101E4 File Offset: 0x0000E3E4
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x000101EC File Offset: 0x0000E3EC
		public int Quantity { get; private set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x000101F5 File Offset: 0x0000E3F5
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x000101FD File Offset: 0x0000E3FD
		public Vector3 Point0 { get; private set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00010206 File Offset: 0x0000E406
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0001020E File Offset: 0x0000E40E
		public Vector3 Point1 { get; private set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00010217 File Offset: 0x0000E417
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0001021F File Offset: 0x0000E41F
		public Vector3 Point2 { get; private set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00010228 File Offset: 0x0000E428
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x00010230 File Offset: 0x0000E430
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x060003B9 RID: 953 RVA: 0x0001023C File Offset: 0x0000E43C
		public bool Test()
		{
			double num = this.plane.Normal.Dot(this.triangle.V0) - this.plane.Constant;
			if (Math.Abs(num) <= this.epsilon)
			{
				num = 0.0;
			}
			double num2 = this.plane.Normal.Dot(this.triangle.V1) - this.plane.Constant;
			if (Math.Abs(num2) <= this.epsilon)
			{
				num2 = 0.0;
			}
			double num3 = this.plane.Normal.Dot(this.triangle.V2) - this.plane.Constant;
			if (Math.Abs(num3) <= this.epsilon)
			{
				num3 = 0.0;
			}
			return (num <= 0.0 || num2 <= 0.0 || num3 <= 0.0) && (num >= 0.0 || num2 >= 0.0 || num3 >= 0.0);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00010384 File Offset: 0x0000E584
		public bool Find()
		{
			double num = this.plane.Normal.Dot(this.triangle.V0) - this.plane.Constant;
			if (Math.Abs(num) <= this.epsilon)
			{
				num = 0.0;
			}
			double num2 = this.plane.Normal.Dot(this.triangle.V1) - this.plane.Constant;
			if (Math.Abs(num2) <= this.epsilon)
			{
				num2 = 0.0;
			}
			double num3 = this.plane.Normal.Dot(this.triangle.V2) - this.plane.Constant;
			if (Math.Abs(num3) <= this.epsilon)
			{
				num3 = 0.0;
			}
			Vector3 v = this.triangle.V0;
			Vector3 v2 = this.triangle.V1;
			Vector3 v3 = this.triangle.V2;
			if (num > 0.0)
			{
				if (num2 > 0.0)
				{
					if (num3 > 0.0)
					{
						this.Quantity = 0;
						this.IntersectionType = Intersection.Type.IT_EMPTY;
					}
					else if (num3 < 0.0)
					{
						this.Quantity = 2;
						Vector3 left = v;
						double num4 = num;
						this.Point0 = left + num4 / (num4 - num3) * (v3 - v);
						Vector3 left2 = v2;
						double num5 = num2;
						this.Point1 = left2 + num5 / (num5 - num3) * (v3 - v2);
						this.IntersectionType = Intersection.Type.IT_SEGMENT;
					}
					else
					{
						this.Quantity = 1;
						this.Point0 = v3;
						this.IntersectionType = Intersection.Type.IT_POINT;
					}
				}
				else if (num2 < 0.0)
				{
					if (num3 > 0.0)
					{
						this.Quantity = 2;
						Vector3 left3 = v;
						double num6 = num;
						this.Point0 = left3 + num6 / (num6 - num2) * (v2 - v);
						Vector3 left4 = v2;
						double num7 = num2;
						this.Point1 = left4 + num7 / (num7 - num3) * (v3 - v2);
						this.IntersectionType = Intersection.Type.IT_SEGMENT;
					}
					else if (num3 < 0.0)
					{
						this.Quantity = 2;
						Vector3 left5 = v;
						double num8 = num;
						this.Point0 = left5 + num8 / (num8 - num2) * (v2 - v);
						Vector3 left6 = v;
						double num9 = num;
						this.Point1 = left6 + num9 / (num9 - num3) * (v3 - v);
						this.IntersectionType = Intersection.Type.IT_SEGMENT;
					}
					else
					{
						this.Quantity = 2;
						Vector3 left7 = v;
						double num10 = num;
						this.Point0 = left7 + num10 / (num10 - num2) * (v2 - v);
						this.Point1 = v3;
						this.IntersectionType = Intersection.Type.IT_SEGMENT;
					}
				}
				else if (num3 > 0.0)
				{
					this.Quantity = 1;
					this.Point0 = v2;
					this.IntersectionType = Intersection.Type.IT_POINT;
				}
				else if (num3 < 0.0)
				{
					this.Quantity = 2;
					Vector3 left8 = v;
					double num11 = num;
					this.Point0 = left8 + num11 / (num11 - num3) * (v3 - v);
					this.Point1 = v2;
					this.IntersectionType = Intersection.Type.IT_SEGMENT;
				}
				else
				{
					this.Quantity = 2;
					this.Point0 = v2;
					this.Point1 = v3;
					this.IntersectionType = Intersection.Type.IT_SEGMENT;
				}
			}
			else if (num < 0.0)
			{
				if (num2 > 0.0)
				{
					if (num3 > 0.0)
					{
						this.Quantity = 2;
						Vector3 left9 = v;
						double num12 = num;
						this.Point0 = left9 + num12 / (num12 - num2) * (v2 - v);
						Vector3 left10 = v;
						double num13 = num;
						this.Point1 = left10 + num13 / (num13 - num3) * (v3 - v);
						this.IntersectionType = Intersection.Type.IT_SEGMENT;
					}
					else if (num3 < 0.0)
					{
						this.Quantity = 2;
						Vector3 left11 = v;
						double num14 = num;
						this.Point0 = left11 + num14 / (num14 - num2) * (v2 - v);
						Vector3 left12 = v2;
						double num15 = num2;
						this.Point1 = left12 + num15 / (num15 - num3) * (v3 - v2);
						this.IntersectionType = Intersection.Type.IT_SEGMENT;
					}
					else
					{
						this.Quantity = 2;
						Vector3 left13 = v;
						double num16 = num;
						this.Point0 = left13 + num16 / (num16 - num2) * (v2 - v);
						this.Point1 = v3;
						this.IntersectionType = Intersection.Type.IT_SEGMENT;
					}
				}
				else if (num2 < 0.0)
				{
					if (num3 > 0.0)
					{
						this.Quantity = 2;
						Vector3 left14 = v;
						double num17 = num;
						this.Point0 = left14 + num17 / (num17 - num3) * (v3 - v);
						Vector3 left15 = v2;
						double num18 = num2;
						this.Point1 = left15 + num18 / (num18 - num3) * (v3 - v2);
						this.IntersectionType = Intersection.Type.IT_SEGMENT;
					}
					else if (num3 < 0.0)
					{
						this.Quantity = 0;
						this.IntersectionType = Intersection.Type.IT_EMPTY;
					}
					else
					{
						this.Quantity = 1;
						this.Point0 = this.triangle.V2;
						this.IntersectionType = Intersection.Type.IT_POINT;
					}
				}
				else if (num3 > 0.0)
				{
					this.Quantity = 2;
					Vector3 left16 = v;
					double num19 = num;
					this.Point0 = left16 + num19 / (num19 - num3) * (v3 - v);
					this.Point1 = v2;
					this.IntersectionType = Intersection.Type.IT_SEGMENT;
				}
				else if (num3 < 0.0)
				{
					this.Quantity = 1;
					this.Point0 = v2;
					this.IntersectionType = Intersection.Type.IT_POINT;
				}
				else
				{
					this.Quantity = 2;
					this.Point0 = v2;
					this.Point1 = v3;
					this.IntersectionType = Intersection.Type.IT_SEGMENT;
				}
			}
			else if (num2 > 0.0)
			{
				if (num3 > 0.0)
				{
					this.Quantity = 1;
					this.Point0 = v;
					this.IntersectionType = Intersection.Type.IT_POINT;
				}
				else if (num3 < 0.0)
				{
					this.Quantity = 2;
					Vector3 left17 = v2;
					double num20 = num2;
					this.Point0 = left17 + num20 / (num20 - num3) * (v3 - v2);
					this.Point1 = v;
					this.IntersectionType = Intersection.Type.IT_SEGMENT;
				}
				else
				{
					this.Quantity = 2;
					this.Point0 = v;
					this.Point1 = v3;
					this.IntersectionType = Intersection.Type.IT_SEGMENT;
				}
			}
			else if (num2 < 0.0)
			{
				if (num3 > 0.0)
				{
					this.Quantity = 2;
					Vector3 left18 = v2;
					double num21 = num2;
					this.Point0 = left18 + num21 / (num21 - num3) * (v3 - v2);
					this.Point1 = v;
					this.IntersectionType = Intersection.Type.IT_SEGMENT;
				}
				else if (num3 < 0.0)
				{
					this.Quantity = 1;
					this.Point0 = v;
					this.IntersectionType = Intersection.Type.IT_POINT;
				}
				else
				{
					this.Quantity = 2;
					this.Point0 = v;
					this.Point1 = v3;
					this.IntersectionType = Intersection.Type.IT_SEGMENT;
				}
			}
			else if (num3 > 0.0)
			{
				this.Quantity = 2;
				this.Point0 = v;
				this.Point1 = v2;
				this.IntersectionType = Intersection.Type.IT_SEGMENT;
			}
			else if (num3 < 0.0)
			{
				this.Quantity = 2;
				this.Point0 = v;
				this.Point1 = v2;
				this.IntersectionType = Intersection.Type.IT_SEGMENT;
			}
			else
			{
				this.Quantity = 3;
				this.Point0 = v;
				this.Point1 = v2;
				this.Point2 = v3;
				this.IntersectionType = Intersection.Type.IT_POLYGON;
			}
			return this.IntersectionType > Intersection.Type.IT_EMPTY;
		}

		// Token: 0x04000103 RID: 259
		private readonly Plane3 plane;

		// Token: 0x04000104 RID: 260
		private readonly Triangle3 triangle;

		// Token: 0x04000105 RID: 261
		private readonly double epsilon;
	}
}
