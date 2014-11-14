namespace Practice.Vfs
{
    public interface IStorage
    {
        int Store(string data);

        int Store(byte[] data);

        bool Update(int address, string data);

        bool Update(int address, byte[] data);

        string Delete(int address);

        byte[] Delete(int address, bool returnBytes);

        string Read(int address);

        byte[] Read(int address, bool returnBytes);

        void AppendCustomBootInfo(string info);

        string RemoveCustomBootInfo();

        string ReadCustomBootInfo();
    }
}
