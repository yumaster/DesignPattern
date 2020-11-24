using System;
using System.Collections.Generic;
using System.Text;

namespace 责任链模式
{
    class CheckHandThree:Checker
    {
        //public CheckHandThree(object objPara) : base(objPara)
        //{ }
        /// <summary>
        /// 具体请求处理方法
        /// </summary>
        /// <param name="objPara"></param>
        /// <returns></returns>
        public override bool ProcessRequest(object objPara)
        {
            Console.WriteLine(this.GetType().Name);
            bool ret;
            if (objPara.ToString() == "THREE")//true
            {
                ret = this.successor.ProcessRequest("FOUR");
            }
            else
            {
                ret = false;
            }
            return ret;
        }
    }
}
