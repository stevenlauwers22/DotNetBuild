using System;

namespace DotNetBuild.Core.Facilities.State
{
    public interface IStateWriter
        : IFacility
    {
        void Add(String key, object value);
    }
}