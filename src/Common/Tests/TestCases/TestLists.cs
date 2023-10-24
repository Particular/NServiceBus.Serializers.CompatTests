namespace Common.Tests.TestCases
{
    using System;
    using System.Globalization;
    using NUnit.Framework;
    using Types;

    public class TestLists : TestCase
    {
        public override Type MessageType => typeof(MessageWithLists);

        public override object CreateInstance()
        {
            var expected = new MessageWithLists
            {
                Bools =
                [
                    true,
                    false
                ],
                Chars =
                [
                    'a',
                    'b',
                    'c',
                    'd',
                    'e',
                    'f'
                ],
                Bytes =
                [
                    byte.MinValue,
                    byte.MaxValue,
                    11,
                    1,
                    1,
                    0
                ],
                Ints =
                [
                    int.MinValue,
                    int.MaxValue,
                    1,
                    2,
                    3,
                    4,
                    5,
                    6
                ],
                Decimals =
                [
                    decimal.MinValue,
                    decimal.MaxValue,
                    .2m,
                    4m,
                    .5m,
                    .4234m
                ],
                Doubles =
                [
                    double.MinValue,
                    double.MaxValue,
                    .223d,
                    234d,
                    .513d,
                    .4212334d
                ],
                Floats =
                [
                    float.MinValue,
                    float.MaxValue,
                    .223f,
                    234f,
                    .513f,
                    .4212334f
                ],
                Enums =
                [
                    DateTimeStyles.AdjustToUniversal,
                    DateTimeStyles.AllowLeadingWhite,
                    DateTimeStyles.AllowTrailingWhite
                ],
                Longs =
                [
                    long.MaxValue,
                    long.MinValue,
                    34234,
                    234324,
                    45345345,
                    34534534565
                ],
                SBytes =
                [
                    sbyte.MaxValue,
                    sbyte.MaxValue,
                    56,
                    13
                ],
                Shorts =
                [
                    short.MinValue,
                    short.MaxValue,
                    5231,
                    6123
                ],
                Strings =
                [
                    "Key1",
                    "Value1",
                    "Key2",
                    "Value2",
                    "Key3",
                    "Value3"
                ],
                UInts =
                [
                    uint.MinValue,
                    23,
                    uint.MaxValue,
                    34324
                ],
                ULongs =
                [
                    //HINT: max ulong values are not supported by JSON format
                    //ulong.MinValue,
                    //ulong.MaxValue,
                    34324234,
                    3243243245
                ],
                UShorts =
                [
                    ushort.MinValue,
                    ushort.MaxValue,
                    42324,
                    32
                ]
            };

            return expected;
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