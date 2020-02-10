using System;
using System.Collections.Generic;
using System.Linq;
using Hao.Export.Geometry;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;

namespace Hao.Export.MachineData
{
	// Token: 0x02000020 RID: 32
	public class ReinfSorter
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00009FD5 File Offset: 0x000081D5
		private ItMachineDataBase.CNCElementData CNCElementData { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00009FDD File Offset: 0x000081DD
		private ItGeMatrix3d MatWcsToPalette { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00009FE5 File Offset: 0x000081E5
		private Func<Element, bool> AdditionalFilter { get; }

		// Token: 0x0600014A RID: 330 RVA: 0x00009FF0 File Offset: 0x000081F0
		internal ReinfSorter(AssemblyInstance assembly, ItMachineDataBase.CNCElementData cncElementData, ItGeMatrix3d matWcsToPalette, Func<Element, bool> additionalFilter)
		{
			this.CNCElementData = cncElementData;
			this.MatWcsToPalette = matWcsToPalette;
			this.AdditionalFilter = additionalFilter;
			ICollection<ElementId> memberIds = assembly.GetMemberIds();
			this.members = (from id in memberIds
			select assembly.Document.GetElement(id)).ToList<Element>();
			this.steelGroupElements = new List<SteelGroupElement>();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000A05C File Offset: 0x0000825C
		public void SortElements()
		{
			this.SortAreaReinforcement();
			this.SortFabricReinforcement();
			this.SortRebars();
			this.SortGirders();
			this.MergeStandardFabrics();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000A084 File Offset: 0x00008284
		private void MergeStandardFabrics()
		{
			List<SteelGroupElementMeshSFS> list = this.steelGroupElements.OfType<SteelGroupElementMeshSFS>().ToList<SteelGroupElementMeshSFS>();
			bool flag = list.none<SteelGroupElementMeshSFS>();
			if (!flag)
			{
				List<SteelGroupElementMeshSFS> collection = SteelGroupElementMeshSFS.GroupSheets(list);
				foreach (SteelGroupElementMeshSFS item in list)
				{
					this.steelGroupElements.Remove(item);
				}
				this.steelGroupElements.AddRange(collection);
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000A110 File Offset: 0x00008310
		public ILookup<SteelGroup, SteelGroupElement> GetSteelGroups()
		{
			ILookup<SteelGroup, SteelGroupElement> lookup = this.steelGroupElements.ToLookup((SteelGroupElement element) => element.Group);
			using (IEnumerator<IGrouping<SteelGroup, SteelGroupElement>> enumerator = lookup.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IGrouping<SteelGroup, SteelGroupElement> group = enumerator.Current;
					group.Key.AddElements(group.ToList<SteelGroupElement>());
					group.ToList<SteelGroupElement>().ForEach(delegate(SteelGroupElement element)
					{
						element.Group = group.Key;
						element.SetReinforcementType();
					});
				}
			}
			return lookup;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000A1CC File Offset: 0x000083CC
		private void SortGirders()
		{
			List<FamilyInstance> list = null;// this.members.OfType<FamilyInstance>().Where(this.AdditionalFilter).ToList<FamilyInstance>();
            foreach (FamilyInstance element in list)
			{
				SteelGroupElement instance = SteelGroupElement.GetInstance(element, this.CNCElementData, this.MatWcsToPalette, this.steelGroupElements);
				bool flag = instance != null;
				if (flag)
				{
					this.steelGroupElements.Add(instance);
				}
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000A26C File Offset: 0x0000846C
		private void SortRebars()
		{
			List<Rebar> list = null;// this.members.OfType<Rebar>().Where(this.AdditionalFilter).ToList<Rebar>();
            foreach (Rebar element in list)
			{
				SteelGroupElement instance = SteelGroupElement.GetInstance(element, this.CNCElementData, this.MatWcsToPalette, this.steelGroupElements);
				bool flag = instance != null;
				if (flag)
				{
					this.steelGroupElements.Add(instance);
				}
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000A30C File Offset: 0x0000850C
		private void SortFabricReinforcement()
		{
            List<FabricSheet> list = null;// (this.members.OfType<FabricSheet>().Where(this.AdditionalFilter)).ToList<FabricSheet>();
			foreach (FabricSheet element in list)
			{
				SteelGroupElement instance = SteelGroupElement.GetInstance(element, this.CNCElementData, this.MatWcsToPalette);
				bool flag = instance != null;
				if (flag)
				{
					this.steelGroupElements.Add(instance);
				}
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000A3A4 File Offset: 0x000085A4
		private void SortAreaReinforcement()
		{
			List<RebarInSystem> list = null;// this.members.OfType<RebarInSystem>().Where(this.AdditionalFilter).ToList<RebarInSystem>();
            foreach (RebarInSystem element in list)
			{
				SteelGroupElement instance = SteelGroupElement.GetInstance(element, this.CNCElementData, this.MatWcsToPalette);
				bool flag = instance != null;
				if (flag)
				{
					this.steelGroupElements.Add(instance);
				}
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000A43C File Offset: 0x0000863C
		public IEnumerable<ItGePoint3d> GetPoints(bool schematic)
		{
			List<ItGePoint3d> list = new List<ItGePoint3d>();
			foreach (SteelGroupElement steelGroupElement in this.steelGroupElements)
			{
				list.AddRange(steelGroupElement.GetPoints(schematic));
			}
			return list;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000A4A8 File Offset: 0x000086A8
		public void TransformBy(ItGeMatrix3d transformation)
		{
			this.steelGroupElements.ForEach(delegate(SteelGroupElement elem)
			{
				elem.TransformBy(transformation);
			});
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000A4DC File Offset: 0x000086DC
		public double GetSteelVolumeInPart(RevitElement<Part> part)
		{
			double num = 0.0;
			ItSolid partSolid = part.getSolid(false, null, false, true).DeepClone();
			foreach (SteelGroupElement steelGroupElement in this.steelGroupElements)
			{
				num += steelGroupElement.GetSteelVolumeInPart(part, partSolid);
			}
			return num;
		}

		// Token: 0x0400005F RID: 95
		private readonly List<Element> members;

		// Token: 0x04000060 RID: 96
		private readonly List<SteelGroupElement> steelGroupElements;
	}
}
