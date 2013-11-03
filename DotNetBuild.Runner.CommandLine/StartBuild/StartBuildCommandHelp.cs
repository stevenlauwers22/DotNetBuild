using DotNetBuild.Runner.Infrastructure.Logging;

namespace DotNetBuild.Runner.CommandLine.StartBuild
{
    public class StartBuildCommandHelp 
        : ICommandHelp
    {
        public void Print(ILogger logger)
        {
            logger.Write("START-BUILD");
            logger.Write("-----------");
            logger.Write(string.Empty);

            logger.Write("Example:");
            logger.Write("\tstart-build " +
                StartBuildCommandConstants.BuildParameterAssembly + "[Assembly] " +
                StartBuildCommandConstants.BuildParameterTarget + "[Target] " +
                StartBuildCommandConstants.BuildParameterConfiguration + "[Configuration] ");

            logger.Write("Description:");
            logger.Write("\tstarts a build using the DotNetBuild runner");

            logger.Write("Parameters:");
            logger.Write("\t" + StartBuildCommandConstants.BuildParameterAssembly + "\tThe Path to the assembly you want to execute");
            logger.Write("\t" + StartBuildCommandConstants.BuildParameterTarget + "\tOPTIONAL The target you want to run");
            logger.Write("\t" + StartBuildCommandConstants.BuildParameterConfiguration + "\tOPTIONAL The configuration you want to use");
            logger.Write(string.Empty);
        }
    }
}