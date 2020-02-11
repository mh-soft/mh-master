using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Ray3 : IEquatable<Ray3>
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x000099B6 File Offset: 0x00007BB6
		public Ray3(Vector3 origin, UnitVector3 direction)
		{
			this = default(Ray3);
			this.Origin = origin;
			this.Direction = direction;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000099CD File Offset: 0x00007BCD
		// (set) Token: 0x060001AA RID: 426 RVA: 0x000099D5 File Offset: 0x00007BD5
		public Vector3 Origin { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000099DE File Offset: 0x00007BDE
		// (set) Token: 0x060001AC RID: 428 RVA: 0x000099E6 File Offset: 0x00007BE6
		public UnitVector3 Direction { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000099F0 File Offset: 0x00007BF0
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(O:");
				stringBuilder.Append(this.Origin.DebuggerDisplay);
				stringBuilder.Append(" D:");
				stringBuilder.Append(this.Direction.DebuggerDisplay);
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00009A55 File Offset: 0x00007C55
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Ray3)obj);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00009A88 File Offset: 0x00007C88
		public bool Equals(Ray3 other)
		{
			return this.Origin.Equals(other.Origin) && this.Direction.Equals(other.Direction);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00009AC4 File Offset: 0x00007CC4
		public override int GetHashCode()
		{
			return this.Origin.GetHashCode() ^ this.Direction.GetHashCode();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00009AFC File Offset: 0x00007CFC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Ray3{Origin");
			stringBuilder.Append("{X:");
			stringBuilder.Append(this.Origin.X);
			stringBuilder.Append(" Y:");
			stringBuilder.Append(this.Origin.Y);
			stringBuilder.Append(" Z:");
			stringBuilder.Append(this.Origin.Z);
			stringBuilder.Append("}");
			stringBuilder.Append(" Direction");
			stringBuilder.Append("{X:");
			stringBuilder.Append(this.Direction.X);
			stringBuilder.Append(" Y:");
			stringBuilder.Append(this.Direction.Y);
			stringBuilder.Append(" Z:");
			stringBuilder.Append(this.Direction.Z);
			stringBuilder.Append("}}");
			return stringBuilder.ToString();
		}
	}
}
