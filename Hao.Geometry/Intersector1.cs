using System;

namespace Hao.Geometry
{
	// Token: 0x02000073 RID: 115
	internal class Intersector1
	{
		// Token: 0x06000471 RID: 1137 RVA: 0x00015AFC File Offset: 0x00013CFC
		public Intersector1(double u0, double u1, double v0, double v1)
		{
			MathBase.Assert(u0 <= u1 && v0 <= v1, "Intersector1(): bad arguments");
			this.u0 = u0;
			this.u1 = u1;
			this.v0 = v0;
			this.v1 = v1;
			this.Quantity = 0;
		}


		public int Quantity { get; private set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00015B5D File Offset: 0x00013D5D
		public double Overlap0
		{
			get
			{
				return this.overlap0;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00015B65 File Offset: 0x00013D65
		public double Overlap1
		{
			get
			{
				return this.overlap1;
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00015B6D File Offset: 0x00013D6D
		public bool Test()
		{
			return this.u0 < this.v1 + 1E-08 && this.u1 > this.v0 - 1E-08;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00015BA4 File Offset: 0x00013DA4
		public bool Find()
		{
			if (this.u1 < this.v0 - 1E-08 || this.u0 > this.v1 + 1E-08)
			{
				this.Quantity = 0;
			}
			else if (this.u1 > this.v0 + 1E-08)
			{
				if (this.u0 < this.v1 - 1E-08)
				{
					this.Quantity = 2;
					this.overlap0 = ((this.u0 < this.v0) ? this.v0 : this.u0);
					this.overlap1 = ((this.u1 > this.v1) ? this.v1 : this.u1);
					if (Math.Abs(this.overlap0 - this.overlap1) < 1E-08)
					{
						this.Quantity = 1;
					}
				}
				else
				{
					this.Quantity = 1;
					this.overlap0 = this.u0;
				}
			}
			else
			{
				this.Quantity = 1;
				this.overlap0 = this.u1;
			}
			return this.Quantity > 0;
		}

		// Token: 0x0400014B RID: 331
		private readonly double u0;

		// Token: 0x0400014C RID: 332
		private readonly double u1;

		// Token: 0x0400014D RID: 333
		private readonly double v0;

		// Token: 0x0400014E RID: 334
		private readonly double v1;

		// Token: 0x0400014F RID: 335
		private double overlap0;

		// Token: 0x04000150 RID: 336
		private double overlap1;
	}
}
