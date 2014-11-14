using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Practice.DataBase.Storage;
using Practice.DataBase.UserInterface;

namespace Practice.DataBase
{
    public class DBEngine : IDisposable
    {
        private StorageProvider _dataAdapter;
        private FileStream _dataStream;
        private FileStream _sysStream;
        private List<RecordSet> _recordSets;
 
        public DBEngine(string dataFile, string sysFile)
        {
            _dataStream = new FileStream(dataFile, FileMode.Open, FileAccess.ReadWrite);
            _sysStream = new FileStream(sysFile, FileMode.Open, FileAccess.ReadWrite);
            _recordSets = new List<RecordSet>();

            _dataAdapter=new StorageProvider(_dataStream, _sysStream);
            //_dataAdapter = new DataAdapter(new MemoryStream(int.MaxValue/100), new MemoryStream(int.MaxValue/100));
        }

        public RecordSet OpenRecordSet(string recordPath)
        {
            RecordSet set = _recordSets.FirstOrDefault(r => r.Table == recordPath);
            if (set == null)
            {
                set=new RecordSet(_dataAdapter, recordPath);
                _recordSets.Add(set);
            }

            return set;
        }

        public void Commit()
        {
            _recordSets.ForEach(r => r.Commit());
            _dataStream.Flush();
            _sysStream.Flush();
        }

        public void Dispose()
        {
            Commit();

            _sysStream.Close();
            _sysStream.Dispose();
            _dataStream.Close();
            _dataStream.Dispose();
        }
    }
}
