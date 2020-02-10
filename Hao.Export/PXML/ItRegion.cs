using System;
using System.Xml.Serialization;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x02000038 RID: 56
	public class ItRegion : ItPXMLItem
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00013994 File Offset: 0x00011B94
		[XmlIgnore]
		public bool IntervalCountSpecified
		{
			get
			{
				return this.IntervalCount > 0;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x000139B0 File Offset: 0x00011BB0
		[XmlIgnore]
		public bool PitchSpecified
		{
			get
			{
				return !double.IsNaN(this.Pitch);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x000139D0 File Offset: 0x00011BD0
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x000139D8 File Offset: 0x00011BD8
		[XmlElement(Order = 2)]
		public bool IncludeBegin { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x000139E4 File Offset: 0x00011BE4
		[XmlIgnore]
		public bool IncludeBeginSpecified
		{
			get
			{
				return this.IncludeBegin;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x000139FC File Offset: 0x00011BFC
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x00013A04 File Offset: 0x00011C04
		[XmlElement(Order = 3)]
		public bool IncludeEnd { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00013A10 File Offset: 0x00011C10
		[XmlIgnore]
		public bool IncludeEndSpecified
		{
			get
			{
				return this.IncludeEnd;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00013A28 File Offset: 0x00011C28
		[XmlIgnore]
		public bool RefIndexSpecified
		{
			get
			{
				return this.RefIndex > -1;
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00013A43 File Offset: 0x00011C43
		public ItRegion()
		{
			base.WBI();
		}

		// Token: 0x0400014B RID: 331
		[XmlElement(Order = 0)]
		public int IntervalCount = -1;

		// Token: 0x0400014C RID: 332
		[XmlElement(Order = 1)]
		public double Pitch = double.NaN;

		// Token: 0x0400014F RID: 335
		[XmlElement(Order = 4)]
		public int RefIndex = -1;
	}
}
