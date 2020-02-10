using System;
using System.Xml.Serialization;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x02000039 RID: 57
	public class ItSection : ItPXMLItem
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00013A71 File Offset: 0x00011C71
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x00013A79 File Offset: 0x00011C79
		[XmlElement(Order = 0)]
		public double L { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00013A84 File Offset: 0x00011C84
		[XmlIgnore]
		public bool LSpecified
		{
			get
			{
				return this.L > -ItDocument.epsilon;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00013AA4 File Offset: 0x00011CA4
		[XmlIgnore]
		public bool SSpecified
		{
			get
			{
				return !double.IsNaN(this.S);
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00013AC4 File Offset: 0x00011CC4
		public ItSection()
		{
			base.WBI();
		}

		// Token: 0x04000151 RID: 337
		[XmlElement(Order = 1)]
		public double S = double.NaN;
	}
}
