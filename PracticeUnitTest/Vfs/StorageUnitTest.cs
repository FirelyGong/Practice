using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Practice.Vfs;
using Practice.Vfs.Exceptions;

namespace PracticeUnitTest.Vfs
{
    [TestClass]
    public class StorageUnitTest
    {
        [TestMethod]
        public void Initialize_EmptyStream()
        {
            MemoryStream ms = new MemoryStream();
            StreamStorage ss = new StreamStorage(ms);
            bool hasException = false;
            try
            {
                ss.Initialize(0);
                Assert.Fail("Empty stream shouldn't be accepted");
            }
            catch (InvalidBootInfoException)
            {
                hasException = true;
            }

            Assert.IsTrue(hasException);
        }

        [TestMethod]
        public void Initialize_Create()
        {
            MemoryStream ms = new MemoryStream();
            StreamStorage ss = new StreamStorage(ms);
                ss.Initialize(100);

            Assert.AreNotEqual(ms.Length, 0);
        }
        
        [TestMethod]
        public void ReadData_OneBlock()
        {
            MemoryStream ms = new MemoryStream();
            StreamStorage ss = new StreamStorage(ms);
            ss.Initialize(100);

            Assert.AreNotEqual(ms.Length, 0);

            string content = GenerateString(50);
            ss.Store(content);
            string data = ss.Read(0);

            Assert.AreEqual(content, data);
        }

        [TestMethod]
        public void ReadData_MultipleBlocks()
        {
            MemoryStream ms = new MemoryStream();
            StreamStorage ss = new StreamStorage(ms);
            ss.Initialize(100);

            Assert.AreNotEqual(ms.Length, 0);

            string content = GenerateString(150);
            ss.Store(content);
            string data = ss.Read(0);

            Assert.AreEqual(content, data);
        }
        
        [TestMethod]
        public void ReadData_MultipleBlocks2()
        {
            MemoryStream ms = new MemoryStream();
            StreamStorage ss = new StreamStorage(ms);
            ss.Initialize(100);

            Assert.AreNotEqual(ms.Length, 0);

            string content = GenerateString(580);
            ss.Store(content);
            string data = ss.Read(0);

            Assert.AreEqual(content, data);
        }

        [TestMethod]
        public void ReadData_FromMiddle()
        {
            MemoryStream ms = new MemoryStream();
            StreamStorage ss = new StreamStorage(ms);
            ss.Initialize(100);

            Assert.AreNotEqual(ms.Length, 0);

            string content = GenerateString(580);
            ss.Store(content);
            string data = ss.Read(1);

            Assert.AreEqual(string.Empty, data);
        }

        [TestMethod]
        public void ReadData_InvalidAddress()
        {
            MemoryStream ms = new MemoryStream();
            StreamStorage ss = new StreamStorage(ms);
            ss.Initialize(100);

            Assert.AreNotEqual(ms.Length, 0);

            string content = GenerateString(580);
            ss.Store(content);
            string data = ss.Read(11);

            Assert.AreEqual(string.Empty, data);
        }

        [TestMethod]
        public void DeleteData_DeleteLongAndStoreShort()
        {
            MemoryStream ms = new MemoryStream();
            StreamStorage ss = new StreamStorage(ms);
            ss.Initialize(100);

            Assert.AreNotEqual(ms.Length, 0);

            string content = GenerateString(580);
            ss.Store(content);

            string delete = ss.Delete(0);
            Assert.AreEqual(content, delete);

            string newContent = GenerateString(400);
            int address1 = ss.Store(newContent);
            string data1 = ss.Read(address1);
            Assert.AreEqual(0, address1);
            Assert.AreEqual(newContent, data1);

            string content2 = GenerateString(200);
            int address2 = ss.Store(content2);
            Assert.AreEqual(5, address2);
            string data2 = ss.Read(address2);
            Assert.AreEqual(content2, data2);
        }

        [TestMethod]
        public void DeleteData_DeleteShortAndStoreLong()
        {
            MemoryStream ms = new MemoryStream();
            StreamStorage ss = new StreamStorage(ms);
            ss.Initialize(100);

            Assert.AreNotEqual(ms.Length, 0);

            string content = GenerateString(100);
            ss.Store(content);

            string delete = ss.Delete(0);
            Assert.AreEqual(content, delete);

            string newContent = GenerateString(510);
            int address1 = ss.Store(newContent);
            string data1 = ss.Read(address1);
            Assert.AreEqual(0, address1);
            Assert.AreEqual(newContent, data1);

            string content2 = GenerateString(200);
            int address2 = ss.Store(content2);
            Assert.AreEqual(7, address2);
            string data2 = ss.Read(address2);
            Assert.AreEqual(content2, data2);
        }

        [TestMethod]
        public void DeleteData_DeleteFromMiddle()
        {
            MemoryStream ms = new MemoryStream();
            StreamStorage ss = new StreamStorage(ms);
            ss.Initialize(100);

            Assert.AreNotEqual(ms.Length, 0);

            string content = GenerateString(580);
            ss.Store(content);

            string delete = ss.Delete(4);
            Assert.AreEqual(string.Empty, delete);

            string content1 = GenerateString(400);
            int address = ss.Store(content1);
            Assert.AreEqual(8, address);
            string data1 = ss.Read(8);
            Assert.AreEqual(content1, data1);
        }

        [TestMethod]
        public void UpdateData_InvailedAddress()
        {
            MemoryStream ms = new MemoryStream();
            StreamStorage ss = new StreamStorage(ms);
            ss.Initialize(100);

            Assert.AreNotEqual(ms.Length, 0);

            string content = GenerateString(320);
            ss.Store(content);

            string content1 = GenerateString(230);
            ss.Update(1, content1);

            string content2 = ss.Read(0);

            Assert.AreEqual(content, content2);
        }

        [TestMethod]
        public void UpdateData_Shorter()
        {
            MemoryStream ms = new MemoryStream();
            StreamStorage ss = new StreamStorage(ms);
            ss.Initialize(100);

            Assert.AreNotEqual(ms.Length, 0);

            string content = GenerateString(320);
            ss.Store(content);

            string content1 = GenerateString(210);
            ss.Update(0, content1);

            string content2 = GenerateString(100);
            int address = ss.Store(content2);
            string content3 = ss.Read(0);

            Assert.AreEqual(content1, content3);
            Assert.AreEqual(3, address);
            string content4 = ss.Read(3);
            Assert.AreEqual(content2, content4);
        }

        [TestMethod]
        public void UpdateData_Longer()
        {
            MemoryStream ms = new MemoryStream();
            StreamStorage ss = new StreamStorage(ms);
            ss.Initialize(100);

            Assert.AreNotEqual(ms.Length, 0);

            string content = GenerateString(320);
            ss.Store(content);

            string content1 = GenerateString(500);
            ss.Update(0, content1);

            string content2 = GenerateString(100);
            int address = ss.Store(content2);
            string content3 = ss.Read(0);

            Assert.AreEqual(content1, content3);
            Assert.AreEqual(7, address);
        }

        private string GenerateString(int length)
        {
            string chars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder sb=new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                sb.Append(chars.Substring(random.Next(62), 1));
            }

            return sb.ToString();
        }
    }
}
