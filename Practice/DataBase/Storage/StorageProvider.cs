using System.IO;
using Practice.Algorithm.Tree.BPlusTree;
using Practice.DataBase.UserInterface;

namespace Practice.DataBase.Storage
{
    public class StorageProvider
    {
        private const int BTreeBranchMinCount = 50;

        private BPTree _dataTree;
        private FileHeader _sysHeader;
        private StreamHelper _sysFile;
        private Space _space;
        private Node _node;
        private NodeStore _nodeStore;
        private string[] _path;

        public StorageProvider(Stream dataStream, Stream systemStream)
        {
            StreamHelper dataFile = new StreamHelper(dataStream);
            _sysFile = new StreamHelper(systemStream);
            _sysHeader = new FileHeader();
            _space = new Space(GetSpaceMinBlockSize());
            _node = new Node();
            
            _sysHeader.Deserialize(_sysFile.Starter);

            if (!_sysHeader.IsConfigured)
            {
                Initailize();
            }
            else
            {
                LoadSystem();
            }

            CreateDataTree(dataFile);
        }

        public BPTree DataTree
        {
            get
            {
                return _dataTree;
            }
        }

        public long Size
        {
            get;
            private set;
        }

        public void Save(long key, string value)
        {
            long nodeStoreVer = _nodeStore.Version;
            //Console.Out.WriteLine("1," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
            _dataTree.Add(key.ToString(), value);

            if (nodeStoreVer != _nodeStore.Version)
            {
                RefreshSystem();
            }
        }

        public string Read(long key)
        {
            return _dataTree.Find(key.ToString());
        }

        public string[] Read(long keyFrom, long keyTo)
        {
            return _dataTree.Find(keyFrom.ToString(), keyTo.ToString());
        }

        public bool Delete(long key)
        {
            long nodeStoreVer = _nodeStore.Version;
            bool bln = _dataTree.Delete(key.ToString());

            if (nodeStoreVer != _nodeStore.Version)
            {
                RefreshSystem();
            }

            return bln;
        }

        public bool Update(long key, string value)
        {
            long nodeStoreVer = _nodeStore.Version;
            if (!_dataTree.Delete(key.ToString()))
            {
                return false;
            }

            bool bln = _dataTree.Add(key.ToString(), value);

            if (nodeStoreVer != _nodeStore.Version)
            {
                RefreshSystem();
            }

            return bln;
        }

        public void SetPath(params string[] path)
        {
            _path = path;
        }

        public void Commit()
        {
            bool commitChanges = _dataTree.Commit();
            if (commitChanges)
            {
                RefreshSystem();
            }  
        }

        private void Initailize()
        {
            _sysFile.Starter = _sysHeader.Serialize();
            _space.AddSpace(new Pointer(0, int.MaxValue));
            _node.ObtainNodeId();
        }
        
        private void RefreshSystem()
        {
            byte[] space = _space.Serialize();
            byte[] node = _node.Serialize();

            using (ByteArraySerializer serializer = new ByteArraySerializer())
            {
                serializer.Write(space.Length);
                serializer.Write(space);
                serializer.Write(node.Length);
                serializer.Write(node);

                byte[] buffer = serializer.SerializedBuffer;
                _sysHeader.DataTreeRootNode = string.Format("{0},{1}", _dataTree.Root.NodeId, (int)_dataTree.Root.NodeType);
                _sysHeader.SystemArea = new Pointer(0, buffer.Length);
                _sysFile.Starter = _sysHeader.Serialize();
                _sysFile.Store(_sysHeader.SystemArea, buffer);
                Size = _sysHeader.SystemArea.PositionPlusSize + FileHeader.HeaderSize;
            }
        }

        private void LoadSystem()
        {
            Pointer point = _sysHeader.SystemArea;
            byte[] buffer = _sysFile.Load(point);
            using (ByteArraySerializer serializer = new ByteArraySerializer())
            {
                serializer.SetDeserializingBuffer(buffer);
                int space = serializer.ReadInt();
                _space.Deserialize(serializer.ReadBytes(space));
                int node = serializer.ReadInt();
                _node.Deserialize(serializer.ReadBytes(node));
            }
        }
        
        private void CreateDataTree(StreamHelper dataFile)
        {
            _nodeStore = new NodeStore(dataFile, _space, _node);
            string[] rootNodeInfo = _sysHeader.DataTreeRootNode.Split(',');
            int rootNodeId = int.Parse(rootNodeInfo[0]);
            BPTreeNodeType nodeType = (BPTreeNodeType)int.Parse(rootNodeInfo[1]);
            _dataTree = new BPTree(BTreeBranchMinCount, _nodeStore);
        }
        
        private int GetSpaceMinBlockSize()
        {
            return BTreeBranchMinCount * RecordSetPathChecker.PathMaxDeepth / 2 * RecordSetPathChecker.SingleLevelPathMaxlength;
        }
    }
}
