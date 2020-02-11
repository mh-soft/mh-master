using System;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000013 RID: 19
	public class ConeSurface : ISurface
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00004C07 File Offset: 0x00002E07
		public ConeSurface(Vector3 origin, UnitVector3 unitX, UnitVector3 unitY, bool direct, double angle, double radius)
		{
			this.Origin = origin;
			this.UnitX = unitX;
			this.UnitY = unitY;
			this.Direct = direct;
			this.Angle = angle;
			this.Radius = radius;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004C3C File Offset: 0x00002E3C
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00004C44 File Offset: 0x00002E44
		public Vector3 Origin { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004C4D File Offset: 0x00002E4D
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00004C55 File Offset: 0x00002E55
		public UnitVector3 UnitX { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004C5E File Offset: 0x00002E5E
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00004C66 File Offset: 0x00002E66
		public UnitVector3 UnitY { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004C70 File Offset: 0x00002E70
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004CAE File Offset: 0x00002EAE
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00004CB6 File Offset: 0x00002EB6
		public bool Direct { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004CBF File Offset: 0x00002EBF
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00004CC7 File Offset: 0x00002EC7
		public double Angle { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004CD0 File Offset: 0x00002ED0
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00004CD8 File Offset: 0x00002ED8
		public double Radius { get; private set; }

		// Token: 0x060000E7 RID: 231 RVA: 0x00004CE1 File Offset: 0x00002EE1
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((ConeSurface)obj);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004D08 File Offset: 0x00002F08
		public bool Equals(ConeSurface other)
		{
			return this.Origin.Equals(other.Origin) && this.UnitX.Equals(other.UnitX) && this.UnitY.Equals(other.UnitY) && this.Direct.Equals(other.Direct) && this.Angle.Equals(other.Angle) && this.Radius.Equals(other.Radius);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004D9C File Offset: 0x00002F9C
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.UnitX.GetHashCode() ^ this.UnitY.GetHashCode() ^ this.Direct.GetHashCode() ^ this.Angle.GetHashCode() ^ this.Radius.GetHashCode();
		}
	}
}
