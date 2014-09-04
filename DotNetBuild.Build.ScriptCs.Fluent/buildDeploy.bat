copy ..\DotNetBuild.Core\bin\Debug\DotNetBuild.Core.dll .\bin\DotNetBuild.Core.dll /y
copy ..\DotNetBuild.Core\bin\Debug\DotNetBuild.Core.pdb .\bin\DotNetBuild.Core.pdb /y
copy ..\DotNetBuild.Runner\bin\Debug\DotNetBuild.Runner.dll .\bin\DotNetBuild.Runner.dll /y
copy ..\DotNetBuild.Runner\bin\Debug\DotNetBuild.Runner.pdb .\bin\DotNetBuild.Runner.pdb /y
copy ..\DotNetBuild.Runner.ScriptCs\bin\Debug\DotNetBuild.Runner.ScriptCs.dll .\bin\DotNetBuild.Runner.ScriptCs.dll /y
copy ..\DotNetBuild.Runner.ScriptCs\bin\Debug\DotNetBuild.Runner.ScriptCs.pdb .\bin\DotNetBuild.Runner.ScriptCs.pdb /y
copy ..\DotNetBuild.Runner.ScriptCs\bin\Debug\ScriptCs.Contracts.dll .\bin\ScriptCs.Contracts.dll /y
copy ..\DotNetBuild.Tasks\bin\Debug\DotNetBuild.Tasks.dll .\bin\DotNetBuild.Tasks.dll /y
copy ..\DotNetBuild.Tasks\bin\Debug\DotNetBuild.Tasks.pdb .\bin\DotNetBuild.Tasks.pdb /y

scriptcs build.csx -- target:deploy configuration:defaultConfig versionNumber:1.0.0
pause