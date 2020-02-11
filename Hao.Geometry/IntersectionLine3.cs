using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x02000056 RID: 86
	public static class IntersectionLine3
	{
		// Token: 0x06000361 RID: 865 RVA: 0x0000E8A0 File Offset: 0x0000CAA0
		public static bool Intersects(this Line3 line, Cylinder3 cylinder)
		{
			IntersectionLine3Cylinder3 intersectionLine3Cylinder = new IntersectionLine3Cylinder3(line, cylinder);
			return intersectionLine3Cylinder.Find();
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
		public static Segment3? IntersectionWith(this Line3 line, Cylinder3 cylinder)
		{
			IntersectionLine3Cylinder3 intersectionLine3Cylinder = new IntersectionLine3Cylinder3(line, cylinder);
			intersectionLine3Cylinder.Find();
			if (intersectionLine3Cylinder.IntersectionType == Intersection.Type.IT_SEGMENT)
			{
				return new Segment3?(new Segment3(intersectionLine3Cylinder.Point0, intersectionLine3Cylinder.Point1));
			}
			if (intersectionLine3Cylinder.IntersectionType == Intersection.Type.IT_POINT)
			{
				return new Segment3?(new Segment3(intersectionLine3Cylinder.Point0, intersectionLine3Cylinder.Point0));
			}
			return null;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000E930 File Offset: 0x0000CB30
		public static ICollection<Vector3> IntersectionPointsWith(this Line3 line, Cylinder3 cylinder)
		{
			Segment3? segment = line.IntersectionWith(cylinder);
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

		// Token: 0x06000364 RID: 868 RVA: 0x0000E9A0 File Offset: 0x0000CBA0
		public static bool Intersects(this Line3 line, Torus3 torus)
		{
			IntersectionLine3Torus3 intersectionLine3Torus = new IntersectionLine3Torus3(line, torus);
			return intersectionLine3Torus.Find();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		public static ICollection<Vector3> IntersectionPointsWith(this Line3 line, Torus3 torus)
		{
			List<Vector3> list = new List<Vector3>();
			IntersectionLine3Torus3 intersectionLine3Torus = new IntersectionLine3Torus3(line, torus);
			if (intersectionLine3Torus.Find())
			{
				Vector3[] array = new Vector3[]
				{
					intersectionLine3Torus.Point0,
					intersectionLine3Torus.Point1,
					intersectionLine3Torus.Point2,
					intersectionLine3Torus.Point3
				};
				for (int i = 0; i < intersectionLine3Torus.Quantity; i++)
				{
					list.Add(array[i]);
				}
			}
			return list;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000EA44 File Offset: 0x0000CC44
		public static bool Intersects(this Line3 line, Box3 box)
		{
			IntersectionLine3Box3 intersectionLine3Box = new IntersectionLine3Box3(line, box);
			return intersectionLine3Box.Test();
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000EA61 File Offset: 0x0000CC61
		public static Segment3? IntersectionWith(this Line3 line, Box3 box)
		{
			return Intersection.DoClipping(double.MinValue, double.MaxValue, line.Origin, line.Direction, box);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000EA8C File Offset: 0x0000CC8C
		public static ICollection<Vector3> IntersectionPointsWith(this Line3 line, Box3 box)
		{
			Segment3? segment = line.IntersectionWith(box);
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

		// Token: 0x06000369 RID: 873 RVA: 0x0000EAFC File Offset: 0x0000CCFC
		public static bool Intersects(this Line3 line, AxisAlignedBox3 axisAlignedBox)
		{
			Box3 box = new Box3(axisAlignedBox.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY, axisAlignedBox.ExtentZ);
			IntersectionLine3Box3 intersectionLine3Box = new IntersectionLine3Box3(line, box);
			return intersectionLine3Box.Test();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000EB4C File Offset: 0x0000CD4C
		public static Segment3? IntersectionWith(this Line3 line, AxisAlignedBox3 axisAlignedBox)
		{
			Box3 box = new Box3(axisAlignedBox.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY, axisAlignedBox.ExtentZ);
			return Intersection.DoClipping(double.MinValue, double.MaxValue, line.Origin, line.Direction, box);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000EBB4 File Offset: 0x0000CDB4
		public static ICollection<Vector3> IntersectionPointsWith(this Line3 line, AxisAlignedBox3 axisAlignedBox)
		{
			Box3 box = new Box3(axisAlignedBox.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY, axisAlignedBox.ExtentZ);
			Segment3? segment = line.IntersectionWith(box);
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

		// Token: 0x0600036C RID: 876 RVA: 0x0000EC58 File Offset: 0x0000CE58
		public static bool Intersects(this Line3 line, Plane3 plane)
		{
			IntersectionLine3Plane3 intersectionLine3Plane = new IntersectionLine3Plane3(line, plane);
			return intersectionLine3Plane.Test();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000EC78 File Offset: 0x0000CE78
		public static Vector3? IntersectionWith(this Line3 line, Plane3 plane)
		{
			IntersectionLine3Plane3 intersectionLine3Plane = new IntersectionLine3Plane3(line, plane);
			if (intersectionLine3Plane.Find() && intersectionLine3Plane.IntersectionType != Intersection.Type.IT_EMPTY)
			{
				return new Vector3?(line.Origin + intersectionLine3Plane.LineParameter * line.Direction);
			}
			return null;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000ECD0 File Offset: 0x0000CED0
		public static bool Intersects(this Line3 line, Triangle3 triangle)
		{
			IntersectionLine3Triangle3 intersectionLine3Triangle = new IntersectionLine3Triangle3(line, triangle);
			return intersectionLine3Triangle.Test();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		public static Vector3? IntersectionWith(this Line3 line, Triangle3 triangle3)
		{
			IntersectionLine3Triangle3 intersectionLine3Triangle = new IntersectionLine3Triangle3(line, triangle3);
			if (intersectionLine3Triangle.Find() && intersectionLine3Triangle.IntersectionType != Intersection.Type.IT_EMPTY)
			{
				return new Vector3?(line.Origin + intersectionLine3Triangle.LineParameter * line.Direction);
			}
			return null;
		}
	}
}
