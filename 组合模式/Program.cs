using System;
using System.Collections.Generic;
/*
* 组合模式主要用来处理一类具有“容器特征”的对象——即他们在充当对象的同事，又可以作为容器包含其他多个对象。
* 组合模式，将对象组合成树形结构以表示“部分-整体”的层次结构，组合模式使得用户对单个对象和组合对象的使用具有一致性。
*/
namespace 组合模式
{
    
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            People p = new People("开始");//定义一个根容器
            Chinese e = new Chinese("中国人");
            p.Add(e);
            Usa e1 = new Usa("美国人");
            p.Add(e1);//为容器添加节点

            People p2 = new People("地球人P2");//定义一个容器，理解为装在根容器中
            p2.Add(e);//为容器添加节点
            p2.Add(e1);//为容器添加节点


            People p3 = new People("地球人P3");//定义一个容器，理解为装在P2容器中
            p3.Add(e);
            p3.Add(e1);


            p2.Add(p3);
            p.Add(p2);
            p.Say(1);

            Console.ReadKey();
        }
    }

    public abstract class Event
    {
        public abstract void Say(int a);
    }
    public class Chinese : Event
    {
        public string Name { get; set; }
        public Chinese(string name)
        {
            Name = name;
        }
        public override void Say(int a)
        {
            Console.WriteLine(new String('-', a) + Name + "：我们说中文！");
        }
    }
    public class Usa : Event
    {
        public string Name { get; set; }
        public Usa(string name)
        {
            Name = name;
        }
        public override void Say(int a)
        {
            Console.WriteLine(new String('-', a) + Name + "：我们说英文！");
        }
    }
    public class People : Event //people是一个对象也是一个容器，将我们要的组合装起来
    {
        List<Event> list = new List<Event>();//定义可以存储节点的集合
        public string Name { get; set; }
        public People(string name)
        {
            Name = name;
        }
        public override void Say(int a)
        {
            Console.WriteLine(new String('-', a) + Name);
            foreach (var item in list)
            {
                item.Say(a + 2);
            }
        }
        public void Add(Event ev)
        {
            list.Add(ev);
        }
        public void Remove(Event ev)
        {
            list.Remove(ev);
        }
    }

}
