using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates C# switch expressions and pattern matching with examples.
    /// Covers simple value switch, type pattern matching, and tuple switch with guards.
    /// </summary>
    public class SwitchExpressionExample : ITopicModule
    {
        public string Name => "Switch Expression";
        public string Description => "Show switch expressions and pattern matching examples.";

        public void Run()
        {
            Console.WriteLine("Switch Expression examples:\n");

            // Simple int switch expression
            int number = 3;
            string description = number switch
            {
                1 => "One",
                2 => "Two",
                3 => "Three",
                _ => "Unknown"
            };
            Console.WriteLine($"number = {number} => {description}"); // Output: Three

            // Switch expression with type patterns
            object obj = 42;
            string typeDescription = obj switch
            {
                int i => $"Integer: {i}",
                string s => $"String: {s}",
                null => "Null value",
                _ => "Other type"
            };
            Console.WriteLine(typeDescription); // Output: Integer: 42

            // Tuple switch with guards
            var point = (x: 0, y: 5);
            string pointLocation = point switch
            {
                (0, 0) => "Origin",
                (0, _) => "On Y axis",
                (_, 0) => "On X axis",
                var (x, y) when x == y => "On diagonal",
                _ => "Somewhere else"
            };
            Console.WriteLine($"point {point} => {pointLocation}");

            // More advanced: when using properties or nested patterns
            Person p = new Person("Alice", 30);
            string personDesc = p switch
            {
                { Age: < 18 } => "Minor",
                { Age: >= 18 and < 65 } => "Adult",
                { Age: >= 65 } => "Senior",
                _ => "Unknown"
            };
            Console.WriteLine($"Person {p.Name}, Age {p.Age} => {personDesc}");

            Console.WriteLine();
            Console.WriteLine("Notes: switch expressions are concise, return a value, and support patterns and guards (when).\n");

            Console.WriteLine("Press Enter to return to main menu...");
            Console.ReadLine();
        }

        private record Person(string Name, int Age);
    }
}
