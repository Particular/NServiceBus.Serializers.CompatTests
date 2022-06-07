namespace Tests
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;
    using CliWrap;
    using NUnit.Framework;

    [TestFixture, Order(2)]
    public class DeserializationTests
    {
        [TestCaseSource(typeof(TestCaseGenerator), nameof(TestCaseGenerator.NServiceBusVersions))]
        public async Task Deserialize(TestDescription testDescription)
        {
            var extension = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ".exe" : ".dll";
            var execPath = Path.Combine(testDescription.Directory, testDescription.Name + extension);
            var standardOutput = new StringBuilder();
            var errorOutput = new StringBuilder();
            try
            {
                await Cli.Wrap(execPath)
                    .WithArguments("Deserialize")
                    .WithStandardOutputPipe(PipeTarget.ToStringBuilder(standardOutput))
                    .WithStandardErrorPipe(PipeTarget.ToStringBuilder(errorOutput))
                    .ExecuteAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to run deserialization for version " + testDescription.Name, e);
            }
            finally
            {
                Console.WriteLine(errorOutput.ToString());
                Console.WriteLine(standardOutput.ToString());
            }
        }
    }
}