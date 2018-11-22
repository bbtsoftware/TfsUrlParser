namespace TfsUrlParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Describes the different parts of a repository URL.
    /// </summary>
    public class RepositoryDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryDescription"/> class.
        /// </summary>
        /// <param name="repoUrl">Full URL of the repository,
        /// eg. <code>http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        public RepositoryDescription(Uri repoUrl)
        {
            if (repoUrl == null)
            {
                throw new ArgumentNullException(nameof(repoUrl));
            }

            this.RepositoryUrl = ConvertToSupportedUriScheme(repoUrl);
            var repoUrlString = this.RepositoryUrl.ToString();

            var gitSeparator = new[] { "/_git/" };
            var splitPath = repoUrl.AbsolutePath.Split(gitSeparator, StringSplitOptions.None);
            if (splitPath.Length < 2)
            {
                throw new UriFormatException("No valid Git repository URL.");
            }

            this.ServerUrl = new Uri(repoUrl.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped));

            var splitFirstPart = splitPath[0].Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            // visualstudio.com URLs don't contain the collection in the path
            if (splitFirstPart.Length == 1 && this.ServerUrl.Host.EndsWith("visualstudio.com"))
            {
                this.CollectionName = "DefaultCollection";
                this.CollectionUrl = this.ServerUrl;
            }
            else if (splitFirstPart.Length >= 2)
            {
                this.CollectionName = splitFirstPart.Reverse().Skip(1).Take(1).Single();
                this.CollectionUrl =
                    new Uri(
                        repoUrlString.Substring(
                            0,
                            repoUrlString.IndexOf("/" + this.CollectionName + "/", StringComparison.OrdinalIgnoreCase) + this.CollectionName.Length + 1));
            }
            else
            {
                throw new UriFormatException("No valid Git repository URL containing default collection and project name.");
            }

            var splitLastPart = splitPath[1].Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            this.ProjectName = splitFirstPart.Last();
            this.RepositoryName = splitLastPart.First();
        }

        /// <summary>
        /// Gets the Url of the Team Foundation Server.
        /// </summary>
        public Uri ServerUrl { get; }

        /// <summary>
        /// Gets the name of the Team Foundation Server collection.
        /// </summary>
        public string CollectionName { get; }

        /// <summary>
        /// Gets the URL for accessing the web portal of the Team Foundation Server collection.
        /// </summary>
        public Uri CollectionUrl { get; private set; }

        /// <summary>
        /// Gets the name of the Team Foundation Server project.
        /// </summary>
        public string ProjectName { get; private set; }

        /// <summary>
        /// Gets the name of the Git repository.
        /// </summary>
        public string RepositoryName { get; private set; }

        /// <summary>
        /// Gets the URL of the Git repository.
        /// URLs using SSH scheme are converted to HTTPS.
        /// </summary>
        public Uri RepositoryUrl { get; private set; }

        /// <summary>
        /// Converts the repository URL to a supported scheme if possible.
        /// </summary>
        /// <param name="repoUrl">Repository URL.</param>
        /// <returns>Repository URL with a supported scheme.</returns>
        private static Uri ConvertToSupportedUriScheme(Uri repoUrl)
        {
            if (repoUrl.Scheme == Uri.UriSchemeHttp || repoUrl.Scheme == Uri.UriSchemeHttps)
            {
                return repoUrl;
            }

            // Convert SSH url to HTTPS
            if (repoUrl.Scheme == "ssh")
            {
                var uriBuilder = new UriBuilder(repoUrl)
                {
                    Scheme = Uri.UriSchemeHttps,
                    UserName = string.Empty,
                    Password = string.Empty
                };
                return uriBuilder.Uri;
            }

            throw new UriFormatException(
                $"Invalid scheme in URI {repoUrl}. Only HTTP, HTTPS and SSH are supported");
        }
    }
}
