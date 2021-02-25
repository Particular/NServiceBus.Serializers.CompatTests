namespace Tests.CompatibilityTests
{
    using static SimpleExec.Command;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    public class NewTest
    {
#if DEBUG
        const string ConfigurationFolder = "Debug";
#else
        const string ConfigurationFolder = "Release";
#endif

        [Test]
        public void Serialize()
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\"));
            //TODO: Discover all test versions
            var nserviceBusVersions = new[]
            {
                "NServiceBus6.1",
                "NServiceBus6.2"
            };

            foreach (var version in nserviceBusVersions)
            {
                //TODO: we need to discover the available target platforms for each project somehow
                var workingDir = Path.Combine(projectDirectory, version, "bin", ConfigurationFolder, "net452");
                var execPath = Path.Combine(workingDir, version + ".exe");
                try
                {
                    //TODO Running just the executable from the correct working directory doesn't seem to work
                    //Run(version + ".exe", args: "Serialize", workingDirectory: workingDir);
                    //TODO redirecting standard output would be nice
                    Run(execPath, args: "Serialize");
                }
                catch (Exception e)
                {
                    TestContext.WriteLine(e);
                    throw new Exception("Failed to run serialization for version " + version, e);
                }

            }
        }

        static string[] NserviceBusVersions = new[]
        {
            "NServiceBus6.1",
            "NServiceBus6.2"
        };

        [TestCaseSource(nameof(NserviceBusVersions))]
        public void Deserialize(string version)
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\"));
            //TODO: Discover all test versions

            //TODO: we need to discover the available target platforms for each project somehow
            var workingDir = Path.Combine(projectDirectory, version, "bin", ConfigurationFolder, "net452");
            var execPath = Path.Combine(workingDir, version + ".exe");
            try
            {
                //TODO Running just the executable from the correct working directory doesn't seem to work
                //Run(version + ".exe", args: "Serialize", workingDirectory: workingDir);
                //TODO redirecting standard output would be nice
                Console.Write(Read(execPath, args: "Deserialize"));
            }
            catch (Exception e)
            {
                TestContext.WriteLine(e);
                throw new Exception("Failed to run deserialization for version " + version, e);
            }
        }


    }
}