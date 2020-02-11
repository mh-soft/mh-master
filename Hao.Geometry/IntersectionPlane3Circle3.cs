using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x0200003E RID: 62
	internal struct IntersectionPlane3Circle3
	{
		// Token: 0x0600027D RID: 637 RVA: 0x0000A7AC File Offset: 0x000089AC
		public IntersectionPlane3Circle3(Plane3 plane, Circle3 circle)
		{
			this = default(IntersectionPlane3Circle3);
			this.plane = plane;
			this.circle = circle;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A7C4 File Offset: 0x000089C4
		public bool Test()
		{
			Plane3 plane = new Plane3(this.circle.Normal, this.circle.Center);
			IntersectionPlane3Plane3 intersectionPlane3Plane = new IntersectionPlane3Plane3(this.plane, plane);
			if (!intersectionPlane3Plane.Find())
			{
				return false;
			}
			if (intersectionPlane3Plane.IntersectionType == Intersection.Type.IT_PLANE)
			{
				return true;
			}
			Line3 intersectionLine = intersectionPlane3Plane.IntersectionLine;
			Vector3 vector = intersectionLine.Origin - this.circle.Center;
			double num = vector.Dot(intersectionLine.Direction);
			double num2 = vector.SquaredLength - this.circle.Radius * this.circle.Radius;
			return num * num - num2 * 1.0 >= 0.0;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A894 File Offset: 0x00008A94
		public ICollection<Vector3> Find()
		{
			Plane3 plane = new Plane3(this.circle.Normal, this.circle.Center);
			IntersectionPlane3Plane3 intersectionPlane3Plane = new IntersectionPlane3Plane3(this.plane, plane);
			if (!intersectionPlane3Plane.Find())
			{
				return new Vector3[0];
			}
			if (intersectionPlane3Plane.IntersectionType == Intersection.Type.IT_PLANE)
			{
				return new Vector3[0];
			}
			Line3 intersectionLine = intersectionPlane3Plane.IntersectionLine;
			Vector3 vector = intersectionLine.Origin - this.circle.Center;
			double num = vector.Dot(intersectionLine.Direction);
			double num2 = vector.SquaredLength - this.circle.Radius * this.circle.Radius;
			double num3 = num;
			double num4 = num3 * num3 - num2 * 1.0;
			if (num4 < 0.0)
			{
				return new Vector3[0];
			}
			double num5 = 1.0;
			if (num4 < 1E-08)
			{
				return new Vector3[]
				{
					intersectionLine.Origin - num * num5 * intersectionLine.Direction
				};
			}
			double num6 = Math.Sqrt(num4);
			return new Vector3[]
			{
				intersectionLine.Origin - (num + num6) * num5 * intersectionLine.Direction,
				intersectionLine.Origin - (num - num6) * num5 * intersectionLine.Direction
			};
		}

		// Token: 0x0400008C RID: 140
		private readonly Plane3 plane;

		// Token: 0x0400008D RID: 141
		private readonly Circle3 circle;
	}
}
