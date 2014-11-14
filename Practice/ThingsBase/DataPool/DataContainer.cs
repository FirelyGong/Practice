using Practice.Algorithm.Tree.BPlusTree;
using Practice.Vfs;

namespace Practice.ThingsBase.DataPool
{
    public class DataContainer:DisposableObject
    {
        private const int DataBlockSize = 512;

        private const int TreeNodeSize = 50;

        private IStorage _storage;

        private BPTree _indexTree;

        public DataContainer(string dataFile)
        {
            _storage = new FileStorage(dataFile, DataBlockSize);
            var persistence = new NodePersistence(_storage);

            _indexTree = new BPTree(TreeNodeSize, persistence);
        }

        public bool Save(string key, string data)
        {
            return _indexTree.Add(key, data);
        }

        public string Get(string key)
        {
            return _indexTree.Find(key);
        }

        public string Delete(string key)
        {
            string value;
            bool bln = _indexTree.Delete(key, out value);
            if (bln)
            {
                return value;
            }

            return string.Empty;
        }

        protected override void DoDispose()
        {
            ((FileStorage)_storage).Dispose();
        }
    }
}
