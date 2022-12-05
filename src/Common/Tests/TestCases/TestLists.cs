namespace Common.Tests.TestCases
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using NUnit.Framework;
    using Types;

    public class TestLists : TestCase
    {
        public override Type MessageType => typeof(MessageWithLists);

        public override void Populate(object instance)
        {
            var expected = (MessageWithLists)instance;

            expected.Bools = new List<bool>
            {
                true,
                false
            };
            expected.Chars = new List<char>
            {
                'a',
                'b',
                'c',
                'd',
                'e',
                'f'
            };
            expected.Bytes = new List<byte>
            {
                byte.MinValue,
                byte.MaxValue,
                11,
                1,
                1,
                0
            };
            expected.Ints = new List<int>
            {
                int.MinValue,
                int.MaxValue,
                1,
                2,
                3,
                4,
                5,
                6
            };
            expected.Decimals =
                new List<decimal>
                {
                    decimal.MinValue,
                    decimal.MaxValue,
                    .2m,
                    4m,
                    .5m,
                    .4234m
                };
            expected.Doubles =
                new List<double>
                {
                    double.MinValue,
                    double.MaxValue,
                    .223d,
                    234d,
                    .513d,
                    .4212334d
                };
            expected.Floats =
                new List<float>
                {
                    float.MinValue,
                    float.MaxValue,
                    .223f,
                    234f,
                    .513f,
                    .4212334f
                };
            expected.Enums = new List<DateTimeStyles>
            {
                DateTimeStyles.AdjustToUniversal,
                DateTimeStyles.AllowLeadingWhite,
                DateTimeStyles.AllowTrailingWhite
            };
            expected.Longs =
                new List<long>
                {
                    long.MaxValue,
                    long.MinValue,
                    34234,
                    234324,
                    45345345,
                    34534534565
                };
            expected.SBytes = new List<sbyte>
            {
                sbyte.MaxValue,
                sbyte.MaxValue,
                56,
                13
            };
            expected.Shorts = new List<short>
            {
                short.MinValue,
                short.MaxValue,
                5231,
                6123
            };
            expected.Strings = new List<string>
            {
                "Key1",
                "Value1",
                "Key2",
                "Value2",
                "Key3",
                "Value3"
            };
            expected.UInts = new List<uint>
            {
                uint.MinValue,
                23,
                uint.MaxValue,
                34324
            };
            expected.ULongs = new List<ulong>
            {
                //HINT: max ulong values are not supported by JSON format
                //ulong.MinValue,
                //ulong.MaxValue,
                34324234,
                3243243245
            };
            expected.UShorts = new List<ushort>
            {
                ushort.MinValue,
                ushort.MaxValue,
                42324,
                32
            };
        }

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
        {
            var expected = (MessageWithLists)expectedObj;
            var actual = (MessageWithLists)actualObj;

            CollectionAssert.AreEqual(expected.Bools, actual.Bools, "Bools collection does not match");
            CollectionAssert.AreEqual(expected.Chars, actual.Chars, "Chars collection does not match");
            CollectionAssert.AreEqual(expected.Bytes, actual.Bytes, " Bytes collection does not match");
            CollectionAssert.AreEqual(expected.Ints, actual.Ints, "Ints collection does not match");
            CollectionAssert.AreEqual(expected.Decimals, actual.Decimals, "Decimals collection does not match");
            CollectionAssert.AreEqual(expected.Doubles, actual.Doubles, "Doubles collection does not match");
            CollectionAssert.AreEqual(expected.Floats, actual.Floats, "Floats collection does not match");
            CollectionAssert.AreEqual(expected.Enums, actual.Enums, "Enums collection does not match");
            CollectionAssert.AreEqual(expected.Longs, actual.Longs, "Longs collection does not match");
            CollectionAssert.AreEqual(expected.SBytes, actual.SBytes, "SBytes collection does not match");
            CollectionAssert.AreEqual(expected.Shorts, actual.Shorts, "Shorts collection does not match");
            CollectionAssert.AreEqual(expected.Strings, actual.Strings, "String collection does not match");
            CollectionAssert.AreEqual(expected.UInts, actual.UInts, "UInts collection does not match");
            CollectionAssert.AreEqual(expected.ULongs, actual.ULongs, "ULongs collection does not match");
            CollectionAssert.AreEqual(expected.UShorts, actual.UShorts, "UShorts collection does not match");
        }
    }
}