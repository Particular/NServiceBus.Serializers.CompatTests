using System;
using System.Collections.Generic;
using Common;
using Common.Tests.TestCases;

class ExcludeList : Excludes
{
    protected override Dictionary<Type, string[]> FilesToExclude { get; } = new Dictionary<Type, string[]>
    {
        {typeof(TestEvents), new[]
        {
            "NServiceBus3.3 .NET Framework 4.5.2.json",
            "NServiceBus4.0 .NET Framework 4.5.2.json",
            "NServiceBus4.1 .NET Framework 4.5.2.json",
            "NServiceBus4.2 .NET Framework 4.5.2.json",
            "NServiceBus4.3 .NET Framework 4.5.2.json",
            "NServiceBus4.4 .NET Framework 4.5.2.json",
            "NServiceBus4.5 .NET Framework 4.5.2.json",
            "NServiceBus4.6 .NET Framework 4.5.2.json",
            "NServiceBus4.7 .NET Framework 4.5.2.json",
        }}
    };
}