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
            if (!assemblyFileInfo.Exists) 
                return null;
            
            //TODO: dit lijkt helemaal niet nodig te zijn owv Assembly.LoadFrom ipv Assembly.LoadFile
            //LoadDependencies(assembly);

            var assemblyFile = Assembly.LoadFrom(assembly);
            var assemblyWrapper = new AssemblyWrapper(assemblyFile);
            return assemblyWrapper;
        }

        //private static void LoadDependencies(string assembly)
        //{
        //    var assemblyFile = new FileInfo(assembly);
        //    var assemblyDirectory = assemblyFile.Directory;
        //    if (assemblyDirectory == null)
        //        return;

        //    var assemblyExtensions = new List<string> { "exe", "dll" };
        //    AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
        //    {
        //        var assemblyName = new AssemblyName(args.Name);
        //        foreach (var assemblyExtension in assemblyExtensions)
        //        {
        //            var assemblyPath = Path.Combine(assemblyDirectory.FullName, string.Format("{0}.{1}", assemblyName.Name, assemblyExtension));
        //            var assemblyFileInfo = new FileInfo(assemblyPath);
        //            if (assemblyFileInfo.Exists)
        //                return Assembly.LoadFrom(assemblyFileInfo.FullName);   
        //        }

        //        return null;
        //    };
        //}
    }
}