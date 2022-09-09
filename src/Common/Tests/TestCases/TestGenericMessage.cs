namespace Common.Tests.TestCases
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

        public override void Populate(object instance)
        {
            var expected = (GenericMessage<int, string>)instance;

            expected.SagaId = Guid.NewGuid();
            expected.Data1 = 1234;
            expected.Data2 = "Lorem ipsum";
        }

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            var expected = (GenericMessage<int, string>)expectedObj;
            var actual = (GenericMessage<int, string>)actualObj;

            Assert.AreEqual(expected.Data1, actual.Data1, "Generic Data1");
            Assert.AreEqual(expected.Data2, actual.Data2, "Generic Data2");
        }
    }
}