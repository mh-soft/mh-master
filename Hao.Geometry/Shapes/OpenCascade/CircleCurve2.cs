using System;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000011 RID: 17
	public class CircleCurve2 : ICurve2
	{
		// Token: 0x060000BA RID: 186 RVA: 0x000047EE File Offset: 0x000029EE
		public CircleCurve2(Vector2 origin, UnitVector2 unitX, UnitVector2 unitY, double radius)
		{
			this.Origin = origin;
			this.UnitX = unitX;
			this.UnitY = unitY;
			this.Radius = radius;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004813 File Offset: 0x00002A13
		public CircleCurve2(Circle2 circle)
		{
			this.Origin = circle.Center;
			this.UnitX = UnitVector2.UnitX;
			this.UnitY = UnitVector2.UnitY;
			this.Radius = circle.Radius;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000484B File Offset: 0x00002A4B
		public Circle2 Circle
		{
			get
			{
				return new Circle2(this.Origin, this.Radius);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000485E File Offset: 0x00002A5E
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00004866 File Offset: 0x00002A66
		public Vector2 Origin { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000486F File Offset: 0x00002A6F
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00004877 File Offset: 0x00002A77
		public UnitVector2 UnitX { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004880 File Offset: 0x00002A80
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00004888 File Offset: 0x00002A88
		public UnitVector2 UnitY { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004891 File Offset: 0x00002A91
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00004899 File Offset: 0x00002A99
		public double Radius { get; private set; }

		// Token: 0x060000C5 RID: 197 RVA: 0x000048A2 File Offset: 0x00002AA2
		public Vector2 EvalAt(double parameter)
		{
			return this.Origin + this.Radius * (Math.Cos(parameter) * this.UnitX + Math.Sin(parameter) * this.UnitY);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000048E1 File Offset: 0x00002AE1
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((CircleCurve2)obj);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004908 File Offset: 0x00002B08
		public bool Equals(CircleCurve2 other)
		{
			return this.Origin.Equals(other.Origin) && this.UnitX.Equals(other.UnitX) && this.UnitY.Equals(other.UnitY) && this.Radius.Equals(other.Radius);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004970 File Offset: 0x00002B70
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.UnitX.GetHashCode() ^ this.UnitY.GetHashCode() ^ this.Radius.GetHashCode();
		}
	}
}
