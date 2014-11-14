using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Practice.Algorithm.Tree.BPlusTree;

namespace PracticeUnitTest.BPlusTree
{
    /// <summary>
    /// Summary description for BPTreeLeafNodeUnitTest
    /// </summary>
    [TestClass]
    public class BPTreeLeafNodeUnitTest
    {
        private BPTreeLeafNode _leafNode;

        [TestInitialize]
        public void TestPrepare()
        {
            _leafNode = new BPTreeLeafNode(null, null, 0, 2);
            _leafNode.AddElement("33", "33");
            _leafNode.AddElement("55", "55");
            _leafNode.AddElement("44", "44");
        }

        [TestMethod]
        public void LeafNodeAdd_basical()
        {
            var leafNode = new BPTreeLeafNode(null, null, 0, 2);
            leafNode.AddElement("33", "33");
            leafNode.AddElement("55", "55");
            leafNode.AddElement("44", "44");

            Assert.AreEqual("33", leafNode.Elements[0].Key);
            Assert.AreEqual("44", leafNode.Elements[1].Key);
            Assert.AreEqual("55", leafNode.Elements[2].Key);
        }

        [TestMethod]
        public void LeafNodeAdd_Split()
        {
            var leafNode = new BPTreeLeafNode(null, null, 0, 2);
            var node1 = leafNode.AddElement("33", "33");
            var node2 = leafNode.AddElement("55", "55");
            var node3 = leafNode.AddElement("44", "44");
            var node4 = leafNode.AddElement("06", "06");
            var node5 = leafNode.AddElement("25", "25");
            var node6 = leafNode.Parent.AddElement("30", "30");

            Assert.AreEqual(true, node1);
            Assert.AreEqual(true, node2);
            Assert.AreEqual(true, node3);
            Assert.AreEqual(true, node4);
            Assert.AreEqual(true, node5);
            Assert.AreEqual(true, node6);

            Assert.AreEqual("44,55", leafNode.Next().ToString());
            Assert.AreEqual(4, leafNode.Size);
            Assert.AreEqual(2, leafNode.Next().Size);
            Assert.AreEqual("06", leafNode.Key);
            Assert.AreEqual("44", leafNode.Next().Key);
        }

        [TestMethod]
        public void LeafNodeAdd_Move_Next()
        {
            var leafNode = new BPTreeLeafNode(null, null, 0, 2);
            var node1 = leafNode.AddElement("33", "33");
            var node2 = leafNode.AddElement("55", "55");
            var node3 = leafNode.AddElement("44", "44");
            var node4 = leafNode.AddElement("06", "06");
            var node5 = leafNode.AddElement("25", "25");
            var node6 = leafNode.Parent.AddElement("30", "30");
            var node7 = leafNode.Parent.AddElement("10", "10");

            Assert.AreEqual(true, node1);
            Assert.AreEqual(true, node2);
            Assert.AreEqual(true, node3);
            Assert.AreEqual(true, node4);
            Assert.AreEqual(true, node5);
            Assert.AreEqual(true, node6);
            Assert.AreEqual(true, node7);

            Assert.AreEqual(4, leafNode.Size);
            Assert.AreEqual(3, leafNode.Next().Size);
            Assert.AreEqual("06", leafNode.Key);
            Assert.AreEqual("33", leafNode.Next().Key);
        }

        [TestMethod]
        public void LeafNodeAdd_Move_Previous()
        {
            var leafNode = new BPTreeLeafNode(null, null, 0, 2);
            var node1 = leafNode.AddElement("33", "33");
            var node2 = leafNode.AddElement("55", "55");
            var node3 = leafNode.AddElement("44", "44");
            var node4 = leafNode.AddElement("06", "06");
            var node5 = leafNode.AddElement("25", "25");
            var node6 = leafNode.Parent.AddElement("60", "60");
            var node7 = leafNode.Parent.AddElement("70", "70");
            var node8 = leafNode.Parent.AddElement("47", "47");

            Assert.AreEqual(true, node1);
            Assert.AreEqual(true, node2);
            Assert.AreEqual(true, node3);
            Assert.AreEqual(true, node4);
            Assert.AreEqual(true, node5);
            Assert.AreEqual(true, node6);
            Assert.AreEqual(true, node7);
            Assert.AreEqual(true, node8);

            Assert.AreEqual("06", leafNode.Key);
            Assert.AreEqual("47", leafNode.Next().Key);
            Assert.AreEqual(4, leafNode.Size);
            Assert.AreEqual(4, leafNode.Next().Size);
        }

        [TestMethod]
        public void LeafNodeDelete_NotExist()
        {
            string delete;
            var node1 = _leafNode.DeleteElement("51", out delete);

            Assert.AreEqual(false, node1);
            Assert.AreEqual(null, delete);
        }

