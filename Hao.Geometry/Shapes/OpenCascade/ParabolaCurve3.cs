using System;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000023 RID: 35
	public class ParabolaCurve3 : ICurve3
	{
		// Token: 0x0600019A RID: 410 RVA: 0x00006882 File Offset: 0x00004A82
		public ParabolaCurve3(Vector3 origin, UnitVector3 unitX, UnitVector3 unitY, double focal)
		{
			this.Origin = origin;
			this.UnitX = unitX;
			this.UnitY = unitY;
			this.Focal = focal;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000068A7 File Offset: 0x00004AA7
		// (set) Token: 0x0600019C RID: 412 RVA: 0x000068AF File Offset: 0x00004AAF
		public Vector3 Origin { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000068B8 File Offset: 0x00004AB8
		// (set) Token: 0x0600019E RID: 414 RVA: 0x000068C0 File Offset: 0x00004AC0
		public UnitVector3 UnitX { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000068C9 File Offset: 0x00004AC9
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x000068D1 File Offset: 0x00004AD1
		public UnitVector3 UnitY { get; private set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000068DC File Offset: 0x00004ADC
		public UnitVector3 Normal
		{
			get
			{
				return this.UnitX.UnitCross(this.UnitY);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000068FD File Offset: 0x00004AFD
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00006905 File Offset: 0x00004B05
		public double Focal { get; private set; }

		// Token: 0x060001A4 RID: 420 RVA: 0x00006910 File Offset: 0x00004B10
		public Vector3 EvalAt(double parameter)
		{
			return this.Origin + this.Focal * (parameter * parameter * this.UnitX + 2.0 * parameter * this.UnitY);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000695C File Offset: 0x00004B5C
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((ParabolaCurve3)obj);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006984 File Offset: 0x00004B84
		public bool Equals(ParabolaCurve3 other)
		{
			return this.Origin.Equals(other.Origin) && this.UnitX.Equals(other.UnitX) && this.UnitY.Equals(other.UnitY) && this.Focal.Equals(other.Focal);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000069EC File Offset: 0x00004BEC
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.UnitX.GetHashCode() ^ this.UnitY.GetHashCode() ^ this.Focal.GetHashCode();
		}
	}
}
