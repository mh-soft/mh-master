using System;
using System.Collections.Generic;

namespace Hao.Geometry.Algorithms
{
	// Token: 0x0200000A RID: 10
	public class QuantizedAxisAlignedBox3Tree
	{
		// Token: 0x06000031 RID: 49 RVA: 0x0000285C File Offset: 0x00000A5C
		public QuantizedAxisAlignedBox3Tree(IList<InputBox> inputBoxes)
		{
			this.globalAabb = QuantizedAxisAlignedBox3Tree.MergeBoxes(inputBoxes, 0, inputBoxes.Count);
			Vector3 vector = this.globalAabb.Max - this.globalAabb.Min;
			this.bvhQuantization = new Vector3(65535.0 / vector.X, 65535.0 / vector.Y, 65535.0 / vector.Z);
			int num = inputBoxes.Count * 2 - 1;
			this.quantizedAabbTreeNodes = new QuantizedAxisAlignedBox3TreeNode[num];
			QuantizedAxisAlignedBox3 quantizedAxisAlignedBox;
			this.BuildSubTree(0, inputBoxes, 0, inputBoxes.Count, out quantizedAxisAlignedBox);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002905 File Offset: 0x00000B05
		public Vector3 Quantization
		{
			get
			{
				return this.bvhQuantization;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000290D File Offset: 0x00000B0D
		public int DataNodeCount
		{
			get
			{
				return (this.quantizedAabbTreeNodes.Length + 1) / 2;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000291C File Offset: 0x00000B1C
		public ICollection<int> BoxQuery(AxisAlignedBox3 box)
		{
			List<int> list = new List<int>();
			QuantizedAxisAlignedBox3 quantizedAabb = this.Quantize(box);
			int i = 0;
			while (i < this.quantizedAabbTreeNodes.Length)
			{
				bool flag = this.TestQuantizedBoxOverlap(i, quantizedAabb);
				bool flag2 = this.IsDataNode(i);
				if (flag2 && flag)
				{
					list.Add(this.GetNodeDataValue(i));
					i++;
				}
				else if (flag2 || flag)
				{
					i++;
				}
				else
				{
					i += this.GetBranchNodeWidth(i);
				}
			}
			return list;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002990 File Offset: 0x00000B90
		public ICollection<int> RayQuery(Ray3 ray)
		{
			List<int> list = new List<int>();
			int i = 0;
			int num = this.quantizedAabbTreeNodes.Length;
			while (i < num)
			{
				bool flag = this.GetAabb(i).CollideRay(ray);
				bool flag2 = this.IsDataNode(i);
				if (flag2 && flag)
				{
					list.Add(this.GetNodeDataValue(i));
				}
				if (flag || flag2)
				{
					i++;
				}
				else
				{
					i += this.GetBranchNodeWidth(i);
				}
			}
			return list;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029FC File Offset: 0x00000BFC
		public ICollection<int> ClippingPlanesQuery(ICollection<Plane3> clippingPlanes)
		{
			List<int> list = new List<int>();
			int i = 0;
			int num = this.quantizedAabbTreeNodes.Length;
			while (i < num)
			{
				bool flag = this.GetAabb(i).CollidePlanes(clippingPlanes);
				bool flag2 = this.IsDataNode(i);
				if (flag2 && flag)
				{
					list.Add(this.GetNodeDataValue(i));
				}
				if (flag || flag2)
				{
					i++;
				}
				else
				{
					i += this.GetBranchNodeWidth(i);
				}
			}
			return list;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A68 File Offset: 0x00000C68
		public double CalculateAverageNodeDepth()
		{
			double num = 0.0;
			Stack<int> stack = new Stack<int>();
			for (int i = 0; i < this.quantizedAabbTreeNodes.Length; i++)
			{
				while (stack.Count != 0 && stack.Peek() == i)
				{
					stack.Pop();
				}
				if (this.IsDataNode(i))
				{
					num += (double)stack.Count;
				}
				else
				{
					int branchNodeWidth = this.GetBranchNodeWidth(i);
					stack.Push(i + branchNodeWidth);
				}
			}
			return num / (double)this.DataNodeCount;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public void Visit(IQuantizedAabbVisitor visitor)
		{
			Stack<VisitorStackItem> stack = new Stack<VisitorStackItem>();
			for (int i = 0; i < this.quantizedAabbTreeNodes.Length; i++)
			{
				while (stack.Count != 0 && stack.Peek().BranchNodeEndIndex == i)
				{
					VisitorStackItem visitorStackItem = stack.Pop();
					visitor.VisitBranchNode(visitorStackItem.Depth, visitorStackItem.BranchNodeIndex, visitorStackItem.BranchNodeCount, visitorStackItem.DataNodeCount);
					if (stack.Count != 0)
					{
						VisitorStackItem item = stack.Pop();
						item.DataNodeCount += visitorStackItem.DataNodeCount;
						item.BranchNodeCount += visitorStackItem.BranchNodeCount + 1;
						stack.Push(item);
					}
				}
				if (stack.Count != 0 && this.IsDataNode(i - 1))
				{
					visitor.VisitRightNode(stack.Peek().BranchNodeIndex, i);
				}
				if (this.IsDataNode(i))
				{
					visitor.VisitDataNode(stack.Count, i, this.GetNodeDataValue(i));
					VisitorStackItem item2 = stack.Pop();
					item2.DataNodeCount++;
					stack.Push(item2);
				}
				else
				{
					int branchNodeWidth = this.GetBranchNodeWidth(i);
					stack.Push(new VisitorStackItem
					{
						BranchNodeIndex = i,
						Depth = stack.Count,
						BranchNodeEndIndex = i + branchNodeWidth,
						BranchNodeCount = 0,
						DataNodeCount = 0
					});
					int num = i;
					visitor.VisitLeftNode(num, num + 1);
				}
			}
			while (stack.Count != 0)
			{
				VisitorStackItem visitorStackItem2 = stack.Pop();
				visitor.VisitBranchNode(visitorStackItem2.Depth, visitorStackItem2.BranchNodeIndex, visitorStackItem2.BranchNodeCount, visitorStackItem2.DataNodeCount);
				if (stack.Count != 0)
				{
					VisitorStackItem item3 = stack.Pop();
					item3.DataNodeCount += visitorStackItem2.DataNodeCount;
					item3.BranchNodeCount += visitorStackItem2.BranchNodeCount + 1;
					stack.Push(item3);
				}
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002CA8 File Offset: 0x00000EA8
		private static int InPlaceSplitX(IList<InputBox> inputBoxes, int left, int right, double pivotValue)
		{
			int num = left;
			for (int i = left; i < right; i++)
			{
				if (inputBoxes[i].Aabb.Center.X > pivotValue)
				{
					QuantizedAxisAlignedBox3Tree.SwapInputBoxes(inputBoxes, i, num);
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002CF0 File Offset: 0x00000EF0
		private static int InPlaceSplitY(IList<InputBox> inputBoxes, int left, int right, double pivotValue)
		{
			int num = left;
			for (int i = left; i < right; i++)
			{
				if (inputBoxes[i].Aabb.Center.Y > pivotValue)
				{
					QuantizedAxisAlignedBox3Tree.SwapInputBoxes(inputBoxes, i, num);
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002D38 File Offset: 0x00000F38
		private static int InPlaceSplitZ(IList<InputBox> inputBoxes, int left, int right, double pivotValue)
		{
			int num = left;
			for (int i = left; i < right; i++)
			{
				if (inputBoxes[i].Aabb.Center.Z > pivotValue)
				{
					QuantizedAxisAlignedBox3Tree.SwapInputBoxes(inputBoxes, i, num);
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002D80 File Offset: 0x00000F80
		private static Vector3 CalculateMeanOfCenterPoints(IList<InputBox> inputBoxes, int left, int right)
		{
			InputBox inputBox = inputBoxes[left];
			Vector3 vector = inputBox.Aabb.Center;
			for (int i = left + 1; i < right; i++)
			{
				Vector3 left2 = vector;
				inputBox = inputBoxes[i];
				vector = left2 + inputBox.Aabb.Center;
			}
			int num = right - left;
			return vector / (double)num;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002DD8 File Offset: 0x00000FD8
		private static Aabb MergeBoxes(IList<InputBox> inputBoxes, int left, int right)
		{
			Aabb result = inputBoxes[left].Aabb;
			for (int i = left + 1; i < right; i++)
			{
				result = result.CreateMergedWith(inputBoxes[i].Aabb);
			}
			return result;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002E18 File Offset: 0x00001018
		private static void SwapInputBoxes(IList<InputBox> inputBoxes, int itemA, int itemB)
		{
			InputBox value = inputBoxes[itemA];
			inputBoxes[itemA] = inputBoxes[itemB];
			inputBoxes[itemB] = value;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002E44 File Offset: 0x00001044
		private static ushort RoundedQuantize(double value, double quantization)
		{
			double num = value * quantization;
			if (num < 0.5)
			{
				return 0;
			}
			if (num > 65534.5)
			{
				return ushort.MaxValue;
			}
			return (ushort)(num + 0.5);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002E81 File Offset: 0x00001081
		private bool TestQuantizedBoxOverlap(int nodeIndex, QuantizedAxisAlignedBox3 quantizedAabb)
		{
			return this.quantizedAabbTreeNodes[nodeIndex].TestQuantizedBoxOverlap(quantizedAabb);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002E95 File Offset: 0x00001095
		private bool IsDataNode(int nodeIndex)
		{
			return this.quantizedAabbTreeNodes[nodeIndex].IsDataNode();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002EA8 File Offset: 0x000010A8
		private QuantizedAxisAlignedBox3 GetQuantizedAabb(int nodeIndex)
		{
			return this.quantizedAabbTreeNodes[nodeIndex].GetQuantizedAabb();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002EBC File Offset: 0x000010BC
		private Aabb GetAabb(int nodeIndex)
		{
			QuantizedAxisAlignedBox3 quantizedAabb = this.quantizedAabbTreeNodes[nodeIndex].GetQuantizedAabb();
			return this.Unquantize(quantizedAabb);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002EE2 File Offset: 0x000010E2
		private void CreateDataNode(int nodeIndex, QuantizedAxisAlignedBox3 quantizedAabb, int dataValue)
		{
			this.quantizedAabbTreeNodes[nodeIndex].SetAsDataValue(dataValue, quantizedAabb);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002EF7 File Offset: 0x000010F7
		private void CreateBranchNode(int nodeIndex, QuantizedAxisAlignedBox3 quantizedAabb, int width)
		{
			this.quantizedAabbTreeNodes[nodeIndex].SetAsBranch(width, quantizedAabb);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002F0C File Offset: 0x0000110C
		private int GetNodeDataValue(int nodeIndex)
		{
			return this.quantizedAabbTreeNodes[nodeIndex].GetDataNodeValue();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002F1F File Offset: 0x0000111F
		private int GetBranchNodeWidth(int nodeIndex)
		{
			return this.quantizedAabbTreeNodes[nodeIndex].GetBranchNodeWidth();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002F34 File Offset: 0x00001134
		private QuantizedAxisAlignedBox3 Quantize(Aabb aabb)
		{
			Vector3 vector = aabb.Min - this.globalAabb.Min;
			Vector3 vector2 = aabb.Max - this.globalAabb.Min;
			return new QuantizedAxisAlignedBox3(QuantizedAxisAlignedBox3Tree.RoundedQuantize(vector.X, this.bvhQuantization.X), QuantizedAxisAlignedBox3Tree.RoundedQuantize(vector2.X, this.bvhQuantization.X), QuantizedAxisAlignedBox3Tree.RoundedQuantize(vector.Y, this.bvhQuantization.Y), QuantizedAxisAlignedBox3Tree.RoundedQuantize(vector2.Y, this.bvhQuantization.Y), QuantizedAxisAlignedBox3Tree.RoundedQuantize(vector.Z, this.bvhQuantization.Z), QuantizedAxisAlignedBox3Tree.RoundedQuantize(vector2.Z, this.bvhQuantization.Z));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003000 File Offset: 0x00001200
		private Aabb Unquantize(QuantizedAxisAlignedBox3 value)
		{
			Vector3 right = new Vector3((double)value.MinX / this.bvhQuantization.X + this.globalAabb.Min.X, (double)value.MinY / this.bvhQuantization.Y + this.globalAabb.Min.Y, (double)value.MinZ / this.bvhQuantization.Z + this.globalAabb.Min.Z);
			Vector3 left = new Vector3((double)value.MaxX / this.bvhQuantization.X + this.globalAabb.Min.X, (double)value.MaxY / this.bvhQuantization.Y + this.globalAabb.Min.Y, (double)value.MaxZ / this.bvhQuantization.Z + this.globalAabb.Min.Z);
			Vector3 center = (left + right) / 2.0;
			Vector3 extent = (left - right) / 2.0;
			return new Aabb(center, extent);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000313C File Offset: 0x0000133C
		private int BuildSubTree(int outputIndex, IList<InputBox> inputBoxes, int left, int right, out QuantizedAxisAlignedBox3 subTreeBoundingBox)
		{
			if (right - left == 1)
			{
				QuantizedAxisAlignedBox3 quantizedAxisAlignedBox = this.Quantize(inputBoxes[left].Aabb);
				this.CreateDataNode(outputIndex, quantizedAxisAlignedBox, inputBoxes[left].Data);
				subTreeBoundingBox = quantizedAxisAlignedBox;
				return 1;
			}
			if (right - left == 2)
			{
				QuantizedAxisAlignedBox3 quantizedAabb = this.Quantize(inputBoxes[left].Aabb);
				this.CreateDataNode(outputIndex + 1, quantizedAabb, inputBoxes[left].Data);
				QuantizedAxisAlignedBox3 quantizedAxisAlignedBox2 = this.Quantize(inputBoxes[left + 1].Aabb);
				this.CreateDataNode(outputIndex + 2, quantizedAxisAlignedBox2, inputBoxes[left + 1].Data);
				subTreeBoundingBox = quantizedAabb.CreateMergedWith(quantizedAxisAlignedBox2);
				this.CreateBranchNode(outputIndex, subTreeBoundingBox, 3);
				return 3;
			}
			int num = this.SplitByOneAxis(inputBoxes, left, right);
			QuantizedAxisAlignedBox3 quantizedAxisAlignedBox3;
			int num2 = this.BuildSubTree(outputIndex + 1, inputBoxes, left, num, out quantizedAxisAlignedBox3);
			QuantizedAxisAlignedBox3 other;
			int num3 = this.BuildSubTree(outputIndex + num2 + 1, inputBoxes, num, right, out other);
			subTreeBoundingBox = quantizedAxisAlignedBox3.CreateMergedWith(other);
			int num4 = num2 + num3 + 1;
			this.CreateBranchNode(outputIndex, subTreeBoundingBox, num4);
			return num4;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000325C File Offset: 0x0000145C
		private int SplitByOneAxis(IList<InputBox> inputBoxes, int left, int right)
		{
			Vector3 right2 = QuantizedAxisAlignedBox3Tree.CalculateMeanOfCenterPoints(inputBoxes, left, right);
			Vector3 left2 = new Vector3(0.0, 0.0, 0.0);
			for (int i = left; i < right; i++)
			{
				Vector3 vector = inputBoxes[i].Aabb.Center - right2;
				Vector3 right3 = vector * vector;
				left2 += right3;
			}
			int num;
			if (left2.X > left2.Y)
			{
				if (left2.X > left2.Z)
				{
					num = QuantizedAxisAlignedBox3Tree.InPlaceSplitX(inputBoxes, left, right, right2.X);
				}
				else
				{
					num = QuantizedAxisAlignedBox3Tree.InPlaceSplitZ(inputBoxes, left, right, right2.Z);
				}
			}
			else if (left2.Y > left2.Z)
			{
				num = QuantizedAxisAlignedBox3Tree.InPlaceSplitY(inputBoxes, left, right, right2.Y);
			}
			else
			{
				num = QuantizedAxisAlignedBox3Tree.InPlaceSplitZ(inputBoxes, left, right, right2.Z);
			}
			int num2 = (right - left) / 3;
			if (num <= left + num2)
			{
				num = (left + right) / 2;
			}
			else if (num >= right - 1 - num2)
			{
				num = (left + right) / 2;
			}
			return num;
		}

		// Token: 0x04000016 RID: 22
		private readonly QuantizedAxisAlignedBox3TreeNode[] quantizedAabbTreeNodes;

		// Token: 0x04000017 RID: 23
		private Aabb globalAabb;

		// Token: 0x04000018 RID: 24
		private Vector3 bvhQuantization;
	}
}
