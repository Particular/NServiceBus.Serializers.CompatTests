namespace Common.Tests.TestCases
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using Types;

    public class TestPolymorphics : TestCase
    {
        public override Type MessageType => typeof(Polymorphic);

        public override bool IsSupported(SerializationFormat format, PackageVersion version)
        {
            return format == SerializationFormat.Json;
        }

        public override object CreateInstance(ISerializerFacade serializer) =>
            new Polymorphic
            {
                Items = new List<BaseEntity>
                {
                    new SpecializationA
                    {
                        Name = "A"
                    },
                    new SpecializationB
                    {
                        Name = "B"
                    }
                }
            };

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            var expected = (Polymorphic)actualObj;
            var actual = (Polymorphic)expectedObj;

            CollectionAssert.AreEqual(expected.Items, actual.Items, "Polymorphic object graph does not match.");
        }
    }
}