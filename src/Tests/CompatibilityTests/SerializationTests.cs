namespace Tests.CompatibilityTests
{
    using static SimpleExec.Command;
    using System;
    using System.IO;
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
        public void Serialize(TestDescription testDescription)
        {
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
    }
}