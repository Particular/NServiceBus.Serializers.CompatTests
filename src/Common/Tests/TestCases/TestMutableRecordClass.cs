namespace Common.Tests.TestCases
{
    using System;
    using NUnit.Framework;
    using Types;

    public class TestMutableRecordClass : TestCase
    {
        public override Type MessageType => typeof(MutableRecordClass);

        public override object CreateInstance() =>
            new MutableRecordClass()
            {
                IntProperty = 42,
                StringProperty = "Hello World!"
            };

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            Assert.That(expectedObj, Is.InstanceOf<MutableRecordClass>());
            Assert.That(actualObj, Is.InstanceOf<MutableRecordClass>());
            Assert.That(actualObj, Is.EqualTo(expectedObj), "record types should implement value equality by default");
        }
    }
}