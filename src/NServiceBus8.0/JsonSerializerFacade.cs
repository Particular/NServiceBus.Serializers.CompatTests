using System;
using System.IO;
using Common;
using Common.Tests;
using NServiceBus;
using NServiceBus.MessageInterfaces.MessageMapper.Reflection;
using NServiceBus.Serialization;
using NServiceBus.Settings;

class JsonSerializerFacade : ISerializerFacade
{
    public JsonSerializerFacade(params Type[] objectTypes)
    {
        this.objectTypes = objectTypes;
        mapper = new MessageMapper();
        mapper.Initialize(objectTypes);
        var settings = new SettingsHolder();
        serializer = new NewtonsoftJsonSerializer().Configure(settings)(mapper);
    }

    public SerializationFormat SerializationFormat => SerializationFormat.Json;

    public void Serialize(Stream stream, object instance)
    {
        serializer.Serialize(instance, stream);
    }

    public object[] Deserialize(Stream stream)
    {
        return serializer.Deserialize(stream.ReadFully(), objectTypes);
    }

    public object CreateInstance(Type type)
    {
        return type.IsInterface ? mapper.CreateInstance(type) : Activator.CreateInstance(type);
    }

    MessageMapper mapper;
    IMessageSerializer serializer;
    Type[] objectTypes;
}
