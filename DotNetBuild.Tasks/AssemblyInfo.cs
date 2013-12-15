﻿

//-----------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs">(c) http://www.codeplex.com/MSBuildExtensionPack. This source is subject to the Microsoft Permissive License. See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx. All other rights reserved.</copyright>
// This task is based on the AssemblyInfo task written by Neil Enns (http://code.msdn.microsoft.com/AssemblyInfoTaskvers). It is used here with permission.
//-----------------------------------------------------------------------

namespace MSBuild.ExtensionPack.Framework
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Specifies how certain version numbers are incremented by the task.
    /// </summary>
    public enum IncrementMethod
    {
        /// <summary>
        /// Do not auto-increment the number.
        /// </summary>
        NoIncrement = 0,

        /// <summary>
        /// Add one to the current number.
        /// </summary>
        AutoIncrement = 1,

        /// <summary>
        /// Format the current date and time using a formatting string, and use that as the number.
        /// </summary>
        DateString = 2,

        /// <summary>
        /// Format the current date as the two digit year and the day of the year, and use that as the number, i.e. the revision number for 7/03/2009 is 09184
        /// </summary>
        Julian = 3,

        /// <summary>
        /// Format the current date as YYWWDW where YY is the year, WW is the week number and DW is the day of the week e.g. 2 Feb 2010 would be 10062. 15 March 2010 will be 10121 and 19 December 2010 10475.
        /// </summary>
        YearWeekDay = 4,

        /// <summary>
        /// Calculate the number of days elapsed since a given StartDate. Take note of the StartDate, PaddingCount and PaddingDigit parameters.
        /// </summary>
        ElapsedDays = 5
    }

    /// <summary>
    /// The AssemblyInfo task provides a way to manipulate the content of AssemblyInfo files at build time. It works with
    /// C#, VB, and J# AssemblyInfo files.
    /// <para/>This task is based on the AssemblyInfo task written by Neil Enns (http://code.msdn.microsoft.com/AssemblyInfoTaskvers). It is used here with permission.<para/>
    /// </summary>
    /// <remarks>
    ///     <para>The primary use of the AssemblyInfo task is to set assembly version numbers
    ///     at build time. The typical way to use it is to add the
    ///     MSBuild.ExtensionPack.VersionNumber.Targets file to your project file, and to then specify
    ///     properties in your project file to control the assembly version numbers.</para>
    ///     <para>Version numbers are of the form A.B.C.D, where:</para>
    ///     <list type="bullet">
    ///         <item>A is the major version</item>
    ///         <item>B is the minor version</item>
    ///         <item>C is the build number</item>
    ///         <item>D is the revision</item>
    ///     </list>
    ///     <para>Typically the major and minor versions are fixed and do not change over the
    ///     course of multiple daily builds. The build number is frequently set to increment on a daily
    ///     basis, either starting at 1 and continuing from there, or as some representation of
    ///     the date of the build. The revision is typically used to differentiate between multiple builds on
    ///     the same day, usually starting at 1 and incrementing for each build.</para>
    ///     <para>
    ///         To get the standard Visual Studio-style version simply add the
    ///         MSBuild.ExtensionPack.VersionNumber.Targets file to your project. To override the default
    ///         version numbers, such as the major and minor version, you can set the
    ///         appropriate properties. For more information see the
    ///         <see cref="AssemblyMajorVersion">AssemblyMajorVersion</see> and
    ///         <see cref="AssemblyMinorVersion">AssemblyMinorVersion</see> items.
    ///     </para>
    /// <para/>
    /// <para/>
    /// <b>For use with Team Foundation Server, see this blog post: </b><a href="http://blogs.msdn.com/aaronhallberg/archive/2007/06/08/team-build-and-the-assemblyinfo-task.aspx">Team Build and the AssemblyInfo Task</a><para/>
    /// <para/><b>How To: Auto-Increment Version Numbers for a Project</b>
    /// The most common way to use the AssemblyInfo task is to add a reference to the MSBuild.ExtensionPack.VersionNumber.targets file to any project file whose AssemblyInfo you want to manage.
    /// <para/>
    /// For standard .csproj, .vbproj, and .vjsproj files do the following:
    /// <para/>
    /// Open the project in Visual Studio 2005 
    /// Right-click on the project in Solution Explorer and select Unload Project 
    /// Right-click on the project in Solution Explorer and select Edit [project file]
    /// If the AssemblyInfoTask was installed into the Global Assembly Cache add the following line at the end of the project file after the last &lt;Import&gt; tag:
    /// <para/>
    /// &lt;Import Project="$(MSBuildExtensionsPath)\Microsoft\ExtensionPack\MSBuild.ExtensionPack.VersionNumber.targets"/&gt;
    /// <para/>
    /// If the AssemblyInfoTask was installed into the user's Application Data folder add the following line at the end of the project file after the last &lt;Import&gt; tag:
    /// <para/>
    /// &lt;Import Project="$(APPDATA)\Microsoft\MSBuild\ExtensionPack\MSBuild.ExtensionPack.VersionNumber.targets"/&gt;
    /// Save and close the project file 
    /// Right-click on the project in Solution Explorer and select Reload Project
    /// With these project file modifications all builds will have auto-incrementing assembly and file versions of the following format:
    /// <para/>
    /// 1.0.date.revision
    /// <para/>
    /// For example, the first build on November 10th, 2005 will have a version number of:
    /// <para/>
    /// 1.0.51110.00 
    /// <para/>
    /// Subsequent builds on the same day will have version numbers 1.0.51110.01, 1.0.51110.02, and so on.
    /// <para/>
    /// Note: All AssemblyInfo.* files must have have entries with a starting value of "1.0.0.0" for AssemblyVersion and AssemblyFileVersion so the AssemblyInfoTask will work correctly. If these entries are missing from the files a build error will be generated.
    /// <para/>
    /// Overriding the Default Version Number Behaviour
    /// In some situations the desired version number behaviour may be different than the defaults offered by the MSBuild.ExtensionPack.VersionNumber.targets file.
    /// <para/>
    /// To set the assembly and file versions to specific numbers add the appropriate property to your project file. For example, to set the major version to 8, add the following two properties:
    /// <para/>
    /// &lt;AssemblyMajorVersion&gt;8&lt;/AssemblyMajorVersion&gt;
    /// <para/>
    /// &lt;AssemblyFileMajorVersion&gt;8&lt;/AssemblyFileMajorVersion&gt;
    /// <para/>
    /// For more information see the assembly version properties reference.
    /// </remarks>
    /// <seealso cref="AssemblyMajorVersion"/>
    /// <seealso cref="AssemblyMinorVersion"/>
    /// <example>
    /// <code lang="xml"><![CDATA[
    /// <Project ToolsVersion="4.0" DefaultTargets="Default" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
    ///     <PropertyGroup>
    ///         <TPath>$(MSBuildProjectDirectory)\..\MSBuild.ExtensionPack.tasks</TPath>
    ///         <TPath Condition="Exists('$(MSBuildProjectDirectory)\..\..\Common\MSBuild.ExtensionPack.tasks')">$(MSBuildProjectDirectory)\..\..\Common\MSBuild.ExtensionPack.tasks</TPath>
    ///     </PropertyGroup>
    ///     <Import Project="$(TPath)"/>
    ///     <Target Name="Default">
    ///         <ItemGroup>
    ///             <AssemblyInfoFiles Include="C:\a\CommonAssemblyInfo.cs"/>
    ///             <AssemblyInfoFiles1 Include="C:\a\CommonAssemblyInfo1.cs"/>
    ///             <AssemblyInfoFiles2 Include="C:\a\CommonAssemblyInfo2.cs"/>
    ///         </ItemGroup>
    ///         <!-- Update an attribute and don't do any versioning -->
    ///         <MSBuild.ExtensionPack.Framework.AssemblyInfo ComVisible="true" AssemblyInfoFiles="@(AssemblyInfoFiles)" SkipVersioning="true"/>
    ///         <!-- Version using YearWeekDay and set the start of the week as a Sunday -->
    ///         <MSBuild.ExtensionPack.Framework.AssemblyInfo AssemblyInfoFiles="@(AssemblyInfoFiles1)" AssemblyBuildNumberType="YearWeekDay" FirstDayOfWeek="Sunday"/>
    ///         <!-- Version using the number of days elapsed since a given start date-->
    ///         <MSBuild.ExtensionPack.Framework.AssemblyInfo AssemblyInfoFiles="@(AssemblyInfoFiles2)" StartDate="1 Jan 1976" AssemblyBuildNumberType="ElapsedDays"/>
    ///     </Target>
    /// </Project>
    /// ]]></code>    
    /// </example>
    public class AssemblyInfo
    {
        private AssemblyVersionSettings _assemblyFileVersionSettings;
        private AssemblyVersionSettings _assemblyVersionSettings;
        private string _maxAssemblyFileVersion;
        private string _maxAssemblyVersion;
        private Encoding _fileEncoding = Encoding.UTF8;
        private string _firstDayOfWeek = "Monday";

        /// <summary>
        /// The major version of the assembly.
        /// </summary>
        /// <remarks>
        ///     <para>To change the assembly major version set this to the specific major version
        ///     you want. For example, for Visual Studio 2005 build 8.0.50727.42 this is set to
        ///     "8".</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyMajorVersion</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyMajorVersion&gt;8&lt;/AssemblyMajorVersion&gt;
        ///     </code>
        /// </example>
        public string AssemblyMajorVersion
        {
            get { return _assemblyVersionSettings.MajorVersion; }
            set { _assemblyVersionSettings.MajorVersion = value; }
        }

        /// <summary>
        /// The minor version of the assembly.
        /// </summary>
        /// <remarks>
        ///     <para>To change the assembly minor version set this to the specific minor version
        ///     you want. For example, for Visual Studio 2005 build 8.0.50727.42 this is set to
        ///     "0".</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyMinorVersion</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyMinorVersion&gt;0&lt;/AssemblyMinorVersion&gt;
        ///     </code>
        /// </example>
        public string AssemblyMinorVersion
        {
            get { return _assemblyVersionSettings.MinorVersion; }
            set { _assemblyVersionSettings.MinorVersion = value; }
        }

        /// <summary>
        /// The build number of the assembly.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         To change the assembly build number set this to the specific build number you
        ///         want. In most cases you do not want to use this property. Instead, use the
        ///         <see cref="AssemblyBuildNumberType">AssemblyBuildNumberType</see> and
        ///         <see cref="AssemblyBuildNumberFormat">AssemblyBuildNumberFormat</see>
        ///         properties to have this value determined automatically at build time.
        ///     </para>
        ///     <para>
        ///         To force the build number to a specific value when using the
        ///         MSBuild.ExtensionPack.VersionNumber.Targets, use the <em>AssemblyBuildNumber</em> property,
        ///         and set the <em>AssemblyBuildNumberFormat</em> property to
        ///         <see cref="IncrementMethod">DirectSet</see>.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyBuildNumber&gt;0&lt;/AssemblyBuildNumber&gt;
        /// &lt;AssemblyBuildNumberType&gt;DirectSet&lt;/AssemblyBuildNumberType&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyBuildNumberType"/>
        /// <seealso cref="AssemblyBuildNumberFormat"/>
        public string AssemblyBuildNumber
        {
            get { return _assemblyVersionSettings.BuildNumber; }
            set { _assemblyVersionSettings.BuildNumber = value; }
        }

        /// <summary>
        /// The revision of the assembly.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         To change the assembly revision set this to the specific revision number you
        ///         want. In most cases you do not want to use this property. Instead, use the
        ///         <see cref="AssemblyRevisionType">AssemblyRevisionNumberType</see> and
        ///         <see cref="AssemblyRevisionFormat">AssemblyRevisionNumberFormat</see>
        ///         properties to have this value determined automatically at build time.
        ///     </para>
        ///     <para>
        ///         To force the revision number to a specific value when using the
        ///         MSBuild.ExtensionPack.VersionNumber.Targets, set the <em>AssemblyRevision</em> property to
        ///         the value and set the <em>AssemblyRevisionFormat</em> property to
        ///         <see cref="IncrementMethod">DirectSet</see>.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyRevision&gt;0&lt;/AssemblyRevision&gt;
        /// &lt;AssemblyRevisionType&gt;DirectSet&lt;/AssemblyRevisionType&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyRevisionType"/>
        /// <seealso cref="AssemblyRevisionFormat"/>
        public string AssemblyRevision
        {
            get { return _assemblyVersionSettings.Revision; }
            set { _assemblyVersionSettings.Revision = value; }
        }

        /// <summary>
        /// The complete version of the assembly.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Use AssemblyVersion to directly set the entire version number with a single
        ///         parameter. For example, if you know you want your version to be "1.2.3.4", you
        ///         can set AssemblyVersion to this instead of having to use each of the individual
        ///         <see cref="AssemblyMajorVersion">AssemblyMajorVersion</see>,
        ///         <see cref="AssemblyMinorVersion">AssemblyMinorVersion</see>,
        ///         <see cref="AssemblyBuildNumber">AssemblyBuildNumber</see>, and
        ///         <see cref="AssemblyRevision">AssemblyRevision</see> properties.
        ///     </para>
        ///     <para>Note that the other four properties override this one. For example, If you
        ///     set AssemblyVersion to "1.2.3.4" and then set AssemblyMinorVersion to 6, the resulting
        ///     version will be "1.6.3.4".</para>
        ///     <para>
        ///         This property is an input only. If you want to know what the final version
        ///         generated was, use the
        ///         <see cref="MaxAssemblyVersion">MaxAssemblyVersion</see> output property
        ///         instead.
        ///     </para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file the best way to specify
        ///     this is to set the <em>AssemblyVersion</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyVersion&gt;1.2.3.4&lt;/AssemblyVersion&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="MaxAssemblyVersion"/>
        public string AssemblyVersion
        {
            get { return _assemblyVersionSettings.Version; }
            set { _assemblyVersionSettings.Version = value; }
        }

        /// <summary>
        /// The type of update to use when setting the <see cref="AssemblyBuildNumber">AssemblyBuildNumber</see> property.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The AssemblyBuildNumber can be set using several different methods. The
        ///         AssemblyBuildNumberType property is used to select the desired method. The
        ///         supported types are defined in the
        ///         <see cref="IncrementMethod">IncrementMethod</see> enumeration.
        ///     </para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file the default setting is
        ///     DateFormat. To override this set the <em>AssemblyBuildNumberType</em>
        ///     property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyBuildNumberType&gt;DateFormat&lt;/AssemblyBuildNumberType&gt;
        /// &lt;AssemblyBuildNumberFormat&gt;yyMMdd&lt;/AssemblyBuildNumberFormat&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyBuildNumberFormat"/>
        /// <seealso cref="IncrementMethod"/>
        public string AssemblyBuildNumberType { get; set; }

        /// <summary>
        /// The type of update to use when setting the <see cref="AssemblyRevision">AssemblyRevision</see> property.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The AssemblyRevision can be set using several different methods. The
        ///         AssemblyRevisionType property is used to select the desired method. The
        ///         supported types are defined in the
        ///         <see cref="IncrementMethod">IncrementMethod</see> enumeration.
        ///     </para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file the default setting is
        ///     AutoIncrement. To override this set the <em>AssemblyRevisionType</em>
        ///     property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyRevisionType&gt;AutoIncrement&lt;/AssemblyRevisionType&gt;
        /// &lt;AssemblyRevisionFormat&gt;00&lt;/AssemblyRevisionFormat&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyRevisionFormat"/>
        /// <seealso cref="IncrementMethod"/>
        public string AssemblyRevisionType { get; set; }

        /// <summary>
        /// Whether the AssemblyRevisionNumber will be reset to 0 when a DateString or Julian BuildNumberType is used in conjunction with an AutoIncrement <see cref="AssemblyRevisionType">AssemblyRevisionType</see>.
        /// </summary>
        /// <remarks>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file, the default setting is
        ///     True. To override this set the <em>AssemblyRevisionReset</em>
        ///     property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyRevisionReset&gt;False&lt;/AssemblyRevisionReset&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyRevisionFormat"/>
        /// <seealso cref="AssemblyRevisionType"/>
        /// <seealso cref="IncrementMethod"/>
        public string AssemblyRevisionReset { get; set; }

        /// <summary>
        /// Whether the AssemblyFileRevisionNumber will be reset to 0 when a DateString or Julian BuildNumberType is used in conjunction with an AutoIncrement <see cref="AssemblyRevisionType">AssemblyRevisionType</see>.
        /// </summary>
        /// <remarks>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file, the default setting is
        ///     True. To override this set the <em>AssemblyFileRevisionReset</em>
        ///     property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyFileRevisionReset&gt;False&lt;/AssemblyFileRevisionReset&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyRevisionFormat"/>
        /// <seealso cref="AssemblyRevisionType"/>
        /// <seealso cref="IncrementMethod"/>
        public string AssemblyFileRevisionReset { get; set; }

        /// <summary>
        /// Set to true to skip setting version information. Default is false.
        /// </summary>
        public bool SkipVersioning { get; set; }

        /// <summary>
        /// The format string to apply when converting the build number to a text string.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Use this property to control the formatting of the build number when it is
        ///         converted from a number to a string. This is particularly useful when used in
        ///         conjunction with the <see cref="IncrementMethod">DateFormat</see> or
        ///         <see cref="IncrementMethod">AutoIncrement</see> methods of setting the build
        ///         number. Any valid .NET formatting string can be specified.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyBuildNumberType&gt;DateFormat&lt;/AssemblyBuildNumberType&gt;
        /// &lt;AssemblyBuildNumberFormat&gt;yyMMdd&lt;/AssemblyBuildNumberFormat&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyBuildNumberType"/>
        public string AssemblyBuildNumberFormat
        {
            get { return _assemblyVersionSettings.BuildNumberFormat; }
            set { _assemblyVersionSettings.BuildNumberFormat = value; }
        }

        /// <summary>
        /// The format string to apply when converting the revision to a text string.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Use this property to control the formatting of the revision when it is
        ///         converted from a number to a string. This is particularly useful when used in
        ///         conjunction with the <see cref="IncrementMethod">DateFormat</see> or
        ///         <see cref="IncrementMethod">AutoIncrement</see> methods of setting the
        ///         revision. Any valid .NET formatting string can be specified.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyRevisionType&gt;AutoIncrement&lt;/AssemblyRevisionType&gt;
        /// &lt;AssemblyRevisionFormat&gt;00&lt;/AssemblyRevisionFormat&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyBuildNumberType"/>
        public string AssemblyRevisionFormat
        {
            get { return _assemblyVersionSettings.RevisionFormat; }
            set { _assemblyVersionSettings.RevisionFormat = value; }
        }

        /// <summary>Returns the largest assembly version set by the task.</summary>
        /// <remarks>
        ///     <para>Use this property to find out the largest assembly version that was generated
        ///     by the task. If only one assemblyinfo.* file was specified as an input, this will
        ///     be the resulting assembly version for that file. If more than one assemblyinfo.*
        ///     file was specified, this will be the largest build number generated.</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file this value is placed in
        ///     <em>MaxAssemblyVersion</em> property after the UpdateAssemblyInfoFiles target is
        ///     run.</para>
        /// </remarks>
        public string MaxAssemblyVersion
        {
            get { return _maxAssemblyVersion; }
            set { _maxAssemblyVersion = value; }
        }

        /// <summary>
        /// The major version of the assembly file.
        /// </summary>
        /// <remarks>
        ///     <para>To change the assembly file major version set this to the specific major version
        ///     you want. For example, for Visual Studio 2005 build 8.0.50727.42 this is set to
        ///     "8".</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyFileMajorVersion</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyFileMajorVersion&gt;8&lt;/AssemblyFileMajorVersion&gt;
        ///     </code>
        /// </example>
        public string AssemblyFileMajorVersion
        {
            get { return _assemblyFileVersionSettings.MajorVersion; }
            set { _assemblyFileVersionSettings.MajorVersion = value; }
        }

        /// <summary>
        /// The minor version of the assembly file.
        /// </summary>
        /// <remarks>
        ///     <para>To change the assembly file minor version set this to the specific minor version
        ///     you want. For example, for Visual Studio 2005 build 8.0.50727.42 this is set to
        ///     "0".</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyFileMinorVersion</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyFileMinorVersion&gt;0&lt;/AssemblyFileMinorVersion&gt;
        ///     </code>
        /// </example>
        public string AssemblyFileMinorVersion
        {
            get { return _assemblyFileVersionSettings.MinorVersion; }
            set { _assemblyFileVersionSettings.MinorVersion = value; }
        }

        /// <summary>
        /// The build number of the assembly file.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         To change the assembly file build number set this to the specific build number you
        ///         want. In most cases you do not want to use this property. Instead, use the
        ///         <see cref="AssemblyFileBuildNumberType">AssemblyFileBuildNumberType</see> and
        ///         <see cref="AssemblyFileBuildNumberFormat">AssemblyFileBuildNumberFormat</see>
        ///         properties to have this value determined automatically at build time.
        ///     </para>
        ///     <para>
        ///         To force the build number to a specific value when using the
        ///         MSBuild.ExtensionPack.VersionNumber.Targets, use the <em>AssemblyFileBuildNumber</em> property,
        ///         and set the <em>AssemblyFileBuildNumberFormat</em> property to
        ///         <see cref="IncrementMethod">DirectSet</see>.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyFileBuildNumber&gt;0&lt;/AssemblyFileBuildNumber&gt;
        /// &lt;AssemblyFileBuildNumberType&gt;DirectSet&lt;/AssemblyFileBuildNumberType&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyFileBuildNumberType"/>
        /// <seealso cref="AssemblyFileBuildNumberFormat"/>
        public string AssemblyFileBuildNumber
        {
            get { return _assemblyFileVersionSettings.BuildNumber; }
            set { _assemblyFileVersionSettings.BuildNumber = value; }
        }

        /// <summary>
        /// The revision of the assembly file.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         To change the assembly file revision set this to the specific revision number you
        ///         want. In most cases you do not want to use this property. Instead, use the
        ///         <see cref="AssemblyFileRevisionType">AssemblyFileRevisionNumberType</see> and
        ///         <see cref="AssemblyFileRevisionFormat">AssemblyFileRevisionNumberFormat</see>
        ///         properties to have this value determined automatically at build time.
        ///     </para>
        ///     <para>
        ///         To force the revision number to a specific value when using the
        ///         MSBuild.ExtensionPack.VersionNumber.Targets, set the <em>AssemblyFileRevision</em> property to
        ///         the value and set the <em>AssemblyFileRevisionFormat</em> property to
        ///         <see cref="IncrementMethod">DirectSet</see>.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyFileRevision&gt;0&lt;/AssemblyFileRevision&gt;
        /// &lt;AssemblyFileRevisionType&gt;DirectSet&lt;/AssemblyFileRevisionType&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyRevisionType"/>
        /// <seealso cref="AssemblyRevisionFormat"/>
        public string AssemblyFileRevision
        {
            get { return _assemblyFileVersionSettings.Revision; }
            set { _assemblyFileVersionSettings.Revision = value; }
        }

        /// <summary>
        /// The complete version of the assembly file.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Use AssemblyFileVersion to directly set the entire file version number with a single
        ///         parameter. For example, if you know you want your version to be "1.2.3.4", you
        ///         can set AssemblyVersion to this instead of having to use each of the individual
        ///         <see cref="AssemblyFileMajorVersion">AssemblyFileMajorVersion</see>,
        ///         <see cref="AssemblyFileMinorVersion">AssemblyFileMinorVersion</see>,
        ///         <see cref="AssemblyFileBuildNumber">AssemblyFileBuildNumber</see>, and
        ///         <see cref="AssemblyFileRevision">AssemblyFileRevision</see> properties.
        ///     </para>
        ///     <para>Note that the other four properties override this one. For example, If you
        ///     set AssemblyFileVersion to "1.2.3.4" and then set AssemblyFileMinorVersion to 6, the resulting
        ///     version will be "1.6.3.4".</para>
        ///     <para>
        ///         This property is an input only. If you want to know what the final version
        ///         generated was, use the
        ///         <see cref="MaxAssemblyFileVersion">MaxAssemblyFileVersion</see> output property
        ///         instead.
        ///     </para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file the best way to specify
        ///     this is to set the <em>AssemblyFileVersion</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyFileVersion&gt;1.2.3.4&lt;/AssemblyFileVersion&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="MaxAssemblyFileVersion"/>
        public string AssemblyFileVersion
        {
            get { return _assemblyFileVersionSettings.Version; }
            set { _assemblyFileVersionSettings.Version = value; }
        }

        /// <summary>
        /// The type of update to use when setting the <see cref="AssemblyFileBuildNumber">AssemblyFileBuildNumber</see> property.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The AssemblyFileBuildNumber can be set using several different methods. The
        ///         AssemblyFileBuildNumberType property is used to select the desired method. The
        ///         supported types are defined in the
        ///         <see cref="IncrementMethod">IncrementMethod</see> enumeration.
        ///     </para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file the default setting is
        ///     DateFormat. To override this set the <em>AssemblyFileBuildNumberType</em>
        ///     property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyFileBuildNumberType&gt;DateFormat&lt;/AssemblyFileBuildNumberType&gt;
        /// &lt;AssemblyFileBuildNumberFormat&gt;yyMMdd&lt;/AssemblyFileBuildNumberFormat&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyFileBuildNumberFormat"/>
        /// <seealso cref="IncrementMethod"/>
        public string AssemblyFileBuildNumberType { get; set; }

        /// <summary>
        /// The type of update to use when setting the <see cref="AssemblyFileRevision">AssemblyFileRevision</see> property.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The AssemblyFileRevision can be set using several different methods. The
        ///         AssemblyFileRevisionType property is used to select the desired method. The
        ///         supported types are defined in the
        ///         <see cref="IncrementMethod">IncrementMethod</see> enumeration.
        ///     </para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file the default setting is
        ///     AutoIncrement. To override this set the <em>AssemblyFileRevisionType</em>
        ///     property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyFileRevisionType&gt;AutoIncrement&lt;/AssemblyFileRevisionType&gt;
        /// &lt;AssemblyFileRevisionFormat&gt;00&lt;/AssemblyFileRevisionFormat&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyFileRevisionFormat"/>
        /// <seealso cref="IncrementMethod"/>
        public string AssemblyFileRevisionType { get; set; }

        /// <summary>
        /// The format string to apply when converting the file build number to a text string.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Use this property to control the formatting of the file build number when it is
        ///         converted from a number to a string. This is particularly useful when used in
        ///         conjunction with the <see cref="IncrementMethod">DateFormat</see> or
        ///         <see cref="IncrementMethod">AutoIncrement</see> methods of setting the file build
        ///         number. Any valid .NET formatting string can be specified.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyFileBuildNumberType&gt;DateFormat&lt;/AssemblyFileBuildNumberType&gt;
        /// &lt;AssemblyFileBuildNumberFormat&gt;yyMMdd&lt;/AssemblyFileBuildNumberFormat&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyFileBuildNumberType"/>
        public string AssemblyFileBuildNumberFormat
        {
            get { return _assemblyFileVersionSettings.BuildNumberFormat; }
            set { _assemblyFileVersionSettings.BuildNumberFormat = value; }
        }

        /// <summary>
        /// The format string to apply when converting the file revision to a text string.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Use this property to control the formatting of the file revision when it is
        ///         converted from a number to a string. This is particularly useful when used in
        ///         conjunction with the <see cref="IncrementMethod">DateFormat</see> or
        ///         <see cref="IncrementMethod">AutoIncrement</see> methods of setting the file
        ///         revision. Any valid .NET formatting string can be specified.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyFileRevisionType&gt;AutoIncrement&lt;/AssemblyFileRevisionType&gt;
        /// &lt;AssemblyFileRevisionFormat&gt;00&lt;/AssemblyFileRevisionFormat&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyBuildNumberType"/>
        public string AssemblyFileRevisionFormat
        {
            get { return _assemblyFileVersionSettings.RevisionFormat; }
            set { _assemblyFileVersionSettings.RevisionFormat = value; }
        }

        /// <summary>Returns the largest assembly file version set by the task.</summary>
        /// <remarks>
        ///     <para>Use this property to find out the largest assembly file version that was generated
        ///     by the task. If only one assemblyinfo.* file was specified as an input, this will
        ///     be the resulting assembly file version for that file. If more than one assemblyinfo.*
        ///     file was specified, this will be the largest build number generated.</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file this value is placed in
        ///     <em>MaxAssemblyFileVersion</em> property after the UpdateAssemblyInfoFiles target is
        ///     run.</para>
        /// </remarks>
        public string MaxAssemblyFileVersion
        {
            get { return _maxAssemblyFileVersion; }
            set { _maxAssemblyFileVersion = value; }
        }

        /// <summary>The title of the assembly.</summary>
        /// <remarks>
        ///     <para>To change the
        ///     <a href="http://msdn.microsoft.com/en-us/library/zf8bbayf(v=vs.100).aspx">
        ///     assembly title</a> set this to the specific title you want. For example, for Visual
        ///     Studio 2005 this is set to "Microsoft� Visual Studio� 2005".</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyTitle</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="MSBuild" title="[New Example]">
        /// &lt;AssemblyTitle&gt;Microsoft� Visual Studio� 2005&lt;/AssemblyTitle&gt;
        ///     </code>
        /// </example>
        public string AssemblyTitle { get; set; }

        /// <summary>The description of the assembly.</summary>
        /// <remarks>
        ///     <para>To change the
        ///     <a href="http://msdn.microsoft.com/en-us/library/05bfs1hb(v=vs.100).aspx">
        ///     assembly description</a> set this to the specific description you want. For
        ///     example, for Visual Studio 2005 this is set to "Microsoft Visual Studio
        ///     2005".</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyDescription</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyDescription&gt;Microsoft Visual Studio 2005&lt;/AssemblyDescription&gt;
        ///     </code>
        /// </example>
        public string AssemblyDescription { get; set; }

        /// <summary>The configuration of the assembly.</summary>
        /// <remarks>
        ///     <para>To change the
        ///     <a href="http://msdn.microsoft.com/en-us/library/zf72c7kz(v=vs.100).aspx">
        ///     assembly configuration text</a> set this to the specific configuration text you
        ///     want.</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyConfiguration</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyConfiguration&gt;Debug&lt;/AssemblyConfiguration&gt;
        ///     </code>
        /// </example>
        public string AssemblyConfiguration { get; set; }

        /// <summary>The company that created the assembly.</summary>
        /// <remarks>
        ///     <para>To change the
        ///     <a href="http://msdn.microsoft.com/en-us/library/bw6s9761(v=vs.100).aspx">
        ///     assembly company</a> set this to the specific company name you want. For example,
        ///     for Visual Studio 2005 this is set to "Microsoft Corporation".</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyCompany</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyCompany&gt;Microsoft Corporation&lt;/AssemblyCompany&gt;
        ///     </code>
        /// </example>
        public string AssemblyCompany { get; set; }

        /// <summary>The product name of the assembly.</summary>
        /// <remarks>
        ///     <para>To change the
        ///     <a href="http://msdn.microsoft.com/en-us/library/system.reflection.assemblyproductattribute.aspx">
        ///     assembly company</a> set this to the specific company name you want. For example,
        ///     for Visual Studio 2005 assemblies this is set to "Microsoft� Visual Studio�
        ///     2005".</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyProduct</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyProduct&gt;Microsoft� Visual Studio� 2005&lt;/AssemblyProduct&gt;
        ///     </code>
        /// </example>
        public string AssemblyProduct { get; set; }

        /// <summary>The copyright information for the assembly.</summary>
        /// <remarks>
        ///     <para>To change the
        ///     <a href="http://msdn.microsoft.com/en-us/library/system.reflection.assemblycopyrightattribute(v=vs.100).aspx">
        ///     assembly copyright</a> set this to the specific copyright text you want. For
        ///     example, for Visual Studio 2005 assemblies this is set to "� Microsoft Corporation.
        ///     All rights reserved.".</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyCopyright</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyCopyright&gt;� Microsoft Corporation. All rights reserved.&lt;/AssemblyCopyright&gt;
        ///     </code>
        /// </example>
        public string AssemblyCopyright { get; set; }

        /// <summary>The trademark information for the assembly.</summary>
        /// <remarks>
        ///     <para>To change the
        ///     <a href="http://msdn.microsoft.com/en-us/library/hk2dbf64(v=vs.100).aspx">
        ///     assembly trademark</a> set this to the specific trademark text you want.</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyTrademark</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyTrademark&gt;Microsoft Corporation&lt;/AssemblyTrademark&gt;
        ///     </code>
        /// </example>
        public string AssemblyTrademark { get; set; }

        /// <summary>The AssemblyInformationalVersion for the assembly.</summary>
        /// <remarks>
        ///     <para>The 
        ///     <a href="http://msdn.microsoft.com/en-us/library/system.reflection.assemblyinformationalversionattribute(v=vs.100).aspx">
        /// AssemblyInformationalVersion </a> attribute attaches additional version information to an assembly. If this attribute is applied to an assembly, the string it specifies can be obtained at run time by using the Application.ProductVersion property. The string is also used in the path and registry key provided by the Application.UserAppDataPath property and the Application.UserAppDataRegistry property. If the AssemblyInformationalVersionAttribute is not applied to an assembly, the version number specified by the AssemblyVersionAttribute attribute is used instead.
        /// Although you can specify any text, a warning message appears on compilation if the string is not in the format used by the assembly version number, or if it is in that format but contains wildcard characters. This warning is harmless. </para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyInformationalVersion&gt;1.2.3.4&lt;/AssemblyInformationalVersion&gt;
        ///     </code>
        /// </example>
        public string AssemblyInformationalVersion { get; set; }

        /// <summary>Set to true to update the AssemblyInformationalVersion.</summary>
        public bool UpdateAssemblyInformationalVersion { get; set; }

        /// <summary>The culture information for the assembly.</summary>
        /// <remarks>
        ///     <para>To change the
        ///     <a href="http://msdn.microsoft.com/en-us/library/de8csy41(v=vs.100).aspx">
        ///     assembly culture</a> set this to the specific culture text you want. For example,
        ///     for the English satellite resources this is set to "en".</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyCulture</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyCulture&gt;en&lt;/AssemblyCulture&gt;
        ///     </code>
        /// </example>
        public string AssemblyCulture { get; set; }

        /// <summary>The GUID for the assembly.</summary>
        /// <remarks>
        ///     <para>To change the
        ///     GUID for the assembly set this to the specific GUID you want.</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyGuid</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyGuid&gt;56269a04-c55a-4c5a-92ba-dfdb569bc708&lt;/AssemblyGuid&gt;
        ///     </code>
        /// </example>
        public string Guid { get; set; }

        /// <summary>Controls whether assembly signing information is replaced in the AssemblyInfo files.</summary>
        /// <remarks>
        ///     <para>
        ///         This property controls whether the
        ///         <see cref="AssemblyDelaySign">AssemblyDelaySign</see>,
        ///         <see cref="AssemblyKeyFile">AssemblyKeyFile</see> and
        ///         <see cref="AssemblyKeyName">AssemblyKeyName</see> properties are written out to
        ///         the assembly info files. In order for either of those three properties to
        ///         persist, AssemblyIncludeSigningInformation must be set to true.
        ///     </para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyIncludeSigningInformation</em> property. By default this is set to
        ///     <em>false</em>.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyIncludeSigningInformation&gt;true&lt;/AssemblyIncludeSigningInformation&gt;
        /// &lt;AssemblyDelaySign&gt;true&lt;/AssemblyDelaySign&gt;
        ///     </code>
        /// </example>
        public bool AssemblyIncludeSigningInformation { get; set; }

        /// <summary>Controls delay signing of the assembly.</summary>
        /// <remarks>
        ///     <para>To enable delay signing of the assembly set this property to "true".</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyDelaySign</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyDelaySign&gt;false&lt;/AssemblyDelaySign&gt;
        ///     </code>
        /// </example>
        public string AssemblyDelaySign { get; set; }

        /// <summary>Specifies the key file used to sign the assembly.</summary>
        /// <remarks>
        ///     <para>To specify the key file used to sign the compiled assembly set this to the file name of the key file.</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyKeyFile</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyKeyFile&gt;c:\key.snk&lt;/AssemblyKeyFile&gt;
        ///     </code>
        /// </example>
        public string AssemblyKeyFile { get; set; }

        /// <summary>Specifies the name of a key container within the CSP containing the key pair used to generate a strong name.</summary>
        /// <remarks>
        ///     <para>To specify the key used to sign the compiled assembly set this to the name of the key container.</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyKeyName</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyKeyName&gt;myContainer&lt;/AssemblyKeyName&gt;
        ///     </code>
        /// </example>
        public string AssemblyKeyName { get; set; }

        /// <summary>Specifies whether the assembly is visible to COM.</summary>
        /// <remarks>
        ///     <para>
        ///         To specify whether the assembly shoul be visible to COM set this to true and
        ///         provide a valid GUID using the <see cref="Guid">Guid</see>
        ///         property. The default value is <em>null</em>.
        ///     </para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file set this using the
        ///     <em>AssemblyComVisible</em> property.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;AssemblyComVisible&gt;myContainer&lt;/AssemblyComVisible&gt;
        ///     </code>
        /// </example>
        public string ComVisible { get; set; }

        /// <summary>
        /// Specifies the list of AssemblyInfo files the task should update.
        /// </summary>
        /// <remarks>
        ///     <para>Use the AssemblyInfoFile property to provide the task with the list of AssemblyInfo files that should
        /// be updated by the task. This can be a mix of VB, C# and J# AssemblyInfo Files.</para>
        ///     <para>When using the MSBuild.ExtensionPack.VersionNumber.Targets file add items to the AssemblyInfoFiles item group
        /// to have them processed by the task.</para>
        /// </remarks>
        /// <example>
        ///     <code lang="xml">
        /// &lt;!-- Add all AssemblyInfo files in all sub-directories to the list of
        /// files that should be processed by the task --&gt;
        /// &lt;ItemGroup&gt;
        ///     &lt;AssemblyInfoFiles&gt;**\AssemblyInfo.*&lt;/AssemblyInfoFiles&gt;
        /// &lt;/ItemGroup&gt;
        ///     </code>
        /// </example>
        public string[] AssemblyInfoFiles { get; set; }

        /// <summary>
        /// Set to true to use UTC Date / Time in calculations. Default is false.
        /// </summary>
        public bool UseUtc { get; set; }

        /// <summary>
        /// Set the first day of the week for IncrementMethod.YearWeekDay. Defaults to Monday
        /// </summary>
        public string FirstDayOfWeek
        {
            get { return _firstDayOfWeek; }
            set { _firstDayOfWeek = value; }
        }

        /// <summary>
        /// Sets the number of padding digits to use, e.g. 4
        /// </summary>
        public int PaddingCount { get; set; }

        /// <summary>
        /// Sets the padding digit to use, e.g. 0
        /// </summary>
        public char PaddingDigit { get; set; }

        /// <summary>
        /// Sets the start date to use when using IncrementMethod.ElapsedDays
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The encoding to write the new file in. The default is UTF8
        /// </summary>
        public string TextEncoding { get; set; }

        /// <summary>
        /// Executes the AssemblyInfo task.
        /// </summary>
        /// <returns>True if the task was run sucecssfully. False if the task failed.</returns>
        public bool Execute()
        {
            FileInfo writerInfo = null;

            // Try and parse all the increment properties to ensure they are valid for the increment enum. If not,
            // bail out.
            if (!ParseIncrementProperties())
            {
                return false;
            }

            // Validate that the enum values set match with what was passed into the associated parameters. If not,
            // bail out.
            if (!ValidateIncrementProperties())
            {
                return false;
            }

            // Set the max versions before running through the loop
            MaxAssemblyVersion = "0.0.0.0";
            MaxAssemblyFileVersion = "0.0.0.0";

            foreach (var item in AssemblyInfoFiles)
            {
                if (!File.Exists(item))
                {
                    //Log.LogError(string.Format(CultureInfo.CurrentUICulture, "File not found: {0}", item.ItemSpec));
                    return false;
                }

                var assemblyInfo = new AssemblyInfoWrapper(item);

                // Validate that stub file entries exist for any of the properties we've been asked to set.
                if (!ValidateFileEntries(assemblyInfo, item))
                {
                    return false;
                }

                //this.Log.LogMessage(MessageImportance.Low, "Updating assembly info for {0}", item);
                if (!SkipVersioning)
                {
                    Version versionToUpdate;
                    try
                    {
                        versionToUpdate = new Version(assemblyInfo["AssemblyVersion"], true);
                    }
                    catch (Exception)
                    {
                        //Log.LogError(string.Format(CultureInfo.CurrentUICulture,"Unable to read current AssemblyVersion from file {0}: {1}",item, ex.Message));
                        return false;
                    }

                    UpdateAssemblyVersion(versionToUpdate, _assemblyVersionSettings);
                    assemblyInfo["AssemblyVersion"] = versionToUpdate.ToString();
                    if (UpdateAssemblyInformationalVersion)
                    {
                        if (ValidateFileEntry("AssemblyInformationalVersion", assemblyInfo, "AssemblyInformationalVersion", item))
                        {
                            if (string.IsNullOrEmpty(AssemblyInformationalVersion))
                            {
                                assemblyInfo["AssemblyInformationalVersion"] = versionToUpdate.ToString();
                            }
                            else
                            {
                                assemblyInfo["AssemblyInformationalVersion"] = AssemblyInformationalVersion;
                            }
                        }
                    }

                    UpdateMaxVersion(ref _maxAssemblyVersion, assemblyInfo["AssemblyVersion"]);
                    try
                    {
                        versionToUpdate = new Version(assemblyInfo["AssemblyFileVersion"]);
                        UpdateAssemblyVersion(versionToUpdate, _assemblyFileVersionSettings);
                        assemblyInfo["AssemblyFileVersion"] = versionToUpdate.ToString();
                        UpdateMaxVersion(ref _maxAssemblyFileVersion, assemblyInfo["AssemblyFileVersion"]);
                    }
                    catch (ArgumentException)
                    {
                        //Log.LogWarning(string.Format(CultureInfo.CurrentUICulture,"File {0} contains a verbatim AssemblyFileVersion - skipping",item));
                    }
                }

                UpdateProperty(assemblyInfo, "AssemblyTitle");
                UpdateProperty(assemblyInfo, "AssemblyDescription");
                UpdateProperty(assemblyInfo, "AssemblyConfiguration");
                UpdateProperty(assemblyInfo, "AssemblyCompany");
                UpdateProperty(assemblyInfo, "AssemblyProduct");
                UpdateProperty(assemblyInfo, "AssemblyCopyright");
                UpdateProperty(assemblyInfo, "AssemblyTrademark");
                UpdateProperty(assemblyInfo, "AssemblyCulture");
                UpdateProperty(assemblyInfo, "Guid");
                if (AssemblyIncludeSigningInformation)
                {
                    UpdateProperty(assemblyInfo, "AssemblyKeyName");
                    UpdateProperty(assemblyInfo, "AssemblyKeyFile");
                    UpdateProperty(assemblyInfo, "AssemblyDelaySign");
                }

                UpdateProperty(assemblyInfo, "ComVisible");

                try
                {
                    writerInfo = GetTemporaryFileInfo();

                    if (!string.IsNullOrEmpty(TextEncoding))
                    {
                        try
                        {
                            _fileEncoding = GetTextEncoding(TextEncoding);
                        }
                        catch (ArgumentException)
                        {
                            //Log.LogError(string.Format(CultureInfo.CurrentCulture,"Error, {0} is not a supported encoding name.", this.TextEncoding));
                            return false;
                        }
                    }

                    using (var writer = new StreamWriter(writerInfo.OpenWrite(), _fileEncoding))
                    {
                        assemblyInfo.Write(writer);
                    }

                    var changedAttribute = false;

                    // First make sure the file is writable.
                    var fileAttributes = File.GetAttributes(item);

                    // If readonly attribute is set, reset it.
                    if ((fileAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        //Log.LogMessage(MessageImportance.Low, "Making file writable");
                        File.SetAttributes(item, fileAttributes ^ FileAttributes.ReadOnly);
                        changedAttribute = true;
                    }

                    File.Copy(writerInfo.FullName, item, true);

                    if (changedAttribute)
                    {
                        //Log.LogMessage(MessageImportance.Low, "Making file readonly");
                        File.SetAttributes(item, FileAttributes.ReadOnly);
                    }
                }
                finally
                {
                    if (writerInfo != null)
                    {
                        writerInfo.Delete();
                    }
                }
            }

            return true;
        }

        private static Encoding GetTextEncoding(string enc)
        {
            switch (enc)
            {
                case "DEFAULT":
                    return Encoding.Default;
                case "ASCII":
                    return Encoding.ASCII;
                case "Unicode":
                    return Encoding.Unicode;
                case "UTF7":
                    return Encoding.UTF7;
                case "UTF8":
                    return Encoding.UTF8;
                case "UTF32":
                    return Encoding.UTF32;
                case "BigEndianUnicode":
                    return Encoding.BigEndianUnicode;
                default:
                    if (!string.IsNullOrEmpty(enc))
                    {
                        return Encoding.GetEncoding(enc);
                    }

                    return null;
            }
        }

        private static void UpdateMaxVersion(ref string maxVersion, string newVersion)
        {
            if (newVersion == null)
            {
                return;
            }

            var max = new System.Version(maxVersion);
            var candidate = new System.Version(newVersion);

            if (candidate > max)
            {
                maxVersion = newVersion;
            }
        }

        private void UpdateAssemblyVersion(Version versionToUpdate, AssemblyVersionSettings requestedVersion)
        {
            // The string version of the assembly goes first, so the others can override it.
            if (requestedVersion.Version != null)
            {
                //this.Log.LogMessage(MessageImportance.Low, "\tUpdating assembly version to {0}",requestedVersion.Version);
                versionToUpdate.VersionString = requestedVersion.Version;
            }

            if (requestedVersion.MajorVersion != null)
            {
                //this.Log.LogMessage(MessageImportance.Low, "\tUpdating major version to {0}",requestedVersion.MajorVersion);
                versionToUpdate.MajorVersion = requestedVersion.MajorVersion;
            }

            if (requestedVersion.MinorVersion != null)
            {
                //this.Log.LogMessage(MessageImportance.Low, "\tUpdating minor version to {0}",requestedVersion.MinorVersion);
                versionToUpdate.MinorVersion = requestedVersion.MinorVersion;
            }

            // The BuildNumber and Revision updates are closely related when the BuildNumber updates daily and
            // the Revision updates on every build. It's important to ensure that the Revision resets to 0
            // when the BuildNumber flips across to a new day.
            var originalBuildNumber = string.Empty;
            var handleSpecialInteraction = ((requestedVersion.BuildNumberType == IncrementMethod.DateString) ||
                                             (requestedVersion.BuildNumberType == IncrementMethod.Julian)) &&
                                            (requestedVersion.RevisionType == IncrementMethod.AutoIncrement);
            handleSpecialInteraction = handleSpecialInteraction && requestedVersion.RevisionReset;

            if (handleSpecialInteraction)
            {
                originalBuildNumber = versionToUpdate.BuildNumber;
            }

            // Go ahead and update the BuildNumber. After this is done we'll see if it's different than it
            // was when we started, and then handle the Revision as necessary.
            versionToUpdate.BuildNumber = UpdateVersionProperty(versionToUpdate.BuildNumber,
                                                                     requestedVersion.BuildNumberType,
                                                                     requestedVersion.BuildNumber,
                                                                     requestedVersion.BuildNumberFormat,
                                                                     "\tUpdating build number to {0}");

            // If we're in the special situation of DateString for BuildNumber and AutoIncrement for Revision
            // check and see if the BuildNumber changed, indicating we're on a new day. If so tweak the
            // Revision so when the AutoIncrement on it happens the value will become 0.
            if (handleSpecialInteraction && (originalBuildNumber != versionToUpdate.BuildNumber))
            {
                versionToUpdate.Revision = "-1";
            }

            versionToUpdate.Revision = UpdateVersionProperty(versionToUpdate.Revision,
                                                                  requestedVersion.RevisionType,
                                                                  requestedVersion.Revision,
                                                                  requestedVersion.RevisionFormat,
                                                                  "\tUpdating revision number to {0}");
            //this.Log.LogMessage(MessageImportance.Low, "\tFinal assembly version is {0}", versionToUpdate.ToString());
        }

        private void UpdateProperty(AssemblyInfoWrapper assemblyInfo, string propertyName)
        {
            var propInfo = GetType().GetProperty(propertyName);
            var value = (string)propInfo.GetValue(this, null);

            if (value != null)
            {
                assemblyInfo[propertyName] = value;
                //this.Log.LogMessage(MessageImportance.Low, "\tUpdating {0} to \"{1}\"", propertyName, value);
            }
        }

        private string UpdateVersionProperty(string versionNumber, IncrementMethod method, string value, string format, string logMessage)
        {
            //this.Log.LogMessage(MessageImportance.Low, "\tUpdate method is {0}", method.ToString());
            if (string.IsNullOrEmpty(format))
            {
                format = "0";
            }

            switch (method)
            {
                case IncrementMethod.NoIncrement:
                    if (value == null)
                    {
                        return versionNumber;
                    }

                    //this.Log.LogMessage(MessageImportance.Low, logMessage, value);
                    return value;
                case IncrementMethod.AutoIncrement:
                    var newVersionNumber = int.Parse(versionNumber, CultureInfo.InvariantCulture);
                    newVersionNumber++;
                    //this.Log.LogMessage(MessageImportance.Low, logMessage,newVersionNumber.ToString(format, CultureInfo.InvariantCulture));
                    return newVersionNumber.ToString(format, CultureInfo.InvariantCulture);
                case IncrementMethod.DateString:
                    var newVersionNumber1 = UseUtc
                                                   ? DateTime.UtcNow.ToString(format, CultureInfo.InvariantCulture)
                                                   : DateTime.Now.ToString(format, CultureInfo.InvariantCulture);
                    //this.Log.LogMessage(MessageImportance.Low, logMessage, newVersionNumber1);
                    return newVersionNumber1;
                case IncrementMethod.Julian:
                    var newVersionNumber2 = UseUtc
                                                   ? DateTime.UtcNow.ToString(format, CultureInfo.InvariantCulture)
                                                   : DateTime.Now.ToString("yy", CultureInfo.InvariantCulture);
                    newVersionNumber2 += DateTime.Now.DayOfYear.ToString("000", CultureInfo.InvariantCulture);
                    //this.Log.LogMessage(MessageImportance.Low, logMessage, newVersionNumber2);
                    return newVersionNumber2;
                case IncrementMethod.YearWeekDay:
                    var now = UseUtc ? DateTime.UtcNow : DateTime.Now;
                    var newVersionNumber3 = now.ToString("yy", CultureInfo.InvariantCulture);
                    newVersionNumber3 +=
                        CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(now, CalendarWeekRule.FirstDay, (DayOfWeek)Enum.Parse(typeof(DayOfWeek), FirstDayOfWeek))
                                   .ToString("D2", CultureInfo.InvariantCulture);
                    newVersionNumber3 += ((int)now.DayOfWeek).ToString(CultureInfo.InvariantCulture);
                    //Log.LogMessage(MessageImportance.Low, logMessage, newVersionNumber3);
                    return newVersionNumber3;
                case IncrementMethod.ElapsedDays:
                    var now2 = UseUtc ? DateTime.UtcNow : DateTime.Now;
                    var elapsed = now2 - Convert.ToDateTime(StartDate);
                    return elapsed.Days.ToString(CultureInfo.CurrentCulture).PadLeft(PaddingCount, PaddingDigit);
                default:
                    return string.Empty;
            }
        }

        private FileInfo GetTemporaryFileInfo()
        {
            FileInfo myFileInfo;
            try
            {
                var tempFileName = Path.GetTempFileName();
                myFileInfo = new FileInfo(tempFileName) { Attributes = FileAttributes.Temporary };
            }
            catch (Exception e)
            {
                //this.Log.LogError("Unable to create temporary file: {0}", e.Message);
                return null;
            }

            return myFileInfo;
        }

        // This converts all the string properties to one of the valid enum values. If any of them fail it logs an error to the console and
        // returns false.
        private bool ParseIncrementProperties()
        {
            var enumNames = string.Join(", ", Enum.GetNames(typeof(IncrementMethod)));

            // Handle AssemblyBuildNumberType
            if (AssemblyBuildNumberType == null)
            {
                _assemblyVersionSettings.BuildNumberType = IncrementMethod.NoIncrement;
            }
            else
            {
                if (!Enum.IsDefined(typeof(IncrementMethod), AssemblyBuildNumberType))
                {
                    //this.Log.LogError("The value specified for AssemblyBuildNumberType is invalid. It must be one of: {0}", enumNames);

                    return false;
                }

                _assemblyVersionSettings.BuildNumberType = (IncrementMethod)Enum.Parse(typeof(IncrementMethod), AssemblyBuildNumberType);
            }

            // Handle AssemblyRevisionNumberType
            if (AssemblyRevisionType == null)
            {
                _assemblyVersionSettings.RevisionType = IncrementMethod.NoIncrement;
            }
            else
            {
                if (!Enum.IsDefined(typeof(IncrementMethod), AssemblyRevisionType))
                {
                    //this.Log.LogError("The value specified for AssemblyRevisionType is invalid. It must be one of: {0}", enumNames);

                    return false;
                }

                _assemblyVersionSettings.RevisionType = (IncrementMethod)Enum.Parse(typeof(IncrementMethod), AssemblyRevisionType);
            }

            // Handle AssemblyRevisionRevisionReset
            if (AssemblyRevisionReset == null)
            {
                _assemblyVersionSettings.RevisionReset = true;
            }
            else
            {
                if (!bool.TryParse(AssemblyRevisionReset, out _assemblyVersionSettings.RevisionReset))
                {
                    //this.Log.LogError("The value specified for AssemblyRevisionReset is invalid. It must be a string representation of a boolean value");

                    return false;
                }
            }

            // Handle AssemblyFileBuildNumberType
            if (AssemblyFileBuildNumberType == null)
            {
                _assemblyFileVersionSettings.BuildNumberType = IncrementMethod.NoIncrement;
            }
            else
            {
                if (!Enum.IsDefined(typeof(IncrementMethod), AssemblyFileBuildNumberType))
                {
                    //this.Log.LogError("The value specified for AssemblyFileBuildNumberType is invalid. It must be one of: {0}",enumNames);

                    return false;
                }

                _assemblyFileVersionSettings.BuildNumberType = (IncrementMethod)Enum.Parse(typeof(IncrementMethod), AssemblyFileBuildNumberType);
            }

            // Handle AssemblyFileRevisionReset
            if (AssemblyFileRevisionReset == null)
            {
                _assemblyFileVersionSettings.RevisionReset = true;
            }
            else
            {
                if (!bool.TryParse(AssemblyFileRevisionReset, out _assemblyFileVersionSettings.RevisionReset))
                {
                    //this.Log.LogError("The value specified for AssemblyFileRevisionReset is invalid. It must be a string representation of a boolean value");

                    return false;
                }
            }

            // Handle AssemblyFileRevisionType
            if (AssemblyFileRevisionType == null)
            {
                _assemblyFileVersionSettings.RevisionType = IncrementMethod.NoIncrement;
            }
            else
            {
                if (!Enum.IsDefined(typeof(IncrementMethod), AssemblyFileRevisionType))
                {
                    //this.Log.LogError("The value specified for AssemblyFileRevisionType is invalid. It must be one of: {0}", enumNames);

                    return false;
                }

                _assemblyFileVersionSettings.RevisionType = (IncrementMethod)Enum.Parse(typeof(IncrementMethod), AssemblyFileRevisionType);
            }

            return true;
        }

        private bool ValidateIncrementProperties()
        {
            if ((_assemblyVersionSettings.BuildNumberType == IncrementMethod.DateString) &&
                (_assemblyVersionSettings.BuildNumberFormat == null))
            {
                //this.Log.LogError("The version increment method for AssemblyBuildNumber was set to DateString, but AssemblyBuildNumberFormat was not specified. Both properties must be set to use a date string as a build number.");
                return false;
            }

            if ((_assemblyVersionSettings.RevisionType == IncrementMethod.DateString) &&
                (_assemblyVersionSettings.RevisionFormat == null))
            {
                //this.Log.LogError("The version increment method for AssemblyRevision was set to DateString, but AssemblyRevisionFormat was not specified. Both properties must be set to use a date string as a revision.");
                return false;
            }

            if ((_assemblyFileVersionSettings.BuildNumberType == IncrementMethod.DateString) &&
                (AssemblyFileBuildNumberFormat == null))
            {
                //this.Log.LogError("The version increment method for AssemblyFileBuildNumber was set to DateString, but AssemblyFileBuildNumberFormat was not specified. Both properties must be set to use a date string as a build number.");
                return false;
            }

            if ((_assemblyFileVersionSettings.RevisionType == IncrementMethod.DateString) &&
                (AssemblyFileRevisionFormat == null))
            {
                //this.Log.LogError("The version increment method for AssemblyFileRevision was set to DateString, but AssemblyFileRevisionFormat was not specified. Both properties must be set to use a date string as a revision.");
                return false;
            }

            return true;
        }

        // There's an inherent limitation to this task in that it can only replace content for attributes
        // already present in the assemblyinfo file. If the stub isn't there then it can't be set. This method
        // goes through and validates that a stub is present in the file for any of the properties that were set
        // on the task.
        private bool ValidateFileEntries(AssemblyInfoWrapper assemblyInfo, string fileName)
        {
            if (((AssemblyBuildNumber != null) ||
                 (AssemblyRevision != null) ||
                 (AssemblyMajorVersion != null) ||
                 (AssemblyMinorVersion != null)) &&
                (assemblyInfo["AssemblyVersion"] == null))
            {
                //this.Log.LogError("Unable to update the AssemblyVersion for {0}: No stub entry for AssemblyVersion was found in the AssemblyInfo file.",fileName);
                return false;
            }

            if (((AssemblyFileBuildNumber != null) ||
                 (AssemblyFileRevision != null) ||
                 (AssemblyFileMajorVersion != null) ||
                 (AssemblyFileMinorVersion != null)) &&
                (assemblyInfo["AssemblyFileVersion"] == null))
            {
                //this.Log.LogError("Unable to update the AssemblyFileVersion for {0}: No stub entry for AssemblyFileVersion was found in the AssemblyInfo file.",fileName);
                return false;
            }

            if (!ValidateFileEntry(AssemblyCompany, assemblyInfo, "AssemblyCompany", fileName))
            {
                return false;
            }

            if (!ValidateFileEntry(AssemblyConfiguration, assemblyInfo, "AssemblyConfiguration", fileName))
            {
                return false;
            }

            if (!ValidateFileEntry(AssemblyCopyright, assemblyInfo, "AssemblyCopyright", fileName))
            {
                return false;
            }

            if (!ValidateFileEntry(AssemblyCulture, assemblyInfo, "AssemblyCulture", fileName))
            {
                return false;
            }

            if (!ValidateFileEntry(AssemblyDescription, assemblyInfo, "AssemblyDescription", fileName))
            {
                return false;
            }

            if (!ValidateFileEntry(AssemblyProduct, assemblyInfo, "AssemblyProduct", fileName))
            {
                return false;
            }

            if (!ValidateFileEntry(AssemblyTitle, assemblyInfo, "AssemblyTitle", fileName))
            {
                return false;
            }

            if (!ValidateFileEntry(AssemblyTrademark, assemblyInfo, "AssemblyTrademark", fileName))
            {
                return false;
            }

            if (AssemblyIncludeSigningInformation)
            {
                if (!ValidateFileEntry(AssemblyDelaySign, assemblyInfo, "AssemblyDelaySign", fileName))
                {
                    return false;
                }

                if (!ValidateFileEntry(AssemblyKeyFile, assemblyInfo, "AssemblyKeyFile", fileName))
                {
                    return false;
                }

                if (!ValidateFileEntry(AssemblyKeyName, assemblyInfo, "AssemblyKeyName", fileName))
                {
                    return false;
                }
            }

            return true;
        }

        // This validates a single attribute in the file given the value passed into the task, and the file attribute to look up.
        // The filename is only used for making the error message pretty.
        private bool ValidateFileEntry(string taskAttributeValue, AssemblyInfoWrapper assemblyInfo, string fileAttribute, string fileName)
        {
            if ((taskAttributeValue != null) && (assemblyInfo[fileAttribute] == null))
            {
                //this.Log.LogError("Unable to update the {0} for {1}: No stub entry for {0} was found in the AssemblyInfo file.",fileAttribute, fileName);
                return false;
            }

            return true;
        }

        private struct AssemblyVersionSettings
        {
            public string BuildNumber;
            public string BuildNumberFormat;
            public IncrementMethod BuildNumberType;
            public string MajorVersion;
            public string MinorVersion;
            public string Revision;
            public string RevisionFormat;
            public IncrementMethod RevisionType;
            public bool RevisionReset;
            public string Version;
        }
    }
}

