using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Practice.ThingsBase.DataPool;

namespace PracticeUnitTest.ThingsBase
{
    [TestClass]
    public class DataContainerTest
    {
        private string _dbPath;

        [TestInitialize]
        public void InitializeTest()
        {
            const string _testDbFile = "test.stg";
            string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.Templates);
            _dbPath = Path.Combine(rootPath, _testDbFile);
            if (File.Exists(_dbPath))
            {
                File.Delete(_dbPath);
            }

            if (!File.Exists(_dbPath))
            {
                using (var stream = File.Create(_dbPath))
                {
                }
            }
        }

        [TestMethod]
        public void WriteAndReadTest()
        {
            List<string> lst = new List<string>();
            var dc = new DataContainer(_dbPath);
            int loop = 1000;
            for (int i = 0; i < loop; i++)
            {
                string key = GenerateString();
                lst.Add(key);
                bool save = dc.Save(key, key);

                Assert.AreEqual(save, true);
            }

            for (int i = 0; i < loop; i++)
            {
                string key = lst[i];
                string value = dc.Get(key);
                Assert.AreEqual(key, value);
            }

            dc.Dispose();

            dc=new DataContainer(_dbPath);
            
            for (int i = 0; i < loop; i++)
            {
                string key = lst[i];
                string value = dc.Get(key);
                Assert.AreEqual(key, value);
            }

            for (int i = 0; i < loop; i++)
            {
                string key = lst[i];
                string value = dc.Delete(key);
                Assert.AreEqual(key, value);
            }

            dc.Dispose();
            
            // add again
            lst.Clear();
            dc = new DataContainer(_dbPath);
            for (int i = 0; i < loop; i++)
            {
                string key = GenerateString();
                lst.Add(key);
                bool save = dc.Save(key, key);

                Assert.AreEqual(save, true);
            }

            for (int i = 0; i < loop; i++)
            {
                string key = lst[i];
                string value = dc.Get(key);
                Assert.AreEqual(key, value);
            }

            dc.Dispose();

            dc = new DataContainer(_dbPath);

            for (int i = 0; i < loop; i++)
            {
                string key = lst[i];
                string value = dc.Get(key);
                Assert.AreEqual(key, value);
            }
        }

        [TestCleanup]
        public void FinishTest()
        {
            
        }

        private string GenerateString(int length)
        {
            string chars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                sb.Append(chars.Substring(random.Next(62), 1));
            }

            return sb.ToString();
        }

        private string GenerateString()
        {
                Random random = new Random(Guid.NewGuid().GetHashCode());

            int len = random.Next(300, 700);
            return GenerateString(len);
        }
    }
}
