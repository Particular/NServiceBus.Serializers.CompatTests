namespace Common.Tests.TestCases
{
    using System;
    using NUnit.Framework;
    using Types;

    public class TestPoco : TestCase
    {
        public override Type MessageType => typeof(Person);

        public override void Populate(object instance)
        {
            var expected = (Person) instance;

            expected.FirstName = "John";
            expected.LastName = "Smith";

            expected.DateTime = expectedDateTime;
            expected.DateTimeUtc = expectedDateTimeUtc;
            expected.DateTimeLocal = expectedDateTimeLocal;
            expected.DateTimeOffset = expectedDateTimeOffset;
            expected.DateTimeOffsetLocal = expectedDateTimeOffsetLocal;
            expected.DateTimeOffsetUtc = expectedDateTimeOffsetUtc;
        }

        public override void CheckIfAreEqual(object instanceA, object instanceB)
        {
            var expected = (Person) instanceA;
            var other = (Person) instanceB;

            Assert.AreEqual(expected.FirstName, other.FirstName);
            Assert.AreEqual(expected.LastName, other.LastName);

            Assert.AreEqual(expected.DateTime, other.DateTime);
            Assert.AreEqual(expected.DateTimeUtc, other.DateTimeUtc);
            Assert.AreEqual(expected.DateTimeLocal, other.DateTimeLocal);

            Assert.AreEqual(expected.DateTimeOffset, other.DateTimeOffset);
            Assert.AreEqual(expected.DateTimeOffset.Offset, other.DateTimeOffset.Offset);
            Assert.AreEqual(expected.DateTimeOffsetLocal, other.DateTimeOffsetLocal);
            Assert.AreEqual(expected.DateTimeOffsetLocal.Offset, other.DateTimeOffsetLocal.Offset);
            Assert.AreEqual(expected.DateTimeOffsetUtc, other.DateTimeOffsetUtc);
            Assert.AreEqual(expected.DateTimeOffsetUtc.Offset, other.DateTimeOffsetUtc.Offset);
        }

        static DateTime expectedDateTime = new DateTime(2010, 10, 13, 12, 32, 42, DateTimeKind.Unspecified);
        static DateTime expectedDateTimeLocal = new DateTime(2010, 10, 13, 12, 32, 42, DateTimeKind.Local);
        static DateTime expectedDateTimeUtc = new DateTime(2010, 10, 13, 12, 32, 42, DateTimeKind.Utc);
        static DateTimeOffset expectedDateTimeOffset = new DateTimeOffset(2012, 12, 12, 12, 12, 12, TimeSpan.FromHours(6));
        static DateTimeOffset expectedDateTimeOffsetLocal = new DateTimeOffset(expectedDateTimeLocal);
        static DateTimeOffset expectedDateTimeOffsetUtc = new DateTimeOffset(expectedDateTimeUtc);
    }
}