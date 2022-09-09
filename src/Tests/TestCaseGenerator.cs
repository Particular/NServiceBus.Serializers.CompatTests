namespace Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
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
            var allDirectories = Directory.GetDirectories(Constants.ProjectDirectory);
            var regex = new Regex(@"NServiceBus(?<major>\d+)\.\d+");

            foreach (var directory in allDirectories)
            {
                var match = regex.Match(directory);
                if (match.Success)
                {
                    var major = int.Parse(match.Groups["major"].Value);

                    var versionName = match.Groups[0];

                    var platforms = Directory.GetDirectories(Path.Combine(directory, "bin", ConfigurationFolder));

                    foreach (var platformPath in platforms)
                    {
                        var platformName = platformPath.Split(Path.DirectorySeparatorChar).Last();
                        if (major >= 8 && platformName.StartsWith("netcoreapp"))
                        {
                            continue; // NServiceBus 8 no longer targets netcore 3.1
                        }
                        if (platformName.StartsWith("net4") && !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        {
                            continue;
                        }
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