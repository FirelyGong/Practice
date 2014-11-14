using System;
using System.Collections.Generic;
using System.Linq;

namespace Practice.Algorithm.Tree.BPlusTree
{
    public class BPTreeDataCollection
    {
        private readonly List<BPTreeNodeData> _dataCollection;

        public BPTreeDataCollection()
        {
            _dataCollection = new List<BPTreeNodeData>();
        }

        public event Action DataChanged;

        public short Count
        {
            get
            {
                return (short)_dataCollection.Count;
            }
        }

        public BPTreeNodeData this[int index]
        {
            get
            {
                if (CheckIndex(index))
                {
                    return _dataCollection[index];
                }

                return null;
            }
        }

        public int FindIndex(string key)
        {
            return GetKeys().BinarySearch(key);
        }

        public BPTreeNodeData First()
        {
            return _dataCollection.First();
        }

        public BPTreeNodeData Last()
        {
            return _dataCollection[Count-1];
        }

        public void Insert(int index, BPTreeNodeData data)
        {
            if (index >= 0 && index <= Count)
            {
                _dataCollection.Insert(index, data);

                NotifyDataChanged();
            }
        }

        public void Add(BPTreeNodeData data)
        {
            Insert(Count, data);
        }

        public void AddRange(int index, BPTreeNodeData[] datas)
        {
            if (index >= 0 && index <= Count)
            {
                for (int i = 0; i < datas.Count(); i++)
                {
                    Insert(index + i, datas[i]);
                }
            }
        }

        public BPTreeNodeData[] DeleteRange(int indexFrom, int count = 0)
        {
            var toDelete = new List<BPTreeNodeData>();
            if (CheckIndex(indexFrom))
            {
                int loop = count;
                if (loop == 0)
                {
                    loop = Count - indexFrom;
                }

                for (int i = 0; i < loop; i++)
                {
                    toDelete.Add(this[indexFrom + i]);
                }

                toDelete.ForEach(c => Delete(_dataCollection.IndexOf(c)));
            }

            return toDelete.ToArray();
        }

        public BPTreeNodeData Delete(int index)
        {
            if (CheckIndex(index))
            {
                var data = _dataCollection[index];
                _dataCollection.RemoveAt(index);
                NotifyDataChanged();
                return data;
            }

            return null;
        }

        public void Restore(IList<BPTreeNodeData> data)
        {
            _dataCollection.Clear();
            _dataCollection.AddRange(data);
        }

        public override string ToString()
        {
            IList<string> arr = new List<string>();
            _dataCollection.ForEach(d => arr.Add(d.Key));

            return string.Join(",", arr.ToArray());
        }

        private bool CheckIndex(int index)
        {
            return index >= 0 && index < Count;
        }

        private List<string> GetKeys()
        {
            return _dataCollection.Select(item => item.Key).ToList();
        }

        private void NotifyDataChanged()
        {
            if (DataChanged != null)
            {
                DataChanged();
            }
        }
    }
}
