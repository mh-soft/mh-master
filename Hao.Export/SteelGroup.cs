using System;
using System.Collections.Generic;
using System.Linq;
using Hao.Export.Geometry;
using Hao.Export.MachineData.PXML;
using Hao.Export.Reinforcement;
using Autodesk.Revit.DB;

namespace Hao.Export.MachineData
{
	// Token: 0x02000021 RID: 33
	public class SteelGroup : IEquatable<SteelGroup>
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000A55C File Offset: 0x0000875C
		public Part HostPart { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000A564 File Offset: 0x00008764
		public string OwnerUid { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000A56C File Offset: 0x0000876C
		public PosReinfLayer ReinfLayer { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000A574 File Offset: 0x00008774
		public ReinforcementType PrecastReinfType { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000A57C File Offset: 0x0000877C
		public List<SteelGroupElement> Elements { get; }

		// Token: 0x0600015A RID: 346 RVA: 0x0000A584 File Offset: 0x00008784
		public SteelGroup(Part hostPart, string ownerUid, PosReinfLayer posReinfLayer, ReinforcementType precastReinfType)
		{
			this.HostPart = hostPart;
			this.OwnerUid = ownerUid;
			this.ReinfLayer = posReinfLayer;
			this.PrecastReinfType = precastReinfType;
			this.Elements = new List<SteelGroupElement>();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000A5B8 File Offset: 0x000087B8
		public bool Equals(SteelGroup other)
		{
			bool flag = other == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this == other;
				result = (flag2 || ((this.HostPart == other.HostPart || this.HostPart.Id == other.HostPart.Id) && this.ReinfLayer == other.ReinfLayer && this.PrecastReinfType == other.PrecastReinfType && string.Equals(this.OwnerUid, other.OwnerUid)));
			}
			return result;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000A640 File Offset: 0x00008840
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this == obj;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = obj.GetType() != base.GetType();
					result = (!flag3 && this.Equals((SteelGroup)obj));
				}
			}
			return result;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000A690 File Offset: 0x00008890
		public override int GetHashCode()
		{
			int num = (this.HostPart != null) ? this.HostPart.Id.IntegerValue : 0;
			num = (num * 397 ^ ((this.OwnerUid != null) ? this.OwnerUid.GetHashCode() : 0));
			num = (num * 397 ^ (int)this.ReinfLayer);
			return num * 397 ^ (int)this.PrecastReinfType;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000A700 File Offset: 0x00008900
		public static bool operator ==(SteelGroup left, SteelGroup right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000A71C File Offset: 0x0000891C
		public static bool operator !=(SteelGroup left, SteelGroup right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000A738 File Offset: 0x00008938
		public void AddElements(List<SteelGroupElement> members)
		{
			this.Elements.AddRange(members);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000A748 File Offset: 0x00008948
		internal void WriteUnitechnik(ICollection<ExportReinfData> exportedElements)
		{
			foreach (SteelGroupElement steelGroupElement in this.Elements)
			{
				steelGroupElement.WriteToUnitechnik(exportedElements);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000A7A4 File Offset: 0x000089A4
		internal ItSteel ToPXML(ICollection<ExportReinfData> exportedElements)
		{
			ItSteel steelBlock = new ItSteel();
			steelBlock.SteelType = this.GetSteelType();
			this.SetSteelBlockOffset(steelBlock);
			bool flag = steelBlock.SteelType == "mesh";
			if (flag)
			{
				steelBlock.MeshType = "0";
			}
			steelBlock.BorderStrength = 0;
			this.Elements.ForEach(delegate(SteelGroupElement elem)
			{
				elem.WriteToPXML(steelBlock, exportedElements);
			});
			return steelBlock;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000A844 File Offset: 0x00008A44
		private void SetSteelBlockOffset(ItSteel steelBlock)
		{
			List<ItGePoint3d> points = this.GetPoints(false);
			bool flag = points.none<ItGePoint3d>();
			if (!flag)
			{
				steelBlock.X = points.Min((ItGePoint3d p) => p.x);
				steelBlock.Y = points.Min((ItGePoint3d p) => p.y);
				steelBlock.Z = points.Min((ItGePoint3d p) => p.z);
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		public List<ItGePoint3d> GetPoints(bool schematic)
		{
			return this.Elements.SelectMany((SteelGroupElement element) => element.GetPoints(schematic)).ToList<ItGePoint3d>();
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000A930 File Offset: 0x00008B30
		private string GetSteelType()
		{
			bool flag = this.PrecastReinfType == ReinforcementType.LintelReinf;
			string result;
			if (flag)
			{
				result = "cage";
			}
			else
			{
				bool flag2 = this.Elements.OfType<SteelGroupElementMeshCFS>().any<SteelGroupElementMeshCFS>();
				if (flag2)
				{
					result = "mesh";
				}
				else
				{
					bool flag3 = this.Elements.OfType<SteelGroupElementMeshSFS>().any<SteelGroupElementMeshSFS>();
					if (flag3)
					{
						result = "extIron";
					}
					else
					{
						result = "none";
					}
				}
			}
			return result;
		}
	}
}
