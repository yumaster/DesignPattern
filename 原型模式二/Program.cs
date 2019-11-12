using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
/*
* 原型模式：通过将一个原型对象传给那个要发动创建的对象，这个要发动创建的对象通过请求原型对象拷贝它们自己来实施创建。（包括深度克隆和浅克隆）
* 主要面对的问题是：“某些结构复杂的对象”的创建工作；由于需求的变化，这些对象经常面临着剧烈的变化，但是它们却拥有比较稳定的一致的接口。
* 
* 1.浅克隆没有克隆对象中的引用类型
* 2.string是特殊的引用类型，字符串的值一个是定存的，重新改值会实例化一个新的对象，所以在遍历中不建议使用string，因为损耗性能
* 
* 1.复杂对象指的是当创建该对象消耗资源过多
* 2.面临的剧烈变化，比如发邮件，我们需要发N条，但是这N条邮件的对象每个人发送的信息也不同，所以导致实例出的对象也不完全一样
* 3.稳定的接口指的是都是通过同一个方法将对象发送出去，即调用方法一般不存在变化，而是对象改变
* 
* 在什么情况下选择原型模式
* 1.是类初始化需要消耗非常多的资源，这个资源包括数据、硬件资源等
* 2.是通过new产生一个对象需要非常繁琐的数据准备或访问权限，则可以使用原型模式
* 原型模式的浅度克隆和深度克隆是什么意思？
* 1.浅度复制：将原来对象中的所有字段逐个复制到一个新对象，如果字段是值类型，则简单滴复制一个副本到新对象
* 改变新对象的值类型字段不会影响原对象；如果字段是引用类型，则复制的是引用，改变目标对象中引用类型字段的值将会影响原对象
* 2.深度复制：与浅度复制不同之处在于对引用类型的处理，深度复制将新对象中引用类型字段指向复制过的新对象，改变新对象中引用的任何对象，不会影响到原来的对象中对应字段的内容
*/
namespace 原型模式二
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("--------------------------------");
            //PeoplePrototype peoplePrototype1 = PeoplePrototype.CreateInstance();
            //Console.WriteLine("{0},{1}", peoplePrototype1.Name, peoplePrototype1.Id);
            //PeoplePrototype peoplePrototype2 = PeoplePrototype.CreateInstance();
            //Console.WriteLine("{0},{1}", peoplePrototype2.Name, peoplePrototype2.Id);
            //peoplePrototype2.Name = "hahahahha";//修改peoplePrototype2的值，不影响peoplePrototype3的值，证明他们不是同一个引用对象
            //Console.WriteLine("{0},{1}", peoplePrototype2.Name, peoplePrototype2.Id);
            //PeoplePrototype peoplePrototype3 = PeoplePrototype.CreateInstance();
            //Console.WriteLine("{0},{1}", peoplePrototype3.Name, peoplePrototype3.Id);
            //Console.WriteLine("--------------------------------");

            //Console.WriteLine("----------浅克隆----------------");
            //PeoplePrototype peoplePrototype4 = PeoplePrototype.CreateInstance();
            //Console.WriteLine("{0},{1}", peoplePrototype4.Dept.Name, peoplePrototype4.Dept.Id);
            //PeoplePrototype peoplePrototype5 = PeoplePrototype.CreateInstance();
            //Console.WriteLine("{0},{1}", peoplePrototype5.Dept.Name, peoplePrototype5.Dept.Id);
            //peoplePrototype4.Dept.Id = 22;
            //peoplePrototype4.Dept.Name = "测试部";//修改peoplePrototype4的Dept.Name，影响peoplePrototype5的值，证明Dept同一个引用对象
            //Console.WriteLine("{0},{1}", peoplePrototype4.Dept.Name, peoplePrototype4.Dept.Id);
            //Console.WriteLine("{0},{1}", peoplePrototype5.Dept.Name, peoplePrototype5.Dept.Id);
            //Console.WriteLine("--------------------------------");
            //Console.WriteLine("----------深克隆----------------");
            //PeoplePrototype peoplePrototype6 = PeoplePrototype.CreateInstanceDeep();
            //Console.WriteLine("{0},{1}", peoplePrototype6.Dept.Name, peoplePrototype6.Dept.Id);
            //PeoplePrototype peoplePrototype7 = PeoplePrototype.CreateInstanceDeep();
            //peoplePrototype6.Dept.Id = 22;
            //peoplePrototype6.Dept.Name = "测试部";
            //Console.WriteLine("{0},{1}", peoplePrototype6.Dept.Name, peoplePrototype6.Dept.Id);
            //Console.WriteLine("{0},{1}", peoplePrototype7.Dept.Name, peoplePrototype7.Dept.Id);
            Console.WriteLine("----------深克隆----------------");
            PeoplePrototype peoplePrototype8 = PeoplePrototype.CreateInstanceSerialize();
            Console.WriteLine("{0},{1}", peoplePrototype8.Dept.Name, peoplePrototype8.Dept.Id);
            PeoplePrototype peoplePrototype9 = PeoplePrototype.CreateInstanceSerialize();
            peoplePrototype8.Dept.Id = 22;
            peoplePrototype9.Dept.Name = "测试部";
            Console.WriteLine("{0},{1}", peoplePrototype8.Dept.Name, peoplePrototype8.Dept.Id);
            Console.WriteLine("{0},{1}", peoplePrototype9.Dept.Name, peoplePrototype9.Dept.Id);



            Console.ReadKey();
        }
    }
    [Serializable]
    public class Dept
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    [Serializable]
    public class PeoplePrototype
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public Dept Dept { get; set; }
        private static PeoplePrototype _peoplePrototype = null;
        private PeoplePrototype()
        {
            Console.WriteLine("{0}被创建了,线程ID{1}", this.GetType(), Thread.CurrentThread.ManagedThreadId);
        }
        static PeoplePrototype()
        {
            _peoplePrototype = new PeoplePrototype()//静态构造函数，赋初始值
            {
                Id = 1,
                Name = "ZhangYu",
                Dept = new Dept
                {
                    Id = 11,
                    Name = "技术部"
                }
            };
        }
        //静态方法：克隆一个对象
        public static PeoplePrototype CreateInstance()
        {
            PeoplePrototype peoplePrototype = (PeoplePrototype)_peoplePrototype.MemberwiseClone();
            return peoplePrototype;
        }
        //静态方法：深度克隆一个对象
        public static PeoplePrototype CreateInstanceDeep()
        {
            PeoplePrototype peoplePrototype = (PeoplePrototype)_peoplePrototype.MemberwiseClone();
            //深度克隆，重新实例化一个对象和开辟一个内存，所以克隆的结果指向的地址每次都不一样
            peoplePrototype.Dept = new Dept()
            {
                Id = 11,
                Name = "技术部"
            };
            return peoplePrototype;
        }
        //静态方法：通过序列化生成实体
        public static PeoplePrototype CreateInstanceSerialize()
        {
            return SerializaUtil.DeepClone<PeoplePrototype>(_peoplePrototype);
        }
    }
    /// <summary>
    /// 帮助类，创建对象
    /// </summary>
    public class SerializaUtil
    {
        public static string Serialize(object obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, obj);
                return Convert.ToBase64String(stream.ToArray());
            }
        }
        public static T Deserialize<T>(string target)
        {
            byte[] targetArray = Convert.FromBase64String(target);
            using (MemoryStream stream = new MemoryStream(targetArray))
            {
                return (T)new BinaryFormatter().Deserialize(stream);
            }
        }
        public static T DeepClone<T>(T t)
        {
            return Deserialize<T>(Serialize(t));
        }
    }
}
