﻿namespace Tests
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;
    using CliWrap;
    using Common;
    using NUnit.Framework;

    [TestFixture, Order(1)]
    public class SerializationTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            Directory.Delete(Settings.TestDirectory.FullName, true);
        }

        [TestCaseSource(typeof(TestCaseGenerator), nameof(TestCaseGenerator.NServiceBusVersions))]
        public async Task Serialize(TestDescription testDescription)
        {
            var extension = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ".exe" : "";
            var execPath = Path.Combine(testDescription.Directory, testDescription.Name + extension);
            var standardOutput = new StringBuilder();
            var errorOutput = new StringBuilder();
            try
            {
                await Cli.Wrap(execPath)
                    .WithArguments("Serialize")
                    .WithStandardOutputPipe(PipeTarget.ToStringBuilder(standardOutput))
                    .WithStandardErrorPipe(PipeTarget.ToStringBuilder(errorOutput))
                    .ExecuteAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to run serialization for version " + testDescription.Name, e);
            }
            finally
            {
                Console.WriteLine(errorOutput.ToString());
                Console.WriteLine(standardOutput.ToString());
            }
        }
    }
}