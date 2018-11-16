// <copyright file="AuthenticationResponse.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;

namespace Okta.Auth.Sdk
{
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

        /// <inheritdoc/>
        public IList<Factor> Factors => Embedded.GetArrayProperty<Factor>("factors");

        /// <inheritdoc/>
        public Factor Factor => Embedded.GetProperty<Factor>("factor");

        /// <inheritdoc/>
        public string RecoveryToken => GetStringProperty("recoveryToken");

        /// <inheritdoc/>
        public RecoveryType RecoveryType => GetEnumProperty<RecoveryType>("recoveryType");

        /// <inheritdoc/>
        public FactorType FactorType => GetEnumProperty<FactorType>("factorType");

        /// <inheritdoc/>
        public FactorResult FactorResult => GetEnumProperty<FactorResult>("factorResult");
    }
}
