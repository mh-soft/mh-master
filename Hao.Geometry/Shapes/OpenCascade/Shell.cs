using System;
using System.Collections.Generic;
using System.Linq;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000025 RID: 37
	public class Shell
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00006BDA File Offset: 0x00004DDA
		public Shell(IEnumerable<Face> faces)
		{
			this.Faces = new List<Face>(faces).AsReadOnly();
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00006BF3 File Offset: 0x00004DF3
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00006BFB File Offset: 0x00004DFB
		public IList<Face> Faces { get; private set; }

		// Token: 0x060001B8 RID: 440 RVA: 0x00006C04 File Offset: 0x00004E04
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Shell)obj);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006C2A File Offset: 0x00004E2A
		public bool Equals(Shell other)
		{
			return this.Faces.SequenceEqual(other.Faces);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00006C3D File Offset: 0x00004E3D
		public override int GetHashCode()
		{
			return this.Faces.GetHashCode();
		}
	}
}
