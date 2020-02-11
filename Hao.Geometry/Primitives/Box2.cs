using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{
	// Token: 0x02000008 RID: 8
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[Guid("932326B3-D828-4584-B152-41E4CF59C6C5")]
	[Serializable]
	public struct Box2 : IEquatable<Box2>
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00003388 File Offset: 0x00001588
		public Box2(Vector2 center, UnitVector2[] axis, double[] extent)
		{
			this = default(Box2);
			if (axis.Length != 2 || extent.Length != 2)
			{
				throw new ArgumentException("Box2 needs 2 axises and 2 extents");
			}
			MathBase.Assert(Math.Abs(axis[0].Dot(axis[1])) < 1E-08, "Box2 constructor: axes not perpendicular.");
			if (axis[0].Dot(UnitVector2.UnitX) >= 0.0)
			{
				MathBase.Assert(axis[1].Dot(UnitVector2.UnitY) >= 0.0, "Box2 constructor: axes not right-handed.");
			}
			else
			{
				MathBase.Assert(axis[1].Dot(UnitVector2.UnitY) < 0.0, "Box2 constructor: axes not right-handed.");
			}
			this.Center = center;
			this.Axis0 = axis[0];
			this.Axis1 = axis[1];
			this.Extent0 = extent[0];
			this.Extent1 = extent[1];
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003480 File Offset: 0x00001680
		public Box2(Vector2 center, UnitVector2 axis0, UnitVector2 axis1, double extent0, double extent1)
		{
			this = default(Box2);
			MathBase.Assert(Math.Abs(axis0.Dot(axis1)) < 1E-08, "Box2 constructor: axes not perpendicular.");
			if (axis0.Dot(UnitVector2.UnitX) >= 0.0)
			{
				MathBase.Assert(axis1.Dot(UnitVector2.UnitY) >= 0.0, "Box2 constructor: axes not right-handed.");
			}
			else
			{
				MathBase.Assert(axis1.Dot(UnitVector2.UnitY) < 0.0, "Box2 constructor: axes not right-handed.");
			}
			this.Center = center;
			this.Axis0 = axis0;
			this.Axis1 = axis1;
			this.Extent0 = extent0;
			this.Extent1 = extent1;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003539 File Offset: 0x00001739
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003541 File Offset: 0x00001741
		public Vector2 Center { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000354A File Offset: 0x0000174A
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003552 File Offset: 0x00001752
		public UnitVector2 Axis0 { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000355B File Offset: 0x0000175B
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003563 File Offset: 0x00001763
		public UnitVector2 Axis1 { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000356C File Offset: 0x0000176C
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00003574 File Offset: 0x00001774
		public double Extent0 { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000357D File Offset: 0x0000177D
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003585 File Offset: 0x00001785
		public double Extent1 { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003590 File Offset: 0x00001790
		
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
				stringBuilder.Append("))");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003684 File Offset: 0x00001884
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Box2 other = (Box2)obj;
			return this.Equals(other);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000036C4 File Offset: 0x000018C4
		public bool Equals(Box2 other)
		{
			return this.Center.Equals(other.Center) && this.Axis0.Equals(other.Axis0) && this.Axis1.Equals(other.Axis1) && this.Extent0.Equals(other.Extent0) && this.Extent1.Equals(other.Extent1);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003744 File Offset: 0x00001944
		public override int GetHashCode()
		{
			return this.Center.GetHashCode() ^ this.Axis0.GetHashCode() ^ this.Axis1.GetHashCode() ^ this.Extent0.GetHashCode() ^ this.Extent1.GetHashCode();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000037B0 File Offset: 0x000019B0
		public ICollection<Vector2> ComputeVertices()
		{
			Vector2 right = this.Extent0 * this.Axis0;
			Vector2 right2 = this.Extent1 * this.Axis1;
			return new Vector2[]
			{
				this.Center - right - right2,
				this.Center + right - right2,
				this.Center + right + right2,
				this.Center - right + right2
			};
		}
	}
}
