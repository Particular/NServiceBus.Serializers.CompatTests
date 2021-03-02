﻿namespace Common
{
    using System;
    using System.IO;
    using Tests;

    public interface ISerializerFacade
    {
        SerializationFormat serializationFormat { get; }
        void Serialize(Stream stream, object instance);
        object[] Deserialize(Stream stream);
        object CreateInstance(Type type);
    }
}