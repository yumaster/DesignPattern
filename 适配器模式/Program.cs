using System;
/*
 * Adapter适配器模式是将两个不兼容的类组合在一起使用
 */
namespace 适配器模式
{
    //保留现有类所提供的服务，向客户提供接口，以满足客户的期望
    class Program
    {
        static void Main(string[] args)
        {
            //正常的接口实现方法
            ISpeak speak = new Chinese();
            speak.Say();
            //第一种。Adapter适配器模式实现方法
            EnglishAdapter adapter = new EnglishAdapter();
            adapter.Say();
            //第二种：Adapter中间类继承实现
            ISpeak speak1 = new JapanAdapter();
            speak1.Say();

            Console.ReadKey();
        }
    }
    //抽象接口
    public interface ISpeak
    {
        void Say();
    }
    //1.接口具体实现依赖于抽象接口
    public class Chinese : ISpeak
    {
        public void Say()
        {
            Console.WriteLine("中文");
        }
    }
    //2.第一种：Adapter适配器模式是将两个不兼容的类组合在一起使用，接口具体实现依赖于抽象接口
    public class EnglishAdapter : ISpeak
    {
        private English english = new English();
        public void Say()
        {
            english.SayEnglish();
        }
    }
    public class English
    {
        public void SayEnglish()
        {
            Console.WriteLine("英语");
        }
    }
    //3.第二种：Adapter适配器模式中间类继承实现
    public class JapanAdapter : Japan, ISpeak
    {
        public void Say()
        {
            SayJapan();
        }
    }
    public class Japan
    {
        public void SayJapan()
        {
            Console.WriteLine("日文");
        }
    }

}
