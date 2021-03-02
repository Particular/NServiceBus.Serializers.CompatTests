using System;
using System.Collections.Generic;
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

    //TODO where should we store the files?
    static readonly DirectoryInfo TestDirectory = Directory.CreateDirectory(Path.Combine(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()), "temp", "serializer-compat-tests"));

    static void Main(string[] args)
    {
        Console.WriteLine("Arguments: " + string.Join(Environment.NewLine, args));

        //TODO determine correct assembly full name to load type
        var jsonSerializerFacade = Type.GetType("JsonSerializerFacade", true);
        var xmlSerializerFacade = Type.GetType("XmlSerializerFacade", true);

        var jsonSupportedTestCases = GetTestCasesMatchingCurrentVersion(SerializationFormat.Json);
        var xmlSupportedTestCases = GetTestCasesMatchingCurrentVersion(SerializationFormat.Xml);

        if (args.Contains("Serialize") || args.Length == 0)
        {
            Console.WriteLine("Running Serialization tests for:");
            foreach (var jsonSupportedTestCase in jsonSupportedTestCases)
            {
                Console.WriteLine($"JSON - {jsonSupportedTestCase.MessageType.Name}");
                Serialize(jsonSerializerFacade, jsonSupportedTestCase);
            }

            foreach (var xmlSupportedTestCase in xmlSupportedTestCases)
            {
                Console.WriteLine($"XML - {xmlSupportedTestCase.MessageType.Name}");
                Serialize(xmlSerializerFacade, xmlSupportedTestCase);
            }
        }

        if (args.Contains("Deserialize") || args.Length == 0)
        {
            Console.WriteLine("Running Deserialization tests for:");
            foreach (var jsonSupportedTestCase in jsonSupportedTestCases)
            {
                Console.WriteLine($"JSON - {jsonSupportedTestCase.MessageType.Name}");

                DeserializeAndVerify(jsonSerializerFacade, jsonSupportedTestCase);
            }

            foreach (var xmlSupportedTestCase in xmlSupportedTestCases)
            {
                Console.WriteLine($"XML - {xmlSupportedTestCase.MessageType.Name}");
                DeserializeAndVerify(xmlSerializerFacade, xmlSupportedTestCase);
            }
        }
    }

    static void DeserializeAndVerify(Type serializerType, TestCase testCase)
    {
        var serializer = (ISerializerFacade)Activator.CreateInstance(serializerType, testCase.MessageType);
        var expectedValues = serializer.CreateInstance(testCase.MessageType);

        var testCaseFolder = GetTestCaseFolder(testCase, serializer.serializationFormat);
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
        var testCaseFolder = Path.Combine(TestDirectory.FullName, serializationFormat.ToString("G"), testCase.GetType().Name);

        if (!Directory.Exists(testCaseFolder))
        {
            Directory.CreateDirectory(testCaseFolder);
        }

        return testCaseFolder;
    }

    static void Serialize(Type serializerType, TestCase testCase)
    {
        var serializer = (ISerializerFacade)Activator.CreateInstance(serializerType, testCase.MessageType);
        var testInstance = serializer.CreateInstance(testCase.MessageType);

        var testCaseFolder = GetTestCaseFolder(testCase, serializer.serializationFormat);
        var fileName = GetFileName(testCaseFolder, serializer.serializationFormat.ToString("G").ToLower());

        using (var stream = new FileStream(fileName, FileMode.Create))
        {
            serializer.Serialize(stream, testInstance);
            stream.Flush(true);
        }
    }

    static string GetFileName(string testCaseFolder, string fileExtension) => Path.Combine(testCaseFolder, $"{Assembly.GetExecutingAssembly().GetName().Name} {TargetFrameworkAttribute.FrameworkDisplayName}.{fileExtension}");

    static IEnumerable<TestCase> GetTestCasesMatchingCurrentVersion(SerializationFormat serializationFormat)
    {
        var nsbVersion = FileVersionInfo.GetVersionInfo(Assembly.Load(Assembly.GetCallingAssembly().GetReferencedAssemblies().Single(x => x.Name.Equals("NServiceBus.Core"))).Location);
        var testCaseType = typeof(TestCase);
        var testTypes=  testCaseType.Assembly.GetTypes().Where(p => testCaseType.IsAssignableFrom(p) && testCaseType != p);
        foreach (var testType in testTypes)
        {
            var testCase = (TestCase) Activator.CreateInstance(testType);

            var version = new ValueTuple<int, int, int>(nsbVersion.FileMajorPart, nsbVersion.FileMinorPart, nsbVersion.FileBuildPart);
            if (testCase.IsSupported(serializationFormat, version))
            {
                yield return testCase;
            }
        }
    }
}
