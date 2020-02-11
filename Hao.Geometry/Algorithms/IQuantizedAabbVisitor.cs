using System;

namespace Hao.Geometry.Algorithms
{
	// Token: 0x02000002 RID: 2
	public interface IQuantizedAabbVisitor
	{
		// Token: 0x06000001 RID: 1
		void VisitBranchNode(int level, int branchNodeId, int branchNodeCount, int dataNodeCount);

		// Token: 0x06000002 RID: 2
		void VisitDataNode(int level, int dataNodeId, int value);

		// Token: 0x06000003 RID: 3
		void VisitLeftNode(int branchNodeId, int leftNodeId);

		// Token: 0x06000004 RID: 4
		void VisitRightNode(int branchNodeId, int rightNodeId);
	}
}
