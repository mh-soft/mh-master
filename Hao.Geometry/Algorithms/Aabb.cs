using System;
using System.Collections.Generic;

namespace Hao.Geometry.Algorithms
{
	// Token: 0x02000007 RID: 7
	public struct Aabb
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000024CE File Offset: 0x000006CE
		public Aabb(Vector3 center, Vector3 extent)
		{
			this = default(Aabb);
			this.Center = center;
			this.Extent = extent;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000024E5 File Offset: 0x000006E5
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000024ED File Offset: 0x000006ED
		public Vector3 Center { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000024F6 File Offset: 0x000006F6
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000024FE File Offset: 0x000006FE
		public Vector3 Extent { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002507 File Offset: 0x00000707
		public Vector3 Min
		{
			get
			{
				return this.Center - this.Extent;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000251A File Offset: 0x0000071A
		public Vector3 Max
		{
			get
			{
				return this.Center + this.Extent;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002530 File Offset: 0x00000730
		public static implicit operator Aabb(AxisAlignedBox3 box)
		{
			Vector3 center = (box.Max + box.Min) / 2.0;
			Vector3 extent = (box.Max - box.Min) / 2.0;
			return new Aabb(center, extent);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002588 File Offset: 0x00000788
		public Aabb CreateMergedWith(Aabb other)
		{
			Vector3 right = Vector3.Min(this.Min, other.Min);
			Vector3 left = Vector3.Max(this.Max, other.Max);
			Vector3 center = (left + right) / 2.0;
			Vector3 extent = (left - right) / 2.0;
			return new Aabb(center, extent);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025EC File Offset: 0x000007EC
		internal bool CollideRay(Ray3 ray)
		{
			Vector3 vector = ray.Origin - this.Center;
			if (Math.Abs(vector.X) > this.Extent.X && vector.X * ray.Direction.X >= 0.0)
			{
				return false;
			}
			if (Math.Abs(vector.Y) > this.Extent.Y && vector.Y * ray.Direction.Y >= 0.0)
			{
				return false;
			}
			if (Math.Abs(vector.Z) > this.Extent.Z && vector.Z * ray.Direction.Z >= 0.0)
			{
				return false;
			}
			Vector3 vector2 = ray.Direction.Cross(vector);
			Vector3 vector3 = new Vector3(Math.Abs(ray.Direction.X), Math.Abs(ray.Direction.Y), Math.Abs(ray.Direction.Z));
			return Math.Abs(vector2.X) <= this.Extent.Y * vector3.Z + this.Extent.Z * vector3.Y && Math.Abs(vector2.Y) <= this.Extent.Z * vector3.X + this.Extent.X * vector3.Z && Math.Abs(vector2.Z) <= this.Extent.X * vector3.Y + this.Extent.Y * vector3.X;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000027E0 File Offset: 0x000009E0
		internal bool CollidePlanes(ICollection<Plane3> clippingPlanes)
		{
			AxisAlignedBox3 axisAlignedBox = new AxisAlignedBox3(this.Center - this.Extent, this.Center + this.Extent);
			using (IEnumerator<Plane3> enumerator = clippingPlanes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.SideClassification(axisAlignedBox) == PlaneSideClassification.Front)
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}
