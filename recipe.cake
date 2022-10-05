#load nuget:?package=Cake.Recipe&version=3.0.0

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context, 
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "TfsUrlParser",
    repositoryOwner: "bbtsoftware",
    repositoryName: "TfsUrlParser",
    appVeyorAccountName: "BBTSoftwareAG",
    shouldRunCodecov: true,
    shouldGenerateDocumentation: false);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    //dupFinderExcludePattern: new string[] { BuildParameters.RootDirectoryPath + "/src/TfsUrlParser.Tests/*.cs" },
    testCoverageFilter: "+[*]* -[xunit.*]* -[*.Tests]* -[Shouldly]* -[DiffEngine]* -[EmptyFiles]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Build.RunDotNetCore();
