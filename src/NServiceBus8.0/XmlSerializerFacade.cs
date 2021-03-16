using System;
using System.IO;
using System.Reflection;
using Common;
using Common.Tests;
using NServiceBus;
using NServiceBus.MessageInterfaces.MessageMapper.Reflection;
using NServiceBus.Serialization;
using NServiceBus.Settings;
using NServiceBus.Unicast.Messages;

class XmlSerializerFacade : ISerializerFacade
{
    public XmlSerializerFacade(params Type[] objectTypes)
    {
        mapper = new MessageMapper();
        var settings = new SettingsHolder();
        var conventions = CreateTestConventions(settings);
        // evil hack
        settings.Set((MessageMetadataRegistry)Activator.CreateInstance
            (typeof(MessageMetadataRegistry),
            BindingFlags.NonPublic | BindingFlags.Instance, null,
            new object[] { new Func<Type, bool>(conventions.IsMessageType) },
            null));
        settings.Set(conventions);
        settings.Set("TypesToScan", objectTypes);

        serializer = new XmlSerializer().Configure(settings)(mapper);
        mapper.Initialize(objectTypes);
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

    public object CreateInstance(Type type)
    {
        return type.IsInterface ? mapper.CreateInstance(type) : Activator.CreateInstance(type);
    }

    Conventions CreateTestConventions(SettingsHolder settings)
    {
        var builder = new ConventionsBuilder(settings);
        builder.DefiningMessagesAs(type => type.FullName.Contains("TestCases"));
        return builder.Conventions;
    }

    IMessageSerializer serializer;
    MessageMapper mapper;
}