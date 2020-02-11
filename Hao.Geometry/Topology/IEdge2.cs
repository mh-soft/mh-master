using System;

namespace Hao.Geometry.Topology
{
	// Token: 0x0200000A RID: 10
	public interface IEdge2
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600003E RID: 62
		Vector2 StartPoint { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600003F RID: 63
		Vector2 EndPoint { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000040 RID: 64
		UnitVector2 StartDirection { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000041 RID: 65
		UnitVector2 EndDirection { get; }

		// Token: 0x06000042 RID: 66
		AxisAlignedBox2 ComputeAxisAlignedBoundingBox();
	}
}
