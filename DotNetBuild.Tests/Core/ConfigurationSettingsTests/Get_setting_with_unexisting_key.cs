﻿using System;
using DotNetBuild.Core;
using Xunit;

namespace DotNetBuild.Tests.Core.ConfigurationSettingsTests
{
    public class Get_setting_with_unexisting_key
        : TestSpecification<ConfigurationSettings>
    {
        private String _key;
        private String _result;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
        }

        protected override ConfigurationSettings CreateSubjectUnderTest()
        {
            return new ConfigurationSettings();
        }

        protected override void Act()
        {
            _result = Sut.Get<String>(_key);
        }

        [Fact]
        public void Returns_the_setting()
        {
            Assert.Null(_result);
        }
    }
}