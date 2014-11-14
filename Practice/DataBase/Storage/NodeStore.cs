using Practice.Algorithm.Tree.BPlusTree;

namespace Practice.DataBase.Storage
{
    public class NodeStore : INodePersistence
    {
        private StreamHelper _fileHelper;
        private Space _space;
        private Node _node;

        public NodeStore(StreamHelper fileHelper, Space space, Node node)
        {
            _fileHelper = fileHelper;
            _space = space;
            _node = node;
        }

        public long Version { get; private set; }
        
        public int ObtainNodeId()
        {
            return _node.ObtainNodeId();
        }

        public void Release(int nodeId)
        {
            Pointer pointer = _node.TryRemoveNodePointer(nodeId);
            if (pointer!=null)
            {
                _space.Free(pointer);
            }

            Version++;
        }

        public bool Exists(int nodeId)
        {
            return _node[nodeId] != null;
        }

        public void Write(int nodeId, byte[] buffer)
        {
            Pointer existing = _node[nodeId];
            if (existing!=null)
            {
                //Console.Out.Write("node [" + nodeId + "] free [" + existing + "]...");
                _space.Free(existing);
                //Console.Out.WriteLine(", "+_spaceManager.ToString());
            }

            int allocSize = buffer.Length;
            //if (allocSize < 8000)
            //{
            //    allocSize = 8000;
            //}

            Pointer pointer = _space.Allocate(allocSize);

            //Console.Out.WriteLine("node [" + nodeId + "] allocate [" + pointer + "], " + _spaceManager.ToString());
            _fileHelper.Store(pointer, buffer);

            _node[nodeId]=pointer;
            
            Version++;
        }

        public byte[] Read(int nodeId)
        {
            Pointer pointer = _node[nodeId];
            if (pointer!=null)
            {
                return _fileHelper.Load(pointer);
            }

            return new byte[] { };
        }

        public void Commit()
        {
            _fileHelper.Commit();
        }

        public void Close()
        {
            _fileHelper.Commit();
            _fileHelper.Dispose();
        }


        public void SaveRootInfo(string nodeId)
        {
            throw new System.NotImplementedException();
        }

        public string ReadRootInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
