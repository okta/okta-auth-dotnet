// <copyright file="UserAgentHelper.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Okta.Sdk.Abstractions
{
    /// <summary>
    /// Utility class for user-agent report.
    /// </summary>
    public static class UserAgentHelper
    {
        private const string DotNetCoreRuntimeLabel = ".NET Core";

        /// <summary>
        /// Gets the SDK version.
        /// </summary>
        public static Version SdkVersion
        {
            get { return typeof(BaseOktaClient).GetTypeInfo().Assembly.GetName().Version; }
        }

        /// <summary>
        /// Gets the .NET runtime version.
        /// </summary>
        /// <param name="runtimeFrameworkDescription">The runtime description.</param>
        /// <param name="runtimeAssemblyCodeBase">The runtime code base.</param>
        /// <returns>The .NET runtime version.</returns>
        public static string GetRuntimeVersion(string runtimeFrameworkDescription = "", string runtimeAssemblyCodeBase = "")
        {
            var frameworkDescription = string.IsNullOrEmpty(runtimeFrameworkDescription) ? RuntimeInformation.FrameworkDescription : runtimeFrameworkDescription;
            var assemblyCodeBase = runtimeAssemblyCodeBase;

            // RuntimeInformation.FrameworkDescription is not always accurate, it can report versions like 4.6.x for .NET Core 2.x.
            if (frameworkDescription.StartsWith(DotNetCoreRuntimeLabel, StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrEmpty(assemblyCodeBase))
                {
                    assemblyCodeBase = typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly?.CodeBase;
                }

                if (assemblyCodeBase != null)
                {
                    var runtimeAssemblyLocation = assemblyCodeBase.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
                    int dotnetCoreAppIndex = Array.IndexOf(runtimeAssemblyLocation, "Microsoft.NETCore.App");

                    if (dotnetCoreAppIndex >= 0 && dotnetCoreAppIndex < runtimeAssemblyLocation.Length - 2)
                    {
                        frameworkDescription = $"{DotNetCoreRuntimeLabel} {runtimeAssemblyLocation[dotnetCoreAppIndex + 1]}";
                    }
                }
            }

            return frameworkDescription;
        }
    }
}
