using System;
using System.Collections.Generic;

namespace Hao.Geometry.Topology
{
	// Token: 0x02000002 RID: 2
	public class Contour2
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Contour2(Wire2 outerWire, IEnumerable<Wire2> holes)
		{
			if (!outerWire.Closed)
			{
				throw new ArgumentException("outer wire cannot be open", "outerWire");
			}
			this.OuterWire = outerWire;
			this.holes = new List<Wire2>();
			foreach (Wire2 wire in holes)
			{
				if (!wire.Closed)
				{
					throw new ArgumentException("Hole cannot be open", "holes");
				}
				this.holes.Add(wire);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020E8 File Offset: 0x000002E8
		public double ContinuousError
		{
			get
			{
				double num = this.OuterWire.ContinuousError;
				foreach (Wire2 wire in this.Holes)
				{
					double continuousError = wire.ContinuousError;
					if (continuousError > num)
					{
						num = continuousError;
					}
				}
				return num;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002148 File Offset: 0x00000348
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002150 File Offset: 0x00000350
		public Wire2 OuterWire { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002159 File Offset: 0x00000359
		public IList<Wire2> Holes
		{
			get
			{
				return this.holes;
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly List<Wire2> holes;
	}
}
