using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Practice.DataBase.Storage;

namespace Practice.DataBase.Model
{
    public class ObjectSerializer:IObjectOperation
    {
        public static string Serialize(object item)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(item);
            return json;
        }

        public static T Deserialize<T>(string serialized)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            T obj = serializer.Deserialize<T>(serialized);

            return obj;
        }

        public static object Deserialize(string serialized)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            object obj = serializer.DeserializeObject(serialized);

            return obj;
        }

        public bool AddObject(long key, object item, StorageProvider storage, string table)
        {
            string buffer = Serialize(item);
            storage.SetPath(table);
            storage.Save(key, buffer);
            return true;
        }

        public bool DeleteObject(long key, StorageProvider storage, string table)
        {
            storage.SetPath(table);
            return storage.Delete(key);
        }
    }
}
