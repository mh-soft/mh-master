using System;
using System.Xml.Serialization;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x0200003D RID: 61
	public class ItSpacer : ItPXMLItem
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x000145E0 File Offset: 0x000127E0
		[XmlIgnore]
		public bool TypeSpecified
		{
			get
			{
				return this.Type > -1;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x000145FC File Offset: 0x000127FC
		[XmlIgnore]
		public bool PositionSpecified
		{
			get
			{
				return !double.IsNaN(this.Position);
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001461C File Offset: 0x0001281C
		public ItSpacer()
		{
			base.WBI();
		}

		// Token: 0x04000188 RID: 392
		[XmlElement(Order = 0)]
		public int Type = -1;

		// Token: 0x04000189 RID: 393
		[XmlElement(Order = 1)]
		public double Position = double.NaN;
	}
}
