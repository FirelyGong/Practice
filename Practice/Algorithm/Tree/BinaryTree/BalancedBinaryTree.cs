
namespace Practice.Algorithm.Tree.BinaryTree
{
    public class BalancedBinaryTree<T>:SimpleBinaryTree<T>
    {
        protected override void KeepBalance(BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            int sizeLeft = -1;
            int sizeRight = -1;
            int sizeLeftLeft = -1;
            int sizeLeftRight = -1;
            int sizeRightLeft = -1;
            int sizeRightRight = -1;
            if (node.LeftChild != null)
            {
                sizeLeft = node.LeftChild.Size;
                if (node.LeftChild.LeftChild != null)
                {
                    sizeLeftLeft = node.LeftChild.LeftChild.Size;
                }
                if (node.LeftChild.RightChild != null)
                {
                    sizeLeftRight = node.LeftChild.RightChild.Size;
                }
            }
            if (node.RightChild != null)
            {
                sizeRight = node.RightChild.Size;
                if (node.RightChild.LeftChild != null)
                {
                    sizeRightLeft = node.RightChild.LeftChild.Size;
                }
                if (node.RightChild.RightChild != null)
                {
                    sizeRightRight = node.RightChild.RightChild.Size;
                }
            }

            if (sizeLeftLeft > sizeRight)
            {
                RightRotate(node);
                KeepBalance(node.RightChild);
                KeepBalance(node);
            }
            else if (sizeLeftRight > sizeRight)
            {
                LeftRotate(node.LeftChild);
                RightRotate(node);
                KeepBalance(node.LeftChild);
                KeepBalance(node.RightChild);
            }
            else if (sizeRightLeft > sizeLeft)
            {
                RightRotate(node.RightChild);
                LeftRotate(node);
                KeepBalance(node.LeftChild);
                KeepBalance(node.RightChild);
            }
            else if(sizeRightRight>sizeLeft)
            {
                LeftRotate(node);
                KeepBalance(node.LeftChild);
                KeepBalance(node);
            }
        }

        private void RightRotate(BinaryTreeNode<T> node)
        {
            if (node.LeftChild == null)
            {
                return;
            }

            var parent = node.Parent;
            var select = node.LeftChild;
            node.RemoveChild(node.LeftChild);
            select.Size = node.Size;
            node.AddChild(select.RightChild);
            select.AddChild(node);
            node.Size = 0;
            if (node.RightChild != null)
            {
                node.Size += node.RightChild.Size + 1;
            }
            if (node.LeftChild != null)
            {
                node.Size += node.LeftChild.Size + 1;
            }

            if (parent==null)
            {
                RootNode = select;
            }
            else
            {
                parent.AddChild(select);
            }
        }

        private void LeftRotate(BinaryTreeNode<T> node)
        {
            if (node.RightChild == null)
            {
                return;
            }

            var parent = node.Parent;
            var select = node.RightChild;
            node.RemoveChild(node.RightChild);
            select.Size = node.Size;
            node.AddChild(select.LeftChild);
            select.AddChild(node);
            node.Size = 0;
            if (node.RightChild != null)
            {
                node.Size += node.RightChild.Size + 1;
            }
            if (node.LeftChild != null)
            {
                node.Size += node.LeftChild.Size + 1;
            }

            if (parent == null)
            {
                RootNode = select;
            }
            else
            {
                parent.AddChild(select);
            }
        }
    }
}
