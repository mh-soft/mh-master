using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Matrix3 : IEquatable<Matrix3>
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00005B30 File Offset: 0x00003D30
		public Matrix3(Vector3 column0, Vector3 column1, Vector3 column2)
		{
			this = default(Matrix3);
			this.Column0 = column0;
			this.Column1 = column1;
			this.Column2 = column2;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005B50 File Offset: 0x00003D50
		public Matrix3(double entry00, double entry11, double entry22)
		{
			this = default(Matrix3);
			this.Column0 = new Vector3(entry00, 0.0, 0.0);
			this.Column1 = new Vector3(0.0, entry11, 0.0);
			this.Column2 = new Vector3(0.0, 0.0, entry22);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005BBE File Offset: 0x00003DBE
		private Matrix3(double m00, double m01, double m02, double m10, double m11, double m12, double m20, double m21, double m22)
		{
			this = default(Matrix3);
			this.Column0 = new Vector3(m00, m10, m20);
			this.Column1 = new Vector3(m01, m11, m21);
			this.Column2 = new Vector3(m02, m12, m22);
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005BF7 File Offset: 0x00003DF7
		
		public static Matrix3 Zero
		{
			get
			{
				return new Matrix3(Vector3.Zero, Vector3.Zero, Vector3.Zero);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005C0D File Offset: 0x00003E0D
		
		public static Matrix3 Identity
		{
			get
			{
				return new Matrix3(Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005C23 File Offset: 0x00003E23
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00005C2B File Offset: 0x00003E2B
		public Vector3 Column0 { get; private set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005C34 File Offset: 0x00003E34
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00005C3C File Offset: 0x00003E3C
		public Vector3 Column1 { get; private set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00005C45 File Offset: 0x00003E45
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00005C4D File Offset: 0x00003E4D
		public Vector3 Column2 { get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00005C58 File Offset: 0x00003E58
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(C0:");
				stringBuilder.Append(this.Column0.DebuggerDisplay);
				stringBuilder.Append(" C1:");
				stringBuilder.Append(this.Column1.DebuggerDisplay);
				stringBuilder.Append(" C2:");
				stringBuilder.Append(this.Column2.DebuggerDisplay);
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000086 RID: 134
		public double this[int rowIndex, int columnIndex]
		{
			get
			{
				if (rowIndex < 0 || rowIndex > 2)
				{
					throw new KeyNotFoundException("Matrix3.[int,int].get: invalid row index");
				}
				switch (columnIndex)
				{
				case 0:
					switch (rowIndex)
					{
					case 0:
						return this.Column0.X;
					case 1:
						return this.Column0.Y;
					case 2:
						return this.Column0.Z;
					default:
						throw new KeyNotFoundException("Matrix3.[int,int].get: invalid column index");
					}
					break;
				case 1:
					switch (rowIndex)
					{
					case 0:
						return this.Column1.X;
					case 1:
						return this.Column1.Y;
					case 2:
						return this.Column1.Z;
					default:
						throw new KeyNotFoundException("Matrix3.[int,int].get: invalid column index");
					}
					break;
				case 2:
					switch (rowIndex)
					{
					case 0:
						return this.Column2.X;
					case 1:
						return this.Column2.Y;
					case 2:
						return this.Column2.Z;
					default:
						throw new KeyNotFoundException("Matrix3.[int,int].get: invalid column index");
					}
					break;
				default:
					throw new KeyNotFoundException("Matrix3.[int,int].get: invalid column index");
				}
			}
			set
			{
				switch (columnIndex)
				{
				case 0:
					switch (rowIndex)
					{
					case 0:
						this.Column0 = new Vector3(value, this.Column0.Y, this.Column0.Z);
						return;
					case 1:
						this.Column0 = new Vector3(this.Column0.X, value, this.Column0.Z);
						return;
					case 2:
						this.Column0 = new Vector3(this.Column0.X, this.Column0.Y, value);
						return;
					default:
						throw new KeyNotFoundException("Matrix3.[int,int].set: invalid row index " + rowIndex);
					}
					break;
				case 1:
					switch (rowIndex)
					{
					case 0:
						this.Column1 = new Vector3(value, this.Column1.Y, this.Column1.Z);
						return;
					case 1:
						this.Column1 = new Vector3(this.Column1.X, value, this.Column1.Z);
						return;
					case 2:
						this.Column1 = new Vector3(this.Column1.X, this.Column1.Y, value);
						return;
					default:
						throw new KeyNotFoundException("Matrix3.[int,int].set: invalid row index " + rowIndex);
					}
					break;
				case 2:
					switch (rowIndex)
					{
					case 0:
						this.Column2 = new Vector3(value, this.Column2.Y, this.Column2.Z);
						return;
					case 1:
						this.Column2 = new Vector3(this.Column2.X, value, this.Column2.Z);
						return;
					case 2:
						this.Column2 = new Vector3(this.Column2.X, this.Column2.Y, value);
						return;
					default:
						throw new KeyNotFoundException("Matrix3.[int,int].set: invalid row index " + rowIndex);
					}
					break;
				default:
					throw new KeyNotFoundException("Matrix3.[int,int].get: invalid column index " + columnIndex);
				}
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006030 File Offset: 0x00004230
		public static Matrix3 operator +(Matrix3 left, Matrix3 right)
		{
			return new Matrix3(left.Column0 + right.Column0, left.Column1 + right.Column1, left.Column2 + right.Column2);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006070 File Offset: 0x00004270
		public static Matrix3 operator -(Matrix3 left, Matrix3 right)
		{
			return new Matrix3(left.Column0 - right.Column0, left.Column1 - right.Column1, left.Column2 - right.Column2);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000060B0 File Offset: 0x000042B0
		public static Matrix3 operator *(Matrix3 left, Matrix3 right)
		{
			Vector3 column = left.Column0;
			Vector3 column2 = left.Column1;
			Vector3 column3 = left.Column2;
			Vector3 column4 = right.Column0;
			Vector3 column5 = right.Column1;
			Vector3 column6 = right.Column2;
			return new Matrix3(column.X * column4.X + column2.X * column4.Y + column3.X * column4.Z, column.X * column5.X + column2.X * column5.Y + column3.X * column5.Z, column.X * column6.X + column2.X * column6.Y + column3.X * column6.Z, column.Y * column4.X + column2.Y * column4.Y + column3.Y * column4.Z, column.Y * column5.X + column2.Y * column5.Y + column3.Y * column5.Z, column.Y * column6.X + column2.Y * column6.Y + column3.Y * column6.Z, column.Z * column4.X + column2.Z * column4.Y + column3.Z * column4.Z, column.Z * column5.X + column2.Z * column5.Y + column3.Z * column5.Z, column.Z * column6.X + column2.Z * column6.Y + column3.Z * column6.Z);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000629B File Offset: 0x0000449B
		public static Matrix3 operator *(double scalar, Matrix3 matrix)
		{
			return matrix * scalar;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000062A4 File Offset: 0x000044A4
		public static Matrix3 operator *(Matrix3 matrix, double scalar)
		{
			return new Matrix3(scalar * matrix.Column0, scalar * matrix.Column1, scalar * matrix.Column2);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000062D4 File Offset: 0x000044D4
		public static Matrix3 operator /(Matrix3 matrix, double scalar)
		{
			if (scalar != 0.0)
			{
				double scalar2 = 1.0 / scalar;
				return new Matrix3(scalar2 * matrix.Column0, scalar2 * matrix.Column1, scalar2 * matrix.Column2);
			}
			return new Matrix3(Vector3.MaxValue, Vector3.MaxValue, Vector3.MaxValue);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000633C File Offset: 0x0000453C
		public static Vector3 operator *(Matrix3 matrix, Vector3 vector)
		{
			return new Vector3(matrix.Column0.X * vector.X + matrix.Column1.X * vector.Y + matrix.Column2.X * vector.Z, matrix.Column0.Y * vector.X + matrix.Column1.Y * vector.Y + matrix.Column2.Y * vector.Z, matrix.Column0.Z * vector.X + matrix.Column1.Z * vector.Y + matrix.Column2.Z * vector.Z);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006423 File Offset: 0x00004623
		public void SetColumn(int index, Vector3 column)
		{
			switch (index)
			{
			case 0:
				this.Column0 = column;
				return;
			case 1:
				this.Column1 = column;
				return;
			case 2:
				this.Column2 = column;
				return;
			default:
				throw new KeyNotFoundException("Matrix3.SetColumn(): invalid column index");
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000645B File Offset: 0x0000465B
		public Vector3 GetColumn(int index)
		{
			switch (index)
			{
			case 0:
				return this.Column0;
			case 1:
				return this.Column1;
			case 2:
				return this.Column2;
			default:
				throw new KeyNotFoundException("Matrix3.GetColumn(): invalid column index");
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006490 File Offset: 0x00004690
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Matrix3)obj);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000064C0 File Offset: 0x000046C0
		public bool Equals(Matrix3 other)
		{
			return this.Column0.Equals(other.Column0) && this.Column1.Equals(other.Column1) && this.Column2.Equals(other.Column2);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006514 File Offset: 0x00004714
		public override int GetHashCode()
		{
			return this.Column0.GetHashCode() ^ this.Column1.GetHashCode() ^ this.Column2.GetHashCode();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006560 File Offset: 0x00004760
		public Matrix3 Transpose()
		{
			return new Matrix3(this.Column0.X, this.Column0.Y, this.Column0.Z, this.Column1.X, this.Column1.Y, this.Column1.Z, this.Column2.X, this.Column2.Y, this.Column2.Z);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000065F0 File Offset: 0x000047F0
		public Matrix3 Inverse()
		{
			Matrix3 matrix = new Matrix3(new Vector3(this.Column1.Y * this.Column2.Z - this.Column2.Y * this.Column1.Z, this.Column2.Y * this.Column0.Z - this.Column0.Y * this.Column2.Z, this.Column0.Y * this.Column1.Z - this.Column1.Y * this.Column0.Z), new Vector3(this.Column2.X * this.Column1.Z - this.Column1.X * this.Column2.Z, this.Column0.X * this.Column2.Z - this.Column2.X * this.Column0.Z, this.Column1.X * this.Column0.Z - this.Column0.X * this.Column1.Z), new Vector3(this.Column1.X * this.Column2.Y - this.Column2.X * this.Column1.Y, this.Column2.X * this.Column0.Y - this.Column0.X * this.Column2.Y, this.Column0.X * this.Column1.Y - this.Column1.X * this.Column0.Y));
			double num = this.Column0.X * matrix.Column0.X + this.Column1.X * matrix.Column0.Y + this.Column2.X * matrix.Column0.Z;
			if (Math.Abs(num) <= 1E-08)
			{
				return Matrix3.Zero;
			}
			double scalar = 1.0 / num;
			return new Matrix3(matrix.Column0 * scalar, matrix.Column1 * scalar, matrix.Column2 * scalar);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000068D4 File Offset: 0x00004AD4
		public Matrix3 Adjoint()
		{
			return new Matrix3(this.Column1.Y * this.Column2.Z - this.Column2.Y * this.Column1.Z, this.Column2.X * this.Column1.Z - this.Column1.X * this.Column2.Z, this.Column1.X * this.Column2.Y - this.Column2.X * this.Column1.Y, this.Column2.Y * this.Column0.Z - this.Column0.Y * this.Column2.Z, this.Column0.X * this.Column2.Z - this.Column2.X * this.Column0.Z, this.Column2.X * this.Column0.Y - this.Column0.X * this.Column2.Y, this.Column0.Y * this.Column1.Z - this.Column1.Y * this.Column0.Z, this.Column1.X * this.Column0.Z - this.Column0.X * this.Column1.Z, this.Column0.X * this.Column1.Y - this.Column1.X * this.Column0.Y);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006AFC File Offset: 0x00004CFC
		public double Determinant()
		{
			double num = this.Column1.Y * this.Column2.Z - this.Column2.Y * this.Column1.Z;
			double num2 = this.Column2.Y * this.Column0.Z - this.Column0.Y * this.Column2.Z;
			double num3 = this.Column0.Y * this.Column1.Z - this.Column1.Y * this.Column0.Z;
			return this.Column0.X * num + this.Column1.X * num2 + this.Column2.X * num3;
		}
	}
}
