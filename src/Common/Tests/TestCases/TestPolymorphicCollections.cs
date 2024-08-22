﻿namespace Common.Tests.TestCases
{
    using System;
    using NUnit.Framework;
    using Types;

    public class TestPolymorphics : TestCase
    {
        public override Type MessageType => typeof(Polymorphic);

        public override bool IsSupported(SerializationFormat format, PackageVersion version)
        {
            return format == SerializationFormat.Json;
        }

        public override object CreateInstance() =>
            new Polymorphic
            {
                Items =
                [
                    new SpecializationA
                    {
                        Name = "A"
                    },
                    new SpecializationB
                    {
                        Name = "B"
                    }
                ]
            };

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            var expected = (Polymorphic)actualObj;
            var actual = (Polymorphic)expectedObj;

            Assert.That(actual.Items, Is.EqualTo(expected.Items).AsCollection, "Polymorphic object graph does not match.");
        }
    }
}