namespace DotNetBuild.Core.Facilities.State
{
    public interface IStateReader
        : IFacility
    {
        T Get<T>(string key);
    }
}