using System.Diagnostics;

class Program
{
    static void Main()
    {
        Stopwatch sw = Stopwatch.StartNew();
        sw.Start();

        ReadFromExternalWithoutInMemory();

        sw.Stop();
        Console.WriteLine(sw.ElapsedMilliseconds);
        sw.Restart();

        ReadFromExternalWithInMemory();

        sw.Stop();
        Console.WriteLine(sw.ElapsedMilliseconds);
    }

    static void ReadFromExternalWithoutInMemory()
    {
        BinaryReader binaryReader = new(new FileStream("map.osm", FileMode.Open));

        for (int i = 0; i < binaryReader.BaseStream.Length; i++)
        {
            binaryReader.ReadByte();
        }

        binaryReader.Close();
    }

    static void ReadFromExternalWithInMemory()
    {
        BinaryReader binaryReader = new(new FileStream("map.osm", FileMode.Open));

        BinaryReader binaryReaderInMemory = new(new MemoryStream(binaryReader.ReadBytes((int)binaryReader.BaseStream.Length)));

        for (int i = 0; i < binaryReaderInMemory.BaseStream.Length; i++)
        {
            binaryReaderInMemory.ReadByte();
        }

        binaryReader.Close();
    }
}
