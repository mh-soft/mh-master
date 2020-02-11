using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{

	[Serializable]
	public struct Matrix2 : IEquatable<Matrix2>
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00005415 File Offset: 0x00003615
		public Matrix2(Vector2 column0, Vector2 column1)
		{
			this = default(Matrix2);
			this.Column0 = column0;
			this.Column1 = column1;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000542C File Offset: 0x0000362C
		public Matrix2(double entry00, double entry11)
		{
			this = default(Matrix2);
			this.Column0 = new Vector2(entry00, 0.0);
			this.Column1 = new Vector2(0.0, entry11);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000545F File Offset: 0x0000365F
		private Matrix2(double m00, double m01, double m10, double m11)
		{
			this = default(Matrix2);
			this.Column0 = new Vector2(m00, m10);
			this.Column1 = new Vector2(m01, m11);
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00005483 File Offset: 0x00003683
		
		public static Matrix2 Zero
		{
			get
			{
				return new Matrix2(Vector2.Zero, Vector2.Zero);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005494 File Offset: 0x00003694
		
		public static Matrix2 Identity
		{
			get
			{
				return new Matrix2(Vector2.UnitX, Vector2.UnitY);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000054A5 File Offset: 0x000036A5
		// (set) Token: 0x06000129 RID: 297 RVA: 0x000054AD File Offset: 0x000036AD
		public Vector2 Column0 { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000054B6 File Offset: 0x000036B6
		// (set) Token: 0x0600012B RID: 299 RVA: 0x000054BE File Offset: 0x000036BE
		public Vector2 Column1 { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000054C8 File Offset: 0x000036C8
		
		internal string DebuggerDisplay
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(C0:");
				stringBuilder.Append(this.Column0.DebuggerDisplay);
				stringBuilder.Append(" C1:");
				stringBuilder.Append(this.Column1.DebuggerDisplay);
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x1700007F RID: 127
		public double this[int rowIndex, int columnIndex]
		{
			get
			{
				if (columnIndex != 0)
				{
					if (columnIndex != 1)
					{
						throw new KeyNotFoundException("Matrix2.[int,int].get: invalid column index");
					}
					if (rowIndex == 0)
					{
						return this.Column1.X;
					}
					if (rowIndex != 1)
					{
						throw new KeyNotFoundException("Matrix2.[int,int].get: invalid row index");
					}
					return this.Column1.Y;
				}
				else
				{
					if (rowIndex == 0)
					{
						return this.Column0.X;
					}
					if (rowIndex != 1)
					{
						throw new KeyNotFoundException("Matrix2.[int,int].get: invalid row index");
					}
					return this.Column0.Y;
				}
			}
			set
			{
				if (columnIndex != 0)
				{
					if (columnIndex != 1)
					{
						throw new KeyNotFoundException("Matrix2.[int,int].set: invalid column index");
					}
					if (rowIndex == 0)
					{
						this.Column1 = new Vector2(value, this.Column1.Y);
						return;
					}
					if (rowIndex != 1)
					{
						throw new KeyNotFoundException("Matrix2.[int,int].set: invalid row index");
					}
					this.Column1 = new Vector2(this.Column1.X, value);
					return;
				}
				else
				{
					if (rowIndex == 0)
					{
						this.Column0 = new Vector2(value, this.Column0.Y);
						return;
					}
					if (rowIndex != 1)
					{
						throw new KeyNotFoundException("Matrix2.[int,int].set: invalid row index");
					}
					this.Column0 = new Vector2(this.Column0.X, value);
					return;
				}
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000566B File Offset: 0x0000386B
		public static Matrix2 operator +(Matrix2 left, Matrix2 right)
		{
			return new Matrix2(left.Column0 + right.Column0, left.Column1 + right.Column1);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005698 File Offset: 0x00003898
		public static Matrix2 operator -(Matrix2 left, Matrix2 right)
		{
			return new Matrix2(left.Column0 - right.Column0, left.Column1 - right.Column1);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000056C8 File Offset: 0x000038C8
		public static Matrix2 operator *(Matrix2 left, Matrix2 right)
		{
			return new Matrix2(left.Column0.X * right.Column0.X + left.Column1.X * right.Column0.Y, left.Column0.X * right.Column1.X + left.Column1.X * right.Column1.Y, left.Column0.Y * right.Column0.X + left.Column1.Y * right.Column0.Y, left.Column0.Y * right.Column1.X + left.Column1.Y * right.Column1.Y);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000057D6 File Offset: 0x000039D6
		public static Matrix2 operator *(double scalar, Matrix2 matrix)
		{
			return matrix * scalar;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000057DF File Offset: 0x000039DF
		public static Matrix2 operator *(Matrix2 matrix, double scalar)
		{
			return new Matrix2(scalar * matrix.Column0, scalar * matrix.Column1);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005800 File Offset: 0x00003A00
		public static Vector2 operator *(Matrix2 matrix, Vector2 vector)
		{
			return new Vector2(matrix.Column0.X * vector.X + matrix.Column1.X * vector.Y, matrix.Column0.Y * vector.X + matrix.Column1.Y * vector.Y);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005870 File Offset: 0x00003A70
		public static Matrix2 operator /(Matrix2 matrix, double scalar)
		{
			if (scalar != 0.0)
			{
				double scalar2 = 1.0 / scalar;
				return new Matrix2(scalar2 * matrix.Column0, scalar2 * matrix.Column1);
			}
			return new Matrix2(Vector2.MaxValue, Vector2.MaxValue);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000058C4 File Offset: 0x00003AC4
		public Vector2 GetColumn(int index)
		{
			if (index == 0)
			{
				return this.Column0;
			}
			if (index != 1)
			{
				throw new KeyNotFoundException("Matrix2.GetColumn(): invalid column index");
			}
			return this.Column1;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000058E7 File Offset: 0x00003AE7
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Matrix2)obj);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005918 File Offset: 0x00003B18
		public bool Equals(Matrix2 other)
		{
			return this.Column0.Equals(other.Column0) && this.Column1.Equals(other.Column1);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005954 File Offset: 0x00003B54
		public override int GetHashCode()
		{
			return this.Column0.GetHashCode() ^ this.Column1.GetHashCode();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000598C File Offset: 0x00003B8C
		public Matrix2 Transpose()
		{
			return new Matrix2(this.Column0.X, this.Column0.Y, this.Column1.X, this.Column1.Y);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000059D8 File Offset: 0x00003BD8
		public Matrix2 Inverse()
		{
			double num = this.Column0.X * this.Column1.Y - this.Column1.X * this.Column0.Y;
			if (Math.Abs(num) > 1E-08)
			{
				double num2 = 1.0 / num;
				return new Matrix2(new Vector2(this.Column1.Y * num2, -this.Column0.Y * num2), new Vector2(-this.Column1.X * num2, this.Column0.X * num2));
			}
			throw new ArgumentException("No inverse");
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005A9C File Offset: 0x00003C9C
		public Matrix2 Adjoint()
		{
			return new Matrix2(this.Column1.Y, -this.Column1.X, -this.Column0.Y, this.Column0.X);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005AE8 File Offset: 0x00003CE8
		public double Determinant()
		{
			return this.Column0.X * this.Column1.Y - this.Column1.X * this.Column0.Y;
		}
	}
}