namespace MSBuild.ExtensionPack.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text.RegularExpressions;

    internal class AssemblyInfoWrapper
    {
        private readonly Regex _attributeBooleanValuePattern = new Regex(@"\((?<attributeValue>([tT]rue|[fF]alse))\)", RegexOptions.Compiled);
        private readonly Dictionary<string, int> _attributeIndex = new Dictionary<string, int>();
        private readonly Regex _attributeNamePattern = new Regex(@"[aA]ssembly:?\s*(?<attributeName>\w+)\s*\(", RegexOptions.Compiled);
        private readonly Regex _attributeStringValuePattern = new Regex(@"""(?<attributeValue>.*?)""", RegexOptions.Compiled);
        private readonly Regex _multilineCSharpCommentEndPattern = new Regex(@".*?\*/", RegexOptions.Compiled);
        private readonly Regex _multilineCSharpCommentStartPattern = new Regex(@"\s*/\*^\*", RegexOptions.Compiled);
        private readonly List<string> _rawFileLines = new List<string>();
        private readonly Regex _singleLineCSharpCommentPattern = new Regex(@"(?m:^(\s*//.*)$)", RegexOptions.Compiled);
        private readonly Regex _singleLineVbCommentPattern = new Regex(@"\s*'", RegexOptions.Compiled);

        //// The ^\* is so the regex works with J# files that use /** to indicate the actual attribute lines.
        //// This does mean that lines like /** in C# will get treated as valid lines, but that's a real borderline case.
        public AssemblyInfoWrapper(string fileName)
        {
            using (var reader = File.OpenText(fileName))
            {
                var lineNumber = 0;
                string input;
                var skipLine = false;

                while ((input = reader.ReadLine()) != null)
                {
                    _rawFileLines.Add(input);

                    // Skip single comment lines
                    if (_singleLineCSharpCommentPattern.IsMatch(input) || _singleLineVbCommentPattern.IsMatch(input))
                    {
                        lineNumber++;
                        continue;
                    }

                    // Skip multi-line C# comments
                    if (_multilineCSharpCommentStartPattern.IsMatch(input))
                    {
                        lineNumber++;
                        skipLine = true;
                        continue;
                    }

                    // Stop skipping when we're at the end of a C# multiline comment
                    if (_multilineCSharpCommentEndPattern.IsMatch(input) && skipLine)
                    {
                        lineNumber++;
                        skipLine = false;
                        continue;
                    }

                    // If we're in the middle of a multiline comment, keep going
                    if (skipLine)
                    {
                        lineNumber++;
                        continue;
                    }

                    // Check to see if the current line is an attribute on the assembly info.
                    // If so we need to keep the line number in our dictionary so we can go
                    // back later and get it when this class is accessed through its indexer.
                    var matches = _attributeNamePattern.Matches(input);
                    if (matches.Count > 0)
                    {
                        if (_attributeIndex.ContainsKey(matches[0].Groups["attributeName"].Value) == false)
                        {
                            _attributeIndex.Add(matches[0].Groups["attributeName"].Value, lineNumber);
                        }
                    }

                    lineNumber++;
                }
            }
        }

        public string this[string attribute]
        {
            get
            {
                if (!_attributeIndex.ContainsKey(attribute))
                {
                    return null;
                }

                // Try to match string properties first
                var matches = _attributeStringValuePattern.Matches(_rawFileLines[_attributeIndex[attribute]]);
                if (matches.Count > 0)
                {
                    return matches[0].Groups["attributeValue"].Value;
                }

                // If that fails, try to match a boolean value
                matches = _attributeBooleanValuePattern.Matches(_rawFileLines[_attributeIndex[attribute]]);
                if (matches.Count > 0)
                {
                    return matches[0].Groups["attributeValue"].Value;
                }

                return null;
            }

            set
            {
                // The set case requires fancy footwork. In this case we actually replace the attribute
                // value in the string using a regex to the value that was passed in.
                if (!_attributeIndex.ContainsKey(attribute))
                {
                    throw new ArgumentOutOfRangeException("attribute", string.Format(CultureInfo.CurrentUICulture, "{0} is not an attribute in the specified AssemblyInfo.cs file", attribute));
                }

                // Try setting it as a string property first
                var matches = _attributeStringValuePattern.Matches(_rawFileLines[_attributeIndex[attribute]]);
                if (matches.Count > 0)
                {
                    _rawFileLines[_attributeIndex[attribute]] = _attributeStringValuePattern.Replace(_rawFileLines[_attributeIndex[attribute]], "\"" + value + "\"");
                    return;
                }

                // If that fails try setting it as a boolean property
                matches = _attributeBooleanValuePattern.Matches(_rawFileLines[_attributeIndex[attribute]]);
                if (matches.Count > 0)
                {
                    _rawFileLines[_attributeIndex[attribute]] = _attributeBooleanValuePattern.Replace(_rawFileLines[_attributeIndex[attribute]], "(" + value + ")");
                }
            }
        }

        public void Write(TextWriter streamWriter)
        {
            foreach (var line in _rawFileLines)
            {
                streamWriter.WriteLine(line);
            }
        }
    }
}

