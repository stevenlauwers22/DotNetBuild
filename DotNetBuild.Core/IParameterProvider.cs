using System;

namespace DotNetBuild.Core
{
    public interface IParameterProvider
    {
        String Get(String key);
    }
}