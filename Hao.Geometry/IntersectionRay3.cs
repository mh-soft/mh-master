using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x0200005F RID: 95
	public static class IntersectionRay3
	{
		// Token: 0x060003BB RID: 955 RVA: 0x00010B04 File Offset: 0x0000ED04
		public static bool Intersects(this Ray3 ray, Cylinder3 cylinder)
		{
			IntersectionRay3Cylinder3 intersectionRay3Cylinder = new IntersectionRay3Cylinder3(ray, cylinder);
			return intersectionRay3Cylinder.Find();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00010B24 File Offset: 0x0000ED24
		public static Segment3? IntersectionWith(this Ray3 ray, Cylinder3 cylinder)
		{
			IntersectionRay3Cylinder3 intersectionRay3Cylinder = new IntersectionRay3Cylinder3(ray, cylinder);
			intersectionRay3Cylinder.Find();
			if (intersectionRay3Cylinder.IntersectionType == Intersection.Type.IT_SEGMENT)
			{
				return new Segment3?(new Segment3(intersectionRay3Cylinder.Point0, intersectionRay3Cylinder.Point1));
			}
			if (intersectionRay3Cylinder.IntersectionType == Intersection.Type.IT_POINT)
			{
				return new Segment3?(new Segment3(intersectionRay3Cylinder.Point0, intersectionRay3Cylinder.Point0));
			}
			return null;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00010B94 File Offset: 0x0000ED94
		public static ICollection<Vector3> IntersectionPointsWith(this Ray3 ray, Cylinder3 cylinder)
		{
			Segment3? segment = ray.IntersectionWith(cylinder);
			if (segment == null)
			{
				return new List<Vector3>();
			}
			Segment3 value = segment.Value;
			if (value.Extent < 1E-08)
			{
				return new List<Vector3>
				{
					value.Origin
				};
			}
			return new List<Vector3>
			{
				value.NegativeEnd,
				value.PositiveEnd
			};
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00010C04 File Offset: 0x0000EE04
		public static bool Intersects(this Ray3 ray, Torus3 torus)
		{
			IntersectionRay3Torus3 intersectionRay3Torus = new IntersectionRay3Torus3(ray, torus);
			return intersectionRay3Torus.Find();
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00010C24 File Offset: 0x0000EE24
		public static ICollection<Vector3> IntersectionPointsWith(this Ray3 ray, Torus3 torus)
		{
			List<Vector3> list = new List<Vector3>();
			IntersectionRay3Torus3 intersectionRay3Torus = new IntersectionRay3Torus3(ray, torus);
			if (intersectionRay3Torus.Find())
			{
				Vector3[] array = new Vector3[]
				{
					intersectionRay3Torus.Point0,
					intersectionRay3Torus.Point1,
					intersectionRay3Torus.Point2,
					intersectionRay3Torus.Point3
				};
				for (int i = 0; i < intersectionRay3Torus.Quantity; i++)
				{
					list.Add(array[i]);
				}
			}
			return list;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00010CA8 File Offset: 0x0000EEA8
		public static bool Intersects(this Ray3 ray, Box3 box)
		{
			IntersectionRay3Box3 intersectionRay3Box = new IntersectionRay3Box3(ray, box);
			return intersectionRay3Box.Test();
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00010CC5 File Offset: 0x0000EEC5
		public static Segment3? IntersectionWith(this Ray3 ray, Box3 box)
		{
			return Intersection.DoClipping(0.0, double.MaxValue, ray.Origin, ray.Direction, box);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00010CF0 File Offset: 0x0000EEF0
		public static ICollection<Vector3> IntersectionPointsWith(this Ray3 ray, Box3 box)
		{
			Segment3? segment = ray.IntersectionWith(box);
			if (segment == null)
			{
				return new List<Vector3>();
			}
			Segment3 value = segment.Value;
			if (value.Extent < 1E-08)
			{
				return new List<Vector3>
				{
					value.Origin
				};
			}
			return new List<Vector3>
			{
				value.NegativeEnd,
				value.PositiveEnd
			};
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00010D60 File Offset: 0x0000EF60
		public static bool Intersects(this Ray3 ray, Plane3 plane)
		{
			IntersectionRay3Plane3 intersectionRay3Plane = new IntersectionRay3Plane3(ray, plane);
			return intersectionRay3Plane.Test();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00010D80 File Offset: 0x0000EF80
		public static Vector3? IntersectionWith(this Ray3 ray, Plane3 plane)
		{
			IntersectionRay3Plane3 intersectionRay3Plane = new IntersectionRay3Plane3(ray, plane);
			if (intersectionRay3Plane.Find() && intersectionRay3Plane.IntersectionType != Intersection.Type.IT_EMPTY)
			{
				return new Vector3?(ray.Origin + intersectionRay3Plane.RayParameter * ray.Direction);
			}
			return null;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00010DD8 File Offset: 0x0000EFD8
		public static bool Intersects(this Ray3 ray, Triangle3 triangle)
		{
			IntersectionRay3Triangle3 intersectionRay3Triangle = new IntersectionRay3Triangle3(ray, triangle);
			return intersectionRay3Triangle.Find();
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00010DF8 File Offset: 0x0000EFF8
		public static Vector3? IntersectionWith(this Ray3 ray, Triangle3 triangle)
		{
			IntersectionRay3Triangle3 intersectionRay3Triangle = new IntersectionRay3Triangle3(ray, triangle);
			if (intersectionRay3Triangle.Find() && intersectionRay3Triangle.IntersectionType != Intersection.Type.IT_EMPTY)
			{
				return new Vector3?(ray.Origin + intersectionRay3Triangle.RayParameter * ray.Direction);
			}
			return null;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00010E4E File Offset: 0x0000F04E
		public static bool Intersects(this Ray3 ray, AxisAlignedBox3 axisAlignedBox)
		{
			return IntersectionRay3AxisAlignedBox3.Test(ray, axisAlignedBox);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00010E58 File Offset: 0x0000F058
		public static Segment3? IntersectionWith(this Ray3 ray, AxisAlignedBox3 axisAlignedBox)
		{
			Box3 box = new Box3(axisAlignedBox.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY, axisAlignedBox.ExtentZ);
			return Intersection.DoClipping(0.0, double.MaxValue, ray.Origin, ray.Direction, box);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00010EC0 File Offset: 0x0000F0C0
		public static ICollection<Vector3> IntersectionPointsWith(this Ray3 ray, AxisAlignedBox3 axisAlignedBox)
		{
			Box3 box = new Box3(axisAlignedBox.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY, axisAlignedBox.ExtentZ);
			Segment3? segment = ray.IntersectionWith(box);
			if (segment == null)
			{
				return new List<Vector3>();
			}
			Segment3 value = segment.Value;
			if (value.Extent < 1E-08)
			{
				return new List<Vector3>
				{
					value.Origin
				};
			}
			return new List<Vector3>
			{
				value.NegativeEnd,
				value.PositiveEnd
			};
		}
	}
}
