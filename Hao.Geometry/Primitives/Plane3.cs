using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Plane3 : IEquatable<Plane3>
	{
	
		public Plane3(UnitVector3 normal, double constant)
		{
			this = default(Plane3);
			this.Normal = normal;
			this.Constant = constant;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00008F2C File Offset: 0x0000712C
		public Plane3(UnitVector3 planeNormal, Vector3 pointOnThePlane)
		{
			this = default(Plane3);
			this.Normal = planeNormal;
			this.Constant = this.Normal.Dot(pointOnThePlane);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008F5C File Offset: 0x0000715C
		public Plane3(Vector3 point0, Vector3 point1, Vector3 point2)
		{
			this = default(Plane3);
			Vector3 vector = point1 - point0;
			Vector3 vector2 = point2 - point0;
			this.Normal = vector.UnitCross(vector2);
			this.Constant = this.Normal.Dot(point0);
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00008FA3 File Offset: 0x000071A3
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00008FAB File Offset: 0x000071AB
		public UnitVector3 Normal { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00008FB4 File Offset: 0x000071B4
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00008FBC File Offset: 0x000071BC
		public double Constant { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00008FC8 File Offset: 0x000071C8
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(N:");
				stringBuilder.Append(this.Normal.DebuggerDisplay);
				stringBuilder.Append(" C:");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.Constant
				}));
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00009044 File Offset: 0x00007244
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Plane3 other = (Plane3)obj;
			return this.Equals(other);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00009084 File Offset: 0x00007284
		public bool Equals(Plane3 other)
		{
			return this.Normal.Equals(other.Normal) && this.Constant == other.Constant;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000090BC File Offset: 0x000072BC
		public override int GetHashCode()
		{
			return this.Normal.GetHashCode() ^ this.Constant.GetHashCode();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000090EC File Offset: 0x000072EC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Plane3{Normal:");
			stringBuilder.Append("{X:");
			stringBuilder.Append(this.Normal.X);
			stringBuilder.Append(" Y:");
			stringBuilder.Append(this.Normal.Y);
			stringBuilder.Append(" Z:");
			stringBuilder.Append(this.Normal.Z);
			stringBuilder.Append("}");
			stringBuilder.Append(" D:");
			stringBuilder.Append(this.Constant.ToString());
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
