using System.Collections.Generic;
using System.Linq;

namespace Practice.Algorithm.Tree.BTree
{
    public class BTreeNode<T>
    {
        private IList<BTreeNode<T>> _children;
        private int _minSize;

        public BTreeNode(int minSize)
        {
            _minSize = minSize;
            _children = new List<BTreeNode<T>>();
            Elements = new List<BTreeNodeElement<T>>();
            Parent = null;
        } 

        public IList<BTreeNodeElement<T>> Elements { get; private set; }
        public BTreeNode<T> Parent { get; private set; }

        public int Size
        {
            get
            {
                return Elements.Count;
            }
        }

        public BTreeNodeStatus Status
        {
            get
            {
                if (Size < _minSize && Parent != null)
                {
                    return BTreeNodeStatus.Underflow;
                }

                if (Size > 2 * _minSize)
                {
                    return BTreeNodeStatus.Overflow;
                }

                if (Size == _minSize)
                {
                    return BTreeNodeStatus.Normal;
                }

                return BTreeNodeStatus.Full;
            }
        }

        public int Index
        {
            get
            {
                if (Parent == null)
                {
                    return -1;
                }

                return Parent._children.IndexOf(this);
            }
        }

        public void SetParent(BTreeNode<T> node)
        {
            Parent = node;
        }

        public BTreeNode<T> Children(int index)
        {
            if (_children.Count > index && index >= 0)
            {
                return _children[index];
            }

            return null;
        }

        public void AddChild(BTreeNode<T> child)
        {
            _children.Add(child);
            child.Parent = this;
        }

        public void AddChild(int index, BTreeNode<T> child)
        {
            _children.Insert(index, child);
            child.Parent = this;
        }

        public void RemoveChild(int index)
        {
            if (_children.Count > index)
            {
                _children.RemoveAt(index);
            }    
        }

        public BTreeNode<T> AddElement(long key, T value)
        {
            return AddElement(
                new BTreeNodeElement<T>
                {
                    Key = key,
                    Value = value,
                },
                0);
        }

        public BTreeNode<T> DeleteElement(long key)
        {
            return DeleteElement(key, 0);
        }

        public T[] FindElement(long minkey, long maxKey)
        {
            IList<T> result = new List<T>();
            FindElement(result, minkey, maxKey);
            return result.ToArray();
        }

        public BTreeNode<T> GetMinChild()
        {
            BTreeNode<T> minNode = Children(0);
            if (minNode != null)
            {
                return Children(0).GetMinChild();
            }

            return this;
        }

        public BTreeNode<T> GetMaxChild()
        {
            BTreeNode<T> maxNode = Children(Size);
            if (maxNode != null)
            {
                return maxNode.GetMaxChild();
            }

            return this;
        } 

        private void FindElement(IList<T> result, long minKey, long maxKey)
        {
            for (int i = 0; i < Size; i++)
            {
                if (Elements[i].Key > minKey)
                {
                    if (Children(i) != null)
                    {
                        if (Elements[i].Key > maxKey)
                        {
                            Children(i).FindElement(result, minKey, maxKey);
                        }
                        else
                        {
                            Children(i).FindElement(result, minKey, Elements[i].Key);
                        }
                    }
                }

                if (Elements[i].Key <= maxKey && Elements[i].Key >= minKey)
                {
                    result.Add(Elements[i].Value);
                }
            }

            if (Elements.Any() && maxKey > Elements.Last().Key)
            {
                if (Children(Size) != null)
                {
                    if (Elements.Last().Key > minKey)
                    {
                        Children(Size).FindElement(result, Elements.Last().Key, maxKey);
                    }
                    else
                    {
                        Children(Size).FindElement(result, minKey, maxKey);
                    }
                }
            }
        }

        private BTreeNode<T> DeleteElement(long key, int index)
        {
            if (index == Size)
            {
                if (Children(index) != null)
                {
                    return Children(index).DeleteElement(key);
                }

                return null;
            }

            if (Elements[index].Key == key)
            {
                Elements.RemoveAt(index);
                BTreeNode<T> rightChild = Children(index + 1);
                if (rightChild != null)
                {
                    BTreeNode<T> minNode = rightChild.GetMinChild();
                    Elements.Insert(index, minNode.Elements[0]);
                    return minNode.DeleteElement(minNode.Elements[0].Key);
                }

                return this;
            }

            if (Elements[index].Key < key)
            {
                return DeleteElement(key, index+1);
            }

            if (Children(index) != null)
            {
                return Children(index).DeleteElement(key);
            }

            return null;
        }

        private BTreeNode<T> AddElement(BTreeNodeElement<T> element, int index)
        {
            if (index == Size)
            {
                if (Children(index) != null)
                {
                    return Children(index).AddElement(element.Key, element.Value);
                }

                Elements.Add(element);
                return this;
            }

            if (Elements[index].Key == element.Key)
            {
                return null;
            }

            if (element.Key < Elements[index].Key)
            {
                if (Children(index) != null)
                {
                    return Children(index).AddElement(element.Key, element.Value);
                }

                Elements.Insert(index, element);

                return this;
            }

            return AddElement(element, index+1);
        }
    }
}
