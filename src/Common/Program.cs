using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using Common;
using Common.Tests;

class Program
{
    static readonly TargetFrameworkAttribute TargetFrameworkAttribute = (TargetFrameworkAttribute)Assembly.GetExecutingAssembly()
        .GetCustomAttributes(typeof(TargetFrameworkAttribute), false)
        .SingleOrDefault();

    static readonly FileVersionInfo NsbVersion = FileVersionInfo.GetVersionInfo(Assembly.Load(Assembly.GetCallingAssembly().GetReferencedAssemblies().Single(x => x.Name.Equals("NServiceBus.Core"))).Location);

    static void Main(string[] args)
    {
        Console.WriteLine($"Running NServiceBus {NsbVersion.FileMajorPart}.{NsbVersion.FileMinorPart}.{NsbVersion.FileBuildPart}");
        Console.WriteLine("Arguments: " + string.Join(Environment.NewLine, args));

        var serializers = new[]
        {
            Type.GetType("JsonSerializerFacade", true),
            Type.GetType("XmlSerializerFacade", true)
        };
        var testCases = DiscoverTestCases();

        if (args.Contains("Serialize") || args.Length == 0)
        {
            Console.WriteLine("Running Serialization tests for:");
            foreach (var serializerType in serializers)
            {
                foreach (var testCase in testCases)
                {
                    var serializer = (ISerializerFacade)Activator.CreateInstance(serializerType, testCase.MessageType);
                    if (testCase.IsSupported(serializer.SerializationFormat, new ValueTuple<int, int, int>(NsbVersion.FileMajorPart, NsbVersion.FileMinorPart, NsbVersion.FileBuildPart)))
                    {
                        Console.WriteLine($"{serializer.SerializationFormat:G} — {testCase.GetType().Name}");
                        Serialize(serializer, testCase);
                    }
                }
            }
        }

        if (args.Contains("Deserialize") || args.Length == 0)
        {
            Console.WriteLine("Running Deserialization tests for:");
            foreach (var serializerType in serializers)
            {
                foreach (var testCase in testCases)
                {
                    var serializer = (ISerializerFacade)Activator.CreateInstance(serializerType, testCase.MessageType);
                    if (testCase.IsSupported(serializer.SerializationFormat, new ValueTuple<int, int, int>(NsbVersion.FileMajorPart, NsbVersion.FileMinorPart, NsbVersion.FileBuildPart)))
                    {
                        Console.WriteLine($"{serializer.SerializationFormat:G} — {testCase.GetType().Name}");
                        DeserializeAndVerify(serializer, testCase);
                    }
                }
            }
        }
    }

    static void DeserializeAndVerify(ISerializerFacade serializer, TestCase testCase)
    {
        var expectedValues = serializer.CreateInstance(testCase.MessageType);
        testCase.Populate(expectedValues);

        var testCaseFolder = GetTestCaseFolder(testCase, serializer.SerializationFormat);
        var files = Directory.GetFiles(testCaseFolder);
        foreach (var file in files)
        {
            using (var stream = new FileStream(file, FileMode.Open))
            {
                var deserializedType = serializer.Deserialize(stream).First();

                testCase.CheckIfAreEqual(deserializedType, expectedValues);
            }
        }
    }

    static string GetTestCaseFolder(TestCase testCase, SerializationFormat serializationFormat)
    {
        var testCaseFolder = Path.Combine(Settings.TestDirectory.FullName, serializationFormat.ToString("G"), testCase.GetType().Name);

        if (!Directory.Exists(testCaseFolder))
        {
            Directory.CreateDirectory(testCaseFolder);
        }

        return testCaseFolder;
    }

    static void Serialize(ISerializerFacade serializer, TestCase testCase)
    {
        var testInstance = serializer.CreateInstance(testCase.MessageType);
        testCase.Populate(testInstance);

        var testCaseFolder = GetTestCaseFolder(testCase, serializer.SerializationFormat);
        var fileName = GetFileName(testCaseFolder, serializer.SerializationFormat.ToString("G").ToLower());

        using (var stream = new FileStream(fileName, FileMode.Create))
        {
            serializer.Serialize(stream, testInstance);
            stream.Flush(true);
        }
    }

    static string GetFileName(string testCaseFolder, string fileExtension) => Path.Combine(testCaseFolder, $"{Assembly.GetExecutingAssembly().GetName().Name} {TargetFrameworkAttribute.FrameworkDisplayName}.{fileExtension}");

    static TestCase[] DiscoverTestCases()
    {
        var testCaseType = typeof(TestCase);
        var testTypes = testCaseType.Assembly.GetTypes()
            .Where(p => p.IsSubclassOf(testCaseType))
            .Select(p => (TestCase)Activator.CreateInstance(p));
        return testTypes.ToArray();
    }
}
