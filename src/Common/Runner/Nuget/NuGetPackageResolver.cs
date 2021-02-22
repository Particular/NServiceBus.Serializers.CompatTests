namespace Common.Runner.Nuget
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using NuGet.Common;
    using NuGet.Packaging;
    using NuGet.Protocol;
    using NuGet.Protocol.Core.Types;
    using NuGet.Versioning;

    public class NuGetPackageResolver
    {
        public NuGetPackageResolver(string packagesStore, string nugetFeed = "https://api.nuget.org/v3/index.json")
        {
            this.packagesStore = packagesStore;
            this.nugetFeed = nugetFeed;
        }

        public async Task<Package> DownloadPackageWithDependencies(PackageInfo packageInfo)
        {
            var latesVersion = await GetLatestPackageVersion(packageInfo);

            var packageLocation = await DownloadPackage(packageInfo, latesVersion);
            var dependencies = await GetAllDependencies(packageInfo, latesVersion);

            var package = new Package
            {
                Info = packageInfo,
                Version = latesVersion.ToString(),
                Files = dependencies.Where(f => Path.GetExtension(f.Name) == ".dll").ToArray()
            };

            return package;
        }

        async Task<NuGetVersion> GetLatestPackageVersion(PackageInfo packageInfo)
        {
            var logger = NullLogger.Instance;
            var cancellationToken = CancellationToken.None;

            var cache = new SourceCacheContext();
            var repository = Repository.Factory.GetCoreV3(nugetFeed);
            var resource = await repository.GetResourceAsync<FindPackageByIdResource>();

            var versions = await resource.GetAllVersionsAsync(
                packageInfo.PackageName,
                cache,
                logger,
                cancellationToken);

            return packageInfo.VersionConstraint.FindBestMatch(versions);
        }

        async Task<IEnumerable<FileInfo>> GetAllDependencies(PackageInfo package, NuGetVersion version)
        {
            var packageDetails = await GetPackageDetails(package.PackageName, version);

            var fileInfos = new List<FileInfo>();

            var dependencies = packageDetails.GetDependencyGroups().SelectMany(x => x.Packages.Select(y => new PackageInfo(y.Id, y.VersionRange)));

            foreach (var dependency in dependencies)
            {
                var result = await DownloadPackageWithDependencies(dependency);

                fileInfos.AddRange(result.Files);
            }

            return fileInfos;
        }

        async Task<string> DownloadPackage(PackageInfo package, NuGetVersion version)
        {
            var logger = NullLogger.Instance;
            var cancellationToken = CancellationToken.None;

            var cache = new SourceCacheContext();
            var repository = Repository.Factory.GetCoreV3(nugetFeed);
            var resource = await repository.GetResourceAsync<FindPackageByIdResource>();

            using (var packageStream = new MemoryStream())
            {

                await resource.CopyNupkgToStreamAsync(
                    package.PackageName,
                    version,
                    packageStream,
                    cache,
                    logger,
                    cancellationToken);

                Console.WriteLine($"Downloaded package {package.PackageName} {version}");

                using (var packageReader = new PackageArchiveReader(packageStream))
                {

                    return packageReader.GetFiles().First();
                }
            }
        }

        async Task<NuspecReader> GetPackageDetails(string packageName, NuGetVersion version)
        {
            var logger = NullLogger.Instance;
            var cancellationToken = CancellationToken.None;

            var cache = new SourceCacheContext();
            var repository = Repository.Factory.GetCoreV3(nugetFeed);
            var resource = await repository.GetResourceAsync<FindPackageByIdResource>();

            using (var packageStream = new MemoryStream())
            {

                await resource.CopyNupkgToStreamAsync(
                    packageName,
                    version,
                    packageStream,
                    cache,
                    logger,
                    cancellationToken);

                Console.WriteLine($"Downloaded package {packageName} {version}");

                using (var packageReader = new PackageArchiveReader(packageStream))
                {
                    var nuspecReader = await packageReader.GetNuspecReaderAsync(cancellationToken);
                    return nuspecReader;
                }
            }
        }

        async Task<IEnumerable<NuGetVersion>> GetAllVersions(string packageName)
        {
            var logger = NullLogger.Instance;
            var cancellationToken = CancellationToken.None;

            var cache = new SourceCacheContext();
            var repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
            var resource = await repository.GetResourceAsync<FindPackageByIdResource>();

            var versions = await resource.GetAllVersionsAsync(
                packageName,
                cache,
                logger,
                cancellationToken);
            //todo: include the requirements
            return versions;
        }

        readonly string packagesStore;
        readonly string nugetFeed;
    }
}