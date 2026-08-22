using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    public class BoxingUnboxingExample : ITopicModule
    {
        public string Name => "Boxing and Unboxing Example";
        public string Description => "Demonstrates boxing and unboxing in C# with value types, structs and interfaces.";

        public void Run()
        {
            Console.WriteLine("Boxing and Unboxing Demo:\n");

            // Boxing: value type -> object (heap)
            int value = 123;
            Console.WriteLine($"Original int value: {value}");

            object boxed = value; // boxing
            Console.WriteLine($"After boxing into object: {boxed} (type: {boxed.GetType()})");

            // Changing the original value does not change the boxed copy
            value = 456;
            Console.WriteLine($"Changed original int to: {value}");
            Console.WriteLine($"Boxed object still holds: {boxed}");

            // Unboxing: object -> value type (must cast to the exact value type)
            int unboxed = (int)boxed;
            Console.WriteLine($"Unboxed value: {unboxed}");

            // Wrong unboxing causes InvalidCastException
            try
            {
                // boxed currently contains an int, not a long
                long wrong = (long)boxed; // invalid unboxing
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"InvalidCastException on wrong unboxing: {ex.Message}");
            }

            Console.WriteLine();

            // Boxing with structs: demonstrates copy semantics
            var es = new EmployeeStruct { Id = 1, Name = "Alice" };
            Console.WriteLine($"Original struct before boxing: {es}");
            object boxedStruct = es; // boxing creates a copy on the heap

            // Modify original struct; boxed copy remains unchanged
            es.Name = "Bob";
            Console.WriteLine($"Original struct after modification: {es}");
            Console.WriteLine($"Boxed struct still holds: {boxedStruct}");

            // Unbox back to a struct value
            var unboxedStruct = (EmployeeStruct)boxedStruct;
            Console.WriteLine($"Unboxed struct: {unboxedStruct}");

            Console.WriteLine();

            // Boxing when assigning a value type to an interface reference
            var number = new NumberStruct(99);
            Console.WriteLine("NumberStruct before boxing and interface call:");
            number.Print();

            // Assign to interface - this causes boxing of the value type
            INumberPrinter printer = number; // boxes NumberStruct
            Console.WriteLine("Calling Print() via interface reference (operates on boxed copy):");
            printer.Print();

            // Mutate original value; boxed interface copy is unaffected
            number.Value = 100;
            Console.WriteLine($"Original NumberStruct after mutation: {number.Value}");
            Console.WriteLine("Boxed copy (via interface) remains the same when printed again:");
            printer.Print();

            Console.WriteLine();
            Console.WriteLine("Notes:");
            Console.WriteLine("- Boxing copies the value type into an object on the heap.");
            Console.WriteLine("- Unboxing extracts the value; it requires the correct target type or an exception is thrown.");
            Console.WriteLine("- Boxing/unboxing has runtime cost and can affect performance and memory pressure.");

            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }
    }

    // Simple struct used to demonstrate boxing/unboxing behavior
    public struct EmployeeStruct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString() => $"Id={Id}, Name={Name}";
    }

    // Interface and value type demonstrating boxing when a value type is assigned to an interface
    public interface INumberPrinter
    {
        void Print();
    }

    public struct NumberStruct : INumberPrinter
    {
        public int Value;
        public NumberStruct(int v) { Value = v; }
        public void Print() => Console.WriteLine($"NumberStruct value = {Value}");
    }
}
