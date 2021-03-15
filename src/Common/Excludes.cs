namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Tests;

    /// <summary>
    /// Allows a specific test project to define test files that should not be verified.
    /// Create a class called "ExcludeList" without a namespace in the project and provide the file names per test case that should be ignored.
    /// </summary>
    public abstract class Excludes
    {
        protected abstract Dictionary<Type, string[]> FilesToExclude { get; }

        public static Excludes BuildExcludes()
        {
            var excludeListType = Type.GetType("ExcludeList," + Assembly.GetEntryAssembly().FullName, false);
            if (excludeListType != null)
            {
                return (Excludes)Activator.CreateInstance(excludeListType);
            }

            return new EmptyExcludes();
        }

        public bool Contains(TestCase testCase, string fileName)
        {
            if (FilesToExclude.TryGetValue(testCase.GetType(), out var filesToExclude))
            {
                return filesToExclude.Contains(fileName);
            }

            return false;
        }
    }

    class EmptyExcludes : Excludes
    {
        protected override Dictionary<Type, string[]> FilesToExclude { get; } = new Dictionary<Type, string[]>(0);
    }
}