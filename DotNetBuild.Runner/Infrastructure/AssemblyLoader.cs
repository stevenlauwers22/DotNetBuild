using System.IO;
using System.Reflection;

namespace DotNetBuild.Runner.Infrastructure
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
            if (assemblyFileInfo.Exists)
            {
                var assemblyFile = Assembly.LoadFile(assembly);
                var assemblyWrapper = new AssemblyWrapper(assemblyFile);
                return assemblyWrapper;
            }

            return null;
        }
    }
}