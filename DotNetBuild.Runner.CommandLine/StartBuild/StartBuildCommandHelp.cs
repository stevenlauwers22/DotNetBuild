using System;

namespace DotNetBuild.Runner.CommandLine.StartBuild
{
    public class StartBuildCommandHelp 
        : ICommandHelp
    {
        public void Print()
        {
            Console.WriteLine("START-BUILD");
            Console.WriteLine("-----------");
            Console.WriteLine();

            Console.WriteLine("Example:");
            Console.WriteLine("\tstart-build " +
                StartBuildCommandConstants.BuildParameterAssembly + "[Assembly] " +
                StartBuildCommandConstants.BuildParameterTarget + "[Target] " +
                StartBuildCommandConstants.BuildParameterConfiguration + "[Configuration] ");

            Console.WriteLine("Description:");
            Console.WriteLine("\tstarts a build using the DotNetBuild runner");

            Console.WriteLine("Parameters:");
            Console.WriteLine("\t" + StartBuildCommandConstants.BuildParameterAssembly + "\tThe Path to the assembly you want to execute");
            Console.WriteLine("\t" + StartBuildCommandConstants.BuildParameterTarget + "\tOPTIONAL The target you want to run");
            Console.WriteLine("\t" + StartBuildCommandConstants.BuildParameterConfiguration + "\tOPTIONAL The configuration you want to use");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}