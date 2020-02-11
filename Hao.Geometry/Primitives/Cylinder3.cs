using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Cylinder3 : IEquatable<Cylinder3>
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00004896 File Offset: 0x00002A96
		public Cylinder3(Line3 axis, double radius, double height)
		{
			this = default(Cylinder3);
			this.Axis = axis;
			this.Radius = radius;
			this.Height = height;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000048B4 File Offset: 0x00002AB4
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x000048BC File Offset: 0x00002ABC
		public Line3 Axis { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000048C5 File Offset: 0x00002AC5
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x000048CD File Offset: 0x00002ACD
		public double Radius { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000048D6 File Offset: 0x00002AD6
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x000048DE File Offset: 0x00002ADE
		public double Height { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000048E8 File Offset: 0x00002AE8
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(A:");
				stringBuilder.Append(this.Axis.DebuggerDisplay);
				stringBuilder.Append(" R:");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.Radius
				}));
				stringBuilder.Append(" H:");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.Height
				}));
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000499C File Offset: 0x00002B9C
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Cylinder3 other = (Cylinder3)obj;
			return this.Equals(other);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000049DC File Offset: 0x00002BDC
		public bool Equals(Cylinder3 other)
		{
			return this.Axis.Equals(other.Axis) && this.Radius.Equals(other.Radius) && this.Height.Equals(other.Height);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004A30 File Offset: 0x00002C30
		public override int GetHashCode()
		{
			return this.Axis.GetHashCode() ^ this.Radius.GetHashCode() ^ this.Height.GetHashCode();
		}
	}
}
