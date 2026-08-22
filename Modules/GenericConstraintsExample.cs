using System;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates generic constraints in C# with concrete examples for each
    /// common constraint form: struct, class, new(), base class, interface,
    /// multiple constraints, and constraints on multiple type parameters.
    /// </summary>
    public class GenericConstraintsExample : ITopicModule
    {
        public string Name => "Generic Constraints";
        public string Description => "Shows examples of where T : struct, class, new(), base class, interface and combinations.";

        public void Run()
        {
            Console.WriteLine("Generic Constraints examples:\n");

            // 1) where T : struct
            Console.WriteLine("1) where T : struct -> restricts T to non-nullable value types (int, double, user-defined structs)");
            var intContainer = new ValueContainer<int> { Data = 123 };
            Console.WriteLine($"   ValueContainer<int>.Data = {intContainer.Data}");

            // 2) where T : class
            Console.WriteLine("\n2) where T : class -> restricts T to reference types (classes, interfaces, delegates, arrays)");
            var refContainer = new ReferenceContainer<string> { Data = "hello" };
            Console.WriteLine($"   ReferenceContainer<string>.Data = {refContainer.Data}");

            // 3) where T : new()
            Console.WriteLine("\n3) where T : new() -> restricts T to types with a public parameterless constructor so the generic can instantiate T");
            var factory = new Factory<StringBuilder>();
            var sb = factory.CreateInstance();
            sb.Append("Created via Factory<T> where T : new()");
            Console.WriteLine($"   {sb}");

            // 4) where T : BaseClassName
            Console.WriteLine("\n4) where T : BaseClassName -> restricts T to types derived from a specific base class so you can use base class members");
            var dogContainer = new AnimalContainer<Dog> { Data = new Dog("Rex") };
            Console.WriteLine($"   AnimalContainer<Dog>.Data.Speak() -> {dogContainer.Data.Speak()}");

            // 5) where T : InterfaceName
            Console.WriteLine("\n5) where T : InterfaceName -> restricts T to types that implement a specific interface so you can call interface methods on T");
            var shapeContainer = new ShapeContainer<Circle>();
            Console.Write("   ");
            shapeContainer.Render(new Circle(5));

            // 6) Multiple constraints (class, new())
            Console.WriteLine("\n6) Multiple constraints -> combine constraints (e.g., class, new()) to narrow allowed types and enable instantiation");
            var created = new Sample<StringBuilder>().Create();
            Console.WriteLine($"   Sample<StringBuilder>.Create produced: {created.GetType().Name}");

            // 7) Constraints on multiple type parameters
            Console.WriteLine("\n7) Constraints on multiple type parameters -> each generic parameter can have its own constraints (e.g., T1 : class, T2 : struct)");
            var two = new SampleTwo<StringBuilder, int>
            {
                RefData = new StringBuilder("ref"),
                ValData = 77
            };
            Console.WriteLine($"   SampleTwo.RefData type: {two.RefData.GetType().Name}, ValData: {two.ValData}");

            Console.WriteLine("\nNotes: generic constraints limit what types callers may use and enable safe usage of members (e.g., calling interface methods or creating instances).");
            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }
    }

    // 1. where T : struct
    class ValueContainer<T> where T : struct
    {
        public T Data { get; set; }
    }

    // 2. where T : class
    class ReferenceContainer<T> where T : class
    {
        public T Data { get; set; }
    }

    // 3. where T : new()
    class Factory<T> where T : new()
    {
        public T CreateInstance()
        {
            return new T();
        }
    }

    // 4. where T : BaseClassName
    class Animal { public virtual string Speak() => "..."; }
    class Dog : Animal { public Dog(string name) { Name = name; } public string Name { get; } public override string Speak() => $"Woof ({Name})"; }
    class AnimalContainer<T> where T : Animal
    {
        public T Data { get; set; }
    }

    // 5. where T : InterfaceName
    interface IShape { void Draw(); }
    class Circle : IShape { public double Radius { get; } public Circle(double r) { Radius = r; } public void Draw() { Console.WriteLine($"Drawing circle with radius {Radius}"); } }
    class ShapeContainer<T> where T : IShape
    {
        public void Render(T shape)
        {
            Console.Write("Rendering shape: ");
            shape.Draw();
        }
    }

    // 6. Multiple constraints: class and new()
    class Sample<T> where T : class, new()
    {
        public T Create() => new T();
    }

    // 7. Constraints on multiple type parameters
    class SampleTwo<T1, T2>
        where T1 : class
        where T2 : struct
    {
        public T1 RefData { get; set; }
        public T2 ValData { get; set; }
    }
}
