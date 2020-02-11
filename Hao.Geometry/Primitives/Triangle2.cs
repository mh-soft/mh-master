using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Triangle2 : IEquatable<Triangle2>
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x0000A97F File Offset: 0x00008B7F
		public Triangle2(Vector2 vector0, Vector2 vector1, Vector2 vector2)
		{
			this = default(Triangle2);
			this.V0 = vector0;
			this.V1 = vector1;
			this.V2 = vector2;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000A99D File Offset: 0x00008B9D
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000A9A5 File Offset: 0x00008BA5
		public Vector2 V0 { get; private set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000A9AE File Offset: 0x00008BAE
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public Vector2 V1 { get; private set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000A9BF File Offset: 0x00008BBF
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000A9C7 File Offset: 0x00008BC7
		public Vector2 V2 { get; private set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		
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

		// Token: 0x170000C0 RID: 192
		public Vector2 this[int index]
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
					throw new KeyNotFoundException("Triangle2[].get, index out of range");
				}
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000AA8C File Offset: 0x00008C8C
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Triangle2 other = (Triangle2)obj;
			return this.Equals(other);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000AACC File Offset: 0x00008CCC
		public bool Equals(Triangle2 other)
		{
			return this.V0.Equals(other.V0) && this.V1.Equals(other.V1) && this.V2.Equals(other.V2);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000AB20 File Offset: 0x00008D20
		public override int GetHashCode()
		{
			return this.V0.GetHashCode() ^ this.V1.GetHashCode() ^ this.V2.GetHashCode();
		}
	}
}
