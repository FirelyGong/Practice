using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Practice.DataBase.Storage;

namespace Practice.DataBase.Model
{
    public class ObjectAnalyser:IObjectOperation
    {
        private const string SubTable = "Attributes";

        public bool AddObject(long key, object item, StorageProvider storage, string table)
        {
            storage.SetPath(table, SubTable);
            PropertyInfo[] propertyInfos =item.GetType().GetProperties();
            foreach (PropertyInfo p in propertyInfos)
            {
                if (p.PropertyType.IsValueType || p.PropertyType.Equals(typeof(string)))
                {
                    //string buffer=storage.Read()
                }   
            }

            return false;
        }

        public bool DeleteObject(long key, StorageProvider storage, string table)
        {
            throw new NotImplementedException();
        }
    }
}
