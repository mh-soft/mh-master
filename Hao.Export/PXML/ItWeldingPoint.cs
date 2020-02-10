using System;
using System.Xml.Serialization;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x02000041 RID: 65
	public class ItWeldingPoint : ItPXMLItem
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00014E5C File Offset: 0x0001305C
		[XmlIgnore]
		public bool WeldingOutputSpecified
		{
			get
			{
				return !double.IsNaN(this.WeldingOutput);
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00014E7C File Offset: 0x0001307C
		[XmlIgnore]
		public bool PositionSpecified
		{
			get
			{
				return !double.IsNaN(this.Position);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00014E9C File Offset: 0x0001309C
		[XmlIgnore]
		public bool WeldingPointTypeSpecified
		{
			get
			{
				return this.WeldingPointType > -1;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00014EB8 File Offset: 0x000130B8
		[XmlIgnore]
		public bool WeldingPrgNoSpecified
		{
			get
			{
				return this.WeldingPrgNo > -1;
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00014ED3 File Offset: 0x000130D3
		public ItWeldingPoint()
		{
			base.WBI();
		}

		// Token: 0x040001AD RID: 429
		[XmlElement(Order = 0)]
		public double WeldingOutput = double.NaN;

		// Token: 0x040001AE RID: 430
		[XmlElement(Order = 1)]
		public double Position = double.NaN;

		// Token: 0x040001AF RID: 431
		[XmlElement(Order = 2)]
		public int WeldingPointType = -1;

		// Token: 0x040001B0 RID: 432
		[XmlElement(Order = 3)]
		public int WeldingPrgNo = -1;
	}
}
