namespace Common.Tests.TestCases
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using NUnit.Framework;
    using Types;

    public class TestDictionaries : TestCase
    {
        public override Type MessageType => typeof(MessageWithDictionaries);

        public override bool IsSupported(SerializationFormat format, PackageVersion version)
        {
            return version.Major > 3 || (version.Major == 3 && version.Minor == 3);
        }

        public override object CreateInstance()
        {
            var dictionary = new MessageWithDictionaries
            {
                Bools = new Dictionary<bool, bool>
                {
                    { true, true },
                    { false, false }
                },
                Chars = new Dictionary<char, char>
                {
                    //{char.MinValue, char.MaxValue}, // doesn't work because we use UTF8
                    { 'a', 'b' },
                    { 'c', 'd' },
                    { 'e', 'f' }
                },
                Bytes = new Dictionary<byte, byte>
                {
                    { byte.MinValue, byte.MaxValue },
                    { 11, 1 },
                    { 1, 0 }
                },
                Ints = new Dictionary<int, int>
                {
                    { int.MinValue, int.MaxValue },
                    { 1, 2 },
                    { 3, 4 },
                    { 5, 6 }
                },
                Decimals = new Dictionary<decimal, decimal>
                {
                    { decimal.MinValue, decimal.MaxValue },
                    { .2m, 4m },
                    { .5m, .4234m }
                },
                Doubles = new Dictionary<double, double>
                {
                    //HINT: double.MinValue and double.MaxValue stored as keys in dictionary cannot be properly serialized by Json.Net
                    { 0, double.MinValue },
                    { 1, double.MaxValue },
                    { .223d, 234d },
                    { .513d, .4212334d }
                },
                Floats = new Dictionary<float, float>
                {
                    //HINT: float.MinValue and float.MaxValue stored as keys in dictioanry cannot be properly serialized by Json.Net
                    { 0, float.MaxValue },
                    { 1, float.MinValue },
                    { .223f, 234f },
                    { .513f, .4212334f }
                },
                Enums = new Dictionary<DateTimeStyles, DateTimeKind>
                {
                    { DateTimeStyles.AdjustToUniversal, DateTimeKind.Local },
                    { DateTimeStyles.AllowLeadingWhite, DateTimeKind.Unspecified }
                },
                Longs = new Dictionary<long, long>
                {
                    { long.MaxValue, long.MinValue },
                    { 34234, 234324 },
                    { 45345345, 34534534565 }
                },
                SBytes = new Dictionary<sbyte, sbyte>
                {
                    { sbyte.MaxValue, sbyte.MaxValue },
                    { 56, 13 }
                },
                Shorts = new Dictionary<short, short>
                {
                    { short.MinValue, short.MaxValue },
                    { 5231, 6123 }
                },
                Strings = new Dictionary<string, string>
                {
                    { "Key1", "Value1" },
                    { "Key2", "Value2" },
                    { "Key3", "Value3" }
                },
                UInts = new Dictionary<uint, uint>
                {
                    { uint.MinValue, 23 },
                    { uint.MaxValue, 34324 }
                },
                ULongs = new Dictionary<ulong, ulong>
                {
                    //HINT: unsigned ulong cannot be properly deserialized by Json.Net
                    //      see: http://stackoverflow.com/questions/9355091/json-net-crashes-when-serializing-unsigned-integer-ulong-array
                    //      looks like JSON format constraint
                    //{ulong.MinValue, ulong.MaxValue},
                    { 34324234, 3243243245 }
                },
                UShorts = new Dictionary<ushort, ushort>
                {
                    { ushort.MinValue, ushort.MaxValue },
                    { 42324, 32 }
                }
            };

            return dictionary;
        }

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            var expected = (MessageWithDictionaries)expectedObj;
            var actual = (MessageWithDictionaries)actualObj;

            Assert.Multiple(() =>
            {
                Assert.That(actual.Bools, Is.EqualTo(expected.Bools).AsCollection);
                Assert.That(actual.Chars, Is.EqualTo(expected.Chars).AsCollection);
                Assert.That(actual.Bytes, Is.EqualTo(expected.Bytes).AsCollection);
                Assert.That(actual.Ints, Is.EqualTo(expected.Ints).AsCollection);
                Assert.That(actual.Decimals, Is.EqualTo(expected.Decimals).AsCollection);
                Assert.That(actual.Doubles, Is.EqualTo(expected.Doubles).AsCollection);
                Assert.That(actual.Floats, Is.EqualTo(expected.Floats).AsCollection);
                Assert.That(actual.Enums, Is.EqualTo(expected.Enums).AsCollection);
                Assert.That(actual.Longs, Is.EqualTo(expected.Longs).AsCollection);
                Assert.That(actual.SBytes, Is.EqualTo(expected.SBytes).AsCollection);
                Assert.That(actual.Shorts, Is.EqualTo(expected.Shorts).AsCollection);
                Assert.That(actual.Strings, Is.EqualTo(expected.Strings).AsCollection);
                Assert.That(actual.UInts, Is.EqualTo(expected.UInts).AsCollection);
                Assert.That(actual.ULongs, Is.EqualTo(expected.ULongs).AsCollection);
                Assert.That(actual.UShorts, Is.EqualTo(expected.UShorts).AsCollection);
            });
        }
    }
}