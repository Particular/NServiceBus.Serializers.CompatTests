namespace Common.Tests.TestCases
{
    using System;
    using NUnit.Framework;
    using Types;

    public class TestXmlSpecialCharacters : TestCase
    {
        public override Type MessageType => typeof(TestMessageWithChar);

        public override bool IsSupported(SerializationFormat format, PackageVersion version)
        {
            return version.Major != 3;
        }

        public override object CreateInstance() =>
            new TestMessageWithChar
            {
                ValidCharacter = 'a',
                InvalidCharacter = '<'
            };

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            var expected = (TestMessageWithChar)expectedObj;
            var actual = (TestMessageWithChar)actualObj;

            Assert.Multiple(() =>
            {
                Assert.That(actual.ValidCharacter, Is.EqualTo(expected.ValidCharacter), "Valid characters do not match.");
                Assert.That(actual.InvalidCharacter, Is.EqualTo(expected.InvalidCharacter), "Invalid characters do not match.");
            });
        }
    }
}