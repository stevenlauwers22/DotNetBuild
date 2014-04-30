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
            session.AddReference("DotNetBuild.Core.dll");
            session.ImportNamespace("DotNetBuild.Core");

            session.AddReference("DotNetBuild.Runner.dll");
            session.ImportNamespace("DotNetBuild.Runner");
        }

        void IScriptPack.Terminate()
        {
        }
    }
}