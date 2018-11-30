// <copyright file="EnrollU2FFactorOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// This class contains all the request parameters for performing an enroll U2F factor request
    /// </summary>
    public class EnrollU2FFactorOptions
    {
        /// <summary>
        /// Gets or sets the state token
        /// </summary>
        /// <value>The state token</value>
        public string StateToken { get; set; }

        /// <summary>
        /// Gets or sets the provider
        /// </summary>
        /// <value>The provider</value>
        public string Provider { get; set; } = OktaDefaults.FidoProvider;
    }
}
