using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Line3 : IEquatable<Line3>
	{
		// Token: 0x06000113 RID: 275 RVA: 0x0000513A File Offset: 0x0000333A
		public Line3(Vector3 origin, UnitVector3 direction)
		{
			this = default(Line3);
			this.Origin = origin;
			this.Direction = direction;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005151 File Offset: 0x00003351
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00005159 File Offset: 0x00003359
		public Vector3 Origin { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005162 File Offset: 0x00003362
		// (set) Token: 0x06000117 RID: 279 RVA: 0x0000516A File Offset: 0x0000336A
		public UnitVector3 Direction { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005174 File Offset: 0x00003374
		
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

		// Token: 0x06000119 RID: 281 RVA: 0x000051DC File Offset: 0x000033DC
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Line3 other = (Line3)obj;
			return this.Equals(other);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000521C File Offset: 0x0000341C
		public bool Equals(Line3 other)
		{
			return this.Origin.Equals(other.Origin) && this.Direction.Equals(other.Direction);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005258 File Offset: 0x00003458
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.Direction.GetHashCode();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005290 File Offset: 0x00003490
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Line3{Origin");
			stringBuilder.Append("{X:");
			stringBuilder.Append(this.Origin.X);
			stringBuilder.Append(" Y:");
			stringBuilder.Append(this.Origin.Y);
			stringBuilder.Append(" Z:");
			stringBuilder.Append(this.Origin.Z);
			stringBuilder.Append("}");
			stringBuilder.Append(" Direction");
			stringBuilder.Append("{X:");
			stringBuilder.Append(this.Direction.X);
			stringBuilder.Append(" Y:");
			stringBuilder.Append(this.Direction.Y);
			stringBuilder.Append(" Z:");
			stringBuilder.Append(this.Direction.Z);
			stringBuilder.Append("}}");
			return stringBuilder.ToString();
		}
	}
}
