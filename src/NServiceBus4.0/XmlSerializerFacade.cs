﻿using System;
using System.IO;
using Common;
using Common.Tests;
using NServiceBus.MessageInterfaces.MessageMapper.Reflection;
using NServiceBus.Serializers.XML;

class XmlSerializerFacade : ISerializerFacade
{
    public XmlSerializerFacade(params Type[] objectTypes)
    {
        mapper = new MessageMapper();
        serializer = new XmlMessageSerializer(mapper);
        mapper.Initialize(objectTypes);
        serializer.Initialize(objectTypes);
    }

    public SerializationFormat serializationFormat => SerializationFormat.Xml;

    public void Serialize(Stream stream, object instance)
    {
        serializer.Serialize(new[]
        {
            instance
        }, stream);
    }

    public object[] Deserialize(Stream stream)
    {
        return serializer.Deserialize(stream, null);
    }

    public object CreateInstance(Type type)
    {
        return type.IsInterface ? mapper.CreateInstance(type) : Activator.CreateInstance(type);
    }

    readonly XmlMessageSerializer serializer;
    readonly MessageMapper mapper;
}