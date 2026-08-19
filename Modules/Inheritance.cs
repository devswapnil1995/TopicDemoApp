using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates inheritance and polymorphism using a small company/staff example.
    ///
    /// - StaffMember is the base class that encapsulates common state and behavior.
    /// - Manager, Developer and Contractor derive from StaffMember and override
    ///   behavior to provide role-specific logic.
    /// - Run creates a list of StaffMember instances and shows how polymorphism
    ///   allows calling the same method to produce different results.
    /// </summary>
    public class Inheritance : ITopicModule
    {
        public string Name => "Inheritance";
        public string Description => "Demonstrates the concept of inheritance in C#.";

        public void Run()
        {
            Console.WriteLine("Inheritance module is running.\n");

            // Create a heterogeneous collection of staff members
            var staff = new List<StaffMember>
            {
                new Manager(1, "Alice", 80000),
                new Developer(2, "Bob", 60000),
                new Contractor(3, "Charlie", 40000)
            };

            // Polymorphism: the loop treats every item as a StaffMember but the
            // overridden methods run for each concrete type.
            foreach (var s in staff)
            {
                s.ShowDetails();
                Console.WriteLine();
            }
        }
    }

    /// <summary>
    /// Base class representing a staff member in the company. It encapsulates
    /// common fields and provides virtual members that derived classes can
    /// override to implement specialized behavior.
    /// </summary>
    public class StaffMember
    {
        // Private fields to demonstrate encapsulation
        private readonly int _id;
        private readonly string _name;
        private readonly double _baseSalary;
        private readonly double _taxRate = 0.1; // simple flat tax for demo

        public StaffMember(int id, string name, double baseSalary)
        {
            _id = id;
            _name = name;
            _baseSalary = baseSalary;
        }

        /// <summary>
        /// Returns the role name. Derived classes should override this.
        /// </summary>
        public virtual string Role => "Staff";

        /// <summary>
        /// Calculates a role-specific bonus. Base implementation returns 0.
        /// Derived types override to provide meaningful bonus calculations.
        /// </summary>
        public virtual double CalculateBonus()
        {
            return 0.0;
        }

        /// <summary>
        /// Calculates net salary after tax and including any bonus. This method
        /// is non-virtual to show a common algorithm that uses virtual extension points.
        /// </summary>
        /// <summary>
        /// Protected access for derived classes that need to read the base salary.
        /// Using a protected getter preserves encapsulation while allowing subclasses
        /// to implement role-specific calculations.
        /// </summary>
        protected double BaseSalary => _baseSalary;

        public double CalculateNetSalary()
        {
            var bonus = CalculateBonus();
            var gross = _baseSalary + bonus;
            var net = gross - (gross * _taxRate);
            return net;
        }

        /// <summary>
        /// Shows information about the staff member. Derived classes inherit this
        /// behavior and will display role-specific details because CalculateBonus
        /// and Role are virtual.
        /// </summary>
        public void ShowDetails()
        {
            Console.WriteLine("Id: {0}", _id);
            Console.WriteLine("Name: {0}", _name);
            Console.WriteLine("Role: {0}", Role);
            Console.WriteLine("Base Salary: {0:C}", _baseSalary);
            Console.WriteLine("Bonus: {0:C}", CalculateBonus());
            Console.WriteLine("Net Salary: {0:C}", CalculateNetSalary());
        }
    }

    /// <summary>
    /// Manager receives a higher bonus percentage.
    /// </summary>
    public class Manager : StaffMember
    {
        public Manager(int id, string name, double baseSalary)
            : base(id, name, baseSalary)
        {
        }

        public override string Role => "Manager";

        public override double CalculateBonus()
        {
            // Managers get 20% of base salary as bonus
            return 0.20 * BaseSalary;
        }
    }

    /// <summary>
    /// Developer receives a moderate bonus (e.g., performance bonus).
    /// </summary>
    public class Developer : StaffMember
    {
        public Developer(int id, string name, double baseSalary)
            : base(id, name, baseSalary)
        {
        }

        public override string Role => "Developer";

        public override double CalculateBonus()
        {
            // Developers get 10% of base salary as bonus
            return 0.10 * BaseSalary;
        }
    }

    /// <summary>
    /// Contractor does not receive the company bonus in this simple example.
    /// </summary>
    public class Contractor : StaffMember
    {
        public Contractor(int id, string name, double baseSalary)
            : base(id, name, baseSalary)
        {
        }

        public override string Role => "Contractor";

        public override double CalculateBonus()
        {
            return 0.0;
        }
    }
}
