namespace Common.Tests.TestCases
{
    using System;
    using NUnit.Framework;
    using Types;

    public class TestEvents : TestCase
    {
        public override Type MessageType => typeof(ISampleEvent);

        public override object CreateInstance() => new SampleEventImpl { Value = "TestValue" };

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            var expected = (ISampleEvent)expectedObj;
            var actual = (ISampleEvent)actualObj;

            Assert.AreEqual(expected.Value, actual.Value);
        }

        class SampleEventImpl : ISampleEvent
        {
            public string Value { get; set; }
        }
    }
}