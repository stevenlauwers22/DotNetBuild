using System;
using System.IO;
using System.Reflection;

namespace DotNetBuild.Runner
{
    public interface IAssemblyLoader
    {
        IAssemblyWrapper Load(String assembly);
    }

    public class AssemblyLoader 
        : IAssemblyLoader
    {
        public IAssemblyWrapper Load(String assembly)
        {
            var assemblyFileInfo = new FileInfo(assembly);
            if (!assemblyFileInfo.Exists) 
                return null;

            var assemblyFile = Assembly.LoadFrom(assembly);
            var assemblyWrapper = new AssemblyWrapper(assemblyFile);
            return assemblyWrapper;
        }
    }
}