using System;

using Practice.Vfs.Exceptions;

namespace Practice.Vfs
{
    public class DataBlock
    {
        public const int HeaderSize = sizeof(int) * 3 + sizeof(short) * 3 + 2;
        public static readonly byte[] Recognization = { 0xb0, 0xda };
        
        public int Id { get; private set; }

        public BlockType Type { get; set; }

        public BlockState State { get; set; }

        public int Previsous { get; set; }

        public int Next { get; set; }

        public short Size { get; set; }

        public byte[] Content { get; set; }

        public static DataBlock Empty(int blockId)
        {
            var block = new DataBlock
            {
                Id = blockId,
                Type = BlockType.Data,
                State = BlockState.Empty,
                Previsous = -1,
                Next = -1,
                Size = HeaderSize,
                Content = new byte[0]
            };

            return block;
        }

        public static DataBlock Parse(byte[] buffer)
        {
            try
            {
                if (buffer.Length == 0)
                {
                    return null;
                }

                using (var reader = new BytesSeriesReader(buffer))
                {
                    byte[] recog = reader.ReadBytes(2);
                    if (recog[0] != Recognization[0] || recog[1] != Recognization[1])
                    {
                        return null;
                    }
                    var block = new DataBlock
                    {
                        Id = reader.ReadInt(),
                        Type = (BlockType)reader.ReadShort(),
                        State = (BlockState)reader.ReadShort(),
                        Previsous = reader.ReadInt(),
                        Next = reader.ReadInt(),
                        Size = reader.ReadShort()
                    };
                    block.Content = reader.ReadBytes(block.Size);

                    return block;
                }
            }
            catch (Exception ex)
            {
                throw new DataBlockParseException(ex.Message, ex);
            }
        }

        public byte[] Persist()
        {
            using (var writer = new BytesSeriesWriter())
            {
                writer.WriteBytes(Recognization);
                writer.WriteInt(Id);
                writer.WriteShort((short)Type);
                writer.WriteShort((short)State);
                writer.WriteInt(Previsous);
                writer.WriteInt(Next);
                writer.WriteShort(Size);
                writer.WriteBytes(Content);

                return writer.Output;
            }
        }
    }
}
