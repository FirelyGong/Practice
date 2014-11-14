using System;
using System.Collections.Generic;

namespace Practice.Algorithm.Tree.BPlusTree
{
    public class BPTreeLeafNode:BPTreeNode
    {
        private BPTreeDataCollection _elements;

        public BPTreeLeafNode(BPTreeInternalNode parent, BPTree tree, int nodeId, short minSize, string key = "")
            : base(parent,tree, nodeId, minSize, key)
        {
            _elements = null;
            NodeType = BPTreeNodeType.Leaf;
        }

        public BPTreeDataCollection Elements
        {
            get
            {
                if (_elements == null)
                {
                    _elements = new BPTreeDataCollection();
                    if (NodeId >= 0)
                    {
                        Load();
                    }
                }

                return _elements;
            }
        }
        
        public override bool AddElement(string key, string value)
        {
            int index = Elements.FindIndex(key);
            if (index < 0)
            {
                index = ~index;
            }

            Elements.Insert(index, new BPTreeNodeData { Key = key, Value = value });

            if (State == BPTreeNodeState.Overflow)
            {
                Overflow();
            }
            else
            {
                Store();
                if (index == 0)
                {
                    IsModified = true;
                    if (Parent != null)
                    {
                        Parent.Maintain();
                    }
                    IsModified = false;
                }
            }

            return true;
        }

        public override bool DeleteElement(string key, out string value)
        {
            value = null;
            int index = Elements.FindIndex(key);
            if (index >= 0)
            {
                BPTreeNodeData data = Elements.Delete(index);
                value = data.Value;

                if (State == BPTreeNodeState.Underflow)
                {
                    Underflow();
                }
                else
                {
                    Store();
                    //if (index == 0)
                    //{
                        IsModified = true;
                        if (Parent != null)
                        {
                            Parent.Maintain();
                        }
                        IsModified = false;
                    //}
                }
                return true;
            }

            return false;
        }

        public override string FindElement(string key)
        {
            int index = Elements.FindIndex(key);
            if (index >= 0)
            {
                return Elements[index].Value;
            }

            return null;
        }

        public override IList<string> FindElements(string keyFrom, string keyTo)
        {
            var result = new List<string>();

            for (int i = 0; i < Elements.Count; i++)
            {
                var element = Elements[i];

                if (String.CompareOrdinal(element.Key, keyFrom)>=0 && String.CompareOrdinal(element.Key, keyTo)<=0)
                {
                    result.Add(element.Value);
                }
            }

            return result;
        }

        protected override short GetSize()
        {
            return Elements.Count;
        }
        
        protected override byte[] Serialize()
        {
            byte[] buffer;
            using (var serializer = new ByteArraySerializer())
            {
                serializer.Write(Elements.Count);
                for (int i = 0; i < Elements.Count; i++)
                {
                    serializer.Write(Elements[i].Key);
                    serializer.Write(Elements[i].Value);
                }

                buffer = serializer.SerializedBuffer;
            }

            return buffer;
        }

        protected override void Deserialize(byte[] serialized)
        {
            using (var serializer = new ByteArraySerializer())
            {
                serializer.SetDeserializingBuffer(serialized);

                int count = serializer.ReadShort();

                var list = new List<BPTreeNodeData>();
                for (int i = 0; i < count; i++)
                {
                    list.Add(new BPTreeNodeData { Key = serializer.ReadString(), Value = serializer.ReadString() });
                }

                _elements.Restore(list);
            }
        }

        protected override string GetKey()
        {
            if (_elements != null && _elements.Count>0)
            {
                return _elements.First().Key;
            }

            return null;
        }

        internal BPTreeNodeData[] RemoveNodeData(bool fromTail, int count)
        {
            int deleteIndex;
            if (fromTail)
            {
                deleteIndex = Size - count;
            }
            else
            {
                deleteIndex = 0;
            }

            var splitElements = Elements.DeleteRange(deleteIndex, count);

            Store();
            //if (deleteIndex == 0)
            //{
                IsModified = true;
                if (Parent != null)
                {
                    Parent.Maintain();
                }
                IsModified = false;
            //}

            return splitElements;
        }

        internal void AddNodeData(bool fromTail, BPTreeNodeData[] data)
        {
            int insertIndex;
            if (fromTail)
            {
                insertIndex = Elements.Count;
            }
            else
            {
                insertIndex = 0;
            }

            Elements.AddRange(insertIndex, data);

            Store();
            //if (insertIndex == 0)
            //{
                IsModified = true;
                if (Parent != null)
                {
                    Parent.Maintain();
                }
                IsModified = false;
            //}
        }

        private void Overflow()
        {
            var previous = Previous();
            var next = Next();
            if (next != null && next.EvaluateState(1) == BPTreeNodeState.Full)
            {
                var data = RemoveNodeData(true, 1);
                ((BPTreeLeafNode)next).AddNodeData(false, data);
            }
            else if (previous != null && previous.EvaluateState(1) == BPTreeNodeState.Full)
            {
                var data = RemoveNodeData(false, 1);
                ((BPTreeLeafNode)previous).AddNodeData(true, data);
            }
            else
            {
                if (Parent == null)
                {
                    var parent = new BPTreeInternalNode(null, Tree, ObtainNodeId(), MinSize);
                    parent.Children.Add(this);
                    Parent = parent;
                    UpdateRootNode(parent);
                }

                var newNode = new BPTreeLeafNode(Parent, Tree, ObtainNodeId(), MinSize);

                Parent.Children.Insert(Index + 1, newNode);
                var data = RemoveNodeData(true, MinSize);
                newNode.AddNodeData(false, data);
            }
        }

        private void Underflow()
        {
            var previous = Previous();
            var next = Next();
            if (previous != null && previous.EvaluateState(-1) >= BPTreeNodeState.Normal)
            {
                var data = ((BPTreeLeafNode)previous).RemoveNodeData(true, 1);
                AddNodeData(false, data);
            }
            else if (next != null && next.EvaluateState(-1) >= BPTreeNodeState.Normal)
            {
                var data = ((BPTreeLeafNode)next).RemoveNodeData(false, 1);
                AddNodeData(true, data);
            }
            else if (next != null)
            {
                next.Parent.Children.Delete(next);
                var data = ((BPTreeLeafNode)next).RemoveNodeData(false, next.Size);
                next.Parent = null;
                AddNodeData(true, data);
            }
            else if (previous != null)
            {
                previous.Parent.Children.Delete(previous);
                var data = ((BPTreeLeafNode)previous).RemoveNodeData(true, previous.Size);
                previous.Parent = null;
                AddNodeData(false, data);
            }
            else
            {
                //it's root node
                IsModified = true;
                if (Parent != null && Tree!=null)
                {
                    Parent = null;
                    Tree.Root = this;
                }
            }
        }
    }
}
