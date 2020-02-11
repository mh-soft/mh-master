using System;

namespace Hao.Geometry
{
	// Token: 0x02000030 RID: 48
	internal struct DistanceVector3Box3
	{
		// Token: 0x06000215 RID: 533 RVA: 0x0000949C File Offset: 0x0000769C
		public DistanceVector3Box3(Vector3 vector, Box3 box)
		{
			this = default(DistanceVector3Box3);
			Vector3 diff = vector - box.Center;
			Vector3 right;
			double num = DistanceVector3Box3.CalcAxis(diff, box.Axis0, box.Extent0, out right);
			Vector3 right2;
			double num2 = DistanceVector3Box3.CalcAxis(diff, box.Axis1, box.Extent1, out right2);
			Vector3 right3;
			double num3 = DistanceVector3Box3.CalcAxis(diff, box.Axis2, box.Extent2, out right3);
			this.ClosestPointOnVector = vector;
			this.ClosestPointOnBox = box.Center + right + right2 + right3;
			this.SquaredDistance = num + num2 + num3;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00009536 File Offset: 0x00007736
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000953E File Offset: 0x0000773E
		public double SquaredDistance { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00009547 File Offset: 0x00007747
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00009554 File Offset: 0x00007754
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000955C File Offset: 0x0000775C
		public Vector3 ClosestPointOnVector { get; private set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00009565 File Offset: 0x00007765
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000956D File Offset: 0x0000776D
		public Vector3 ClosestPointOnBox { get; private set; }

		// Token: 0x0600021D RID: 541 RVA: 0x00009578 File Offset: 0x00007778
		private static double CalcAxis(Vector3 diff, UnitVector3 axis, double extent, out Vector3 closestPointOnBox)
		{
			double result = 0.0;
			double num = diff.Dot(axis);
			if (num < -extent)
			{
				double num2 = num + extent;
				result = num2 * num2;
				closestPointOnBox = -extent * axis;
			}
			else if (num > extent)
			{
				double num3 = num - extent;
				result = num3 * num3;
				closestPointOnBox = extent * axis;
			}
			else
			{
				closestPointOnBox = num * axis;
			}
			return result;
		}
	}
}
