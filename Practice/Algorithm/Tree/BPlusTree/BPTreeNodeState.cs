namespace Practice.Algorithm.Tree.BPlusTree
{
    public enum BPTreeNodeState : byte
    {
        Underflow = 0,

        Normal = 1,

        Full = 2,

        Overflow = 3,
    }
}
