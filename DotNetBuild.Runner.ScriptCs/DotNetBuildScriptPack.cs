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
            session.AddReference("ScriptCs.Contracts.dll");
            session.AddReference("DotNetBuild.Core.dll");
            session.AddReference("DotNetBuild.Runner.dll");

            session.ImportNamespace("DotNetBuild.Core");
            session.ImportNamespace("DotNetBuild.Runner");
            session.ImportNamespace("DotNetBuild.Runner.ScriptCs");

            var container = TinyIoCContainer.Current;
            Container.Install(container);
        }

        void IScriptPack.Terminate()
        {
        }
    }
}