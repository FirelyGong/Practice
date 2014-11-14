using System.Linq;

namespace Practice.Algorithm.Tree.BTree
{
    public class SimpleBTree<T>
    {
        private int _nodeMinSize;

        public SimpleBTree(int nodeMinSize)
        {
            _nodeMinSize = nodeMinSize;
        } 

        public BTreeNode<T> RootNode { get; private set; }

        public bool Add(long key, T value)
        {
            if (RootNode == null)
            {
                RootNode = new BTreeNode<T>(_nodeMinSize);
            }

            BTreeNode<T> node = RootNode.AddElement(key, value);
            if (node != null)
            {
                CheckStatus(node);
                return true;
            }

            return false;
        }

        public bool Delete(long key)
        {
            BTreeNode<T> node = RootNode.DeleteElement(key);
            if (node != null)
            {
                CheckStatus(node);
                return true;
            }

            return false;
        }

        public T Find(long key)
        {
            T[] result = RootNode.FindElement(key, key);
            if (result.Any())
            {
                return result[0];
            }

            return default (T);
        }
        
        public T[] Find(long minKey, long maxKey)
        {
            if (minKey > maxKey)
            {
                return new T[] { };
            }

            return RootNode.FindElement(minKey, maxKey);
        }

        private void CheckStatus(BTreeNode<T> node)
        {
            if (node.Status == BTreeNodeStatus.Overflow)
            {
                SplitNode(node);
            }
            
            if(node.Status== BTreeNodeStatus.Underflow)
            {
                MergeNode(node);
            }
        }

        private void SplitNode(BTreeNode<T> node)
        {
            BTreeNode<T> newNode = new BTreeNode<T>(_nodeMinSize);
            int loop = node.Size - _nodeMinSize - 1;
            for (int i = 0; i < loop; i++)
            {
                newNode.Elements.Add(node.Elements[_nodeMinSize + 1]);
                node.Elements.RemoveAt(_nodeMinSize + 1);
            }

            for (int i = 0; i < loop + 1; i++)
            {
                if (node.Children(_nodeMinSize + 1) != null)
                {
                    newNode.AddChild(node.Children(_nodeMinSize + 1));
                    node.RemoveChild(_nodeMinSize + 1);
                }
            }

            if (node.Parent == null)
            {
                RootNode = new BTreeNode<T>(_nodeMinSize);
                RootNode.AddChild(node);
                node.Parent.Elements.Add(node.Elements[_nodeMinSize]);
                node.Parent.AddChild(newNode);
            }
            else
            {
                node.Parent.Elements.Insert(node.Index, node.Elements[_nodeMinSize]);
                node.Parent.AddChild(node.Index + 1, newNode);
            }

            node.Elements.RemoveAt(_nodeMinSize);
            CheckStatus(node.Parent);
        }

        private void MergeNode(BTreeNode<T> node)
        {
            int index = node.Index;

            BTreeNode<T> leftBrother = node.Parent.Children(index - 1);
            BTreeNode<T> rightBrother = node.Parent.Children(index + 1);

            if (leftBrother == null)
            {
                if (rightBrother.Status == BTreeNodeStatus.Full)
                {
                    //get a element from right brother
                    GetElementFromRightBrother(node);
                }
                else
                {
                    //merge with right brother
                    MergeWithRightBrother(node);
                    CheckStatus(node.Parent);
                }
            }
            else if (leftBrother.Status == BTreeNodeStatus.Full)
            {
                //get a element from left brother
                GetElementFromLeftBrother(node);
            }
            else if (rightBrother != null && rightBrother.Status == BTreeNodeStatus.Full)
            {
                //get a element from right brother
                GetElementFromRightBrother(node);
            }
            else
            {
                //merge with left brother
                MergeWithLeftBrother(node);
                CheckStatus(node.Parent);
            }
        }

        private void GetElementFromRightBrother(BTreeNode<T> node)
        {
            int index = node.Index;

            BTreeNode<T> rightBrotherMinChild = node.Parent.Children(index + 1).GetMinChild();
            BTreeNodeElement<T> rightElement = node.Parent.Elements[index];
            node.Parent.Elements.Remove(rightElement);
            node.Elements.Add(rightElement);
            node.Parent.Elements.Insert(index, rightBrotherMinChild.Elements[0]);
            rightBrotherMinChild.Elements.RemoveAt(0);
        }

        private void GetElementFromLeftBrother(BTreeNode<T> node)
        {
            int index = node.Index;

            BTreeNode<T> leftBrotherMaxChild = node.Parent.Children(index - 1).GetMaxChild();
            BTreeNodeElement<T> leftElement = node.Parent.Elements[index - 1];
            node.Parent.Elements.Remove(leftElement);
            node.Elements.Insert(0, leftElement);
            node.Parent.Elements.Insert(index - 1, leftBrotherMaxChild.Elements.Last());
            leftBrotherMaxChild.Elements.Remove(leftBrotherMaxChild.Elements.Last());
        }

        private void MergeWithLeftBrother(BTreeNode<T> node)
        {
            int index = node.Index;

            BTreeNode<T> leftBrother = node.Parent.Children(index - 1);
            BTreeNodeElement<T> leftElement = node.Parent.Elements[index - 1];
            leftBrother.Elements.Add(leftElement);
            int loop = node.Size;
            for (int i = 0; i < loop; i++)
            {
                leftBrother.Elements.Add(node.Elements[i]);
            }

            for (int i = 0; i < loop + 1; i++)
            {
                if (node.Children(i) != null)
                {
                    leftBrother.AddChild(node.Children(i));
                }
            }

            node.Parent.Elements.Remove(leftElement);
            node.Parent.RemoveChild(index);
            UpdateDeepth(leftBrother);
        }

        private void MergeWithRightBrother(BTreeNode<T> node)
        {
            int index = node.Index;

            BTreeNode<T> rightBrother = node.Parent.Children(index + 1);
            BTreeNodeElement<T> rightElement = node.Parent.Elements[index];
            node.Elements.Add(rightElement);
            int loop = rightBrother.Size;
            for (int i = 0; i < loop; i++)
            {
                node.Elements.Add(rightBrother.Elements[i]);
            }

            for (int i = 0; i < loop + 1; i++)
            {
                if (rightBrother.Children(i) != null)
                {
                    node.AddChild(rightBrother.Children(i));
                }
            }

            node.Parent.Elements.Remove(rightElement);
            node.Parent.RemoveChild(index + 1);
            UpdateDeepth(node);
        }

        private void UpdateDeepth(BTreeNode<T> node)
        {
            if (node.Parent.Size == 0)
            {
                node.SetParent(null);
                RootNode = node;
            }
        }
    }
}
