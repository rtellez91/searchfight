using System;

namespace SearchFight
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var item in args)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
