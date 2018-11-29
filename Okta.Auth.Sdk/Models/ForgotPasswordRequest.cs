// <copyright file="ForgotPasswordRequest.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk.Models
{
    /// <summary>
    /// This class represents the body of the forgot password request
    /// </summary>
    public class ForgotPasswordRequest : Resource
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
        /// Gets or sets the relay state
        /// </summary>
        /// <value>The relay state</value>
        public string RelayState
        {
            get => GetStringProperty("relayState");
            set => this["relayState"] = value;
        }

        /// <summary>
        /// Gets or sets the factor type
        /// </summary>
        /// <value>The factor type</value>
        public string FactorType
        {
            get => GetStringProperty("factorType");
            set => this["factorType"] = value;
        }
    }
}
