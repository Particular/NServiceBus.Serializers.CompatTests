namespace Common.Tests
{
    using System;

    public abstract class TestCase
    {
        public abstract Type MessageType { get; }

        public virtual bool IsSupported(SerializationFormat format, PackageVersion version) => true;

        public abstract void Populate(object instance);

        public abstract void CheckIfAreEqual(object expectedObj, object actualObj);
    }
}