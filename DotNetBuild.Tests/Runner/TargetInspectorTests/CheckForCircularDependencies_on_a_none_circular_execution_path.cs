using System;
using System.Collections.Generic;
using System.Diagnostics;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.TargetInspectorTests
{
    public class CheckForCircularDependencies_on_a_none_circular_execution_path
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
                get { return null; }
            }

            public Boolean Execute(TargetExecutionContext context)
            {
                Debug.WriteLine("{0} - executing", Description);
                return true;
            }
        }
    }
}