using System;

namespace Hao.Geometry
{
	// Token: 0x02000033 RID: 51
	internal struct DistanceVector3Rectangle3
	{
		// Token: 0x0600022E RID: 558 RVA: 0x00009788 File Offset: 0x00007988
		public DistanceVector3Rectangle3(Vector3 vector, Rectangle3 rectangle)
		{
			this = default(DistanceVector3Rectangle3);
			Vector3 vector2 = rectangle.Center - vector;
			double num = vector2.Dot(rectangle.Axis0);
			double num2 = vector2.Dot(rectangle.Axis1);
			double num3 = -num;
			double num4 = -num2;
			double num5 = vector2.SquaredLength;
			if (num3 < -rectangle.Extent0)
			{
				num3 = -rectangle.Extent0;
			}
			else if (num3 > rectangle.Extent0)
			{
				num3 = rectangle.Extent0;
			}
			double num6 = num5;
			double num7 = num3;
			num5 = num6 + num7 * (num7 + 2.0 * num);
			if (num4 < -rectangle.Extent1)
			{
				num4 = -rectangle.Extent1;
			}
			else if (num4 > rectangle.Extent1)
			{
				num4 = rectangle.Extent1;
			}
			double num8 = num5;
			double num9 = num4;
			num5 = num8 + num9 * (num9 + 2.0 * num2);
			if (num5 < 0.0)
			{
				num5 = 0.0;
			}
			this.ClosestPointOnVector = vector;
			this.ClosestPointOnRectangle = rectangle.Center + num3 * rectangle.Axis0 + num4 * rectangle.Axis1;
			this.RectCoord0 = num3;
			this.RectCoord1 = num4;
			this.SquaredDistance = num5;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000098BD File Offset: 0x00007ABD
		// (set) Token: 0x06000230 RID: 560 RVA: 0x000098C5 File Offset: 0x00007AC5
		public double SquaredDistance { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000098CE File Offset: 0x00007ACE
		public double Distance
		{
			get
			{
				return Math.Sqrt(this.SquaredDistance);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000232 RID: 562 RVA: 0x000098DB File Offset: 0x00007ADB
		// (set) Token: 0x06000233 RID: 563 RVA: 0x000098E3 File Offset: 0x00007AE3
		public double RectCoord0 { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000234 RID: 564 RVA: 0x000098EC File Offset: 0x00007AEC
		// (set) Token: 0x06000235 RID: 565 RVA: 0x000098F4 File Offset: 0x00007AF4
		public double RectCoord1 { get; private set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000236 RID: 566 RVA: 0x000098FD File Offset: 0x00007AFD
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00009905 File Offset: 0x00007B05
		public Vector3 ClosestPointOnVector { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000990E File Offset: 0x00007B0E
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00009916 File Offset: 0x00007B16
		public Vector3 ClosestPointOnRectangle { get; private set; }
	}
}
