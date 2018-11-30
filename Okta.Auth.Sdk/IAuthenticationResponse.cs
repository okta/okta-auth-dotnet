// <copyright file="IAuthenticationResponse.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using Okta.Sdk.Abstractions;

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// Interface for authentication responses
    /// </summary>
    public interface IAuthenticationResponse : IResource
    {
        /// <summary>
        /// Gets the state token
        /// </summary>
        /// <value>The state token</value>
        string StateToken { get; }

        /// <summary>
        /// Gets the session token
        /// </summary>
        /// <value>The session token</value>
        string SessionToken { get; }

        /// <summary>
        /// Gets the relay state
        /// </summary>
        /// <value>The relay state</value>
        string RelayState { get; }

        /// <summary>
        /// Gets the expires at
        /// </summary>
        /// <value>The expires at</value>
        DateTimeOffset? ExpiresAt { get; }

        /// <summary>
        /// Gets the authentication status
        /// </summary>
        /// <value>The authenitcation status</value>
        AuthenticationStatus AuthenticationStatus { get; }

        /// <summary>
        /// Gets the embedded
        /// </summary>
        /// <value>The embedded</value>
        Resource Embedded { get; }

        /// <summary>
        /// Gets the links
        /// </summary>
        /// <value>The links</value>
        Resource Links { get; }
    }
}
