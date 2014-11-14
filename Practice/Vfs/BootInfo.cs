using System;

using Practice.Vfs.Exceptions;

namespace Practice.Vfs
{
    public class BootInfo
    {
        public const int MinSize = sizeof(int) * 4 + 1;
        public static readonly byte[] Recognization = {0xb0,0x0f};

        public int BootSize { get; set; }
        public int BlockSize { get; set; }
        public int LastBlock { get; set; }
        public int Space { get; set; }
        public string Information { get; set; }
        
        public static BootInfo Parse(byte[] buffer)
        {
            try
            {
                var info = new BootInfo();
                using (var reader = new BytesSeriesReader(buffer))
                {
                    var r = reader.ReadBytes(2);
                    if (r[0] != Recognization[0] || r[1]!=Recognization[1])
                    {
                        throw new InvalidBootInfoException("Can't recognize the boot.");
                    }

                    info.BootSize = reader.ReadInt();
                    info.BlockSize = reader.ReadInt();
                    info.LastBlock = reader.ReadInt();
                    info.Space = reader.ReadInt();
                    info.Information = reader.ReadString();
                }

                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] Persist()
        {
            using (var writer = new BytesSeriesWriter())
            {
                writer.WriteBytes(Recognization);
                writer.WriteInt(BootSize);
                writer.WriteInt(BlockSize);
                writer.WriteInt(LastBlock);
                writer.WriteInt(Space);
                writer.WriteString(Information);

                return writer.Output;
            }
        }
    }
}
