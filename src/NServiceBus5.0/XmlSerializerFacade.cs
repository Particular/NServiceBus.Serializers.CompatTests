using System;
using System.IO;
using Common;
using Common.Tests;
using NServiceBus;
using NServiceBus.MessageInterfaces.MessageMapper.Reflection;
using NServiceBus.Serializers.XML;

class XmlSerializerFacade : ISerializerFacade
{
    public XmlSerializerFacade(params Type[] objectTypes)
    {
        mapper = new MessageMapper();
        serializer = new XmlMessageSerializer(mapper, new Conventions());
        mapper.Initialize(objectTypes);
        serializer.Initialize(objectTypes);
    }

    public SerializationFormat SerializationFormat => SerializationFormat.Xml;

    public void Serialize(Stream stream, object instance)
    {
        serializer.Serialize(instance, stream);
    }

    public object[] Deserialize(Stream stream)
    {
        return serializer.Deserialize(stream);
    }

    readonly XmlMessageSerializer serializer;
    readonly MessageMapper mapper;
}