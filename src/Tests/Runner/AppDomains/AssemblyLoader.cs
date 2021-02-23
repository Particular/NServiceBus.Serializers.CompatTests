namespace Common.Runner.AppDomains
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class AssemblyLoader : MarshalByRefObject
    {
        public void LoadAssembly(string assemblyFile)
        {
            try
            {
                Assembly.LoadFrom(assemblyFile);
            }
            catch (FileNotFoundException)
            {
                // continue loading no matter what
            }
        }

        public IEnumerable<AssemblyName> GetLoadedAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().Select(assembly => assembly.GetName()).ToList();
        }
    }
}