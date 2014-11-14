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
    public class BPTreeInternalNodeUnitTest
    {
        private BPTreeInternalNode _internalNode;

        private BPTreeLeafNode _leafNode1;

        private BPTreeLeafNode _leafNode2;

        [TestInitialize]
        public void PrepareTest()
        {
            _leafNode1 = new BPTreeLeafNode(null, null,0, 2);
            _leafNode1.AddElement("11", "11");
            _leafNode1.AddElement("22", "22");
            _leafNode1.AddElement("33", "33");
            _leafNode1.AddElement("44", "44");
            _leafNode1.AddElement("46", "46");

            //result: _internalNode(_leafNode1(11,22,33)->_leafNode2(44,46))
            _internalNode = _leafNode1.Parent;
            //_internalNode = new BPTreeInternalNode(0, 2);
            //_internalNode.Insert(0, _leafNode1);
            //_internalNode.Insert(1, _leafNode2);
        }

        [TestMethod]
        public void InternalNodeAdd_Basical()
        {
            var node1 = _internalNode.AddElement("55", "55");
            var node2 = _internalNode.AddElement("77", "77");
            var node3 = _internalNode.AddElement("10", "10");
            var node4 = _internalNode.AddElement("15", "15");
            var node5 = _internalNode.AddElement("80", "80");
            var node6 = _internalNode.AddElement("56", "56");
            var node7 = _internalNode.AddElement("57", "57");
            var node8 = _internalNode.AddElement("58", "58");
            var node9 = _internalNode.AddElement("59", "59");
            var node10 = _internalNode.AddElement("25", "25");
            var node11 = _internalNode.AddElement("20", "20");

            //result: _internalNode(_leafNode1(10,11,15)->(20,22)->(25,33,44,46))->node11(_leafNode2(55,56,57,58)->(59,77,80))

            Assert.AreEqual(true, node1);
            Assert.AreEqual(true, node2);
            Assert.AreEqual(true, node3);
            Assert.AreEqual(true, node4);
            Assert.AreEqual(true, node5);
            Assert.AreEqual(true, node6);
            Assert.AreEqual(true, node7);
            Assert.AreEqual(true, node8);
            Assert.AreEqual(true, node9);
            Assert.AreEqual(true, node10);
            Assert.AreNotEqual(false, node11);
            Assert.AreEqual("55", _internalNode.Next().Key);
            Assert.AreEqual(2, _internalNode.Next().Size);
            Assert.AreEqual(3, _internalNode.Size);
            Assert.AreEqual(3, _leafNode1.Size);
            Assert.AreEqual(4, _internalNode.Next().Children[0].Size);
            Assert.AreEqual("55,59", _internalNode.Next().ToString());
        }

        [TestMethod]
        public void InternalNodeAdd_Move_Next()
        {
            var node1 = _internalNode.AddElement("55", "55");
            var node2 = _internalNode.AddElement("77", "77");
            var node3 = _internalNode.AddElement("10", "10");
            var node4 = _internalNode.AddElement("15", "15");
            var node5 = _internalNode.AddElement("80", "80");
            var node6 = _internalNode.AddElement("56", "56");
            var node7 = _internalNode.AddElement("57", "57");
            var node8 = _internalNode.AddElement("58", "58");
            var node9 = _internalNode.AddElement("59", "59");
            var node10 = _internalNode.AddElement("25", "25");
            var node11 = _internalNode.AddElement("20", "20");

            //result: _internalNode(_leafNode1(10,11,15)->(20,22)->(25,33,44,46))->node11(_leafNode2(55,56,57,58)->(59,77,80))
            //the result is verified by test InternalNodeAdd_Basical

            var node12 = _internalNode.Parent.AddElement("35", "35");
            var node13 = _internalNode.Parent.AddElement("30", "30");
            var node14 = _internalNode.Parent.AddElement("45", "45");
            var node15 = _internalNode.Parent.AddElement("47", "47");
            var node16 = _internalNode.Parent.AddElement("48", "48");
            var node17 = _internalNode.Parent.AddElement("52", "52");
            var node18 = _internalNode.Parent.AddElement("53", "53");

            //result: _internalNode(_leafNode1(10,11,15)->(20,22,25,30)->(33,35,44,45)->(46,47,48))->node11((52,53)->_leafNode2(55,56,57,58)->(59,77,80))

            var node19 = _internalNode.Parent.AddElement("49", "49");
            var node20 = _internalNode.Parent.AddElement("50", "50");

            //result: _internalNode(_leafNode1(10,11,15)->(20,22,25,30)->(33,35,44,45)->(46,47,48,49))->node11((50,52,53)->_leafNode2(55,56,57,58)->(59,77,80))

            Assert.AreEqual(true, node12);
            Assert.AreEqual(true, node13);
            Assert.AreEqual(true, node14);
            Assert.AreEqual(true, node15);
            Assert.AreEqual(true, node16);
            Assert.AreEqual(true, node17);
            Assert.AreEqual(true, node18);
            Assert.AreEqual(true, node19);
            Assert.AreEqual(true, node20);

            Assert.AreEqual(4, _internalNode.Size);
            Assert.AreEqual("10", _leafNode1.Key);
            Assert.AreEqual(3, _leafNode1.Size);
            Assert.AreEqual("55,56,57,58", _internalNode.Next().Children[1].ToString());
            Assert.AreEqual(4, _internalNode.Next().Children[1].Size);
            Assert.AreEqual("55", _internalNode.Next().Children[1].Key);
            Assert.AreEqual(3, _internalNode.Next().Children[0].Size);
            Assert.AreEqual("50", _internalNode.Next().Children[0].Key);
        }

        [TestMethod]
        public void InternalNodeAdd_Move_Previous()
        {
            var node1 = _internalNode.AddElement("55", "55");
            var node2 = _internalNode.AddElement("77", "77");
            var node3 = _internalNode.AddElement("10", "10");
            var node4 = _internalNode.AddElement("15", "15");
            var node5 = _internalNode.AddElement("80", "80");
            var node6 = _internalNode.AddElement("56", "56");
            var node7 = _internalNode.AddElement("57", "57");
            var node8 = _internalNode.AddElement("58", "58");
            var node9 = _internalNode.AddElement("59", "59");
            var node10 = _internalNode.AddElement("25", "25");
            var node11 = _internalNode.AddElement("20", "20");

            //result: _internalNode(_leafNode1(10,11,15)->(20,22)->(25,33,44,46))->node11(_leafNode2(55,56,57,58)->(59,77,80))
            //the result is verified by test InternalNodeAdd_Basical

            var node12 = _internalNode.Parent.AddElement("60", "60");
            var node13 = _internalNode.Parent.AddElement("61", "61");
            var node14 = _internalNode.Parent.AddElement("62", "62");
            var node15 = _internalNode.Parent.AddElement("65", "65");
            var node16 = _internalNode.Parent.AddElement("66", "66");
            var node17 = _internalNode.Parent.AddElement("70", "70");
            var node18 = _internalNode.Parent.AddElement("99", "99");
            var node19 = _internalNode.Parent.AddElement("64", "64");
            var node20 = _internalNode.Parent.AddElement("75", "75");
            var node21 = _internalNode.Parent.AddElement("73", "73");

            //result: _internalNode(_leafNode1(10,11,15)->(20,22)->(25,33,44,46)->_leafNode2(55,56,57,58))->node11((59,60,61,62)->(64,65,66)->(70,73)->(75,77,80,99))

            Assert.AreEqual(true, node12);
            Assert.AreEqual(true, node13);
            Assert.AreEqual(true, node14);
            Assert.AreEqual(true, node15);
            Assert.AreEqual(true, node16);
            Assert.AreEqual(true, node17);
            Assert.AreEqual(true, node18);
            Assert.AreEqual(true, node19);
            Assert.AreEqual(true, node20);
            Assert.AreEqual(true, node21);

            Assert.AreEqual("10,20,25,55", _internalNode.ToString());
            Assert.AreEqual(4, _internalNode.Size);
            Assert.AreEqual("10,11,15", _leafNode1.ToString());
            Assert.AreEqual("10", _leafNode1.Key);
            Assert.AreEqual(3, _leafNode1.Size);
            Assert.AreEqual(4, _internalNode.Children[3].Size);
            Assert.AreEqual("55", _internalNode.Children[3].Key);
            Assert.AreEqual(4, _internalNode.Next().Size);
            Assert.AreEqual("59", _internalNode.Next().Key);
        }

        [TestMethod]
        public void InternalNodeDelete_Move_Next()
        {
            var node1 = _internalNode.AddElement("55", "55");
            var node2 = _internalNode.AddElement("77", "77");
            var node3 = _internalNode.AddElement("10", "10");
            var node4 = _internalNode.AddElement("15", "15");
            var node5 = _internalNode.AddElement("80", "80");
            var node6 = _internalNode.AddElement("56", "56");
            var node7 = _internalNode.AddElement("57", "57");
            var node8 = _internalNode.AddElement("58", "58");
            var node9 = _internalNode.AddElement("59", "59");
            var node10 = _internalNode.AddElement("25", "25");
            var node11 = _internalNode.AddElement("20", "20");

            //result: _internalNode(_leafNode1(10,11,15)->(20,22)->(25,33,44,46))->node11(_leafNode2(55,56,57,58)->(59,77,80))
            //the result is verified by test InternalNodeAdd_Basical
            var node12 = _internalNode.Parent.AddElement("99", "99");
            var node13 = _internalNode.Parent.AddElement("70", "70");
            //result: _internalNode(_leafNode1(10,11,15)->(20,22)->(25,33,44,46))->node11(_leafNode2(55,56,57,58)->(59,70,77)->(80,99))
            string delete;
            var node14 = _internalNode.Parent.DeleteElement("22", out delete);
            Assert.AreEqual("22", delete);
            var node15 = _internalNode.Parent.DeleteElement("33", out delete);
            Assert.AreEqual("33", delete);
            var node16 = _internalNode.Parent.DeleteElement("46", out delete);
            Assert.AreEqual("46", delete);
            var node17 = _internalNode.Parent.DeleteElement("10", out delete);
            Assert.AreEqual("10", delete);
            var node18 = _internalNode.Parent.DeleteElement("15", out delete);
            Assert.AreEqual("15", delete);
            var node19 = _internalNode.Parent.DeleteElement("11", out delete);
            Assert.AreEqual("11", delete);

            //result: _internalNode(_leafNode1(20,25,44)->_leafNode2(55,56,57,58))->node11(->(59,70,77)->(80,99))
            
            Assert.AreEqual(true, node12);
            Assert.AreEqual(true, node13);
            Assert.AreEqual(true, node14);
            Assert.AreEqual(true, node15);
            Assert.AreEqual(true, node16);
            Assert.AreEqual(true, node17);
            Assert.AreEqual(true, node18);
            Assert.AreEqual(true, node19);

            Assert.AreEqual(3, _leafNode1.Size);
            Assert.AreEqual(4, _leafNode1.Next().Size);
            Assert.AreEqual("20", _leafNode1.Key);
            Assert.AreEqual("55", _leafNode1.Next().Key);
            Assert.AreEqual(4, _internalNode.Children[1].Size);
            Assert.AreEqual("55", _internalNode.Children[1].Key);
        }

        [TestMethod]
        public void InternalNodeDelete_Move_Previous()
        {
            var node1 = _internalNode.AddElement("55", "55");
            var node2 = _internalNode.AddElement("77", "77");
            var node3 = _internalNode.AddElement("10", "10");
            var node4 = _internalNode.AddElement("15", "15");
            var node5 = _internalNode.AddElement("80", "80");
            var node6 = _internalNode.AddElement("56", "56");
            var node7 = _internalNode.AddElement("57", "57");
            var node8 = _internalNode.AddElement("58", "58");
            var node9 = _internalNode.AddElement("59", "59");
            var node10 = _internalNode.AddElement("25", "25");
            var node11 = _internalNode.AddElement("20", "20");

            //result: _internalNode(_leafNode1(10,11,15)->(20,22)->(25,33,44,46))->node11(_leafNode2(55,56,57,58)->(59,77,80))
            //the result is verified by test InternalNodeAdd_Basical
            string delete;
            var node12 = _internalNode.Parent.DeleteElement("56", out delete);
            Assert.AreEqual("56", delete);
            var node13 = _internalNode.Parent.DeleteElement("77", out delete);
            Assert.AreEqual("77", delete);
            var node14 = _internalNode.Parent.DeleteElement("78", out delete);
            Assert.AreEqual(null, delete);
            var node15 = _internalNode.Parent.DeleteElement("55", out delete);
            Assert.AreEqual("55", delete);
            var node16 = _internalNode.Parent.DeleteElement("58", out delete);
            Assert.AreEqual("58", delete);
            var node17 = _internalNode.Parent.DeleteElement("46", out delete);
            Assert.AreEqual("46", delete);
            var node18 = _internalNode.Parent.DeleteElement("57", out delete);
            Assert.AreEqual("57", delete);

            //result: _internalNode(_leafNode1(10,11,15)->(20,22))->node11((25,33)->_leafNode2(44,59,80))

            Assert.AreEqual(true, node12);
            Assert.AreEqual(true, node13);
            Assert.AreEqual(false, node14);
            Assert.AreEqual(true, node15);
            Assert.AreEqual(true, node16);
            Assert.AreEqual(true, node17);
            Assert.AreEqual(true, node18);

            Assert.AreEqual(2, _internalNode.Size);
            Assert.AreEqual("10", _internalNode.Key);
            Assert.AreEqual(3, _leafNode1.Size);
            Assert.AreEqual(2, _internalNode.Next().Size);
            Assert.AreEqual("10", _leafNode1.Key);
            Assert.AreEqual("44", _internalNode.Next().Children[1].Key);
            Assert.AreEqual(2, _internalNode.Children[1].Size);
            Assert.AreEqual("20", _internalNode.Children[1].Key);
        }

        [TestMethod]
        public void InternalNodeDelete_Merge_Internal()
        {
            var node1 = _internalNode.AddElement("55", "55");
            var node2 = _internalNode.AddElement("77", "77");
            var node3 = _internalNode.AddElement("10", "10");
            var node4 = _internalNode.AddElement("15", "15");
            var node5 = _internalNode.AddElement("80", "80");
            var node6 = _internalNode.AddElement("56", "56");
            var node7 = _internalNode.AddElement("57", "57");
            var node8 = _internalNode.AddElement("58", "58");
            var node9 = _internalNode.AddElement("59", "59");
            var node10 = _internalNode.AddElement("25", "25");
            var node11 = _internalNode.AddElement("20", "20");

            //result: _internalNode(_leafNode1(10,11,15)->(20,22)->(25,33,44,46))->node11(_leafNode2(55,56,57,58)->(59,77,80))
            //the result is verified by test InternalNodeAdd_Basical

            var node12 = _internalNode.Parent.AddElement("60", "60");
            var node13 = _internalNode.Parent.AddElement("61", "61");
            var node14 = _internalNode.Parent.AddElement("62", "62");
            var node15 = _internalNode.Parent.AddElement("65", "65");
            var node16 = _internalNode.Parent.AddElement("66", "66");
            var node17 = _internalNode.Parent.AddElement("70", "70");
            var node18 = _internalNode.Parent.AddElement("99", "99");
            var node19 = _internalNode.Parent.AddElement("64", "64");
            var node20 = _internalNode.Parent.AddElement("75", "75");
            var node21 = _internalNode.Parent.AddElement("73", "73");

            //result: _internalNode(_leafNode1(10,11,15)->(20,22)->(25,33,44,46)->_leafNode2(55,56,57,58))->node11((59,60,61,62)->(64,65,66)->(70,73)->(75,77,80,99))
            //the result is verified by test InternalNodeAdd_Move_Previous

            var node22 = _internalNode.Parent.AddElement("68", "68");
            var node23 = _internalNode.Parent.AddElement("72", "72");
            var node24 = _internalNode.Parent.AddElement("74", "74");
            var node25 = _internalNode.Parent.AddElement("67", "67");

            //result: _internalNode(_leafNode1(10,11,15)->(20,22)->(25,33,44,46)->_leafNode2(55,56,57,58))->node11((59,60,61,62)->(64,65,66)->(67,68))->node25((70,72,73,74)->(75,77,80,99))

            Assert.AreEqual(true, node22);
            Assert.AreEqual(true, node23);
            Assert.AreEqual(true, node24);
            Assert.AreEqual(true, node25);
            //Assert.AreEqual(node25, node11.Next);
            //Assert.AreEqual(2, node25.Size);
            //Assert.AreEqual("70", node25.Key);

            string delete;
            var node26 = _internalNode.Parent.DeleteElement("22", out delete);
            Assert.AreEqual("22", delete);
            var node27 = _internalNode.Parent.DeleteElement("33", out delete);
            Assert.AreEqual("33", delete);
            var node28 = _internalNode.Parent.DeleteElement("46", out delete);
            Assert.AreEqual("46", delete);
            var node29 = _internalNode.Parent.DeleteElement("10", out delete);
            Assert.AreEqual("10", delete);
            var node30 = _internalNode.Parent.DeleteElement("15", out delete);
            Assert.AreEqual("15", delete);
            var node31 = _internalNode.Parent.DeleteElement("11", out delete);
            Assert.AreEqual("11", delete);
            var node32 = _internalNode.Parent.DeleteElement("20", out delete);
            Assert.AreEqual("20", delete);
            var node33 = _internalNode.Parent.DeleteElement("68", out delete);
            Assert.AreEqual("68", delete);
            var node34 = _internalNode.Parent.DeleteElement("67", out delete);
            Assert.AreEqual("67", delete);
            var node35 = _internalNode.Parent.DeleteElement("65", out delete);
            Assert.AreEqual("65", delete);
            var node36 = _internalNode.Parent.DeleteElement("64", out delete);
            Assert.AreEqual("64", delete);
            var node37 = _internalNode.Parent.DeleteElement("62", out delete);
            Assert.AreEqual("62", delete);
            var node38 = _internalNode.Parent.DeleteElement("66", out delete);
            Assert.AreEqual("66", delete);
            var node39 = _internalNode.Parent.DeleteElement("57", out delete);
            Assert.AreEqual("57", delete);
            var node40 = _internalNode.Parent.DeleteElement("58", out delete);
            Assert.AreEqual("58", delete);
            var node41 = _internalNode.Parent.DeleteElement("25", out delete);
            Assert.AreEqual("25", delete);

            //result: _internalNode(_leafNode1(25,44)->_leafNode2(55,56))->node11((59,60)->(61,70,72))->node25((73,74)->(75,77,80,99))
            //result: _internalNode(_leafNode1(44,55,56))->node11((59,60)->(61,70,72))->node25((73,74)->(75,77,80,99))
            //result: _internalNode(_leafNode1(44,55,56)->(59,60)->(61,70,72))->node25((73,74)->(75,77,80,99))

            Assert.AreEqual(true, node26);
            Assert.AreEqual(true, node27);
            Assert.AreEqual(true, node28);
            Assert.AreEqual(true, node29);
            Assert.AreEqual(true, node30);
            Assert.AreEqual(true, node31);
            Assert.AreEqual(true, node32);
            Assert.AreEqual(true, node33);
            Assert.AreEqual(true, node34);
            Assert.AreEqual(true, node35);
            Assert.AreEqual(true, node36);
            Assert.AreEqual(true, node37);
            Assert.AreEqual(true, node38);
            Assert.AreEqual(true, node39);
            Assert.AreEqual(true, node40);
            Assert.AreEqual(true, node41);
            Assert.AreEqual(node11, node41);

            Assert.AreEqual(3, _internalNode.Size);
            Assert.AreEqual("44", _internalNode.Key);
            Assert.AreEqual(3, _leafNode1.Size);
            Assert.AreEqual("44", _leafNode1.Key);
        }

        [TestMethod]
        public void InternalNodeDelete_Merge_DifferentParent()
        {
            var node1 = _internalNode.AddElement("55", "55");
            var node2 = _internalNode.AddElement("77", "77");
            var node3 = _internalNode.AddElement("10", "10");
            var node4 = _internalNode.AddElement("15", "15");
            var node5 = _internalNode.AddElement("80", "80");
            var node6 = _internalNode.AddElement("56", "56");
            var node7 = _internalNode.AddElement("57", "57");
            var node8 = _internalNode.AddElement("58", "58");
            var node9 = _internalNode.AddElement("59", "59");
            var node10 = _internalNode.AddElement("25", "25");
            var node11 = _internalNode.AddElement("20", "20");

            //result: _internalNode(_leafNode1(10,11,15)->(20,22)->(25,33,44,46))->node11(_leafNode2(55,56,57,58)->(59,77,80))
            //the result is verified by test InternalNodeAdd_Basical

            var node12 = _internalNode.Parent.AddElement("60", "60");
            var node13 = _internalNode.Parent.AddElement("61", "61");
            var node14 = _internalNode.Parent.AddElement("62", "62");
            var node15 = _internalNode.Parent.AddElement("65", "65");
            var node16 = _internalNode.Parent.AddElement("66", "66");
            var node17 = _internalNode.Parent.AddElement("70", "70");
            var node18 = _internalNode.Parent.AddElement("99", "99");
            var node19 = _internalNode.Parent.AddElement("64", "64");
            var node20 = _internalNode.Parent.AddElement("75", "75");
            var node21 = _internalNode.Parent.AddElement("73", "73");

            //result: _internalNode(_leafNode1(10,11,15)->(20,22)->(25,33,44,46)->_leafNode2(55,56,57,58))->node11((59,60,61,62)->(64,65,66)->(70,73)->(75,77,80,99))
            //the result is verified by test InternalNodeAdd_Move_Previous

            var node22 = _internalNode.Parent.AddElement("68", "68");
            var node23 = _internalNode.Parent.AddElement("72", "72");
            var node24 = _internalNode.Parent.AddElement("74", "74");
            var node25 = _internalNode.Parent.AddElement("67", "67");

            //result: _internalNode(_leafNode1(10,11,15)->(20,22)->(25,33,44,46)->_leafNode2(55,56,57,58))->node11((59,60,61,62)->(64,65,66)->(67,68))->node25((70,72,73,74)->(75,77,80,99))
            //the result is verified by test InternalNodeDelete_Merge_Internal

            var node26 = _internalNode.Parent.AddElement("04", "04");
            var node27 = _internalNode.Parent.AddElement("01", "01");
            var node28 = _internalNode.Parent.AddElement("09", "09");
            var node29 = _internalNode.Parent.AddElement("08", "08");
            var node30 = _internalNode.Parent.AddElement("34", "34");
            //result: _internalNode(_leafNode1(01,04,08)->(09,10)->(11,15,20,22))->node30((25,33,34)->(44,46))->node11(_leafNode2(55,56,57,58)->(59,60,61,62)->(64,65,66)->(67,68))->node25((70,72,73,74)->(75,77,80,99))

            var node31 = _internalNode.Parent.AddElement("12", "12");
            var node32 = _internalNode.Parent.AddElement("13", "13");
            var node33 = _internalNode.Parent.AddElement("14", "14");
            var node34 = _internalNode.Parent.AddElement("16", "16");
            var node35 = _internalNode.Parent.AddElement("17", "17");
            var node36 = _internalNode.Parent.AddElement("18", "18");
            var node37 = _internalNode.Parent.AddElement("19", "19");

            //result: _internalNode(_leafNode1(01,04,08)->(09,10,11,12)->(13,14,15,16)->(17,18,19,20))->node30((22,25,33,34)->(44,46))->node11(_leafNode2(55,56,57,58)->(59,60,61,62)->(64,65,66)->(67,68))->node25((70,72,73,74)->(75,77,80,99))

            var node38 = _internalNode.Parent.AddElement("21", "21");
            //result: _internalNode(_leafNode1(01,04,08)->(09,10,11,12)->(13,14,15,16)->(17,18,19))->node30((20,21)->(22,25,33,34)->(44,46))->node11(_leafNode2(55,56,57,58)->(59,60,61,62)->(64,65,66)->(67,68))->node25((70,72,73,74)->(75,77,80,99))

            var node39 = _internalNode.Parent.AddElement("23", "23");
            var node40 = _internalNode.Parent.AddElement("24", "24");
            var node41 = _internalNode.Parent.AddElement("26", "26");
            var node42 = _internalNode.Parent.AddElement("27", "27");
            //result: _internalNode(_leafNode1(01,04,08)->(09,10,11,12)->(13,14,15,16)->(17,18,19))->node30((20,21,22,23)->(24,25,26,27)->(33,34,44,46))->node11(_leafNode2(55,56,57,58)->(59,60,61,62)->(64,65,66)->(67,68))->node25((70,72,73,74)->(75,77,80,99))

            var node43 = _internalNode.Parent.AddElement("28", "28");
            var node44 = _internalNode.Parent.AddElement("29", "29");
            var node45 = _internalNode.Parent.AddElement("30", "30");
            var node46 = _internalNode.Parent.AddElement("31", "31");
            //result: _internalNode(_leafNode1(01,04,08)->(09,10,11,12)->(13,14,15,16)->(17,18,19))->node30((20,21,22,23)->(24,25,26,27)->(28,29,30,31)->(33,34,44,46))->node11(_leafNode2(55,56,57,58)->(59,60,61,62)->(64,65,66)->(67,68))->node25((70,72,73,74)->(75,77,80,99))

            var node47 = _internalNode.Parent.AddElement("32", "32");
            //result: _internalNode(_leafNode1(01,04,08)->(09,10,11,12)->(13,14,15,16)->(17,18,19))->node30((20,21,22,23)->(24,25,26,27)->(28,29,30)->(31,32)->(33,34,44,46))->node11(_leafNode2(55,56,57,58)->(59,60,61,62)->(64,65,66)->(67,68))->node25((70,72,73,74)->(75,77,80,99))
            //result: _internalNode(_leafNode1(01,04,08)->(09,10,11,12)->(13,14,15,16)->(17,18,19))->node30((20,21,22,23)->(24,25,26,27)->(28,29,30))->node47((31,32)->(33,34,44,46))->node11(_leafNode2(55,56,57,58)->(59,60,61,62)->(64,65,66)->(67,68))->node25((70,72,73,74)->(75,77,80,99))
            //result: (_internalNode(_leafNode1(01,04,08)->(09,10,11,12)->(13,14,15,16)->(17,18,19))->node30((20,21,22,23)->(24,25,26,27)->(28,29,30))->node47((31,32)->(33,34,44,46)))->(node11(_leafNode2(55,56,57,58)->(59,60,61,62)->(64,65,66)->(67,68))->node25((70,72,73,74)->(75,77,80,99)))
            
            Assert.AreEqual(true, node26);
            Assert.AreEqual(true, node27);
            Assert.AreEqual(true, node28);
            Assert.AreEqual(true, node29);
            Assert.AreEqual(true, node30);
            Assert.AreEqual(true, node31);
            Assert.AreEqual(true, node32);
            Assert.AreEqual(true, node33);
            Assert.AreEqual(true, node34);
            Assert.AreEqual(true, node35);
            Assert.AreEqual(true, node36);
            Assert.AreEqual(true, node37);
            Assert.AreEqual(true, node38);
            Assert.AreEqual(true, node39);
            Assert.AreEqual(true, node40);
            Assert.AreEqual(true, node41);
            Assert.AreEqual(true, node42);
            Assert.AreEqual(true, node43);
            Assert.AreEqual(true, node44);
            Assert.AreEqual(true, node45);
            Assert.AreEqual(true, node46);
            Assert.AreEqual(true, node47);
            //Assert.AreEqual(2, node47.Size);
            //Assert.AreEqual("31", node47.Key);

            string delete;
            var node48 = _internalNode.Parent.Parent.DeleteElement("44", out delete);
            Assert.AreEqual("44", delete);
            var node49 = _internalNode.Parent.Parent.DeleteElement("46", out delete);
            Assert.AreEqual("46", delete);
            var node50 = _internalNode.Parent.Parent.DeleteElement("34", out delete);
            Assert.AreEqual("34", delete);
            var node51 = _internalNode.Parent.Parent.DeleteElement("33", out delete);
            Assert.AreEqual("33", delete);
            var node52 = _internalNode.Parent.Parent.DeleteElement("56", out delete);
            Assert.AreEqual("56", delete);
            //result: (_internalNode(_leafNode1(01,04,08)->(09,10,11,12)->(13,14,15,16)->(17,18,19))->node30((20,21,22,23)->(24,25,26,27)->(28,29,30))->node47((31,32,55)->_leafNode2(57,58)))->(node11((59,60,61,62)->(64,65,66)->(67,68))->node25((70,72,73,74)->(75,77,80,99)))

            var node53 = _internalNode.Parent.Parent.DeleteElement("59", out delete);
            Assert.AreEqual("59", delete);
            var node54 = _internalNode.Parent.Parent.DeleteElement("61", out delete);
            Assert.AreEqual("61", delete);
            var node55 = _internalNode.Parent.Parent.DeleteElement("62", out delete);
            Assert.AreEqual("62", delete);
            var node56 = _internalNode.Parent.Parent.DeleteElement("66", out delete);
            Assert.AreEqual("66", delete);
            var node57 = _internalNode.Parent.Parent.DeleteElement("68", out delete);
            Assert.AreEqual("68", delete);
            var node58 = _internalNode.Parent.Parent.DeleteElement("32", out delete);
            Assert.AreEqual("32", delete);
            //result: (_internalNode(_leafNode1(01,04,08)->(09,10,11,12)->(13,14,15,16)->(17,18,19))->node30((20,21,22,23)->(24,25,26,27)->(28,29,30))->node47((31,55)->_leafNode2(57,58)))->(node11((60,64)->(65,67))->node25((70,72,73,74)->(75,77,80,99)))

            var node59 = _internalNode.Parent.Parent.DeleteElement("58", out delete);
            Assert.AreEqual("58", delete);
            //result: (_internalNode(_leafNode1(01,04,08)->(09,10,11,12)->(13,14,15,16)->(17,18,19))->node30((20,21,22,23)->(24,25,26,27)->(28,29,30))->node47((31,55)->_leafNode2(57,58)))->(node11((60,64)->(65,67))->node25((70,72,73,74)->(75,77,80,99)))
            //result: (_internalNode(_leafNode1(01,04,08)->(09,10,11,12)->(13,14,15,16)->(17,18,19))->node30((20,21,22,23)->(24,25,26,27)->(28,29,30))->node47((31,55)->_leafNode2(57,60,64)))->(node11((65,67))->node25((70,72,73,74)->(75,77,80,99)))
            //result: (_internalNode(_leafNode1(01,04,08)->(09,10,11,12)->(13,14,15,16)->(17,18,19))->node30((20,21,22,23)->(24,25,26,27)->(28,29,30))->node47((31,55)->_leafNode2(57,60,64)))->(node11((65,67)->(70,72,73,74)->(75,77,80,99)))
            //result: (_internalNode(_leafNode1(01,04,08)->(09,10,11,12)->(13,14,15,16)->(17,18,19))->node30((20,21,22,23)->(24,25,26,27)->(28,29,30)))->(node47((31,55)->_leafNode2(57,60,64))->node11((65,67)->(70,72,73,74)->(75,77,80,99)))
            
            Assert.AreEqual(true, node48);
            Assert.AreEqual(true, node49);
            Assert.AreEqual(true, node50);
            Assert.AreEqual(true, node51);
            Assert.AreEqual(true, node52);
            Assert.AreEqual(true, node53);
            Assert.AreEqual(true, node54);
            Assert.AreEqual(true, node55);
            Assert.AreEqual(true, node56);
            Assert.AreEqual(true, node57);
            Assert.AreEqual(true, node58);
            Assert.AreEqual(true, node59);
            Assert.AreEqual(node25, node59);
            //Assert.AreEqual(2, node47.Size);
            //Assert.AreEqual("31", node47.Key);
            //Assert.AreEqual(node11, node47.Next);
            //Assert.AreEqual(node30, node47.Previous);
        }

        [TestMethod]
        public void InternalNodeFind_Exist()
        {
            var value = _internalNode.FindElement("33");
            Assert.AreEqual("33", value);
        }

        [TestMethod]
        public void InternalNodeFind_NotExist()
        {
            var value = _internalNode.FindElement("34");
            Assert.AreEqual(null, value);
        }

        [TestMethod]
        public void InternalNodeFindElements_Error()
        {
            var value = _internalNode.FindElements("33", "22");
            Assert.AreEqual("", string.Join(",", value.ToArray()));
        }

        [TestMethod]
        public void InternalNodeFindElements_Inner1()
        {
            var value = _internalNode.FindElements("11","33");
            Assert.AreEqual("11,22,33", string.Join(",",value.ToArray()));
        }

        [TestMethod]
        public void InternalNodeFindElements_Inner2()
        {
            var value = _internalNode.FindElements("22", "33");
            Assert.AreEqual("22,33", string.Join(",", value.ToArray()));
        }

        [TestMethod]
        public void InternalNodeFindElements_Inner3()
        {
            var value = _internalNode.FindElements("01", "33");
            Assert.AreEqual("11,22,33", string.Join(",", value.ToArray()));
        }

        [TestMethod]
        public void InternalNodeFindElements_Inner4()
        {
            var value = _internalNode.FindElements("01", "22");
            Assert.AreEqual("11,22", string.Join(",", value.ToArray()));
        }

        [TestMethod]
        public void InternalNodeFindElements_Inner5()
        {
            var value = _internalNode.FindElements("20", "30");
            Assert.AreEqual("22", string.Join(",", value.ToArray()));
        }

        [TestMethod]
        public void InternalNodeFindElements_Inner6()
        {
            var value = _internalNode.FindElements("22", "22");
            Assert.AreEqual("22", string.Join(",", value.ToArray()));
        }

        [TestMethod]
        public void InternalNodeFindElements_Outer1()
        {
            var value = _internalNode.FindElements("11", "44");
            Assert.AreEqual("11,22,33,44", string.Join(",", value.ToArray()));
        }

        [TestMethod]
        public void InternalNodeFindElements_Outer2()
        {
            var value = _internalNode.FindElements("33", "44");
            Assert.AreEqual("33,44", string.Join(",", value.ToArray()));
        }

        [TestMethod]
        public void InternalNodeFindElements_Outer3()
        {
            var value = _internalNode.FindElements("03", "99");
            Assert.AreEqual("11,22,33,44,46", string.Join(",", value.ToArray()));
        }
    }
}
