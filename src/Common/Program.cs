using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Common;
using Common.Tests;
using Common.Tests.TestCases;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
        Console.WriteLine("Base dir:" + AppDomain.CurrentDomain.BaseDirectory);
        Console.WriteLine("Arguments: " + string.Join(Environment.NewLine, args));

        //TODO where should we store the files?
        var testDirectory = Directory.CreateDirectory(Path.Combine(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()), "temp", "serializer-compat-tests"));

        //TODO determine correct assembly full name to load type
        var jsonSerializerFacade = Type.GetType("JsonSerializerFacade", true);

        //TODO check if test type is supported on current version
        //TODO for each test type
        TestCase testCase = new TestDates();

        var testCaseFolder = Path.Combine(testDirectory.FullName, SerializationFormat.Json.ToString("G"), testCase.GetType().Name);
        //TODO check if exists and cache result
        Directory.CreateDirectory(testCaseFolder);

        if (args.Contains("Serialize") || args.Length == 0)
        {
            //TODO for both XML and JSON


            var fileName = Path.Combine(testCaseFolder, Assembly.GetExecutingAssembly().GetName().Name + ".json");
            

            var serializer = (ISerializerFacade)Activator.CreateInstance(jsonSerializerFacade, testCase.MessageType);
            var testInstance = serializer.CreateInstance(testCase.MessageType);
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(stream, testInstance);
                stream.Flush(true);
            }
        }

        if (args.Contains("Deserialize") || args.Length == 0)
        {
            var files = Directory.GetFiles(testCaseFolder);

            var serializer = (ISerializerFacade)Activator.CreateInstance(jsonSerializerFacade, testCase.MessageType);
            var expectedValues = serializer.CreateInstance(testCase.MessageType);

            foreach (var file in files)
            {
                using (var stream = new FileStream(file, FileMode.Open))
                {
                    var deserializedType = serializer.Deserialize(stream).First();

                    testCase.CheckIfAreEqual(deserializedType, expectedValues);
                }
            }

        }
    }
}
