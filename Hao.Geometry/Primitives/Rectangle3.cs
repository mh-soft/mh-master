using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Rectangle3 : IEquatable<Rectangle3>
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x00009C0C File Offset: 0x00007E0C
		public Rectangle3(Vector3 center, UnitVector3[] axis, double[] extent)
		{
			this = default(Rectangle3);
			if (axis.Length != 2 || extent.Length != 2)
			{
				throw new ArgumentException("Rectangle3 needs 2 axises and 2 extents");
			}
			MathBase.Assert(Math.Abs(axis[0].Dot(axis[1])) < 1E-08, "Rectangle3 constructor: axes not perpendicular.");
			this.Center = center;
			this.Axis0 = axis[0];
			this.Axis1 = axis[1];
			this.Extent0 = extent[0];
			this.Extent1 = extent[1];
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00009C98 File Offset: 0x00007E98
		public Rectangle3(Vector3 center, UnitVector3 axis0, UnitVector3 axis1, double extent0, double extent1)
		{
			this = default(Rectangle3);
			MathBase.Assert(Math.Abs(axis0.Dot(axis1)) < 1E-08, "Rectangle3 constructor: axes not perpendicular.");
			this.Axis0 = axis0;
			this.Axis1 = axis1;
			this.Extent0 = extent0;
			this.Extent1 = extent1;
			this.Center = center;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00009CF3 File Offset: 0x00007EF3
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00009CFB File Offset: 0x00007EFB
		public Vector3 Center { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00009D04 File Offset: 0x00007F04
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00009D0C File Offset: 0x00007F0C
		public UnitVector3 Axis0 { get; private set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00009D15 File Offset: 0x00007F15
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00009D1D File Offset: 0x00007F1D
		public UnitVector3 Axis1 { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00009D26 File Offset: 0x00007F26
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00009D2E File Offset: 0x00007F2E
		public double Extent0 { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00009D37 File Offset: 0x00007F37
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00009D3F File Offset: 0x00007F3F
		public double Extent1 { get; private set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00009D48 File Offset: 0x00007F48
		public Vector3 PPCorner
		{
			get
			{
				return this.Center + this.Extent0 * this.Axis0 + this.Extent1 * this.Axis1;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00009D7C File Offset: 0x00007F7C
		public Vector3 PMCorner
		{
			get
			{
				return this.Center + this.Extent0 * this.Axis0 - this.Extent1 * this.Axis1;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00009DB0 File Offset: 0x00007FB0
		public Vector3 MPCorner
		{
			get
			{
				return this.Center - this.Extent0 * this.Axis0 + this.Extent1 * this.Axis1;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00009DE4 File Offset: 0x00007FE4
		public Vector3 MMCorner
		{
			get
			{
				return this.Center - this.Extent0 * this.Axis0 - this.Extent1 * this.Axis1;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00009E18 File Offset: 0x00008018
		
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

		// Token: 0x060001C3 RID: 451 RVA: 0x00009F0C File Offset: 0x0000810C
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Rectangle3)obj);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009F3C File Offset: 0x0000813C
		public bool Equals(Rectangle3 other)
		{
			return this.Center.Equals(other.Center) && this.Axis0.Equals(other.Axis0) && this.Axis1.Equals(other.Axis1) && this.Extent0.Equals(other.Extent0) && this.Extent1.Equals(other.Extent1);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009FBC File Offset: 0x000081BC
		public override int GetHashCode()
		{
			return this.Center.GetHashCode() ^ this.Axis0.GetHashCode() ^ this.Axis1.GetHashCode() ^ this.Extent0.GetHashCode() ^ this.Extent1.GetHashCode();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000A028 File Offset: 0x00008228
		public Vector3[] ComputeVertices()
		{
			Vector3 right = this.Extent0 * this.Axis0;
			Vector3 right2 = this.Extent1 * this.Axis1;
			return new Vector3[]
			{
				this.Center - right - right2,
				this.Center + right - right2,
				this.Center + right + right2,
				this.Center - right + right2
			};
		}
	}
}
