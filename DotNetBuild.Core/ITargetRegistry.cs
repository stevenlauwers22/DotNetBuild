using System;

namespace DotNetBuild.Core
{
    public interface ITargetRegistry
    {
        ITarget Get(String key);
        void Add(String key, ITarget value);
    }
}