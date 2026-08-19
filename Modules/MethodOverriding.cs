using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates method overriding in C#. Method overriding allows a derived
    /// class to provide a specific implementation of a method that is already
    /// defined in its base class. This demo also shows runtime polymorphism
    /// when a base-class reference points to a derived-class instance.
    /// </summary>
    public class MethodOverriding : ITopicModule
    {
        public string Name => "Method Overriding";
        public string Description => "Learn about method overriding in C#";

        /// <summary>
        /// Runs the demo by calling Display on different objects. The example
        /// illustrates how the override in DerivedClass replaces the base
        /// implementation when invoked on a DerivedClass instance. It also
        /// demonstrates polymorphism when a BaseClass reference holds a
        /// DerivedClass instance.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Running Method Overriding Demo...");

            // Direct BaseClass instance uses the base implementation
            BaseClass baseObj = new BaseClass();
            Console.WriteLine("Calling Display on BaseClass instance:");
            baseObj.Display();
            Console.ReadLine();

            // Direct DerivedClass instance uses the overridden implementation
            DerivedClass derivedObj = new DerivedClass();
            Console.WriteLine("Calling Display on DerivedClass instance:");
            derivedObj.Display();
            Console.ReadLine();

            // Polymorphism: base reference pointing to derived instance
            BaseClass poly = new DerivedClass();
            Console.WriteLine("Calling Display on BaseClass reference that refers to a DerivedClass instance (polymorphism):");
            poly.Display();
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Base class that declares a virtual method which can be overridden
    /// by derived classes.
    /// </summary>
    public class BaseClass
    {
        /// <summary>
        /// A virtual method that provides the default behavior. Derived classes
        /// may override this method to provide a specialized implementation.
        /// </summary>
        public virtual void Display()
        {
            Console.WriteLine("Display method in BaseClass");
        }
    }

    /// <summary>
    /// Derived class that overrides the Display method to provide a custom
    /// implementation.
    /// </summary>
    public class DerivedClass : BaseClass 
    {
        /// <summary>
        /// Overrides the base implementation to demonstrate how the derived
        /// method is invoked instead of the base method when appropriate.
        /// </summary>
        public override void Display()
        {
            Console.WriteLine("Display method in DerivedClass");
        }
    }
}
