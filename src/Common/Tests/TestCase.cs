namespace Common.Tests
{
    using System;

    public abstract class TestCase
    {
        public abstract Type MessageType { get; }

        public virtual bool IsSupported(SerializationFormat format, PackageVersion version) => true;

        /// <summary>
        /// This is a workaround to allow creation of message instances via the message mapper that is exposed via the <see cref="ISerializerFacade.CreateInstance"/> method. The message mapper might be required to test the behavior of older versions of NServiceBus which heavily rely on the message mapper logic to handle interface message types.
        /// </summary>
        public virtual object CreateInstance(ISerializerFacade serializer) => CreateInstance();
        public abstract object CreateInstance();

        public abstract void CheckIfAreEqual(object expectedObj, object actualObj);
    }
}