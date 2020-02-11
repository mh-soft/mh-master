using System;
using System.Collections.Generic;

namespace Hao.Geometry.Topology
{
	// Token: 0x0200000E RID: 14
	public class Wire3
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002954 File Offset: 0x00000B54
		public Wire3(IEnumerable<IEdge3> edges, bool closed)
		{
			this.edges = new List<IEdge3>();
			foreach (IEdge3 item in edges)
			{
				this.edges.Add(item);
			}
			this.Closed = closed;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000029BC File Offset: 0x00000BBC
		public IList<IEdge3> Edges
		{
			get
			{
				return this.edges;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000029C4 File Offset: 0x00000BC4
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Closed { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000029D8 File Offset: 0x00000BD8
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
					IEdge3 edge = this.edges[i];
					IEdge3 edge2 = this.edges[i + 1];
					double length = (edge.EndPoint - edge2.StartPoint).Length;
					if (length > num)
					{
						num = length;
					}
				}
				if (this.Closed)
				{
					IEdge3 edge3 = this.edges[0];
					double length2 = (this.edges[this.edges.Count - 1].EndPoint - edge3.StartPoint).Length;
					if (length2 > num)
					{
						num = length2;
					}
				}
				return num;
			}
		}

		// Token: 0x0400000C RID: 12
		private readonly List<IEdge3> edges;
	}
}
