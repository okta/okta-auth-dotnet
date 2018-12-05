// Default MSBuild configuration arguments

var configuration = Argument("configuration", "Release");

Task("Clean")
.Does(() =>
{
    CleanDirectory("./artifacts/");

    GetDirectories("./**/bin")
        .ToList()
        .ForEach(d => CleanDirectory(d));

    GetDirectories("./**/obj")
        .ToList()
        .ForEach(d => CleanDirectory(d));
});

Task("Restore")
.Does(() => 
{
    DotNetCoreRestore("./Okta.Auth.Sdk.sln");
});

Task("Build")
.IsDependentOn("Restore")
.Does(() =>
{
    var projects = GetFiles("./**/*.csproj");
    Console.WriteLine("Building {0} projects", projects.Count());

    foreach (var project in projects)
    {
        Console.WriteLine("Building project ", project.GetFilenameWithoutExtension());
        DotNetCoreBuild(project.FullPath, new DotNetCoreBuildSettings
        {
            Configuration = configuration
        });
    }
});

Task("Test")
.IsDependentOn("Restore")
.IsDependentOn("Build")
.Does(() =>
{
    var testProjects = new[] { "Okta.Auth.Sdk.UnitTests" };
    // For now, we won't run integration tests in CI

    foreach (var name in testProjects)
    {
        DotNetCoreTest(string.Format("./{0}/{0}.csproj", name));
    }
});
/*Task("IntegrationTest")
.IsDependentOn("Restore")
.IsDependentOn("Build")
.Does(() =>
{
    var testProjects = new[] { "Okta.Auth.Sdk.IntegrationTests" };
    // Run integration tests in nightly travis cron job

    foreach (var name in testProjects)
    {
        DotNetCoreTest(string.Format("./{0}/{0}.csproj", name));
    }
});*/
Task("Pack")
.IsDependentOn("Test")
//.IsDependentOn("IntegrationTest")
.Does(() =>
{
	var projects = new List<string>()
	{
		"Okta.Sdk.Abstractions",
		"Okta.Auth.Sdk"
	};
	
	projects
    .ForEach(name =>
    {
        Console.WriteLine($"\nCreating NuGet package for {name}");
        
		DotNetCorePack($"./{name}", new DotNetCorePackSettings
		{
			Configuration = configuration,
			OutputDirectory = "./artifacts",
		});
    });
	
});

// Define top-level tasks

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Pack");

// Default task
var target = Argument("target", "Default");
RunTarget(target);
