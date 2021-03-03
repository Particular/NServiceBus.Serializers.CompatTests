namespace Tests.CompatibilityTests
{
    using static SimpleExec.Command;
    using System;
    using System.IO;
    using NUnit.Framework;

    [TestFixture, Order(2)]
    public class DeserializationTests
    {
        [TestCaseSource(typeof(TestCaseGenerator), nameof(TestCaseGenerator.NServiceBusVersions))]
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
    }
}