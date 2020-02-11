using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{
	// Token: 0x02000009 RID: 9
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[Guid("A531AE44-AD95-480c-936B-4A8670F75163")]
	[Serializable]
	public struct Box3 : IEquatable<Box3>
	{
		// Token: 0x0600008F RID: 143 RVA: 0x0000384C File Offset: 0x00001A4C
		public Box3(Vector3 center, UnitVector3[] axis, double[] extent)
		{
			this = default(Box3);
			if (axis.Length != 3 || extent.Length != 3)
			{
				throw new ArgumentException("Box3 needs 3 axises and 3 extents");
			}
			MathBase.Assert(Math.Abs(1.0 - axis[0].Cross(axis[1]).Dot(axis[2])) < 1E-08, "Box3 constructor: axes must be perpendicular and right-handed.");
			this.Center = center;
			this.Axis0 = axis[0];
			this.Axis1 = axis[1];
			this.Axis2 = axis[2];
			this.Extent0 = extent[0];
			this.Extent1 = extent[1];
			this.Extent2 = extent[2];
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003908 File Offset: 0x00001B08
		public Box3(Vector3 center, UnitVector3 axis0, UnitVector3 axis1, UnitVector3 axis2, double extent0, double extent1, double extent2)
		{
			this = default(Box3);
			MathBase.Assert(Math.Abs(1.0 - axis0.Cross(axis1).Dot(axis2)) < 1E-08, "Box3 constructor: axes must be perpendicular and right-handed.");
			this.Center = center;
			this.Axis0 = axis0;
			this.Axis1 = axis1;
			this.Axis2 = axis2;
			this.Extent0 = extent0;
			this.Extent1 = extent1;
			this.Extent2 = extent2;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003987 File Offset: 0x00001B87
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000398F File Offset: 0x00001B8F
		public Vector3 Center { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003998 File Offset: 0x00001B98
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000039A0 File Offset: 0x00001BA0
		public UnitVector3 Axis0 { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000039A9 File Offset: 0x00001BA9
		// (set) Token: 0x06000096 RID: 150 RVA: 0x000039B1 File Offset: 0x00001BB1
		public UnitVector3 Axis1 { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000039BA File Offset: 0x00001BBA
		// (set) Token: 0x06000098 RID: 152 RVA: 0x000039C2 File Offset: 0x00001BC2
		public UnitVector3 Axis2 { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000039CB File Offset: 0x00001BCB
		// (set) Token: 0x0600009A RID: 154 RVA: 0x000039D3 File Offset: 0x00001BD3
		public double Extent0 { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000039DC File Offset: 0x00001BDC
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000039E4 File Offset: 0x00001BE4
		public double Extent1 { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000039ED File Offset: 0x00001BED
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000039F5 File Offset: 0x00001BF5
		public double Extent2 { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003A00 File Offset: 0x00001C00
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(C:");
				stringBuilder.Append(this.Center.DebuggerDisplay);
				stringBuilder.Append(" A0:");
				stringBuilder.Append(this.Axis0.DebuggerDisplay);
				stringBuilder.Append(" A1:");
				stringBuilder.Append(this.Axis1.DebuggerDisplay);
				stringBuilder.Append(" A2:");
				stringBuilder.Append(this.Axis2.DebuggerDisplay);
				stringBuilder.Append(" E:(");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.Extent0
				}));
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.Extent1
				}));
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.Extent2
				}));
				stringBuilder.Append("))");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003B4B File Offset: 0x00001D4B
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Box3)obj);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003B7C File Offset: 0x00001D7C
		public bool Equals(Box3 other)
		{
			return this.Center.Equals(other.Center) && this.Axis0.Equals(other.Axis0) && this.Axis1.Equals(other.Axis1) && this.Axis2.Equals(other.Axis2) && this.Extent0.Equals(other.Extent0) && this.Extent1.Equals(other.Extent1) && this.Extent2.Equals(other.Extent2);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003C30 File Offset: 0x00001E30
		public override int GetHashCode()
		{
			return this.Center.GetHashCode() ^ this.Axis0.GetHashCode() ^ this.Axis1.GetHashCode() ^ this.Axis2.GetHashCode() ^ this.Extent0.GetHashCode() ^ this.Extent1.GetHashCode() ^ this.Extent2.GetHashCode();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003CC0 File Offset: 0x00001EC0
		public ICollection<Vector3> ComputeVertices()
		{
			Vector3 right = this.Extent0 * this.Axis0;
			Vector3 right2 = this.Extent1 * this.Axis1;
			Vector3 right3 = this.Extent2 * this.Axis2;
			return new Vector3[]
			{
				this.Center - right - right2 - right3,
				this.Center + right - right2 - right3,
				this.Center + right + right2 - right3,
				this.Center - right + right2 - right3,
				this.Center - right - right2 + right3,
				this.Center + right - right2 + right3,
				this.Center + right + right2 + right3,
				this.Center - right + right2 + right3
			};
		}
	}
}
