using System;
using System.IO;

static class StreamExtensions
{
    public static ReadOnlyMemory<byte> ReadFully(this Stream s)
    {
        using (var memoryStream = new MemoryStream())
        {
            s.CopyTo(memoryStream);
            return new ReadOnlyMemory<byte>(memoryStream.ToArray());
        }
    }
}

