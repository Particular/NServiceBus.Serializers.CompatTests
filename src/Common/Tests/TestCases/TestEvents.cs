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
            // This test requires the message mapper to create instances of the message type as old serializers can't handle arbitrary types implementing the interface.
            var impl = (ISampleEvent)serializer.CreateInstance(typeof(ISampleEvent));
            impl.Value = "TestValue";
            return impl;
        }

        public override object CreateInstance() => throw new NotImplementedException();

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            var expected = (ISampleEvent)expectedObj;
            var actual = (ISampleEvent)actualObj;

            Assert.That(actual.Value, Is.EqualTo(expected.Value));
        }
    }
}