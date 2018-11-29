// <copyright file="AuthenticationRequestOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk.Models
{
    /// <summary>
    /// This class represents the options of the authentication request
    /// </summary>
    public class AuthenticationRequestOptions : Resource
    {
        /// <summary>
        /// Gets or sets the multiOptionalFactorEnroll flag
        /// </summary>
        /// <remarks><see href="https://developer.okta.com/docs/api/resources/authn#options-object"/></remarks>
        /// <value>The multiOptionalFactorEnroll flag</value>
        public bool? MultiOptionalFactorEnroll
        {
            get => GetBooleanProperty("multiOptionalFactorEnroll");
            set => this["multiOptionalFactorEnroll"] = value;
        }

        /// <summary>
        /// Gets or sets the warnBeforePasswordExpired flag
        /// </summary>
        /// <remarks><see href="https://developer.okta.com/docs/api/resources/authn#options-object"/></remarks>
        /// <value>The warnBeforePasswordExpired flag</value>
        public bool? WarnBeforePasswordExpired
        {
            get => GetBooleanProperty("warnBeforePasswordExpired");
            set => this["warnBeforePasswordExpired"] = value;
        }
    }
}
