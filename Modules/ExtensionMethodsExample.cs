using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates extension methods in C# with three types:
    /// 1. Extension methods on classes (reference types)
    /// 2. Extension methods on structs (value types)
    /// 3. Extension methods on interfaces (contracts)
    ///
    /// Extension methods add new functionality to existing types without modifying
    /// the original class. They are static methods defined in static classes that
    /// appear as instance methods on the extended type.
    /// </summary>
    public class ExtensionMethodsExample : ITopicModule
    {
        public string Name => "Extension Methods";
        public string Description => "Demonstrates extension methods on classes, structs, and interfaces.";

        public void Run()
        {
            Console.WriteLine("Extension Methods Demo:\n");

            // Type 1: Extension methods on CLASS (reference type)
            Console.WriteLine("=== Type 1: Extension Method on CLASS (Employee) ===");
            var emp = new EmployeeExt { Id = 1, Name = "Alice", Salary = 50000 };
            Console.WriteLine($"Employee: {emp.Name}");
            Console.WriteLine($"Formatted as CSV: {emp.ToCsvLine()}");
            Console.WriteLine($"With bonus (10%): {emp.GetSalaryWithBonus(0.10m):C}");
            Console.WriteLine($"Is Senior (salary > 60000)? {emp.IsSenior()}");
            Console.WriteLine();

            // Type 2: Extension methods on STRUCT (value type)
            Console.WriteLine("=== Type 2: Extension Method on STRUCT (Salary) ===");
            var salary = new Salary { Amount = 75000m, Currency = "USD" };
            Console.WriteLine($"Salary: {salary.Amount} {salary.Currency}");
            Console.WriteLine($"Formatted: {salary.Format()}");
            Console.WriteLine($"Monthly: {salary.GetMonthly():C}");
            Console.WriteLine($"Is above average (50000)? {salary.IsAboveThreshold(50000)}");
            Console.WriteLine();

            // Type 3: Extension methods on INTERFACE (contract)
            Console.WriteLine("=== Type 3: Extension Method on INTERFACE (IPerson) ===");
            var manager = new ManagerExt { Id = 2, Name = "Bob", Department = "Engineering" };
            var developer = new DeveloperExt { Id = 3, Name = "Charlie", Skills = "C#, ASP.NET" };

            IPerson managerInterface = manager;
            IPerson developerInterface = developer;

            // Extension method called on interface reference
            Console.WriteLine($"Manager greeting: {managerInterface.GetGreeting()}");
            Console.WriteLine($"Manager display: {managerInterface.DisplayInfo()}");
            Console.WriteLine();

            Console.WriteLine($"Developer greeting: {developerInterface.GetGreeting()}");
            Console.WriteLine($"Developer display: {developerInterface.DisplayInfo()}");
            Console.WriteLine();

            // Chaining extension methods
            Console.WriteLine("=== Chaining Extension Methods ===");
            var emp2 = new EmployeeExt { Id = 4, Name = "Diana", Salary = 65000 };
            Console.WriteLine($"Formatted: {emp2.ToCsvLine().ToUpper()}");
            Console.WriteLine();

            Console.WriteLine("Key Points:");
            Console.WriteLine("- Extension methods are static methods in static classes");
            Console.WriteLine("- First parameter uses 'this' keyword to specify extended type");
            Console.WriteLine("- They work on classes, structs, and interfaces");
            Console.WriteLine("- They appear as instance methods when called on objects");
            Console.WriteLine("- They cannot override existing methods (non-virtual)");
            Console.WriteLine("- Useful for adding functionality without modifying source");

            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }
    }

    // ==================== Target Types to Extend ====================

    /// <summary>
    /// Reference type (class) - will be extended with methods
    /// </summary>
    public class EmployeeExt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
    }

    /// <summary>
    /// Value type (struct) - will be extended with methods
    /// </summary>
    public struct Salary
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }

    /// <summary>
    /// Interface (contract) - will be extended with methods
    /// </summary>
    public interface IPerson
    {
        int Id { get; }
        string Name { get; }
    }

    /// <summary>
    /// Implements IPerson - demonstrates interface extension
    /// </summary>
    public class ManagerExt : IPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
    }

    /// <summary>
    /// Implements IPerson - demonstrates interface extension
    /// </summary>
    public class DeveloperExt : IPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Skills { get; set; }
    }

    // ==================== EXTENSION METHODS ====================

    /// <summary>
    /// Extension methods for CLASS (Employee)
    /// These add functionality to the Employee class without modifying it
    /// </summary>
    public static class EmployeeExtensions
    {
        /// <summary>
        /// Extension method #1: Convert employee to CSV line
        /// </summary>
        public static string ToCsvLine(this EmployeeExt emp)
        {
            return $"{emp.Id},{emp.Name},{emp.Salary:C}";
        }

        /// <summary>
        /// Extension method #2: Calculate salary with bonus
        /// </summary>
        public static decimal GetSalaryWithBonus(this EmployeeExt emp, decimal bonusPercentage)
        {
            return emp.Salary + (emp.Salary * bonusPercentage);
        }

        /// <summary>
        /// Extension method #3: Check if employee is senior
        /// </summary>
        public static bool IsSenior(this EmployeeExt emp)
        {
            return emp.Salary > 60000;
        }

        /// <summary>
        /// Extension method #4: Get raise amount
        /// </summary>
        public static decimal CalculateRaise(this EmployeeExt emp, decimal raisePercentage)
        {
            return emp.Salary * raisePercentage;
        }
    }

    /// <summary>
    /// Extension methods for STRUCT (Salary)
    /// These add functionality to the Salary value type without modifying it
    /// </summary>
    public static class SalaryExtensions
    {
        /// <summary>
        /// Extension method #1: Format salary nicely
        /// </summary>
        public static string Format(this Salary salary)
        {
            return $"{salary.Amount:N2} {salary.Currency}";
        }

        /// <summary>
        /// Extension method #2: Calculate monthly salary
        /// </summary>
        public static decimal GetMonthly(this Salary salary)
        {
            return salary.Amount / 12m;
        }

        /// <summary>
        /// Extension method #3: Check if above threshold
        /// </summary>
        public static bool IsAboveThreshold(this Salary salary, decimal threshold)
        {
            return salary.Amount > threshold;
        }

        /// <summary>
        /// Extension method #4: Calculate net after tax
        /// </summary>
        public static decimal GetNetAfterTax(this Salary salary, decimal taxRate)
        {
            return salary.Amount - (salary.Amount * taxRate);
        }
    }

    /// <summary>
    /// Extension methods for INTERFACE (IPerson)
    /// These add functionality to any type implementing IPerson without modifying them
    /// </summary>
    public static class PersonExtensions
    {
        /// <summary>
        /// Extension method #1: Get greeting message
        /// Works on any type implementing IPerson
        /// </summary>
        public static string GetGreeting(this IPerson person)
        {
            return $"Hello, I'm {person.Name} (ID: {person.Id})";
        }

        /// <summary>
        /// Extension method #2: Display person info
        /// Works on any type implementing IPerson
        /// </summary>
        public static string DisplayInfo(this IPerson person)
        {
            return $"Person: {person.Name} | Type: {person.GetType().Name}";
        }

        /// <summary>
        /// Extension method #3: Get unique identifier
        /// </summary>
        public static string GetUniqueId(this IPerson person)
        {
            return $"{person.GetType().Name}_{person.Id}";
        }

        /// <summary>
        /// Extension method #4: Compare by name
        /// </summary>
        public static int CompareByName(this IPerson person1, IPerson person2)
        {
            return person1.Name.CompareTo(person2.Name);
        }
    }

    // ==================== ADVANCED EXAMPLES ====================

    /// <summary>
    /// Extension methods using generics
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Generic extension to repeat any value N times
        /// </summary>
        public static string Repeat<T>(this T value, int count)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append(value.ToString());
                if (i < count - 1) sb.Append(", ");
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// Extension methods on built-in types
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Extension on string - make title case
        /// </summary>
        public static string ToTitleCase(this string str)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
    }

    /// <summary>
    /// Extension methods on collections
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Extension on IEnumerable<T> - get average salary
        /// </summary>
        public static decimal AverageSalary(this IEnumerable<EmployeeExt> employees)
        {
            var sum = 0m;
            var count = 0;
            foreach (var emp in employees)
            {
                sum += emp.Salary;
                count++;
            }
            return count > 0 ? sum / count : 0;
        }
    }
}
