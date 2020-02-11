using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Matrix4 : IEquatable<Matrix4>
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00006BEF File Offset: 0x00004DEF
		public Matrix4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
		{
			this = default(Matrix4);
			this.Column0 = column0;
			this.Column1 = column1;
			this.Column2 = column2;
			this.Column3 = column3;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006C18 File Offset: 0x00004E18
		public Matrix4(double entry00, double entry11, double entry22, double entry33)
		{
			this = default(Matrix4);
			this.Column0 = new Vector4(entry00, 0.0, 0.0, 0.0);
			this.Column1 = new Vector4(0.0, entry11, 0.0, 0.0);
			this.Column2 = new Vector4(0.0, 0.0, entry22, 0.0);
			this.Column3 = new Vector4(0.0, 0.0, 0.0, entry33);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006CCC File Offset: 0x00004ECC
		private Matrix4(double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13, double m20, double m21, double m22, double m23, double m30, double m31, double m32, double m33)
		{
			this = default(Matrix4);
			this.Column0 = new Vector4(m00, m10, m20, m30);
			this.Column1 = new Vector4(m01, m11, m21, m31);
			this.Column2 = new Vector4(m02, m12, m22, m32);
			this.Column3 = new Vector4(m03, m13, m23, m33);
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00006D29 File Offset: 0x00004F29
		
		public static Matrix4 Zero
		{
			get
			{
				return new Matrix4(Vector4.Zero, Vector4.Zero, Vector4.Zero, Vector4.Zero);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00006D44 File Offset: 0x00004F44
		
		public static Matrix4 Identity
		{
			get
			{
				return new Matrix4(Vector4.UnitX, Vector4.UnitY, Vector4.UnitZ, Vector4.UnitW);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00006D5F File Offset: 0x00004F5F
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00006D67 File Offset: 0x00004F67
		public Vector4 Column0 { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00006D70 File Offset: 0x00004F70
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00006D78 File Offset: 0x00004F78
		public Vector4 Column1 { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00006D81 File Offset: 0x00004F81
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00006D89 File Offset: 0x00004F89
		public Vector4 Column2 { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00006D92 File Offset: 0x00004F92
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00006D9A File Offset: 0x00004F9A
		public Vector4 Column3 { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00006DA4 File Offset: 0x00004FA4
		
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
				stringBuilder.Append(" C3:");
				stringBuilder.Append(this.Column3.DebuggerDisplay);
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x1700008E RID: 142
		public double this[int rowIndex, int columnIndex]
		{
			get
			{
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
					case 3:
						return this.Column0.W;
					default:
						throw new KeyNotFoundException("Matrix4.[int,int].get: invalid row index " + rowIndex);
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
					case 3:
						return this.Column1.W;
					default:
						throw new KeyNotFoundException("Matrix4.[int,int].get: invalid row index" + rowIndex);
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
					case 3:
						return this.Column2.W;
					default:
						throw new KeyNotFoundException("Matrix4.[int,int].get: invalid row index " + rowIndex);
					}
					break;
				case 3:
					switch (rowIndex)
					{
					case 0:
						return this.Column3.X;
					case 1:
						return this.Column3.Y;
					case 2:
						return this.Column3.Z;
					case 3:
						return this.Column3.W;
					default:
						throw new KeyNotFoundException("Matrix4.[int,int].get: invalid row index " + rowIndex);
					}
					break;
				default:
					throw new KeyNotFoundException("Matrix4.[int,int].get: invalid column index " + columnIndex);
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
						this.Column0 = new Vector4(value, this.Column0.Y, this.Column0.Z, this.Column0.W);
						return;
					case 1:
						this.Column0 = new Vector4(this.Column0.X, value, this.Column0.Z, this.Column0.W);
						return;
					case 2:
						this.Column0 = new Vector4(this.Column0.X, this.Column0.Y, value, this.Column0.W);
						return;
					case 3:
						this.Column0 = new Vector4(this.Column0.X, this.Column0.Y, this.Column0.Z, value);
						return;
					default:
						throw new KeyNotFoundException("Matrix4.[int,int].set: invalid row index " + rowIndex);
					}
					break;
				case 1:
					switch (rowIndex)
					{
					case 0:
						this.Column1 = new Vector4(value, this.Column1.Y, this.Column1.Z, this.Column1.W);
						return;
					case 1:
						this.Column1 = new Vector4(this.Column1.X, value, this.Column1.Z, this.Column1.W);
						return;
					case 2:
						this.Column1 = new Vector4(this.Column1.X, this.Column1.Y, value, this.Column1.W);
						return;
					case 3:
						this.Column1 = new Vector4(this.Column1.X, this.Column1.Y, this.Column1.Z, value);
						return;
					default:
						throw new KeyNotFoundException("Matrix4.[int,int].set: invalid row index " + rowIndex);
					}
					break;
				case 2:
					switch (rowIndex)
					{
					case 0:
						this.Column2 = new Vector4(value, this.Column2.Y, this.Column2.Z, this.Column2.W);
						return;
					case 1:
						this.Column2 = new Vector4(this.Column2.X, value, this.Column2.Z, this.Column2.W);
						return;
					case 2:
						this.Column2 = new Vector4(this.Column2.X, this.Column2.Y, value, this.Column2.W);
						return;
					case 3:
						this.Column2 = new Vector4(this.Column2.X, this.Column2.Y, this.Column2.Z, value);
						return;
					default:
						throw new KeyNotFoundException("Matrix4.[int,int].set: invalid row index " + rowIndex);
					}
					break;
				case 3:
					switch (rowIndex)
					{
					case 0:
						this.Column3 = new Vector4(value, this.Column3.Y, this.Column3.Z, this.Column3.W);
						return;
					case 1:
						this.Column3 = new Vector4(this.Column3.X, value, this.Column3.Z, this.Column3.W);
						return;
					case 2:
						this.Column3 = new Vector4(this.Column3.X, this.Column3.Y, value, this.Column3.W);
						return;
					case 3:
						this.Column3 = new Vector4(this.Column3.X, this.Column3.Y, this.Column3.Z, value);
						return;
					default:
						throw new KeyNotFoundException("Matrix4.[int,int].set: invalid row index " + rowIndex);
					}
					break;
				default:
					throw new KeyNotFoundException("Matrix4.[int,int].get: invalid column index " + columnIndex);
				}
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000074A8 File Offset: 0x000056A8
		public static Matrix4 operator +(Matrix4 left, Matrix4 right)
		{
			return new Matrix4(left.Column0 + right.Column0, left.Column1 + right.Column1, left.Column2 + right.Column2, left.Column3 + right.Column3);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007508 File Offset: 0x00005708
		public static Matrix4 operator -(Matrix4 left, Matrix4 right)
		{
			return new Matrix4(left.Column0 - right.Column0, left.Column1 - right.Column1, left.Column2 - right.Column2, left.Column3 - right.Column3);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007568 File Offset: 0x00005768
		public static Matrix4 operator *(Matrix4 left, Matrix4 right)
		{
			Vector4 column = left.Column0;
			Vector4 column2 = left.Column1;
			Vector4 column3 = left.Column2;
			Vector4 column4 = left.Column3;
			Vector4 column5 = right.Column0;
			Vector4 column6 = right.Column1;
			Vector4 column7 = right.Column2;
			Vector4 column8 = right.Column3;
			return new Matrix4(column.X * column5.X + column2.X * column5.Y + column3.X * column5.Z + column4.X * column5.W, column.X * column6.X + column2.X * column6.Y + column3.X * column6.Z + column4.X * column6.W, column.X * column7.X + column2.X * column7.Y + column3.X * column7.Z + column4.X * column7.W, column.X * column8.X + column2.X * column8.Y + column3.X * column8.Z + column4.X * column8.W, column.Y * column5.X + column2.Y * column5.Y + column3.Y * column5.Z + column4.Y * column5.W, column.Y * column6.X + column2.Y * column6.Y + column3.Y * column6.Z + column4.Y * column6.W, column.Y * column7.X + column2.Y * column7.Y + column3.Y * column7.Z + column4.Y * column7.W, column.Y * column8.X + column2.Y * column8.Y + column3.Y * column8.Z + column4.Y * column8.W, column.Z * column5.X + column2.Z * column5.Y + column3.Z * column5.Z + column4.Z * column5.W, column.Z * column6.X + column2.Z * column6.Y + column3.Z * column6.Z + column4.Z * column6.W, column.Z * column7.X + column2.Z * column7.Y + column3.Z * column7.Z + column4.Z * column7.W, column.Z * column8.X + column2.Z * column8.Y + column3.Z * column8.Z + column4.Z * column8.W, column.W * column5.X + column2.W * column5.Y + column3.W * column5.Z + column4.W * column5.W, column.W * column6.X + column2.W * column6.Y + column3.W * column6.Z + column4.W * column6.W, column.W * column7.X + column2.W * column7.Y + column3.W * column7.Z + column4.W * column7.W, column.W * column8.X + column2.W * column8.Y + column3.W * column8.Z + column4.W * column8.W);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000079AE File Offset: 0x00005BAE
		public static Matrix4 operator *(double scalar, Matrix4 matrix)
		{
			return matrix * scalar;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000079B7 File Offset: 0x00005BB7
		public static Matrix4 operator *(Matrix4 matrix, double scalar)
		{
			return new Matrix4(scalar * matrix.Column0, scalar * matrix.Column1, scalar * matrix.Column2, scalar * matrix.Column3);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000079F4 File Offset: 0x00005BF4
		public static Vector4 operator *(Matrix4 matrix, Vector4 vector)
		{
			Vector4 column = matrix.Column0;
			Vector4 column2 = matrix.Column1;
			Vector4 column3 = matrix.Column2;
			Vector4 column4 = matrix.Column3;
			return new Vector4(column.X * vector.X + column2.X * vector.Y + column3.X * vector.Z + column4.X * vector.W, column.Y * vector.X + column2.Y * vector.Y + column3.Y * vector.Z + column4.Y * vector.W, column.Z * vector.X + column2.Z * vector.Y + column3.Z * vector.Z + column4.Z * vector.W, column.W * vector.X + column2.W * vector.Y + column3.W * vector.Z + column4.W * vector.W);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00007B24 File Offset: 0x00005D24
		public static Matrix4 operator /(Matrix4 matrix, double scalar)
		{
			if (scalar != 0.0)
			{
				double scalar2 = 1.0 / scalar;
				return new Matrix4(scalar2 * matrix.Column0, scalar2 * matrix.Column1, scalar2 * matrix.Column2, scalar2 * matrix.Column3);
			}
			return new Matrix4(Vector4.MaxValue, Vector4.MaxValue, Vector4.MaxValue, Vector4.MaxValue);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007B9C File Offset: 0x00005D9C
		public void SetColumn(int index, Vector4 column)
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
			case 3:
				this.Column3 = column;
				return;
			default:
				throw new KeyNotFoundException("Matrix4.SetColumn(): invalid column index");
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007BEB File Offset: 0x00005DEB
		public Vector4 GetColumn(int index)
		{
			switch (index)
			{
			case 0:
				return this.Column0;
			case 1:
				return this.Column1;
			case 2:
				return this.Column2;
			case 3:
				return this.Column3;
			default:
				throw new KeyNotFoundException("Matrix4.GetColumn(): invalid column index");
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007C2B File Offset: 0x00005E2B
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Matrix4)obj);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007C5C File Offset: 0x00005E5C
		public bool Equals(Matrix4 other)
		{
			return this.Column0.Equals(other.Column0) && this.Column1.Equals(other.Column1) && this.Column2.Equals(other.Column2) && this.Column3.Equals(other.Column3);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007CC8 File Offset: 0x00005EC8
		public override int GetHashCode()
		{
			return this.Column0.GetHashCode() ^ this.Column1.GetHashCode() ^ this.Column2.GetHashCode() ^ this.Column3.GetHashCode();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007D28 File Offset: 0x00005F28
		public Matrix4 Transpose()
		{
			return new Matrix4(this.Column0.X, this.Column0.Y, this.Column0.Z, this.Column0.W, this.Column1.X, this.Column1.Y, this.Column1.Z, this.Column1.W, this.Column2.X, this.Column2.Y, this.Column2.Z, this.Column2.W, this.Column3.X, this.Column3.Y, this.Column3.Z, this.Column3.W);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007E1C File Offset: 0x0000601C
		public Matrix4 Inverse()
		{
			double num = this.Column0.X * this.Column1.Y - this.Column1.X * this.Column0.Y;
			double num2 = this.Column0.X * this.Column2.Y - this.Column2.X * this.Column0.Y;
			double num3 = this.Column0.X * this.Column3.Y - this.Column3.X * this.Column0.Y;
			double num4 = this.Column1.X * this.Column2.Y - this.Column2.X * this.Column1.Y;
			double num5 = this.Column1.X * this.Column3.Y - this.Column3.X * this.Column1.Y;
			double num6 = this.Column2.X * this.Column3.Y - this.Column3.X * this.Column2.Y;
			double num7 = this.Column0.Z * this.Column1.W - this.Column1.Z * this.Column0.W;
			double num8 = this.Column0.Z * this.Column2.W - this.Column2.Z * this.Column0.W;
			double num9 = this.Column0.Z * this.Column3.W - this.Column3.Z * this.Column0.W;
			double num10 = this.Column1.Z * this.Column2.W - this.Column2.Z * this.Column1.W;
			double num11 = this.Column1.Z * this.Column3.W - this.Column3.Z * this.Column1.W;
			double num12 = this.Column2.Z * this.Column3.W - this.Column3.Z * this.Column2.W;
			double num13 = num * num12 - num2 * num11 + num3 * num10 + num4 * num9 - num5 * num8 + num6 * num7;
			if (Math.Abs(num13) <= 1E-08)
			{
				return Matrix4.Zero;
			}
			Matrix4 matrix = new Matrix4(new Vector4(this.Column1.Y * num12 - this.Column2.Y * num11 + this.Column3.Y * num10, -(this.Column0.Y * num12) + this.Column2.Y * num9 - this.Column3.Y * num8, this.Column0.Y * num11 - this.Column1.Y * num9 + this.Column3.Y * num7, -(this.Column0.Y * num10) + this.Column1.Y * num8 - this.Column2.Y * num7), new Vector4(-(this.Column1.X * num12) + this.Column2.X * num11 - this.Column3.X * num10, this.Column0.X * num12 - this.Column2.X * num9 + this.Column3.X * num8, -(this.Column0.X * num11) + this.Column1.X * num9 - this.Column3.X * num7, this.Column0.X * num10 - this.Column1.X * num8 + this.Column2.X * num7), new Vector4(this.Column1.W * num6 - this.Column2.W * num5 + this.Column3.W * num4, -(this.Column0.W * num6) + this.Column2.W * num3 - this.Column3.W * num2, this.Column0.W * num5 - this.Column1.W * num3 + this.Column3.W * num, -(this.Column0.W * num4) + this.Column1.W * num2 - this.Column2.W * num), new Vector4(-(this.Column1.Z * num6) + this.Column2.Z * num5 - this.Column3.Z * num4, this.Column0.Z * num6 - this.Column2.Z * num3 + this.Column3.Z * num2, -(this.Column0.Z * num5) + this.Column1.Z * num3 - this.Column3.Z * num, this.Column0.Z * num4 - this.Column1.Z * num2 + this.Column2.Z * num));
			double scalar = 1.0 / num13;
			return new Matrix4(matrix.Column0 * scalar, matrix.Column1 * scalar, matrix.Column2 * scalar, matrix.Column3 * scalar);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008548 File Offset: 0x00006748
		public Matrix4 Adjoint()
		{
			double num = this.Column0.X * this.Column1.Y - this.Column1.X * this.Column0.Y;
			double num2 = this.Column0.X * this.Column2.Y - this.Column2.X * this.Column0.Y;
			double num3 = this.Column0.X * this.Column3.Y - this.Column3.X * this.Column0.Y;
			double num4 = this.Column1.X * this.Column2.Y - this.Column2.X * this.Column1.Y;
			double num5 = this.Column1.X * this.Column3.Y - this.Column3.X * this.Column1.Y;
			double num6 = this.Column2.X * this.Column3.Y - this.Column3.X * this.Column2.Y;
			double num7 = this.Column0.Z * this.Column1.W - this.Column1.Z * this.Column0.W;
			double num8 = this.Column0.Z * this.Column2.W - this.Column2.Z * this.Column0.W;
			double num9 = this.Column0.Z * this.Column3.W - this.Column3.Z * this.Column0.W;
			double num10 = this.Column1.Z * this.Column2.W - this.Column2.Z * this.Column1.W;
			double num11 = this.Column1.Z * this.Column3.W - this.Column3.Z * this.Column1.W;
			double num12 = this.Column2.Z * this.Column3.W - this.Column3.Z * this.Column2.W;
			return new Matrix4(this.Column1.Y * num12 - this.Column2.Y * num11 + this.Column3.Y * num10, -(this.Column1.X * num12) + this.Column2.X * num11 - this.Column3.X * num10, this.Column1.W * num6 - this.Column2.W * num5 + this.Column3.W * num4, -(this.Column1.Z * num6) + this.Column2.Z * num5 - this.Column3.Z * num4, -(this.Column0.Y * num12) + this.Column2.Y * num9 - this.Column3.Y * num8, this.Column0.X * num12 - this.Column2.X * num9 + this.Column3.X * num8, -(this.Column0.W * num6) + this.Column2.W * num3 - this.Column3.W * num2, this.Column0.Z * num6 - this.Column2.Z * num3 + this.Column3.Z * num2, this.Column0.Y * num11 - this.Column1.Y * num9 + this.Column3.Y * num7, -(this.Column0.X * num11) + this.Column1.X * num9 - this.Column3.X * num7, this.Column0.W * num5 - this.Column1.W * num3 + this.Column3.W * num, -(this.Column0.Z * num5) + this.Column1.Z * num3 - this.Column3.Z * num, -(this.Column0.Y * num10) + this.Column1.Y * num8 - this.Column2.Y * num7, this.Column0.X * num10 - this.Column1.X * num8 + this.Column2.X * num7, -(this.Column0.W * num4) + this.Column1.W * num2 - this.Column2.W * num, this.Column0.Z * num4 - this.Column1.Z * num2 + this.Column2.Z * num);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00008BDC File Offset: 0x00006DDC
		public double Determinant()
		{
			double num = this.Column0.X * this.Column1.Y - this.Column1.X * this.Column0.Y;
			double num2 = this.Column0.X * this.Column2.Y - this.Column2.X * this.Column0.Y;
			double num3 = this.Column0.X * this.Column3.Y - this.Column3.X * this.Column0.Y;
			double num4 = this.Column1.X * this.Column2.Y - this.Column2.X * this.Column1.Y;
			double num5 = this.Column1.X * this.Column3.Y - this.Column3.X * this.Column1.Y;
			double num6 = this.Column2.X * this.Column3.Y - this.Column3.X * this.Column2.Y;
			double num7 = this.Column0.Z * this.Column1.W - this.Column1.Z * this.Column0.W;
			double num8 = this.Column0.Z * this.Column2.W - this.Column2.Z * this.Column0.W;
			double num9 = this.Column0.Z * this.Column3.W - this.Column3.Z * this.Column0.W;
			double num10 = this.Column1.Z * this.Column2.W - this.Column2.Z * this.Column1.W;
			double num11 = this.Column1.Z * this.Column3.W - this.Column3.Z * this.Column1.W;
			double num12 = this.Column2.Z * this.Column3.W - this.Column3.Z * this.Column2.W;
			return num * num12 - num2 * num11 + num3 * num10 + num4 * num9 - num5 * num8 + num6 * num7;
		}
	}
}
