using System;
using DotNetBuild.Runner.Configuration;
using DotNetBuild.Runner.Infrastructure.TinyIoC;

namespace DotNetBuild.Runner.CommandLine
{
    public class Program
    {
        public static int Main(String[] args)
        {
            var container = TinyIoCContainer.Current;
            Container.Install(container);

            var dotNetBuild = new DotNetBuild(args, container);
            return dotNetBuild.Run();
        }
    }
}