# Azure DevOps URL parser
[![NuGet](https://img.shields.io/nuget/v/TfsUrlParser.svg)](https://www.nuget.org/packages/TfsUrlParser)
[![Build status](https://github.com/bbtsoftware/TfsUrlParser/actions/workflows/dotnet.yml/badge.svg?branch=develop)](https://github.com/bbtsoftware/TfsUrlParser/actions/workflows/dotnet.yml)
[![Build status](https://github.com/bbtsoftware/TfsUrlParser/actions/workflows/release.yml/badge.svg)](https://github.com/bbtsoftware/TfsUrlParser/actions/workflows/release.yml)
[![Coverage Status](https://codecov.io/gh/bbtsoftware/TfsUrlParser/branch/develop/graph/badge.svg?token=0VLbB8a8EF)](https://codecov.io/gh/bbtsoftware/TfsUrlParser)

This NuGet package contains code for parsing Azure DevOps and Azure DevOps Server URLs.

[![License](http://img.shields.io/:license-mit-blue.svg)](https://github.com/bbtsoftware/TfsUrlParser/blob/master/LICENSE)

## How to use

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
