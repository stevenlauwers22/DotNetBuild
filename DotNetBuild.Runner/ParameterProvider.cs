using System;
using DotNetBuild.Core;

namespace DotNetBuild.Runner
{
    public class ParameterProvider : IParameterProvider
    {
        private readonly String[] _parameters;

        public ParameterProvider(String[] parameters)
        {
            _parameters = parameters;
        }

        public String Get(String key)
        {
            foreach (var parameter in _parameters)
            {
                if (parameter == null)
                    continue;
                
                if (parameter.StartsWith(key + ":", StringComparison.OrdinalIgnoreCase))
                {
                    var value = parameter.Substring(key.Length + 1);
                    return value;
                }
            }

            return null;
        }
    }
}