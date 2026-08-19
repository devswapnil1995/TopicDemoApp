using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates sealed classes and sealed overrides using an employee example.
    /// Covers:
    /// - sealed classes (cannot be inherited)
    /// - sealed overrides (prevents further overriding of an inherited virtual method)
    /// - why and when to seal members or classes
    /// </summary>
    public class SealedClassMethodExample : ITopicModule
    {
        public string Name => "Sealed Classes & Methods";
        public string Description => "Shows sealed classes and sealed overrides with employees.";

        public void Run()
        {
            Console.WriteLine("Sealed classes and methods demo:\n");

            var workers = new List<SealedEmployeeBase>
            {
                new TeamLead(1, "Alice", 90000),
                new SeniorLeader(2, "Bob", 120000),
                new ContractorSealed(3, "Charlie", 3000)
            };

            foreach (var w in workers)
            {
                w.ShowInfo();
                Console.WriteLine("Calculated Bonus: {0:C}", w.CalculateBonus());
                Console.WriteLine();
            }

            Console.WriteLine("Notes:");
            Console.WriteLine("- TeamLead seals the DisplayRole method so further derived classes cannot override it.");
            Console.WriteLine("- ContractorSealed is a sealed class; it cannot be used as a base class.");
            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Base class for the sealed example. It exposes virtual members that
    /// derived classes can override.
    /// </summary>
    public class SealedEmployeeBase
    {
        public int Id { get; }
        public string Name { get; }
        protected double BaseSalary { get; }

        public SealedEmployeeBase(int id, string name, double baseSalary)
        {
            Id = id;
            Name = name;
            BaseSalary = baseSalary;
        }

        // Virtual method that can be overridden and optionally sealed by a derived class
        public virtual string DisplayRole()
        {
            return "Employee";
        }

        // Virtual method for bonus calculation
        public virtual double CalculateBonus()
        {
            // Default: no bonus
            return 0.0;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Role (via DisplayRole): {DisplayRole()}");
            Console.WriteLine($"Base Salary: {BaseSalary:C}");
        }
    }

    /// <summary>
    /// TeamLead overrides CalculateBonus and seals DisplayRole so further derived
    /// classes cannot change how the role is displayed. Sealing a method is useful
    /// when the base implementation must remain stable for correctness or security.
    /// </summary>
    public class TeamLead : SealedEmployeeBase
    {
        public TeamLead(int id, string name, double baseSalary)
            : base(id, name, baseSalary)
        {
        }

        // Override CalculateBonus normally
        public override double CalculateBonus()
        {
            // Team leads get 15% of base salary
            return BaseSalary * 0.15;
        }

        // Seal the DisplayRole override to prevent further overrides in subclasses
        public sealed override string DisplayRole()
        {
            return "Team Lead";
        }
    }

    /// <summary>
    /// SeniorLeader derives from TeamLead. It can override CalculateBonus but cannot
    /// override DisplayRole because TeamLead sealed that method. This shows sealed override
    /// behavior: only methods that are overriding can be sealed.
    /// </summary>
    public class SeniorLeader : TeamLead
    {
        public SeniorLeader(int id, string name, double baseSalary)
            : base(id, name, baseSalary)
        {
        }

        // Allowed: override CalculateBonus because TeamLead did not seal it
        public override double CalculateBonus()
        {
            // Senior leaders get 25% of base salary
            return BaseSalary * 0.25;
        }

        // Not allowed (would not compile): trying to override DisplayRole here will fail
        // public override string DisplayRole() { return "Senior Leader"; }
    }

    /// <summary>
    /// A sealed class cannot be used as a base class. Use sealed when you want to
    /// prevent inheritance (for security, versioning, or performance reasons).
    /// </summary>
    public sealed class ContractorSealed : SealedEmployeeBase
    {
        public ContractorSealed(int id, string name, double fixedFee)
            : base(id, name, fixedFee)
        {
        }

        public override double CalculateBonus()
        {
            // Contractors do not receive a company bonus in this example
            return 0.0;
        }
    }
}
