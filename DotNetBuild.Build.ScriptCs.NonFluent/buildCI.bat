copy ..\DotNetBuild.Core\bin\Debug\DotNetBuild.Core.dll .\scriptcs_bin\DotNetBuild.Core.dll /y
copy ..\DotNetBuild.Core\bin\Debug\DotNetBuild.Core.pdb .\scriptcs_bin\DotNetBuild.Core.pdb /y
copy ..\DotNetBuild.Runner\bin\Debug\DotNetBuild.Runner.dll .\scriptcs_bin\DotNetBuild.Runner.dll /y
copy ..\DotNetBuild.Runner\bin\Debug\DotNetBuild.Runner.pdb .\scriptcs_bin\DotNetBuild.Runner.pdb /y
copy ..\DotNetBuild.Runner.ScriptCs\bin\Debug\DotNetBuild.Runner.ScriptCs.dll .\scriptcs_bin\DotNetBuild.Runner.ScriptCs.dll /y
copy ..\DotNetBuild.Runner.ScriptCs\bin\Debug\DotNetBuild.Runner.ScriptCs.pdb .\scriptcs_bin\DotNetBuild.Runner.ScriptCs.pdb /y
copy ..\DotNetBuild.Runner.ScriptCs\bin\Debug\ScriptCs.Contracts.dll .\scriptcs_bin\ScriptCs.Contracts.dll /y
copy ..\DotNetBuild.Tasks\bin\Debug\DotNetBuild.Tasks.dll .\scriptcs_bin\DotNetBuild.Tasks.dll /y
copy ..\DotNetBuild.Tasks\bin\Debug\DotNetBuild.Tasks.pdb .\scriptcs_bin\DotNetBuild.Tasks.pdb /y

scriptcs build.csx -- target:ci configuration:defaultConfig
pause