        [TestMethod]
        public void LeafNodeDelete_Basical()
        {
            var leafNode = new BPTreeLeafNode(null, null, 0, 2);
            var node1 = leafNode.AddElement("33", "33");
            var node2 = leafNode.AddElement("55", "55");
            var node3 = leafNode.AddElement("44", "44");
            string delete;
            var node4 = leafNode.DeleteElement("55", out delete);

            Assert.AreEqual(true, node1);
            Assert.AreEqual(true, node2);
            Assert.AreEqual(true, node3);
            Assert.AreEqual(true, node4);
            Assert.AreEqual("55", delete);
            Assert.AreEqual(2, leafNode.Size);
        }

        [TestMethod]
        public void LeafNodeDelete_Move_Next()
        {
            var leafNode = new BPTreeLeafNode(null, null, 0, 2);
            var node1 = leafNode.AddElement("33", "33");
            var node2 = leafNode.AddElement("55", "55");
            var node3 = leafNode.AddElement("44", "44");
            var node4 = leafNode.AddElement("06", "06");
            var node5 = leafNode.AddElement("25", "25");
            var node6 = leafNode.Parent.AddElement("60", "60");
            string delete;
            var node7 = leafNode.DeleteElement("25", out delete);
            Assert.AreEqual("25", delete);
            var node8 = leafNode.DeleteElement("06", out delete);
            Assert.AreEqual("06", delete);

            Assert.AreEqual(true, node1);
            Assert.AreEqual(true, node2);
            Assert.AreEqual(true, node3);
            Assert.AreEqual(true, node4);
            Assert.AreEqual(true, node5);
            Assert.AreEqual(true, node6);
            Assert.AreEqual(true, node7);
            Assert.AreEqual(true, node8);

            Assert.AreEqual(2, leafNode.Size);
            Assert.AreEqual(2, leafNode.Next().Size);
            Assert.AreEqual("33", leafNode.Key);
            Assert.AreEqual("55", leafNode.Next().Key);
        }

        [TestMethod]
        public void LeafNodeDelete_Move_Previous()
        {
            var leafNode = new BPTreeLeafNode(null,null,0, 2);
            var node1 = leafNode.AddElement("33", "33");
            var node2 = leafNode.AddElement("55", "55");
            var node3 = leafNode.AddElement("44", "44");
            var node4 = leafNode.AddElement("06", "06");
            var node5 = leafNode.AddElement("25", "25");
            string delete;
            var node6 = leafNode.Parent.DeleteElement("44", out delete);
            Assert.AreEqual("44", delete);

            Assert.AreEqual(true, node1);
            Assert.AreEqual(true, node2);
            Assert.AreEqual(true, node3);
            Assert.AreEqual(true, node4);
            Assert.AreEqual(true, node5);
            Assert.AreEqual(true, node6);

            Assert.AreEqual(2, leafNode.Size);
            Assert.AreEqual(2, leafNode.Next().Size);
            Assert.AreEqual("06", leafNode.Key);
            Assert.AreEqual("33", leafNode.Next().Key);
        }

        [TestMethod]
        public void LeafNodeDelete_Merge_Next()
        {
            var leafNode = new BPTreeLeafNode(null, null, 0, 2);
            var node1 = leafNode.AddElement("33", "33");
            var node2 = leafNode.AddElement("55", "55");
            var node3 = leafNode.AddElement("44", "44");
            var node4 = leafNode.AddElement("06", "06");
            var node5 = leafNode.AddElement("25", "25");
            string delete;
            var node6 = leafNode.DeleteElement("25", out delete);
            Assert.AreEqual("25", delete);
            var node7 = leafNode.DeleteElement("06", out delete);
            Assert.AreEqual("06", delete);

            Assert.AreEqual(true, node1);
            Assert.AreEqual(true, node2);
            Assert.AreEqual(true, node3);
            Assert.AreEqual(true, node4);
            Assert.AreEqual(true, node5);
            Assert.AreEqual(true, node6);
            Assert.AreEqual(true, node7);

            Assert.AreEqual(3, leafNode.Size);
            Assert.AreEqual(null, leafNode.Next());
            Assert.AreEqual("33", leafNode.Key);
        }

        [TestMethod]
        public void LeafNodeDelete_Merge_Previous()
        {
            var leafNode = new BPTreeLeafNode(null, null, 0, 2);
            var node1 = leafNode.AddElement("33", "33");
            var node2 = leafNode.AddElement("55", "55");
            var node3 = leafNode.AddElement("44", "44");
            var node4 = leafNode.AddElement("06", "06");
            var node5 = leafNode.AddElement("25", "25");
            var next = leafNode.Next();
            string delete;
            var node6 = leafNode.Parent.DeleteElement("55", out delete);
            Assert.AreEqual("55", delete);
            var node7 = leafNode.Parent.DeleteElement("44", out delete);
            Assert.AreEqual("44", delete);

            Assert.AreEqual(true, node1);
            Assert.AreEqual(true, node2);
            Assert.AreEqual(true, node3);
            Assert.AreEqual(true, node4);
            Assert.AreEqual(true, node5);
            Assert.AreEqual(true, node6);
            Assert.AreEqual(true, node7);

            Assert.AreEqual(0, leafNode.Size);
            Assert.AreEqual(null, leafNode.Next());
            Assert.AreEqual(null, leafNode.Previous());
            Assert.AreEqual("06", next.Key);
        }

