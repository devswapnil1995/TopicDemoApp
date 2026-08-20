using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates static classes/members vs instance classes/members using a simple
    /// employee-counter style example. Shows:
    /// - how to call static methods and access static data without creating an instance
    /// - how instance methods and fields are per-object
    /// - that static state is shared across all instances
    /// - static constructor behavior
    /// </summary>
    public class StaticVsInstanceExample : ITopicModule
    {
        public string Name => "Static vs Instance";
        public string Description => "Compare static classes/methods and instance classes/members.";

        public void Run()
        {
            Console.WriteLine("Static vs Instance example:\n");

            // Call a static method without creating any object
            Console.WriteLine("Calling StaticUtils.GetGlobalCount() before any instances:");
            Console.WriteLine($"GlobalCount: {StaticUtils.GetGlobalCount()}\n");

            Console.WriteLine("Create two InstanceCounter objects. Each will update the shared static counter in their constructor.");
            var a = new InstanceCounter("Alice");
            var b = new InstanceCounter("Bob");

            Console.WriteLine();
            Console.WriteLine("Each instance has its own local counter. Increment them differently:");
            a.Increment(); // a.LocalCounter = 1
            a.Increment(); // a.LocalCounter = 2
            b.Increment(); // b.LocalCounter = 1

            Console.WriteLine($"{a.Name} local: {a.GetLocalCount()}");
            Console.WriteLine($"{b.Name} local: {b.GetLocalCount()}");

            Console.WriteLine();
            Console.WriteLine("Static (global) counter is shared across all instances and was incremented in each constructor:");
            Console.WriteLine($"StaticUtils.GlobalCount: {StaticUtils.GetGlobalCount()}");

            Console.WriteLine();
            Console.WriteLine("You can call a static method directly:");
            StaticUtils.PrintInfo();

            Console.WriteLine();
            Console.WriteLine("Instance methods can call static members if needed (demonstrated inside InstanceCounter.ShowInfo).");
            a.ShowInfo();
            b.ShowInfo();

            Console.WriteLine("\nKey takeaways:");
            Console.WriteLine("- Static members belong to the type itself; they are shared across all instances.");
            Console.WriteLine("- Instance members belong to a specific object; each object has its own copy.");
            Console.WriteLine("- Static classes cannot be instantiated and can only contain static members.");

            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }
    }

    /// <summary>
    /// A static utility class. It cannot be instantiated and may only contain static members.
    /// Useful for stateless helpers or for shared global state (use with care).
    /// </summary>
    public static class StaticUtils
    {
        // Static field shared across the AppDomain
        private static int _globalCount;

        // Static constructor runs once before the first access to any static member
        static StaticUtils()
        {
            _globalCount = 0;
            // Note: static constructor will run before first access and can be used for one-time init
            Console.WriteLine("[StaticUtils] static constructor executed (one-time initialization)");
        }

        public static int GetGlobalCount() => _globalCount;

        public static void IncrementGlobal()
        {
            _globalCount++;
        }

        public static void PrintInfo()
        {
            Console.WriteLine($"[StaticUtils] GlobalCount = {_globalCount}");
        }
    }

    /// <summary>
    /// Instance class that maintains per-object state but interacts with StaticUtils
    /// to demonstrate shared static state.
    /// </summary>
    public class InstanceCounter
    {
        public string Name { get; }
        private int _localCount;

        public InstanceCounter(string name)
        {
            Name = name;
            _localCount = 0;
            // Show that constructors can modify static state if desired
            StaticUtils.IncrementGlobal();
        }

        public void Increment()
        {
            _localCount++;
        }

        public int GetLocalCount() => _localCount;

        public void ShowInfo()
        {
            Console.WriteLine($"Instance {Name} -> local: {_localCount}, global: {StaticUtils.GetGlobalCount()}");
        }
    }
}
