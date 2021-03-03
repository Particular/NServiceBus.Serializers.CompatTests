namespace Tests.CompatibilityTests
{
    using static SimpleExec.Command;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using NUnit.Framework;

    [TestFixture]
    public class NewTest
    {
#if DEBUG
        const string ConfigurationFolder = "Debug";
#else
        const string ConfigurationFolder = "Release";
#endif

        [TestCaseSource(nameof(NServiceBusVersions)), Order(1)]
        public void Serialize(TestDescription testDescription)
        {
            //TODO: Clean the target directory first.

            var execPath = Path.Combine(testDescription.Directory, testDescription.Name + ".exe");
            try
            {
                //TODO redirecting standard output would be nice
                Console.Write(Read(execPath, args: "Serialize"));
            }
            catch (Exception e)
            {
                TestContext.WriteLine(e);
                throw new Exception("Failed to run serialization for version " + testDescription.Name, e);
            }
        }

        [TestCaseSource(nameof(NServiceBusVersions)), Order(2)]
        public void Deserialize(TestDescription testDescription)
        {
            var execPath = Path.Combine(testDescription.Directory, testDescription.Name + ".exe");
            try
            {
                //TODO redirecting standard output would be nice
                Console.Write(Read(execPath, args: "Deserialize"));
            }
            catch (Exception e)
            {
                TestContext.WriteLine(e);
                throw new Exception("Failed to run deserialization for version " + testDescription.Name, e);
            }
        }

        static IEnumerable<TestDescription> NServiceBusVersions()
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\"));
            var allDirectories = Directory.GetDirectories(projectDirectory);
            var regex = new Regex(@"NServiceBus\d+\.\d+");

            foreach (var directory in allDirectories)
            {
                var match = regex.Match(directory);
                if (match.Success)
                {
                    var versionName = match.Groups[0];

                    var platforms = Directory.GetDirectories(Path.Combine(directory, "bin", ConfigurationFolder))
                        .Where(p => !p.Contains("netcoreapp")); // currently not supported

                    foreach (var platformPath in platforms)
                    {
                        var platformName = platformPath.Split(Path.DirectorySeparatorChar).Last();
                        yield return new TestDescription(versionName.Value, platformPath, platformName);
                    }
                }
            }
        }

        public class TestDescription
        {
            public TestDescription(string versionName, string directory, string platform)
            {
                Name = versionName;
                Directory = directory;
                Platform = platform;
            }

            public string Name { get; }
            public string Directory { get; }
            public string Platform { get; }

            public override string ToString()
            {
                return $"{Name}, {Platform}";
            }
        }
    }
}