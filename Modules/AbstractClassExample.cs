using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates abstract classes in C# with an employee/company example.
    ///
    /// Covers:
    /// - abstract methods and properties
    /// - protected members and constructors
    /// - virtual members and overriding
    /// - sealed overrides (prevents further overriding)
    /// - non-abstract members inside an abstract class
    /// - polymorphism using abstract base references
    /// </summary>
    public class AbstractClassExample : ITopicModule
    {
        public string Name => "Abstract Class Example";
        public string Description => "Demonstrates the use of abstract classes in C#.";

        public void Run()
        {
            Console.WriteLine("Abstract class examples:\n");

            // We cannot instantiate AbstractBaseEmployee directly. The following
            // line would not compile if uncommented:
            // var invalid = new AbstractBaseEmployee(1000);

            // Create concrete implementations and treat them as the abstract base
            var staff = new List<AbstractBaseEmployee>
            {
                new SalariedEmployee(1, "Alice", 50000, 5000),
                new HourlyEmployee(2, "Bob", 30000, 200)
            };

            foreach (var s in staff)
            {
                // Polymorphism: calls the implementation provided by the concrete type
                s.ShowDetails();
                s.CalculateSalary();
                Console.WriteLine();
            }

            // Demonstrate calling a static helper on the abstract type
            AbstractBaseEmployee.ExplainConcept();

            // Pause so the user can read the output before returning to the menu
            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }

    }

    /// <summary>
    /// Abstract base class for employees. It defines a contract (CalculateSalary)
    /// and provides reusable implementation (ShowDetails, protected BaseSalary).
    /// Abstract classes can contain fields, constructors, properties, methods
    /// (both abstract and non-abstract), and static members.
    /// </summary>
    public abstract class AbstractBaseEmployee
    {
        // Protected member is visible to derived classes but hidden from callers.
        protected double BaseSalary { get; }

        public int Id { get; }
        public string Name { get; }

        /// <summary>
        /// Abstract property that derived classes must implement to provide bonus.
        /// </summary>
        public abstract double Bonus { get; }

        /// <summary>
        /// Abstract method that derived classes must implement.
        /// </summary>
        public abstract void CalculateSalary();

        /// <summary>
        /// Non-abstract helper method available to all derived types.
        /// </summary>
        public void ShowDetails()
        {
            Console.WriteLine($"Employee Id: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Base Salary: {BaseSalary:C}");
            Console.WriteLine($"Bonus (declared abstract): {Bonus:C}");
        }

        /// <summary>
        /// Virtual helper that derived classes may override to customize gross salary calculation.
        /// </summary>
        public virtual double GetGrossSalary()
        {
            return BaseSalary + Bonus;
        }

        /// <summary>
        /// Protected constructor: abstract classes can define constructors used by derived types.
        /// </summary>
        protected AbstractBaseEmployee(int id, string name, double baseSalary)
        {
            Id = id;
            Name = name;
            BaseSalary = baseSalary;
        }

        /// <summary>
        /// Static helper to explain the concept at runtime.
        /// </summary>
        public static void ExplainConcept()
        {
            Console.WriteLine("Abstract classes define a common contract and provide shared implementation.\n");
        }
    }

    /// <summary>
    /// Salaried employee implements the abstract members. It also seals the
    /// CalculateSalary override to prevent further overriding in subclasses.
    /// </summary>
    public class SalariedEmployee : AbstractBaseEmployee
    {
        public override double Bonus { get; }

        public SalariedEmployee(int id, string name, double baseSalary, double bonus)
            : base(id, name, baseSalary)
        {
            Bonus = bonus;
        }

        // Sealed override: prevents derived classes from overriding this method.
        public sealed override void CalculateSalary()
        {
            var gross = GetGrossSalary();
            var net = gross - (gross * 0.1); // simple tax for example
            Console.WriteLine($"[Salaried] Gross: {gross:C}, Net: {net:C}");
        }
    }

    /// <summary>
    /// Hourly employee implements the abstract members and customizes gross salary
    /// calculation by overriding GetGrossSalary.
    /// </summary>
    public class HourlyEmployee : AbstractBaseEmployee
    {
        private double _hourlyRate;
        private double _hoursWorked;

        public override double Bonus => 0.0; // contractors or hourly workers may not receive a bonus

        public HourlyEmployee(int id, string name, double baseSalary, double hourlyRate)
            : base(id, name, baseSalary)
        {
            _hourlyRate = hourlyRate;
            _hoursWorked = 160; // assume full-time hours for demo
        }

        public override void CalculateSalary()
        {
            var gross = GetGrossSalary();
            var net = gross - (gross * 0.1);
            Console.WriteLine($"[Hourly] Gross: {gross:C}, Net: {net:C}");
        }

        // Override virtual behavior to include hourly pay in gross calculation
        public override double GetGrossSalary()
        {
            return BaseSalary + (_hourlyRate * _hoursWorked) + Bonus;
        }
    }
}
