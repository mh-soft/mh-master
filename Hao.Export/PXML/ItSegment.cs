using System;
using System.Xml.Serialization;
using Hao.Export.Geometry;

namespace Hao.Export.MachineData.PXML
{
	// Token: 0x0200003A RID: 58
	public class ItSegment : ItPXMLItem
	{
		// Token: 0x0600043D RID: 1085 RVA: 0x00013AE4 File Offset: 0x00011CE4
		private ItSegment()
		{
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00013B0C File Offset: 0x00011D0C
		public ItSegment(double l, double r, double rotX, double bendY, ItGeMatrix3d matrix, string segmentType = "normal")
		{
			this.type = segmentType;
			this.L = l;
			this.R = r;
			this.RotX = rotX;
			this.BendY = bendY;
			ItGePoint3d itGePoint3d;
			ItGeVector3d xvector;
			ItGeVector3d yvector;
			ItGeVector3d zvector;
			matrix.getCoordSystem(out itGePoint3d, out xvector, out yvector, out zvector);
			this.XVector = xvector;
			this.YVector = yvector;
			this.ZVector = zvector;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00013B8F File Offset: 0x00011D8F
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x00013B97 File Offset: 0x00011D97
		[XmlAttribute(DataType = "string", AttributeName = "Type")]
		public string type { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00013BA0 File Offset: 0x00011DA0
		[XmlIgnore]
		public bool RotXSpecified
		{
			get
			{
				return !double.IsNaN(this.RotX);
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00013BC0 File Offset: 0x00011DC0
		[XmlIgnore]
		public bool BendYSpecified
		{
			get
			{
				return !double.IsNaN(this.BendY);
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00013BE0 File Offset: 0x00011DE0
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x00013BE8 File Offset: 0x00011DE8
		[XmlElement(Order = 2)]
		public double L { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00013BF4 File Offset: 0x00011DF4
		[XmlIgnore]
		public bool LSpecified
		{
			get
			{
				return this.L > ItDocument.epsilon;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00013C13 File Offset: 0x00011E13
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x00013C1B File Offset: 0x00011E1B
		[XmlElement(Order = 3)]
		public double R { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00013C24 File Offset: 0x00011E24
		[XmlIgnore]
		public bool RSpecified
		{
			get
			{
				return this.R > ItDocument.epsilon;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00013C43 File Offset: 0x00011E43
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x00013C4B File Offset: 0x00011E4B
		[XmlIgnore]
		public ItGeVector3d XVector { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00013C54 File Offset: 0x00011E54
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x00013C5C File Offset: 0x00011E5C
		[XmlIgnore]
		public ItGeVector3d YVector { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00013C65 File Offset: 0x00011E65
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x00013C6D File Offset: 0x00011E6D
		[XmlIgnore]
		public ItGeVector3d ZVector { get; set; }

		// Token: 0x04000152 RID: 338
		public const string NormalType = "normal";

		// Token: 0x04000153 RID: 339
		public const string SpiralType = "spiral";

		// Token: 0x04000155 RID: 341
		[XmlElement(Order = 0)]
		public double RotX = double.NaN;

		// Token: 0x04000156 RID: 342
		[XmlElement(Order = 1)]
		public double BendY = double.NaN;
	}
}
