using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 意图：用原型实例指定创建对象的种类，并且通过拷贝这些原型创建新的对象
 * 主要解决：在运行期建立和删除原型
 */
namespace 原型模式
{
    class Program
    {
        static void Main(string[] args)
        {
            ShapeCache.LoadCache();
            Shape clonedShape = (Shape)ShapeCache.GetShape("1");
            Console.WriteLine("Shape:" + clonedShape.Type);


            Shape clonedShape2 = (Shape)ShapeCache.GetShape("2");
            Console.WriteLine("Shape:" + clonedShape2.Type);
            Console.ReadKey();
        }
    }
    //步骤1 创建一个实现了ICloneable接口的抽象类
    public abstract class Shape : ICloneable
    {
        private string id;
        private string type;
        public string Id { get => id; set => id = value; }
        public string Type { get => type; set => type = value; }
        public abstract void Draw();
        public object Clone()
        {
            Object clone = null;
            try
            {
                clone = this.MemberwiseClone();
            }
            catch (Exception ex)
            {

            }
            return clone;
        }
    }
    //步骤2 创建扩展了上面抽象类的实体类
    public class Reactangle : Shape
    {
        public Reactangle()
        {
            Type = "Reactangle";
        }
        public override void Draw()
        {
            Console.WriteLine("Reactangle:draw() method");
        }
    }
    public class Circle : Shape
    {
        public Circle()
        {
            Type = "Circle";
        }
        public override void Draw()
        {
            Console.WriteLine("Circle:draw() method");
        }
    }
    //步骤3 创建一个类，从数据库获取实体类，并把它们存储在一个Hashtable中
    public class ShapeCache
    {
        private static Hashtable shapeMap = new Hashtable();
        public static Shape GetShape(string shapeId)
        {
            Shape cachedShape = shapeMap[shapeId] as Shape;
            return (Shape)cachedShape.Clone();
        }
        /*
         * 对每种形状都运行数据库查询，并创建该形状
         * shapeMap.put(shapeKey,shape);
         * 例如，我们要添加三种形状
         */
        public static void LoadCache()
        {
            Circle circle = new Circle();
            circle.Id = "1";
            shapeMap.Add(circle.Id, circle);

            Reactangle reactangle = new Reactangle();
            reactangle.Id = "2";
            shapeMap.Add(reactangle.Id, reactangle);
        }
    }
}
