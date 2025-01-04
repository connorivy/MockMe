// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Reflection;

Console.WriteLine("Hello, World!");

var testsFolderPath = Path.Combine(
    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        ?? throw new InvalidOperationException("path must not be null"),
    "..",
    "..",
    "..",
    ".."
);

var slnf = Path.Combine(testsFolderPath, "MockMe.Tests.slnf");

Thread.Sleep(3000);

//ProcessStartInfo cleanStartInfo = new() { FileName = "dotnet", Arguments = $"clean {slnf}" };

//using Process clean =
//    Process.Start(cleanStartInfo)
//    ?? throw new InvalidOperationException("process must not be null");
//await clean.WaitForExitAsync();

ProcessStartInfo cleanStartInfo =
    new()
    {
        FileName = "dotnet",
        Arguments = $"build {Path.Combine(testsFolderPath, "..\\", "src", "MockMe")} -c Debug",
    };

using Process clean =
    Process.Start(cleanStartInfo)
    ?? throw new InvalidOperationException("process must not be null");
await clean.WaitForExitAsync();

var generatorBinPath = Path.Combine(
    testsFolderPath,
    "..",
    "src",
    "MockMe.Generator",
    "bin",
    "Debug",
    "netstandard2.0"
);
foreach (var x in Directory.GetFiles(generatorBinPath))
{
    Console.WriteLine(x);
}

if (IsCiBuild())
{
    Assembly.LoadFrom(Path.Combine(generatorBinPath, "MockMe.Generator.dll"));
}

ProcessStartInfo buildStartInfo =
    new() { FileName = "dotnet", Arguments = $"build --no-incremental -c Debug" };

using Process build =
    Process.Start(buildStartInfo)
    ?? throw new InvalidOperationException("process must not be null");
await build.WaitForExitAsync();

ProcessStartInfo testStartInfo =
    new() { FileName = "dotnet", Arguments = $"test --no-build -c Debug" };

using Process test =
    Process.Start(testStartInfo) ?? throw new InvalidOperationException("process must not be null");

await test.WaitForExitAsync();

static bool IsCiBuild() =>
    bool.TryParse(
        Environment.GetEnvironmentVariable("ContinuousIntegrationBuild"),
        out bool isCiBuild
    ) && isCiBuild;
