// <copyright file="AuthenticationRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Authn.Abstractions;

namespace Okta.Authn.Models
{
    public class AuthenticationRequest : Resource
    {
        public string Username
        {
            get => GetStringProperty("username");
            set => this["username"] = value;
        }

        public string Password
        {
            get => GetStringProperty("password");
            set => this["password"] = value;
        }

        public string Audience
        {
            get => GetStringProperty("audience");
            set => this["audience"] = value;
        }

        public string RelayState
        {
            get => GetStringProperty("relayState");
            set => this["relayState"] = value;
        }

        public string ActivationToken
        {
            get => GetStringProperty("token");
            set => this["token"] = value;
        }

        public AuthenticationRequestOptions Options
        {
            get => GetResourceProperty<AuthenticationRequestOptions>("options");
            set => this["options"] = value;
        }

        public AuthenticationRequestContext Context
        {
            get => GetResourceProperty<AuthenticationRequestContext>("context");
            set => this["context"] = value;
        }
    }
}
