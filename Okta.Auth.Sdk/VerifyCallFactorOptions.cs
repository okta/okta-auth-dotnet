// <copyright file="VerifyCallFactorOptions.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Auth.Sdk
{
    /// <summary>
    /// This class contains all the request parameters for performing a verify call factor request
    /// </summary>
    public class VerifyCallFactorOptions
    {
        /// <summary>
        /// Gets or sets the factor id
        /// </summary>
        /// <value>The factor id</value>
        public string FactorId { get; set; }

        /// <summary>
        /// Gets or sets the state token
        /// </summary>
        /// <value>The state token</value>
        public string StateToken { get; set; }

        /// <summary>
        /// Gets or sets the pass code
        /// </summary>
        /// <value>The pass code</value>
        public string PassCode { get; set; }

        /// <summary>
        /// Gets or sets the remember device flag
        /// </summary>
        /// <value>The remember device flag</value>
        public bool? RememberDevice { get; set; }
    }
}
