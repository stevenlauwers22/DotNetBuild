using System;
using System.Collections.Generic;
using System.Diagnostics;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_TargetInspector
{
    public class When_told_to_CheckForCircularDependencies_on_a_none_circular_execution_path
        : TestSpecification<TargetInspector>
    {
        private ITarget _target;
        private IEnumerable<Type> _result;

        protected override void Arrange()
        {
            _target = new Dummy1Target();
        }

        protected override TargetInspector CreateSubjectUnderTest()
        {
            return new TargetInspector();
        }

        protected override void Act()
        {
            _result = Sut.CheckForCircularDependencies(_target);
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.Empty(_result);
        }

        private class Dummy1Target : ITarget
        {
            public string Name
            {
                get { return "Dummy target 1"; }
            }

            public bool ContinueOnError
            {
                get { return false; }
            }

            public IEnumerable<ITarget> DependsOn
            {
                get { return new List<ITarget> { new Dummy2Target() }; }
            }

            public bool Execute(IConfigurationSettings configurationSettings)
            {
                Debug.WriteLine("{0} - executing", Name);
                return true;
            }
        }

        private class Dummy2Target : ITarget
        {
            public string Name
            {
                get { return "Dummy target 2"; }
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
                Debug.WriteLine("{0} - executing", Name);
                return true;
            }
        }
    }
}