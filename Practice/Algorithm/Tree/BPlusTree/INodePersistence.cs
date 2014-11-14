namespace Practice.Algorithm.Tree.BPlusTree
{
    public interface INodePersistence
    {
        /// <summary>
        /// Obtained handle is always unique
        /// </summary>
        int ObtainNodeId();
        void Release(int nodeId);
        void Write(int nodeId, byte[] buffer);
        byte[] Read(int nodeId);

        void SaveRootInfo(string root);

        string ReadRootInfo();
    }
}
