using System;
using System.Collections.Generic;
using System.Text;

namespace 责任链模式
{
    class Program
    {
        static void Main(string[] args)
        {
            bool ret = Helper.GetResult();

            Console.WriteLine(ret);
            Console.ReadKey();
            Console.ReadLine();
        }
    }
}
