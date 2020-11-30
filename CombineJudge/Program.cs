using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombineJudge
{
    class Program
    {
        static void Main(string[] args)
        {
            //车牌识别业务执行
            BusinessProcess.GetInstance().GetResultEvent += Program_GetResultEvent;
            string result = BusinessProcess.GetInstance().StartExec("ONE", BusinessType.车牌识别门禁);
            if (!string.IsNullOrEmpty(result))
            {
                //_isMultipleEntry = false;

                Console.WriteLine(result);
                //return;
            }
            BusinessProcess.GetInstance().GetResultEvent -= Program_GetResultEvent;
            Console.ReadLine();
        }

        private static void Program_GetResultEvent(string result, BusinessType btype)
        {
            if (btype == BusinessType.车牌识别门禁)
            {
                Console.WriteLine(result);
            }
        }
    }
}
