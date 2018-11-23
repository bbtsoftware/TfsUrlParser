# RepositoryDescription

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
