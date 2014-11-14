using System.IO;

namespace Practice.Vfs
{
    public class BytesSeriesWriter : DisposableObject
    {
        private readonly MemoryStream _stream;

        private readonly BinaryWriter _writer;

        public BytesSeriesWriter()
        {
            _stream = new MemoryStream();
            _writer = new BinaryWriter(_stream);
        }

        public void WriteInt(int value)
        {
            _writer.Write(value);
        }

        public void WriteShort(short value)
        {
            _writer.Write(value);
        }

        public void WriteString(string value)
        {
            _writer.Write(value);
        }

        public void WriteBytes(byte[] buffer)
        {
            _writer.Write(buffer);
        }

        public byte[] Output
        {
            get
            {
                return _stream.ToArray();
            }
        }

        protected override void DoDispose()
        {
            if (_writer != null)
            {
                _writer.Dispose();
            }

            if (_stream != null)
            {
                _stream.Dispose();
            }
        }
    }
}
