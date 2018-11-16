// <copyright file="IAuthenticationResponse.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using Okta.Auth.Sdk.Abstractions;
using Okta.Sdk.Abstractions;

namespace Okta.Auth.Sdk.Models
{
    public interface IAuthenticationResponse : IResource
    {
        string StateToken { get; }

        string SessionToken { get; }

        string RelayState { get; }

        DateTimeOffset? ExpiresAt { get; }

        AuthenticationStatus AuthenticationStatus { get; }

        Resource Embedded { get; }

        Resource Links { get; }

        IList<Factor> Factors { get; }

        /// <summary>
        /// Some times the response has a single factor instead of a list
        /// </summary>
        Factor Factor { get; }

        FactorType FactorType { get; }

        FactorResult FactorResult { get; }

        string RecoveryToken { get; }

        RecoveryType RecoveryType { get; }
    }
}
