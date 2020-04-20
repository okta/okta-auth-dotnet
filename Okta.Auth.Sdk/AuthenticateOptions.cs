// <copyright file="AuthenticateOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// This class contains all the request parameters for performing a primary authentication request
    /// </summary>
    public class AuthenticateOptions
    {
        /// <summary>
        /// Gets or sets the username
        /// </summary>
        /// <value>The username</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        /// <value>The password</value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the audience
        /// </summary>
        /// <value>The audience</value>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the relay state
        /// </summary>
        /// <value>The relay state</value>
        public string RelayState { get; set; }

        /// <summary>
        /// Gets or sets the multiOptionalFactorEnroll flag
        /// </summary>
        /// <remarks><see href="https://developer.okta.com/docs/api/resources/authn#options-object"/></remarks>
        /// <value>The multiOptionalFactorEnroll flag</value>
        public bool? MultiOptionalFactorEnroll { get; set; }

        /// <summary>
        /// Gets or sets the warnBeforePasswordExpired flag
        /// </summary>
        /// <remarks><see href="https://developer.okta.com/docs/api/resources/authn#options-object"/></remarks>
        /// <value>The warnBeforePasswordExpired flag</value>
        public bool? WarnBeforePasswordExpired { get; set; }

        /// <summary>
        /// Gets or sets the device token
        /// </summary>
        /// <value>The device token</value>
        public string DeviceToken { get; set; }

        /// <summary>
        /// Gets or sets the device fingerprint
        /// </summary>
        /// <value>The device fingerprint</value>
        public string DeviceFingerprint { get; set; }

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

        /// <summary>
        /// Gets or sets the state token
        /// </summary>
        /// <value>The state token</value>
        public string StateToken { get; set; }
    }
}
