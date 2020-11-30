using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace CombineJudge
{
    /// <summary>
    /// 业务起点
    /// </summary>
    public class BusinessProcess
    {
        #region 单例
        private static BusinessProcess uniqueInstance;
        private BusinessProcess()
        {
        }

        private static object BusinessProcess_Lock = new object();
        public static BusinessProcess GetInstance()
        {
            if (uniqueInstance == null)
            {
                lock (BusinessProcess_Lock)
                {
                    uniqueInstance = new BusinessProcess();
                }
            }
            return uniqueInstance;
        }
        #endregion

        public delegate void GetResultHandle(string result, BusinessType btype);
        public event GetResultHandle GetResultEvent;
        /// <summary>
        /// 上一个业务返回的参数
        /// </summary>
        private string _paras = "";
        /// <summary>
        /// 开始执行，根据接口设置调整执行顺序
        /// </summary>
        /// <param name="paras">传递参数</param>
        /// <param name="businesstype">业务类型</param>
        /// <returns></returns>
        public string StartExec(string paras, BusinessType businesstype)
        {
            _paras = "";
            Dictionary<BusinessType, List<TypeObj>> dict = getAllFunction();
            List<TypeObj> tlist = new List<TypeObj>();
            if (dict.ContainsKey(businesstype))
            {
                tlist = dict[businesstype];
            }

            for (int i = 0; i < tlist.Count; i++)
            {
                object obj = Activator.CreateInstance(tlist[i].type);//创建一个obj对象
                MethodInfo mi = tlist[i].type.GetMethod("Process");
                List<object> olist = new List<object>();
                if (tlist[i].ispreresult)
                {
                    olist.Add(_paras);
                }
                else
                {
                    olist.Add(paras);
                }
                _paras = mi.Invoke(obj, olist.ToArray()) as string;

                //是否继续执行自己
                string str1 = tlist[i].type.GetProperty("IsContinueOneSelf").GetValue(obj, null).ToString();
                bool Iscontinueoneself = string.IsNullOrEmpty(str1) ? false : bool.Parse(str1);

                //是否执行下一个接口实现
                string str2 = tlist[i].type.GetProperty("IsNextFun").GetValue(obj, null).ToString();
                bool isnextfun = string.IsNullOrEmpty(str2) ? false : bool.Parse(str2);

                //是否重头开始判断
                string str3 = tlist[i].type.GetProperty("IsReset").GetValue(obj, null).ToString();
                bool Isreset = string.IsNullOrEmpty(str3) ? false : bool.Parse(str3);

                if (Iscontinueoneself)
                {
                    i--;
                    if (GetResultEvent != null)
                    {
                        if (i < 0)
                        {
                            GetResultEvent(_paras, tlist[0].businesstype);
                        }
                        else
                        {
                            GetResultEvent(_paras, tlist[i].businesstype);
                        }
                    }
                    Thread.Sleep(3000);
                }
                if (Isreset)
                {
                    i = -1;
                    if (GetResultEvent != null)
                    {
                        GetResultEvent(_paras, tlist[i + 1].businesstype);
                    }
                    Thread.Sleep(3000);
                }
                if (!isnextfun)
                {
                    break;
                }
                Thread.Sleep(100);
            }
            return _paras;
        }

        /// <summary>
        /// 获取所有实现接口类
        /// </summary>
        /// <returns></returns>
        private Dictionary<BusinessType, List<TypeObj>> getAllFunction()
        {
            Dictionary<BusinessType, List<TypeObj>> dic = new Dictionary<BusinessType, List<TypeObj>>();
            //接口实现类集合
            List<TypeObj> tlist = new List<TypeObj>();

            Assembly ass = Assembly.GetAssembly(typeof(IBusiness));
            Type[] types = ass.GetTypes();
            foreach (Type item in types)
            {
                if (item.IsInterface) continue;//判断是否是接口
                Type[] ins = item.GetInterfaces();
                foreach (Type ty in ins)
                {
                    if (ty == typeof(IBusiness))
                    {
                        object obj = Activator.CreateInstance(item);//创建一个obj对象
                        //执行顺序
                        string str = item.GetProperty("ProcessOrder").GetValue(obj, null).ToString();
                        double processorder = string.IsNullOrEmpty(str) ? 0 : double.Parse(str);

                        //上一个实现返回值
                        string str2 = item.GetProperty("IsPreResult").GetValue(obj, null).ToString();
                        bool ispreresult = string.IsNullOrEmpty(str2) ? false : bool.Parse(str2);

                        //业务是否启用
                        string str5 = item.GetProperty("IsEnable").GetValue(obj, null).ToString();
                        bool isenable = string.IsNullOrEmpty(str5) ? false : bool.Parse(str5);

                        //业务类型
                        string str3 = item.GetProperty("BusinessType").GetValue(obj, null).ToString();
                        BusinessType businesstype = string.IsNullOrEmpty(str3) ? BusinessType.无 : (BusinessType)Enum.Parse(typeof(BusinessType), str3);

                        if (isenable)
                        {
                            tlist.Add(new TypeObj() { type = item, processorder = processorder, ispreresult = ispreresult, businesstype = businesstype });
                        }
                    }
                }
            }

            foreach (IGrouping<BusinessType, TypeObj> group in tlist.GroupBy(x => x.businesstype))
            {
                dic.Add(group.Key, group.OrderBy(a => a.processorder).ToList());
            }
            return dic;
        }
    }

    /// <summary>
    /// 反射类集合（排序使用）
    /// </summary>
    public class TypeObj
    {
        public Type type { get; set; }

        public double processorder { get; set; }

        public bool ispreresult { get; set; }

        public BusinessType businesstype { get; set; }
    }
}
