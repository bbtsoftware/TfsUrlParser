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
            "defaultcollection",
            @"http://myserver:8080/tfs/defaultcollection",
            "myproject",
            "myrepository")]
        [InlineData(
            @"http://tfs.myserver/defaultcollection/myproject/_git/myrepository",
            "defaultcollection",
            @"http://tfs.myserver/defaultcollection",
            "myproject",
            "myrepository")]
        [InlineData(
            @"http://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository",
            "defaultcollection",
            @"http://mytenant.visualstudio.com/defaultcollection",
            "myproject",
            "myrepository")]
        [InlineData(
            @"http://tfs.foo.com/foo/foo/_git/foo",
            "foo",
            @"http://tfs.foo.com/foo",
            "foo",
            "foo")]
        [InlineData(
            @"http://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository/somethingelse",
            "defaultcollection",
            @"http://mytenant.visualstudio.com/defaultcollection",
            "myproject",
            "myrepository")]
        [InlineData(
            @"https://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository",
            "defaultcollection",
            @"https://myserver:8080/tfs/defaultcollection",
            "myproject",
            "myrepository")]
        [InlineData(
            @"https://tfs.myserver/defaultcollection/myproject/_git/myrepository",
            "defaultcollection",
            @"https://tfs.myserver/defaultcollection",
            "myproject",
            "myrepository")]
        [InlineData(
            @"https://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository",
            "defaultcollection",
            @"https://mytenant.visualstudio.com/defaultcollection",
            "myproject",
            "myrepository")]
        [InlineData(
            @"https://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository/somethingelse",
            "defaultcollection",
            @"https://mytenant.visualstudio.com/defaultcollection",
            "myproject",
            "myrepository")]
        [InlineData(
            @"https://tfs.foo.com/foo/foo/_git/foo",
            "foo",
            @"https://tfs.foo.com/foo",
            "foo",
            "foo")]
        [InlineData(
            @"ssh://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository",
            "defaultcollection",
            @"https://myserver:8080/tfs/defaultcollection",
            "myproject",
            "myrepository")]
        [InlineData(
            @"ssh://tfs.myserver/defaultcollection/myproject/_git/myrepository",
            "defaultcollection",
            @"https://tfs.myserver/defaultcollection",
            "myproject",
            "myrepository")]
        [InlineData(
            @"ssh://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository",
            "defaultcollection",
            @"https://mytenant.visualstudio.com/defaultcollection",
            "myproject",
            "myrepository")]
        [InlineData(
            @"ssh://mytenant.visualstudio.com/defaultcollection/myproject/_git/myrepository/somethingelse",
            "defaultcollection",
            @"https://mytenant.visualstudio.com/defaultcollection",
            "myproject",
            "myrepository")]
        [InlineData(
            @"ssh://tfs.foo.com/foo/foo/_git/foo",
            "foo",
            @"https://tfs.foo.com/foo",
            "foo",
            "foo")]
        [InlineData(
            @"ssh://foo:bar@myserver:8080/tfs/defaultcollection/myproject/_git/myrepository",
            "defaultcollection",
            @"https://myserver:8080/tfs/defaultcollection",
            "myproject",
            "myrepository")]
        public void Should_Parse_Repo_Url(string repoUrl, string collectionName, string collectionurl, string projectName, string repositoryName)
        {
            // Given / When
            var repositoryDescription = new RepositoryDescription(new Uri(repoUrl));

            // Then
            repositoryDescription.CollectionName.ShouldBe(collectionName);
            repositoryDescription.CollectionUrl.ShouldBe(new Uri(collectionurl));
            repositoryDescription.ProjectName.ShouldBe(projectName);
            repositoryDescription.RepositoryName.ShouldBe(repositoryName);
        }
    }
}
