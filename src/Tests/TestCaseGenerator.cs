namespace Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class TestCaseGenerator
    {
#if DEBUG
        const string ConfigurationFolder = "Debug";
#else
        const string ConfigurationFolder = "Release";
#endif

        public static IEnumerable<TestDescription> NServiceBusVersions()
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