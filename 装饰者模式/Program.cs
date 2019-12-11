using System;

/*
 * 一、装饰者模式：装饰模式指的是在不必改变原类文件和使用继承的情况下，动态地扩展一个对象的功能
 * 二、在以上代码中 我们是中国人是根本行为，我们给中国人装饰我会说英语、日语。这三种行为，那按正常的理解来说，这是多态，
 * 那我们对People中的Say我们是不是需要有三种实现方法呢？三种还是比较好解决，但是如果我们有N种呢？难道我们要有N个实现类吗？
 * 1.我们抽象PeoPle这个类，还有抽象的Say方法，这时PeoPle就可以有多国人的实现，我们使用一种中国人，假设，这是我们现有的代码，
 * 但根据要求，我们需要给中国人添加我会说英语，我会说日语，那该怎么办？根据以往想法，一般设计如下：
 */
namespace 装饰者模式
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //People p = new English();
            //p.Say();
            //People p1 = new Japan();
            //p1.Say();
            //People p2 = new JE();
            //p2.Say();
            People p = new Chinese();//没有扩展的实现
            Decorator d = new English(p);//将会说英语的扩展给中国人
            d.Say();
            Console.WriteLine("--------------------------------");
            Decorator d1 = new Japan(p);
            d1.Say();
            Console.WriteLine("--------------------------------");


            English e = new English(p);//将会说英语和日语的扩展给中国人，这里是装饰者模式的核心，当我们存在这里三种可能的组合时，正常逻辑可能是每一种都实现一次，这样就存在三个实现子类
            //但是如果可能组合有几十种上百种怎么办，这是我们可以使用装饰者模式，在代码中，我们只是实现英语和日语的两种可能，但是我们可以实现组合得出第三种可能，这样代码就可以减少很多。
            Japan j = new Japan(e);
            j.Say();



            Console.ReadKey();

        }
    }
    public abstract class People
    {
        public abstract void Say();
    }
    public class Chinese : People
    {
        public override void Say()    //根本的行为
        {
            Console.WriteLine("我是中国人");
        }
    }
    public class Decorator : People
    {
        public People people;

        public Decorator(People p)
        {
            this.people = p;
        }

        public override void Say()
        {
            people.Say();
            //Console.WriteLine("我是会说英语");
        }
    }


    public class English : Decorator
    {
        public English(People p):base(p)
        {

        }
        public override void Say()
        {
            base.Say();
            //添加新的行为，动态地扩展一个对象的功能
            SayEnglish();
        }
        /// <summary>
        /// 新的行为方法
        /// </summary>
        public void SayEnglish()
        {
            Console.WriteLine("我会说英语");
        }
    }

    public class Japan : Decorator
    {
        public Japan(People p) : base(p)
        {

        }
        public override void Say()
        {
            base.Say();
            //Console.WriteLine("我是会说日语");
            SayJan();
        }
        public void SayJan()
        {
            Console.WriteLine("我会说日语");
        }
    }
    //public class JE : Chinese
    //{
    //    public override void Say()
    //    {
    //        base.Say();
    //        Console.WriteLine("我是会说英语和日语");
    //    }
    //}
}
