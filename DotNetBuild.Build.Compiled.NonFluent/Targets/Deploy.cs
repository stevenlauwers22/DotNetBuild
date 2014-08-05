﻿using System;
using System.Collections.Generic;
using DotNetBuild.Core;

namespace DotNetBuild.Build.Assembly.NonFluent.Targets
{
    public class Deploy : ITarget
    {
        public String Description
        {
            get { return "Deploy to NuGet"; }
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
            // TODO
            return true;
        }
    }
}