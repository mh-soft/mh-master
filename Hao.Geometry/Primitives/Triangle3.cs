using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Triangle3 : IEquatable<Triangle3>
	{
		// Token: 0x06000203 RID: 515 RVA: 0x0000AB6B File Offset: 0x00008D6B
		public Triangle3(Vector3 vector0, Vector3 vector1, Vector3 vector2)
		{
			this = default(Triangle3);
			this.V0 = vector0;
			this.V1 = vector1;
			this.V2 = vector2;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000AB89 File Offset: 0x00008D89
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000AB91 File Offset: 0x00008D91
		public Vector3 V0 { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000AB9A File Offset: 0x00008D9A
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000ABA2 File Offset: 0x00008DA2
		public Vector3 V1 { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000ABAB File Offset: 0x00008DAB
		// (set) Token: 0x06000209 RID: 521 RVA: 0x0000ABB3 File Offset: 0x00008DB3
		public Vector3 V2 { get; private set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000ABBC File Offset: 0x00008DBC
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(V0:");
				stringBuilder.Append(this.V0.DebuggerDisplay);
				stringBuilder.Append(" V1:");
				stringBuilder.Append(this.V1.DebuggerDisplay);
				stringBuilder.Append(" V2:");
				stringBuilder.Append(this.V2.DebuggerDisplay);
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170000C5 RID: 197
		public Vector3 this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.V0;
				case 1:
					return this.V1;
				case 2:
					return this.V2;
				default:
					throw new KeyNotFoundException("Triangle3[].get, index out of range");
				}
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000AC77 File Offset: 0x00008E77
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Triangle3)obj);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000ACA8 File Offset: 0x00008EA8
		public bool Equals(Triangle3 other)
		{
			return this.V0.Equals(other.V0) && this.V1.Equals(other.V1) && this.V2.Equals(other.V2);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000ACFC File Offset: 0x00008EFC
		public override int GetHashCode()
		{
			return this.V0.GetHashCode() ^ this.V1.GetHashCode() ^ this.V2.GetHashCode();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000AD48 File Offset: 0x00008F48
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Triangle3{V0:{ ");
			stringBuilder.Append(string.Concat(new object[]
			{
				this.V0.X,
				", ",
				this.V0.Y,
				", ",
				this.V0.Z
			}));
			stringBuilder.Append(" } V1:{ ");
			stringBuilder.Append(string.Concat(new object[]
			{
				this.V1.X,
				", ",
				this.V1.Y,
				", ",
				this.V1.Z
			}));
			stringBuilder.Append(" } V2:{ ");
			stringBuilder.Append(string.Concat(new object[]
			{
				this.V2.X,
				", ",
				this.V2.Y,
				", ",
				this.V2.Z
			}));
			stringBuilder.Append(" }}");
			return stringBuilder.ToString();
		}
	}
}
