using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x0200006F RID: 111
	public static class IntersectionTriangle3
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x000137CC File Offset: 0x000119CC
		public static bool Intersects(this Triangle3 triangle, Box3 box)
		{
			IntersectionTriangle3Box3 intersectionTriangle3Box = new IntersectionTriangle3Box3(triangle, box);
			return intersectionTriangle3Box.Test();
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000137EC File Offset: 0x000119EC
		public static ICollection<Vector3> IntersectionPointsWith(this Triangle3 triangle, Box3 box)
		{
			IntersectionTriangle3Box3 intersectionTriangle3Box = new IntersectionTriangle3Box3(triangle, box);
			intersectionTriangle3Box.Find();
			List<Vector3> list = new List<Vector3>();
			for (int i = 0; i < intersectionTriangle3Box.Quantity; i++)
			{
				list.Add(intersectionTriangle3Box.Points[i]);
			}
			return list;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00013838 File Offset: 0x00011A38
		public static bool Intersects(this Triangle3 triangle, AxisAlignedBox3 axisAlignedBox)
		{
			Box3 box = new Box3(axisAlignedBox.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY, axisAlignedBox.ExtentZ);
			IntersectionTriangle3Box3 intersectionTriangle3Box = new IntersectionTriangle3Box3(triangle, box);
			return intersectionTriangle3Box.Test();
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00013888 File Offset: 0x00011A88
		public static ICollection<Vector3> IntersectionPointsWith(this Triangle3 triangle, AxisAlignedBox3 axisAlignedBox)
		{
			Box3 box = new Box3(axisAlignedBox.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY, axisAlignedBox.ExtentZ);
			IntersectionTriangle3Box3 intersectionTriangle3Box = new IntersectionTriangle3Box3(triangle, box);
			intersectionTriangle3Box.Find();
			List<Vector3> list = new List<Vector3>();
			for (int i = 0; i < intersectionTriangle3Box.Quantity; i++)
			{
				list.Add(intersectionTriangle3Box.Points[i]);
			}
			return list;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00013904 File Offset: 0x00011B04
		public static bool Intersects(this Triangle3 triangle, Triangle3 other)
		{
			IntersectionTriangle3Triangle3 intersectionTriangle3Triangle = new IntersectionTriangle3Triangle3(triangle, other);
			return intersectionTriangle3Triangle.Test();
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00013924 File Offset: 0x00011B24
		public static ICollection<Vector3> IntersectionPointsWith(this Triangle3 triangle, Triangle3 other)
		{
			IntersectionTriangle3Triangle3 intersectionTriangle3Triangle = new IntersectionTriangle3Triangle3(triangle, other);
			intersectionTriangle3Triangle.Find();
			List<Vector3> list = new List<Vector3>();
			for (int i = 0; i < intersectionTriangle3Triangle.Quantity; i++)
			{
				list.Add(intersectionTriangle3Triangle.Points[i]);
			}
			return list;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001396E File Offset: 0x00011B6E
		public static bool Intersects(this Triangle3 triangle, Line3 line)
		{
			return line.Intersects(triangle);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00013977 File Offset: 0x00011B77
		public static Vector3? IntersectionWith(this Triangle3 triangle, Line3 line)
		{
			return line.IntersectionWith(triangle);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00013980 File Offset: 0x00011B80
		public static bool Intersects(this Triangle3 triangle, Plane3 plane)
		{
			return plane.Intersects(triangle);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00013989 File Offset: 0x00011B89
		public static Segment3? IntersectionWith(this Triangle3 triangle, Plane3 plane)
		{
			return plane.IntersectionWith(triangle);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00013992 File Offset: 0x00011B92
		public static ICollection<Vector3> IntersectionPointsWith(this Triangle3 triangle, Plane3 plane)
		{
			return plane.IntersectionPointsWith(triangle);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001399B File Offset: 0x00011B9B
		public static bool Intersects(this Triangle3 triangle, Ray3 ray)
		{
			return ray.Intersects(triangle);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000139A4 File Offset: 0x00011BA4
		public static Vector3? IntersectionWith(this Triangle3 triangle, Ray3 ray)
		{
			return ray.IntersectionWith(triangle);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000139AD File Offset: 0x00011BAD
		public static bool Intersects(this Triangle3 triangle, Segment3 segment)
		{
			return segment.Intersects(triangle);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x000139B6 File Offset: 0x00011BB6
		public static Vector3? IntersectionWith(this Triangle3 triangle, Segment3 segment)
		{
			return segment.IntersectionWith(triangle);
		}
	}
}
