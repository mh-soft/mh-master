using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{
	// Token: 0x02000007 RID: 7

	[Serializable]
	public struct AxisAlignedBox3 : IEquatable<AxisAlignedBox3>
	{

		public AxisAlignedBox3(double minX, double maxX, double minY, double maxY, double minZ, double maxZ)
		{
			this = default(AxisAlignedBox3);
			this.MinX = minX;
			this.MaxX = maxX;
			this.MinY = minY;
			this.MaxY = maxY;
			this.MinZ = minZ;
			this.MaxZ = maxZ;
		}



        // Token: 0x06000066 RID: 102 RVA: 0x00002F6C File Offset: 0x0000116C
        public AxisAlignedBox3(Vector3 min, Vector3 max)
		{
			this = default(AxisAlignedBox3);
			this.MinX = min.X;
			this.MaxX = max.X;
			this.MinY = min.Y;
			this.MaxY = max.Y;
			this.MinZ = min.Z;
			this.MaxZ = max.Z;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002FCE File Offset: 0x000011CE
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002FD6 File Offset: 0x000011D6
		public double MinX { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002FDF File Offset: 0x000011DF
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002FE7 File Offset: 0x000011E7
		public double MaxX { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002FF0 File Offset: 0x000011F0
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002FF8 File Offset: 0x000011F8
		public double MinY { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003001 File Offset: 0x00001201
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003009 File Offset: 0x00001209
		public double MaxY { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003012 File Offset: 0x00001212
		// (set) Token: 0x06000070 RID: 112 RVA: 0x0000301A File Offset: 0x0000121A
		public double MinZ { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003023 File Offset: 0x00001223
		// (set) Token: 0x06000072 RID: 114 RVA: 0x0000302B File Offset: 0x0000122B
		public double MaxZ { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003034 File Offset: 0x00001234
		public Vector3 Center
		{
			get
			{
				return new Vector3
				{
					X = 0.5 * (this.MaxX + this.MinX),
					Y = 0.5 * (this.MaxY + this.MinY),
					Z = 0.5 * (this.MaxZ + this.MinZ)
				};
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000030A4 File Offset: 0x000012A4
		
		public Vector3 Extents
		{
			get
			{
				return new Vector3((this.MaxX - this.MinX) * 0.5, (this.MaxY - this.MinY) * 0.5, (this.MaxZ - this.MinZ) * 0.5);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000030FB File Offset: 0x000012FB
		public double ExtentX
		{
			get
			{
				return 0.5 * (this.MaxX - this.MinX);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003114 File Offset: 0x00001314
		public double ExtentY
		{
			get
			{
				return 0.5 * (this.MaxY - this.MinY);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000312D File Offset: 0x0000132D
		public double ExtentZ
		{
			get
			{
				return 0.5 * (this.MaxZ - this.MinZ);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003146 File Offset: 0x00001346
		public Vector3 Min
		{
			get
			{
				return new Vector3(this.MinX, this.MinY, this.MinZ);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000315F File Offset: 0x0000135F
		public Vector3 Max
		{
			get
			{
				return new Vector3(this.MaxX, this.MaxY, this.MaxZ);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003178 File Offset: 0x00001378
		
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
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.MinZ
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
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.MaxZ
				}));
				stringBuilder.Append("))");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000032E1 File Offset: 0x000014E1
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((AxisAlignedBox3)obj);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003314 File Offset: 0x00001514
		public bool Equals(AxisAlignedBox3 other)
		{
			return this.Min.Equals(other.Min) && this.Max.Equals(other.Max);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003350 File Offset: 0x00001550
		public override int GetHashCode()
		{
			return this.Min.GetHashCode() ^ this.Max.GetHashCode();
		}
	}
}
