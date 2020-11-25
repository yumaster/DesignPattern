using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 责任链模式
{
    public class Helper
    {
        public static bool GetResult()
        {
            Checker hdOne, hdTwo, hdThree;
            hdOne = new CheckHandOne();
            hdTwo = new CheckHandTwo();
            hdThree = new CheckHandThree();

            hdOne.SetSuccessor(hdTwo);//1->2
            hdTwo.SetSuccessor(hdThree);//2->3

            return hdOne.ProcessRequest("ONE");
        }
    }
}
