using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates C# access modifiers with a company/employee themed example.
    /// Shows which members are accessible from the declaring class, derived classes
    /// in the same assembly, and non-derived classes in the same assembly.
    ///
    /// Note: some behaviors (internal vs protected internal across assemblies)
    /// require a second assembly to demonstrate fully; comments indicate the
    /// expected cross-assembly behavior.
    /// </summary>
    public class AccessModifiersExample : ITopicModule
    {
        public string Name => "Access Modifiers";
        public string Description => "Demonstrates public, private, protected, internal, protected internal and private protected.";

        public void Run()
        {
            Console.WriteLine("Access Modifiers Demo:\n");

            var baseObj = new AccessBase();
            Console.WriteLine("From AccessBase.ShowWithinBase():");
            baseObj.ShowWithinBase();
            Console.WriteLine();

            var derived = new SameAssemblyDerived();
            Console.WriteLine("From SameAssemblyDerived.ShowFromDerived():");
            derived.ShowFromDerived();
            Console.WriteLine();

            var nonDerived = new NonDerivedClassSameAssembly();
            Console.WriteLine("From NonDerivedClassSameAssembly.ShowFromNonDerived():");
            nonDerived.ShowFromNonDerived();
            Console.WriteLine();

            Console.WriteLine("Notes:");
            Console.WriteLine("- public: accessible everywhere (any assembly)");
            Console.WriteLine("- private: accessible only inside the declaring class (hidden from derived/non-derived)");
            Console.WriteLine("- protected: accessible in declaring class and derived classes (even in other assemblies if derived)");
            Console.WriteLine("- internal: accessible anywhere inside the same assembly (project), not from other assemblies");
            Console.WriteLine("- protected internal: accessible inside same assembly OR in derived classes (other assemblies too)");
            Console.WriteLine("- private protected: accessible only in derived classes that are in the same assembly (not visible to other types or other assemblies)");

            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Base class declaring members with different access modifiers.
    /// </summary>
    public class AccessBase
    {
        // Public: visible anywhere
        public string PublicInfo = "Public Info";

        // Private: visible only inside AccessBase
        private string PrivateInfo = "Private Info";

        // Protected: visible inside AccessBase and derived classes
        protected string ProtectedInfo = "Protected Info";

        // Internal: visible anywhere in the same assembly
        internal string InternalInfo = "Internal Info";

        // Protected internal: visible in same assembly OR in derived classes (other assemblies too)
        protected internal string ProtectedInternalInfo = "Protected Internal Info";

        // Private protected: visible only in derived classes that are in the same assembly
        private protected string PrivateProtectedInfo = "Private Protected Info";

        public void ShowWithinBase()
        {
            // All members are accessible inside the declaring class, including private
            Console.WriteLine($"Public: {PublicInfo}");
            Console.WriteLine($"Private: {PrivateInfo}");
            Console.WriteLine($"Protected: {ProtectedInfo}");
            Console.WriteLine($"Internal: {InternalInfo}");
            Console.WriteLine($"Protected Internal: {ProtectedInternalInfo}");
            Console.WriteLine($"Private Protected: {PrivateProtectedInfo}");
        }
    }

    /// <summary>
    /// Derived class in the same assembly. It can access protected, internal,
    /// protected internal and private protected members.
    /// </summary>
    public class SameAssemblyDerived : AccessBase
    {
        public void ShowFromDerived()
        {
            // Accessible: public, protected, internal, protected internal, private protected
            Console.WriteLine($"Public: {PublicInfo}");
            // PrivateInfo is NOT accessible here (would be a compile error):
            // Console.WriteLine(PrivateInfo);
            Console.WriteLine($"Protected: {ProtectedInfo}");
            Console.WriteLine($"Internal: {InternalInfo}");
            Console.WriteLine($"Protected Internal: {ProtectedInternalInfo}");
            Console.WriteLine($"Private Protected: {PrivateProtectedInfo}");
        }
    }

    /// <summary>
    /// Non-derived class in the same assembly. It can access public, internal,
    /// and protected internal (because protected internal behaves as internal here).
    /// It cannot access protected or private protected members.
    /// </summary>
    public class NonDerivedClassSameAssembly
    {
        public void ShowFromNonDerived()
        {
            var b = new AccessBase();
            Console.WriteLine($"Public: {b.PublicInfo}");

            // Private is not accessible: b.PrivateInfo -> compile error

            // Protected is not accessible here: b.ProtectedInfo -> compile error

            // Internal is accessible because we're in the same assembly
            Console.WriteLine($"Internal: {b.InternalInfo}");

            // Protected internal: accessible here because of internal (same assembly)
            Console.WriteLine($"Protected Internal: {b.ProtectedInternalInfo}");

            // Private protected is NOT accessible here (only visible to derived types in same assembly)
            // Console.WriteLine(b.PrivateProtectedInfo); // compile error if uncommented
        }
    }
}
