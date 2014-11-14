using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Practice.Algorithm.Tree.BPlusTree;

namespace PracticeUnitTest.BPlusTree
{
    [TestClass]
    public class BPTreeTest
    {
        [TestMethod]
        public void Test()
        {
            BPTree tree = new BPTree(3);
            int[] values = GenerateIntArray(1000);
            for (int i = 0; i < values.Count(); i++)
            {
                tree.Add(values[i].ToString("0000"), values[i].ToString("0000"));
            }
            
            var list = values.ToList();
            list.Sort();

            Verify(list, tree);

            for (int i = 0; i < values.Count(); i++)
            {
                var rdn = new Random(Guid.NewGuid().GetHashCode()).Next(0, list.Count);
                int key = list[rdn];
                var value = tree.Delete(key.ToString("0000"));
                Assert.AreEqual(value, true);
                list.Remove(key);
               
                Verify(list,tree);
            }

            //add again
            values = GenerateIntArray(1000);
            for (int i = 0; i < values.Count(); i++)
            {
                tree.Add(values[i].ToString("0000"), values[i].ToString("0000"));
            }

            list = values.ToList();
            list.Sort();

            Verify(list, tree);
        }

        private int[] GenerateIntArray(int count)
        {
            var arr=new List<int>();
            for (int i = 0; i < count; i++)
            {
                var rdn = new Random(Guid.NewGuid().GetHashCode()).Next(0, 9999);
                arr.Add(rdn);
            }

            return arr.ToArray();
        }

        private void Verify(IList<int> list, BPTree tree)
        {
            IList<string> result = new List<string>();
            foreach (BPTreeNodeData item in tree.DataSet)
            {
                result.Add(item.Value);
            }

            var arr = result.Select(s => int.Parse(s));
            string actual = string.Join(",", arr);

            string expect = string.Join(",", list.ToArray());

            Assert.AreEqual(expect.Length, actual.Length);
            Assert.AreEqual(expect, actual);
        }

    }
}
