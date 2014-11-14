using Practice.ThingsBase.DataPool;

namespace Practice.ThingsBase.VTable
{
    public class ThingsClass
    {
        private readonly DataContainer _dataContainer;
        public ThingsClass(string className, string dataFile)
        {
            _dataContainer = new DataContainer(dataFile);
            Class = className;
        }

        public string Class { get; private set; }

        public bool Save(string key, string data)
        {
            return _dataContainer.Save(ComposeKey(key), data);
        }

        public string Get(string key)
        {
            return _dataContainer.Get(ComposeKey(key));
        }

        public string Delete(string key)
        {
            return _dataContainer.Delete(ComposeKey(key));
        }

        public string ComposeKey(string key)
        {
            return string.Concat(Class, "/", key);
        }
    }
}
