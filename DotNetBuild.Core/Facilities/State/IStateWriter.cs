namespace DotNetBuild.Core.Facilities.State
{
    public interface IStateWriter
        : IFacility
    {
        void Add(string key, object value);
    }
}