using System;
using System.Collections.Generic;

namespace Hao.Geometry
{
	// Token: 0x02000084 RID: 132
	public static class Transform3ExtensionMethods
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x00017F48 File Offset: 0x00016148
		public static AffineTransform3 GetInversed(this AffineTransform3 transform)
		{
			UnitVector3 axisX = new UnitVector3(transform.AxisX.X, transform.AxisY.X, transform.AxisZ.X);
			UnitVector3 axisY = new UnitVector3(transform.AxisX.Y, transform.AxisY.Y, transform.AxisZ.Y);
			UnitVector3 axisZ = new UnitVector3(transform.AxisX.Z, transform.AxisY.Z, transform.AxisZ.Z);
			Vector3 origin = new Vector3(-transform.AxisX.Dot(transform.Origin), -transform.AxisY.Dot(transform.Origin), -transform.AxisZ.Dot(transform.Origin)) / transform.Scale;
			return new AffineTransform3(axisX, axisY, axisZ, origin, 1.0 / transform.Scale);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00018060 File Offset: 0x00016260
		public static AffineTransform2 GetInversed(this AffineTransform2 transform)
		{
			UnitVector2 axisX = new UnitVector2(transform.AxisX.X, transform.AxisY.X);
			UnitVector2 axisY = new UnitVector2(transform.AxisX.Y, transform.AxisY.Y);
			Vector2 origin = new Vector2(-transform.AxisX.Dot(transform.Origin), -transform.AxisY.Dot(transform.Origin)) / transform.Scale;
			return new AffineTransform2(axisX, axisY, origin, 1.0 / transform.Scale);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001810D File Offset: 0x0001630D
		public static AffineTransform3 Transform(this AffineTransform3 transformer, AffineTransform3 source)
		{
			return Transform3Factory.Combine(source, transformer);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00018118 File Offset: 0x00016318
		public static AffineTransform2 Transform(this AffineTransform2 transformer, AffineTransform2 source)
		{
			UnitVector2 axisX = transformer.AxisX;
			UnitVector2 axisY = transformer.AxisY;
			double scale = transformer.Scale;
			UnitVector2 axisX2 = source.AxisX;
			UnitVector2 axisY2 = source.AxisY;
			Vector2 origin = transformer.Origin;
			Vector2 origin2 = source.Origin;
			double x = axisX.X * axisX2.X + axisY.X * axisX2.Y;
			double x2 = axisX.X * axisY2.X + axisY.X * axisY2.Y;
			double y = axisX.Y * axisX2.X + axisY.Y * axisX2.Y;
			double y2 = axisX.Y * axisY2.X + axisY.Y * axisY2.Y;
			double x3 = scale * (axisX.X * origin2.X + axisY.X * origin2.Y) + origin.X;
			double y3 = scale * (axisX.Y * origin2.X + axisY.Y * origin2.Y) + origin.Y;
			UnitVector2 axisX3 = new UnitVector2(x, y);
			UnitVector2 axisY3 = new UnitVector2(x2, y2);
			Vector2 origin3 = new Vector2(x3, y3);
			return new AffineTransform2(axisX3, axisY3, origin3, transformer.Scale * source.Scale);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00018270 File Offset: 0x00016470
		public static Vector3 Transform(this AffineTransform3 transformer, Vector3 input)
		{
			return transformer.Origin + transformer.Scale * (transformer.AxisX * input.X + transformer.AxisY * input.Y + transformer.AxisZ * input.Z);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000182D8 File Offset: 0x000164D8
		public static Vector2 Transform(this AffineTransform2 transformer, Vector2 input)
		{
			return transformer.Origin + transformer.Scale * (transformer.AxisX * input.X + transformer.AxisY * input.Y);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00018328 File Offset: 0x00016528
		public static UnitVector3 Transform(this AffineTransform3 transformer, UnitVector3 input)
		{
			return (transformer.AxisX * input.X + transformer.AxisY * input.Y + transformer.AxisZ * input.Z).GetNormalized();
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00018380 File Offset: 0x00016580
		public static UnitVector2 Transform(this AffineTransform2 transformer, UnitVector2 input)
		{
			return (transformer.AxisX * input.X + transformer.AxisY * input.Y).GetNormalized();
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000183C0 File Offset: 0x000165C0
		public static Angle Transform(this AffineTransform2 transformer, Angle input)
		{
			double num = Math.Acos(transformer.AxisX.Dot(UnitVector2.UnitX));
			if (transformer.AxisX.Dot(UnitVector2.UnitY) < 0.0)
			{
				num *= -1.0;
			}
			Angle right = Angle.FromRadians(num);
			return input + right;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00018420 File Offset: 0x00016620
		public static Triangle3 Transform(this AffineTransform3 transformer, Triangle3 input)
		{
			return new Triangle3(transformer.Transform(input.V0), transformer.Transform(input.V1), transformer.Transform(input.V2));
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001844E File Offset: 0x0001664E
		public static Triangle2 Transform(this AffineTransform2 transformer, Triangle2 input)
		{
			return new Triangle2(transformer.Transform(input.V0), transformer.Transform(input.V1), transformer.Transform(input.V2));
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001847C File Offset: 0x0001667C
		public static AxisAlignedBox3 Transform(this AffineTransform3 transformer, AxisAlignedBox3 input)
		{
			Vector3 vector = transformer.Transform(input.Center - input.Extents);
			Vector3 vector2 = vector;
			foreach (Vector3 right in new List<Vector3>
			{
				new Vector3(input.ExtentX, -input.ExtentY, -input.ExtentZ),
				new Vector3(-input.ExtentX, input.ExtentY, -input.ExtentZ),
				new Vector3(input.ExtentX, input.ExtentY, -input.ExtentZ),
				new Vector3(-input.ExtentX, -input.ExtentY, input.ExtentZ),
				new Vector3(input.ExtentX, -input.ExtentY, input.ExtentZ),
				new Vector3(-input.ExtentX, input.ExtentY, input.ExtentZ),
				new Vector3(input.ExtentX, input.ExtentY, input.ExtentZ)
			})
			{
				Vector3 value = transformer.Transform(input.Center + right);
				vector = Vector3.Min(vector, value);
				vector2 = Vector3.Max(vector2, value);
			}
			return new AxisAlignedBox3(vector, vector2);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000185FC File Offset: 0x000167FC
		public static AxisAlignedBox2 Transform(this AffineTransform2 transformer, AxisAlignedBox2 input)
		{
			Vector2 vector = transformer.Transform(input.Center - input.Extents);
			Vector2 vector2 = vector;
			foreach (Vector2 right in new List<Vector2>
			{
				new Vector2(input.ExtentX, -input.ExtentY),
				new Vector2(-input.ExtentX, input.ExtentY),
				new Vector2(input.ExtentX, input.ExtentY)
			})
			{
				Vector2 value = transformer.Transform(input.Center + right);
				vector = Vector2.Min(vector, value);
				vector2 = Vector2.Max(vector2, value);
			}
			return new AxisAlignedBox2(vector, vector2);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000186E0 File Offset: 0x000168E0
		public static Box3 Transform(this AffineTransform3 transformer, Box3 box)
		{
			AffineTransform3 source = new AffineTransform3(box.Axis0, box.Axis1, box.Axis2, box.Center);
			AffineTransform3 affineTransform = transformer.Transform(source);
			return new Box3(affineTransform.Origin, affineTransform.AxisX, affineTransform.AxisY, affineTransform.AxisZ, box.Extent0 * transformer.Scale, box.Extent1 * transformer.Scale, box.Extent2 * transformer.Scale);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00018768 File Offset: 0x00016968
		public static Box2 Transform(this AffineTransform2 transformer, Box2 box)
		{
			AffineTransform2 source = new AffineTransform2(box.Axis0, box.Axis1, box.Center, 1.0);
			AffineTransform2 affineTransform = transformer.Transform(source);
			return new Box2(affineTransform.Origin, affineTransform.AxisX, affineTransform.AxisY, box.Extent0 * transformer.Scale, box.Extent1 * transformer.Scale);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000187DC File Offset: 0x000169DC
		public static Rectangle3 Transform(this AffineTransform3 transformer, Rectangle3 rectangle)
		{
			AffineTransform3 source = new AffineTransform3(rectangle.Axis0, rectangle.Axis1, rectangle.Axis0.UnitCross(rectangle.Axis1), rectangle.Center, 1.0);
			AffineTransform3 affineTransform = transformer.Transform(source);
			return new Rectangle3(affineTransform.Origin, affineTransform.AxisX, affineTransform.AxisY, rectangle.Extent0 * transformer.Scale, rectangle.Extent1 * transformer.Scale);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00018864 File Offset: 0x00016A64
		public static Ellipse2 Transform(this AffineTransform2 transformer, Ellipse2 ellipse)
		{
			AffineTransform2 source = new AffineTransform2(ellipse.Axis0, ellipse.Axis1, ellipse.Center, 1.0);
			AffineTransform2 affineTransform = transformer.Transform(source);
			return new Ellipse2(affineTransform.Origin, affineTransform.AxisX, affineTransform.AxisY, ellipse.Extent0 * transformer.Scale, ellipse.Extent1 * transformer.Scale);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000188D6 File Offset: 0x00016AD6
		public static EllipticArc2 Transform(this AffineTransform2 transformer, EllipticArc2 ellipticArc)
		{
			return new EllipticArc2(transformer.Transform(ellipticArc.Ellipse), ellipticArc.StartAngle, ellipticArc.DeltaAngle);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000188F8 File Offset: 0x00016AF8
		public static Circle3 Transform(this AffineTransform3 transformer, Circle3 circle)
		{
			AffineTransform3 source = new AffineTransform3(circle.UnitU, circle.UnitV, circle.Normal, circle.Center);
			AffineTransform3 affineTransform = transformer.Transform(source);
			return new Circle3(affineTransform.Origin, affineTransform.AxisX, affineTransform.AxisY, affineTransform.AxisZ, circle.Radius * transformer.Scale);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00018960 File Offset: 0x00016B60
		public static Circle2 Transform(this AffineTransform2 transformer, Circle2 circle)
		{
			return new Circle2(transformer.Transform(circle.Center), circle.Radius * transformer.Scale);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00018983 File Offset: 0x00016B83
		public static Arc3 Transform(this AffineTransform3 transformer, Arc3 arc)
		{
			return new Arc3(transformer.Transform(arc.Circle), arc.StartAngle, arc.DeltaAngle);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000189A8 File Offset: 0x00016BA8
		public static Arc2 Transform(this AffineTransform2 transformer, Arc2 arc)
		{
			Circle2 circle = transformer.Transform(arc.Circle);
			Angle startAngle = transformer.Transform(arc.StartAngle);
			return new Arc2(circle, startAngle, arc.DeltaAngle);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000189E0 File Offset: 0x00016BE0
		public static Line3 Transform(this AffineTransform3 transformer, Line3 line)
		{
			Vector3 origin = transformer.Transform(line.Origin);
			UnitVector3 direction = transformer.Transform(line.Direction);
			return new Line3(origin, direction);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00018A10 File Offset: 0x00016C10
		public static Line2 Transform(this AffineTransform2 transformer, Line2 line)
		{
			Vector2 origin = transformer.Transform(line.Origin);
			UnitVector2 direction = transformer.Transform(line.Direction);
			return new Line2(origin, direction);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00018A40 File Offset: 0x00016C40
		public static Ray3 Transform(this AffineTransform3 transformer, Ray3 ray)
		{
			Vector3 origin = transformer.Transform(ray.Origin);
			UnitVector3 direction = transformer.Transform(ray.Direction);
			return new Ray3(origin, direction);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00018A70 File Offset: 0x00016C70
		public static Ray2 Transform(this AffineTransform2 transformer, Ray2 ray)
		{
			Vector2 origin = transformer.Transform(ray.Origin);
			UnitVector2 direction = transformer.Transform(ray.Direction);
			return new Ray2(origin, direction);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00018AA0 File Offset: 0x00016CA0
		public static Segment3 Transform(this AffineTransform3 transformer, Segment3 segment)
		{
			Vector3 origin = transformer.Transform(segment.Origin);
			UnitVector3 direction = transformer.Transform(segment.Direction);
			return new Segment3(origin, direction, transformer.Scale * segment.Extent);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00018AE0 File Offset: 0x00016CE0
		public static Segment2 Transform(this AffineTransform2 transformer, Segment2 segment)
		{
			Vector2 origin = transformer.Transform(segment.Origin);
			UnitVector2 direction = transformer.Transform(segment.Direction);
			return new Segment2(origin, direction, transformer.Scale * segment.Extent);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00018B20 File Offset: 0x00016D20
		public static Plane3 Transform(this AffineTransform3 transformer, Plane3 plane)
		{
			UnitVector3 planeNormal = transformer.Transform(plane.Normal);
			Vector3 pointOnThePlane = transformer.Transform(plane.Normal * plane.Constant);
			return new Plane3(planeNormal, pointOnThePlane);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00018B5A File Offset: 0x00016D5A
		public static Cylinder3 Transform(this AffineTransform3 transformer, Cylinder3 cylinder)
		{
			return new Cylinder3(transformer.Transform(cylinder.Axis), transformer.Scale * cylinder.Radius, transformer.Scale * cylinder.Height);
		}
	}
}
