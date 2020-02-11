using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x02000068 RID: 104
	public static class IntersectionSegment3
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x0001226C File Offset: 0x0001046C
		public static bool Intersects(this Segment3 segment, Cylinder3 cylinder)
		{
			IntersectionSegment3Cylinder3 intersectionSegment3Cylinder = new IntersectionSegment3Cylinder3(segment, cylinder);
			return intersectionSegment3Cylinder.Find();
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001228C File Offset: 0x0001048C
		public static Segment3? IntersectionWith(this Segment3 segment, Cylinder3 cylinder)
		{
			IntersectionSegment3Cylinder3 intersectionSegment3Cylinder = new IntersectionSegment3Cylinder3(segment, cylinder);
			intersectionSegment3Cylinder.Find();
			if (intersectionSegment3Cylinder.IntersectionType == Intersection.Type.IT_SEGMENT)
			{
				return new Segment3?(new Segment3(intersectionSegment3Cylinder.Point0, intersectionSegment3Cylinder.Point1));
			}
			if (intersectionSegment3Cylinder.IntersectionType == Intersection.Type.IT_POINT)
			{
				return new Segment3?(new Segment3(intersectionSegment3Cylinder.Point0, intersectionSegment3Cylinder.Point0));
			}
			return null;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000122FC File Offset: 0x000104FC
		public static ICollection<Vector3> IntersectionPointsWith(this Segment3 segment, Cylinder3 cylinder)
		{
			Segment3? segment2 = segment.IntersectionWith(cylinder);
			if (segment2 == null)
			{
				return new List<Vector3>();
			}
			Segment3 value = segment2.Value;
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

		// Token: 0x0600040C RID: 1036 RVA: 0x0001236C File Offset: 0x0001056C
		public static bool Intersects(this Segment3 segment, Box3 box)
		{
			IntersectionSegment3Box3 intersectionSegment3Box = new IntersectionSegment3Box3(segment, box, true);
			return intersectionSegment3Box.Test();
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001238A File Offset: 0x0001058A
		public static Segment3? IntersectionWith(this Segment3 segment, Box3 box)
		{
			return Intersection.DoClipping(-segment.Extent, segment.Extent, segment.Origin, segment.Direction, box);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000123B0 File Offset: 0x000105B0
		public static ICollection<Vector3> IntersectionPointsWith(this Segment3 segment, Box3 box)
		{
			Segment3? segment2 = segment.IntersectionWith(box);
			if (segment2 == null)
			{
				return new List<Vector3>();
			}
			Segment3 value = segment2.Value;
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

		// Token: 0x0600040F RID: 1039 RVA: 0x00012420 File Offset: 0x00010620
		public static bool Intersects(this Segment3 segment, AxisAlignedBox3 axisAlignedBox)
		{
			Box3 box = new Box3(axisAlignedBox.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY, axisAlignedBox.ExtentZ);
			IntersectionSegment3Box3 intersectionSegment3Box = new IntersectionSegment3Box3(segment, box, true);
			return intersectionSegment3Box.Test();
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00012470 File Offset: 0x00010670
		public static Segment3? IntersectionWith(this Segment3 segment, AxisAlignedBox3 axisAlignedBox)
		{
			Box3 box = new Box3(axisAlignedBox.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY, axisAlignedBox.ExtentZ);
			return Intersection.DoClipping(-segment.Extent, segment.Extent, segment.Origin, segment.Direction, box);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000124D4 File Offset: 0x000106D4
		public static ICollection<Vector3> IntersectionPointsWith(this Segment3 segment, AxisAlignedBox3 axisAlignedBox)
		{
			Box3 box = new Box3(axisAlignedBox.Center, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, axisAlignedBox.ExtentX, axisAlignedBox.ExtentY, axisAlignedBox.ExtentZ);
			Segment3? segment2 = segment.IntersectionWith(box);
			if (segment2 == null)
			{
				return new List<Vector3>();
			}
			Segment3 value = segment2.Value;
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

		// Token: 0x06000412 RID: 1042 RVA: 0x00012578 File Offset: 0x00010778
		public static bool Intersects(this Segment3 segment, Plane3 plane)
		{
			IntersectionSegment3Plane3 intersectionSegment3Plane = new IntersectionSegment3Plane3(segment, plane);
			return intersectionSegment3Plane.Test();
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00012598 File Offset: 0x00010798
		public static Vector3? IntersectionWith(this Segment3 segment, Plane3 plane)
		{
			IntersectionSegment3Plane3 intersectionSegment3Plane = new IntersectionSegment3Plane3(segment, plane);
			if (intersectionSegment3Plane.Find() && intersectionSegment3Plane.IntersectionType != Intersection.Type.IT_EMPTY)
			{
				return new Vector3?(segment.Origin + intersectionSegment3Plane.SegmentParameter * segment.Direction);
			}
			return null;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000125F0 File Offset: 0x000107F0
		public static bool Intersects(this Segment3 segment, Triangle3 triangle)
		{
			IntersectionSegment3Triangle3 intersectionSegment3Triangle = new IntersectionSegment3Triangle3(segment, triangle);
			return intersectionSegment3Triangle.Test();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00012610 File Offset: 0x00010810
		public static Vector3? IntersectionWith(this Segment3 segment, Triangle3 triangle)
		{
			IntersectionSegment3Triangle3 intersectionSegment3Triangle = new IntersectionSegment3Triangle3(segment, triangle);
			if (intersectionSegment3Triangle.Find() && intersectionSegment3Triangle.IntersectionType != Intersection.Type.IT_EMPTY)
			{
				return new Vector3?(segment.Origin + intersectionSegment3Triangle.SegmentParameter * segment.Direction);
			}
			return null;
		}
	}
}
