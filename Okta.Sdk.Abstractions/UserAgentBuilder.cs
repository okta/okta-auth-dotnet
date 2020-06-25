// <copyright file="UserAgentBuilder.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Okta.Sdk.Abstractions
{
    /// <summary>
    /// A User-Agent string generator that uses reflection to detect the current assembly version.
    /// </summary>
    public sealed class UserAgentBuilder
    {
        // Lazy ensures this only runs once and is cached.
        private readonly Lazy<string> _cachedUserAgent;

        private string _oktaSdkUserAgentName = string.Empty;

        private Version _sdkVersion;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAgentBuilder"/> class.
        /// </summary>
        /// <param name="sdkUserAgentName">The user agent name. i.e: okta-sdk-dotnet.</param>
        /// <param name="sdkVersion">The sdk version.</param>
        public UserAgentBuilder(string sdkUserAgentName, Version sdkVersion)
        {
            _cachedUserAgent = new Lazy<string>(Generate);
            _oktaSdkUserAgentName = sdkUserAgentName;
            _sdkVersion = sdkVersion;
        }

        /// <summary>
        /// Constructs a User-Agent string.
        /// </summary>
        /// <returns>A User-Agent string with the default tokens, and any additional tokens.</returns>
        public string GetUserAgent() => _cachedUserAgent.Value;

        private string Generate()
        {
            var sdkToken = $"{_oktaSdkUserAgentName}/{GetSdkVersion()}";

            var runtimeToken = $"runtime/{UserAgentHelper.GetRuntimeVersion()}";

            var operatingSystemToken = $"os/{Sanitize(RuntimeInformation.OSDescription)}";

            return string.Join(
                " ",
                sdkToken,
                runtimeToken,
                operatingSystemToken)
            .Trim();
        }

        private string GetSdkVersion()
        {
            return $"{_sdkVersion.Major}.{_sdkVersion.Minor}.{_sdkVersion.Build}";
        }

        private static readonly char[] IllegalCharacters = new char[] { '/', ':', ';' };

        private static string Sanitize(string input)
            => IllegalCharacters.Aggregate(input, (current, bad) => current.Replace(bad, '-'));
    }
}
