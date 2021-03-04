namespace Common
{
    using System.IO;

    public class Settings
    {
        public static readonly DirectoryInfo TestDirectory = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), "serializer-compat-tests"));
    }
}