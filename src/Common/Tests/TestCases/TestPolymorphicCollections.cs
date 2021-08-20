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

        public override void Populate(object instance)
        {
            var expected = (Polymorphic)instance;

            expected.Items = new List<BaseEntity>
            {
                new SpecializationA
                {
                    Name = "A"
                },
                new SpecializationB
                {
                    Name = "B"
                }
            };
        }

        public override void CheckIfAreEqual(object instanceA, object instanceB)
        {
            var expected = (Polymorphic)instanceA;
            var pc = (Polymorphic)instanceB;

            CollectionAssert.AreEqual(expected.Items, pc.Items);
        }
    }
}