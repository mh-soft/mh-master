using System;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000015 RID: 21
	public class SurfaceOfRevolution : ISurface
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00004E14 File Offset: 0x00003014
		public SurfaceOfRevolution(Vector3 origin, UnitVector3 direction, ICurve3 meridian)
		{
			this.Origin = origin;
			this.Direction = direction;
			this.Meridian = meridian;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004E31 File Offset: 0x00003031
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00004E39 File Offset: 0x00003039
		public Vector3 Origin { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004E42 File Offset: 0x00003042
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00004E4A File Offset: 0x0000304A
		public UnitVector3 Direction { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004E53 File Offset: 0x00003053
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00004E5B File Offset: 0x0000305B
		public ICurve3 Meridian { get; private set; }

		// Token: 0x060000F2 RID: 242 RVA: 0x00004E64 File Offset: 0x00003064
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((SurfaceOfRevolution)obj);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004E8C File Offset: 0x0000308C
		public bool Equals(SurfaceOfRevolution other)
		{
			return this.Origin.Equals(other.Origin) && this.Direction.Equals(other.Direction) && this.Meridian.Equals(other.Meridian);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004ED8 File Offset: 0x000030D8
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.Direction.GetHashCode() ^ this.Meridian.GetHashCode();
		}
	}
}
