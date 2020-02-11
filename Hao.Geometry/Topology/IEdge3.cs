using System;

namespace Hao.Geometry.Topology
{
	// Token: 0x0200000B RID: 11
	public interface IEdge3
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000043 RID: 67
		Vector3 StartPoint { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000044 RID: 68
		Vector3 EndPoint { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000045 RID: 69
		UnitVector3 StartDirection { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000046 RID: 70
		UnitVector3 EndDirection { get; }

		// Token: 0x06000047 RID: 71
		AxisAlignedBox3 ComputeAxisAlignedBoundingBox();
	}
}