namespace MSBuild.ExtensionPack.Framework
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    internal class Version
    {
        private string _versionString;

        public Version()
        {
            MajorVersion = "1";
            MinorVersion = "0";
            BuildNumber = "0";
            Revision = "0";
        }

        public Version(string version)
            : this(version, false)
        {
        }

        public Version(string version, bool isAssemblyVersion)
        {
            if (isAssemblyVersion)
            {
                ParseAssemblyVersion(version);
            }
            else
            {
                ParseVersion(version);
            }
        }

        public string VersionString
        {
            get { return _versionString; }
            set { ParseVersion(value); }
        }

        public string MajorVersion { get; set; }

        public string MinorVersion { get; set; }

        public string BuildNumber { get; set; }

        public string Revision { get; set; }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}.{3}", MajorVersion, MinorVersion, BuildNumber, Revision);
        }

        private static string ValidateAssemblyVersionPart(string part)
        {
            return string.IsNullOrEmpty(part) || part == "*" ? "0" : part;
        }

        private void ParseAssemblyVersion(string version)
        {
            var versionPattern = new Regex(@"(?<majorVersion>(\d+))(\.(?<minorVersion>(\d+)))(\.(?<buildNumber>(\d+|\*)))?(\.(?<revision>(\d+|\*)))?", RegexOptions.Compiled);
            var matches = versionPattern.Matches(version);
            if (matches.Count != 1)
            {
                throw new ArgumentException("The specified string \"" + version + "\" is not a valid AssemblyVersion number", "version");
            }

            MajorVersion = matches[0].Groups["majorVersion"].Value;
            MinorVersion = matches[0].Groups["minorVersion"].Value;
            BuildNumber = ValidateAssemblyVersionPart(matches[0].Groups["buildNumber"].Value);
            Revision = ValidateAssemblyVersionPart(matches[0].Groups["revision"].Value);
            _versionString = version;
        }

        private void ParseVersion(string version)
        {
            var versionPattern = new Regex(@"(?<majorVersion>(\d+))(\.(?<minorVersion>(\d+)))(\.(?<buildNumber>(\d+)))(\.(?<revision>(\d+)))", RegexOptions.Compiled);
            var matches = versionPattern.Matches(version);
            if (matches.Count != 1)
            {
                throw new ArgumentException("The specified string \"" + version + "\" is not a valid version number", "version");
            }

            MajorVersion = matches[0].Groups["majorVersion"].Value;
            MinorVersion = matches[0].Groups["minorVersion"].Value;
            BuildNumber = matches[0].Groups["buildNumber"].Value;
            Revision = matches[0].Groups["revision"].Value;
            _versionString = version; // Very important that this is a little v, not big v, otherwise you get infinite recursion!
        }
    }
}