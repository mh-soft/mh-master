using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x0200003B RID: 59
	public class ItShape : ItPXMLItem
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00013C76 File Offset: 0x00011E76
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x00013C7E File Offset: 0x00011E7E
		public bool Cutout { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00013C88 File Offset: 0x00011E88
		[XmlIgnore]
		public bool CutoutSpecified
		{
			get
			{
				return this.Cutout;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00013CA0 File Offset: 0x00011EA0
		[XmlIgnore]
		public bool RefHeightSpecified
		{
			get
			{
				return !double.IsNaN(this.RefHeight);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00013CC0 File Offset: 0x00011EC0
		[XmlIgnore]
		public bool sVertexListSpecified
		{
			get
			{
				return this.sVertexList.Count > 0;
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00013CE0 File Offset: 0x00011EE0
		public ItShape()
		{
			base.WBI();
		}

		// Token: 0x0400015D RID: 349
		public double RefHeight = double.NaN;

		// Token: 0x0400015E RID: 350
		[XmlElement("SVertex")]
		public List<ItSVertex> sVertexList = new List<ItSVertex>();
	}
}
