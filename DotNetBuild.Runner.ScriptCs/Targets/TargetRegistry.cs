using System;
using System.Collections.Generic;
using DotNetBuild.Core;

namespace DotNetBuild.Runner.ScriptCs.Targets
{
    public static class TargetRegistry
    {
        private static readonly IDictionary<string, ITarget> Registrations;

        static TargetRegistry()
        {
            Registrations = new Dictionary<string, ITarget>();
        }

        public static ITarget Get(string key)
        {
            if (!Registrations.ContainsKey(key))
                return null;

            var value = Registrations[key];
            return value;
        }

        public static void Add(string key, ITarget value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            if (value == null)
                throw new ArgumentNullException("value");

            Registrations[key] = value;
        }
    }
}