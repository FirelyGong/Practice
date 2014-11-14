using System.Collections;
using System.Collections.Generic;

namespace Practice.Algorithm.Tree.BPlusTree
{
    public class BPTreeDataEnumerator : IEnumerator<BPTreeNodeData>
    {
        private readonly BPTree _bpTree;

        private BPTreeLeafNode _currentNode;

        private int _currentElementIndex;

        public BPTreeDataEnumerator(BPTree tree)
        {
            _bpTree = tree;
            Reset();
        }

        public BPTreeNodeData Current
        {
            get
            {
                return _currentNode.Elements[_currentElementIndex];
            }
        }

        public void Dispose()
        {
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public bool MoveNext()
        {
            _currentElementIndex++;
            if (_currentElementIndex < _currentNode.Size)
            {
                return true;
            }
            
            if(_currentNode.Next()!=null)
            {
                _currentElementIndex = 0;
                _currentNode = (BPTreeLeafNode)_currentNode.Next();
                return true;
            }

            return false;
        }

        public void Reset()
        {
            _currentElementIndex = -1;
            _currentNode = GetFirstLeafNode(_bpTree);
        }

        private BPTreeLeafNode GetFirstLeafNode(BPTree tree)
        {
            var node = tree.Root;
            while (node.NodeType == BPTreeNodeType.Internal)
            {
                node = node.Children.First();
            }

            return (BPTreeLeafNode)node;
        }
    }
}
