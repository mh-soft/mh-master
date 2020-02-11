using System;
using System.Collections.Generic;
using System.Linq;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000026 RID: 38
	public class Solid
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00006C4A File Offset: 0x00004E4A
		public Solid(IEnumerable<Shell> shells)
		{
			this.Shells = new List<Shell>(shells).AsReadOnly();
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00006C63 File Offset: 0x00004E63
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00006C6B File Offset: 0x00004E6B
		public IList<Shell> Shells { get; private set; }

		// Token: 0x060001BE RID: 446 RVA: 0x00006C74 File Offset: 0x00004E74
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Solid)obj);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00006C9A File Offset: 0x00004E9A
		public bool Equals(Solid other)
		{
			return this.Shells.SequenceEqual(other.Shells);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00006CAD File Offset: 0x00004EAD
		public override int GetHashCode()
		{
			return this.Shells.GetHashCode();
		}
	}
}
