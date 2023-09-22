namespace Common.Tests.TestCases
{
    using System;
    using NUnit.Framework;
    using Types;

    public class TestEvents : TestCase
    {
        public override Type MessageType => typeof(ISampleEvent);

        public override object CreateInstance(ISerializerFacade serializer)
        {
            var impl = (ISampleEvent)serializer.CreateInstance(typeof(ISampleEvent));
            impl.Value = "TestValue";
            return impl;
        }

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            var expected = (ISampleEvent)expectedObj;
            var actual = (ISampleEvent)actualObj;

            Assert.AreEqual(expected.Value, actual.Value);
        }
    }
}