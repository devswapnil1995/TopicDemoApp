using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// A small demo module that shows basic abstraction and encapsulation concepts.
    ///
    /// Abstraction: The <see cref="Employee"/> class exposes a simple public method
    /// (ShowEmployeeDetails) so callers can get the employee information without
    /// needing to know how the net salary is calculated.
    ///
    /// Encapsulation: Employee state and implementation details (fields and the
    /// CalculateSalary method) are kept private so they cannot be modified
    /// directly from outside the class.
    /// </summary>
    public class AbstractionEncapsulationBasic : ITopicModule
    {
        public string Name => "Abstraction and Encapsulation - Basic";
        public string Description => "Learn the basics of abstraction and encapsulation in C#.";

        /// <summary>
        /// Entry point for the demo. Creates Employee instances and shows details.
        /// The caller does not need to know how net salary is computed; that logic
        /// is encapsulated inside Employee.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Running Abstraction and Encapsulation - Basic...");

            // Even though we pass the gross salary, the consumer only sees the
            // resulting net salary. The calculation is hidden inside Employee.
            Employee employeeSwapnil = new Employee(1, "Swapnil", 31000);
            employeeSwapnil.ShowEmployeeDetails();

            Console.ReadLine(); // Wait for user to observe output

            Employee employeeOmkar = new Employee(2, "Omkar", 29000);
            employeeOmkar.ShowEmployeeDetails();

            Console.ReadLine(); // Wait for user to observe output
        }
    }

    /// <summary>
    /// Represents an employee and encapsulates salary calculation details.
    /// </summary>
    public class Employee
    {
        // Private fields keep internal state hidden (encapsulation)
        private int EmpId;
        private string EmpName;
        private double GrossSalary; // Gross salary provided to the class
        private double TaxDeuction = 0.1; // Flat tax deduction used in this example
        private double NetSalary; // Calculated net salary

        /// <summary>
        /// Initializes a new instance of Employee with the provided values.
        /// </summary>
        /// <param name="EmpId">Employee identifier</param>
        /// <param name="EmpName">Employee name</param>
        /// <param name="GrossSalary">Gross salary used to compute net salary</param>
        public Employee(int EmpId, string EmpName, double GrossSalary)
        {
            this.EmpId = EmpId;
            this.EmpName = EmpName;
            this.GrossSalary = GrossSalary;
        }

        /// <summary>
        /// Calculates the net salary from the gross salary and stores it in a private field.
        /// This method is private because calculation details are an implementation detail
        /// that should not be visible to code that uses Employee.
        /// </summary>
        /// <param name="GrossSalary">The gross salary to calculate from</param>
        private void CalculateSalary(double GrossSalary)
        {
            if (GrossSalary > 30000)
            {
                this.NetSalary = GrossSalary - (GrossSalary * TaxDeuction);
            }
            else
            {
                this.NetSalary = GrossSalary;
            }
        }

        /// <summary>
        /// Public method that exposes the employee's details. Callers do not need to
        /// know how the NetSalary was calculated; they only get the final values.
        /// This demonstrates abstraction by providing a simple interface to the
        /// underlying behavior.
        /// </summary>
        public void ShowEmployeeDetails()
        {
            CalculateSalary(this.GrossSalary);
            Console.WriteLine("EmpId: {0}", this.EmpId);
            Console.WriteLine("EmpName: {0}", this.EmpName);
            Console.WriteLine("NetSalary: {0}", this.NetSalary);
        }
    }
}
