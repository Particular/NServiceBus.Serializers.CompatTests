﻿namespace Common.Tests.TestCases
{
    using System;
    using NUnit.Framework;
    using Types;

    public class TestGenericMessage : TestCase
    {
        public override Type MessageType => typeof(GenericMessage<int, string>);

        public override bool IsSupported(SerializationFormat format, PackageVersion version)
        {
            if (format == SerializationFormat.Xml)
            {
                return false;
            }

            return base.IsSupported(format, version);
        }

        public override object CreateInstance() =>
            new GenericMessage<int, string>
            {
                SagaId = Guid.NewGuid(),
                Data1 = 1234,
                Data2 = "Lorem ipsum"
            };

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            var expected = (GenericMessage<int, string>)expectedObj;
            var actual = (GenericMessage<int, string>)actualObj;

            Assert.Multiple(() =>
            {
                Assert.That(actual.Data1, Is.EqualTo(expected.Data1), "Generic Data1");
                Assert.That(actual.Data2, Is.EqualTo(expected.Data2), "Generic Data2");
            });
        }
    }
}