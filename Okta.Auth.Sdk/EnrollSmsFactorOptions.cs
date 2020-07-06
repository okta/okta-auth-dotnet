// <copyright file="EnrollSmsFactorOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// This class contains all the request parameters for performing an enroll SMS factor request
    /// </summary>
    public class EnrollSmsFactorOptions
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
        public string Provider { get; set; } = OktaDefaults.OktaProvider;

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        /// <value>The phone number</value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the phone extension
        /// </summary>
        /// <value>The phone extension</value>
        public string PhoneExtension { get; set; }

        /// <summary>
        /// Gets or sets the factor id
        /// </summary>
        /// /// <value>The factor id</value>
        public string FactorId { get; set; }
    }
}
