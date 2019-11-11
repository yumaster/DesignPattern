using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

/*
 * 单例模式
 * 意图：保证一个类仅有一个实例，并提供一个访问它的全局访问点
 * 主要解决：一个全局使用的类频繁地创建与销毁
 * 何时使用：当你想控制实例数目，节省系统资源的时候
 * 如何解决：判断系统中是否已经有这个单例，如果有则返回，如果没有则创建
 * 关键代码：构造函数是私有的
 * 优点：在内存里只有一个实例，减少了内存的开销，尤其是频繁的创建和销毁实例；避免对资源的多重占用（比如写文件操作）
 * 缺点：没有接口，不能继承，与单一职责原则冲突，一个类应该只关心内部逻辑，而不关心外面怎么样来实例化。
 */
namespace 单例模式
{
    class Program
    {
        /*
         * 单例模式，保证整个进程中对象只被实例化一次，常驻内存，根据这个特点：单例模式有三种写法 
         * 普通的类型是需要的时候初始化，使用完被GC回收，根静态不一样。
         */
        static void Main(string[] args)
        {
            #region 普通调用
            //Singleton singleton = Singleton.CreateInstance();//创建一个单例对象
            //for (int i = 0; i < 10; i++)
            //{
            //    Singleton singletonss = Singleton.CreateInstance();
            //    singletonss.Show();
            //}
            #endregion
            #region 第一种写法：静态方法  多线程调用
            //多线程测试调用，结果也是公用一个对象，只调用一次构造函数
            //List<IAsyncResult> asyncResults = new List<IAsyncResult>();
            //for (int i = 0; i < 5; i++)
            //{
            //    asyncResults.Add(new Action(() =>
            //    {
            //        Singleton singleton = Singleton.CreateInstance();
            //        singleton.Show();
            //    }).BeginInvoke(null, null));//会启动一个异步多线程的调用
            //}
            #endregion

            #region 第二种写法：静态构造单例  多线程调用
            List<IAsyncResult> asyncResults = new List<IAsyncResult>();
            for (int i = 0; i < 5; i++)
            {
                asyncResults.Add(new Action(() =>
                {
                    SingletonSecond singleton = SingletonSecond.CreateInstance();
                    singleton.Show();
                }).BeginInvoke(null, null));//会启动一个异步多线程的调用
            }
            #endregion

            #region 第三种写法：静态变量单例  多线程调用
            //List<IAsyncResult> asyncResults = new List<IAsyncResult>();
            //for (int i = 0; i < 5; i++)
            //{
            //    asyncResults.Add(new Action(() =>
            //    {
            //        SingletonThird singleton = SingletonThird.CreateInstance();
            //        singleton.Show();
            //    }).BeginInvoke(null, null));//会启动一个异步多线程的调用
            //}
            #endregion


            Console.ReadKey();
            //Console.WriteLine("----------------------------");
        }
    }
    /// <summary>
    /// 第一种写法：静态方法
    /// </summary>
    public class Singleton
    {
        //让构造函数为 private，这样该类就不会被实例化
        private Singleton()
        {
            Console.WriteLine("{0}被创建了，线程ID{1}！", this.GetType().Name, Thread.CurrentThread.ManagedThreadId);
        }
        public static Singleton singleton = null;
        public static object lockObject = new object();
        public static Singleton CreateInstance()
        {
            if (singleton == null)//保证对象初始化之后的所有线程，不需要等待锁
            {
                Console.WriteLine("准备进入Lock");
                lock (lockObject)//保证只有一个进程去判断
                {
                    if (singleton == null)//保证为空的时候才创建
                    {
                        singleton = new Singleton();
                    }
                }
            }
            return singleton;
        }
        public void Show()
        {
            Console.WriteLine("显示！！！");
        }
    }
    /// <summary>
    /// 第二种写法：静态构造单例
    /// </summary>
    public class SingletonSecond
    {
        public static SingletonSecond singleton = null;
        //让构造函数为 private，这样该类就不会被实例化
        private SingletonSecond()
        {
            Console.WriteLine("{0}被创建了,线程ID{1}！", this.GetType().Name, Thread.CurrentThread.ManagedThreadId);
        }
        /// <summary>
        /// 1.静态构造函数:由CLR保证，在第一次使用这个类型之前，调用而且只调用一次
        /// </summary>
        static SingletonSecond()
        {
            singleton = new SingletonSecond();
        }
        public static SingletonSecond CreateInstance()
        {
            return singleton;
        }
        public void Show()
        {
            Console.WriteLine("显示！！！");
        }
    }
    /// <summary>
    /// 第三种写法：静态变量单例
    /// </summary>
    public class SingletonThird
    {
        //让构造函数为 private，这样该类就不会被实例化
        private SingletonThird()
        {
            Console.WriteLine("{0}被创建了,线程ID{1}！", this.GetType().Name, Thread.CurrentThread.ManagedThreadId);
        }
        //静态变量：会在类型第一次使用时候初始化，而且只初始化一次
        private static SingletonThird singleton = new SingletonThird();
        public static SingletonThird CreateInstance()
        {
            return singleton;
        }
        public void Show()
        {
            Console.WriteLine("显示！！！");
        }
    }
}
