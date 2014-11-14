using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Practice.Vfs.Exceptions;

namespace Practice.Vfs
{
    public class StreamStorage: DisposableObject, IStorage
    {
        private readonly Encoding _encoding = new UTF8Encoding();
        private readonly BlockManager _spaceManager;

        public StreamStorage(Stream dataStream)
        {
            _spaceManager = new BlockManager(dataStream);
        }

        public void Initialize(int blockSize)
        {
            try
            {
                _spaceManager.ReadBoot();
            }
            catch (InvalidBootInfoException ex)
            {
                if (blockSize > 0)
                {
                    var info = new BootInfo
                    {
                        BlockSize = blockSize,
                        BootSize = 512,
                        LastBlock = -1,
                        Space = -1,
                        Information = ""
                    };

                    _spaceManager.CreateBoot(info);
                }
                else
                {
                    throw ex;
                }
            }
        }

        public int Store(byte[] data)
        {
            var byteses = PrepareByteses(data);
            var blocks = new List<DataBlock>();
            for (int start=0; start < byteses.Count;start++)
            {
                DataBlock block = _spaceManager.AllocateBlock();
                block.Content = byteses[start];
                block.Size = (short)byteses[start].Length;

                var last = blocks.LastOrDefault();
                if (last != null)
                {
                    last.Next = block.Id;
                    block.Previsous = last.Id;
                }
                else
                {
                    block.Previsous = -1;
                }

                block.Next = -1;

                blocks.Add(block);
            }

            blocks.ForEach(b => _spaceManager.SaveBlock(b));
            _spaceManager.Commit();

            return blocks[0].Id;
        }

        public int Store(string data)
        {
            byte[] bytes = _encoding.GetBytes(data);
            return Store(bytes);
        }

        public bool Update(int address, byte[] data)
        {
            var blocks = LoadBlocks(address);
            if (blocks.Count == 0 || blocks[0].Previsous >= 0)
            {
                return false;
            }

            List<byte[]> byteses = PrepareByteses(data);

            int loop = byteses.Count;
            for (int i = 0; i < loop; i++)
            {
                if (i < blocks.Count)
                {
                    blocks[i].Content = byteses[i];
                    blocks[i].Size = (short)byteses[i].Length;
                }
                else
                {
                    var block = _spaceManager.AllocateBlock();
                    block.Content = byteses[i];
                    block.Size = (short)byteses[i].Length;
                    var last = blocks.LastOrDefault();
                    last.Next = block.Id;
                    block.Previsous = last.Id;
                    block.Next = -1;
                    blocks.Add(block);
                }
            }

            if (blocks.Count > byteses.Count)
            {
                blocks[byteses.Count - 1].Next = -1;
                var deletes = new List<DataBlock>();
                for (int i = byteses.Count; i < blocks.Count; i++)
                {
                    deletes.Add(blocks[i]);
                }

                _spaceManager.FreeBlock(deletes);
            }

            blocks.ForEach(b => _spaceManager.SaveBlock(b));
            _spaceManager.Commit();

            return true;
        }

        public bool Update(int address, string data)
        {
            byte[] bytes = _encoding.GetBytes(data);
            return Update(address, bytes);
        }

        public string Delete(int address)
        {
            var buffer = Delete(address, true);
            return _encoding.GetString(buffer);
        }

        public byte[] Delete(int address, bool returnBytes)
        {
            if (!returnBytes)
            {
                return new byte[] { };
            }

            var blocks = LoadBlocks(address);
            var buffer = new List<byte>();
            blocks.ForEach(b => buffer.AddRange(b.Content));

            _spaceManager.FreeBlock(blocks);
            _spaceManager.Commit();
            return buffer.ToArray();
        }

        public string Read(int address)
        {
            var buffer = Read(address, true);
            return _encoding.GetString(buffer);
        }

        public byte[] Read(int address, bool returnBytes)
        {
            if (!returnBytes)
            {
                return new byte[] { };
            }

            var blocks = LoadBlocks(address);
            var buffer = new List<byte>();
            blocks.ForEach(b => buffer.AddRange(b.Content));

            return buffer.ToArray();
        }

        public void AppendCustomBootInfo(string info)
        {
            _spaceManager.Boot.Information = info;
            _spaceManager.UpdateBoot();
        }

        public string RemoveCustomBootInfo()
        {
            string info = _spaceManager.Boot.Information;
            _spaceManager.Boot.Information = "";
            _spaceManager.UpdateBoot();
            return info;
        }

        public string ReadCustomBootInfo()
        {
            string info = _spaceManager.Boot.Information;
            return info;
        }

        protected override void DoDispose()
        {
            _spaceManager.Dispose();
        }

        private List<DataBlock> LoadBlocks(int address)
        {
            var buffer = new List<DataBlock>();
            var block = _spaceManager.ReadBlock(address);
            if (block == null || block.Previsous >= 0)
            {
                return buffer;
            }

            int next = address;
            while (next >= 0)
            {
                var b = _spaceManager.ReadBlock(next);
                buffer.Add(b);
                next = b.Next;
            }

            return buffer;
        }
        
        private List<byte[]> PrepareByteses(byte[] bytes)
        {
            var result = new List<byte[]>();
            int start = 0;
            while (start < bytes.Length)
            {
                int len = Math.Min(bytes.Length - start, _spaceManager.BlockContentSize);
                var b = new byte[len];
                Array.Copy(bytes, start, b, 0, len);
                start += len;
                result.Add(b);
            }

            return result;
        } 
    }
}
