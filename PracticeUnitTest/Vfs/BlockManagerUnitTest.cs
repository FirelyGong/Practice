using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Practice.Vfs;
using Practice.Vfs.Exceptions;

namespace PracticeUnitTest.Vfs
{
    [TestClass]
    public class BlockManagerUnitTest
    {
        [TestMethod]
        public void ReadBoot_Empty()
        {
            MemoryStream ms = new MemoryStream();

            BlockManager bm = new BlockManager(ms);

            bool hasException = false;
            try
            {
                bm.ReadBoot();
            }
            catch (InvalidBootInfoException ex)
            {
                hasException = true;
            }

            Assert.AreEqual(true, hasException);
        }

        [TestMethod]
        public void ReadBoot_Invalid()
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(new byte[14], 0, 14);
            BlockManager bm = new BlockManager(ms);

            bool hasException = false;
            try
            {
                bm.ReadBoot();
            }
            catch (InvalidBootInfoException ex)
            {
                hasException = true;
            }

            Assert.AreEqual(true, hasException);
        }

        [TestMethod]
        public void ReadBoot_NotEmpty()
        {
            MemoryStream ms = new MemoryStream();

            var info = new BootInfo
            {
                BlockSize = 100,
                BootSize = 512,
                LastBlock = -1,
                Space = -1,
                Information = ""
            };
            byte[] data = info.Persist();
            ms.Write(data, 0, data.Length);

            BlockManager bm1=new BlockManager(ms);
            bm1.ReadBoot();
            Assert.AreEqual(info.BlockSize, bm1.Boot.BlockSize);
            Assert.AreEqual(info.BootSize, bm1.Boot.BootSize);
            Assert.AreEqual(info.LastBlock, bm1.Boot.LastBlock);
            Assert.AreEqual(info.Space, bm1.Boot.Space);
            Assert.AreEqual(info.Information, bm1.Boot.Information);
        }

        [TestMethod]
        public void AllocateBlock_AddNewBlock()
        {
            MemoryStream ms = new MemoryStream();

            var info = new BootInfo
            {
                BlockSize = 100,
                BootSize = 512,
                LastBlock = -1,
                Space = -1,
                Information = ""
            };
            byte[] data = info.Persist();
            ms.Write(data, 0, data.Length);

            BlockManager bm1 = new BlockManager(ms);
            bm1.ReadBoot();

            var block  = bm1.AllocateBlock();

            Assert.AreEqual(0, block.Id);
            Assert.AreEqual(-1, bm1.Boot.Space);
        }

        [TestMethod]
        public void FreeBlock_Block()
        {
            MemoryStream ms = new MemoryStream();

            var info = new BootInfo
            {
                BlockSize = 100,
                BootSize = 512,
                LastBlock = -1,
                Space = -1,
                Information = ""
            };
            byte[] data = info.Persist();
            ms.Write(data, 0, data.Length);

            BlockManager bm1 = new BlockManager(ms);
            bm1.ReadBoot();

            var block = bm1.AllocateBlock();
            bm1.FreeBlock(new List<DataBlock>(new []{block}));

            Assert.AreEqual(0, block.Id);
            Assert.AreEqual(0, bm1.Boot.Space);
        }
    }
}
