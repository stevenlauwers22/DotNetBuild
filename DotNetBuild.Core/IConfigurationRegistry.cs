using System;

namespace DotNetBuild.Core
{
    public interface IConfigurationRegistry
    {
        IConfigurationSettings Get(String key);
        void Add(String key, IConfigurationSettings value);
    }
}