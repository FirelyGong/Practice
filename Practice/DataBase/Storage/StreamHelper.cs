using System.IO;

namespace Practice.DataBase.Storage
{
    public class StreamHelper
    {
        private Stream _dataStream;
        private BinaryReader _reader;
        private BinaryWriter _writer;
        private FileHeader _setting;
        private FileHeader _fileHeader;

        public StreamHelper(Stream dataStream)
        {
            _dataStream = dataStream;

            _reader = new BinaryReader(_dataStream);
            _writer = new BinaryWriter(_dataStream);
        }

        //public FileHeader Header
        //{
        //    get
        //    {
        //        if (_fileHeader == null)
        //        {
        //            _fileHeader = new FileHeader();
        //        }

        //        _fileHeader.Deserialize(Starter);
        //        return _fileHeader;
        //    }

        //    set
        //    {
        //        Starter = value.Serialize();
        //    }
        //}

        public byte[] Starter
        {
            get
            {
                if (_dataStream.Length == 0)
                {
                    return new byte[] { };
                }

                _dataStream.Position = 0;
                byte[] starter = _reader.ReadBytes(FileHeader.HeaderSize);
                return starter;
            }

            set
            {
                _dataStream.Position = 0;
                _writer.Write(value);
                //Commit();
            }
        }

        public void Store(Pointer point, byte[] buffer)
        {
            if (_setting == null)
            {
                _setting = new FileHeader();
                _setting.Deserialize(Starter);
            }

            Locate(point.Position);
            _writer.Write(buffer);
            //Commit();
        }

        public byte[] Load(Pointer point)
        {
            Locate(point.Position);
            return _reader.ReadBytes(point.Size);
        }
        
        public void Commit()
        {
            _writer.Flush();
            _dataStream.Flush();
        }

        public void Dispose()
        {
            Commit();
            //_dataStream.Dispose();
            //_reader.Dispose();
            //_writer.Dispose();
        }

        private void Locate(long position)
        {
            _dataStream.Seek(position + FileHeader.HeaderSize, SeekOrigin.Begin);
        }
    }
}
