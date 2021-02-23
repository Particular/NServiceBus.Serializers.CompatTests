﻿namespace Common.Tests
{
    using System;

    public abstract class TestCase : MarshalByRefObject
    {
        public abstract Type MessageType { get; }

        public virtual bool IsSupported(SerializationFormat format, (int Major, int Minor, int Patch) version) => true;

        public abstract void Populate(object instance);

        public abstract void CheckIfAreEqual(object instanceA, object instanceB);
    }
}