using System;
using System.Collections.Generic;
using System.Linq;

namespace Practice.Algorithm.Tree.BPlusTree
{
    internal class BPTreeNodeCollection
    {
        private readonly List<BPTreeNode> _children;

        public BPTreeNodeCollection()
        {
            _children = new List<BPTreeNode>();
        }

        public event Action DataChanged;

        public short Count
        {
            get
            {
                return (short)_children.Count;
            }
        }

        public BPTreeNode this[int index]
        {
            get
            {
                if (CheckIndex(index))
                {
                    return _children[index];
                }

                return null;
            }
        }

        public int FindIndex(BPTreeNode node)
        {
            return _children.IndexOf(node);
        }

        public bool Any()
        {
            return _children.Any();
        }

        public BPTreeNode FindBranch(string key)
        {
            int index = GetKeys().BinarySearch(key);
            if (index >= 0)
            {
                return this[index];
            }

            index = ~index;
            if (index > 0)
            {
                index--;
            }

            return this[index];
        }

        public BPTreeNode First()
        {
            return _children.First();
        }

        public BPTreeNode Last()
        {
            return _children[Count - 1];
        }

        public void Insert(int index, BPTreeNode branch)
        {
            if (index >= 0 && index <= Count)
            {
                _children.Insert(index, branch);
                NotifyDataChanged();
            }
        }

        public void Add(BPTreeNode node)
        {
            Insert(Count, node);
        }

        public void AddRange(int index, BPTreeNode[] branches)
        {
            if (index >= 0 && index <= Count)
            {
                for (int i = 0; i < branches.Count(); i++)
                {
                    Insert(index + i, branches[i]);
                }
            }
        }

        public BPTreeNode[] DeleteRange(int indexFrom, int count = 0)
        {
            var toDelete = new List<BPTreeNode>();
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
            }

            toDelete.ForEach(c => Delete(_children.IndexOf(c)));

            return toDelete.ToArray();
        }

        public BPTreeNode Delete(int index)
        {
            if (CheckIndex(index))
            {
                var branch = _children[index];
                _children.RemoveAt(index);
                NotifyDataChanged();
                return branch;
            }

            return null;
        }

        public bool Contains(BPTreeNode node)
        {
            return _children.Contains(node);
        }

        public bool Delete(BPTreeNode node)
        {
            bool bln = _children.Remove(node);
            if (bln)
            {
                NotifyDataChanged();
            }

            return bln;
        }

        public void Restore(IList<BPTreeNode> nodes)
        {
            _children.Clear();
            _children.AddRange(nodes);
        }

        public override string ToString()
        {
            IList<string> arr = new List<string>();
            _children.ForEach(d => arr.Add(d.Key));

            return string.Join(",", arr.ToArray());
        }

        private bool CheckIndex(int index)
        {
            return index >= 0 && index < Count;
        }

        private List<string> GetKeys()
        {
            return _children.Select(item => item.Key).ToList();
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
