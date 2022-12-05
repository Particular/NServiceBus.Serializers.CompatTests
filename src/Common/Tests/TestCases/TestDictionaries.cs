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

        public override void Populate(object instance)
        {
            var dictionary = (MessageWithDictionaries)instance;

            dictionary.Bools = new Dictionary<bool, bool>
            {
                { true, true },
                { false, false }
            };
            dictionary.Chars = new Dictionary<char, char>
            {
                //{char.MinValue, char.MaxValue}, // doesn't work because we use UTF8
                { 'a', 'b' },
                { 'c', 'd' },
                { 'e', 'f' }
            };
            dictionary.Bytes = new Dictionary<byte, byte>
            {
                { byte.MinValue, byte.MaxValue },
                { 11, 1 },
                { 1, 0 }
            };
            dictionary.Ints = new Dictionary<int, int>
            {
                { int.MinValue, int.MaxValue },
                { 1, 2 },
                { 3, 4 },
                { 5, 6 }
            };
            dictionary.Decimals = new Dictionary<decimal, decimal>
            {
                { decimal.MinValue, decimal.MaxValue },
                { .2m, 4m },
                { .5m, .4234m }
            };
            dictionary.Doubles = new Dictionary<double, double>
            {
                //HINT: double.MinValue and double.MaxValue stored as keys in dictionary cannot be properly serialized by Json.Net
                { 0, double.MinValue },
                { 1, double.MaxValue },
                { .223d, 234d },
                { .513d, .4212334d }
            };
            dictionary.Floats = new Dictionary<float, float>
            {
                //HINT: float.MinValue and float.MaxValue stored as keys in dictioanry cannot be properly serialized by Json.Net
                { 0, float.MaxValue },
                { 1, float.MinValue },
                { .223f, 234f },
                { .513f, .4212334f }
            };
            dictionary.Enums = new Dictionary<DateTimeStyles, DateTimeKind>
            {
                { DateTimeStyles.AdjustToUniversal, DateTimeKind.Local },
                { DateTimeStyles.AllowLeadingWhite, DateTimeKind.Unspecified }
            };
            dictionary.Longs = new Dictionary<long, long>
            {
                { long.MaxValue, long.MinValue },
                { 34234, 234324 },
                { 45345345, 34534534565 }
            };
            dictionary.SBytes = new Dictionary<sbyte, sbyte>
            {
                { sbyte.MaxValue, sbyte.MaxValue },
                { 56, 13 }
            };
            dictionary.Shorts = new Dictionary<short, short>
            {
                { short.MinValue, short.MaxValue },
                { 5231, 6123 }
            };
            dictionary.Strings = new Dictionary<string, string>
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" },
                { "Key3", "Value3" }
            };
            dictionary.UInts = new Dictionary<uint, uint>
            {
                { uint.MinValue, 23 },
                { uint.MaxValue, 34324 }
            };
            dictionary.ULongs = new Dictionary<ulong, ulong>
            {
                //HINT: unsigned ulong cannot be properly deserialized by Json.Net
                //      see: http://stackoverflow.com/questions/9355091/json-net-crashes-when-serializing-unsigned-integer-ulong-array
                //      looks like JSON format constraint
                //{ulong.MinValue, ulong.MaxValue},
                { 34324234, 3243243245 }
            };
            dictionary.UShorts = new Dictionary<ushort, ushort>
            {
                { ushort.MinValue, ushort.MaxValue },
                { 42324, 32 }
            };
        }

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            var expected = (MessageWithDictionaries)expectedObj;
            var actual = (MessageWithDictionaries)actualObj;

            CollectionAssert.AreEqual(expected.Bools, actual.Bools);
            CollectionAssert.AreEqual(expected.Chars, actual.Chars);
            CollectionAssert.AreEqual(expected.Bytes, actual.Bytes);
            CollectionAssert.AreEqual(expected.Ints, actual.Ints);
            CollectionAssert.AreEqual(expected.Decimals, actual.Decimals);
            CollectionAssert.AreEqual(expected.Doubles, actual.Doubles);
            CollectionAssert.AreEqual(expected.Floats, actual.Floats);
            CollectionAssert.AreEqual(expected.Enums, actual.Enums);
            CollectionAssert.AreEqual(expected.Longs, actual.Longs);
            CollectionAssert.AreEqual(expected.SBytes, actual.SBytes);
            CollectionAssert.AreEqual(expected.Shorts, actual.Shorts);
            CollectionAssert.AreEqual(expected.Strings, actual.Strings);
            CollectionAssert.AreEqual(expected.UInts, actual.UInts);
            CollectionAssert.AreEqual(expected.ULongs, actual.ULongs);
            CollectionAssert.AreEqual(expected.UShorts, actual.UShorts);
        }
    }
}