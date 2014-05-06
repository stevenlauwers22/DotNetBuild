using DotNetBuild.Runner.Configuration;
using DotNetBuild.Runner.Infrastructure.TinyIoC;
using ScriptCs.Contracts;

namespace DotNetBuild.Runner.ScriptCs
{
    public class DotNetBuildScriptPack : IScriptPack
    {
        IScriptPackContext IScriptPack.GetContext()
        {
            return new DotNetBuildScriptPackContext();
        }

        void IScriptPack.Initialize(IScriptPackSession session)
        {
            var container = TinyIoCContainer.Current;
            Container.Install(container);
        }

        void IScriptPack.Terminate()
        {
        }
    }
}