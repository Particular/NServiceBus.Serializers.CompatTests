namespace Common.Tests.TestCases
{
    using System;
    using NUnit.Framework;
    using Types;

    public class TestDates : TestCase
    {
        public override bool IsSupported(SerializationFormat format, PackageVersion version)
        {
            if (format != SerializationFormat.Json)
            {
                return true;
            }

            if (version.Major < 5)
            {
                return false;
            }
            if (version.Major == 5)
            {
                if (version.Minor < 2)
                {
                    return false;
                }

                if (version.Major > 2)
                {
                    return true;
                }

                return version.Patch > 20;
            }
            if (version.Major == 6)
            {
                return version.Minor > 0 || version.Patch > 3;
            }
            return true;
        }

        public override Type MessageType => typeof(Dates);

        public override object CreateInstance() => new Dates
        {
            DateTime = expectedDateTime,
            DateTimeUtc = expectedDateTimeUtc,
            DateTimeLocal = expectedDateTimeLocal,
            DateTimeOffset = expectedDateTimeOffset,
            DateTimeOffsetLocal = expectedDateTimeOffsetLocal,
            DateTimeOffsetUtc = expectedDateTimeOffsetUtc
        };

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            var expected = (Dates)expectedObj;
            var actual = (Dates)actualObj;

            Assert.AreEqual(expected.DateTime, actual.DateTime);
            Assert.AreEqual(expected.DateTimeUtc, actual.DateTimeUtc);
            Assert.AreEqual(expected.DateTimeLocal, actual.DateTimeLocal);

            Assert.AreEqual(expected.DateTimeOffset, actual.DateTimeOffset);
            Assert.AreEqual(expected.DateTimeOffset.Offset, actual.DateTimeOffset.Offset);
            Assert.AreEqual(expected.DateTimeOffsetLocal, actual.DateTimeOffsetLocal);
            Assert.AreEqual(expected.DateTimeOffsetLocal.Offset, actual.DateTimeOffsetLocal.Offset);
            Assert.AreEqual(expected.DateTimeOffsetUtc, actual.DateTimeOffsetUtc);
            Assert.AreEqual(expected.DateTimeOffsetUtc.Offset, actual.DateTimeOffsetUtc.Offset);
        }

        static DateTime expectedDateTime = new DateTime(2010, 10, 13, 12, 32, 42, DateTimeKind.Unspecified);
        static DateTime expectedDateTimeLocal = new DateTime(2010, 10, 13, 12, 32, 42, DateTimeKind.Local);
        static DateTime expectedDateTimeUtc = new DateTime(2010, 10, 13, 12, 32, 42, DateTimeKind.Utc);
        static DateTimeOffset expectedDateTimeOffset = new DateTimeOffset(2012, 12, 12, 12, 12, 12, TimeSpan.FromHours(6));
        static DateTimeOffset expectedDateTimeOffsetLocal = new DateTimeOffset(expectedDateTimeLocal);
        static DateTimeOffset expectedDateTimeOffsetUtc = new DateTimeOffset(expectedDateTimeUtc);
    }
}