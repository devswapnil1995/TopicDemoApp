using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates various constructor types in C# using an employee example.
    /// Covers:
    /// - Default constructor (no parameters)
    /// - Parameterized constructors (one or more parameters)
    /// - Copy constructor (create a new object from an existing one)
    /// - Static constructor (initializes static members once per type)
    /// </summary>
    public class ConstructorExample : ITopicModule
    {
        public string Name => "Constructor Example";
        public string Description => "Demonstrates constructors in C# with an employee/company example.";

        public void Run()
        {
            Console.WriteLine("Constructor examples:\n");

            // Default constructor
            var defaultEmployee = new EmployeeCon();
            Console.WriteLine("Default constructor:");
            defaultEmployee.ShowDetails();
            Console.WriteLine();

            // Parameterized constructor
            var paramEmployee = new EmployeeCon(1, "Alice", 50000m, "Engineering");
            Console.WriteLine("Parameterized constructor:");
            paramEmployee.ShowDetails();
            Console.WriteLine();

            // Copy constructor
            var copyEmployee = new EmployeeCon(paramEmployee);
            Console.WriteLine("Copy constructor (copy of Alice):");
            copyEmployee.ShowDetails();
            Console.WriteLine();

            // Show static information initialized by the static constructor
            Console.WriteLine($"Company Name (static): {EmployeeCon.CompanyName}");
            Console.WriteLine($"Total Employee Instances created (static counter): {EmployeeCon.InstanceCount}");

            // Pause so the user can read the output before returning to the menu
            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Example employee class that implements several constructor patterns.
    /// </summary>
    public class EmployeeCon
    {
        // Static members shared by the type
        public static string CompanyName { get; private set; }
        public static int InstanceCount { get; private set; }

        // Instance properties
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Salary { get; private set; }
        public string Department { get; private set; }

        // Static constructor: runs once before the first access to any static or instance member
        static EmployeeCon()
        {
            CompanyName = "Contoso Ltd.";
            InstanceCount = 0;
            // Note: avoid heavy work in static constructor; it's executed once per type
        }

        /// <summary>
        /// Default constructor (no parameters). Initializes default values.
        /// </summary>
        public EmployeeCon()
        {
            Id = 0;
            Name = "Unknown";
            Salary = 0m;
            Department = "General";
            InstanceCount++;
        }

        /// <summary>
        /// Parameterized constructor. Initializes the object with specific values.
        /// </summary>
        public EmployeeCon(int id, string name, decimal salary)
            : this() // reuse default initialization and increment count once in default
        {
            Id = id;
            Name = name;
            Salary = salary;
            // Department remains as default unless another ctor sets it
        }

        /// <summary>
        /// Parameterized constructor with department.
        /// </summary>
        public EmployeeCon(int id, string name, decimal salary, string department)
            : this(id, name, salary)
        {
            Department = department;
        }

        /// <summary>
        /// Copy constructor: creates a new object from an existing EmployeeCon.
        /// </summary>
        public EmployeeCon(EmployeeCon other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            Id = other.Id;
            Name = other.Name + " (Copy)";
            Salary = other.Salary;
            Department = other.Department;
            InstanceCount++;
        }

        public void ShowDetails()
        {
            Console.WriteLine($"Company: {CompanyName}");
            Console.WriteLine($"Employee ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Salary: {Salary:C}");
            Console.WriteLine($"Department: {Department}");
        }
    }
}
