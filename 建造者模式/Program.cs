using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 意图：将一个复杂的构建与其表示相分离，使得同样的构建过程可以创建出不同的表示。
 * 主要解决：主要解决在软件系统中，有时候面临着“一个复杂对象”的创建工作，其通常由各个部分的子对象用一定的算法构成；
 * 由于需求的变化，这个复杂对象的各个部分经常面临着剧烈的变化，但是将它们组合在一起的算法却相对稳定。
 * 何时使用：一些基本部件不会变，而其组合经常变化的时候。
 * 如何解决：将变与不变分离开
 * 关键代码：建造者：创建和提供实例  导演：管理建造出来的实例的依赖关系
 * 应用实例：去肯德基，汉堡、可乐、薯条、炸鸡翅等是不变的，而其组合是经常变化的，生成出所谓的套餐；
 * 优点：1.建造者独立，易扩展。2.便于控制细节风险
 * 缺点：1.产品必须有共同点，范围有限制。2.如内部变化复杂，会有很多的建造类
 * 使用场景：1.需要生成的对象具有复杂的内部结构。2.需要生成的对象内部属性本身相互依赖
 * 注意事项：与工厂模式的区别是：建造者模式更加关注与零件装配的顺序
 */
namespace 建造者模式
{
    class Program
    {
        /// <summary>
        /// 步骤7 使用MealBuilder 来演示建造者模式
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            MealBuilder builder = new MealBuilder();
            Meal vegMeal = builder.PrepareVegMeal();
            Console.WriteLine("素套餐");
            vegMeal.ShowItems();
            Console.WriteLine("总价：" + vegMeal.GetCost());

            Meal nonVegMeal = builder.PrepareNonVegMeal();
            Console.WriteLine("荤套餐");
            nonVegMeal.ShowItems();
            Console.WriteLine("总价：" + nonVegMeal.GetCost());

            Console.ReadKey();
        }
    }
    /// <summary>
    /// 包装
    /// </summary>
    public interface Packing
    {
        string Pack();
    }
    /// <summary>
    /// 物品
    /// </summary>
    public interface Item
    {
        string Name();
        float Price();
        Packing Packing();
    }
    //步骤2 创建实现Packing接口的实体类
    /// <summary>
    /// 封装式(包装纸)
    /// </summary>
    public class Wrapper : Packing
    {
        public string Pack()
        {
            return "封装式包装";
        }
    }
    /// <summary>
    /// 瓶装式（瓶子杯子）
    /// </summary>
    public class Bottle : Packing
    {
        public string Pack()
        {
            return "瓶装式包装";
        }
    }
    //步骤3 创建实现Item接口的抽象类，该类提供了默认的功能
    public abstract class Burger : Item
    {
        public abstract string Name();
        public abstract float Price();
        public Packing Packing()
        {
            return new Wrapper();
        }
    }
    public abstract class ColdDrink : Item
    {
        public abstract string Name();
        public abstract float Price();
        public Packing Packing()
        {
            return new Bottle();
        }
    }
    //步骤4 创建扩展了Burger和ColdDrink的实体类
    public class VegBurger : Burger
    {
        public override string Name()
        {
            return "蔬菜汉堡";
        }
        public override float Price()
        {
            return 25.0f;
        }
    }
    public class ChickenBurger : Burger
    {
        public override string Name()
        {
            return "鸡肉汉堡";
        }
        public override float Price()
        {
            return 50.5f;
        }
    }
    public class Coca : ColdDrink
    {
        public override string Name()
        {
            return "可口可乐";
        }
        public override float Price()
        {
            return 30.0f; 
        }
    }
    public class Pepsi : ColdDrink
    {
        public override string Name()
        {
            return "百事可乐";
        }
        public override float Price()
        {
            return 35.0f;
        }
    }
    //步骤5 创建一个Meal类，带有上面定义的Item对象  套餐
    public class Meal
    {
        private List<Item> items = new List<Item>();
        public void AddItem(Item item)
        {
            items.Add(item);
        }
        public float GetCost()
        {
            float cost = 0.0f;
            foreach (Item item in items)
            {
                cost += item.Price();
            }
            return cost;
        }
        public void ShowItems()
        {
            foreach (Item item in items)
            {
                Console.Write("物品:" + item.Name());
                Console.Write(",包装:"+item.Packing().Pack());
                Console.Write(",价格:"+item.Price()+"\r\n");
            }
        }
    }
    //步骤6创建一个MealBuilder类，实际的builder类负责创建Meal实例
    public class MealBuilder
    {
        public Meal PrepareVegMeal()
        {
            Meal meal = new Meal();
            meal.AddItem(new VegBurger());
            meal.AddItem(new Coca());
            return meal;
        }
        public Meal PrepareNonVegMeal()
        {
            Meal meal = new Meal();
            meal.AddItem(new ChickenBurger());
            meal.AddItem(new Pepsi());
            return meal;
        }
    }
}
