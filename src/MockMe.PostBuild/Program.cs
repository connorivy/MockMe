// See https://aka.ms/new-console-template for more information
using System.Reflection;

Console.WriteLine("Hello, World!");

var assemblyPath = Path.Combine(
    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
    "C:\\Users\\conno\\Documents\\GitHub\\MockMe\\test\\MockMeTests\\bin\\Debug\\net6.0\\MockMe.Tests.dll"
);

var assembly = Assembly.LoadFrom(assemblyPath);
var mockType = assembly.GetTypes().Where(t => t.Namespace == "MockMe" && t.Name == "Mock").First();

var methods = mockType.GetMethods();

List<(Type, string)> genericTypes =
    (List<(Type, string)>)
        mockType
            .GetMethod("GenericTypes", BindingFlags.Public | BindingFlags.Static)
            .Invoke(null, null);

if (genericTypes.Count == 0)
{
    System.Environment.Exit(0);
}

//using var
//Mono.Cecil.TypeDefinition typeDef =
;
