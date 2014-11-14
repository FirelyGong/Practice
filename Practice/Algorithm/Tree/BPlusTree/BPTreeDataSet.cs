using System.Collections;

namespace Practice.Algorithm.Tree.BPlusTree
{
    public class BPTreeDataSet : IEnumerable
    {
        private readonly BPTree _tree;

        public BPTreeDataSet(BPTree tree)
        {
            _tree = tree;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new BPTreeDataEnumerator(_tree);
        }
    }
}
