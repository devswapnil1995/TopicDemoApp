using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates interfaces in C# using an employee/company example.
    /// Covers:
    /// - defining interfaces and interface inheritance
    /// - multiple interface implementation
    /// - default interface members
    /// - explicit interface implementation
    /// - interface-based polymorphism
    /// - extension methods for interfaces
    /// </summary>
    public class InterfaceExample : ITopicModule
    {
        public string Name => "Interface Example";
        public string Description => "Demonstrates interfaces and related concepts using employees.";

        public void Run()
        {
            Console.WriteLine("Interface examples:\n");

            // Create several workers that implement IEmployee or IPayable
            var workers = new List<IIdentifiable>
            {
                new FullTimeEmployee(1, "Alice", 90000m, 0.10m),
                new PartTimeEmployee(2, "Bob", 40m, 20),
                new ContractorPayable(3, "Charlie", 3000m) // implements IPayable explicitly
            };

            // Use interface-based polymorphism. We only rely on IIdentifiable to get common data.
            foreach (var w in workers)
            {
                // Extension method defined below prints id and name
                w.PrintIdentity();

                // If the object is an IEmployee we can read Role and call CalculatePay
                if (w is IEmployee emp)
                {
                    Console.WriteLine($"Role: {emp.Role}");
                    // Call default interface member ShowPay (available on IPayable)
                    emp.ShowPay();
                }
                else if (w is IPayable payable)
                {
                    // Contractor implements IPayable explicitly; cast to IPayable to invoke methods
                    payable.ShowPay();
                }

                Console.WriteLine();
            }

            Console.WriteLine("Concepts covered: interface contracts, multiple implementation, explicit implementation, default members, and extension methods.");
            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }
    }

    // Basic interfaces --------------------------------------------------

    /// <summary>
    /// Identifiable entity with Id and Name.
    /// </summary>
    public interface IIdentifiable
    {
        int Id { get; }
        string Name { get; }
    }

    /// <summary>
    /// Represents something that can be paid. Includes a default helper ShowPay
    /// which demonstrates default interface members (C# 8+).
    /// </summary>
    public interface IPayable
    {
        decimal CalculatePay();

        // Default interface method: provides a reusable implementation that
        // calls CalculatePay. Implementations can override by providing their
        // own implementation of ShowPay if desired.
        void ShowPay()
        {
            Console.WriteLine($"Pay: {CalculatePay():C}");
        }
    }

    /// <summary>
    /// Employee interface composes IIdentifiable and IPayable and adds Role.
    /// This demonstrates interface inheritance and composition.
    /// </summary>
    public interface IEmployee : IIdentifiable, IPayable
    {
        string Role { get; }
    }

    // Concrete implementations ------------------------------------------

    /// <summary>
    /// Full-time salaried employee. Implements IEmployee.
    /// </summary>
    public class FullTimeEmployee : IEmployee
    {
        public int Id { get; }
        public string Name { get; }
        public string Role => "FullTime";

        private decimal AnnualSalary { get; }
        private decimal BonusPercent { get; }

        public FullTimeEmployee(int id, string name, decimal annualSalary, decimal bonusPercent)
        {
            Id = id;
            Name = name;
            AnnualSalary = annualSalary;
            BonusPercent = bonusPercent;
        }

        // Monthly pay including pro-rated bonus
        public decimal CalculatePay()
        {
            var monthly = AnnualSalary / 12m;
            var bonus = (AnnualSalary * BonusPercent) / 12m;
            return monthly + bonus;
        }
    }

    /// <summary>
    /// Part-time hourly employee. Implements IEmployee.
    /// </summary>
    public class PartTimeEmployee : IEmployee
    {
        public int Id { get; }
        public string Name { get; }
        public string Role => "PartTime";

        private decimal HourlyRate { get; }
        private int HoursWorked { get; }

        public PartTimeEmployee(int id, string name, decimal hourlyRate, int hoursWorked)
        {
            Id = id;
            Name = name;
            HourlyRate = hourlyRate;
            HoursWorked = hoursWorked;
        }

        public decimal CalculatePay()
        {
            return HourlyRate * HoursWorked;
        }
    }

    /// <summary>
    /// ContractorPayable that only implements IPayable. Demonstrates explicit interface
    /// implementation to hide members from the public surface.
    /// </summary>
    public class ContractorPayable : IPayable, IIdentifiable
    {
        public int Id { get; }
        public string Name { get; }

        private decimal FixedFee { get; }

        public ContractorPayable(int id, string name, decimal fixedFee)
        {
            Id = id;
            Name = name;
            FixedFee = fixedFee;
        }

        // Explicit implementation: CalculatePay is not directly visible on Contractor
        // unless cast to IPayable. This is useful to avoid polluting the public API.
        decimal IPayable.CalculatePay()
        {
            return FixedFee;
        }

        // We can still provide a friendly method if desired
        public void ShowContractorInfo()
        {
            Console.WriteLine($"Contractor Id: {Id}, Name: {Name}");
        }
    }

    // Extension methods for interfaces ----------------------------------
    public static class InterfaceExtensions
    {
        // Extension method for IIdentifiable to print identity information.
        public static void PrintIdentity(this IIdentifiable identifiable)
        {
            Console.WriteLine($"Id: {identifiable.Id}");
            Console.WriteLine($"Name: {identifiable.Name}");
        }
    }
}
