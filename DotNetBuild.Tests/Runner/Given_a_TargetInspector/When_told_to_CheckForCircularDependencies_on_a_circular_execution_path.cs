using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_TargetInspector
{
    public class When_told_to_CheckForCircularDependencies_on_a_circular_execution_path
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
            Assert.NotNull(_result);
            Assert.Equal(2, _result.Count());
            Assert.Equal(typeof(Dummy1Target), _result.ElementAt(0));
            Assert.Equal(typeof(Dummy2Target), _result.ElementAt(1));
        }

        private class Dummy1Target : ITarget
        {
            public String Description
            {
                get { return "Dummy target 1"; }
            }

            public Boolean ContinueOnError
            {
                get { return false; }
            }

            public IEnumerable<ITarget> DependsOn
            {
                get { return new List<ITarget> { new Dummy2Target() }; }
            }

            public Boolean Execute(TargetExecutionContext context)
            {
                Debug.WriteLine("{0} - executing", Description);
                return true;
            }
        }

        private class Dummy2Target : ITarget
        {
            public String Description
            {
                get { return "Dummy target 2"; }
            }

            public Boolean ContinueOnError
            {
                get { return false; }
            }

            public IEnumerable<ITarget> DependsOn
            {
                get { return new List<ITarget> { new Dummy1Target() }; }
            }

            public Boolean Execute(TargetExecutionContext context)
            {
                Debug.WriteLine("{0} - executing", Description);
                return true;
            }
        }
    }
}