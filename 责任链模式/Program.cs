using System;
using System.Collections.Generic;
using System.Text;

namespace 责任链模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Checker hdOne, hdTwo, hdThree;
            hdOne = new CheckHandOne();
            hdTwo = new CheckHandTwo();
            hdThree = new CheckHandThree();

            hdOne.SetSuccessor(hdTwo);//1->2
            hdTwo.SetSuccessor(hdThree);//2->3

            bool ret = hdOne.ProcessRequest("ONE");

            Console.WriteLine(ret);
            Console.ReadKey();
            Console.ReadLine();
        }
    }
}
