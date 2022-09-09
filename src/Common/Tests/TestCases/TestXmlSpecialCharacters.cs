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

        public override void Populate(object instance)
        {
            var expected = (TestMessageWithChar)instance;

            expected.ValidCharacter = 'a';
            expected.InvalidCharacter = '<';
        }


        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            var expected = (TestMessageWithChar)expectedObj;
            var actual = (TestMessageWithChar)actualObj;

            Assert.AreEqual(expected.ValidCharacter, actual.ValidCharacter, "Valid characters do not match.");
            Assert.AreEqual(expected.InvalidCharacter, actual.InvalidCharacter, "Invalid characters do not match.");
        }
    }
}