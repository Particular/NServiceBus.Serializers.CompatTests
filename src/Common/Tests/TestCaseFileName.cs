namespace Common.Tests
{
    public class TestCaseFileName
    {
        public TestCaseFileName(string nsbVersion, string testCaseName, SerializationFormat format)
        {
            this.NsbVersion = nsbVersion;

            TestCaseName = testCaseName;
            Format = format;
        }

        string NsbVersion { get; }
        public string TestCaseName { get; }

        public SerializationFormat Format { get; }

        public override string ToString()
        {
            return $"NServiceBus {NsbVersion}_{TestCaseName}_.{Format}";
        }
    }
}