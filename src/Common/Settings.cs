namespace Common
{
    using System.IO;

    public class Settings
    {
        //TODO where should we store the files?
        public static readonly DirectoryInfo TestDirectory = Directory.CreateDirectory(Path.Combine(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()), "temp", "serializer-compat-tests"));
    }
}