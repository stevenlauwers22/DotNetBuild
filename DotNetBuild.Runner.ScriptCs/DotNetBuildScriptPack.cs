﻿using System;
using DotNetBuild.Runner.Configuration;
using DotNetBuild.Runner.Infrastructure.TinyIoC;
using ScriptCs.Contracts;

namespace DotNetBuild.Runner.ScriptCs
{
    public class DotNetBuildScriptPack : IScriptPack
    {
        private String[] _args;

        public IScriptPackContext GetContext()
        {
            var container = TinyIoCContainer.Current;
            Container.Install(container);

            var dotNetBuild = new DotNetBuildScriptPackContext(_args, container);
            return dotNetBuild;
        }

        public void Initialize(IScriptPackSession session)
        {
            _args = session.ScriptArgs;

            session.ImportNamespace("DotNetBuild.Core");
            session.ImportNamespace("DotNetBuild.Runner");
            session.ImportNamespace("DotNetBuild.Runner.ScriptCs");
        }

        public void Terminate()
        {
        }
    }
}