using System;
using System.IO;

class ReadData
{
    public void data()
    {
        StreamReader sw = new StreamReader("D:\\First.txt");
        Console.WriteLine("content of the file");
        //string str =sw.ReadLine();
        sw.BaseStream.Seek(0, SeekOrigin.Begin);
        string s = sw.ReadLine();
        //string str = sw.ReadLine();
        while (s != null)
        {
            Console.WriteLine(s);
            s = sw.ReadLine();

        }
        sw.Close();

    }
}
class ReadInFile
{
    static void Main(string[] args)
    {
        ReadData w = new ReadData();
        w.data();

    }
}

