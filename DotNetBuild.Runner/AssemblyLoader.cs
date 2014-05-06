using System.IO;
using System.Reflection;

namespace DotNetBuild.Runner
{
    public interface IAssemblyLoader
    {
        IAssemblyWrapper Load(string assembly);
    }

    public class AssemblyLoader 
        : IAssemblyLoader
    {
        public IAssemblyWrapper Load(string assembly)
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