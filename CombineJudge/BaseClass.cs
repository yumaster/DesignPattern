using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombineJudge
{
    /// <summary>
    /// 父类,前置业务
    /// </summary>
    public class BaseClass
    {
        /// <summary>
        /// 负责处理所有公共逻辑
        /// </summary>
        protected void PreProcess(IBusiness business)
        {
            business.IsContinueOneSelf = false;
            business.IsNextFun = true;
            business.IsReset = false;
            business.IsPreResult = false;
        }
    }
}
