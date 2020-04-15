// <copyright file="ForgotPasswordOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// This class contains all the request parameters for performing an forgot password request
    /// </summary>
    public class ForgotPasswordOptions : BaseTrustedOptions
    {
        /// <summary>
        /// Gets or sets the username
        /// </summary>
        /// <value>The username</value>
        public string UserName { get; set; }

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
    }
}
