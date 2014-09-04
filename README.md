# DotNetBuild, a C# build system

## What is this?

DotNetBuild is a build system written in C#. A build system allows you to automate your work by scripting all repetitive actions into executable tasks.

## Why yet another 'Make' alternative?

Within the .NET eco-system builds are often automated with the help of MSBuild scripts. I have used MSBuild extensively during the last few years but I never had a lot of joy in writing MSBuild scripts. Using a markup language (XML) to create build scripts never felt right to me.

You're probably thinking now:

> XML is so 90s. You should've used gulp! gulp is cool, new and fancy. JS for the win!

Yeah, I know and you're probably right. Gulp is a great tool.
Anyway things are the way they are. So yes, I created a new 'Make', my apologies to [Hadi Hariri](http://hadihariri.com/2014/04/21/build-make-no-more/)

The reason I created this is twofold:

- Primarely, I wanted to be able to create .NET build scripts by using a .NET language
- It started off as an experiment and a technical challange for myself, but I got some very positive feedback so I decided to publish it

## What does it look like?

There are several ways to writing DotNetBuild scripts. I prefer the fluent API that the [scriptcs](http://scriptcs.net/) runner offers me.
Here's an example:

```C#
var dotNetBuild = Require<DotNetBuildScriptPackContext>();

dotNetBuild.AddTarget("ci", "Continuous integration target", c 
    => c.DependsOn("buildRelease")
        .And("runTests")
);

dotNetBuild.AddTarget("buildRelease", "Build in release mode", c 
	=> c.Do(context => {
            var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
			var msBuildTask = new MsBuildTask
			{
				Project = Path.Combine(solutionDirectory, "DotNetBuild.sln"),
				Target = "Rebuild",
				Parameters = "Configuration=Release"
			};

			return msBuildTask.Execute();
		})
);

dotNetBuild.AddTarget("runTests", "Run tests", c 
	=> c.Do(context => {
            var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
            var xunitExe = context.ConfigurationSettings.Get<String>("PathToXUnitRunnerExe");
            var xunitTask = new XunitTask
            {
                XunitExe = Path.Combine(solutionDirectory, xunitExe),
                Assembly = Path.Combine(solutionDirectory, @"DotNetBuild.Tests\bin\Release\DotNetBuild.Tests.dll")
            };

            return xunitTask.Execute();
		})
);

dotNetBuild.AddConfiguration("defaultConfig", c 
	=> c.AddSetting("SolutionDirectory", @"..\")
        .AddSetting("PathToXUnitRunnerExe", @"packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe")
);
```

The script above is a part of the DotNetBuild script I use to build DotNetBuild itself :). You can find the entire script [here](https://github.com/stevenlauwers22/DotNetBuild/blob/master/DotNetBuild.Build.ScriptCs.Fluent/build.csx)  
If you know Gulp, this syntax will probably look very familiar.

## How can I create DotNetBuild scripts?

Basically there are four ways to create DotNetBuild scripts

1. ScriptCs - Fluent API
2. ScriptCs - Without the fluent API
3. Precompiled .NET assembly - Fluent API
4. Precompiled .NET assembly - Without the fluent API

TODO: add tutorials for each of these

A cool thing about DotNetBuild is that it doesn't limit you to just one programming language to write your build scripts. If you're using scriptcs you can use any programming language that is supported by the scriptcs runtime (currently only C#, but I believe F# support is on its way). When using the precompiled .NET assembly you can use any programming language that compiled to a .NET assembly.

## Can I integrate my DotNetBuild scripts with my CI server?

Yes, custom build steps for TeamCity are on their way.  
Currently the only way to launch your script from your CI server is by using a command line task. 

