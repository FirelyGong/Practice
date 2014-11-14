using System.Collections.Generic;
using System.Linq;

namespace Practice.Algorithm.Tree.BinaryTree
{
    public class SimpleBinaryTree<T>
    {
        public BinaryTreeNode<T> RootNode { get; protected set; }

        public bool AddNode(T value, long key)
        {
            if (RootNode == null)
            {
                RootNode = new BinaryTreeNode<T>(key, value);

                return true;
            }

            bool bln = AddNode(value, key, RootNode);
            return bln;
        }

        public bool DeleteNode(long key)
        {
            return DeleteNode(key, RootNode);
        }

        public T Find(long key)
        {
            BinaryTreeNode<T> node = FindNode(key, RootNode);
            if (node == null)
            {
                return default(T);
            }

            return node.NodeValue;
        }

        public T[] GetAll()
        {
            IList<T> result = new List<T>();
            GetAll(result, RootNode);

            return result.ToArray();
        }

        protected virtual void KeepBalance(BinaryTreeNode<T> node)
        {
        }

        private bool DeleteNode(long key, BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                return false;
            }
            
            bool bln;
            if (node.NodeKey == key)
            {
                DeleteNode(node);
                bln = true;
            }
            else if (node.NodeKey > key)
            {
                bln = DeleteNode(key, node.LeftChild);
            }
            else
            {
                bln = DeleteNode(key, node.RightChild);
            }

            if (bln)
            {
                if (node.Parent != null)
                {
                    KeepBalance(node.Parent);
                }
                else
                {
                    KeepBalance(RootNode);
                }
            }

            return bln;
        }

        private void DeleteNode(BinaryTreeNode<T> node)
        {
            BinaryTreeNode<T> newRoot = null;
            if (node.IsRoot)
            {
                newRoot = new BinaryTreeNode<T>(long.MinValue, node.NodeValue);
                newRoot.AddChild(node);
            }
            else
            {
                node.Parent.Size--;
            }

            if (node.IsLeaf)
            {
                node.Parent.RemoveChild(node);
            }
            else
            {
                if (node.RightChild == null)
                {
                    node.Parent.AddChild(node.LeftChild);
                }
                else
                {
                    node.Parent.AddChild(node.RightChild);
                    
                    if (node.LeftChild != null)
                    {
                        var select = node.RightChild;
                        select.Size += node.LeftChild.Size + 1;
                        while (select.LeftChild != null)
                        {
                            select = select.LeftChild;
                            select.Size += node.LeftChild.Size + 1;
                        }

                        select.AddChild(node.LeftChild);
                    }
                }
            }

            if (newRoot!=null)
            {
                RootNode = newRoot.RightChild;
                newRoot.RemoveChild(RootNode);
            }
        }

        private void GetAll(IList<T> result, BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            if (node.LeftChild != null)
            {
                GetAll(result, node.LeftChild);
            }

            result.Add(node.NodeValue);

            if (node.RightChild != null)
            {
                GetAll(result, node.RightChild);
            }
        }

        private BinaryTreeNode<T> FindNode(long key, BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                return null;
            }

            if (key == node.NodeKey)
            {
                return node;
            }

            if (key > node.NodeKey)
            {
                return FindNode(key, node.RightChild);
            }

            return FindNode(key, node.LeftChild);
        }

        private bool AddNode(T value, long key, BinaryTreeNode<T> node)
        {
            if (key < node.NodeKey)
            {
                if (node.LeftChild == null)
                {
                    node.AddChild(new BinaryTreeNode<T>(key, value));
                    node.Size++;
                    return true;
                }

                bool bln = AddNode(value, key, node.LeftChild);
                if (bln)
                {
                    node.Size++;
                    KeepBalance(node);
                }

                return bln;
            }

            if (key > node.NodeKey)
            {
                if (node.RightChild == null)
                {
                    node.AddChild(new BinaryTreeNode<T>(key, value));
                    node.Size++;
                    return true;
                }

                bool bln = AddNode(value, key, node.RightChild);
                if (bln)
                {
                    node.Size++;
                    KeepBalance(node);
                }

                return bln;
            }

            return false;
        }
    }
}
