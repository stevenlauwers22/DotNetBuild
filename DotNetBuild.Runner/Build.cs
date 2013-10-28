using System;
using System.Collections.Generic;
using DotNetBuild.Runner.Infrastructure.Events;
using DotNetBuild.Runner.StartBuild.BuildRequestedToStart;

namespace DotNetBuild.Runner
{
    public class Build
    {
        private readonly Guid _id;
        private readonly string _assembly;
        private readonly string _target;
        private readonly string _configuration;
        private readonly IEnumerable<KeyValuePair<string, string>> _additionalParameters;

        public Build(string assembly, string target, string configuration, IEnumerable<KeyValuePair<string, string>> additionalParameters)
        {
            _id = Guid.NewGuid();
            _assembly = assembly;
            _target = target;
            _configuration = configuration;
            _additionalParameters = additionalParameters;
        }

        public Guid Id
        {
            get { return _id; }
        }

        public string Assembly
        {
            get { return _assembly; }
        }

        public string Target
        {
            get { return _target; }
        }

        public string Configuration
        {
            get { return _configuration; }
        }

        public IEnumerable<KeyValuePair<string, string>> AdditionalParameters
        {
            get { return _additionalParameters; }
        }

        public event DomainEvent Notify;
        public void RequestStart()
        {
            Notify.InvokeIfNotNull(new BuildRequestedToStart(Id));
        }
    }
}