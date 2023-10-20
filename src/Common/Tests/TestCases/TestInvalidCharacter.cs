namespace Common.Tests.TestCases
{
    using System;
    using System.Text;
    using NUnit.Framework;
    using Types;

    public class TestInvalidCharacter : TestCase
    {
        public override Type MessageType => typeof(MessageWithInvalidCharacter);

        public override bool IsSupported(SerializationFormat format, PackageVersion version) => version.Major != 3;

        public override object CreateInstance()
        {
            var expected = new MessageWithInvalidCharacter();

            var sb = new StringBuilder();
            sb.Append("Hello");
            sb.Append((char)0x1C);
            sb.Append("John");

            expected.Special = sb.ToString();

            return expected;
        }

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            var expected = (MessageWithInvalidCharacter)expectedObj;
            var actual = (MessageWithInvalidCharacter)actualObj;

            Assert.AreEqual(expected.Special, actual.Special);
        }
    }
}