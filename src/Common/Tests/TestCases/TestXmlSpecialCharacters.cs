﻿namespace Common.Tests.TestCases
{
    using System;
    using NUnit.Framework;
    using Types;

    public class TestXmlSpecialCharacters : TestCase
    {
        public override Type MessageType => typeof(TestMessageWithChar);

        public override bool IsSupported(SerializationFormat format, (int Major, int Minor, int Patch) version)
        {
            return !(version.Major != 3 && version.Minor == 3);
        }

        public override void Populate(object instance)
        {
            var expected = (TestMessageWithChar)instance;

            expected.ValidCharacter = 'a';
            expected.InvalidCharacter = '<';
        }


        public override void CheckIfAreEqual(object instanceA, object instanceB)
        {
            var expected = (TestMessageWithChar)instanceA;
            var message = (TestMessageWithChar)instanceB;

            Assert.AreEqual(expected.ValidCharacter, message.ValidCharacter);
            Assert.AreEqual(expected.InvalidCharacter, message.InvalidCharacter);
        }
    }
}