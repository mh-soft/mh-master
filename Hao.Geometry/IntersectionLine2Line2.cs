using System;

namespace Hao.Geometry
{
	/// <summary>
	/// 两条线是否相交
	/// </summary>
	internal struct IntersectionLine2Line2
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="line0"></param>
		/// <param name="line1"></param>
		public IntersectionLine2Line2(Line2 line0, Line2 line1)
		{
			this = default(IntersectionLine2Line2);
			this.line0 = line0;
			this.line1 = line1;
		}


		/// <summary>
		/// 当前的顶点信息
		/// </summary>
		public Vector2 Point { get; private set; }

	
		/// <summary>
		/// 当前相交的类型
		/// </summary>
		public Intersection.Type IntersectionType { get; private set; }

		/// <summary>
		/// 查找当前的信息
		/// </summary>
		/// <returns></returns>
		public bool Find()
		{
			double[] array = new double[2];
			this.IntersectionType = IntersectionLine2Line2.Classify(this.line0.Origin, this.line0.Direction, this.line1.Origin, this.line1.Direction, 1E-08, ref array);
			if (this.IntersectionType == Intersection.Type.IT_POINT)
			{
				this.Point = this.line0.Origin + array[0] * this.line0.Direction;
			}
			return this.IntersectionType > Intersection.Type.IT_EMPTY;
		}

		/// <summary>
		/// 执行当前的节点检测
		/// </summary>
		/// <param name="p0"></param>
		/// <param name="d0"></param>
		/// <param name="p1"></param>
		/// <param name="d1"></param>
		/// <param name="dotThreshold"></param>
		/// <param name="s"></param>
		/// <returns></returns>
		internal static Intersection.Type Classify(Vector2 p0, UnitVector2 d0, Vector2 p1, UnitVector2 d1, double dotThreshold, ref double[] s)
		{
			Vector2 vector = p1 - p0;
			//进行叉乘
			double num = d0.DotPerpendicular(d1);
			if (Math.Abs(num) > dotThreshold)
			{
				if (s.Length >= 2)
				{
					double num2 = 1.0 / num;
					double num3 = vector.DotPerpendicular((Vector2)d0);
					double num4 = vector.DotPerpendicular((Vector2)d1);
					s[0] = num4 * num2;
					s[1] = num3 * num2;
				}
				return Intersection.Type.IT_POINT;
			}
			UnitVector2 unitVector;
			if (!vector.TryGetNormalized(out unitVector))
			{
				return Intersection.Type.IT_LINE;
			}
			if (Math.Abs(unitVector.DotPerpendicular(d1)) <= dotThreshold)
			{
				return Intersection.Type.IT_LINE;
			}
			return Intersection.Type.IT_EMPTY;
		}

		/// <summary>
		/// 第一个线
		/// </summary>
		private readonly Line2 line0;

		/// <summary>
		/// 第二个线
		/// </summary>
		private readonly Line2 line1;
	}
}
