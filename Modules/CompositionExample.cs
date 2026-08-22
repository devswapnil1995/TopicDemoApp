using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp.Modules
{
    /// <summary>
    /// Demonstrates composition (the "has-a" relationship) in C# using a
    /// Computer composed of CPU and RAM objects. The Computer class owns
    /// and manages the lifetime of its parts (composition).
    /// </summary>
    public class CompositionExample : ITopicModule
    {
        public string Name => "Composition Example";
        public string Description => "Demonstrates composition (has-a relationship) in C# using a Computer/CPU/RAM example.";

        public void Run()
        {
            Console.WriteLine("Composition Example:\n");
            // Create a Computer object which composes CPU and RAM
            Computer pc = new Computer("Dell", "Intel i7", 16);
            pc.Boot();
            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Represents a CPU component. In composition the CPU is a part of a Computer
    /// and is typically created and owned by the Computer instance.
    /// </summary>
    public class CPU
    {
        public string Model { get; set; }

        public CPU(string model) { Model = model; }

        public void Process()
        {
            Console.WriteLine($"{Model} CPU is processing data");
        }
    }

    /// <summary>
    /// Represents RAM memory component.
    /// </summary>
    public class RAM
    {
        public int Size { get; set; }

        public RAM(int size) { Size = size; }

        public void LoadData()
        {
            Console.WriteLine($"{Size}GB RAM is loading data");
        }
    }

    /// <summary>
    /// Computer composes CPU and RAM objects. The Computer is responsible for
    /// creating and using its parts, demonstrating composition (owning the parts).
    /// </summary>
    public class Computer
    {
        private readonly CPU cpu;
        private readonly RAM ram;
        public string Brand { get; set; }

        public Computer(string brand, string cpuModel, int ramSize)
        {
            Brand = brand;
            cpu = new CPU(cpuModel);
            ram = new RAM(ramSize);
            Console.WriteLine($"{Brand} computer assembled");
        }

        public void Boot()
        {
            Console.WriteLine($"Booting {Brand} computer...");
            ram.LoadData();
            cpu.Process();
            Console.WriteLine("Computer ready!");
        }
    }

}
