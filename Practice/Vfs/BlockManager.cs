using System.Collections.Generic;
using System.IO;
using System.Linq;

using Practice.Vfs.Exceptions;

namespace Practice.Vfs
{
    public class BlockManager:DisposableObject
    {
        private readonly StreamHelper _streamHelper;

        public BlockManager(Stream dataStream)
        {
            _streamHelper = new StreamHelper(dataStream);
        }

        public BootInfo Boot { get; private set; }

        public int BlockContentSize
        {
            get
            {
                return Boot.BlockSize - DataBlock.HeaderSize;
            }
        }
        
        public void CreateBoot(BootInfo boot)
        {
            Boot = boot;
            byte[] buffer = boot.Persist();
            var point = new DataPoint { Position = 0, Size = boot.BootSize };
            _streamHelper.SaveBytes(point, buffer);
            Commit();
        }

        public void UpdateBoot()
        {
            byte[] buffer = Boot.Persist();
            var point = new DataPoint { Position = 0, Size = Boot.BootSize };
            _streamHelper.SaveBytes(point, buffer);
            Commit();
        }

        public void ReadBoot()
        {
            ReadBootInfo();
        }

        public DataBlock AllocateBlock()
        {
            DataBlock block;
            if (Boot.Space < 0)
            {
                block = AddSpace();
            }
            else
            {
                block = ReadBlock(Boot.Space);
            }

            Boot.Space = block.Next;
            block.State = BlockState.Used;
            block.Type = BlockType.Data;
            block.Previsous = -1;
            block.Next = -1;

            UpdateBoot();

            return block;
        }
        
        public void FreeBlock(List<DataBlock> blocks)
        {
            if (blocks.Count == 0)
            {
                return;
            }

            blocks.Last().Next = Boot.Space;
            blocks[0].Previsous = -1;
            Boot.Space = blocks[0].Id;

            blocks.ForEach(
                block =>
                {
                    block.State = BlockState.Deleted;
                    block.Type = BlockType.Data;
                    SaveBlock(block);
                });
        }

        public DataBlock ReadBlock(int blockId)
        {
            var point = new DataPoint
            {
                Position = Boot.BootSize + blockId * Boot.BlockSize,
                Size = Boot.BlockSize
            };

            var data = _streamHelper.ReadBytes(point);
            var block = DataBlock.Parse(data);
            return block;
        }

        public void SaveBlock(DataBlock block)
        {
            var point = new DataPoint
            {
                Position = Boot.BootSize + block.Id * Boot.BlockSize,
                Size = Boot.BlockSize
            };

            var buffer = block.Persist();
            _streamHelper.SaveBytes(point, buffer);
        }

        public void Commit()
        {
            _streamHelper.Commit();
        }

        protected override void DoDispose()
        {
            _streamHelper.Dispose();
        }

        private void ReadBootInfo()
        {
            var startPoint = new DataPoint { Position = 0, Size = sizeof(byte)*2 + sizeof(int) };
            int bootSize;
            var start = _streamHelper.ReadBytes(startPoint);
            if (start.Length > 0)
            {
                using (var reader = new BytesSeriesReader(start))
                {
                    var recog = reader.ReadBytes(2);
                    if (recog[0] == BootInfo.Recognization[0] || recog[1] == BootInfo.Recognization[1])
                    {
                        bootSize = reader.ReadInt();
                    }
                    else
                    {
                        throw new InvalidBootInfoException("Unkown boot information");
                    }
                }
            }
            else
            {
                throw new InvalidBootInfoException("Empty stream.");
            }

            startPoint.Size = bootSize;

            var data = _streamHelper.ReadBytes(startPoint);
            Boot = BootInfo.Parse(data);
        }

        private DataBlock AddSpace()
        {
            Boot.LastBlock++;
            Boot.Space = Boot.LastBlock;
            var block = DataBlock.Empty(Boot.Space);

            SaveBlock(block);

            return block;
        }
    }
}
