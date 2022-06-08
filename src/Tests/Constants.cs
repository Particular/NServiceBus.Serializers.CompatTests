namespace Tests
{
    using System;
    using System.IO;

    public static class Constants
    {
        static Constants()
        {
            var sep = Path.DirectorySeparatorChar;
            var relativeDir = $"..{sep}..{sep}..{sep}..{sep}";
            ProjectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeDir));

        }

        public static string ProjectDirectory { get; }
    }
}