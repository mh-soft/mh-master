using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Hao.Geometry
{
    /// <summary>
    /// 用于定义一个二维向量
    /// </summary>
    [Serializable]
    public struct Vector2 : IEquatable<Vector2>
    {
        //当前向量的X值
        
        private double x;

        //当前向量的Y值
        private double y;

        /// <summary>
        /// 构造函数，初始化一个二维向量
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
		public Vector2(double x, double y)
        {
            MathBase.AssertValid(x);
            MathBase.AssertValid(y);
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// 返回一个O向量
        /// </summary>
        public static Vector2 Zero
        {
            get
            {
                return new Vector2(0.0, 0.0);
            }
        }

        // 返回一个X单位向量
        public static Vector2 UnitX
        {
            get
            {
                return new Vector2(1.0, 0.0);
            }
        }

        //返回一个Y单位向量
        public static Vector2 UnitY
        {
            get
            {
                return new Vector2(0.0, 1.0);
            }
        }

        // 返回一个单位向量
        public static Vector2 One
        {
            get
            {
                return new Vector2(1.0, 1.0);
            }
        }

        /// <summary>
        /// 返回向量的最小值
        /// </summary>
        public static Vector2 MinValue
        {
            get
            {
                return new Vector2(double.MinValue, double.MinValue);
            }
        }

        /// <summary>
        /// 返回向量中的最大值
        /// </summary>
        public static Vector2 MaxValue
        {
            get
            {
                return new Vector2(double.MaxValue, double.MaxValue);
            }
        }

       
        /// <summary>
        /// 当前的X坐标
        /// </summary>
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


        /// <summary>
        /// 当前的Y坐标
        /// </summary>
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

        /// <summary>
        /// 当前向量的长度
        /// </summary>
        public double Length
        {
            get
            {
                return Math.Sqrt(this.SquaredLength);
            }
        }

        
        /// <summary>
        /// 当前向量的矩形面积
        /// </summary>
        public double SquaredLength
        {
            get
            {
                return this.X * this.X + this.Y * this.Y;
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
                stringBuilder.Append(")");
                return stringBuilder.ToString();
            }
        }


        public double this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return this.X;
                }
                if (index != 1)
                {
                    throw new KeyNotFoundException("Vector2, index out of range");
                }
                return this.Y;
            }
            set
            {
                if (index == 0)
                {
                    this.X = value;
                    return;
                }
                if (index != 1)
                {
                    throw new KeyNotFoundException("Vector2, index out of range");
                }
                this.Y = value;
            }
        }

        public static Vector2 Min(Vector2 value1, Vector2 value2)
        {
            return new Vector2(Math.Min(value1.X, value2.X), Math.Min(value1.Y, value2.Y));
        }

        public static Vector2 Max(Vector2 value1, Vector2 value2)
        {
            return new Vector2(Math.Max(value1.X, value2.X), Math.Max(value1.Y, value2.Y));
        }

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2 operator *(Vector2 vector, double scalar)
        {
            return new Vector2(vector.X * scalar, vector.Y * scalar);
        }

        public static Vector2 operator *(double scalar, Vector2 vector)
        {
            return new Vector2(vector.X * scalar, vector.Y * scalar);
        }

        public static Vector2 operator *(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X * right.X, left.Y * right.Y);
        }

        public static Vector2 operator /(Vector2 vector, double scalar)
        {
            return new Vector2(vector.X / scalar, vector.Y / scalar);
        }

        public static Vector2 operator /(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X / right.X, left.Y / right.Y);
        }

        public static Vector2 operator -(Vector2 vector)
        {
            return new Vector2(-vector.X, -vector.Y);
        }

        public override bool Equals(object obj)
        {
            return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Vector2)obj);
        }

        public double Dot(Vector2 vector)
        {
            return vector.X * this.X + vector.Y * this.Y;
        }

        public double Dot(UnitVector2 vector)
        {
            return vector.X * this.X + vector.Y * this.Y;
        }

        public UnitVector2 GetNormalized()
        {
            double length = this.Length;
            if (length > 1E-08)
            {
                return new UnitVector2(this.x / length, this.y / length);
            }
            throw new InvalidOperationException("length zero, cannot be normalized.");
        }


        public bool TryGetNormalized(out UnitVector2 normalized)
        {
            double length = this.Length;
            if (length > 1E-08)
            {
                normalized = new UnitVector2(this.x / length, this.y / length);
                return true;
            }
            normalized = UnitVector2.UnitX;
            return false;
        }


        public Vector2 Perpendicular()
        {
            return new Vector2(this.y, -this.x);
        }

        // 向量点集
        public double DotPerpendicular(Vector2 vector)
        {
            return this.X * vector.Y - this.Y * vector.X;
        }

        /// <summary>
        /// 进行坐标对比，判断是否是同一个点
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Vector2 other)
        {
            return this.x == other.x && this.y == other.y;
        }

        /// <summary>
        /// 获取当前的hash值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode();
        }

        /// <summary>
        /// 返回当前点的信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Vector2{X:");
            stringBuilder.Append(this.X);
            stringBuilder.Append(" Y:");
            stringBuilder.Append(this.Y);
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
