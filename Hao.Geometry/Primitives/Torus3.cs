using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Torus3 : IEquatable<Torus3>
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00004720 File Offset: 0x00002920
		public Torus3(double outerRadius, double innerRadius)
		{
			this = default(Torus3);
			this.OuterRadius = outerRadius;
			this.InnerRadius = innerRadius;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004737 File Offset: 0x00002937
		// (set) Token: 0x060000DB RID: 219 RVA: 0x0000473F File Offset: 0x0000293F
		public double OuterRadius { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004748 File Offset: 0x00002948
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00004750 File Offset: 0x00002950
		public double InnerRadius { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000475C File Offset: 0x0000295C
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(OR:");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.OuterRadius
				}));
				stringBuilder.Append(" IR:");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.InnerRadius
				}));
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000047F0 File Offset: 0x000029F0
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Torus3 other = (Torus3)obj;
			return this.Equals(other);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004830 File Offset: 0x00002A30
		public bool Equals(Torus3 other)
		{
			return this.OuterRadius.Equals(other.OuterRadius) && this.InnerRadius.Equals(other.InnerRadius);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000486C File Offset: 0x00002A6C
		public override int GetHashCode()
		{
			return this.OuterRadius.GetHashCode() ^ this.InnerRadius.GetHashCode();
		}
	}
}
