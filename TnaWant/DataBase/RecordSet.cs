using System;
using System.IO;
using Practice.Algorithm.Tree.BPlusTree;
using Practice.FileSystem;

namespace TnaWant.DataBase
{
    public class RecordSet<T>
    {
        //private BPTree<long> _index;
        //private FileEngine _fileEngine;

        //public RecordSet(long indexAddress, INodeStore nodePersistence, FileEngine fileEngine)
        //{
        //    _index = new BPTree<long>(10, indexAddress, nodePersistence);
        //    _fileEngine = fileEngine;
        //} 

        //public T Get(long id)
        //{
        //    long address = _index.Find(id);
        //    if (address <= 0)
        //    {
        //        return default (T);
        //    }

        //    byte[] buffer = _fileEngine.Read(address);
        //    string data;
        //    using (MemoryStream ms=new MemoryStream(buffer))
        //    {
        //        using (BinaryReader reader = new BinaryReader(ms))
        //        {
        //            data = reader.ReadString();
        //        }
        //    }

        //    return (T)((object)data);
        //}

        //public void Save(long id, T item)
        //{
        //    //Console.Out.WriteLine("1,"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
        //    byte[] buffer = new byte[item.ToString().Length + 1];
        //    using (MemoryStream ms = new MemoryStream(buffer))
        //    {
        //        using (BinaryWriter writer = new BinaryWriter(ms))
        //        {
        //            writer.Write(item.ToString());
        //        }
        //    }

        //    //Console.Out.WriteLine("2," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
        //    long address = _fileEngine.Save(buffer);
        //    //Console.Out.WriteLine("3," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
        //    //_fileEngine.Commit();

        //    //Console.Out.WriteLine("4," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
        //    _index.Add(id, address);
        //    //Console.Out.WriteLine("5," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
        //}

        //public T Delete(long id)
        //{
        //    long address = _index.Find(id);
        //    if (address <= 0)
        //    {
        //        return default(T);
        //    }

        //    byte[] buffer = _fileEngine.Read(address);
        //    string data;
        //    using (MemoryStream ms = new MemoryStream(buffer))
        //    {
        //        using (BinaryReader reader = new BinaryReader(ms))
        //        {
        //            data = reader.ReadString();
        //        }
        //    }

        //    _fileEngine.Delete(address);
        //    //_fileEngine.Commit();
        //    _index.Delete(id);

        //    return (T)((object)data);
        //}

        //public bool Update(long id, T item)
        //{
        //    long address = _index.Find(id);
        //    if (address <= 0)
        //    {
        //        return false;
        //    }

        //    byte[] buffer = new byte[item.ToString().Length + 1];
        //    using (MemoryStream ms = new MemoryStream(buffer))
        //    {
        //        using (BinaryWriter writer = new BinaryWriter(ms))
        //        {
        //            writer.Write(item.ToString());
        //        }
        //    }

        //    if (_fileEngine.Update(address, buffer))
        //    {
        //        //_fileEngine.Commit();
        //        return true;
        //    }

        //    return false;
        //}
    }
}
