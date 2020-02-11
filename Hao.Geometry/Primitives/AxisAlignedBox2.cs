using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{
	// Token: 0x02000006 RID: 6
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[Guid("C662D4CD-08D5-4e13-A23C-7CFDBA700D87")]
	[Serializable]
	public struct AxisAlignedBox2 : IEquatable<AxisAlignedBox2>
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002C0B File Offset: 0x00000E0B
		public AxisAlignedBox2(double minX, double maxX, double minY, double maxY)
		{
			this = default(AxisAlignedBox2);
			this.MinX = minX;
			this.MaxX = maxX;
			this.MinY = minY;
			this.MaxY = maxY;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C31 File Offset: 0x00000E31
		public AxisAlignedBox2(Vector2 min, Vector2 max)
		{
			this = default(AxisAlignedBox2);
			this.MinX = min.X;
			this.MaxX = max.X;
			this.MinY = min.Y;
			this.MaxY = max.Y;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002C6E File Offset: 0x00000E6E
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002C76 File Offset: 0x00000E76
		public double MinX { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002C7F File Offset: 0x00000E7F
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002C87 File Offset: 0x00000E87
		public double MaxX { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002C90 File Offset: 0x00000E90
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002C98 File Offset: 0x00000E98
		public double MinY { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002CA1 File Offset: 0x00000EA1
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002CA9 File Offset: 0x00000EA9
		public double MaxY { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002CB2 File Offset: 0x00000EB2
		public Vector2 Center
		{
			get
			{
				return new Vector2(0.5 * (this.MaxX + this.MinX), 0.5 * (this.MaxY + this.MinY));
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002CE7 File Offset: 0x00000EE7
		public double ExtentX
		{
			get
			{
				return 0.5 * (this.MaxX - this.MinX);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002D00 File Offset: 0x00000F00
		public double ExtentY
		{
			get
			{
				return 0.5 * (this.MaxY - this.MinY);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002D19 File Offset: 0x00000F19
		
		public Vector2 Extents
		{
			get
			{
				return new Vector2(0.5 * (this.MaxX - this.MinX), 0.5 * (this.MaxY - this.MinY));
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002D50 File Offset: 0x00000F50
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(Min:(");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.MinX
				}));
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.MinY
				}));
				stringBuilder.Append(") Max:(");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.MaxX
				}));
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.MaxY
				}));
				stringBuilder.Append("))");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002E4D File Offset: 0x0000104D
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((AxisAlignedBox2)obj);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002E80 File Offset: 0x00001080
		public bool Equals(AxisAlignedBox2 other)
		{
			return this.MinX.Equals(other.MinX) && this.MinY.Equals(other.MinY) && this.MaxX.Equals(other.MaxX) && this.MaxY.Equals(other.MaxY);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002EEC File Offset: 0x000010EC
		public override int GetHashCode()
		{
			return this.MinX.GetHashCode() ^ this.MinY.GetHashCode() ^ this.MaxX.GetHashCode() ^ this.MaxY.GetHashCode();
		}
	}
}
