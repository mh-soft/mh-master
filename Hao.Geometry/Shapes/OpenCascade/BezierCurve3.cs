using System;
using System.Collections.Generic;
using System.Linq;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x0200000C RID: 12
	public class BezierCurve3 : ICurve3
	{
		// Token: 0x0600006D RID: 109 RVA: 0x000037C4 File Offset: 0x000019C4
		public BezierCurve3(IEnumerable<Vector3> poles, IEnumerable<double> weights)
		{
			this.Poles = new List<Vector3>(poles).AsReadOnly();
			this.Weights = new List<double>(weights).AsReadOnly();
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000037EE File Offset: 0x000019EE
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000037F6 File Offset: 0x000019F6
		public IList<Vector3> Poles { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000037FF File Offset: 0x000019FF
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003807 File Offset: 0x00001A07
		public IList<double> Weights { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003810 File Offset: 0x00001A10
		public int Degree
		{
			get
			{
				return this.Poles.Count - 1;
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000381F File Offset: 0x00001A1F
		public Vector3 EvalAt(double parameter)
		{
			throw new NotImplementedException("BezierCurve3.EvalAt()");
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000382B File Offset: 0x00001A2B
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((BezierCurve3)obj);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003851 File Offset: 0x00001A51
		public bool Equals(BezierCurve3 other)
		{
			return this.Poles.SequenceEqual(other.Poles) && this.Weights.SequenceEqual(other.Weights);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003879 File Offset: 0x00001A79
		public override int GetHashCode()
		{
			return this.Poles.GetHashCode() ^ this.Weights.GetHashCode();
		}
	}
}
