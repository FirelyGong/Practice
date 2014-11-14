using System;
using System.Collections.Generic;
using System.Linq;

namespace Practice.Algorithm.Tree.BPlusTree
{
    public class BPTreeInternalNode : BPTreeNode
    {
        public BPTreeInternalNode(BPTreeInternalNode parent, BPTree tree, int nodeId, short minSize, string key = "")
            : base(parent, tree, nodeId, minSize,key)
        {
            NodeType=BPTreeNodeType.Internal;
        }
        
        public override bool AddElement(string key, string value)
        {
            if (!Children.Any())
            {
                throw new Exception("BPTree node error! internal node empty.");
            }

            var branch = Children.FindBranch(key);
            var result = branch.AddElement(key, value);

            return result;
        }

        public override bool DeleteElement(string key, out string value)
        {
            value = string.Empty;

            if (!Children.Any())
            {
                throw new Exception("BPTree node error! internal node empty.");
            }

            var branch = Children.FindBranch(key);
            var result = branch.DeleteElement(key, out value);

            return result;
        }

        public override string FindElement(string key)
        {
            if (!Children.Any())
            {
                return null;
            }

            var branch = Children.FindBranch(key);
            return branch.FindElement(key);
        }

        public override IList<string> FindElements(string keyFrom, string keyTo)
        {
            if (!Children.Any() || String.CompareOrdinal(keyFrom, keyTo) > 0)
            {
                return new List<string>();
            }

            var result = new List<string>();

            for (int i = 0; i < Children.Count; i++)
            {
                BPTreeNode branch = Children[i];
                if (String.CompareOrdinal(branch.Key, keyTo) > 0)
                {
                    break;
                }

                string from = branch.Key;
                if (String.CompareOrdinal(from, keyFrom) < 0)
                {
                    from = keyFrom;
                }

                result.AddRange(branch.FindElements(from, keyTo));
            }

            return result;
        }
        
        internal void Maintain()
        {
            if (State == BPTreeNodeState.Overflow)
            {
                Overflow();
            }
            else if(State==BPTreeNodeState.Underflow)
            {
                //if it's root
                if (!(Parent==null && Size==2))
                {
                    Underflow();
                }
            }
            else 
            {
                bool changed = Key != Children[0].Key;
                if (changed)
                {
                    Store();

                    IsModified = true;
                    if (Parent != null)
                    {
                        Parent.Maintain();
                    }

                    IsModified = false;
                }
            }
        }

        internal BPTreeNode[] RemoveChildren(bool fromTail, int count)
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

            var splitElements = Children.DeleteRange(deleteIndex, count);

            Store();
            IsModified = true;
            if (Parent != null)
            {
                Parent.Maintain();
            }
            IsModified = false;

            return splitElements;
        }

        internal void AddChildren(bool fromTail, BPTreeNode[] data)
        {
            int insertIndex;
            if (fromTail)
            {
                insertIndex = Children.Count;
            }
            else
            {
                insertIndex = 0;
            }

            data.ToList().ForEach(d => d.Parent = this);
            Children.AddRange(insertIndex, data);

            Store();
            IsModified = true;
            if (Parent != null)
            {
                Parent.Maintain();
            }
            IsModified = false;
        }
        
        protected override byte[] Serialize()
        {
            byte[] buffer;
            using (var serializer = new ByteArraySerializer())
            {
                serializer.Write(Children.Count);
                for (int i = 0; i < Children.Count; i++)
                {
                    serializer.Write((byte)Children[i].NodeType);
                    serializer.Write(Children[i].NodeId);
                    serializer.Write(Children[i].MinSize);
                    serializer.Write(Children[i].Key);
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
                int type;
                int id;
                short size;
                string key;

                int count = serializer.ReadShort();
                var list = new List<BPTreeNode>();
                for (int i = 0; i < count; i++)
                {
                    type = serializer.ReadByte();
                    id = serializer.ReadInt();
                    size = serializer.ReadShort();
                    key = serializer.ReadString();

                    if (type == (byte)BPTreeNodeType.Internal)
                    {
                        list.Add(new BPTreeInternalNode(this, Tree, id, size, key));
                    }
                    else
                    {
                        list.Add(new BPTreeLeafNode(this, Tree, id, size, key));
                    }
                }

                RestoreChildren(list);
            }
        }
        
        private void Overflow()
        {
            var previous = Previous();
            var next = Next();
            if (next != null && next.EvaluateState(1) == BPTreeNodeState.Full)
            {
                var data= RemoveChildren(true, 1);
                ((BPTreeInternalNode)next).AddChildren(false, data);
            }
            else if (previous != null && previous.EvaluateState(1) == BPTreeNodeState.Full)
            {
                var data = RemoveChildren(false, 1);
                ((BPTreeInternalNode)previous).AddChildren(true, data);
            }
            else
            {
                if (Parent == null)
                {
                    var parent = new BPTreeInternalNode(null, Tree, ObtainNodeId(), MinSize);
                    parent.Children.Add(this);
                    Parent = parent;
                    UpdateRootNode(Parent);
                }

                var newNode = new BPTreeInternalNode(Parent, Tree, ObtainNodeId(), MinSize);

                Parent.Children.Insert(Index + 1, newNode);
                var data = RemoveChildren(true, MinSize);
                newNode.AddChildren(false, data);
            }
        }

        private void Underflow()
        {
            var previous = Previous();
            var next = Next();
            if (previous != null && previous.EvaluateState(-1) >= BPTreeNodeState.Normal)
            {
                var data = ((BPTreeInternalNode)previous).RemoveChildren(true, 1);
                AddChildren(false, data);
            }
            else if (next != null && next.EvaluateState(-1) >= BPTreeNodeState.Normal)
            {
                var data = ((BPTreeInternalNode)next).RemoveChildren(false, 1);
                AddChildren(true, data);
            }
            else if (next != null)
            {
                next.Parent.Children.Delete(next);
                var data = ((BPTreeInternalNode)next).RemoveChildren(false, next.Size);
                next.Parent = null;
                AddChildren(true, data);
            }
            else if (previous != null)
            {
                previous.Parent.Children.Delete(previous);
                var data = ((BPTreeInternalNode)previous).RemoveChildren(true, previous.Size);
                previous.Parent = null;
                AddChildren(false, data);
            }
            else
            {
                //it's root node
                Parent = null;
                Store();
                IsModified = true;
                if (Size == 1)
                {
                    if (Tree != null)
                    {
                        Children[0].Parent = null;
                        Tree.Root = Children[0];
                    }
                }
            }
        }

        private bool ShouldStoreNode()
        {
            for (int i = 0; i < Size; i++)
            {
                if (Children[i].IsModified)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}
