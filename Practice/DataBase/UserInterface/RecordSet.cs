using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using Practice.DataBase.Exceptions;
using Practice.DataBase.Model;
using Practice.DataBase.Storage;

namespace Practice.DataBase.UserInterface
{
    public class RecordSet
    {
        private StorageProvider _storage;
        private string _table;
        private ObjectOperation _objectOperation;

        public RecordSet(StorageProvider storageProvider, string table)
        {
            _table = table;
            _storage = storageProvider;
            _objectOperation = new ObjectOperation();
        }

        public string Table
        {
            get
            {
                return _table;
            }
        }

        public bool Save(long key, object item)
        {
            try
            {
                return _objectOperation.AddObject(key, item, _storage, Table);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, ExceptionPolicy.Default);
                return false;
            }
        }

        public bool Delete(long key)
        {
            try
            {
                return _objectOperation.DeleteObject(key, _storage, Table);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, ExceptionPolicy.Default);
                return false;
            }
        }

        public object Get(long key)
        {
            try
            {
                string buffer = _storage.Read(key);
                return ObjectSerializer.Deserialize(buffer);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, ExceptionPolicy.Default);
                return null;
            }
        }

        public object[] Get(long keyFrom, long keyTo)
        {
            try
            {
                string[] buffer = _storage.Read(keyFrom, keyTo);
                object[] objs=new object[buffer.Length];
                for (int i = 0; i < buffer.Length; i++)
                {
                    objs[i] = ObjectSerializer.Deserialize(buffer[i]);
                }

                return objs;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, ExceptionPolicy.Default);
                return null;
            }
        }

        public object[] Get(long keyFrom, int count)
        {
            throw new NotImplementedException();
        }

        public int GetRecordCount()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            _storage.Commit();
        }
    }
}
