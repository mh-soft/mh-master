using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x0200003D RID: 61
	internal struct IntersectionPlane3Arc3
	{
		// Token: 0x0600027A RID: 634 RVA: 0x0000A64E File Offset: 0x0000884E
		public IntersectionPlane3Arc3(Plane3 plane, Arc3 arc)
		{
			this = default(IntersectionPlane3Arc3);
			this.plane = plane;
			this.arc = arc;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A668 File Offset: 0x00008868
		public bool Test()
		{
			IntersectionPlane3Circle3 intersectionPlane3Circle = new IntersectionPlane3Circle3(this.plane, this.arc.Circle);
			if (!intersectionPlane3Circle.Test())
			{
				return false;
			}
			ICollection<Vector3> collection = intersectionPlane3Circle.Find();
			if (collection.Count == 0)
			{
				return true;
			}
			foreach (Vector3 point in collection)
			{
				Angle pointDirectionAngle = this.arc.Circle.GetPointDirectionAngle(point);
				if (this.arc.Contains(pointDirectionAngle))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A714 File Offset: 0x00008914
		public ICollection<Vector3> Find()
		{
			List<Vector3> list = new List<Vector3>();
			IntersectionPlane3Circle3 intersectionPlane3Circle = new IntersectionPlane3Circle3(this.plane, this.arc.Circle);
			foreach (Vector3 vector in intersectionPlane3Circle.Find())
			{
				Angle pointDirectionAngle = this.arc.Circle.GetPointDirectionAngle(vector);
				if (this.arc.Contains(pointDirectionAngle))
				{
					list.Add(vector);
				}
			}
			return list;
		}

		// Token: 0x0400008A RID: 138
		private readonly Plane3 plane;

		// Token: 0x0400008B RID: 139
		private readonly Arc3 arc;
	}
}
