using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x0200002C RID: 44
	public class ItAlloc : ItPXMLItem
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00010300 File Offset: 0x0000E500
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00010308 File Offset: 0x0000E508
		[XmlAttribute(DataType = "string", AttributeName = "Type")]
		public string type { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00010314 File Offset: 0x0000E514
		[XmlIgnore]
		public bool guidingBarSpecified
		{
			get
			{
				return this.guidingBar > -1;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00010330 File Offset: 0x0000E530
		[XmlIgnore]
		public bool regionListSpecified
		{
			get
			{
				return this.regionList.Count > 0;
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00010350 File Offset: 0x0000E550
		public ItAlloc()
		{
			base.WBI();
		}

		// Token: 0x040000A6 RID: 166
		[XmlElement("GuidingBar", Order = 0)]
		public int guidingBar = -1;

		// Token: 0x040000A7 RID: 167
		[XmlArray("Region", Order = 1)]
		public List<ItRegion> regionList = new List<ItRegion>();
	}
}
