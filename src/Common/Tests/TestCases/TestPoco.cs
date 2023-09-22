namespace Common.Tests.TestCases
{
    using System;
    using NUnit.Framework;
    using Types;

    public class TestPoco : TestCase
    {
        public override Type MessageType => typeof(Person);

        public override object CreateInstance() => new Person
        {
            FirstName = "John",
            LastName = "Smith"
        };

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            var expected = (Person)expectedObj;
            var actual = (Person)actualObj;

            Assert.AreEqual(expected.FirstName, actual.FirstName, "FirstName does not match");
            Assert.AreEqual(expected.LastName, actual.LastName, "LastName does not match");
        }
    }
}