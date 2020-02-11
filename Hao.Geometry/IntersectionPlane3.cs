using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x0200005B RID: 91
	public static class IntersectionPlane3
	{
		// Token: 0x0600038F RID: 911 RVA: 0x0000FA48 File Offset: 0x0000DC48
		public static bool Intersects(this Plane3 plane, Circle3 circle)
		{
			IntersectionPlane3Circle3 intersectionPlane3Circle = new IntersectionPlane3Circle3(plane, circle);
			return intersectionPlane3Circle.Test();
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000FA68 File Offset: 0x0000DC68
		public static ICollection<Vector3> IntersectionPointsWith(this Plane3 plane, Circle3 circle)
		{
			IntersectionPlane3Circle3 intersectionPlane3Circle = new IntersectionPlane3Circle3(plane, circle);
			return new List<Vector3>(intersectionPlane3Circle.Find());
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000FA8C File Offset: 0x0000DC8C
		public static bool Intersects(this Plane3 plane, Arc3 arc)
		{
			IntersectionPlane3Arc3 intersectionPlane3Arc = new IntersectionPlane3Arc3(plane, arc);
			return intersectionPlane3Arc.Test();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000FAAC File Offset: 0x0000DCAC
		public static ICollection<Vector3> IntersectionPointsWith(this Plane3 plane, Arc3 arc)
		{
			IntersectionPlane3Arc3 intersectionPlane3Arc = new IntersectionPlane3Arc3(plane, arc);
			return intersectionPlane3Arc.Find();
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000FACC File Offset: 0x0000DCCC
		public static bool Intersects(this Plane3 plane, Box3 box)
		{
			IntersectionPlane3Box3 intersectionPlane3Box = new IntersectionPlane3Box3(plane, box);
			return intersectionPlane3Box.Test();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000FAEC File Offset: 0x0000DCEC
		public static PlaneSideClassification SideClassification(this Plane3 plane, Box3 box)
		{
			IntersectionPlane3Box3 intersectionPlane3Box = new IntersectionPlane3Box3(plane, box);
			return intersectionPlane3Box.SideClassification();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000FB0C File Offset: 0x0000DD0C
		public static bool Intersects(this Plane3 plane, AxisAlignedBox3 axisAlignedBox)
		{
			Box3 box = new Box3(axisAlignedBox.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY, axisAlignedBox.ExtentZ);
			IntersectionPlane3Box3 intersectionPlane3Box = new IntersectionPlane3Box3(plane, box);
			return intersectionPlane3Box.Test();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000FB5C File Offset: 0x0000DD5C
		public static PlaneSideClassification SideClassification(this Plane3 plane, AxisAlignedBox3 axisAlignedBox)
		{
			Box3 box = new Box3(axisAlignedBox.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY, axisAlignedBox.ExtentZ);
			IntersectionPlane3Box3 intersectionPlane3Box = new IntersectionPlane3Box3(plane, box);
			return intersectionPlane3Box.SideClassification();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000FBAC File Offset: 0x0000DDAC
		public static bool Intersects(this Plane3 plane, Plane3 other)
		{
			IntersectionPlane3Plane3 intersectionPlane3Plane = new IntersectionPlane3Plane3(plane, other);
			return intersectionPlane3Plane.Test();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000FBCC File Offset: 0x0000DDCC
		public static Line3? IntersectionWith(this Plane3 plane, Plane3 other)
		{
			IntersectionPlane3Plane3 intersectionPlane3Plane = new IntersectionPlane3Plane3(plane, other);
			if (intersectionPlane3Plane.Find() && intersectionPlane3Plane.IntersectionType == Intersection.Type.IT_LINE)
			{
				return new Line3?(intersectionPlane3Plane.IntersectionLine);
			}
			return null;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000FC0C File Offset: 0x0000DE0C
		public static bool Intersects(this Plane3 plane, Triangle3 triangle)
		{
			IntersectionPlane3Triangle3 intersectionPlane3Triangle = new IntersectionPlane3Triangle3(plane, triangle, 0.0);
			return intersectionPlane3Triangle.Test();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000FC34 File Offset: 0x0000DE34
		public static Segment3? IntersectionWith(this Plane3 plane, Triangle3 triangle)
		{
			IntersectionPlane3Triangle3 intersectionPlane3Triangle = new IntersectionPlane3Triangle3(plane, triangle, 0.0);
			bool flag = intersectionPlane3Triangle.Find();
			if (flag && intersectionPlane3Triangle.IntersectionType == Intersection.Type.IT_SEGMENT)
			{
				return new Segment3?(new Segment3(intersectionPlane3Triangle.Point0, intersectionPlane3Triangle.Point1));
			}
			if (flag && intersectionPlane3Triangle.IntersectionType == Intersection.Type.IT_POINT)
			{
				return new Segment3?(new Segment3(intersectionPlane3Triangle.Point0, intersectionPlane3Triangle.Point0));
			}
			return null;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000FCB0 File Offset: 0x0000DEB0
		public static ICollection<Vector3> IntersectionPointsWith(this Plane3 plane, Triangle3 triangle)
		{
			IntersectionPlane3Triangle3 intersectionPlane3Triangle = new IntersectionPlane3Triangle3(plane, triangle, 0.0);
			intersectionPlane3Triangle.Find();
			List<Vector3> list = new List<Vector3>();
			Vector3[] array = new Vector3[]
			{
				intersectionPlane3Triangle.Point0,
				intersectionPlane3Triangle.Point1,
				intersectionPlane3Triangle.Point2
			};
			for (int i = 0; i < intersectionPlane3Triangle.Quantity; i++)
			{
				list.Add(array[i]);
			}
			return list;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000FD2E File Offset: 0x0000DF2E
		public static bool Intersects(this Plane3 plane, Line3 line)
		{
			return line.Intersects(plane);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000FD37 File Offset: 0x0000DF37
		public static Vector3? IntersectionWith(this Plane3 plane, Line3 line)
		{
			return line.IntersectionWith(plane);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000FD40 File Offset: 0x0000DF40
		public static bool Intersects(this Plane3 plane, Ray3 ray)
		{
			return ray.Intersects(plane);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000FD49 File Offset: 0x0000DF49
		public static Vector3? IntersectionWith(this Plane3 plane, Ray3 ray)
		{
			return ray.IntersectionWith(plane);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000FD52 File Offset: 0x0000DF52
		public static bool Intersects(this Plane3 plane, Segment3 segment)
		{
			return segment.Intersects(plane);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000FD5B File Offset: 0x0000DF5B
		public static Vector3? IntersectionWith(this Plane3 plane, Segment3 segment)
		{
			return segment.IntersectionWith(plane);
		}
	}
}
