using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Shape shape = new Shape();
            shape.ShapeChanged += Shape_ShapeChanged;
            shape.ChangeShape();

            Console.ReadLine();
        }

        private static void Shape_ShapeChanged(object sender, EventArgs e)
        {
            Console.WriteLine(e.ToString());
        }
    }
    public interface IDrawingObject
    {
        event EventHandler ShapeChanged;
    }
    public class MyEventArgs : EventArgs
    {
        // class members
        public object arg { get; set; }
    }
    public class Shape : IDrawingObject
    {
        public event EventHandler ShapeChanged;
        public void ChangeShape()
        {
            // Do something here before the event…
            MyEventArgs args = new MyEventArgs();
            args.arg = "111";

            OnShapeChanged(args);

            // or do something here after the event.
        }
        protected virtual void OnShapeChanged(MyEventArgs e)
        {
            ShapeChanged?.Invoke(this, e);
        }
    }
}
