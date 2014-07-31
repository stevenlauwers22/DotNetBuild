using DotNetBuild.Runner.Configuration;
using DotNetBuild.Runner.Infrastructure.TinyIoC;
using ScriptCs.Contracts;

namespace DotNetBuild.Runner.ScriptCs
{
    public class DotNetBuildScriptPack : IScriptPack
    {
        private string[] _args;

        IScriptPackContext IScriptPack.GetContext()
        {
            var container = TinyIoCContainer.Current;
            Container.Install(container);

            var dotNetBuild = new DotNetBuildScriptPackContext(_args, container);
            return dotNetBuild;
        }

        void IScriptPack.Initialize(IScriptPackSession session)
        {
            _args = session.ScriptArgs;

            session.AddReference("ScriptCs.Contracts.dll");
            session.AddReference("DotNetBuild.Core.dll");
            session.AddReference("DotNetBuild.Runner.dll");

            session.ImportNamespace("DotNetBuild.Core");
            session.ImportNamespace("DotNetBuild.Runner");
            session.ImportNamespace("DotNetBuild.Runner.ScriptCs");
        }

        void IScriptPack.Terminate()
        {
        }
    }
}