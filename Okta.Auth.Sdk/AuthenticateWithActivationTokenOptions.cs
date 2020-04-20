// <copyright file="AuthenticateWithActivationTokenOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// This class contains all the request parameters for performing a primary authentication request
    /// </summary>
    public class AuthenticateWithActivationTokenOptions
    {
        /// <summary>
        /// Gets or sets the activation token
        /// </summary>
        /// <value>The activation token</value>
        public string ActivationToken { get; set; }

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
