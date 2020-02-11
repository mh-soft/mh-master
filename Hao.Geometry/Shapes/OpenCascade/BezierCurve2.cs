using System;
using System.Collections.Generic;
using System.Linq;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x0200000B RID: 11
	public class BezierCurve2 : ICurve2
	{
		// Token: 0x06000063 RID: 99 RVA: 0x000036F6 File Offset: 0x000018F6
		public BezierCurve2(IEnumerable<Vector2> poles, IEnumerable<double> weights)
		{
			this.Poles = new List<Vector2>(poles).AsReadOnly();
			this.Weights = new List<double>(weights).AsReadOnly();
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003720 File Offset: 0x00001920
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003728 File Offset: 0x00001928
		public IList<Vector2> Poles { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003731 File Offset: 0x00001931
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003739 File Offset: 0x00001939
		public IList<double> Weights { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003742 File Offset: 0x00001942
		public int Degree
		{
			get
			{
				return this.Poles.Count - 1;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003751 File Offset: 0x00001951
		public Vector2 EvalAt(double parameter)
		{
			throw new NotImplementedException("BezierCurve2.EvalAt()");
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000375D File Offset: 0x0000195D
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((BezierCurve2)obj);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003783 File Offset: 0x00001983
		public bool Equals(BezierCurve2 other)
		{
			return this.Poles.SequenceEqual(other.Poles) && this.Weights.SequenceEqual(other.Weights);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000037AB File Offset: 0x000019AB
		public override int GetHashCode()
		{
			return this.Poles.GetHashCode() ^ this.Weights.GetHashCode();
		}
	}
}