        [TestMethod]
        public void LeafNodeLink_Internal()
        {
            var leafNode = new BPTreeLeafNode(null, null, 0, 2);
            var node1 = leafNode.AddElement("33", "33");
            var node2 = leafNode.AddElement("55", "55");
            var node3 = leafNode.AddElement("44", "44");
            var node4 = leafNode.AddElement("06", "06");
            var node5 = leafNode.AddElement("25", "25");
            var node6 = leafNode.Parent.AddElement("60", "60");
            var node7 = leafNode.Parent.AddElement("70", "70");
            var node8 = leafNode.Parent.AddElement("47", "47");
            var node9 = leafNode.Parent.AddElement("10", "10");

            //result: leafNode(06,10,25)->node9(33,44)->node5(47,55,60,70)

            Assert.AreEqual(true, node1);
            Assert.AreEqual(true, node2);
            Assert.AreEqual(true, node3);
            Assert.AreEqual(true, node4);
            Assert.AreEqual(true, node5);
            Assert.AreEqual(true, node6);
            Assert.AreEqual(true, node7);
            Assert.AreEqual(true, node8);
            Assert.AreEqual(true, node9);

            Assert.AreEqual("33,44", leafNode.Next().ToString());
            Assert.AreEqual("47,55,60,70", leafNode.Next().Next().ToString());
            Assert.AreEqual(null, leafNode.Previous());
            string delete;
            var node10 = leafNode.Parent.DeleteElement("33", out delete);
            Assert.AreEqual("33", delete);
            Assert.AreEqual(true, node10);
            Assert.AreEqual("25", leafNode.Next().Key);
            Assert.AreEqual(4, leafNode.Next().Next().Size);
            Assert.AreEqual(2, leafNode.Size);
            var node11 = leafNode.Parent.DeleteElement("06", out delete);
            Assert.AreEqual("06", delete);
            Assert.AreEqual(true, node11);
            var node12 = leafNode.Parent.DeleteElement("10", out delete);
            Assert.AreEqual("10", delete);

            //result: leafNode(25,44)->node5(47,55,60,70) node9()

            Assert.AreEqual("47,55,60,70", leafNode.Next().ToString());
            Assert.AreEqual("25,44", leafNode.ToString());
            Assert.AreEqual(null, leafNode.Next().Next());
            Assert.AreEqual(null, leafNode.Previous());
        }

        [TestMethod]
        public void LeafNodeFind_Exist()
        {
            var value = _leafNode.FindElement("33");
            Assert.AreEqual("33", value);
        }

        [TestMethod]
        public void LeafNodeFind_NotExist()
        {
            var value = _leafNode.FindElement("30");
            Assert.AreEqual(null, value);
        }

        [TestMethod]
        public void LeafNodeFindElements_Error()
        {
            var value = _leafNode.FindElements("33", "12");
            Assert.AreEqual("", string.Join(",", value.ToArray()));
        }

        [TestMethod]
        public void LeafNodeFindElements_Normal1()
        {
            var value = _leafNode.FindElements("11", "33");
            Assert.AreEqual("33", string.Join(",", value.ToArray()));
        }

        [TestMethod]
        public void LeafNodeFindElements_Normal2()
        {
            var value = _leafNode.FindElements("33", "33");
            Assert.AreEqual("33", string.Join(",", value.ToArray()));
        }

        [TestMethod]
        public void LeafNodeFindElements_Normal3()
        {
            var value = _leafNode.FindElements("33", "45");
            Assert.AreEqual("33,44", string.Join(",", value.ToArray()));
        }

        [TestMethod]
        public void LeafNodeFindElements_Normal4()
        {
            var value = _leafNode.FindElements("33", "55");
            Assert.AreEqual("33,44,55", string.Join(",", value.ToArray()));
        }

        [TestMethod]
        public void LeafNodeFindElements_Normal5()
        {
            var value = _leafNode.FindElements("33", "99");
            Assert.AreEqual("33,44,55", string.Join(",", value.ToArray()));
        }

        [TestMethod]
        public void LeafNodeFindElements_Normal6()
        {
            var value = _leafNode.FindElements("55", "99");
            Assert.AreEqual("55", string.Join(",", value.ToArray()));
        }
    }
}
