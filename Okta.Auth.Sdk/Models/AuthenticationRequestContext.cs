// <copyright file="AuthenticationRequestContext.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk.Models
{
    /// <summary>
    /// This class represents the context of the authentication request
    /// </summary>
    public class AuthenticationRequestContext : Resource
    {
        /// <summary>
        /// Gets or sets the device token
        /// </summary>
        /// <value>The device token</value>
        public string DeviceToken
        {
            get => GetStringProperty("deviceToken");
            set => this["deviceToken"] = value;
        }
    }
}
