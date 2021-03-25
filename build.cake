#addin nuget:?package=Cake.Figlet&version=1.3.1
#addin nuget:?package=Cake.GitPackager&version=0.1.3.2
#addin nuget:?package=Cake.Git&version=0.22.0
#addin nuget:?package=Cake.FileHelpers&version=3.3.0
#tool nuget:?package=docfx.console&version=2.51.0

// Get a tag version name 
public string GetTaggedVersion()
{
    var travisTag = EnvironmentVariable("TRAVIS_TAG");
    return travisTag?.TrimStart('v');
}

// Helper method for setting a lot of file attributes at once
public FilePath[] SetFileAttributes(FilePathCollection files, System.IO.FileAttributes fileAttributes)
{
    var results = new System.Collections.Concurrent.ConcurrentBag<FilePath>();

    Parallel.ForEach(files, f =>
    {
        System.IO.File.SetAttributes(f.FullPath, fileAttributes);
        results.Add(f);
    });

    return results.ToArray();
}

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

Task("Info")
.Does(() => 
{
    Information(Figlet("Okta.Auth.Sdk"));

    var cakeVersion = typeof(ICakeContext).Assembly.GetName().Version.ToString();

    Information("Building using {0} version of Cake", cakeVersion);
});

Task("BuildDocs")
.IsDependentOn("Build")
.Does(() =>
{
    StartProcess(Context.Tools.Resolve("docfx") ?? Context.Tools.Resolve("docfx.exe"), 
                 "./docs/docfx.json");
});

Task("CloneExistingDocs")
.Does(() =>
{
    var tempDir = "./docs/temp";

    if (DirectoryExists(tempDir))
    {
        // Some git files are read-only, so recursively remove any attributes:
        SetFileAttributes(GetFiles(tempDir + "/**/*.*"), System.IO.FileAttributes.Normal);

        DeleteDirectory(tempDir, recursive: true);
    }

    GitClone("https://github.com/okta/okta-auth-dotnet.git",
            tempDir,
            new GitCloneSettings
            {
                BranchName = "gh-pages",
            });
});

Task("CreateRootRedirector")
.Does(() =>
{
    FileWriteText("./docs/temp/index.html",
        @"<meta http-equiv=""refresh"" content=""0; url=https://developer.okta.com/okta-auth-dotnet/latest/"">");
});

Task("PrepareVersionsList").
IsDependentOn("CopyDocsToVersionedDirectories").
Does(()=>
{
    var versionDirectories = System.IO.Directory.GetDirectories("./docs/temp")
        .Select(d => $"\"{System.IO.Path.GetFileName(d)}\"")
        .Where(d => !d.Equals("\".git\""));

    var versions = string.Join(",", versionDirectories);

    FileWriteText("./docs/temp/versions.json", $"{{\"versions\":[{versions}]}}");
});

Task("CopyDocsToVersionedDirectories")
.IsDependentOn("BuildDocs")
.IsDependentOn("CloneExistingDocs")
.Does(() =>
{
    if (DirectoryExists("./docs/temp/latest"))
    {
        DeleteDirectory("./docs/temp/latest", recursive: true);
    }
    Information("Copying docs to docs/temp/latest");
    CopyDirectory("./docs/_site/", "./docs/temp/latest/");

    var taggedVersion = GetTaggedVersion();
    if (string.IsNullOrEmpty(taggedVersion))
    {
        Console.WriteLine("TRAVIS_TAG not set, won't copy docs to a tagged directory");
        return;
    }

    var tagDocsDirectory = string.Format("./docs/temp/{0}", taggedVersion);

    Information("Copying docs to " + tagDocsDirectory);
    CopyDirectory("./docs/_site/", tagDocsDirectory);
});

// Define top-level tasks
Task("Default")
    .IsDependentOn("Info")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Pack");

Task("Docs")
    .IsDependentOn("BuildDocs")
    .IsDependentOn("CloneExistingDocs")
    .IsDependentOn("CopyDocsToVersionedDirectories")
    .IsDependentOn("CreateRootRedirector")
    .IsDependentOn("PrepareVersionsList");

// Default task
var target = Argument("target", "Default");
RunTarget(target);
