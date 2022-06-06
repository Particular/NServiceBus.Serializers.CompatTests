# NServiceBus.Serializers.CompatTests

Tests to ensure backwards compatibility for our serializers.

## How to run the tests

The `Tests` project contains two tests to run:
* Serialize: Serializes all test cases via the available serializers as files to disk. This test should be run first to generate the files required for the deserialization test.
  * The serialization test will delete all serialized files from previous runs.
* Deserialize: Deserializes all previously serialized files from disk and compares them to the expected state.

### Debugging

To debug/investigate a failure on a specific NServiceBus version, set the associated NServiceBus project in the solution as the startup project and start it via the debugger. The test cases that are tested can be configured via command line arguments and/or modification of `Program.cs` to scope the test to the desired scenarios.

## Project structure

The test project consists of two parts:
* The test runner infrastructure
* Executable projects for each NServiceBus version

### Test runner infrastructure

The test runner identifies all NServiceBus versions defined in the solution and invokes each version's executable. The test runner will instruct the executable to certain tasks by passing command line arguments. This design allows the test runner to be more independent from the platform of each version to test. 
The instructions that can be configured via command line arguments is intentionally coarse in favor of keeping the amount of test cases lower (over providing more granular test case reporting) as each generated test cases will run a new executable and therefore extend the total test execution time.

### Executable projects

All version projects use the same `Program.cs` that will run the actual test logic of serializing, deserialzing and verification of specified test types.

The test cases are defined in the `Common` and inherit from `TestCase`.

## Adding new versions

To add a new version to test, follow these steps:
* Create a new project, following the naming convention `NServiceBus<MajorVersion>.<MinorVersion>`.
  * The project needs to be an executable.
  * The project currently needs to target the .NET Framework.
  * Add a reference to the necessary NuGet packages.
    * Use the wildcard pattern to ensure that the package always uses the latest patch version (e.g. `<PackageReference Include="NServiceBus" Version="6.5.*" />`).
  * Add a project reference to the `Common` project.
  * Add a file link to `Common\Program.cs`.
  * Add the classes `JsonSerializerFacade` and `XmlSerializerFacade`, implementing `ISerializerFacade`.
    * This is only necessary once per major version and subsequent minor version projects can add a file link to these classes instead.

## Adding new serializers

To add tests for a new serializer, follow these steps:

* Create a facade class for the new serializer, implementing `ISerializerFacade`.
* Define the class in the folder matching the serializer lowest supported version of NServiceBus. E.g., if the decision is to introduce a new serializer in NServiceBus 7.3, define the new facade in the `NServiceBus7.3` folder.
* Add a link to the new serializer facade file to all subsequent NServiceBus supported versions
* In Octopus, add a manual step to the serializer project to ensure the release to production process checks for compatibility tests results. For an example, look at the [NServiceBus release process steps](https://deploy.particular.net/app#/Spaces-1/projects/nservicebus/deployments/process/steps?actionId=9adbe00e-a81f-4346-8801-0eef1424a917).

## Exclusions

For known test incompatibilities, exclusions can be defined to skip deserialization for specific test types from specific versions on the current project. Exclusions are specified by creating a custom class in a project deriving from `Excludes` (minor versions of the same major version might want to include the exclude list from the MajorVersion.0 project). The `FilesToExclude` contains a collection of test configurations to ignore. Each configuration to ignore is defined by the type (implementing `TestCase`) of the test and the name of the serialized test case file, e.g. `"NServiceBus4.6 .NET Framework 4.5.2.json"`.

## Build triggers

The tests are triggered by:
* A weekly schedule
* Whenever a new (latest) package of `NServiceBus` or `NServiceBus.Newtonsoft.Json` has been pushed to the company's myget feed.

## Dependencies

The test projects reference different Major.Minor versions of NServicebus. In those projects, NServiceBus packages should not be updated. The following [command was used](https://github.com/Particular/NServiceBus.Serializers.CompatTests/pull/23#issuecomment-797354551) to make dependabot ignore NServiceBus updates in this repository. If it proves to be inefficient dependabot could be disabled as in [the following PR](https://github.com/Particular/NServiceBus.Serializers.CompatTests/pull/32)
