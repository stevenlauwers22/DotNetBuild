using System;
using System.Collections.Generic;
using DotNetBuild.Core;

namespace DotNetBuild.Tests.Runner.ScriptCs.Targets
{
    public static class TargetRepository
    {
        private static readonly IDictionary<string, ITarget> TargetRegistry;

        static TargetRepository()
        {
            TargetRegistry = new Dictionary<string, ITarget>();
        }

        public static ITarget Get(string key)
        {
            if (!TargetRegistry.ContainsKey(key))
                return null;

            var value = TargetRegistry[key];
            return value;
        }

        public static void Add(string key, ITarget value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            if (value == null)
                throw new ArgumentNullException("value");

            TargetRegistry[key] = value;
        }
    }
}