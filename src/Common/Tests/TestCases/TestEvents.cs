namespace Common.Tests.TestCases
{
    using System;
    using NUnit.Framework;
    using Types;

    public class TestEvents : TestCase
    {
        public override Type MessageType => typeof(ISampleEvent);

        public override void Populate(object instance)
        {
            var expected = (ISampleEvent)instance;

            expected.Value = "Test Value";
        }

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            var expected = (ISampleEvent)expectedObj;
            var actual = (ISampleEvent)actualObj;

            Assert.AreEqual(expected.Value, actual.Value);
        }
    }
}