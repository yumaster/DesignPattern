using System;

//简单工厂模式
//定义：是由一个工厂对象决定创建出哪一种产品类的实例。

/*
 * 实例：用工人种蔬菜的例子来说明，蔬菜植物的产品器官有根、茎、叶、花、果等5类，因此按产品器官分类也分5种，分别是根菜类，茎菜类，叶菜类，花菜类以及果蔬类。
 * 我们以根菜类，茎菜类为例，分别用简单工厂模式，工厂模式，抽象工厂模式来实现。
*/
namespace 简单工厂模式
{
    class Program
    {
        //请先从第57行  第1项开始
        static void Main(string[] args)
        {
            //4.此时，如果工人再种植叶菜类植物的话，只需要增加一个继承父类Vegetable的子类来实现，这样的设计满足了类之间的层次关系，每一个类都只负责一件具体的事情。
            //在应用程序中，当工人种植根菜类植物时：如下
            //Vegetable vegetableRoot = new RootVegetable();
            //vegetableRoot.PlantVegetable();
            //5.如果工人由种植根菜类植物变成种植茎菜类植物或者叶菜类植物，那么在应用程序中，诸多代码就要改变。
            //工作量就会增加，此时就需要解耦具体的种植哪类植物和应用程序。这就要引入简单工厂模式了。 见 - 6
            Vegetable vge = VegetableFactory.GetVegetableInstance("根菜类蔬菜");
            vge.PlantVegetable();
            Vegetable vge2 = VegetableFactory.GetVegetableInstance("茎菜类蔬菜");
            vge2.PlantVegetable();
            //7.VegetableFactory解决了客户端直接依赖于具体对象的问题，客户端可以消除直接创建对象的责任，工人只需根据蔬菜的类型，便可进行种植对应的蔬菜。
            //简单工厂模式实现了对责任的分割。


            //缺点：工厂类集中了所有产品创建逻辑，一旦添加新产品就不得不修改工厂逻辑，这样就会造成工厂逻辑过于复杂，并且，它所能创建的类只能是事先考虑到的。对系统的维护和扩展相当不利。
            //下面就需要工厂模式来解决了。---见解决方案【工厂模式】
            Console.ReadKey();
        }
    }

    //6.创建蔬菜工厂类（简单工厂）
    public class VegetableFactory
    {
        /// <summary>
        /// 根据蔬菜类型获取蔬菜实例
        /// </summary>
        /// <param name="vegetable">蔬菜类型</param>
        /// <returns></returns>
        public static Vegetable GetVegetableInstance(string vegetable)
        {
            Vegetable vge = null;
            if (vegetable.Equals("根菜类蔬菜"))
            {
                return new RootVegetable();
            }
            else if (vegetable.Equals("茎菜类蔬菜"))
            {
                return new StemVegetable();
            }
            return vge;
        }
    }


    //1.根菜类的植物
    public class RootVegetable:Vegetable
    {
        public override void PlantVegetable()
        {
            Console.WriteLine("亲，我在种植根菜类的植物");
        }
    }
    //2.茎菜类的植物
    public class StemVegetable : Vegetable
    {
        public override void PlantVegetable()
        {
            Console.WriteLine("亲，我在种植茎菜类的植物");
        }
    }

    //3.由1,2 可以进一步抽象，为它们抽象出共同的父类，蔬菜接口。  //再让1,2去继承此抽象类，并重写抽象父类Vegetable的抽象方法
    public abstract class Vegetable
    {
        public abstract void PlantVegetable();
    }
}
