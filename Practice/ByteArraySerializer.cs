using System;
using System.IO;
namespace Practice
{
    public class ByteArraySerializer:IDisposable
    {
        private MemoryStream _readStream;
        private MemoryStream _writeStream;
        private BinaryReader _binaryReader;
        private BinaryWriter _binaryWriter;

        public byte[] SerializedBuffer
        {
            get
            {
                if (_writeStream != null)
                {
                    return _writeStream.ToArray();
                }

                return new byte[] { };
            }
        }

        public void SetDeserializingBuffer(byte[] buffer)
        {
            _readStream=new MemoryStream(buffer);
            _binaryReader = new BinaryReader(_readStream);
        }

        public ByteArraySerializer()
        {
            _writeStream = new MemoryStream();
            _binaryWriter = new BinaryWriter(_writeStream);
        }

        public void Write(long value)
        {
            _binaryWriter.Write(value);
        }

        public void Write(string value)
        {
            _binaryWriter.Write(value);
        }

        public void Write(int value)
        {
            _binaryWriter.Write(value);
        }

        public void Write(short value)
        {
            _binaryWriter.Write(value);
        }

        public void Write(byte value)
        {
            _binaryWriter.Write(value);
        }

        public void Write(bool value)
        {
            _binaryWriter.Write(value);
        }

        public void Write(byte[] value)
        {
            _binaryWriter.Write(value);
        }

        public long ReadLong()
        {
            return _binaryReader.ReadInt64();
        }

        public int ReadInt()
        {
            return _binaryReader.ReadInt32();
        }

        public short ReadShort()
        {
            return _binaryReader.ReadInt16();
        }

        public byte ReadByte()
        {
            return _binaryReader.ReadByte();
        }

        public string ReadString()
        {
            return _binaryReader.ReadString();
        }

        public bool ReadBool()
        {
            return _binaryReader.ReadBoolean();
        }

        public byte[] ReadBytes(int length)
        {
            return _binaryReader.ReadBytes(length);
        }

        public void Dispose()
        {
            if (_binaryWriter != null)
            {
                _binaryWriter.Dispose();
            }

            if (_binaryReader != null)
            {
                _binaryReader.Dispose();
            }

            if (_writeStream != null)
            {
                _writeStream.Dispose();
            }

            if (_readStream != null)
            {
                _readStream.Dispose();
            }
        }
    }
}
