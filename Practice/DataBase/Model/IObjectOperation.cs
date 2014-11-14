using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practice.DataBase.Storage;

namespace Practice.DataBase.Model
{
    public interface IObjectOperation
    {
        bool AddObject(long key, object item, StorageProvider storage, string table);

        bool DeleteObject(long key, StorageProvider storage, string table);
    }
}
