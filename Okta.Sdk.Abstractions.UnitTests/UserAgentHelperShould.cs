using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Okta.Sdk.Abstractions.UnitTests
{
    public class UserAgentHelperShould
    {
        [Theory]
        [InlineData("foo")]
        [InlineData("foo .NET")]
        [InlineData(".NET")]
        [InlineData(".NET foo")]
        [InlineData(".NET Framework 4.8")]
        public void ReturnPassedFrameworkDescriptionIfItDoesNotStartWithKeyword(string runtimeDescription)
        {
            // Keyword for runtimeDescription = ".NET Core"
            var frameworkInfo = UserAgentHelper.GetRuntimeVersion(runtimeDescription);

            frameworkInfo.Should().Be(runtimeDescription);
        }

        [Theory]
        [InlineData("file:///C:/Program Files/dotnet/shared/Microsoft.NETCore.App/2.0.9/System.Private.CoreLib.dll", "2.0.9")]
        [InlineData("Microsoft.NETCore.App/2.0.9/System.Private.CoreLib.dll", "2.0.9")]
        [InlineData("foo/Microsoft.NETCore.App/2.0.9/System.Private.CoreLib.dll", "2.0.9")]
        [InlineData("foo/Microsoft.NETCore.App/3.1.1/System.Private.CoreLib.dll", "3.1.1")]
        public void ReturnNormalizedFrameworkDescriptionWhenItStartsWithKeywordAndCodeBaseHasKeyword(string runtimeAssemblyCodeBase, string expectedVersion)
        {
            // Keyword for runtimeDescription = ".NET Core"
            // Keyword for codeBase = "Microsoft.NETCore.App"
            var frameworkInfo = UserAgentHelper.GetRuntimeVersion(".NET Core", runtimeAssemblyCodeBase);

            frameworkInfo.Should().Be($".NET Core {expectedVersion}");
        }

        [Theory]
        [InlineData("Microsoft.App/2.0.9/System.Private.CoreLib.dll")]
        [InlineData("foo/Microsoft.NETCore.foo/2.0.9/System.Private.CoreLib.dll")]
        [InlineData("foo/Microsoft.NETCore.Apps/3.1.1/System.Private.CoreLib.dll")]
        public void ReturnPassedFrameworkDescriptionWhenAssemblyCodeBaseDoesNotContainKeyword(string runtimeAssemblyCodeBase)
        {
            // Keyword for runtimeDescription = ".NET Core"
            // Keyword for codeBase = "Microsoft.NETCore.App"
            var frameworkInfo = UserAgentHelper.GetRuntimeVersion(".NET Core foo", runtimeAssemblyCodeBase);

            frameworkInfo.Should().Be(".NET Core foo");
        }

        [Fact]
        public void ReturnPassedFrameworkDescriptionWhenAssemblyCodeBaseContainsKeywordButNotVersion()
        {
            // Keyword for runtimeDescription = ".NET Core"
            // Keyword for codeBase = "Microsoft.NETCore.App"
            var frameworkInfo = UserAgentHelper.GetRuntimeVersion(".NET Core foo", "foo/Microsoft.NETCore.App");

            frameworkInfo.Should().Be(".NET Core foo");
        }
    }
}

