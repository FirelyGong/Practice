using System.Collections.Generic;
using System.Text;

namespace Practice.Algorithm.Tree.BPlusTree
{
    public abstract class BPTreeNode
    {
        private BPTreeNodeCollection _children;
        
        protected BPTreeNode(BPTreeInternalNode parent, BPTree tree, int nodeId, short minSize, string key)
        {
            Tree = tree;
            Parent = parent;
            Key = key;
            NodeId = nodeId;
            MinSize = minSize;
            _children = null;
        }

        public int NodeId { get; private set; }

        public BPTreeNodeType NodeType { get; protected set; }

        public BPTree Tree { get; private set; }

        public int Index
        {
            get
            {
                if (Parent == null)
                {
                    return -1;
                }

                int index = Parent.Children.FindIndex(this);
                return index;
            }
        }

        public BPTreeNode Next()
        {
            if (Parent != null)
            {
                if (Index >= Parent.Size - 1)
                {
                    if (Parent.Next() != null)
                    {
                        return Parent.Next().Children[0];
                    }

                    return null;
                }

                return Parent.Children[Index + 1];
            }

            return null;
        }

        public BPTreeNode Previous()
        {
            if (Parent != null)
            {
                if (Index <= 0)
                {
                    if (Parent.Previous() != null)
                    {
                        return Parent.Previous().Children.Last();
                    }

                    return null;
                }

                return Parent.Children[Index - 1];
            }

            return null;
        }

        public BPTreeInternalNode Parent { get; internal set; }

        public short MinSize { get; protected set; }

        public string Key { get; private set; }

        public bool IsModified { get; protected set; }

        public short Size
        {
            get
            {
                return GetSize();
            }
        }

        public BPTreeNodeState State
        {
            get
            {
                return EvaluateState(0);
            }
        }

        internal BPTreeNodeCollection Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new BPTreeNodeCollection();
                    if (NodeId >= 0 && NodeType == BPTreeNodeType.Internal)
                    {
                        Load();
                    }
                }

                return _children;
            }
        }

        internal void RestoreChildren(IList<BPTreeNode> nodes)
        {
            _children.Restore(nodes);
        }

        internal void Store()
        {
            Key = GetKey();
            if (Tree == null)
            {
                return;
            }
            Store(Tree.NodePersist);
        }

        internal void Load()
        {
            if (Tree == null)
            {
                return;
            }

            Load(Tree.NodePersist);
        }
        
        internal BPTreeNodeState EvaluateState(int addtional)
        {
            int newSize = Size + addtional;

            if (newSize < MinSize)
            {
                return BPTreeNodeState.Underflow;
            }

            if (newSize > 2 * MinSize)
            {
                return BPTreeNodeState.Overflow;
            }

            if (newSize == MinSize)
            {
                return BPTreeNodeState.Normal;
            }

            return BPTreeNodeState.Full;
        }
        
        protected int ObtainNodeId()
        {
            if (Tree == null)
            {
                return -1;
            }

            return ObtainNodeId(Tree.NodePersist);
        }

        protected void UpdateRootNode(BPTreeNode node)
        {
            if (Tree == null)
            {
                return;
            }

            Tree.Root = node;
        }
        
        public virtual bool AddElement(string key, string value)
        {
            return false;
        }

        public virtual bool DeleteElement(string key, out string value)
        {
            value = string.Empty;
            return false;
        }

        public virtual string FindElement(string key)
        {
            return null;
        }

        public virtual IList<string> FindElements(string keyFrom, string keyTo)
        {
            return new List<string>();
        }

        protected virtual short GetSize()
        {
            return Children.Count;
        }

        protected virtual string GetKey()
        {
            if (_children != null && _children.Any())
            {
                return _children.First().Key;
            }

            return null;
        }

        protected virtual byte[] Serialize()
        {
            return new byte[] { };
        }

        protected virtual void Deserialize(byte[] serialized)
        {
        }
        
        public override string ToString()
        {
            if (NodeType == BPTreeNodeType.Internal)
            {
                return Children.ToString();
            }

            return ((BPTreeLeafNode)this).Elements.ToString();
        }

        public string DetailString
        {
            get
            {
                if (NodeType == BPTreeNodeType.Internal)
                {
                    List<string> nodes = new List<string>();
                    for (int i = 0; i < Children.Count; i++)
                    {
                        nodes.Add(string.Concat("(",Children[i].NodeType,Children[i].Key,":", Children[i].DetailString, ")"));
                    }

                    return string.Join("->", nodes.ToArray());
                }

                return ((BPTreeLeafNode)this).Elements.ToString();
            }
        }

        private int ObtainNodeId(INodePersistence nodeStore)
        {
            if (nodeStore != null)
            {
                return nodeStore.ObtainNodeId();
            }

            return -1;
        }

        private void Store(INodePersistence store)
        {
            if (store == null)
            {
                return;
            }

            if (Size > 0)
            {
                byte[] serialize = Serialize();
                store.Write(NodeId, serialize);
            }
            else
            {
                store.Release(NodeId);
            }
        }

        private void Load(INodePersistence store)
        {
            if (store == null)
            {
                return;
            }

            byte[] serialized = store.Read(NodeId);

            if (serialized.Length == 20 && Encoding.UTF8.GetString(serialized) == "Reserving data block")
            {
                return;
            }

            if (serialized.Length > 0)
            {
                Deserialize(serialized);
            }
        }
    }
}
