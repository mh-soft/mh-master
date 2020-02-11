using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Circle3 : IEquatable<Circle3>
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00003FF8 File Offset: 0x000021F8
		public Circle3(Vector3 center, UnitVector3 unitU, UnitVector3 unitV, UnitVector3 normal, double radius)
		{
			this = default(Circle3);
			MathBase.Assert(Math.Abs(1.0 - unitU.Cross(unitV).Dot(normal)) < 1E-08, "Circle3 constructor: axes must be perpendicular and right-handed.");
			this.Center = center;
			this.UnitU = unitU;
			this.UnitV = unitV;
			this.Normal = normal;
			this.Radius = radius;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004067 File Offset: 0x00002267
		
		public static Circle3 UnitCircle
		{
			get
			{
				return new Circle3(Vector3.Zero, UnitVector3.UnitX, UnitVector3.UnitY, UnitVector3.UnitZ, 1.0);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000408B File Offset: 0x0000228B
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00004093 File Offset: 0x00002293
		public Vector3 Center { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000409C File Offset: 0x0000229C
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000040A4 File Offset: 0x000022A4
		public UnitVector3 UnitU { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000040AD File Offset: 0x000022AD
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000040B5 File Offset: 0x000022B5
		public UnitVector3 UnitV { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000040BE File Offset: 0x000022BE
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000040C6 File Offset: 0x000022C6
		public UnitVector3 Normal { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000040CF File Offset: 0x000022CF
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000040D7 File Offset: 0x000022D7
		public double Radius { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000040E0 File Offset: 0x000022E0
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(C:");
				stringBuilder.Append(this.Center.DebuggerDisplay);
				stringBuilder.Append(" U:");
				stringBuilder.Append(this.UnitU.DebuggerDisplay);
				stringBuilder.Append(" V:");
				stringBuilder.Append(this.UnitV.DebuggerDisplay);
				stringBuilder.Append(" N:");
				stringBuilder.Append(this.Normal.DebuggerDisplay);
				stringBuilder.Append(" R:");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.Radius
				}));
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000041BF File Offset: 0x000023BF
		public Vector3 GetEdgePoint(Angle angle)
		{
			return this.Center + this.Radius * this.GetRadiusDirection(angle);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000041E0 File Offset: 0x000023E0
		public UnitVector3 GetRadiusDirection(Angle angle)
		{
			Vector3 vector = this.UnitU * angle.Cos + this.UnitV * angle.Sin;
			return new UnitVector3(vector.X, vector.Y, vector.Z);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004234 File Offset: 0x00002434
		public Line3 GetTangent(Angle angle)
		{
			Vector3 vector = -this.UnitU * angle.Sin + this.UnitV * angle.Cos;
			return new Line3(this.GetEdgePoint(angle), new UnitVector3(vector.X, vector.Y, vector.Z));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004296 File Offset: 0x00002496
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Circle3)obj);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000042C8 File Offset: 0x000024C8
		public bool Equals(Circle3 other)
		{
			return this.Center.Equals(other.Center) && this.UnitU.Equals(other.UnitU) && this.UnitV.Equals(other.UnitV) && this.Normal.Equals(other.Normal) && this.Radius == other.Radius;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004344 File Offset: 0x00002544
		public override int GetHashCode()
		{
			return this.Center.GetHashCode() ^ this.UnitU.GetHashCode() ^ this.UnitV.GetHashCode() ^ this.Normal.GetHashCode() ^ this.Radius.GetHashCode();
		}
	}
}
