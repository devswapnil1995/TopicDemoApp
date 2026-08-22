using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates ref, out and in parameter modifiers in C# with simple examples.
    /// - ref: pass variable by reference (must be definitely assigned before call)
    /// - out: pass variable by reference for output (must be assigned by callee)
    /// - in: pass readonly by reference (cannot be modified by callee; useful for large structs)
    /// </summary>
    public class RefOutInExample : ITopicModule
    {
        public string Name => "ref / out / in Example";
        public string Description => "Shows differences between ref, out and in parameter modifiers.";

        public void Run()
        {
            Console.WriteLine("ref / out / in demonstration:\n");

            // ref example
            int a = 10;
            Console.WriteLine($"Before ref call: a = {a}");
            IncrementByRef(ref a);
            Console.WriteLine($"After ref call: a = {a}");
            Console.WriteLine();

            // out example
            Console.WriteLine("Out example: parsing integers from strings:");
            string good = "123";
            string bad = "xyz";
            if (TryParseAndDouble(good, out int doubledGood))
            {
                Console.WriteLine($"Parsed and doubled '{good}' -> {doubledGood}");
            }
            else
            {
                Console.WriteLine($"Failed to parse '{good}'");
            }

            if (TryParseAndDouble(bad, out int doubledBad))
            {
                Console.WriteLine($"Parsed and doubled '{bad}' -> {doubledBad}");
            }
            else
            {
                Console.WriteLine($"Failed to parse '{bad}' -> out param set to {doubledBad}");
            }

            Console.WriteLine();

            // in example: use a large struct to illustrate passing by readonly reference
            var large = new LargeStruct { A = 1, B = 2, C = 3, D = 4 };
            Console.WriteLine("Calling DisplayLargeStruct(in large) — callee receives a readonly reference (no copy for large structs).");
            DisplayLargeStruct(in large);

            Console.WriteLine();
            Console.WriteLine("Notes:");
            Console.WriteLine("- ref: caller and callee share the same variable; callee can read and modify it.");
            Console.WriteLine("- out: caller passes an uninitialized variable; callee must assign it before returning.");
            Console.WriteLine("- in: readonly reference; callee may not modify the value and it's efficient for large value types.");

            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }

        // ----------------------- ref example -----------------------
        /// <summary>
        /// Increments the passed integer by reference. The caller's variable is modified.
        /// </summary>
        private void IncrementByRef(ref int value)
        {
            value += 5;
        }

        // ----------------------- out example -----------------------
        /// <summary>
        /// Tries to parse a string to int and returns double of that value via out parameter.
        /// The out parameter must be assigned by this method before returning.
        /// </summary>
        private bool TryParseAndDouble(string s, out int result)
        {
            if (int.TryParse(s, out int parsed))
            {
                result = parsed * 2;
                return true;
            }
            result = 0; // out must be assigned
            return false;
        }

        // ----------------------- in example -----------------------
        /// <summary>
        /// Receives a LargeStruct by readonly reference. The method cannot modify the struct.
        /// Using 'in' avoids copying large structs while preventing mutation.
        /// </summary>
        private void DisplayLargeStruct(in LargeStruct s)
        {
            // s.A = 10; // compile-time error: cannot assign to variable 'in parameter'
            Console.WriteLine($"LargeStruct contents: {{ A={s.A}, B={s.B}, C={s.C}, D={s.D} }}");
            Console.WriteLine($"Sum: {s.A + s.B + s.C + s.D}");
        }
    }

    /// <summary>
    /// Example large value type to demonstrate 'in' parameter efficiency.
    /// </summary>
    public struct LargeStruct
    {
        public int A;
        public int B;
        public int C;
        public int D;
    }
}

