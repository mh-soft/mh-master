using System;

namespace Hao.Geometry
{
	// Token: 0x02000058 RID: 88
	internal struct IntersectionLine3Cylinder3
	{
		// Token: 0x06000372 RID: 882 RVA: 0x0000EF26 File Offset: 0x0000D126
		public IntersectionLine3Cylinder3(Line3 line, Cylinder3 cylinder)
		{
			this = default(IntersectionLine3Cylinder3);
			this.line = line;
			this.cylinder = cylinder;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000EF3D File Offset: 0x0000D13D
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000EF45 File Offset: 0x0000D145
		public Vector3 Point0 { get; private set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000EF4E File Offset: 0x0000D14E
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000EF56 File Offset: 0x0000D156
		public Vector3 Point1 { get; private set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000EF5F File Offset: 0x0000D15F
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000EF67 File Offset: 0x0000D167
		public Intersection.Type IntersectionType { get; private set; }

		// Token: 0x06000379 RID: 889 RVA: 0x0000EF70 File Offset: 0x0000D170
		public bool Find()
		{
			double[] array = new double[2];
			int num = IntersectionLine3Cylinder3.Find(this.line.Origin, this.line.Direction, this.cylinder, array);
			if (num == 2)
			{
				this.Point0 = this.line.Origin + array[0] * this.line.Direction;
				this.Point1 = this.line.Origin + array[1] * this.line.Direction;
				this.IntersectionType = Intersection.Type.IT_SEGMENT;
			}
			else if (num == 1)
			{
				this.Point0 = this.line.Origin + array[0] * this.line.Direction;
				this.IntersectionType = Intersection.Type.IT_POINT;
			}
			else
			{
				this.IntersectionType = Intersection.Type.IT_EMPTY;
			}
			return this.IntersectionType > Intersection.Type.IT_EMPTY;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000F064 File Offset: 0x0000D264
		internal static int Find(Vector3 origin, UnitVector3 dir, Cylinder3 cylinder, double[] t)
		{
			UnitVector3 direction = cylinder.Axis.Direction;
			UnitVector3 unitVector;
			UnitVector3 unitVector2;
			Vector3Factory.GenerateComplementBasis(out unitVector, out unitVector2, direction);
			double num = 0.5 * cylinder.Height;
			double num2 = cylinder.Radius * cylinder.Radius;
			Vector3 vector = origin - cylinder.Axis.Origin;
			Vector3 vector2 = new Vector3(unitVector.Dot(vector), unitVector2.Dot(vector), direction.Dot(vector));
			double num3 = direction.Dot(dir);
			if (Math.Abs(num3) >= 0.99999999)
			{
				if (num2 - vector2.X * vector2.X - vector2.Y * vector2.Y < 0.0)
				{
					return 0;
				}
				if (num3 > 0.0)
				{
					t[0] = -vector2.Z - num;
					t[1] = -vector2.Z + num;
				}
				else
				{
					t[0] = vector2.Z - num;
					t[1] = vector2.Z + num;
				}
				return 2;
			}
			else
			{
				Vector3 vector3 = new Vector3(unitVector.Dot(dir), unitVector2.Dot(dir), num3);
				if (Math.Abs(vector3.Z) <= 1E-08)
				{
					if (Math.Abs(vector2.Z) > num)
					{
						return 0;
					}
					double num4 = vector2.X * vector2.X + vector2.Y * vector2.Y - num2;
					double num5 = vector2.X * vector3.X + vector2.Y * vector3.Y;
					double num6 = vector3.X * vector3.X + vector3.Y * vector3.Y;
					double num7 = num5;
					double num8 = num7 * num7 - num4 * num6;
					if (num8 < -1E-08)
					{
						return 0;
					}
					if (num8 > 1E-08)
					{
						double num9 = Math.Sqrt(num8);
						double num10 = 1.0 / num6;
						t[0] = (-num5 - num9) * num10;
						t[1] = (-num5 + num9) * num10;
						return 2;
					}
					t[0] = -num5 / num6;
					return 1;
				}
				else
				{
					int num11 = 0;
					double num10 = 1.0 / vector3.Z;
					double num12 = (-num - vector2.Z) * num10;
					double num13 = vector2.X + num12 * vector3.X;
					double num14 = vector2.Y + num12 * vector3.Y;
					double num15 = num13 * num13;
					double num16 = num14;
					if (num15 + num16 * num16 <= num2)
					{
						t[num11++] = num12;
					}
					double num17 = (num - vector2.Z) * num10;
					double num18 = vector2.X + num17 * vector3.X;
					num14 = vector2.Y + num17 * vector3.Y;
					double num19 = num18 * num18;
					double num20 = num14;
					if (num19 + num20 * num20 <= num2)
					{
						t[num11++] = num17;
					}
					if (num11 == 2)
					{
						if (t[0] > t[1])
						{
							double num21 = t[0];
							t[0] = t[1];
							t[1] = num21;
						}
						return 2;
					}
					double num4 = vector2.X * vector2.X + vector2.Y * vector2.Y - num2;
					double num5 = vector2.X * vector3.X + vector2.Y * vector3.Y;
					double num6 = vector3.X * vector3.X + vector3.Y * vector3.Y;
					double num22 = num5;
					double num8 = num22 * num22 - num4 * num6;
					if (num8 < -1E-08)
					{
						MathBase.Assert(num11 == 0, "IntersectionLine3Cylinder3: Unexpected condition\n");
						return 0;
					}
					if (num8 > 1E-08)
					{
						double num9 = Math.Sqrt(num8);
						num10 = 1.0 / num6;
						double num23 = (-num5 - num9) * num10;
						if (num11 != 1 || Math.Abs(t[0] - num23) >= 1E-08)
						{
							if (num12 <= num17)
							{
								if (num12 <= num23 && num23 <= num17)
								{
									t[num11++] = num23;
								}
							}
							else if (num17 <= num23 && num23 <= num12)
							{
								t[num11++] = num23;
							}
						}
						if (num11 == 2)
						{
							if (t[0] > t[1])
							{
								double num24 = t[0];
								t[0] = t[1];
								t[1] = num24;
							}
							return 2;
						}
						num23 = (-num5 + num9) * num10;
						if (num12 <= num17)
						{
							if (num12 <= num23 && num23 <= num17)
							{
								t[num11++] = num23;
							}
						}
						else if (num17 <= num23 && num23 <= num12)
						{
							t[num11++] = num23;
						}
					}
					else
					{
						double num23 = -num5 / num6;
						if (num12 <= num17)
						{
							if (num12 <= num23 && num23 <= num17)
							{
								t[num11++] = num23;
							}
						}
						else if (num17 <= num23 && num23 <= num12)
						{
							t[num11++] = num23;
						}
					}
					if (num11 == 2 && t[0] > t[1])
					{
						double num25 = t[0];
						t[0] = t[1];
						t[1] = num25;
					}
					return num11;
				}
			}
		}

		// Token: 0x040000EC RID: 236
		private readonly Line3 line;

		// Token: 0x040000ED RID: 237
		private readonly Cylinder3 cylinder;
	}
}
