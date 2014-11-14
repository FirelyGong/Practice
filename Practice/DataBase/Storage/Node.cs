using System.Collections.Generic;

namespace Practice.DataBase.Storage
{
    public class Node
    {
        private Dictionary<long, Pointer> _nodes;

        public Node()
        {
            _nodes = new Dictionary<long, Pointer>();
        }

        public int NextNodeId { get; private set; }

        public int ObtainNodeId()
        {
            return NextNodeId++;
        }

        public Pointer TryGetNodePointer(int nodeId)
        {
            Pointer pointer;
            if (_nodes.TryGetValue(nodeId, out pointer))
            {
                return pointer;
            }

            return null;
        }

        public Pointer TryRemoveNodePointer(int nodeId)
        {
            Pointer point = this[nodeId];
            if (point != null)
            {
                _nodes.Remove(nodeId);
                return point;
            }

            return null;
        }

        public Pointer this[int key]
        {
            get
            {
                return TryGetNodePointer(key);
            }

            set
            {
                _nodes[key] = value;
            }
        }

        public byte[] Serialize()
        {
            byte[] result;
            using (ByteArraySerializer serializer = new ByteArraySerializer())
            {
                serializer.Write(NextNodeId);
                serializer.Write(_nodes.Count);

                foreach (var kv in _nodes)
                {
                    serializer.Write(kv.Key);
                    serializer.Write(kv.Value.Position);
                    serializer.Write(kv.Value.Size);
                }

                result = serializer.SerializedBuffer;
            }

            return result;
        }

        public void Deserialize(byte[] buffer)
        {
            using (ByteArraySerializer serializer = new ByteArraySerializer())
            {
                serializer.SetDeserializingBuffer(buffer);

                NextNodeId = serializer.ReadInt();
                int count = serializer.ReadInt();
                _nodes.Clear();

                for (int i = 0; i < count; i++)
                {
                    _nodes.Add(serializer.ReadLong(), new Pointer(serializer.ReadLong(), serializer.ReadInt()));
                }
            }
        }
    }
}
