using System;
using DotNetBuild.Runner.Configuration;
using DotNetBuild.Runner.Infrastructure.TinyIoC;

namespace DotNetBuild.Runner.Assembly
{
    public class Program
    {
        public static int Main(String[] args)
        {
            var container = TinyIoCContainer.Current.RegisterDotNetBuild();
            var dotNetBuild = new DotNetBuild(args, container);
            return dotNetBuild.Run();
        }
    }
}