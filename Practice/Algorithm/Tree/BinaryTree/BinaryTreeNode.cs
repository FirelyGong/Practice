namespace Practice.Algorithm.Tree.BinaryTree
{
    public class BinaryTreeNode<T>
    {
        public BinaryTreeNode(long key, T value)
        {
            NodeKey = key;
            NodeValue = value;
            LeftChild = null;
            RightChild = null;
            Parent = null;
            Size = 0;
        }

        public long NodeKey { get; set; }
        public T NodeValue { get; set; }
        public BinaryTreeNode<T> LeftChild { get; private set; }
        public BinaryTreeNode<T> RightChild { get; private set; }
        public BinaryTreeNode<T> Parent { get; private set; }
        public int Size { get; set; }

        public bool IsLeaf
        {
            get
            {
                return LeftChild == null && RightChild == null;
            }
        }

        public bool IsRoot
        {
            get
            {
                return Parent == null;
            }
        }

        public bool IsLeftChild
        {
            get
            {
                return Parent != null && NodeKey < Parent.NodeKey;
            }
        }

        public bool IsRightChild
        {
            get
            {
                return Parent != null && NodeKey > Parent.NodeKey;
            }
        }

        public void AddChild(BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            if (node.NodeKey > NodeKey)
            {
                RemoveRightChild();
                AddRightChild(node);
            }
            else if (node.NodeKey < NodeKey)
            {
                RemoveLeftChild();
                AddLeftChild(node);
            }
        }

        public void RemoveChild(BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            if (node == LeftChild)
            {
                RemoveLeftChild();
            }
            else if (node == RightChild)
            {
                RemoveRightChild();
            }
        }

        private void AddLeftChild(BinaryTreeNode<T> node)
        {
            LeftChild = node;
            node.Parent = this;
        }

        private void AddRightChild(BinaryTreeNode<T> node)
        {
            RightChild = node;
            node.Parent = this;
        }

        private void RemoveLeftChild()
        {
            if (LeftChild != null)
            {
                if (LeftChild.Parent == this)
                {
                    LeftChild.Parent = null;
                }
                LeftChild = null;
            }
        }

        private void RemoveRightChild()
        {
            if (RightChild != null)
            {
                if (RightChild.Parent == this)
                {
                    RightChild.Parent = null;
                }
                RightChild = null;
            }
        }
    }
}
