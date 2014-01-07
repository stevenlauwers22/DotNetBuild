using System;
using System.Collections.Generic;
using System.Linq;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_TargetTypeFilter
{
    public class When_told_to_Execute_its_Filter
        : TestSpecification<TargetTypeFilter>
    {
        private Type _target1;
        private Type _target2;
        private Type _target3;
        private IEnumerable<Type> _targets;
        private IEnumerable<Type> _result;

        protected override void Arrange()
        {
            _target1 = typeof (Dummy);
            _target2 = typeof (DummyTarget);
            _target3 = typeof (DummyNoMatchTarget);
            _targets = new List<Type> { _target1, _target2, _target3 };
        }

        protected override TargetTypeFilter CreateSubjectUnderTest()
        {
            return new TargetTypeFilter("DummyTarget");
        }

        protected override void Act()
        {
            _result = _targets.Where(t => Sut.Filter(t)).ToList();
        }

        [Fact]
        public void Returns_the_Filter_passed_in_its_constructor()
        {
            Assert.Equal(2, _result.Count());
            Assert.Contains(_target1, _result);
            Assert.Contains(_target2, _result);
            Assert.DoesNotContain(_target3, _result);
        }

        private class Dummy : ITarget
        {
            public string Name
            {
                get { return "Dummy"; }
            }

            public bool ContinueOnError
            {
                get { return false; }
            }

            public IEnumerable<ITarget> DependsOn
            {
                get { return null; }
            }

            public bool Execute(IConfigurationSettings configurationSettings)
            {
                return true;
            }
        }

        private class DummyTarget : ITarget
        {
            public string Name
            {
                get { return "Dummy target"; }
            }

            public bool ContinueOnError
            {
                get { return false; }
            }

            public IEnumerable<ITarget> DependsOn
            {
                get { return null; }
            }

            public bool Execute(IConfigurationSettings configurationSettings)
            {
                return true;
            }
        }

        private class DummyNoMatchTarget : ITarget
        {
            public string Name
            {
                get { return "DummyNoMatch target"; }
            }

            public bool ContinueOnError
            {
                get { return false; }
            }

            public IEnumerable<ITarget> DependsOn
            {
                get { return null; }
            }

            public bool Execute(IConfigurationSettings configurationSettings)
            {
                return true;
            }
        }
    }
}