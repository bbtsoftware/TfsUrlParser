namespace TfsUrlParser.Tests
{
    using System;
    using Shouldly;
    using Xunit;

    public class RepositoryDescriptionTests
    {
        [Theory]
        [InlineData(
            @"http://myserver:8080/tfs/defaultcollection/myproject/",
            "No valid Git repository URL.")]
        [InlineData(
            @"http://myserver:8080/tfs/defaultcollection/myproject/_git",
            "No valid Git repository URL.")]
        [InlineData(
            @"http://myserver:8080/_git/myrepository",
            "No valid Git repository URL containing default collection and project name.")]
        [InlineData(
            @"http://myserver:8080/defaultcollection/_git/myrepository",
            "No valid Git repository URL containing default collection and project name.")]
        public void Should_Throw_If_No_Valid_Url(string repoUrl, string expectedMessage)
        {
            // Given / When
            var result = Record.Exception(() => new RepositoryDescription(new Uri(repoUrl)));

            // Then
            result.IsUriFormatExceptionException(expectedMessage);
        }

        [Theory]
        [InlineData(
            @"http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository",
            @"http://myserver:8080/",
            "defaultcollection",
            @"http://myserver:8080/tfs/defaultcollection",
            "myproject",
            "myrepository",
            @"http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository")]
        [InlineData(
            @"http://tfs.myserver/defaultcollection/myproject/_git/myrepository",
            @"http://tfs.myserver/",
            "defaultcollection",
            @"http://tfs.myserver/defaultcollection",
            "myproject",
            "myrepository",
            @"http://tfs.myserver/defaultcollection/myproject/_git/myrepository")]
        [InlineData(
            @"http://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository",
            @"http://mytenant.visualstudio.com/",
            "defaultcollection",
            @"http://mytenant.visualstudio.com/defaultcollection",
            "myproject",
            "myrepository",
            @"http://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository")]
        [InlineData(
            @"http://tfs.foo.com/foo/foo/_git/foo",
            @"http://tfs.foo.com/",
            "foo",
            @"http://tfs.foo.com/foo",
            "foo",
            "foo",
            @"http://tfs.foo.com/foo/foo/_git/foo")]
        [InlineData(
            @"http://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository/somethingelse",
            @"http://mytenant.visualstudio.com/",
            "defaultcollection",
            @"http://mytenant.visualstudio.com/defaultcollection",
            "myproject",
            "myrepository",
            @"http://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository/somethingelse")]
        [InlineData(
            @"https://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository",
            @"https://myserver:8080/",
            "defaultcollection",
            @"https://myserver:8080/tfs/defaultcollection",
            "myproject",
            "myrepository",
            @"https://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository")]
        [InlineData(
            @"https://tfs.myserver/defaultcollection/myproject/_git/myrepository",
            @"https://tfs.myserver/",
            "defaultcollection",
            @"https://tfs.myserver/defaultcollection",
            "myproject",
            "myrepository",
            @"https://tfs.myserver/defaultcollection/myproject/_git/myrepository")]
        [InlineData(
            @"https://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository",
            @"https://mytenant.visualstudio.com/",
            "defaultcollection",
            @"https://mytenant.visualstudio.com/defaultcollection",
            "myproject",
            "myrepository",
            @"https://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository")]
        [InlineData(
            @"https://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository/somethingelse",
            @"https://mytenant.visualstudio.com/",
            "defaultcollection",
            @"https://mytenant.visualstudio.com/defaultcollection",
            "myproject",
            "myrepository",
            @"https://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository/somethingelse")]
        [InlineData(
            @"https://tfs.foo.com/foo/foo/_git/foo",
            @"https://tfs.foo.com/",
            "foo",
            @"https://tfs.foo.com/foo",
            "foo",
            "foo",
            @"https://tfs.foo.com/foo/foo/_git/foo")]
        [InlineData(
            @"ssh://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository",
            @"ssh://myserver:8080/",
            "defaultcollection",
            @"https://myserver:8080/tfs/defaultcollection",
            "myproject",
            "myrepository",
            @"https://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository")]
        [InlineData(
            @"ssh://tfs.myserver/defaultcollection/myproject/_git/myrepository",
            @"ssh://tfs.myserver/",
            "defaultcollection",
            @"https://tfs.myserver/defaultcollection",
            "myproject",
            "myrepository",
            @"https://tfs.myserver/defaultcollection/myproject/_git/myrepository")]
        [InlineData(
            @"ssh://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository",
            @"ssh://mytenant.visualstudio.com/",
            "defaultcollection",
            @"https://mytenant.visualstudio.com/defaultcollection",
            "myproject",
            "myrepository",
            @"https://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository")]
        [InlineData(
            @"ssh://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository/somethingelse",
            @"ssh://mytenant.visualstudio.com/",
            "defaultcollection",
            @"https://mytenant.visualstudio.com/defaultcollection",
            "myproject",
            "myrepository",
            @"https://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository/somethingelse")]
        [InlineData(
            @"ssh://tfs.foo.com/foo/foo/_git/foo",
            @"ssh://tfs.foo.com/",
            "foo",
            @"https://tfs.foo.com/foo",
            "foo",
            "foo",
            @"https://tfs.foo.com/foo/foo/_git/foo")]
        [InlineData(
            @"ssh://foo:bar@myserver:8080/tfs/defaultcollection/myproject/_git/myrepository",
            @"ssh://myserver:8080/",
            "defaultcollection",
            @"https://myserver:8080/tfs/defaultcollection",
            "myproject",
            "myrepository",
            @"https://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository")]
        [InlineData(
            @"https://myorganization@dev.azure.com/myorganization/myproject/_git/myrepository",
            @"https://myorganization@dev.azure.com/",
            "myorganization",
            @"https://myorganization@dev.azure.com/myorganization",
            "myproject",
            "myrepository",
            @"https://myorganization@dev.azure.com/myorganization/myproject/_git/myrepository")]
        [InlineData(
            @"https://myorganization.visualstudio.com/myproject/_git/myrepository",
            @"https://myorganization.visualstudio.com/",
            "DefaultCollection",
            @"https://myorganization.visualstudio.com",
            "myproject",
            "myrepository",
            @"https://myorganization.visualstudio.com/myproject/_git/myrepository")]
        public void Should_Parse_Repo_Url(
            string repoUrl,
            string serverUrl,
            string collectionName,
            string collectionurl,
            string projectName,
            string repositoryName,
            string repositoryUrl)
        {
            // Given / When
            var repositoryDescription = new RepositoryDescription(new Uri(repoUrl));

            // Then
            repositoryDescription.ServerUrl.ShouldBe(new Uri(serverUrl));
            repositoryDescription.CollectionName.ShouldBe(collectionName);
            repositoryDescription.CollectionUrl.ShouldBe(new Uri(collectionurl));
            repositoryDescription.ProjectName.ShouldBe(projectName);
            repositoryDescription.RepositoryName.ShouldBe(repositoryName);
            repositoryDescription.RepositoryUrl.ShouldBe(new Uri(repositoryUrl));
        }
    }
}
