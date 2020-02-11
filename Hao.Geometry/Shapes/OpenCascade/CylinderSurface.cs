using System;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000016 RID: 22
	public class CylinderSurface : ISurface
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00004F1A File Offset: 0x0000311A
		public CylinderSurface(Vector3 origin, UnitVector3 unitX, UnitVector3 unitY, bool direct, double radius)
		{
			this.Origin = origin;
			this.UnitX = unitX;
			this.UnitY = unitY;
			this.Direct = direct;
			this.Radius = radius;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004F47 File Offset: 0x00003147
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00004F4F File Offset: 0x0000314F
		public Vector3 Origin { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004F58 File Offset: 0x00003158
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00004F60 File Offset: 0x00003160
		public UnitVector3 UnitX { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004F69 File Offset: 0x00003169
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00004F71 File Offset: 0x00003171
		public UnitVector3 UnitY { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004F7C File Offset: 0x0000317C
		public UnitVector3 Direction
		{
			get
			{
				if (this.Direct)
				{
					return this.UnitX.UnitCross(this.UnitY);
				}
				return this.UnitY.UnitCross(this.UnitX);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004FBA File Offset: 0x000031BA
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00004FC2 File Offset: 0x000031C2
		public bool Direct { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004FCB File Offset: 0x000031CB
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00004FD3 File Offset: 0x000031D3
		public double Radius { get; private set; }

		// Token: 0x06000101 RID: 257 RVA: 0x00004FDC File Offset: 0x000031DC
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((CylinderSurface)obj);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005004 File Offset: 0x00003204
		public bool Equals(CylinderSurface other)
		{
			return this.Origin.Equals(other.Origin) && this.UnitX.Equals(other.UnitX) && this.UnitY.Equals(other.UnitY) && this.Direct.Equals(other.Direct) && this.Radius.Equals(other.Radius);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005080 File Offset: 0x00003280
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.UnitX.GetHashCode() ^ this.UnitY.GetHashCode() ^ this.Direct.GetHashCode() ^ this.Radius.GetHashCode();
		}
	}
}
