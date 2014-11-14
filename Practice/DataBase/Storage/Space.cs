using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Practice.DataBase.Storage
{
    public class Space
    {
        private List<Pointer> _freeSpaces;
        private int _minBlockSize;

        public Space():this(0)
        {
        }

        public Space(int minBlockSize)
        {
            _minBlockSize = minBlockSize;
            _freeSpaces = new List<Pointer>();
        }

        public int AvailSize { get; private set; }

        public long LastPosition
        {
            get
            {
                return _freeSpaces.Last().Position;
            }
        }

        public void AddSpace(Pointer space)
        {
            var check = _freeSpaces.Any(sp => sp == space);
            if (check)
            {
                throw new Exception("The space [" + space + "] already exists!");
            }

            _freeSpaces.Add(space);
            AvailSize += space.Size;
        }

        public Pointer Allocate(int size)
        {
            if (AvailSize < size)
            {
                throw new Exception("There isn't enough space!");
            }

            if (size < _minBlockSize)
            {
                size = _minBlockSize;
            }

            int index = FindSpace(size);
            if (index < 0)
            {
                throw new Exception("Can't find sapce for size [" + size + "]!");
            }

            Pointer pointer = _freeSpaces[index];
            long position = pointer.Position;
            pointer.Position += size;
            pointer.Size -= size;

            AvailSize -= size;

            if (pointer.Size <= 0)
            {
                _freeSpaces.RemoveAt(index);
            }

            return new Pointer()
            {
                Position = position,
                Size = size
            };
        }

        public void Free(Pointer pointer)
        {
            int index = _freeSpaces.BinarySearch(pointer);
            if (index >= 0)
            {
                throw new ArgumentException("The space is already freed.");
            }

            index = ~index;
            bool canMerge = false;

            if (index < _freeSpaces.Count)
            {
                Pointer right = _freeSpaces[index];
                if (pointer.PositionPlusSize > right.Position)
                {
                    throw new Exception("Can't free right overlapped space!");
                }

                if (pointer.PositionPlusSize == right.Position)
                {
                    right.Position -= pointer.Size;
                    right.Size += pointer.Size;

                    canMerge = true;
                }
            }

            if (index > 0)
            {
                Pointer left = _freeSpaces[index - 1];
                if (left.PositionPlusSize > pointer.Position)
                {
                    throw new Exception("Can't free left overlapped space!");
                }

                if (left.PositionPlusSize == pointer.Position)
                {
                    if (canMerge)
                    {
                        left.Size += _freeSpaces[index].Size;
                        _freeSpaces.RemoveAt(index);
                    }
                    else
                    {
                        left.Size += pointer.Size;
                        canMerge = true;
                    }

                }
            }

            if (!canMerge)
            {
                _freeSpaces.Insert(index, pointer);
            }

            AvailSize += pointer.Size;
        }

        public byte[] Serialize()
        {
            byte[] buffer;
            using (ByteArraySerializer serializer = new ByteArraySerializer())
            {
                serializer.Write(AvailSize);
                serializer.Write(_freeSpaces.Count);
                _freeSpaces.ForEach(
                    s =>
                    {
                        serializer.Write(s.Position);
                        serializer.Write(s.Size);
                    });

                buffer = serializer.SerializedBuffer;
            }

            return buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            using (ByteArraySerializer serializer = new ByteArraySerializer())
            {
                serializer.SetDeserializingBuffer(buffer);
                AvailSize = serializer.ReadInt();
                int count = serializer.ReadInt();
                
                _freeSpaces.Clear();
                for (int i = 0; i < count; i++)
                {
                    _freeSpaces.Add(new Pointer(serializer.ReadLong(), serializer.ReadInt()));
                }
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("Free Spaces:[");
            int loop = 0;
            _freeSpaces.ForEach(
                s =>
                {
                    stringBuilder.Append(s);
                    loop++;
                    if (loop < _freeSpaces.Count)
                    {
                        stringBuilder.Append(",");
                    }
                });

            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

        private int FindSpace(int size)
        {
            for (int i = 0; i < _freeSpaces.Count; i++)
            {
                if (_freeSpaces[i].Size >= size)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
