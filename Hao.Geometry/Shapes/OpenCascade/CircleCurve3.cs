using System;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000012 RID: 18
	public class CircleCurve3 : ICurve3
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x000049CA File Offset: 0x00002BCA
		public CircleCurve3(Vector3 origin, UnitVector3 unitX, UnitVector3 unitY, double radius)
		{
			this.Origin = origin;
			this.UnitX = unitX;
			this.UnitY = unitY;
			this.Radius = radius;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000049EF File Offset: 0x00002BEF
		public CircleCurve3(Circle3 circle)
		{
			this.Origin = circle.Center;
			this.UnitX = circle.UnitU;
			this.UnitY = circle.UnitV;
			this.Radius = circle.Radius;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004A2B File Offset: 0x00002C2B
		public Circle3 Circle
		{
			get
			{
				return new Circle3(this.Origin, this.UnitX, this.UnitY, this.Normal, this.Radius);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004A50 File Offset: 0x00002C50
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00004A58 File Offset: 0x00002C58
		public Vector3 Origin { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004A61 File Offset: 0x00002C61
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00004A69 File Offset: 0x00002C69
		public UnitVector3 UnitX { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004A72 File Offset: 0x00002C72
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00004A7A File Offset: 0x00002C7A
		public UnitVector3 UnitY { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004A84 File Offset: 0x00002C84
		public UnitVector3 Normal
		{
			get
			{
				return this.UnitX.UnitCross(this.UnitY);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004AA5 File Offset: 0x00002CA5
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00004AAD File Offset: 0x00002CAD
		public double Radius { get; private set; }

		// Token: 0x060000D5 RID: 213 RVA: 0x00004AB6 File Offset: 0x00002CB6
		public Vector3 EvalAt(double parameter)
		{
			return this.Origin + this.Radius * (Math.Cos(parameter) * this.UnitX + Math.Sin(parameter) * this.UnitY);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004AF5 File Offset: 0x00002CF5
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((CircleCurve3)obj);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004B1C File Offset: 0x00002D1C
		public bool Equals(CircleCurve3 other)
		{
			return this.Origin.Equals(other.Origin) && this.UnitX.Equals(other.UnitX) && this.UnitY.Equals(other.UnitY) && this.Normal.Equals(other.Normal) && this.Radius.Equals(other.Radius);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004B98 File Offset: 0x00002D98
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.UnitX.GetHashCode() ^ this.UnitY.GetHashCode() ^ this.Normal.GetHashCode() ^ this.Radius.GetHashCode();
		}
	}
}
