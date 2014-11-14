using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practice.DataBase.Exceptions;
using Practice.DataBase.Storage;

namespace Practice.DataBase.Model
{
    public class ObjectOperation:IObjectOperation
    {
        private ObjectAnalyser _objectAnalyser;
        private ObjectSerializer _objectSerializer;
        private RelationshipModel _relationshipModel;

        public ObjectOperation()
        {
            _objectAnalyser = new ObjectAnalyser();
            _objectSerializer = new ObjectSerializer();
            _relationshipModel = new RelationshipModel();
        }

        public bool AddObject(long key, object item, StorageProvider storage, string table)
        {
            try
            {
                bool bln = _objectSerializer.AddObject(key, item, storage, table);
                if (!bln)
                {
                    return false;
                }

                bln = _objectAnalyser.AddObject(key, item, storage, table);
                if (!bln)
                {
                    return false;
                }

                bln = _relationshipModel.AddObject(key, item, storage, table);
                if (!bln)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, ExceptionPolicy.Default);
                return false;
            }
        }

        public bool DeleteObject(long key, StorageProvider storage, string table)
        {
            try
            {
                bool bln = _objectSerializer.DeleteObject(key, storage, table);
                if (!bln)
                {
                    return false;
                }

                bln = _objectAnalyser.DeleteObject(key, storage, table);
                if (!bln)
                {
                    return false;
                }

                bln = _relationshipModel.DeleteObject(key, storage, table);
                if (!bln)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, ExceptionPolicy.Default);
                return false;
            }
        }
    }
}
