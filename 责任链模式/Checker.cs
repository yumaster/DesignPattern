using System;
using System.Collections.Generic;
using System.Text;

namespace 责任链模式
{
    /// <summary>
    /// 检查者类，充当抽象处理者
    /// </summary>
    abstract class Checker
    {
        protected Checker successor;//定义后继对象
        protected object objPara;//参数
        //public Checker(object objPara)
        //{
        //    this.objPara = objPara;
        //}
        /// <summary>
        /// 设置后继者
        /// </summary>
        /// <param name="successor"></param>
        public void SetSuccessor(Checker successor)
        {
            this.successor = successor;
        }
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="objPara">处理参数</param>
        /// <returns></returns>
        public abstract bool ProcessRequest(object objPara);
    }
}
