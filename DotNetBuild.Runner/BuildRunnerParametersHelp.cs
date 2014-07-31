using System;
using DotNetBuild.Runner.Infrastructure.Logging;

namespace DotNetBuild.Runner
{
    public interface IBuildRunnerParametersHelp
    {
        void Print(ILogger logger);
    }

    public class BuildRunnerParametersHelp 
        : IBuildRunnerParametersHelp
    {
        public void Print(ILogger logger)
        {
            logger.Write("Example:");
            logger.Write("\t" +
                BuildRunnerParametersConstants.Assembly + "[Assembly] " +
                BuildRunnerParametersConstants.Target + "[Target] " +
                BuildRunnerParametersConstants.Configuration + "[Configuration] ");

            logger.Write("Description:");
            logger.Write("\tStarts a build using the DotNetBuild runner");

            logger.Write("Parameters:");
            logger.Write("\t" + BuildRunnerParametersConstants.Assembly + "\tThe Path to the assembly you want to execute");
            logger.Write("\t" + BuildRunnerParametersConstants.Target + "\tThe target you want to run");
            logger.Write("\t" + BuildRunnerParametersConstants.Configuration + "\tOPTIONAL The configuration you want to use");
            logger.Write(String.Empty);
        }
    }
}