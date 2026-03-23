using System.IO;
using System;
class WriteData
    {
        public void data()
        {
            StreamWriter sw = new StreamWriter("D:\\First.txt");
            Console.WriteLine("enter the data you want to write in file");
            string str = Console.ReadLine();
            sw.WriteLine(str);
            sw.Flush();
            sw.Close();

        }
    }
    class WriteInFile
{
        static void Main(string[] args)
        {
            WriteData w = new WriteData();
            w.data();

        }
    }

