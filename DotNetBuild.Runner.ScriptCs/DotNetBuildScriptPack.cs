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
        }

        void IScriptPack.Terminate()
        {
        }
    }
}