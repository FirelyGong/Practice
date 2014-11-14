using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Practice.Algorithm.Tree.BPlusTree.Exceptions;

namespace Practice.Algorithm.Tree.BPlusTree
{
    public class BPTree
    {
        private readonly short _minSize;

        private BPTreeNode _root;

        public BPTree(short minSize, INodePersistence nodeStore = null)
        {
            _minSize = minSize;

            NodePersist = nodeStore;
            var rootNodeId = 0;
            var rootNodeType = BPTreeNodeType.Leaf;
            if (nodeStore != null)
            {
                string rootInfo = nodeStore.ReadRootInfo();
                if (!string.IsNullOrEmpty(rootInfo))
                {
                    string[] arr = rootInfo.Split('-');
                    int.TryParse(arr[1], out rootNodeId);
                    byte tpe = 0;
                    byte.TryParse(arr[0], out tpe);
                    rootNodeType = (BPTreeNodeType)tpe;
                }
                else
                {
                    rootNodeId = nodeStore.ObtainNodeId();
                }
            }

            InitialRootNode(rootNodeId, rootNodeType, minSize);
            DataSet = new BPTreeDataSet(this);
            //Updater = new BPTreeUpdater(this);

            //Console.Out.WriteLine("load node start: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
            //LoadNode(RootBranch.Node);
            //Console.Out.WriteLine("load node end: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + "[" + _nodes + "]");
        }
        
        public INodePersistence NodePersist { get; private set; }

        public BPTreeNode Root
        {
            get
            {
                return _root;
            }

            internal set
            {
                _root = value;
                if (NodePersist != null)
                {
                    NodePersist.SaveRootInfo(string.Concat(((byte)_root.NodeType).ToString(CultureInfo.InvariantCulture), "-", _root.NodeId.ToString(CultureInfo.InvariantCulture)));
                }
            }
        }

        //public BPTreeUpdater Updater { get; private set; }

        public BPTreeDataSet DataSet { get; private set; }

        public bool Add(string key, string value)
        {
            //Console.Out.WriteLine("add start: "+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
            var result = Root.AddElement(key, value);
            //var node = result.CreatedNode;
            ////Console.Out.WriteLine("add end: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
            //if (node != null)
            //{
            //    BPTreeNode old = Root;
            //    Root = new BPTreeInternalNode(null, this, _rootNodeId, _minSize);
            //    Root.Children.Add(old);
            //    int index = Root.Children.FindIndex(node);
            //    if (index < 0)
            //    {
            //        index = ~index;
            //    }

            //    Root.Children.Insert(index, node);
            //}

            return result;
        }

        public bool Delete(string key, out string value)
        {
            var result = Root.DeleteElement(key, out value);
            return result;
        }

        public bool Delete(string key)
        {
            string value;
            var result = Delete(key, out value);
            //var node = result.MergedNode;
            //if (node != null)
            //{
            //    if (node == Root)
            //    {
            //        if (node.Size > 1)
            //        {
            //            throw new BPTreeInvalidNodeException("Root node should be merged but the size>1!");
            //        }

            //        if (node.NodeType == BPTreeNodeType.Internal)
            //        {
            //            Root = node.Children.First();
            //        }
            //    }
            //    else
            //    {
            //        if (node.Next != null)
            //        {
            //            Root = node.Next;
            //        }
            //        else
            //        {
            //            Root = node.Previous;
            //        }
            //    }
            //}

            return result;// value != null;
        }

        public string Find(string key)
        {
            var result = Root.FindElement(key);
            if (result != null)
            {
                return result;
            }

            return string.Empty;
        }

        public string[] Find(string minKey, string maxKey)
        {
            if (Root.Size == 0)
            {
                return new string[] { };
            }

            IList<string> lst = Root.FindElements(minKey, maxKey);

            return lst.ToArray();
        }

        public bool Commit()
        {
            //int commitTempChanges = Updater.CommitAll();
            //return commitTempChanges > 0;
            return false;
        }

        //public void MaintenanceTree(string triggerBranch)
        //{
        //    BPTreeNodeHelper.MaintenanceNode(Root, triggerBranch);
        //}
        
        private void InitialRootNode(int rootNodeId, BPTreeNodeType nodeType, short minSize)
        {
            if (nodeType == BPTreeNodeType.Internal)
            {
                _root = new BPTreeInternalNode(null, this, rootNodeId, minSize);
            }
            else
            {
                _root = new BPTreeLeafNode(null, this, rootNodeId, minSize);
            }
        }

        //private void LoadNode(BPTreeNode node)
        //{
        //    _nodes++;
        //    if (node.NodeType == BPTreeNodeType.Internal)
        //    {
        //        BPTreeBranchCollection branches = ((BPTreeInternalNode)node).Branches;
        //        for (int i = 0; i < branches.Count; i++)
        //        {
        //            if (branches[i].NodeType == BPTreeNodeType.Internal)
        //            {
        //                LoadNode(branches[i].Node);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //node.Load(this);
        //        //_nodes += node.Size;
        //    }
        //}
    }
}
