using System;

namespace DotNetBuild.Core.Facilities.State
{
    public interface IStateReader
        : IFacility
    {
        T Get<T>(String key);
    }
}