using System;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000021 RID: 33
	public class LineCurve3 : ICurve3
	{
		// Token: 0x06000181 RID: 385 RVA: 0x000065B6 File Offset: 0x000047B6
		public LineCurve3(Vector3 origin, UnitVector3 direction)
		{
			this.Origin = origin;
			this.Direction = direction;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000065CC File Offset: 0x000047CC
		public LineCurve3(Line3 line)
		{
			this.Origin = line.Origin;
			this.Direction = line.Direction;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000065EE File Offset: 0x000047EE
		public Line3 Line
		{
			get
			{
				return new Line3(this.Origin, this.Direction);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00006601 File Offset: 0x00004801
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00006609 File Offset: 0x00004809
		public Vector3 Origin { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00006612 File Offset: 0x00004812
		// (set) Token: 0x06000187 RID: 391 RVA: 0x0000661A File Offset: 0x0000481A
		public UnitVector3 Direction { get; private set; }

		// Token: 0x06000188 RID: 392 RVA: 0x00006623 File Offset: 0x00004823
		public Vector3 EvalAt(double parameter)
		{
			return this.Origin + parameter * this.Direction;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00002626 File Offset: 0x00000826
		public bool Parameter(Vector3 value, out double parameter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000663C File Offset: 0x0000483C
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			LineCurve3 other = (LineCurve3)obj;
			return this.Equals(other);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00006670 File Offset: 0x00004870
		public bool Equals(LineCurve3 other)
		{
			return this.Origin.Equals(other.Origin) && this.Direction.Equals(other.Direction);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000066AC File Offset: 0x000048AC
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.Direction.GetHashCode();
		}
	}
}
