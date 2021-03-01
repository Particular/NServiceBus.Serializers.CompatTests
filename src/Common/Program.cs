﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Common;
using Common.Tests;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Base dir:" + AppDomain.CurrentDomain.BaseDirectory);
        Console.WriteLine("Arguments: " + string.Join(Environment.NewLine, args));

        //TODO where should we store the files?
        var testDirectory = Directory.CreateDirectory(Path.Combine(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()), "temp", "serializer-compat-tests"));

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
                var testCaseFolder = Path.Combine(testDirectory.FullName, SerializationFormat.Json.ToString("G"), jsonSupportedTestCase.GetType().Name);

                if (!Directory.Exists(testCaseFolder))
                {
                    Directory.CreateDirectory(testCaseFolder);
                }

                var fileName = Path.Combine(testCaseFolder, Assembly.GetExecutingAssembly().GetName().Name + ".json");

                var serializer = (ISerializerFacade) Activator.CreateInstance(jsonSerializerFacade, jsonSupportedTestCase.MessageType);
                var testInstance = serializer.CreateInstance(jsonSupportedTestCase.MessageType);
                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    serializer.Serialize(stream, testInstance);
                    stream.Flush(true);
                }
            }

            foreach (var xmlSupportedTestCase in xmlSupportedTestCases)
            {
                Console.WriteLine($"XML - {xmlSupportedTestCase.MessageType.Name}");
                var testCaseFolder = Path.Combine(testDirectory.FullName, SerializationFormat.Xml.ToString("G"), xmlSupportedTestCase.GetType().Name);

                if (!Directory.Exists(testCaseFolder))
                {
                    Directory.CreateDirectory(testCaseFolder);
                }

                var fileName = Path.Combine(testCaseFolder, Assembly.GetExecutingAssembly().GetName().Name + ".xml");

                var serializer = (ISerializerFacade) Activator.CreateInstance(xmlSerializerFacade, xmlSupportedTestCase.MessageType);
                var testInstance = serializer.CreateInstance(xmlSupportedTestCase.MessageType);
                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    serializer.Serialize(stream, testInstance);
                    stream.Flush(true);
                }
            }
        }

        if (args.Contains("Deserialize") || args.Length == 0)
        {
            Console.WriteLine("Running Deserialization tests for:");
            foreach (var jsonSupportedTestCase in jsonSupportedTestCases)
            {
                Console.WriteLine($"JSON - {jsonSupportedTestCase.MessageType.Name}");
                var testCaseFolder = Path.Combine(testDirectory.FullName, SerializationFormat.Json.ToString("G"), jsonSupportedTestCase.GetType().Name);

                if (!Directory.Exists(testCaseFolder))
                {
                    Directory.CreateDirectory(testCaseFolder);
                }

                var files = Directory.GetFiles(testCaseFolder);

                var serializer = (ISerializerFacade) Activator.CreateInstance(jsonSerializerFacade, jsonSupportedTestCase.MessageType);
                var expectedValues = serializer.CreateInstance(jsonSupportedTestCase.MessageType);

                foreach (var file in files)
                {
                    using (var stream = new FileStream(file, FileMode.Open))
                    {
                        var deserializedType = serializer.Deserialize(stream).First();

                        jsonSupportedTestCase.CheckIfAreEqual(deserializedType, expectedValues);
                    }
                }
            }

            foreach (var xmlSupportedTestCase in xmlSupportedTestCases)
            {
                Console.WriteLine($"XML - {xmlSupportedTestCase.MessageType.Name}");
                var testCaseFolder = Path.Combine(testDirectory.FullName, SerializationFormat.Xml.ToString("G"), xmlSupportedTestCase.GetType().Name);

                if (!Directory.Exists(testCaseFolder))
                {
                    Directory.CreateDirectory(testCaseFolder);
                }

                var files = Directory.GetFiles(testCaseFolder);

                var serializer = (ISerializerFacade) Activator.CreateInstance(xmlSerializerFacade, xmlSupportedTestCase.MessageType);
                var expectedValues = serializer.CreateInstance(xmlSupportedTestCase.MessageType);

                foreach (var file in files)
                {
                    using (var stream = new FileStream(file, FileMode.Open))
                    {
                        var deserializedType = serializer.Deserialize(stream).First();

                        xmlSupportedTestCase.CheckIfAreEqual(deserializedType, expectedValues);
                    }
                }
            }

        }
    }

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
                yield return (TestCase) Activator.CreateInstance(testType);
            }
        }
    }
}
