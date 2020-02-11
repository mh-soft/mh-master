using System;
using System.Collections.Generic;
using System.Linq;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000029 RID: 41
	public class Wire3
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x00007098 File Offset: 0x00005298
		public Wire3(Orientation orientation, IEnumerable<Edge3> edges)
		{
			this.Orientation = orientation;
			this.Edges = new List<Edge3>(edges).AsReadOnly();
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000070B8 File Offset: 0x000052B8
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x000070C0 File Offset: 0x000052C0
		public Orientation Orientation { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000070C9 File Offset: 0x000052C9
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x000070D1 File Offset: 0x000052D1
		public IList<Edge3> Edges { get; private set; }

		// Token: 0x060001E6 RID: 486 RVA: 0x000070DA File Offset: 0x000052DA
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Wire3)obj);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00007100 File Offset: 0x00005300
		public bool Equals(Wire3 other)
		{
			return this.Orientation.Equals(other.Orientation) && this.Edges.SequenceEqual(other.Edges);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007144 File Offset: 0x00005344
		public override int GetHashCode()
		{
			return this.Orientation.GetHashCode() ^ this.Edges.GetHashCode();
		}
	}
}
