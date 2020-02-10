using System;
using System.Xml;
using System.Xml.Serialization;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x02000037 RID: 55
	public class ItPXMLItem
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00013940 File Offset: 0x00011B40
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x00013948 File Offset: 0x00011B48
		[XmlAttribute(DataType = "string", AttributeName = "GlobalID")]
		public string globalID { get; set; }

		// Token: 0x0600042B RID: 1067 RVA: 0x000051E6 File Offset: 0x000033E6
		protected void WBI()
		{
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00013951 File Offset: 0x00011B51
		public ItPXMLItem()
		{
			this.WBI();
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00013969 File Offset: 0x00011B69
		public ItPXMLItem(XmlNode node)
		{
			this.WBI();
			this.m_node = node;
			this.globalID = "aaaa";
		}

		// Token: 0x04000149 RID: 329
		private XmlNode m_node = null;
	}
}
