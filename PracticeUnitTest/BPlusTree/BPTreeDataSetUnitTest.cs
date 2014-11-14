using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Practice.Algorithm.Tree.BPlusTree;

namespace PracticeUnitTest.BPlusTree
{
    /// <summary>
    /// Summary description for BPTreeDataSetUnitTest
    /// </summary>
    [TestClass]
    public class BPTreeDataSetUnitTest
    {
        [TestMethod]
        public void ForeachTest()
        {
            BPTree tree = new BPTree(2);
            tree.Add("11", "11");
            tree.Add("66", "66");
            tree.Add("77", "77");
            tree.Add("33", "33");
            tree.Add("55", "55");
            tree.Add("44", "44");
            tree.Add("99", "99");
            tree.Add("88", "88");
            tree.Add("22", "22");
            tree.Add("57", "57");
            tree.Add("18", "18");
            tree.Add("41", "41");
            tree.Add("90", "90");

            IList<string> result = new List<string>();
            foreach (BPTreeNodeData d in tree.DataSet)
            {
                result.Add((d.Value));
            }

            Assert.AreEqual("11,18,22,33,41,44,55,57,66,77,88,90,99", string.Join(",", result.ToArray()));
        }
    }
}
