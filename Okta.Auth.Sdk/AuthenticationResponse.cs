// <copyright file="AuthenticationResponse.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// This class represents the authentication response
    /// </summary>
    public class AuthenticationResponse : Resource, IAuthenticationResponse
    {
        /// <inheritdoc/>
        public string StateToken => GetStringProperty("stateToken");

        /// <inheritdoc/>
        public string RelayState => GetStringProperty("relayState");

        /// <inheritdoc/>
        public string SessionToken => GetStringProperty("sessionToken");

        /// <inheritdoc/>
        public DateTimeOffset? ExpiresAt => GetDateTimeProperty("expiresAt");

        /// <inheritdoc/>
        public AuthenticationStatus AuthenticationStatus => GetEnumProperty<AuthenticationStatus>("status");

        /// <inheritdoc/>
        public Resource Embedded => GetResourceProperty<Resource>("_embedded");

        /// <inheritdoc/>
        public Resource Links => GetResourceProperty<Resource>("_links");
    }
}
