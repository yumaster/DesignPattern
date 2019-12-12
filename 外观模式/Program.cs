using System;
/*
 * 外观模式：为子系统中的一组接口提供一个一致的界面，定义一个高层接口，这个接口使得这一子系统更加容易使用
 */
namespace 外观模式
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //不存在外观类时我们的调用方法是这样的，如下代码
            //People p = new People("中国人");
            //Event e = new Event();
            //Console.Write(p.people);
            //e.Say();

            //外观系统，和以上代码比较，我们可以知道：外观模式提供了一个统一的接口，用来访问子系统中的一群接口，而不用实例过多，同时也降低了客户端和子系统之间的紧耦合。
            Facade fa = new Facade("中国人");
            fa.WhoEvent();

            Facade fa1 = new Facade("美国人");
            fa1.WhoEvent();

            Console.ReadKey();
        }
    }
    /// <summary>
    /// 外观类
    /// </summary>
    public class Facade
    {
        private Event ev { get; set; }
        private People pe { get; set; }
        public Facade(string people)
        {
            ev = new Event();
            pe = new People(people);
        }
        public void WhoEvent()
        {
            Console.WriteLine(pe.people);
            ev.Say();
            ev.Eat();
        }

    }
    /// <summary>
    /// 相当于行为子系统
    /// </summary>
    public class Event
    {
        public void Say()
        {
            Console.Write("说话和");
        }
        public void Eat()
        {
            Console.WriteLine("吃饭");
        }
    }
    public class People
    {
        public string people { get; set; }
        public People(string p)
        {
            this.people = p;
        }
    }

}
