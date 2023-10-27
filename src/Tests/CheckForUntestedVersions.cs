namespace Tests
{
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using NuGet.Common;
    using NuGet.Protocol;
    using NuGet.Protocol.Core.Types;
    using NuGet.Versioning;
    using NUnit.Framework;

    [TestFixture]
    class CheckForUntestedVersions
    {
        [Test]
        public async Task Should_test_latest_minor_versions()
        {
            var versions = await GetAllNServiceBusVersions().ConfigureAwait(false);

            var latestVersions = versions
                .Where(v => !v.IsPrerelease)
                .GroupBy(v => v.Version.Major)
                .Select(group => group
                    .Max());

            foreach (var latestVersion in latestVersions)
            {
                var name = $"NServiceBus{latestVersion.Major}.{latestVersion.Minor}";
                if (!Directory.Exists(Path.Combine(Constants.ProjectDirectory, name)))
                {
                    Assert.Fail($"No test project for {name} found.");
                }
            }
        }

        [Test]
        public async Task Should_test_latest_unstable()
        {
            var versions = await GetAllNServiceBusVersions().ConfigureAwait(false);

            var latestPrerelease = versions.Where(v => v.IsPrerelease).Max();

            var name = $"NServiceBus{latestPrerelease.Major}.{latestPrerelease.Minor}";
            if (!Directory.Exists(Path.Combine(Constants.ProjectDirectory, name)))
            {
                Assert.Fail($"No test project for {name} found.");
            }
        }

        static async Task<NuGetVersion[]> GetAllNServiceBusVersions()
        {
            var repository = Repository.Factory.GetCoreV3("https://f.feedz.io/particular-software/packages/nuget/index.json");
            var result = await repository.GetResourceAsync<FindPackageByIdResource>()
                .ConfigureAwait(false);
            var versions = (await result.GetAllVersionsAsync("NServiceBus", Cache, NullLogger.Instance, CancellationToken.None)
                    .ConfigureAwait(false))
                .ToArray();
            return versions;
        }

        static readonly SourceCacheContext Cache = new SourceCacheContext();
    }
}
