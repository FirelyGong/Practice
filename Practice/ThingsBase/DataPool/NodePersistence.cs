using System;
using System.Globalization;

using Practice.Algorithm.Tree.BPlusTree;
using Practice.Vfs;

namespace Practice.ThingsBase.DataPool
{
    public class NodePersistence:INodePersistence
    {
        private readonly IStorage _storage;

        public NodePersistence(IStorage storage)
        {
            _storage = storage;
        }

        public int ObtainNodeId()
        {
            const string defaultString = "Reserving data block";
            int id = _storage.Store(defaultString);
            return id;
        }

        public void Release(int nodeId)
        {
            _storage.Delete(nodeId);
        }

        public void Write(int nodeId, byte[] buffer)
        {
            _storage.Update(nodeId, buffer);
        }

        public byte[] Read(int nodeId)
        {
            return _storage.Read(nodeId, true);
        }
        
        public void SaveRootInfo(string root)
        {
            _storage.AppendCustomBootInfo(root);
        }

        public string ReadRootInfo()
        {
            string info = _storage.ReadCustomBootInfo();

            return info;
        }
    }
}
