// <copyright file="IAuthenticationResponse.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using Okta.Sdk.Abstractions;

namespace Okta.Auth.Sdk
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
    }
}
