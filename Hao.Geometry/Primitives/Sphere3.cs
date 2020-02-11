using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Sphere3 : IEquatable<Sphere3>
	{

		public Sphere3(Vector3 center, double radius)
		{
			this = default(Sphere3);
			this.Center = center;
			this.Radius = radius;
		}


		public Vector3 Center { get; private set; }


		public double Radius { get; private set; }

		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(C:");
				stringBuilder.Append(this.Center.DebuggerDisplay);
				stringBuilder.Append(" R:");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.DoubleComponent, new object[]
				{
					this.Radius
				}));
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Sphere3 other = (Sphere3)obj;
			return this.Equals(other);
		}

		public bool Equals(Sphere3 other)
		{
			return this.Center.Equals(other.Center) && this.Radius == other.Radius;
		}

		public override int GetHashCode()
		{
			return this.Center.GetHashCode() ^ this.Radius.GetHashCode();
		}
	}
}
