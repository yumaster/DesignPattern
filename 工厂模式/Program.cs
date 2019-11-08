using System;
using System.Configuration;
//工厂模式
//定义：定义一个用于创建对象的接口，让子类决定实例化哪一个类。

/*
 *实例：用工人种蔬菜的例子来说明，蔬菜植物的产品器官有根、茎、叶、花、果等5类，因此按产品器官分类也分5种，分别是根菜类，茎菜类，叶菜类，花菜类以及果蔬类。 
 *我们以根菜类，茎菜类为例，分别用简单工厂模式，工厂模式，抽象工厂模式来实现。 
*/
namespace 工厂模式
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * 工厂模式之所以可以解决简单工厂的模式，是因为它的实现把具体产品的创建推迟到子类中，此时工厂类不在负责所有产品的创建
             * 而只是给出具体工厂必须实现的接口，这样工厂方法模式就可以允许系统不修改工厂类逻辑的情况下来添加新产品。
             * 如果此时在种植叶菜类蔬菜，只需增加叶菜类蔬菜工厂和叶菜类蔬菜类，而不用像简单工厂模式中那样去修改工厂类中的实现。代码如下：
             */
            //VegetableFactory factory = new RootVegetableFactory();

            //此时如果想种植茎菜类蔬菜，只需把RootVegetableFactory修改为StemVegetableFactory
            VegetableFactory factory = new StemVegetableFactory();
            Vegetable vegetable = factory.GetVegetableInstance();
            vegetable.PlantVegetable();

            Console.WriteLine("------------------------------");

            /*
             * 注意：1.Assembly.Load("XXX")的使用说明如下：XXX并不是命名空间，而是程序集名称，也就是dll的名称，可以在bin目录里面查看。
             * 即：Assembly.Load("程序集").CreateInstance("命名空间.类");
             * 2.注意CreateInstance()一定是命名空间.类，否则创建的是类为空
             */

            string factoryName = ConfigurationManager.AppSettings["factoryName"];
            VegetableFactory vf = (VegetableFactory)System.Reflection.Assembly.Load("工厂模式").CreateInstance(factoryName);
            Vegetable vge = vf.GetVegetableInstance();
            vge.PlantVegetable();
            // 缺点：在工厂模式中，一个工厂只能创建一种产品，如果要求工厂创建多种产品，工厂模式就不好用了。下面就需要抽象工厂模式来解决了。
            Console.ReadKey();
            //Console.WriteLine("Hello World!");
        }
    }


    /*
     * 由于简单工厂模式系统难以扩展，一旦添加新的蔬菜类型就不得不修改VegetableFactory中的方法。这样就会造成简单工厂的实现逻辑过于复杂
     * 然而工厂模式可以解决简单工厂模式中存在的这个问题。
     * 如果此时引入工厂模式，由于每一种类型的蔬菜就是工厂种植的产品，有两种类型的蔬菜，就需要两个工厂去种植，代码如下
     */
    //1.根菜类
    public class RootVegetableFactory : VegetableFactory
    {
        public override Vegetable GetVegetableInstance()
        {
            return new RootVegetable();
        }
    }
    //2.茎菜类
    public class StemVegetableFactory : VegetableFactory
    {
        public override Vegetable GetVegetableInstance()
        {
            return new StemVegetable();
        }
    }
    //3.这两个工厂是平级的，在此基础上抽象出一个公共的接口,然后让1、2去继承此抽象工厂类，并实现抽象方法；
    public abstract class VegetableFactory
    {
        public abstract Vegetable GetVegetableInstance();
    }






    /******************************简单工厂模式中的类*******************************/

    //1.根菜类的植物
    public class RootVegetable : Vegetable
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
