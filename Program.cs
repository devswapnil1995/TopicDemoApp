using TopicDemoApp.Modules;

namespace TopicDemoApp
{
    public class Program
    {
        private static readonly List<ITopicModule> Modules = new()
        {
            new AbstractionEncapsulationBasicExample(),
            new MethodOverloadingExample(),
            new MethodOverridingExample(),
            new InheritanceExample(),
            new AbstractClassExample(),
            new InterfaceExample(),
            new SealedClassMethodExample(),
            new StaticVsInstanceExample(),
            new ConstructorExample(),
            new AccessModifiersExample(),
            new CompositionExample(),
            new BoxingUnboxingExample(),
            new RefOutInExample(),
            new NullableReferenceTypesExample(),
            // add other modules here
        };

        private static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("StudyApp - Topics");
                Console.WriteLine("Select a topic to run:");
                Console.WriteLine();

                for (int i = 0; i < Modules.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Modules[i].Name} - {Modules[i].Description}");
                }

                Console.WriteLine("0. Exit");
                Console.WriteLine();
                Console.Write("Enter choice: ");

                var choice = Console.ReadLine();
                if (int.TryParse(choice, out var n))
                {
                    if (n == 0) return;
                    if (n >= 1 && n <= Modules.Count)
                    {
                        Modules[n - 1].Run();
                        continue;
                    }
                }

                Console.WriteLine("Invalid choice. Press Enter to try again...");
                Console.ReadLine();
            }
        }
    }
}