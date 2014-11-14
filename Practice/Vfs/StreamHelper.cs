using System;
using System.IO;

namespace Practice.Vfs
{
    public class StreamHelper:DisposableObject
    {
        private readonly Stream _dataStream;
        private readonly BinaryReader _reader;
        private readonly BinaryWriter _writer;

        public StreamHelper(Stream stream)
        {
            _dataStream = stream;
            _reader = new BinaryReader(_dataStream);
            _writer = new BinaryWriter(_dataStream);
        }

        public int ReadInt(DataPoint point)
        {
            Locate(point.Position);
            var value = _reader.ReadInt32();
            return value;
        }

        public byte[] ReadBytes(DataPoint point)
        {
            Locate(point.Position);
            var value = _reader.ReadBytes(point.Size);
            return value;
        }

        public byte ReadByte(DataPoint point)
        {
            Locate(point.Position);
            var value = _reader.ReadByte();
            return value;
        }

        public void SaveInt(DataPoint point, int value)
        {
            Locate(point.Position);
            _writer.Write(value);
        }

        public void SaveBytes(DataPoint point, byte[] value)
        {
            Locate(point.Position);

            byte[] data = new byte[point.Size];
            value.CopyTo(data, 0);
            _writer.Write(data);
        }

        public void Commit()
        {
            _writer.Flush();
            _dataStream.Flush();
        }

        private void Locate(long position)
        {
            _dataStream.Seek(position, SeekOrigin.Begin);
        }

        protected override void DoDispose()
        {
            Commit();
            if (_writer != null)
            {
                _writer.Dispose();
            }
            if (_reader != null)
            {
                _reader.Dispose();
            }
            if (_dataStream != null)
            {
                _dataStream.Dispose();
            }
        }
    }
}
