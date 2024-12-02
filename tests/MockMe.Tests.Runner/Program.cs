// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Reflection;

Console.WriteLine("Hello, World!");

var testsFolderPath = Path.Combine(Assembly.GetExecutingAssembly().Location, @"..\..\..\..\..");

var slnf = Path.Combine(testsFolderPath, "MockMe.Tests.slnf");

Thread.Sleep(5000);

ProcessStartInfo cleanStartInfo = new() { FileName = "dotnet", Arguments = $"clean {slnf}" };

using Process clean =
    Process.Start(cleanStartInfo)
    ?? throw new InvalidOperationException("process must not be null");
await clean.WaitForExitAsync();

ProcessStartInfo buildStartInfo =
    new() { FileName = "dotnet", Arguments = $"build {slnf} --no-incremental -c Debug" };

using Process build =
    Process.Start(buildStartInfo)
    ?? throw new InvalidOperationException("process must not be null");
await build.WaitForExitAsync();

ProcessStartInfo testStartInfo =
    new() { FileName = "dotnet", Arguments = $"test {slnf} --no-build -c Debug" };

using Process test =
    Process.Start(testStartInfo) ?? throw new InvalidOperationException("process must not be null");

await test.WaitForExitAsync();
