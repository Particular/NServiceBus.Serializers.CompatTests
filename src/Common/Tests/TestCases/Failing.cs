﻿namespace Common.Tests.TestCases
{
    using System;
    using NUnit.Framework;
    using Types;

    public class Failing : TestCase
    {
        public override Type MessageType => typeof(Person);

        //HINT: we need to put it and Passing here as Test.dll is not loaded into each custom nsb appdomains
        //      only Common.dll is. It should never run with other serialization test cases.
        public override bool IsSupported(SerializationFormat format, PackageVersion version) => false;

        public override object CreateInstance() => null;

        public override void CheckIfAreEqual(object expectedObj, object actualObj)
            => throw new AssertionException("This test case is always failing");
    }
}