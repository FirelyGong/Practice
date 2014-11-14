using System;
using System.Collections.Generic;
using System.IO;
using Practice.FileSystem;
using TnaWant.DataBase;

namespace TnaWant
{
    public class TnaWantDB //: IDisposable
    {
        //private const int FileBlockSize = 500;
        //private const long SystemRecord = 0;

        //private FileEngine _fileEngine;
        //private Dictionary<string, long> _recordSets;
        //private IndexNodeStore _indexNodeStore;

        //public TnaWantDB(string dbFile)
        //{
        //    _recordSets = new Dictionary<string, long>();
        //    _fileEngine = new FileEngine(dbFile);
        //    if (!_fileEngine.Initialized)
        //    {
        //        _fileEngine.Configure(FileBlockSize, true);
        //    }

        //    _indexNodeStore = new IndexNodeStore(_fileEngine);

        //    byte[] systemRecord = _fileEngine.Read(SystemRecord);
        //    if (systemRecord.Length == 0)
        //    {
        //        CreateSystemRecord();
        //    }
        //    else
        //    {
        //        _recordSets.Clear();
                
        //        ReadSystemRecord(systemRecord);
        //    }
        //}

        //public RecordSet<T> OpenRecordSet<T>()
        //{
        //    long indexAddress;
        //    string recordSetName = typeof(T).Name;
        //    if (_recordSets.TryGetValue(recordSetName, out indexAddress))
        //    {
        //        return new RecordSet<T>(indexAddress, _indexNodeStore, _fileEngine);
        //    }

        //    byte[] buffer = new byte[4];
        //    using (MemoryStream ms = new MemoryStream(buffer))
        //    {
        //        using (BinaryWriter writer = new BinaryWriter(ms))
        //        {
        //            writer.Write("{.}");
        //        }
        //    }

        //    indexAddress = _fileEngine.Save(buffer);

        //    _recordSets.Add(recordSetName, indexAddress);

        //    return new RecordSet<T>(indexAddress, _indexNodeStore, _fileEngine);
        //}

        //public void Commit()
        //{
        //    UpdateSystemRecord();

        //    _fileEngine.Commit();
        //}

        //public void Dispose()
        //{
        //    Commit();

        //    if (_fileEngine != null)
        //    {
        //        _fileEngine.Dispose();
        //    }
        //}

        //private void UpdateSystemRecord()
        //{
        //    byte[] buffer;
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        using (BinaryWriter writer = new BinaryWriter(ms))
        //        {
        //            List<string> lst=new List<string>(_recordSets.Keys);
        //            writer.Write(lst.Count);
        //            lst.ForEach(
        //                l =>
        //                {
        //                    writer.Write(l);
        //                    writer.Write(_recordSets[l]);
        //                });

        //            writer.Write((long)0);
        //        }

        //        buffer = ms.ToArray();
        //    }

        //    _fileEngine.Update(SystemRecord, buffer);
        //}

        //private void CreateSystemRecord()
        //{
        //    byte[] buffer = new byte[sizeof(int) + sizeof(long)];
        //    using (MemoryStream ms = new MemoryStream(buffer))
        //    {
        //        using (BinaryWriter writer = new BinaryWriter(ms))
        //        {
        //            writer.Write(0);
        //            writer.Write((long)0);
        //        }
        //    }

        //    long address = _fileEngine.Save(buffer);
        //    if (address != SystemRecord)
        //    {
        //        throw new Exception("Fail to create system record! the db is not empty!");
        //    }
        //}
        
        //private void ReadSystemRecord(byte[] buffer)
        //{
        //    using (MemoryStream ms = new MemoryStream(buffer))
        //    {
        //        using (BinaryReader reader=new BinaryReader(ms))
        //        {
        //            int count = reader.ReadInt32();
        //            for (int i = 0; i < count; i++)
        //            {
        //                _recordSets.Add(reader.ReadString(),reader.ReadInt64());
        //            }

        //            long next = reader.ReadInt64();
        //            if (next > 0)
        //            {
        //                byte[] data = _fileEngine.Read(next);
        //                ReadSystemRecord(data);
        //            }
        //        }
        //    }

        //}
    }
}
