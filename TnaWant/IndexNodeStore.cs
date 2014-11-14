using System.IO;
using Practice.Algorithm.Tree.BPlusTree;
using Practice.FileSystem;

namespace TnaWant
{
    public class IndexNodeStore : INodeStore
    {
        private FileEngine _dataEngine;

        public IndexNodeStore(FileEngine fileEngine)
        {
            _dataEngine = fileEngine;
        }

        public long ObtainNodeId()
        {
            byte[] data = new byte[4];
            using (MemoryStream ms = new MemoryStream(data))
            {
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    writer.Write("{.}");
                }
            }

            long address = _dataEngine.Save(data);
            //_dataEngine.Commit();
            return address;
        }

        public void Release(long nodeId)
        {
            _dataEngine.Delete(nodeId);
            //_dataEngine.Commit();
        }

        public bool Exists(long nodeId)
        {
            byte[] data = _dataEngine.Read((int)nodeId);
            return data.Length != 1 && data.Length!=0 && data[0] != 145;
        }

        public void Write(long nodeId, byte[] buffer)
        {
            _dataEngine.Update(nodeId, buffer);

            //_dataEngine.Commit();
        }

        public byte[] Read(long nodeId)
        {
            byte[] data = _dataEngine.Read(nodeId);
            string str;
            using (MemoryStream ms = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(ms))
                {
                    str = reader.ReadString();
                }
            }

            if (str == "{.}" || str == "{null}")
            {
                return new byte[]{};
            }

            return data;
        }

        public void Commit()
        {
            _dataEngine.Commit();
        }

        public void Close()
        {
            if (_dataEngine != null)
            {
                _dataEngine.Dispose();
            }
        }
    }
}
