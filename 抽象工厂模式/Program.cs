using System;
using System.Configuration;
//抽象工厂模式
//定义：为创建一组相关或相互依赖的对象提供一个接口，而且无需指定他们的具体类。

/*
 *实例：用工人种蔬菜的例子来说明，蔬菜植物的产品器官有根、茎、叶、花、果等5类，因此按产品器官分类也分5种，分别是根菜类，茎菜类，叶菜类，花菜类以及果蔬类。 
 *我们以根菜类，茎菜类为例，分别用简单工厂模式，工厂模式，抽象工厂模式来实现。 
 * 
 * 缺点:难以支持新种类的产品，难以扩展抽象工厂以生产新种类的产品。这是因为抽象工厂接口确定了可以被创建的产品集合，支持新种类的产品就需要扩展该工厂接口
 * 这将涉及抽象工厂类及所有子类的改变，这样也就违背了“开放--封闭”原则   对扩展开放，对修改关闭（开闭原则）
*/
namespace 抽象工厂模式
{
    /*
     * 随着科技的发展，工人逐步要种植转基因与非转基因食品了。在以前的蔬菜种类上又增加了一个层次，这时候无法将其作为一个层次来解决。
     * 所以必须采用抽象工厂的方式来解决。依然以种植根菜类蔬菜和茎菜类蔬菜为例。
     * 此时在蔬菜下有根菜类蔬菜类型和茎菜类蔬菜类型，在根菜类蔬菜类型下有转基因根菜类型和非转基因根菜类型，在茎菜类蔬菜类型下有转基因茎菜类蔬菜和非转基因茎菜类蔬菜
     */
    class Program
    {
        static void Main(string[] args)
        {
            //AbstractFactory factory = AbstractFactory.GetAbstractFactoryInstance("转基因工厂");//如果想种植非转基因根菜类蔬菜，只需修改

            AbstractFactory factory = AbstractFactory.GetAbstractFactoryInstance();
            //种植转基因类型蔬菜
            RootVegetable rootVegetable = factory.GetRootVegetableInstance();
            rootVegetable.PlantRootVegetable();


            StemVegetable stemVegetable = factory.GetStemVegetableInstance();
            stemVegetable.PlantStemVegetable();
            Console.ReadKey();
            //Console.WriteLine("Hello World!");
        }
    }


    #region 1.定义蔬菜类接口及抽象蔬菜类
    interface Vegetable { }
    public abstract class RootVegetable : Vegetable
    {
        //根菜
        public abstract void PlantRootVegetable();
    }
    public abstract class StemVegetable : Vegetable
    {
        //茎菜
        public abstract void PlantStemVegetable();
    }
    public class GMFRootVegetable : RootVegetable
    {
        public override void PlantRootVegetable()
        {
            Console.WriteLine("亲，我在种植转基因类根菜");
        }
    }
    public class NonGMFRootVegetable : RootVegetable
    {
        public override void PlantRootVegetable()
        {
            Console.WriteLine("亲，我在种植非转基因类根菜");
        }
    }
    public class GMFStemVegetable : StemVegetable
    {
        public override void PlantStemVegetable()
        {
            Console.WriteLine("亲，我在种植转基因类茎菜");
        }
    }
    public class NonGMFStemVegetable : StemVegetable
    {
        public override void PlantStemVegetable()
        {
            Console.WriteLine("亲，我在种植非转基因类茎菜");
        }
    }
    #endregion
    /*
     * 2.由于现在种植转基因和非转基因蔬菜，可以创建转基因蔬菜工厂与非转基因蔬菜工厂，在转基因蔬菜工厂下可以种植转基因根菜类蔬菜和转基因茎菜类蔬菜，
     * 在非转基因蔬菜工厂下可以种植非转基因根菜类蔬菜和非转基因茎菜类蔬菜
     * 如果现在工人只要求是种植转基因蔬菜，代码如下：
     */
     /// <summary>
     /// 抽象工厂
     /// </summary>
    public abstract class AbstractFactory
    {
        //根菜类蔬菜
        public abstract RootVegetable GetRootVegetableInstance();
        //茎菜类蔬菜
        public abstract StemVegetable GetStemVegetableInstance();

        public static AbstractFactory GetAbstractFactoryInstance()
        {
            //AbstractFactory instance = null;
            //if (factoryName.Equals("转基因工厂"))
            //{
            //    instance = new GMFVegetableFactory();
            //}
            //else if (factoryName.Equals("非转基因工厂"))
            //{
            //    instance = new NonGMFVegetableFactory();
            //}
            //return instance;
            //利用.NET反射机制来进一步修改我们的程序，这时需要配置文件
            string factoryName = ConfigurationManager.AppSettings["factoryName"];
            AbstractFactory instance = null;
            //if (!string.IsNullOrEmpty(factoryName))
            if(!factoryName.Equals(string.Empty))
            {
                instance = (AbstractFactory)System.Reflection.Assembly.Load("抽象工厂模式").CreateInstance(factoryName);
            }
            return instance;
        }
    }
    /// <summary>
    /// 转基因工厂
    /// </summary>
    public class GMFVegetableFactory:AbstractFactory
    {
        /// <summary>
        /// 转基因根菜实例
        /// </summary>
        /// <returns></returns>
        public override RootVegetable GetRootVegetableInstance()
        {
            return  new GMFRootVegetable();
        }
        /// <summary>
        /// 转基因茎菜实例
        /// </summary>
        /// <returns></returns>
        public override StemVegetable GetStemVegetableInstance()
        {
            return new GMFStemVegetable();
        }
    }
    /// <summary>
    /// 非转基因工厂
    /// </summary>
    public class NonGMFVegetableFactory:AbstractFactory
    {
        /// <summary>
        /// 非转基因根菜实例
        /// </summary>
        /// <returns></returns>
        public override RootVegetable GetRootVegetableInstance()
        {
            return new NonGMFRootVegetable();
        }
        /// <summary>
        /// 非转基因茎菜实例
        /// </summary>
        /// <returns></returns>
        public override StemVegetable GetStemVegetableInstance()
        {
            return new NonGMFStemVegetable();
        }
    }
}
