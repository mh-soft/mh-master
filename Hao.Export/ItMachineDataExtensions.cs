using System;
using System.Collections.Generic;
using Hao.Export.Geometry;

namespace Hao.Export.MachineData
{
	// Token: 0x02000010 RID: 16
	public static class ItMachineDataExtensions
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00004D70 File Offset: 0x00002F70
		public static List<MountingPartData> transformBy(this List<MountingPartData> listMountParts, ItGeMatrix3d mat)
		{
			bool flag = listMountParts == null;
			List<MountingPartData> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				foreach (MountingPartData mountingPartData in listMountParts)
				{
					bool flag2 = mountingPartData.Contour == null;
					if (!flag2)
					{
						mountingPartData.Contour.transformBy(mat);
					}
				}
				result = listMountParts;
			}
			return result;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004DEC File Offset: 0x00002FEC
		public static bool extents(this List<MountingPartData> listMountParts, out ItGePoint3d ptMin, out ItGePoint3d ptMax)
		{
			ptMin = null;
			ptMax = null;
			bool flag = listMountParts.none<MountingPartData>();
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				foreach (MountingPartData mountingPartData in listMountParts)
				{
					bool flag2 = mountingPartData.Contour == null;
					if (!flag2)
					{
						ItGePoint3d itGePoint3d;
						ItGePoint3d itGePoint3d2;
						bool flag3 = !mountingPartData.Contour.extents(out itGePoint3d, out itGePoint3d2);
						if (!flag3)
						{
							bool flag4 = ptMin == null;
							if (flag4)
							{
								ptMin = new ItGePoint3d(itGePoint3d);
							}
							else
							{
								ptMin.x = Math.Min(ptMin.x, itGePoint3d.x);
								ptMin.y = Math.Min(ptMin.y, itGePoint3d.y);
								ptMin.z = Math.Min(ptMin.z, itGePoint3d.z);
							}
							bool flag5 = ptMax == null;
							if (flag5)
							{
								ptMax = new ItGePoint3d(itGePoint3d2);
							}
							else
							{
								ptMax.x = Math.Max(ptMax.x, itGePoint3d2.x);
								ptMax.y = Math.Max(ptMax.y, itGePoint3d2.y);
								ptMax.z = Math.Max(ptMax.z, itGePoint3d2.z);
							}
						}
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004F74 File Offset: 0x00003174
		public static List<ItBracedGirderData> transformBy(this List<ItBracedGirderData> list, ItGeMatrix3d mat)
		{
			bool flag = list.isNull();
			List<ItBracedGirderData> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				foreach (ItBracedGirderData itBracedGirderData in list)
				{
					bool flag2 = itBracedGirderData.midline.isNull();
					if (!flag2)
					{
						itBracedGirderData.midline.transformBy(mat);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004FF4 File Offset: 0x000031F4
		public static bool extents(this List<ItBracedGirderData> list, out ItGePoint3d ptMin, out ItGePoint3d ptMax)
		{
			ptMin = null;
			ptMax = null;
			bool flag = list.none<ItBracedGirderData>();
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				foreach (ItBracedGirderData itBracedGirderData in list)
				{
					bool flag2 = itBracedGirderData.midline.isNull();
					if (!flag2)
					{
						ItGeBoundBlock3d itGeBoundBlock3d = itBracedGirderData.midline.boundBlock();
						bool flag3 = itGeBoundBlock3d == null;
						if (!flag3)
						{
							ItGePoint3d minPoint = itGeBoundBlock3d.minPoint;
							ItGePoint3d maxPoint = itGeBoundBlock3d.maxPoint;
							bool flag4 = ptMin == null;
							if (flag4)
							{
								ptMin = new ItGePoint3d(minPoint);
							}
							else
							{
								ptMin.x = Math.Min(ptMin.x, minPoint.x);
								ptMin.y = Math.Min(ptMin.y, minPoint.y);
								ptMin.z = Math.Min(ptMin.z, minPoint.z);
							}
							bool flag5 = ptMax == null;
							if (flag5)
							{
								ptMax = new ItGePoint3d(maxPoint);
							}
							else
							{
								ptMax.x = Math.Max(ptMax.x, maxPoint.x);
								ptMax.y = Math.Max(ptMax.y, maxPoint.y);
								ptMax.z = Math.Max(ptMax.z, maxPoint.z);
							}
						}
					}
				}
				result = (ptMin.isNotNull() && ptMax.isNotNull());
			}
			return result;
		}
	}
}
