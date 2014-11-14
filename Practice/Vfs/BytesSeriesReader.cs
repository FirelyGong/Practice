using System.IO;

namespace Practice.Vfs
{
    public class BytesSeriesReader:DisposableObject
    {
        private readonly MemoryStream _stream;

        private readonly BinaryReader _reader;

        public BytesSeriesReader(byte[] buffer)
        {
            _stream = new MemoryStream(buffer);
            _reader = new BinaryReader(_stream);
        }

        public int ReadByte()
        {
            return _reader.ReadByte();
        }

        public int ReadInt()
        {
            return _reader.ReadInt32();
        }

        public long ReadLong()
        {
            return _reader.ReadInt64();
        }

        public short ReadShort()
        {
            return _reader.ReadInt16();
        }

        public string ReadString()
        {
            if (_reader.BaseStream.Position < _reader.BaseStream.Length)
            {
                return _reader.ReadString();
            }

            return string.Empty;
        }

        public byte[] ReadBytes(int count)
        {
            return _reader.ReadBytes(count);
        }
        
        protected override void DoDispose()
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }

            if (_stream != null)
            {
                _stream.Dispose();
            }
        }
    }
}
