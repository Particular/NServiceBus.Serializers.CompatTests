namespace Tests.CompatibilityTests
{
    using System;
    using System.IO;
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
            var execPath = Path.Combine(testDescription.Directory, testDescription.Name + ".exe");
            var standardOutput = new StringBuilder();
            var errorOutput = new StringBuilder();
            try
            {
                await Cli.Wrap(execPath).WithArguments("Deserialize").WithStandardOutputPipe(PipeTarget.ToStringBuilder(standardOutput)).WithStandardErrorPipe(PipeTarget.ToStringBuilder(errorOutput)).ExecuteAsync();
                Console.WriteLine(standardOutput.ToString());
            }
            catch (Exception e)
            {
                TestContext.WriteLine(e);
                Console.WriteLine(errorOutput.ToString());
                throw new Exception("Failed to run deserialization for version " + testDescription.Name, e);
            }
        }
    }
}