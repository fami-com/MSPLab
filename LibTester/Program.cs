using System;
using MSPLib;

namespace LibTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            MSPLib.Lab.Part1();
            Console.ReadKey();

            MSPLib.Lab.Part2();
            Console.ReadKey();
        }
    }
}
