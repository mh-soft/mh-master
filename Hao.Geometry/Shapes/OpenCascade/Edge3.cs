using System;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x02000017 RID: 23
	public class Edge3
	{
		// Token: 0x06000104 RID: 260 RVA: 0x000050EC File Offset: 0x000032EC
		public Edge3(Orientation orientation, ICurve3 curve, ICurve2 curveOnSurface, double startParameter, double endParameter)
		{
			this.Orientation = orientation;
			this.Curve = curve;
			this.CurveOnSurface = curveOnSurface;
			this.StartParameter = startParameter;
			this.EndParameter = endParameter;
			this.StartVertex = this.Curve.EvalAt(this.StartParameter);
			this.EndVertex = this.Curve.EvalAt(this.EndParameter);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005152 File Offset: 0x00003352
		public Edge3(Orientation orientation, ICurve3 curve, ICurve2 curveOnSurface, double startParameter, double endParameter, Vector3 startVertex, Vector3 endVertex)
		{
			this.Orientation = orientation;
			this.Curve = curve;
			this.CurveOnSurface = curveOnSurface;
			this.StartParameter = startParameter;
			this.EndParameter = endParameter;
			this.StartVertex = startVertex;
			this.EndVertex = endVertex;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005190 File Offset: 0x00003390
		public Edge3(Segment3 segment)
		{
			this.Orientation = Orientation.Forward;
			this.CurveOnSurface = null;
			this.Curve = new LineCurve3(segment.Origin, segment.Direction);
			this.StartParameter = -segment.Extent;
			this.EndParameter = segment.Extent;
			this.StartVertex = this.Curve.EvalAt(this.StartParameter);
			this.EndVertex = this.Curve.EvalAt(this.EndParameter);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005214 File Offset: 0x00003414
		public Edge3(Ray3 ray)
		{
			this.Orientation = Orientation.Forward;
			this.CurveOnSurface = null;
			this.Curve = new LineCurve3(ray.Origin, ray.Direction);
			this.StartParameter = 0.0;
			this.EndParameter = double.MaxValue;
			this.StartVertex = this.Curve.EvalAt(this.StartParameter);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005284 File Offset: 0x00003484
		public Edge3(Circle3 circle)
		{
			this.Orientation = Orientation.Forward;
			this.CurveOnSurface = null;
			this.Curve = new CircleCurve3(circle);
			this.StartParameter = 0.0;
			this.EndParameter = 6.2831853071795862;
			this.StartVertex = this.Curve.EvalAt(this.StartParameter);
			this.EndVertex = this.Curve.EvalAt(this.EndParameter);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005300 File Offset: 0x00003500
		public Edge3(Arc3 arc)
		{
			this.Orientation = Orientation.Forward;
			this.CurveOnSurface = null;
			this.Curve = new CircleCurve3(arc.Circle);
			this.StartParameter = arc.StartAngle.Radians;
			this.EndParameter = arc.StartAngle.Radians + arc.DeltaAngle.Radians;
			this.StartVertex = this.Curve.EvalAt(this.StartParameter);
			this.EndVertex = this.Curve.EvalAt(this.EndParameter);
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000539C File Offset: 0x0000359C
		public Segment3? Segment
		{
			get
			{
				if (!(this.Curve is LineCurve3))
				{
					return null;
				}
				if (this.Orientation == Orientation.Forward)
				{
					return new Segment3?(new Segment3(this.StartVertex, this.EndVertex));
				}
				return new Segment3?(new Segment3(this.EndVertex, this.StartVertex));
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000053F8 File Offset: 0x000035F8
		public Segment2? SegmentOnSurface
		{
			get
			{
				if (!(this.CurveOnSurface is LineCurve2))
				{
					return null;
				}
				if (this.Orientation == Orientation.Forward)
				{
					return new Segment2?(new Segment2(this.StartVertexOnSurface, this.EndVertexOnSurface));
				}
				return new Segment2?(new Segment2(this.EndVertexOnSurface, this.StartVertexOnSurface));
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00005454 File Offset: 0x00003654
		public Ray3? Ray
		{
			get
			{
				LineCurve3 lineCurve = this.Curve as LineCurve3;
				if (lineCurve == null || !this.StartParameter.Equals(0.0) || this.EndParameter < 1.7976931348623157E+308)
				{
					return null;
				}
				return new Ray3?(new Ray3(lineCurve.Origin, lineCurve.Direction));
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000054BC File Offset: 0x000036BC
		public Circle3? Circle
		{
			get
			{
				CircleCurve3 circleCurve = this.Curve as CircleCurve3;
				if (circleCurve == null || Math.Abs(this.EndParameter - this.StartParameter - 6.2831853071795862) > 1E-08)
				{
					return null;
				}
				return new Circle3?(circleCurve.Circle);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00005514 File Offset: 0x00003714
		public Circle2? CircleOnSurface
		{
			get
			{
				CircleCurve2 circleCurve = this.CurveOnSurface as CircleCurve2;
				if (circleCurve == null || Math.Abs(this.EndParameter - this.StartParameter - 6.2831853071795862) > 1E-08)
				{
					return null;
				}
				return new Circle2?(circleCurve.Circle);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000556C File Offset: 0x0000376C
		public Arc3? Arc
		{
			get
			{
				CircleCurve3 circleCurve = this.Curve as CircleCurve3;
				if (circleCurve == null)
				{
					return null;
				}
				return new Arc3?(new Arc3(circleCurve.Circle, Angle.FromRadians(this.StartParameter), Angle.FromRadians(this.EndParameter - this.StartParameter)));
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000055C0 File Offset: 0x000037C0
		public Arc2? ArcOnSurface
		{
			get
			{
				CircleCurve2 circleCurve = this.CurveOnSurface as CircleCurve2;
				if (circleCurve == null)
				{
					return null;
				}
				return new Arc2?(new Arc2(circleCurve.Circle, Angle.FromRadians(this.StartParameter), Angle.FromRadians(this.EndParameter - this.StartParameter)));
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005614 File Offset: 0x00003814
		public AxisAlignedBox3? BoundingBox
		{
			get
			{
				Segment3? segment = this.Segment;
				if (segment != null)
				{
					return new AxisAlignedBox3?(segment.Value.ComputeAxisAlignedBoundingBox());
				}
				Circle3? circle = this.Circle;
				if (circle != null)
				{
					return new AxisAlignedBox3?(circle.Value.ComputeAxisAlignedBoundingBox());
				}
				Arc3? arc = this.Arc;
				if (arc != null)
				{
					return new AxisAlignedBox3?(arc.Value.ComputeAxisAlignedBoundingBox());
				}
				return null;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00005690 File Offset: 0x00003890
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00005698 File Offset: 0x00003898
		public Orientation Orientation { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000056A1 File Offset: 0x000038A1
		// (set) Token: 0x06000115 RID: 277 RVA: 0x000056A9 File Offset: 0x000038A9
		public ICurve3 Curve { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000056B2 File Offset: 0x000038B2
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000056BA File Offset: 0x000038BA
		public ICurve2 CurveOnSurface { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000056C3 File Offset: 0x000038C3
		// (set) Token: 0x06000119 RID: 281 RVA: 0x000056CB File Offset: 0x000038CB
		public double StartParameter { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000056D4 File Offset: 0x000038D4
		// (set) Token: 0x0600011B RID: 283 RVA: 0x000056DC File Offset: 0x000038DC
		public double EndParameter { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000056E5 File Offset: 0x000038E5
		// (set) Token: 0x0600011D RID: 285 RVA: 0x000056ED File Offset: 0x000038ED
		public Vector3 StartVertex { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000056F6 File Offset: 0x000038F6
		// (set) Token: 0x0600011F RID: 287 RVA: 0x000056FE File Offset: 0x000038FE
		public Vector3 EndVertex { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00005707 File Offset: 0x00003907
		public Vector2 StartVertexOnSurface
		{
			get
			{
				return this.CurveOnSurface.EvalAt(this.StartParameter);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000571A File Offset: 0x0000391A
		public Vector2 EndVertexOnSurface
		{
			get
			{
				return this.CurveOnSurface.EvalAt(this.EndParameter);
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000572D File Offset: 0x0000392D
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Edge3)obj);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005754 File Offset: 0x00003954
		public bool Equals(Edge3 other)
		{
			return this.Orientation.Equals(other.Orientation) && this.Curve.Equals(other.Curve) && (this.CurveOnSurface == null || this.CurveOnSurface.Equals(other.CurveOnSurface)) && this.StartParameter.Equals(other.StartParameter) && this.EndParameter.Equals(other.EndParameter);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000057DC File Offset: 0x000039DC
		public override int GetHashCode()
		{
			return this.Orientation.GetHashCode() ^ this.Curve.GetHashCode() ^ ((this.CurveOnSurface != null) ? this.CurveOnSurface.GetHashCode() : 0) ^ this.StartParameter.GetHashCode() ^ this.EndParameter.GetHashCode();
		}
	}
}
