using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates delegates and their types in C#:
    /// - Custom delegate declaration
    /// - Single-cast delegates (one target method)
    /// - Multicast delegates (multiple target methods with += and -=)
    /// - Built-in delegate types: Action and Func
    /// - Anonymous methods
    /// - Lambda expressions (modern syntax)
    /// </summary>
    public class DelegateExample : ITopicModule
    {
        public string Name => "Delegate & Types";
        public string Description => "Shows custom delegates, single/multicast delegates, Action, Func, anonymous methods, and lambdas.";

        public void Run()
        {
            Console.WriteLine("Delegates and Types demo:\n");

            // 1) Custom delegate type
            Console.WriteLine("1) Custom delegate type: ProcessSalary (takes Employee, returns void)");
            var emp = new Employee { Id = 1, Name = "Alice", Salary = 50000 };
            ProcessSalary handler = DisplaySalary;
            handler(emp);
            Console.WriteLine();

            // 2) Single-cast delegate: reassignment replaces the target
            Console.WriteLine("2) Single-cast delegate: reassignment");
            handler = ApplySalaryIncrease;
            Console.WriteLine("   After reassigning handler to ApplySalaryIncrease:");
            handler(emp);
            Console.WriteLine();

            // 3) Multicast delegate: += and -= to add/remove targets
            Console.WriteLine("3) Multicast delegate: += to add multiple handlers");
            ProcessSalary multiHandler = DisplaySalary;
            multiHandler += ApplySalaryIncrease;
            multiHandler += LogSalaryChange;
            Console.WriteLine("   Invoking multiHandler (three methods in sequence):");
            multiHandler(emp);
            Console.WriteLine();

            // 4) Multicast: -= to remove a handler
            Console.WriteLine("4) Multicast delegate: -= to remove a handler");
            multiHandler -= ApplySalaryIncrease;
            Console.WriteLine("   After -= ApplySalaryIncrease:");
            multiHandler(emp);
            Console.WriteLine();

            // 5) Built-in Action<T> (return type void)
            Console.WriteLine("5) Built-in Action<T> -> delegate with no return value");
            Action<Employee> actionHandler = e => Console.WriteLine($"   [Action] Processing {e.Name}");
            actionHandler(emp);
            Console.WriteLine();

            // 6) Built-in Func<T, TResult> (returns a value)
            Console.WriteLine("6) Built-in Func<T, TResult> -> delegate that returns a value");
            Func<Employee, decimal> calculateBonus = e => e.Salary * 0.1m;
            decimal bonus = calculateBonus(emp);
            Console.WriteLine($"   [Func] Bonus for {emp.Name}: {bonus:C}");
            Console.WriteLine();

            // 7) Anonymous method syntax (older)
            Console.WriteLine("7) Anonymous method: delegate keyword (older C# syntax)");
            ProcessSalary anonHandler = delegate (Employee e)
            {
                Console.WriteLine($"   [Anonymous] Employee bonus = {e.Salary * 0.05m:C}");
            };
            anonHandler(emp);
            Console.WriteLine();

            // 8) Lambda expression (modern, cleaner)
            Console.WriteLine("8) Lambda expression: arrow syntax (modern)");
            ProcessSalary lambdaHandler = e => Console.WriteLine($"   [Lambda] {e.Name}'s salary is {e.Salary:C}");
            lambdaHandler(emp);
            Console.WriteLine();

            Console.WriteLine("Summary:");
            Console.WriteLine("- Delegates are type-safe function pointers or callback mechanisms.");
            Console.WriteLine("- Single-cast: one target method.");
            Console.WriteLine("- Multicast: multiple target methods (+=, -=).");
            Console.WriteLine("- Action<T>: built-in delegate returning void.");
            Console.WriteLine("- Func<T, TResult>: built-in delegate returning a value.");
            Console.WriteLine("- Lambda expressions are the modern, concise way to create delegate instances.");

            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }

        // Custom delegate type definition
        public delegate void ProcessSalary(Employee emp);

        // Sample employee class
        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Salary { get; set; }
        }

        // Delegate target methods
        private void DisplaySalary(Employee emp)
        {
            Console.WriteLine($"   Displaying: {emp.Name} earns {emp.Salary:C}");
        }

        private void ApplySalaryIncrease(Employee emp)
        {
            emp.Salary *= 1.1m;
            Console.WriteLine($"   10% raise applied: {emp.Name} now earns {emp.Salary:C}");
        }

        private void LogSalaryChange(Employee emp)
        {
            Console.WriteLine($"   [Log] Salary change recorded for {emp.Name}");
        }
    }
}
