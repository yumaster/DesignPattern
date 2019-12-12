using System;
using System.Collections.Generic;
using System.Threading.Tasks;
/*
* 什么是享元模式：采用共享技术来避免大量拥有相同内容对象的开销，主要用于减少创建对象的数量，以减少内存占用和提高性能
* 1.根本的思路就是对象的重用
* 2.根本的实现逻辑就是简单工厂+静态缓存
* 
* 
* 1，String 使用了享元模式初始化，比如我们定义两个string 都赋值是may，按道理来说string都是重新开辟内存空间的，
* 2，我们用判断内存地址去判断两个值得内存地址是一样的，原因是string 初始化时去判断堆中是否有may 了，
* 3，享元模式的原理，字符串享元是全局的，不是局部的，在一个进程中，只有一个堆，所以指向同一个地址的字符串是一样的
*/
namespace 享元模式
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            for (int i = 0; i < 5; i++)
            {
                Task.Run(() =>
                {
                    People ch = FlyWeightFactory.GetChineseObject(FlyWeightFactory.LanguageType.Chinese);
                    ch.Say();
                });
            }
            for (int i = 0; i < 5; i++)
            {
                Task.Run(() =>
                {
                    People usa = FlyWeightFactory.GetChineseObject(FlyWeightFactory.LanguageType.USA);
                    usa.Say();
                });
            }

            Console.ReadKey();
        }
    }
    public abstract class People
    {
        public abstract void Say();
    }
    public class Chinese : People
    {
        public Chinese()
        {
            Console.WriteLine($"{this.GetType().Name}被创建了");
        }
        public override void Say()
        {
            Console.WriteLine("中国人说：你好");
        }
    }
    public class USA : People
    {
        public USA()
        {
            Console.WriteLine($"{this.GetType().Name}被创建了");
        }
        public override void Say()
        {
            Console.WriteLine("USA：Hello");
        }
    }





    /// <summary>
    /// 1.根本的思路就是对象的重用
    /// 2.根本的实现逻辑就是简单工厂+静态缓存
    /// </summary>
    public class FlyWeightFactory
    {
        private static Dictionary<LanguageType, People> dic = new Dictionary<LanguageType, People>();//定义字典来保存不同的变量，相同的则取出
        public static object dic_Lock = new object();
        public static People GetChineseObject(LanguageType language)
        {
            People people = null;
            if (!dic.ContainsKey(language))//先判空，多并发不需要排序
            {
                lock (dic_Lock)//线程锁，当多线程并发时，如果没有Lock或造成dic2已经存在KEY的错误
                {
                    if (!dic.ContainsKey(language))
                    {
                        switch (language)
                        {
                            case LanguageType.Chinese:
                                people = new Chinese();
                                break;
                            case LanguageType.USA:
                                people = new USA();
                                break;
                        }
                        dic.Add(language, people);//我们将新创建的对象存储到缓存中
                    }
                }
            }
            return dic[language];
        }
        public enum LanguageType
        {
            Chinese,
            USA
        }
    }
}
