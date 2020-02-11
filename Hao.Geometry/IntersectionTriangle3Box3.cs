using System;

namespace Hao.Geometry
{
	// Token: 0x02000070 RID: 112
	internal struct IntersectionTriangle3Box3
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x000139BF File Offset: 0x00011BBF
		public IntersectionTriangle3Box3(Triangle3 triangle, Box3 box)
		{
			this = default(IntersectionTriangle3Box3);
			this.Points = new Vector3[7];
			this.triangle = triangle;
			this.box = box;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x000139E2 File Offset: 0x00011BE2
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x000139EA File Offset: 0x00011BEA
		public int Quantity { get; private set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x000139F3 File Offset: 0x00011BF3
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x000139FB File Offset: 0x00011BFB
		public Vector3[] Points { get; private set; }

		// Token: 0x06000459 RID: 1113 RVA: 0x00013A04 File Offset: 0x00011C04
		public bool Test()
		{
			Vector3[] array = new Vector3[3];
			array[0] = this.triangle.V1 - this.triangle.V0;
			array[1] = this.triangle.V2 - this.triangle.V0;
			Vector3 axis = array[0].Cross(array[1]);
			double num = axis.Dot(this.triangle.V0);
			double num2 = num;
			double num3;
			double num4;
			IntersectionUtility3.GetProjection(axis, this.box, out num3, out num4);
			if (num4 < num || num2 < num3)
			{
				return false;
			}
			UnitVector3 unitVector = this.box.Axis0;
			IntersectionUtility3.GetProjection((Vector3)unitVector, this.triangle, out num, out num2);
			double num5 = unitVector.Dot(this.box.Center);
			num3 = num5 - this.box.Extent0;
			num4 = num5 + this.box.Extent0;
			if (num4 < num || num2 < num3)
			{
				return false;
			}
			unitVector = this.box.Axis1;
			IntersectionUtility3.GetProjection((Vector3)unitVector, this.triangle, out num, out num2);
			double num6 = unitVector.Dot(this.box.Center);
			num3 = num6 - this.box.Extent1;
			num4 = num6 + this.box.Extent1;
			if (num4 < num || num2 < num3)
			{
				return false;
			}
			unitVector = this.box.Axis2;
			IntersectionUtility3.GetProjection((Vector3)unitVector, this.triangle, out num, out num2);
			double num7 = unitVector.Dot(this.box.Center);
			num3 = num7 - this.box.Extent2;
			num4 = num7 + this.box.Extent2;
			if (num4 < num || num2 < num3)
			{
				return false;
			}
			array[2] = array[1] - array[0];
			for (int i = 0; i < 3; i++)
			{
				Vector3 axis2 = array[i].Cross(this.box.Axis0);
				IntersectionUtility3.GetProjection(axis2, this.triangle, out num, out num2);
				IntersectionUtility3.GetProjection(axis2, this.box, out num3, out num4);
				if (num4 < num || num2 < num3)
				{
					return false;
				}
				Vector3 axis3 = array[i].Cross(this.box.Axis1);
				IntersectionUtility3.GetProjection(axis3, this.triangle, out num, out num2);
				IntersectionUtility3.GetProjection(axis3, this.box, out num3, out num4);
				if (num4 < num || num2 < num3)
				{
					return false;
				}
				Vector3 axis4 = array[i].Cross(this.box.Axis2);
				IntersectionUtility3.GetProjection(axis4, this.triangle, out num, out num2);
				IntersectionUtility3.GetProjection(axis4, this.box, out num3, out num4);
				if (num4 < num || num2 < num3)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00013D00 File Offset: 0x00011F00
		public bool Find()
		{
			this.Quantity = 3;
			for (int i = 0; i < 3; i++)
			{
				this.Points[i] = this.triangle[i];
			}
			for (int j = -1; j <= 1; j += 2)
			{
				Vector3 normal = (double)j * this.box.Axis0;
				double constant = normal.Dot(this.box.Center) - this.box.Extent0;
				int quantity = this.Quantity;
				Vector3[] points = this.Points;
				IntersectionUtility3.ClipConvexPolygonAgainstPlane(normal, constant, ref quantity, ref points);
				this.Quantity = quantity;
				this.Points = points;
				normal = (double)j * this.box.Axis1;
				constant = normal.Dot(this.box.Center) - this.box.Extent1;
				quantity = this.Quantity;
				points = this.Points;
				IntersectionUtility3.ClipConvexPolygonAgainstPlane(normal, constant, ref quantity, ref points);
				this.Quantity = quantity;
				this.Points = points;
				normal = (double)j * this.box.Axis2;
				constant = normal.Dot(this.box.Center) - this.box.Extent2;
				quantity = this.Quantity;
				points = this.Points;
				IntersectionUtility3.ClipConvexPolygonAgainstPlane(normal, constant, ref quantity, ref points);
				this.Quantity = quantity;
				this.Points = points;
			}
			return this.Quantity > 0;
		}

		// Token: 0x04000141 RID: 321
		private readonly Triangle3 triangle;

		// Token: 0x04000142 RID: 322
		private readonly Box3 box;
	}
}
