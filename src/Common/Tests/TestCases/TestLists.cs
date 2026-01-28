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

            using (Assert.EnterMultipleScope())
            {
                Assert.That(actual.Bools, Is.EqualTo(expected.Bools).AsCollection, "Bools collection does not match");
                Assert.That(actual.Chars, Is.EqualTo(expected.Chars).AsCollection, "Chars collection does not match");
                Assert.That(actual.Bytes, Is.EqualTo(expected.Bytes).AsCollection, " Bytes collection does not match");
                Assert.That(actual.Ints, Is.EqualTo(expected.Ints).AsCollection, "Ints collection does not match");
                Assert.That(actual.Decimals, Is.EqualTo(expected.Decimals).AsCollection, "Decimals collection does not match");
                Assert.That(actual.Doubles, Is.EqualTo(expected.Doubles).AsCollection, "Doubles collection does not match");
                Assert.That(actual.Floats, Is.EqualTo(expected.Floats).AsCollection, "Floats collection does not match");
                Assert.That(actual.Enums, Is.EqualTo(expected.Enums).AsCollection, "Enums collection does not match");
                Assert.That(actual.Longs, Is.EqualTo(expected.Longs).AsCollection, "Longs collection does not match");
                Assert.That(actual.SBytes, Is.EqualTo(expected.SBytes).AsCollection, "SBytes collection does not match");
                Assert.That(actual.Shorts, Is.EqualTo(expected.Shorts).AsCollection, "Shorts collection does not match");
                Assert.That(actual.Strings, Is.EqualTo(expected.Strings).AsCollection, "String collection does not match");
                Assert.That(actual.UInts, Is.EqualTo(expected.UInts).AsCollection, "UInts collection does not match");
                Assert.That(actual.ULongs, Is.EqualTo(expected.ULongs).AsCollection, "ULongs collection does not match");
                Assert.That(actual.UShorts, Is.EqualTo(expected.UShorts).AsCollection, "UShorts collection does not match");
            }
        }
    }
}