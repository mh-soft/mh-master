using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Line2 : IEquatable<Line2>
	{

		public Line2(Vector2 origin, UnitVector2 direction)
		{
			this = default(Line2);
			this.Origin = origin;
			this.Direction = direction;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000500E File Offset: 0x0000320E
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00005016 File Offset: 0x00003216
		public Vector2 Origin { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000501F File Offset: 0x0000321F
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00005027 File Offset: 0x00003227
		public UnitVector2 Direction { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005030 File Offset: 0x00003230
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(O:");
				stringBuilder.Append(this.Origin.DebuggerDisplay);
				stringBuilder.Append(" D:");
				stringBuilder.Append(this.Direction.DebuggerDisplay);
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005095 File Offset: 0x00003295
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Line2)obj);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000050C8 File Offset: 0x000032C8
		public bool Equals(Line2 other)
		{
			return this.Origin.Equals(other.Origin) && this.Direction.Equals(other.Direction);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005104 File Offset: 0x00003304
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.Direction.GetHashCode();
		}
	}
}
