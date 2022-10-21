# Azure DevOps URL parser

This NuGet package contains code for parsing Azure DevOps and Azure DevOps Server URLs.

[![License](http://img.shields.io/:license-mit-blue.svg)](https://github.com/bbtsoftware/TfsUrlParser/blob/master/LICENSE)

## Information

| | Stable | Pre-release |
|:--:|:--:|:--:|
|GitHub Release|-|[![GitHub release](https://img.shields.io/github/release/bbtsoftware/TfsUrlParser.svg)](https://github.com/bbtsoftware/TfsUrlParser/releases/latest)|
|NuGet|[![NuGet](https://img.shields.io/nuget/v/TfsUrlParser.svg)](https://www.nuget.org/packages/TfsUrlParser)|[![NuGet](https://img.shields.io/nuget/vpre/TfsUrlParser.svg)](https://www.nuget.org/packages/TfsUrlParser)|

## Build Status

|Develop|Master|
|:--:|:--:|
|[![Build status](https://github.com/bbtbir/tfsurlparser/actions/workflows/main.yml/badge.svg?branch=develop)](https://github.com/bbtbir/TfsUrlParser/actions/workflows/main.yml?query=branch%3Adevelop)|[![Build status](https://github.com/bbtbir/tfsurlparser/actions/workflows/main.yml/badge.svg?branch=master)](https://github.com/bbtbir/TfsUrlParser/actions/workflows/main.yml?query=branch%3Amaster)|

## Code Coverage

[![Coverage Status](https://coveralls.io/repos/github/BBTSoftwareAG/tfs-url-parser/badge.svg?branch=develop)](https://coveralls.io/github/BBTSoftwareAG/tfs-url-parser?branch=develop)

## Repository Description

To use the Azure DevOps URL parser you need to add the [TfsUrlParser NuGet package](https://www.nuget.org/packages/TfsUrlParser/).

```csharp
    var repositoryDescription =
        new RepositoryDescription(
            "http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository");

    Console.WriteLine(repositoryDescription.ServerUrl);
    Console.WriteLine(repositoryDescription.CollectionUrl);
    Console.WriteLine(repositoryDescription.CollectionName);
    Console.WriteLine(repositoryDescription.ProjectName);
    Console.WriteLine(repositoryDescription.RepositoryUrl);
    Console.WriteLine(repositoryDescription.RepositoryName);
```

## Build

`dotnet build .\src\TfsUrlParser.sln`
