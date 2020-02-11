using System;
using System.Globalization;
using System.IO;

namespace Hao.Geometry.Shapes.Tools
{
	// Token: 0x02000003 RID: 3
	public static class FacetedBRepWriterExtension
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002524 File Offset: 0x00000724
		public static void WriteToFile(this FacetedBRep facetedBRep, string fileName)
		{
			using (StreamWriter streamWriter = File.CreateText(fileName))
			{
				streamWriter.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
				streamWriter.WriteLine("<TSForeignModel Name=\"station\" Units=\"meters\">");
				streamWriter.WriteLine("<ForeignObject ExternalGUID=\"1234-1234-12341234\">");
				streamWriter.WriteLine("<Drawable Layer=\"numbers\" Color=\"ff8281ff\" DoubleSided=\"false\">");
				foreach (FacetedBRepFace facetedBRepFace in facetedBRep.Faces)
				{
					streamWriter.WriteLine("<Polygon>");
					streamWriter.WriteLine("<Outline>");
					foreach (Vector3 vector in facetedBRepFace.Vertices)
					{
						streamWriter.WriteLine(string.Concat(new string[]
						{
							"<Vertex x=\"",
							vector.X.ToString(CultureInfo.InvariantCulture),
							"\" y=\"",
							vector.Y.ToString(CultureInfo.InvariantCulture),
							"\" z=\"",
							vector.Z.ToString(CultureInfo.InvariantCulture),
							"\"/>"
						}));
					}
					streamWriter.WriteLine("</Outline>");
					streamWriter.WriteLine("</Polygon>");
				}
				streamWriter.WriteLine("</Drawable>");
				streamWriter.WriteLine("</ForeignObject>");
				streamWriter.WriteLine("</TSForeignModel>");
			}
		}
	}
}
