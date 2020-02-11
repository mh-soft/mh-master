using System;
using System.Collections.Generic;

namespace Hao.Geometry.Algorithms
{
	// Token: 0x02000003 RID: 3
	public class KdTree3
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002050 File Offset: 0x00000250
		public KdTree3(double epsilon)
		{
			this.Epsilon = epsilon;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000206A File Offset: 0x0000026A
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002072 File Offset: 0x00000272
		public double Epsilon { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000207B File Offset: 0x0000027B
		public Vector3[] Vertices
		{
			get
			{
				return this.vertices.ToArray();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002088 File Offset: 0x00000288
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002090 File Offset: 0x00000290
		public KdTreeNode3 Root { get; private set; }

		// Token: 0x0600000B RID: 11 RVA: 0x0000209C File Offset: 0x0000029C
		public int Insert(Vector3 value)
		{
			if (this.Root == null)
			{
				int count = this.vertices.Count;
				this.vertices.Add(value);
				KdTreeNode3 root = new KdTreeNode3(count);
				this.Root = root;
				return count;
			}
			return KdTree3.RecurseX(this.vertices, this.Root, ref value, this.Epsilon);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020F0 File Offset: 0x000002F0
		private static int RecurseX(IList<Vector3> vertices, KdTreeNode3 treeNode, ref Vector3 value, double epsilon)
		{
			Vector3 treeValue = vertices[treeNode.Index];
			if (KdTree3.Compare(treeValue, ref value, epsilon))
			{
				return treeNode.Index;
			}
			if (value.X - treeValue.X >= -epsilon * 4.0)
			{
				if (treeNode.Front != null)
				{
					return KdTree3.RecurseY(vertices, treeNode.Front, ref value, epsilon);
				}
				int count = vertices.Count;
				vertices.Add(value);
				treeNode.Front = new KdTreeNode3(count);
				return count;
			}
			else
			{
				if (treeNode.Back != null)
				{
					return KdTree3.RecurseY(vertices, treeNode.Back, ref value, epsilon);
				}
				int count2 = vertices.Count;
				vertices.Add(value);
				treeNode.Back = new KdTreeNode3(count2);
				return count2;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021A8 File Offset: 0x000003A8
		private static int RecurseY(IList<Vector3> vertices, KdTreeNode3 treeNode, ref Vector3 value, double epsilon)
		{
			Vector3 treeValue = vertices[treeNode.Index];
			if (KdTree3.Compare(treeValue, ref value, epsilon))
			{
				return treeNode.Index;
			}
			if (value.Y - treeValue.Y >= -epsilon * 4.0)
			{
				if (treeNode.Front != null)
				{
					return KdTree3.RecurseZ(vertices, treeNode.Front, ref value, epsilon);
				}
				int count = vertices.Count;
				vertices.Add(value);
				treeNode.Front = new KdTreeNode3(count);
				return count;
			}
			else
			{
				if (treeNode.Back != null)
				{
					return KdTree3.RecurseZ(vertices, treeNode.Back, ref value, epsilon);
				}
				int count2 = vertices.Count;
				vertices.Add(value);
				treeNode.Back = new KdTreeNode3(count2);
				return count2;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002260 File Offset: 0x00000460
		private static int RecurseZ(IList<Vector3> vertices, KdTreeNode3 treeNode, ref Vector3 value, double epsilon)
		{
			Vector3 treeValue = vertices[treeNode.Index];
			if (KdTree3.Compare(treeValue, ref value, epsilon))
			{
				return treeNode.Index;
			}
			if (value.Z - treeValue.Z >= -epsilon * 4.0)
			{
				if (treeNode.Front != null)
				{
					return KdTree3.RecurseX(vertices, treeNode.Front, ref value, epsilon);
				}
				int count = vertices.Count;
				vertices.Add(value);
				treeNode.Front = new KdTreeNode3(count);
				return count;
			}
			else
			{
				if (treeNode.Back != null)
				{
					return KdTree3.RecurseX(vertices, treeNode.Back, ref value, epsilon);
				}
				int count2 = vertices.Count;
				vertices.Add(value);
				treeNode.Back = new KdTreeNode3(count2);
				return count2;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002318 File Offset: 0x00000518
		private static bool Compare(Vector3 treeValue, ref Vector3 newValue, double epsilon)
		{
			return Math.Abs(treeValue.X - newValue.X) < epsilon && Math.Abs(treeValue.Y - newValue.Y) < epsilon && Math.Abs(treeValue.Z - newValue.Z) < epsilon;
		}

		// Token: 0x04000001 RID: 1
		private readonly List<Vector3> vertices = new List<Vector3>();
	}
}
