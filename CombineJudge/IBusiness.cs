using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombineJudge
{
    public interface IBusiness
    {
        /// <summary>
        /// 执行顺序
        /// </summary>
        double ProcessOrder { get; }

        /// <summary>
        /// 是否需要上一个业务返回的值作为当前业务的入参
        /// </summary>
        bool IsPreResult { get; set; }

        /// <summary>
        /// 是否继续下一个接口实现
        /// </summary>
        bool IsNextFun { get; set; }

        /// <summary>
        /// 是否继续执行当前业务
        /// </summary>
        bool IsContinueOneSelf { get; set; }

        /// <summary>
        /// 是否重头开始
        /// </summary>
        bool IsReset { get; set; }

        /// <summary>
        /// 当前业务是否启用
        /// </summary>
        bool IsEnable { get; }

        /// <summary>
        /// 接口类型，用于区分执行接口实现类
        /// </summary>
        BusinessType BusinessType { get; }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="paras">参数</param>
        /// <returns></returns>
        string Process(string paras);
    }
}
