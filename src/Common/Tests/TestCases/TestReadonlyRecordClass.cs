#if NET6_0_OR_GREATER
namespace Common.Tests.TestCases
{
    using System;
    using NUnit.Framework;
    using Types;

    public class TestReadonlyRecordClass : TestCase
    {
        public override Type MessageType => typeof(ReadonlyRecordClass);

        public override object CreateInstance() =>
            new ReadonlyRecordClass(42, "Hello World!");

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            Assert.IsInstanceOf<ReadonlyRecordClass>(expectedObj);
            Assert.IsInstanceOf<ReadonlyRecordClass>(actualObj);
            Assert.AreEqual(expectedObj, actualObj, "record types should implement value equality by default");
        }
    }
}
#endif