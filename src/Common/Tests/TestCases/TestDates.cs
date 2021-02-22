namespace Common.Tests.TestCases
{
    using System;
    using NuGet.Versioning;
    using NUnit.Framework;
    using Types;

    public class TestDates : TestCase
    {
        public override bool IsSupported(SerializationFormat format, string version)
        {
            if (format != SerializationFormat.Json)
            {
                return true;
            }

            var current = SemanticVersion.Parse(version);

            if (current.Major < 5)
            {
                return false;
            }
            if (current.Major == 5)
            {
                if (current.Minor < 2)
                {
                    return false;
                }

                if (current.Major > 2)
                {
                    return true;
                }

                return current.Patch > 20;
            }
            if (current.Major == 6)
            {
                return current.Minor > 0 || current.Patch > 3;
            }
            return true;
        }

        public override Type MessageType => typeof(Dates);

        public override void Populate(object instance)
        {
            var expected = (Dates)instance;

            expected.DateTime = expectedDateTime;
            expected.DateTimeUtc = expectedDateTimeUtc;
            expected.DateTimeLocal = expectedDateTimeLocal;
            expected.DateTimeOffset = expectedDateTimeOffset;
            expected.DateTimeOffsetLocal = expectedDateTimeOffsetLocal;
            expected.DateTimeOffsetUtc = expectedDateTimeOffsetUtc;
        }

        public override void CheckIfAreEqual(object instanceA, object instanceB)
        {
            var expected = (Dates)instanceA;
            var other = (Dates)instanceB;

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