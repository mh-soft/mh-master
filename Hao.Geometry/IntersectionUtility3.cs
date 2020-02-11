using System;

namespace Hao.Geometry
{
	// Token: 0x02000072 RID: 114
	internal static class IntersectionUtility3
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x00015547 File Offset: 0x00013747
		public static int MaxIndex(double d0, double d1, double d2)
		{
			if (d0 > d1)
			{
				if (d0 > d2)
				{
					return 0;
				}
				return 2;
			}
			else
			{
				if (d1 > d2)
				{
					return 1;
				}
				return 2;
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001555C File Offset: 0x0001375C
		public static void GetProjection(Vector3 axis, Vector3[] segments, out double min, out double max)
		{
			double[] array = new double[]
			{
				axis.Dot(segments[0]),
				axis.Dot(segments[1])
			};
			min = array[0];
			max = min;
			if (array[1] < min)
			{
				min = array[1];
				return;
			}
			if (array[1] > max)
			{
				max = array[1];
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000155B4 File Offset: 0x000137B4
		public static void GetProjection(Vector3 axis, Triangle3 triangle, out double min, out double max)
		{
			double[] array = new double[]
			{
				axis.Dot(triangle.V0),
				axis.Dot(triangle.V1),
				axis.Dot(triangle.V2)
			};
			min = array[0];
			max = min;
			if (array[1] < min)
			{
				min = array[1];
			}
			else if (array[1] > max)
			{
				max = array[1];
			}
			if (array[2] < min)
			{
				min = array[2];
				return;
			}
			if (array[2] > max)
			{
				max = array[2];
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00015638 File Offset: 0x00013838
		public static void GetProjection(Vector3 axis, Box3 box, out double min, out double max)
		{
			double num = axis.Dot(box.Center);
			double num2 = Math.Abs(box.Extent0 * axis.Dot(box.Axis0)) + Math.Abs(box.Extent1 * axis.Dot(box.Axis1)) + Math.Abs(box.Extent2 * axis.Dot(box.Axis2));
			min = num - num2;
			max = num + num2;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x000156B4 File Offset: 0x000138B4
		public static void ClipConvexPolygonAgainstPlane(Vector3 normal, double constant, ref int quantity, ref Vector3[] vertices)
		{
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			int num4 = quantity;
			double[] array = new double[8];
			for (int i = 0; i < quantity; i++)
			{
				array[i] = normal.Dot(vertices[i]) - constant + Math.Abs(constant) * 1E-08;
				if (array[i] >= 0.0)
				{
					num++;
					if (num3 < 0)
					{
						num3 = i;
					}
				}
				else
				{
					num2++;
				}
			}
			if (quantity == 2)
			{
				if (num <= 0)
				{
					quantity = 0;
					return;
				}
				if (num2 > 0)
				{
					int num5;
					if (num3 == 0)
					{
						num5 = 1;
					}
					else
					{
						num5 = 0;
					}
					double scalar = array[num3] / (array[num3] - array[num5]);
					vertices[num5] = vertices[num3] + scalar * (vertices[num5] - vertices[num3]);
					return;
				}
			}
			else
			{
				if (num > 0)
				{
					if (num2 > 0)
					{
						Vector3[] array2 = new Vector3[8];
						int num6 = 0;
						if (num3 > 0)
						{
							int j = num3;
							int num7 = j - 1;
							double scalar2 = array[j] / (array[j] - array[num7]);
							array2[num6++] = vertices[j] + scalar2 * (vertices[num7] - vertices[j]);
							while (j < num4 && array[j] >= 0.0)
							{
								array2[num6++] = vertices[j++];
							}
							if (j < num4)
							{
								num7 = j - 1;
							}
							else
							{
								j = 0;
								num7 = num4 - 1;
							}
							scalar2 = array[j] / (array[j] - array[num7]);
							array2[num6++] = vertices[j] + scalar2 * (vertices[num7] - vertices[j]);
						}
						else
						{
							int j = 0;
							while (j < num4 && array[j] >= 0.0)
							{
								array2[num6++] = vertices[j++];
							}
							int num7 = j - 1;
							double scalar2 = array[j] / (array[j] - array[num7]);
							array2[num6++] = vertices[j] + scalar2 * (vertices[num7] - vertices[j]);
							while (j < num4 && array[j] < 0.0)
							{
								j++;
							}
							if (j < num4)
							{
								num7 = j - 1;
								scalar2 = array[j] / (array[j] - array[num7]);
								array2[num6++] = vertices[j] + scalar2 * (vertices[num7] - vertices[j]);
								while (j < num4)
								{
									if (array[j] < 0.0)
									{
										break;
									}
									array2[num6++] = vertices[j++];
								}
							}
							else
							{
								num7 = num4 - 1;
								scalar2 = array[0] / (array[0] - array[num7]);
								array2[num6++] = vertices[0] + scalar2 * (vertices[num7] - vertices[0]);
							}
						}
						num4 = num6;
						vertices = array2;
					}
					quantity = num4;
					return;
				}
				quantity = 0;
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00015A34 File Offset: 0x00013C34
		public static Vector3 GetPointFromIndex(int index, Box3 box)
		{
			Vector3 vector = box.Center;
			if ((index & 4) != 0)
			{
				vector += box.Extent2 * box.Axis2;
			}
			else
			{
				vector -= box.Extent2 * box.Axis2;
			}
			if ((index & 2) != 0)
			{
				vector += box.Extent1 * box.Axis1;
			}
			else
			{
				vector -= box.Extent1 * box.Axis1;
			}
			if ((index & 1) != 0)
			{
				vector += box.Extent0 * box.Axis0;
			}
			else
			{
				vector -= box.Extent0 * box.Axis0;
			}
			return vector;
		}
	}
}
