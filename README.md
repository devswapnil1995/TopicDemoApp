TopicDemoApp
=============

This repository contains small C# console demo modules that illustrate common object-oriented concepts (examples: method overloading, method overriding, abstraction and encapsulation).

Prerequisites
-------------
- .NET 10 SDK installed
- Visual Studio 2026 (recommended) or the dotnet CLI (cross-platform)

Open and run in Visual Studio
-----------------------------
1. Open the solution: TopicDemoApp.slnx (located at the repository root).
2. Set the startup project to the console application project (the project that contains Program.cs).
3. Press F5 (Debug) or Ctrl+F5 (Run without debugging).

Run from the command line (dotnet CLI)
-------------------------------------
1. Open a terminal (PowerShell) and change to the repository root:
   cd D:\Swapnil\Projects\TopicDemoApp
2. Build the solution:
   dotnet build
3. Run the console app (replace <ProjectPath> with the relative path to the executable project .csproj if needed):
   dotnet run --project <ProjectPath>

Notes about the examples
------------------------
- The demo modules are located in the Modules/ folder. Each module implements ITopicModule and exposes a Run() method.
  Example files:
  - Modules/MethodOverloading.cs
  - Modules/MethodOverriding.cs
  - Modules/AbstractionEncapsulationBasic.cs

- Many demos write output to the console and use Console.ReadLine() to pause so you can observe results. When you run a demo, follow the on-screen instructions and press Enter when prompted to continue.

How to run a specific module
----------------------------
If the console app does not provide an interactive menu, you can run a specific demo by editing Program.cs (or the app's startup code) and invoking the module's Run() method directly. Example:

// inside Program.cs
var demo = new TopicDemoApp.Modules.MethodOverloading();
demo.Run();

Build and run the app after making the change.

Viewing source and learning
---------------------------
- Open files under the Modules/ folder to read the code and the added XML documentation/comments for each demo.
- Use breakpoints and step-through debugging in Visual Studio to inspect how each method is selected (overloading) or how overrides behave at runtime (overriding), or to observe encapsulation and abstraction in the Employee example.

Contributing
------------
Small improvements and additional demo modules are welcome. Follow the repository's coding style and add XML documentation for new modules.

License
-------
This repository does not include a license file. Add one if you intend to publish or share the code publicly.
