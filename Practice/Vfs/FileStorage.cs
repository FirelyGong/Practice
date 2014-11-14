using System;
using System.IO;

namespace Practice.Vfs
{
    public class FileStorage:DisposableObject,IStorage
    {
        public const string FileType = ".stg";

        private StreamStorage _internalStorage;

        public FileStorage(string dataFile)
        {
            Start(dataFile, 0);
        }

        public FileStorage(string dataFile, int blockSize)
        {
            Start(dataFile, blockSize);
        }

        public int Store(byte[] data)
        {
            return _internalStorage.Store(data);
        }
        
        public int Store(string data)
        {
            return _internalStorage.Store(data);
        }

        public bool Update(int address, byte[] data)
        {
            return _internalStorage.Update(address, data);
        }

        public bool Update(int address, string data)
        {
            return _internalStorage.Update(address, data);
        }

        public string Delete(int address)
        {
            return _internalStorage.Delete(address);
        }

        public byte[] Delete(int address, bool returnBytes)
        {
            return _internalStorage.Delete(address, returnBytes);
        }

        public string Read(int address)
        {
            return _internalStorage.Read(address);
        }

        public byte[] Read(int address, bool returnBytes)
        {
            return _internalStorage.Read(address, returnBytes);
        }

        public void AppendCustomBootInfo(string info)
        {
            _internalStorage.AppendCustomBootInfo(info);
        }

        public string RemoveCustomBootInfo()
        {
            return _internalStorage.RemoveCustomBootInfo();
        }

        public string ReadCustomBootInfo()
        {
            return _internalStorage.ReadCustomBootInfo();
        }
        
        protected override void DoDispose()
        {
            ((IDisposable)_internalStorage).Dispose();
        }

        private void Start(string dataFile, int blockSize)
        {
            if (!File.Exists(dataFile))
            {
                throw new FileNotFoundException("The data file doesn't exist.", dataFile);
            }

            string ext = Path.GetExtension(dataFile);
            if (ext != FileType)
            {
                throw new FileLoadException(
                    "The data file isn't recognized, the expected file is *." + FileType,
                    dataFile);
            }

            var stream = new FileStream(dataFile, FileMode.Open, FileAccess.ReadWrite);
            _internalStorage = new StreamStorage(stream);
            _internalStorage.Initialize(blockSize);
        }
    }
}
