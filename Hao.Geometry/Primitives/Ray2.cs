using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Ray2 : IEquatable<Ray2>
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x000043B3 File Offset: 0x000025B3
		public Ray2(Vector2 origin, UnitVector2 direction)
		{
			this = default(Ray2);
			this.Origin = origin;
			this.Direction = direction;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000043CA File Offset: 0x000025CA
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000043D2 File Offset: 0x000025D2
		public Vector2 Origin { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000043DB File Offset: 0x000025DB
		// (set) Token: 0x060000CA RID: 202 RVA: 0x000043E3 File Offset: 0x000025E3
		public UnitVector2 Direction { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000043EC File Offset: 0x000025EC
		
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

		// Token: 0x060000CC RID: 204 RVA: 0x00004451 File Offset: 0x00002651
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Ray2)obj);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004484 File Offset: 0x00002684
		public bool Equals(Ray2 other)
		{
			return this.Origin.Equals(other.Origin) && this.Direction.Equals(other.Direction);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000044C0 File Offset: 0x000026C0
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.Direction.GetHashCode();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000044F8 File Offset: 0x000026F8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Ray2{Origin");
			stringBuilder.Append("{X:");
			stringBuilder.Append(this.Origin.X);
			stringBuilder.Append(" Y:");
			stringBuilder.Append(this.Origin.Y);
			stringBuilder.Append("}");
			stringBuilder.Append(" Direction");
			stringBuilder.Append("{X:");
			stringBuilder.Append(this.Direction.X);
			stringBuilder.Append(" Y:");
			stringBuilder.Append(this.Direction.Y);
			stringBuilder.Append("}}");
			return stringBuilder.ToString();
		}
	}
}
