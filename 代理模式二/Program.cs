using System;
/*
 * 代理模式：为其他对象提供一种代理，以控制对这个对象的访问
 */
namespace 代理模式二
{
    class Program
    {
        /// <summary>
        /// 代理模式提供了一个中介控制对某个对象的访问，现实生活中，我们可能会用支票在市场交易中用来代替现金，支票就是账户中资金的代理
        /// 为其他对象提供一种代理，来帮助达到目的
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //中国人自己去买菜
            //Event c = new Chinese();
            //c.DoSomeThing();

            //代理中国人去买菜
            Event e = new Proxy();
            e.DoSomeThing();
            e.DoSomeThing();
        }
    }
    /// <summary>
    /// 抽象行为
    /// </summary>
    public abstract class Event
    {
        public abstract void DoSomeThing();
    }
    /// <summary>
    /// 中国人
    /// </summary>
    public class Chinese : Event
    {
        /// <summary>
        /// 本来中国人要去买菜
        /// </summary>
        public override void DoSomeThing()
        {
            Console.WriteLine("中国人，去买菜");
        }
    }
    /// <summary>
    /// 代理模式：第三方帮忙达到目的
    /// </summary>
    public class Proxy : Event
    {
        private Chinese chinese = null;
        private Chinese _chinese
        {
            get {
                if (this.chinese == null)
                    chinese = new Chinese();
                return chinese;
            }
        }
        /// <summary>
        /// 日志代理 异常代理 延迟代理 权限代理 单例代理 缓存代理
        /// </summary>
        public bool flag = false;
        /// <summary>
        /// 代理帮忙中国人去买菜
        /// </summary>
        public override void DoSomeThing()
        {
            Console.WriteLine("DoSomeThing start...");
            try
            {
                if (flag)
                {
                    _chinese.DoSomeThing();
                }
                if (!flag)
                {
                    //DoSomeThing 可以在这里实现AOP（日志代理 异常代理 延迟代理 权限代理 单例代理 缓存代理）
                    //初始化AOP写日志，AOP写缓存
                    Console.WriteLine("初始化AOP写日志");
                    Console.WriteLine("初始化AOP写缓存");
                }
                flag = true;
            }
            catch (Exception ex)
            {

            }
            Console.WriteLine("DoSomeThing end...");
        }

    }
}
