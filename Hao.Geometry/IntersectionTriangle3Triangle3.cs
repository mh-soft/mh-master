using System;

namespace Hao.Geometry
{
	// Token: 0x02000071 RID: 113
	internal struct IntersectionTriangle3Triangle3
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x00013E97 File Offset: 0x00012097
		public IntersectionTriangle3Triangle3(Triangle3 triangle0, Triangle3 triangle1)
		{
			this = default(IntersectionTriangle3Triangle3);
			this.Points = new Vector3[6];
			this.triangle0 = triangle0;
			this.triangle1 = triangle1;
			this.ReportCoplanarIntersections = true;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00013EC1 File Offset: 0x000120C1
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00013EC9 File Offset: 0x000120C9
		public bool ReportCoplanarIntersections { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00013ED2 File Offset: 0x000120D2
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x00013EDA File Offset: 0x000120DA
		public int Quantity { get; private set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00013EE3 File Offset: 0x000120E3
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x00013EEB File Offset: 0x000120EB
		public Vector3[] Points { get; private set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00013EF4 File Offset: 0x000120F4
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00013EFC File Offset: 0x000120FC
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x06000464 RID: 1124 RVA: 0x00013F08 File Offset: 0x00012108
		public bool Test()
		{
			Vector3[] array = new Vector3[]
			{
				this.triangle0.V1 - this.triangle0.V0,
				this.triangle0.V2 - this.triangle0.V1,
				this.triangle0.V0 - this.triangle0.V2
			};
			UnitVector3 axis;
			if (!array[0].TryGetUnitCross(array[1], out axis))
			{
				int num = IntersectionUtility3.MaxIndex(array[0].SquaredLength, array[1].SquaredLength, array[2].SquaredLength);
				Segment3 segment = new Segment3(this.triangle0[num], this.triangle0[(num + 1) % 3]);
				IntersectionSegment3Triangle3 intersectionSegment3Triangle = new IntersectionSegment3Triangle3(segment, this.triangle1);
				return intersectionSegment3Triangle.Test();
			}
			double num2 = axis.Dot(this.triangle0.V0);
			double num3;
			double num4;
			IntersectionTriangle3Triangle3.ProjectOntoAxis(this.triangle1, axis, out num3, out num4);
			if (num2 < num3 || num2 > num4)
			{
				return false;
			}
			Vector3[] array2 = new Vector3[]
			{
				this.triangle1.V1 - this.triangle1.V0,
				this.triangle1.V2 - this.triangle1.V1,
				this.triangle1.V0 - this.triangle1.V2
			};
			UnitVector3 unitVector;
			if (!array2[0].TryGetUnitCross(array2[1], out unitVector))
			{
				int num5 = IntersectionUtility3.MaxIndex(array2[0].SquaredLength, array2[1].SquaredLength, array2[2].SquaredLength);
				Segment3 segment2 = new Segment3(this.triangle1[num5], this.triangle1[(num5 + 1) % 3]);
				IntersectionSegment3Triangle3 intersectionSegment3Triangle2 = new IntersectionSegment3Triangle3(segment2, this.triangle0);
				return intersectionSegment3Triangle2.Test();
			}
			Vector3 vector=new Vector3();
			if (axis.Cross(unitVector).Dot(vector) >= 1E-08)
			{
				double num6 = unitVector.Dot(this.triangle1.V0);
				double num7;
				double num8;
				IntersectionTriangle3Triangle3.ProjectOntoAxis(this.triangle0, unitVector, out num7, out num8);
				if (num6 < num7 || num6 > num8)
				{
					return false;
				}
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						UnitVector3 axis2;
						if (array[j].TryGetUnitCross(array2[i], out axis2))
						{
							IntersectionTriangle3Triangle3.ProjectOntoAxis(this.triangle0, axis2, out num7, out num8);
							IntersectionTriangle3Triangle3.ProjectOntoAxis(this.triangle1, axis2, out num3, out num4);
							if (num8 < num3 || num4 < num7)
							{
								return false;
							}
						}
					}
				}
			}
			else
			{
				for (int j = 0; j < 3; j++)
				{
					UnitVector3 axis3;
					if (axis.TryGetUnitCross(array[j], out axis3))
					{
						double num7;
						double num8;
						IntersectionTriangle3Triangle3.ProjectOntoAxis(this.triangle0, axis3, out num7, out num8);
						IntersectionTriangle3Triangle3.ProjectOntoAxis(this.triangle1, axis3, out num3, out num4);
						if (num8 < num3 || num4 < num7)
						{
							return false;
						}
					}
				}
				for (int i = 0; i < 3; i++)
				{
					UnitVector3 axis4;
					if (unitVector.TryGetUnitCross(array2[i], out axis4))
					{
						double num7;
						double num8;
						IntersectionTriangle3Triangle3.ProjectOntoAxis(this.triangle0, axis4, out num7, out num8);
						IntersectionTriangle3Triangle3.ProjectOntoAxis(this.triangle1, axis4, out num3, out num4);
						if (num8 < num3 || num4 < num7)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000142D4 File Offset: 0x000124D4
		public bool Find()
		{
			Vector3[] array = new Vector3[]
			{
				this.triangle0.V1 - this.triangle0.V0,
				this.triangle0.V2 - this.triangle0.V1,
				this.triangle0.V0 - this.triangle0.V2
			};
			UnitVector3 unitVector;
			if (!array[0].TryGetUnitCross(array[1], out unitVector))
			{
				int num = IntersectionUtility3.MaxIndex(array[0].SquaredLength, array[1].SquaredLength, array[2].SquaredLength);
				Segment3 segment = new Segment3(this.triangle0[num], this.triangle0[(num + 1) % 3]);
				IntersectionSegment3Triangle3 intersectionSegment3Triangle = new IntersectionSegment3Triangle3(segment, this.triangle1);
				bool result = intersectionSegment3Triangle.Find();
				if (intersectionSegment3Triangle.IntersectionType == Intersection.Type.IT_POINT)
				{
					this.Points[0] = segment.Origin + intersectionSegment3Triangle.SegmentParameter * segment.Direction;
					this.Quantity = 1;
					this.IntersectionType = Intersection.Type.IT_POINT;
				}
				return result;
			}
			Plane3 plane = new Plane3(this.triangle0.V0, this.triangle0.V1, this.triangle0.V2);
			int[] array2 = new int[3];
			double[] array3 = new double[3];
			int num2;
			int num3;
			int num4;
			IntersectionTriangle3Triangle3.TrianglePlaneRelations(this.triangle1, plane, ref array3, ref array2, out num2, out num3, out num4);
			if (num2 == 3 || num3 == 3)
			{
				return false;
			}
			if (num4 == 3)
			{
				return this.ReportCoplanarIntersections && this.GetCoplanarIntersection(plane, this.triangle0, this.triangle1);
			}
			if (num2 == 0 || num3 == 0)
			{
				if (num4 == 2)
				{
					for (int i = 0; i < 3; i++)
					{
						if (array2[i] != 0)
						{
							int num5 = (i + 2) % 3;
							int num6 = (i + 1) % 3;
							return this.IntersectsSegment(plane, this.triangle0, this.triangle1[num5], this.triangle1[num6]);
						}
					}
				}
				else
				{
					for (int i = 0; i < 3; i++)
					{
						if (array2[i] == 0)
						{
							return this.ContainsPoint(this.triangle0, plane, this.triangle1[i]);
						}
					}
				}
			}
			if (num4 == 0)
			{
				int num7 = (num2 == 1) ? 1 : -1;
				for (int i = 0; i < 3; i++)
				{
					if (array2[i] == num7)
					{
						int num5 = (i + 2) % 3;
						int num6 = (i + 1) % 3;
						double scalar = array3[i] / (array3[i] - array3[num5]);
						Vector3 vector = this.triangle1[i] + scalar * (this.triangle1[num5] - this.triangle1[i]);
						scalar = array3[i] / (array3[i] - array3[num6]);
						Vector3 end = this.triangle1[i] + scalar * (this.triangle1[num6] - this.triangle1[i]);
						return this.IntersectsSegment(plane, this.triangle0, vector, end);
					}
				}
			}
			for (int i = 0; i < 3; i++)
			{
				if (array2[i] == 0)
				{
					int num5 = (i + 2) % 3;
					int num6 = (i + 1) % 3;
					double scalar = array3[num5] / (array3[num5] - array3[num6]);
					Vector3 vector = this.triangle1[num5] + scalar * (this.triangle1[num6] - this.triangle1[num5]);
					return this.IntersectsSegment(plane, this.triangle0, this.triangle1[i], vector);
				}
			}
			MathBase.Assert(false, "Triangle3Triangle3.Find(): should not be here");
			return false;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000146E8 File Offset: 0x000128E8
		private static void ProjectOntoAxis(Triangle3 tri, UnitVector3 axis, out double min, out double max)
		{
			double num = axis.Dot(tri.V0);
			double num2 = axis.Dot(tri.V1);
			double num3 = axis.Dot(tri.V2);
			min = num;
			max = min;
			if (num2 < min)
			{
				min = num2;
			}
			else if (num2 > max)
			{
				max = num2;
			}
			if (num3 < min)
			{
				min = num3;
				return;
			}
			if (num3 > max)
			{
				max = num3;
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001474C File Offset: 0x0001294C
		private static void TrianglePlaneRelations(Triangle3 triangle, Plane3 plane, ref double[] signedDistances, ref int[] signs, out int positive, out int negative, out int zero)
		{
			positive = 0;
			negative = 0;
			zero = 0;
			for (int i = 0; i < 3; i++)
			{
				signedDistances[i] = plane.Normal.Dot(triangle[i]) - plane.Constant;
				if (signedDistances[i] > 1E-08)
				{
					signs[i] = 1;
					positive++;
				}
				else if (signedDistances[i] < -1E-08)
				{
					signs[i] = -1;
					negative++;
				}
				else
				{
					signedDistances[i] = 0.0;
					signs[i] = 0;
					zero++;
				}
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000147E8 File Offset: 0x000129E8
		private bool ContainsPoint(Triangle3 triangle, Plane3 plane, Vector3 point)
		{
			AffineTransform3 affineTransform = Transform3Factory.CreateOrthonormalBasis(plane.Normal);
			UnitVector3 axisX = affineTransform.AxisX;
			UnitVector3 axisY = affineTransform.AxisY;
			Vector3 vector = point - triangle.V0;
			Vector3 vector2 = triangle.V1 - triangle.V0;
			Vector3 vector3 = triangle.V2 - triangle.V0;
			Vector2 rkP = new Vector2(axisX.Dot(vector), axisY.Dot(vector));
			Vector2[] vertices = new Vector2[]
			{
				Vector2.Zero,
				new Vector2(axisX.Dot(vector2), axisY.Dot(vector2)),
				new Vector2(axisX.Dot(vector3), axisY.Dot(vector3))
			};
			if (new IntersectionTriangle3Triangle3.Query2(3, vertices).ToTriangle(rkP, 0, 1, 2) <= 0)
			{
				this.Quantity = 1;
				this.Points[0] = point;
				return true;
			}
			return false;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000148E0 File Offset: 0x00012AE0
		private bool IntersectsSegment(Plane3 plane, Triangle3 triangle, Vector3 end0, Vector3 end1)
		{
			int num = 0;
			double num2 = Math.Abs(plane.Normal.X);
			double num3 = Math.Abs(plane.Normal.Y);
			if (num3 > num2)
			{
				num = 1;
				num2 = num3;
			}
			num3 = Math.Abs(plane.Normal.Z);
			if (num3 > num2)
			{
				num = 2;
			}
			Triangle2 triangle2;
			Vector2 end2;
			Vector2 end3;
			if (num == 0)
			{
				triangle2 = new Triangle2(new Vector2(triangle.V0.Y, triangle.V0.Z), new Vector2(triangle.V1.Y, triangle.V1.Z), new Vector2(triangle.V2.Y, triangle.V2.Z));
				end2 = new Vector2(end0.Y, end0.Z);
				end3 = new Vector2(end1.Y, end1.Z);
			}
			else if (num == 1)
			{
				triangle2 = new Triangle2(new Vector2(triangle.V0.X, triangle.V0.Z), new Vector2(triangle.V1.X, triangle.V1.Z), new Vector2(triangle.V2.X, triangle.V2.Z));
				end2 = new Vector2(end0.X, end0.Z);
				end3 = new Vector2(end1.X, end1.Z);
			}
			else
			{
				triangle2 = new Triangle2(new Vector2(triangle.V0.X, triangle.V0.Y), new Vector2(triangle.V1.X, triangle.V1.Y), new Vector2(triangle.V2.X, triangle.V2.Y));
				end2 = new Vector2(end0.X, end0.Y);
				end3 = new Vector2(end1.X, end1.Y);
			}
			Segment2 segment = new Segment2(end2, end3);
			IntersectionSegment2Triangle2 intersectionSegment2Triangle = new IntersectionSegment2Triangle2(segment, triangle2);
			if (!intersectionSegment2Triangle.Find())
			{
				return false;
			}
			Vector2[] array = new Vector2[2];
			if (intersectionSegment2Triangle.IntersectionType == Intersection.Type.IT_SEGMENT)
			{
				this.IntersectionType = Intersection.Type.IT_SEGMENT;
				this.Quantity = 2;
				array[0] = intersectionSegment2Triangle.Point0;
				array[1] = intersectionSegment2Triangle.Point1;
			}
			else
			{
				MathBase.Assert(intersectionSegment2Triangle.IntersectionType == Intersection.Type.IT_POINT, "Triangle3Triangle3.IntersectsSegment(): intersection type is not point");
				this.IntersectionType = Intersection.Type.IT_POINT;
				this.Quantity = 1;
				array[0] = intersectionSegment2Triangle.Point0;
			}
			if (num == 0)
			{
				double num4 = 1.0 / plane.Normal.X;
				for (int i = 0; i < this.Quantity; i++)
				{
					this.Points[i].Y = array[i].X;
					this.Points[i].Z = array[i].Y;
					this.Points[i].X = num4 * (plane.Constant - plane.Normal.Y * this.Points[i].Y - plane.Normal.Z * this.Points[i].Z);
				}
			}
			else if (num == 1)
			{
				double num5 = 1.0 / plane.Normal.Y;
				for (int i = 0; i < this.Quantity; i++)
				{
					this.Points[i].X = array[i].X;
					this.Points[i].Z = array[i].Y;
					this.Points[i].Y = num5 * (plane.Constant - plane.Normal.X * this.Points[i].X - plane.Normal.Z * this.Points[i].Z);
				}
			}
			else
			{
				double num6 = 1.0 / plane.Normal.Z;
				for (int i = 0; i < this.Quantity; i++)
				{
					this.Points[i].X = array[i].X;
					this.Points[i].Y = array[i].Y;
					this.Points[i].Z = num6 * (plane.Constant - plane.Normal.X * this.Points[i].X - plane.Normal.Y * this.Points[i].Y);
				}
			}
			return true;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00014E70 File Offset: 0x00013070
		private bool GetCoplanarIntersection(Plane3 plane, Triangle3 tri0, Triangle3 tri1)
		{
			int num = 0;
			double num2 = Math.Abs(plane.Normal.X);
			double num3 = Math.Abs(plane.Normal.Y);
			if (num3 > num2)
			{
				num = 1;
				num2 = num3;
			}
			num3 = Math.Abs(plane.Normal.Z);
			if (num3 > num2)
			{
				num = 2;
			}
			Triangle2 triangle;
			Triangle2 triangle2;
			if (num == 0)
			{
				triangle = new Triangle2(new Vector2(tri0.V0.Y, tri0.V0.Z), new Vector2(tri0.V1.Y, tri0.V1.Z), new Vector2(tri0.V2.Y, tri0.V2.Z));
				triangle2 = new Triangle2(new Vector2(tri1.V0.Y, tri1.V0.Z), new Vector2(tri1.V1.Y, tri1.V1.Z), new Vector2(tri1.V2.Y, tri1.V2.Z));
			}
			else if (num == 1)
			{
				triangle = new Triangle2(new Vector2(tri0.V0.X, tri0.V0.Z), new Vector2(tri0.V1.X, tri0.V1.Z), new Vector2(tri0.V2.X, tri0.V2.Z));
				triangle2 = new Triangle2(new Vector2(tri1.V0.X, tri1.V0.Z), new Vector2(tri1.V1.X, tri1.V1.Z), new Vector2(tri1.V2.X, tri1.V2.Z));
			}
			else
			{
				triangle = new Triangle2(new Vector2(tri0.V0.X, tri0.V0.Y), new Vector2(tri0.V1.X, tri0.V1.Y), new Vector2(tri0.V2.X, tri0.V2.Y));
				triangle2 = new Triangle2(new Vector2(tri1.V0.X, tri1.V0.Y), new Vector2(tri1.V1.X, tri1.V1.Y), new Vector2(tri1.V2.X, tri1.V2.Y));
			}
			Vector2 vector = triangle.V1 - triangle.V0;
			Vector2 vector2 = triangle.V2 - triangle.V0;
			if (vector.DotPerpendicular(vector2) < 0.0)
			{
				triangle = new Triangle2(triangle.V0, triangle.V2, triangle.V1);
			}
			vector = triangle2.V1 - triangle2.V0;
			vector2 = triangle2.V2 - triangle2.V0;
			if (vector.DotPerpendicular(vector2) < 0.0)
			{
				triangle2 = new Triangle2(triangle2.V0, triangle2.V2, triangle2.V1);
			}
			IntersectionTriangle2Triangle2 intersectionTriangle2Triangle = new IntersectionTriangle2Triangle2(triangle, triangle2);
			if (!intersectionTriangle2Triangle.Find())
			{
				return false;
			}
			this.Quantity = intersectionTriangle2Triangle.Quantity;
			if (num == 0)
			{
				double num4 = 1.0 / plane.Normal.X;
				for (int i = 0; i < this.Quantity; i++)
				{
					this.Points[i].Y = intersectionTriangle2Triangle.Points[i].X;
					this.Points[i].Z = intersectionTriangle2Triangle.Points[i].Y;
					this.Points[i].X = num4 * (plane.Constant - plane.Normal.Y * this.Points[i].Y - plane.Normal.Z * this.Points[i].Z);
				}
			}
			else if (num == 1)
			{
				double num5 = 1.0 / plane.Normal.Y;
				for (int i = 0; i < this.Quantity; i++)
				{
					this.Points[i].X = intersectionTriangle2Triangle.Points[i].X;
					this.Points[i].Z = intersectionTriangle2Triangle.Points[i].Y;
					this.Points[i].Y = num5 * (plane.Constant - plane.Normal.X * this.Points[i].X - plane.Normal.Z * this.Points[i].Z);
				}
			}
			else
			{
				double num6 = 1.0 / plane.Normal.Z;
				for (int i = 0; i < this.Quantity; i++)
				{
					this.Points[i].X = intersectionTriangle2Triangle.Points[i].X;
					this.Points[i].Y = intersectionTriangle2Triangle.Points[i].Y;
					this.Points[i].Z = num6 * (plane.Constant - plane.Normal.X * this.Points[i].X - plane.Normal.Y * this.Points[i].Y);
				}
			}
			return true;
		}

		// Token: 0x04000145 RID: 325
		private readonly Triangle3 triangle0;

		// Token: 0x04000146 RID: 326
		private readonly Triangle3 triangle1;

		// Token: 0x02000089 RID: 137
		private class Query2
		{
			// Token: 0x060004F5 RID: 1269 RVA: 0x0001A7F9 File Offset: 0x000189F9
			public Query2(int quantity, Vector2[] vertices)
			{
				MathBase.Assert(quantity > 0 && vertices != null, "Triangle3Triangle3.Query2(): invalid params in constructor");
				this.Quantity = quantity;
				this.Vertices = vertices;
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0001A824 File Offset: 0x00018A24
			// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0001A82C File Offset: 0x00018A2C
			public int Quantity { get; private set; }

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001A835 File Offset: 0x00018A35
			// (set) Token: 0x060004F9 RID: 1273 RVA: 0x0001A83D File Offset: 0x00018A3D
			public Vector2[] Vertices { get; private set; }

			// Token: 0x060004FA RID: 1274 RVA: 0x0001A846 File Offset: 0x00018A46
			public int ToLine(int i, int iV0, int iV1)
			{
				return this.ToLine(this.Vertices[i], iV0, iV1);
			}

			// Token: 0x060004FB RID: 1275 RVA: 0x0001A85C File Offset: 0x00018A5C
			public int ToLine(Vector2 rkP, int iV0, int iV1)
			{
				bool flag = IntersectionTriangle3Triangle3.Query2.Sort(ref iV0, ref iV1);
				Vector2 vector = this.Vertices[iV0];
				Vector2 vector2 = this.Vertices[iV1];
				double fX = rkP.X - vector.X;
				double fY = rkP.Y - vector.Y;
				double fX2 = vector2.X - vector.X;
				double fY2 = vector2.Y - vector.Y;
				double num = IntersectionTriangle3Triangle3.Query2.Det2(fX, fY, fX2, fY2);
				if (!flag)
				{
					num = -num;
				}
				if (num > 0.0)
				{
					return 1;
				}
				if (num >= 0.0)
				{
					return 0;
				}
				return -1;
			}

			// Token: 0x060004FC RID: 1276 RVA: 0x0001A8FD File Offset: 0x00018AFD
			public int ToTriangle(int i, int iV0, int iV1, int iV2)
			{
				return this.ToTriangle(this.Vertices[i], iV0, iV1, iV2);
			}

			// Token: 0x060004FD RID: 1277 RVA: 0x0001A918 File Offset: 0x00018B18
			public int ToTriangle(Vector2 rkP, int iV0, int iV1, int iV2)
			{
				int num = this.ToLine(rkP, iV1, iV2);
				if (num > 0)
				{
					return 1;
				}
				int num2 = this.ToLine(rkP, iV0, iV2);
				if (num2 < 0)
				{
					return 1;
				}
				int num3 = this.ToLine(rkP, iV0, iV1);
				if (num3 > 0)
				{
					return 1;
				}
				if (num == 0 || num2 == 0 || num3 == 0)
				{
					return 0;
				}
				return -1;
			}

			// Token: 0x060004FE RID: 1278 RVA: 0x0001A963 File Offset: 0x00018B63
			public int ToCircumcircle(int i, int iV0, int iV1, int iV2)
			{
				return this.ToCircumcircle(this.Vertices[i], iV0, iV1, iV2);
			}

			// Token: 0x060004FF RID: 1279 RVA: 0x0001A97C File Offset: 0x00018B7C
			public int ToCircumcircle(Vector2 rkP, int iV0, int iV1, int iV2)
			{
				bool flag = IntersectionTriangle3Triangle3.Query2.Sort(ref iV0, ref iV1, ref iV2);
				Vector2 vector = this.Vertices[iV0];
				Vector2 vector2 = this.Vertices[iV1];
				Vector2 vector3 = this.Vertices[iV2];
				double num = vector.X + rkP.X;
				double num2 = vector.X - rkP.X;
				double num3 = vector.Y + rkP.Y;
				double num4 = vector.Y - rkP.Y;
				double num5 = vector2.X + rkP.X;
				double num6 = vector2.X - rkP.X;
				double num7 = vector2.Y + rkP.Y;
				double num8 = vector2.Y - rkP.Y;
				double num9 = vector3.X + rkP.X;
				double num10 = vector3.X - rkP.X;
				double num11 = vector3.Y + rkP.Y;
				double num12 = vector3.Y - rkP.Y;
				double fZ = num * num2 + num3 * num4;
				double fZ2 = num5 * num6 + num7 * num8;
				double fZ3 = num9 * num10 + num11 * num12;
				double num13 = IntersectionTriangle3Triangle3.Query2.Det3(num2, num4, fZ, num6, num8, fZ2, num10, num12, fZ3);
				if (!flag)
				{
					num13 = -num13;
				}
				if (num13 < 0.0)
				{
					return 1;
				}
				if (num13 <= 0.0)
				{
					return 0;
				}
				return -1;
			}

			// Token: 0x06000500 RID: 1280 RVA: 0x0001AAE8 File Offset: 0x00018CE8
			private static double Det2(double fX0, double fY0, double fX1, double fY1)
			{
				return fX0 * fY1 - fX1 * fY0;
			}

			// Token: 0x06000501 RID: 1281 RVA: 0x0001AAF4 File Offset: 0x00018CF4
			private static double Det3(double fX0, double fY0, double fZ0, double fX1, double fY1, double fZ1, double fX2, double fY2, double fZ2)
			{
				double num = fY1 * fZ2 - fY2 * fZ1;
				double num2 = fY2 * fZ0 - fY0 * fZ2;
				double num3 = fY0 * fZ1 - fY1 * fZ0;
				return fX0 * num + fX1 * num2 + fX2 * num3;
			}

			// Token: 0x06000502 RID: 1282 RVA: 0x0001AB30 File Offset: 0x00018D30
			private static bool Sort(ref int iV0, ref int iV1)
			{
				int num;
				int num2;
				bool result;
				if (iV0 < iV1)
				{
					num = 0;
					num2 = 1;
					result = true;
				}
				else
				{
					num = 1;
					num2 = 0;
					result = false;
				}
				int[] array = new int[]
				{
					iV0,
					iV1
				};
				iV0 = array[num];
				iV1 = array[num2];
				return result;
			}

			// Token: 0x06000503 RID: 1283 RVA: 0x0001AB70 File Offset: 0x00018D70
			private static bool Sort(ref int iV0, ref int iV1, ref int iV2)
			{
				int num;
				int num2;
				int num3;
				bool result;
				if (iV0 < iV1)
				{
					if (iV2 < iV0)
					{
						num = 2;
						num2 = 0;
						num3 = 1;
						result = true;
					}
					else if (iV2 < iV1)
					{
						num = 0;
						num2 = 2;
						num3 = 1;
						result = false;
					}
					else
					{
						num = 0;
						num2 = 1;
						num3 = 2;
						result = true;
					}
				}
				else if (iV2 < iV1)
				{
					num = 2;
					num2 = 1;
					num3 = 0;
					result = false;
				}
				else if (iV2 < iV0)
				{
					num = 1;
					num2 = 2;
					num3 = 0;
					result = true;
				}
				else
				{
					num = 1;
					num2 = 0;
					num3 = 2;
					result = false;
				}
				int[] array = new int[]
				{
					iV0,
					iV1,
					iV2
				};
				iV0 = array[num];
				iV1 = array[num2];
				iV2 = array[num3];
				return result;
			}
		}
	}
}
