using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.DataBase.Model
{
    public class RelationshipModel:IObjectOperation
    {
        public bool AddObject(long key, object item, Storage.StorageProvider storage, string table)
        {
            throw new NotImplementedException();
        }

        public bool DeleteObject(long key, Storage.StorageProvider storage, string table)
        {
            throw new NotImplementedException();
        }
    }
}
