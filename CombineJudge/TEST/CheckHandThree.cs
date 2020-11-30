using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombineJudge
{
    public class CheckHandThree : BaseClass, IBusiness
    {
        public double ProcessOrder => 3;

        public bool IsPreResult { get; set; }
        public bool IsNextFun { get; set; }
        public bool IsContinueOneSelf { get; set; }
        public bool IsReset { get; set; }

        public bool IsEnable => true;

        public BusinessType BusinessType => BusinessType.车牌识别门禁;

        public string Process(string paras)
        {
            //前置函数处理，预留
            PreProcess(this);
            //---------------------------业务逻辑实现-----------------------
            string result = "";
            if (paras != "THREE")
            {
                result = "参数THREE验证不通过";
                IsReset = false;
                IsContinueOneSelf = true;
            }
            return result;
        }
    }
}
