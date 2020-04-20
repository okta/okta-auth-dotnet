// <copyright file="UnlockAccountOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// This class contains all the request parameters for performing an unlock account request
    /// </summary>
    public class UnlockAccountOptions
    {
        /// <summary>
        /// Gets or sets the username
        /// </summary>
        /// <value>The username</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the relay state
        /// </summary>
        /// <value>The relay state</value>
        public string RelayState { get; set; }

        /// <summary>
        /// Gets or sets the factor type
        /// </summary>
        /// <value>The factor type</value>
        public FactorType FactorType { get; set; }

        /// <summary>
        /// Gets or sets the value for `x-forwarded-for` header.
        /// </summary>
        /// <value>The value for `x-forwarded-for` header.</value>
        public string XForwardedFor { get; set; }

        /// <summary>
        /// Gets or sets the user agent.
        /// </summary>
        /// <value>The user agent.</value>
        public string UserAgent { get; set; }
    }
}
