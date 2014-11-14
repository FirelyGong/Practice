using System;

namespace Practice.ThingsBase
{
    public class ThingsBaseEngine
    {
        private string _dataFile;

        public ThingsBaseEngine(string dataFile)
        {
            _dataFile = dataFile;
        }

        public bool Save(ThingsData data)
        {
            throw new NotImplementedException();
        }

        public ThingsData Get(string key)
        {
            throw new NotImplementedException();
        }

        public ThingsData Delete(string key)
        {
            throw new NotImplementedException();
        }
    }
}
