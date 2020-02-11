using System;
using System.Collections.Generic;

namespace Hao.Geometry.Topology
{
	// Token: 0x0200000D RID: 13
	public class Wire2
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000027FC File Offset: 0x000009FC
		public Wire2(IEnumerable<IEdge2> edges, bool closed)
		{
			this.edges = new List<IEdge2>();
			foreach (IEdge2 item in edges)
			{
				this.edges.Add(item);
			}
			this.Closed = closed;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002864 File Offset: 0x00000A64
		public IList<IEdge2> Edges
		{
			get
			{
				return this.edges;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000286C File Offset: 0x00000A6C
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002874 File Offset: 0x00000A74
		public bool Closed { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002880 File Offset: 0x00000A80
		public double ContinuousError
		{
			get
			{
				double num = 0.0;
				if (this.edges.Count == 0)
				{
					return 0.0;
				}
				for (int i = 0; i < this.edges.Count - 1; i++)
				{
					IEdge2 edge = this.edges[i];
					IEdge2 edge2 = this.edges[i + 1];
					double length = (edge.EndPoint - edge2.StartPoint).Length;
					if (length > num)
					{
						num = length;
					}
				}
				if (this.Closed)
				{
					IEdge2 edge3 = this.edges[0];
					double length2 = (this.edges[this.edges.Count - 1].EndPoint - edge3.StartPoint).Length;
					if (length2 > num)
					{
						num = length2;
					}
				}
				return num;
			}
		}

		// Token: 0x0400000A RID: 10
		private readonly List<IEdge2> edges;
	}
}
