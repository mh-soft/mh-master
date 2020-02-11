using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Vector3 : IEquatable<Vector3>
	{
		/// <summary>
		/// 顶点坐标
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public Vector3(double x, double y, double z)
		{
			MathBase.AssertValid(x);
			MathBase.AssertValid(y);
			MathBase.AssertValid(z);
			this.x = x;
			this.y = y;
			this.z = z;
		}


		
		public static Vector3 Zero
		{
			get
			{
				return new Vector3(0.0, 0.0, 0.0);
			}
		}

		
		public static Vector3 UnitX
		{
			get
			{
				return new Vector3(1.0, 0.0, 0.0);
			}
		}



        
		public static Vector3 UnitY
		{
			get
			{
				return new Vector3(0.0, 1.0, 0.0);
			}
		}

		
		public static Vector3 UnitZ
		{
			get
			{
				return new Vector3(0.0, 0.0, 1.0);
			}
		}

		
		public static Vector3 One
		{
			get
			{
				return new Vector3(1.0, 1.0, 1.0);
			}
		}

		
		public static Vector3 MinValue
		{
			get
			{
				return new Vector3(double.MinValue, double.MinValue, double.MinValue);
			}
		}


		
		public static Vector3 MaxValue
		{
			get
			{
				return new Vector3(double.MaxValue, double.MaxValue, double.MaxValue);
			}
		}


		public double X
		{
			get
			{
				return this.x;
			}
			set
			{
				MathBase.AssertValid(value);
				this.x = value;
			}
		}

		public double Y
		{
			get
			{
				return this.y;
			}
			set
			{
				MathBase.AssertValid(value);
				this.y = value;
			}
		}


		public double Z
		{
			get
			{
				return this.z;
			}
			set
			{
				MathBase.AssertValid(value);
				this.z = value;
			}
		}

		public double Length
		{
			get
			{
				return Math.Sqrt(this.SquaredLength);
			}
		}

		public double SquaredLength
		{
			get
			{
				return this.X * this.X + this.Y * this.Y + this.Z * this.Z;
			}
		}

		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.VectorComponent, new object[]
				{
					this.x
				}));
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.VectorComponent, new object[]
				{
					this.y
				}));
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, Format.DebugView.VectorComponent, new object[]
				{
					this.z
				}));
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}


		public double this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.x;
				case 1:
					return this.y;
				case 2:
					return this.z;
				default:
					throw new KeyNotFoundException("Vector3, index out of range");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					return;
				case 1:
					this.y = value;
					return;
				case 2:
					this.z = value;
					return;
				default:
					throw new KeyNotFoundException("Vector3, index out of range");
				}
			}
		}

		public static Vector3 Min(Vector3 value1, Vector3 value2)
		{
			return new Vector3(Math.Min(value1.X, value2.X), Math.Min(value1.Y, value2.Y), Math.Min(value1.Z, value2.Z));
		}

		public static Vector3 Max(Vector3 value1, Vector3 value2)
		{
			return new Vector3(Math.Max(value1.X, value2.X), Math.Max(value1.Y, value2.Y), Math.Max(value1.Z, value2.Z));
		}

		public static Vector3 operator +(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}

		public static Vector3 operator -(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}

		public static Vector3 operator *(Vector3 vector, double scalar)
		{
			return new Vector3(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
		}

		public static Vector3 operator *(double scalar, Vector3 vector)
		{
			return new Vector3(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
		}

		public static Vector3 operator *(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}

		public static Vector3 operator -(Vector3 vector)
		{
			return new Vector3(-vector.X, -vector.Y, -vector.Z);
		}

		public static Vector3 operator /(Vector3 vector, double scalar)
		{
			return new Vector3(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);
		}

		public static Vector3 operator /(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
		}

		public double Dot(Vector3 vector)
		{
			return vector.X * this.X + vector.Y * this.Y + vector.Z * this.Z;
		}


		public double Dot(UnitVector3 vector)
		{
			return vector.X * this.X + vector.Y * this.Y + vector.Z * this.Z;
		}

		public UnitVector3 GetNormalized()
		{
			double length = this.Length;
			if (length > 1E-08)
			{
				return new UnitVector3(this.X / length, this.Y / length, this.Z / length);
			}
			throw new InvalidOperationException("length zero, cannot be normalized.");
		}

		public bool TryGetNormalized(out UnitVector3 normalized)
		{
			double length = this.Length;
			if (length > 1E-08)
			{
				normalized = new UnitVector3(this.X / length, this.Y / length, this.Z / length);
				return true;
			}
			normalized = UnitVector3.UnitX;
			return false;
		}

		public Vector3 Cross(Vector3 vector)
		{
			return new Vector3(this.Y * vector.Z - this.Z * vector.Y, this.Z * vector.X - this.X * vector.Z, this.X * vector.Y - this.Y * vector.X);
		}


		public Vector3 Cross(UnitVector3 vector)
		{
			return this.Cross((Vector3)vector);
		}


		public UnitVector3 UnitCross(Vector3 vector)
		{
			return this.Cross(vector).GetNormalized();
		}

		public UnitVector3 UnitCross(UnitVector3 vector)
		{
			return this.UnitCross((Vector3)vector);
		}


		public bool TryGetUnitCross(Vector3 vector, out UnitVector3 result)
		{
			return this.Cross(vector).TryGetNormalized(out result);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Vector3 other = (Vector3)obj;
			return this.Equals(other);
		}

		public bool Equals(Vector3 other)
		{
			return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
		}


		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Vector3{X:");
			stringBuilder.Append(this.X);
			stringBuilder.Append(" Y:");
			stringBuilder.Append(this.Y);
			stringBuilder.Append(" Z:");
			stringBuilder.Append(this.Z);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}


		
		private double x;

		
		private double y;


		
		private double z;
	}
}
