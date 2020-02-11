using System;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000020 RID: 32
	public class LineCurve2 : ICurve2
	{
		// Token: 0x06000176 RID: 374 RVA: 0x00006489 File Offset: 0x00004689
		public LineCurve2(Vector2 origin, UnitVector2 direction)
		{
			this.Origin = origin;
			this.Direction = direction;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000649F File Offset: 0x0000469F
		public LineCurve2(Line2 line)
		{
			this.Origin = line.Origin;
			this.Direction = line.Direction;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000064C1 File Offset: 0x000046C1
		public Line2 Line
		{
			get
			{
				return new Line2(this.Origin, this.Direction);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000064D4 File Offset: 0x000046D4
		// (set) Token: 0x0600017A RID: 378 RVA: 0x000064DC File Offset: 0x000046DC
		public Vector2 Origin { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000064E5 File Offset: 0x000046E5
		// (set) Token: 0x0600017C RID: 380 RVA: 0x000064ED File Offset: 0x000046ED
		public UnitVector2 Direction { get; private set; }

		// Token: 0x0600017D RID: 381 RVA: 0x000064F6 File Offset: 0x000046F6
		public Vector2 EvalAt(double parameter)
		{
			return this.Origin + parameter * this.Direction;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006510 File Offset: 0x00004710
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			LineCurve2 other = (LineCurve2)obj;
			return this.Equals(other);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006544 File Offset: 0x00004744
		public bool Equals(LineCurve2 other)
		{
			return this.Origin.Equals(other.Origin) && this.Direction.Equals(other.Direction);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006580 File Offset: 0x00004780
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.Direction.GetHashCode();
		}
	}
}
