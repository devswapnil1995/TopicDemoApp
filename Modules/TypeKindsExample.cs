using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates the differences between class, struct, record, and record struct.
    /// - class: reference type (heap), assignment copies reference, mutable by default.
    /// - struct: value type (stack/inline), assignment copies value, often used for small immutable types.
    /// - record: reference-type record with value-based equality and useful immutability with `with` expressions.
    /// - record struct: value-type record (value semantics) with generated value equality and `with` support.
    /// </summary>
    public class TypeKindsExample : ITopicModule
    {
        public string Name => "Type Kinds: class / struct / record / record struct";
        public string Description => "Compare behavior and semantics of class, struct, record, and record struct.";

        public void Run()
        {
            Console.WriteLine("Type Kinds Example:\n");

            // CLASS (reference type)
            var c1 = new PersonClass("Alice", 30);
            var c2 = c1; // copies reference
            Console.WriteLine("Class (reference type)");
            Console.WriteLine($"c1: {c1}");
            Console.WriteLine($"c2 (after assignment from c1): {c2}");
            c2.Age = 31; // mutating through c2 affects c1
            Console.WriteLine("After c2.Age = 31:");
            Console.WriteLine($"c1: {c1}");
            Console.WriteLine($"ReferenceEquals(c1,c2): {ReferenceEquals(c1,c2)}");
            Console.WriteLine();

            // STRUCT (value type)
            var s1 = new PersonStructKS("Bob", 25);
            var s2 = s1; // copies value
            Console.WriteLine("Struct (value type)");
            Console.WriteLine($"s1: {s1}");
            Console.WriteLine($"s2 (after assignment from s1): {s2}");
            s2.Age = 26; // mutating s2 does not affect s1
            Console.WriteLine("After s2.Age = 26:");
            Console.WriteLine($"s1: {s1}");
            Console.WriteLine($"s2: {s2}");
            Console.WriteLine($"s1.Equals(s2): {s1.Equals(s2)}");
            Console.WriteLine();

            // RECORD (reference type with value equality)
            var r1 = new PersonRecordKS("Carol", 40);
            var r2 = r1 with { Age = 41 }; // 'with' creates a new record
            Console.WriteLine("Record (reference type with value-based equality)");
            Console.WriteLine($"r1: {r1}");
            Console.WriteLine($"r2 (r1 with Age=41): {r2}");
            Console.WriteLine($"r1 == r2: {r1 == r2}");
            var r3 = new PersonRecordKS("Carol", 40);
            Console.WriteLine($"r1 == r3 (same values): {r1 == r3}");
            Console.WriteLine();

            // RECORD STRUCT (value type record)
            var rs1 = new PersonRecordStructKS("Dave", 35);
            var rs2 = rs1 with { Age = 36 }; // produces a new value
            Console.WriteLine("Record struct (value type record)");
            Console.WriteLine($"rs1: {rs1}");
            Console.WriteLine($"rs2: {rs2}");
            Console.WriteLine($"rs1.Equals(rs2): {rs1.Equals(rs2)}");
            Console.WriteLine();

            Console.WriteLine("Summary:");
            Console.WriteLine("- Classes are reference types: assignments copy references; multiple variables may refer to same object.");
            Console.WriteLine("- Structs are value types: assignments copy the value; changes to one copy do not affect others.");
            Console.WriteLine("- Records (reference) provide compiler-generated value equality, immutability patterns with `with`, and concise syntax.");
            Console.WriteLine("- Record structs combine value-type semantics with record features (value equality, with-expressions).");

            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }
    }

    // ---------- sample types ----------

    // Class: reference type
    public class PersonClass
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public PersonClass(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public override string ToString() => $"PersonClass(Name={Name}, Age={Age})";
    }

    // Struct: value type
    public struct PersonStructKS
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public PersonStructKS(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public override string ToString() => $"PersonStruct(Name={Name}, Age={Age})";
    }

    // Record: reference type record with value equality and `with`
    public record PersonRecordKS(string Name, int Age);

    // Record struct: value type record
    public record struct PersonRecordStructKS(string Name, int Age);
}
