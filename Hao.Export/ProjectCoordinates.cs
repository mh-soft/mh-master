using System;
using Hao.Export.Geometry;

namespace Hao.Export.MachineData
{
	// Token: 0x0200001F RID: 31
	public class ProjectCoordinates
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00009EA5 File Offset: 0x000080A5
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00009EAD File Offset: 0x000080AD
		public ItGePoint3d Origin { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00009EB6 File Offset: 0x000080B6
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00009EBE File Offset: 0x000080BE
		public ItGePoint3d YPoint { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00009EC7 File Offset: 0x000080C7
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00009ECF File Offset: 0x000080CF
		public ItGePoint3d XPoint { get; set; }

		// Token: 0x06000144 RID: 324 RVA: 0x00009ED8 File Offset: 0x000080D8
		public double[] getArray()
		{
			double[] array = new double[]
			{
				this.Origin.x,
				this.Origin.y,
				this.Origin.z,
				this.YPoint.x,
				this.YPoint.y,
				this.YPoint.z,
				this.XPoint.x,
				this.XPoint.y,
				this.XPoint.z
			};
			int num;
			for (int i = 0; i < array.Length; i = num + 1)
			{
				array[i] = Math.Round(array[i], 0);
				num = i;
			}
			return array;
		}

		// Token: 0x04000058 RID: 88
		public static ProjectCoordinates Empty = new ProjectCoordinates
		{
			Origin = ItGePoint3d.kOrigin,
			YPoint = ItGePoint3d.kOrigin,
			XPoint = ItGePoint3d.kOrigin
		};
	}
}
