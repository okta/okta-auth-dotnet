// <copyright file="AuthenticationRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk.Models
{
    /// <summary>
    /// This class represents the body of the authentication request
    /// </summary>
    public class AuthenticationRequest : Resource
    {
        /// <summary>
        /// Gets or sets the username
        /// </summary>
        /// <value>The username</value>
        public string Username
        {
            get => GetStringProperty("username");
            set => this["username"] = value;
        }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        /// <value>The password</value>
        public string Password
        {
            get => GetStringProperty("password");
            set => this["password"] = value;
        }

        /// <summary>
        /// Gets or sets the audience
        /// </summary>
        /// <value>The audience</value>
        public string Audience
        {
            get => GetStringProperty("audience");
            set => this["audience"] = value;
        }

        /// <summary>
        /// Gets or sets the relay state
        /// </summary>
        /// <value>The relay state</value>
        public string RelayState
        {
            get => GetStringProperty("relayState");
            set => this["relayState"] = value;
        }

        /// <summary>
        /// Gets or sets the activation token
        /// </summary>
        /// <value>The activation token</value>
        public string ActivationToken
        {
            get => GetStringProperty("token");
            set => this["token"] = value;
        }

        /// <summary>
        /// Gets or sets the state token
        /// </summary>
        /// <value>The state token</value>
        public string StateToken
        {
            get => GetStringProperty("stateToken");
            set => this["stateToken"] = value;
        }

        /// <summary>
        /// Gets or sets the authentication options
        /// </summary>
        /// <value>The authentication options</value>
        public AuthenticationRequestOptions Options
        {
            get => GetResourceProperty<AuthenticationRequestOptions>("options");
            set => this["options"] = value;
        }

        /// <summary>
        /// Gets or sets the context
        /// </summary>
        /// <value>The context</value>
        public AuthenticationRequestContext Context
        {
            get => GetResourceProperty<AuthenticationRequestContext>("context");
            set => this["context"] = value;
        }
    }
}
