using Practice.Algorithm.Tree.BPlusTree;
namespace Practice.DataBase.Storage
{
    public class FileHeader
    {
        public const int HeaderSize = 50;
        public const string SystemVersion = "0.1.1";

        private string _version;

        public FileHeader():this(SystemVersion)
        {
        }

        public FileHeader(string version)
        {
            IsConfigured = false;
            SystemArea = new Pointer(0, 0);
            DataTreeRootNode = string.Format("{0},{1}", 0, (int)BPTreeNodeType.Leaf);
            _version = version;
        }
        
        public string Version
        {
            get
            {
                return _version;
            }

            set
            {
                _version = value;
                IsConfigured = true;
            }
        }
        
        public Pointer SystemArea { get; set; }

        public string DataTreeRootNode { get; set; }

        public bool IsConfigured { get; private set; }

        public byte[] Serialize()
        {
            byte[] buffer;
            using (ByteArraySerializer serializer = new ByteArraySerializer())
            {
                serializer.Write(Version);
                serializer.Write(SystemArea.Position);
                serializer.Write(SystemArea.Size);
                serializer.Write(DataTreeRootNode);

                buffer = serializer.SerializedBuffer;
            }

            return buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            if (buffer.Length == 0)
            {
                return;
            }

            using (ByteArraySerializer serializer = new ByteArraySerializer())
            {
                serializer.SetDeserializingBuffer(buffer);
                string str = serializer.ReadString();
                if (string.IsNullOrEmpty(str))
                {
                    return;
                }

                Version = str;
                SystemArea = new Pointer(serializer.ReadLong(), serializer.ReadInt());
                DataTreeRootNode = serializer.ReadString();
            }
        }
    }
}
