#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates nullable reference types (C# 8+) and nullable value types.
    /// This file enables nullable context locally with '#nullable enable'.
    ///
    /// Topics covered:
    /// - string? (nullable reference types)
    /// - null-coalescing (??) and null-conditional (?.) operators
    /// - null-forgiving operator (!) to silence the compiler
    /// - nullable value types Nullable&lt;T&gt; / T?
    /// </summary>
    public class NullableReferenceTypesExample : ITopicModule
    {
        public string Name => "Nullable Reference & Value Types";
        public string Description => "Shows nullable reference types (string?) and nullable value types (int?).";

        public void Run()
        {
            Console.WriteLine("Nullable Reference Types demo:\n");

            // Nullable reference type: the compiler treats string? as possibly null
            string? maybeName = null;
            Console.WriteLine($"maybeName is null: {maybeName == null}");

            // Use the null-coalescing operator to provide a fallback
            Console.WriteLine($"Name or fallback: {maybeName ?? "(no name provided)"}");

            // Use null-conditional operator to safely access members
            Console.WriteLine($"Length (safe): {maybeName?.Length}");

            // Assign a non-null value
            maybeName = "Swapnil";
            Console.WriteLine($"maybeName now: {maybeName}");
            Console.WriteLine($"Length: {maybeName.Length}");

            // Null-forgiving operator: tells the compiler the expression is not null
            // Use sparingly when you know the value cannot be null
            string definitelyNotNull = maybeName!; // no warning here
            Console.WriteLine($"definitelyNotNull: {definitelyNotNull}");

            Console.WriteLine();
            Console.WriteLine("Nullable value types (Nullable<T> / T?) demo:");

            // defining Nullable type
            Nullable<int> n = null;
            // using GetValueOrDefault will return 0 when null
            Console.WriteLine($"Nullable<int> n default: {n.GetValueOrDefault()}");

            // shorthand syntax
            int? n1 = null;
            Console.WriteLine($"int? n1 default: {n1.GetValueOrDefault()}");

            int? n2 = 47;
            Console.WriteLine($"int? n2 has value: {n2.GetValueOrDefault()}");

            Nullable<int> n3 = 457;
            Console.WriteLine($"Nullable<int> n3 has value: {n3.GetValueOrDefault()}");

            // Use HasValue and Value properties
            if (n3.HasValue)
            {
                Console.WriteLine($"n3.Value: {n3.Value}");
            }

            // Null-coalescing with value types
            int result = n1 ?? -1;
            Console.WriteLine($"n1 coalesced to: {result}");

            Console.WriteLine();
            Console.WriteLine("Notes:");
            Console.WriteLine("- Enable nullable reference types in your project (csproj) with <Nullable>enable</Nullable> to get compiler warnings.");
            Console.WriteLine("- Use string? to indicate a reference may be null and string (non-nullable) when it must not be null.");
            Console.WriteLine("- Prefer null-coalescing and null-conditional operators over suppressing warnings with '!'.");

            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }
    }
}
